/** @file NIMTeamAPI.cs
  * @brief NIM SDK提供的team接口
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using System.Linq;
using NIM.Team.Delegate;

namespace NIM.Team
{
    public delegate void TeamChangedNotificationDelegate(NIMTeamEventData data);

    public delegate void QueryMyTeamsResultDelegate(int count, string[] accountIdList);

    public delegate void QueryMyTeamsInfoResultDelegate(NIMTeamInfo[] infoList);

    public delegate void QueryTeamMembersInfoResultDelegate(string tid, int memberCount, bool includeUserInfo,NIMTeamMemberInfo[] infoList);

    public delegate void QuerySingleMemberResultDelegate(NIMTeamMemberInfo info);

    public delegate void QueryCachedTeamInfoResultDelegate(string tid, NIMTeamInfo info);

    public delegate void QueryTeamMutedListDelegate(ResponseCode res,int count,string tid, NIMTeamMemberInfo[] members);

    public delegate void QueryMyInfoInEachTeamDelegate(List<NIMTeamMemberInfo> infoList);

    public class TeamAPI
    {
        private static TeamEventDelegate _teamEventNotificationDelegate;
        private static TeamOperationDelegate _teamChangedCallback;
        private static QueryMyTeamsDelegate _queryAllMyTeamsCompleted;
        private static QueryMyTeamsDetailInfoDelegate _queryMyTeamsInfoCompleted;
        private static QueryMyTeamsDetailInfoDelegate _queryAllMyTeamsInfoCompleted;
        private static QueryTeamMembersDelegate _queryTeamMembersCompleted;
        private static QuerySingleMemberDelegate _querySingleMemberCompleted;
        private static QueryTeamInfoDelegate _queryCachedTeamInfoCompleted;

        /// <summary>
        /// 群通知事件，注册该事件监听群信息变更
        /// </summary>
        public static EventHandler<NIMTeamEventArgs> TeamEventNotificationHandler;

        public static void RegisterCallbacks()
        {
            _teamEventNotificationDelegate = new TeamEventDelegate(NotifyTeamEvent);
            _teamChangedCallback = new TeamOperationDelegate(OnTeamChanged);
            _queryAllMyTeamsCompleted = new QueryMyTeamsDelegate(OnQueryAllMyTeamsCompleted);
            _queryMyTeamsInfoCompleted = new QueryMyTeamsDetailInfoDelegate(OnQueryMyTeamsInfoCompleted);
            _queryAllMyTeamsInfoCompleted = new QueryMyTeamsDetailInfoDelegate(OnQueryAllMyTeamsInfoCompleted);
            _queryTeamMembersCompleted = new QueryTeamMembersDelegate(OnQueryTeamMembersInfoCompleted);
            _querySingleMemberCompleted = new QuerySingleMemberDelegate(OnQuerySingleMemberCompleted);
            _queryCachedTeamInfoCompleted = new QueryTeamInfoDelegate(OnQueryCachedTeamInfoCompleted);
            TeamNativeMethods.nim_team_reg_team_event_cb(null, _teamEventNotificationDelegate, IntPtr.Zero);
        }
        [MonoPInvokeCallback(typeof(TeamEventDelegate))]
        private static void NotifyTeamEvent(int resCode, int nid, string tid, string result, string jsonExtension, IntPtr userData)
        {
            if (TeamEventNotificationHandler != null)
            {
                NIMTeamEventData eventData = ParseTeamEventData(resCode, nid, tid, result);
                if (eventData != null)
                    TeamEventNotificationHandler.Invoke(null, new NIMTeamEventArgs(eventData));
            }
        }

        private static NIMTeamEventData ParseTeamEventData(int resCode, int nid, string tid, string result)
        {
            NIMTeamEventData eventData = null;
            NIMNotificationType notificationType= (NIMNotificationType)Enum.Parse(typeof(NIMNotificationType), nid.ToString());
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    eventData = NIMTeamEventData.Deserialize(result);
                    if (eventData != null)
                    {
                        if (notificationType.Equals(NIMNotificationType.kNIMNotificationIdLocalGetTeamMsgUnreadCount) ||
                       notificationType.Equals(NIMNotificationType.kNIMNotificationIdLocalGetTeamMsgUnreadList))
                        {
                            NIMTeamEventDataObjects eventDataObjects = NIMTeamEventDataObjects.Deserialize(result);
                            if (eventDataObjects != null)
                                eventData.TeamEvents = eventDataObjects.TeamEventInfos;
                        }
                        else
                        {
                            NIMTeamEventDataObject eventDataObject = NIMTeamEventDataObject.Deserialize(result);
                            if (eventDataObject!=null&&eventDataObject.TeamEvent != null)
                            {
                                eventData.TeamEvent = eventDataObject.TeamEvent;
                            }
                            else
                            {
                                eventData.TeamEvent = new NIMTeamEvent();
                            }
                           
                            eventData.TeamEvent.TeamId = tid;
                            eventData.TeamEvent.ResponseCode = (ResponseCode)Enum.Parse(typeof(ResponseCode), resCode.ToString());
                            eventData.TeamEvent.NotificationType = notificationType;
                        }
                    }
                   
                }
                catch
                {
                    eventData = new NIMTeamEventData();
                    eventData.JSON = result;
                }
            }
            if (eventData == null)
            {
                eventData = new NIMTeamEventData();
                eventData.JSON = result;
            }
            eventData.NotificationId = notificationType;
            
            return eventData;
        }

        /// <summary>
        /// 创建群
        /// </summary>
        /// <param name="teamInfo">群组信息</param>
        /// <param name="idList">成员id列表(不包括自己)</param>
        /// <param name="postscript">附言</param>
        /// <param name="action"></param>
        public static void CreateTeam(NIMTeamInfo teamInfo, string[] idList, string postscript, TeamChangedNotificationDelegate action)
        {
            var tinfoJson = teamInfo.Serialize();
            var idJson = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_create_team_async(tinfoJson, idJson, postscript, null, _teamChangedCallback, ptr);
        }
        [MonoPInvokeCallback(typeof(TeamOperationDelegate))]
        private static void OnTeamChanged(int resCode, int nid, string tid, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                NIMTeamEventData eventData = ParseTeamEventData(resCode, nid, tid, result);
                NimUtility.DelegateConverter.InvokeOnce<TeamChangedNotificationDelegate>(userData, eventData);
            }
        }

        /// <summary>
        /// 邀请好友入群
        /// </summary>
        /// <param name="tid">群id</param>
        /// <param name="idList">被邀请人员id列表</param>
        /// <param name="postscript">邀请附言</param>
        /// <param name="action">操作结果回调</param>
        public static void Invite(string tid, string[] idList, string postscript, TeamChangedNotificationDelegate action)
        {
            var idJson = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_invite_async(tid, idJson, postscript, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 邀请好友入群
        /// </summary>
        /// <param name="tid">群组id</param>
        /// <param name="idList">被邀请人员id列表</param>
        /// <param name="postscript">邀请附言</param>
        /// <param name="attachment">用户可自定义的补充邀请信息</param>
        /// <param name="action">操作结果回调</param>
        public static void InviteEx(string tid,string[]idList,string postscript,string attachment, TeamChangedNotificationDelegate action)
        {
            var idJson = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_invite_async2(tid, idJson, postscript,attachment,null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 将用户踢下线
        /// </summary>
        /// <param name="tid">群id</param>
        /// <param name="idList">被踢用户id 列表</param>
        /// <param name="action"></param>
        public static void KickMemberOutFromTeam(string tid, string[] idList, TeamChangedNotificationDelegate action)
        {
            var idJson = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_kick_async(tid, idJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 离开群
        /// </summary>
        /// <param name="tid">群id</param>
        /// <param name="action">操作结果回调函数</param>
        public static void LeaveTeam(string tid, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_leave_async(tid, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 解散群组
        /// </summary>
        /// <param name="tid">群id</param>
        /// <param name="action">操作结果回调函数</param>
        public static void DismissTeam(string tid, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_dismiss_async(tid, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 更新群信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="info"></param>
        /// <param name="action"></param>
        public static void UpdateTeamInfo(string tid, NIMTeamInfo info, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            var infoJson = info.Serialize();
            TeamNativeMethods.nim_team_update_team_info_async(tid, infoJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 申请入群
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="reason"></param>
        /// <param name="action"></param>
        public static void ApplyForJoiningTeam(string tid, string reason, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_apply_join_async(tid, reason, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 同意入群申请
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="action"></param>
        public static void AgreeJoinTeamApplication(string tid, string uid, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_pass_join_apply_async(tid, uid, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 拒绝入群申请
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="reason"></param>
        /// <param name="action"></param>
        public static void RejectJoinTeamApplication(string tid, string uid, string reason, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_reject_join_apply_async(tid, uid, reason, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 添加群管理员
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="managerIdArray"></param>
        /// <param name="action"></param>
        public static void AddTeamManagers(string tid, string[] managerIdArray, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            var managersJson = NimUtility.Json.JsonParser.Serialize(managerIdArray);
            TeamNativeMethods.nim_team_add_managers_async(tid, managersJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 删除群管理员
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="managerIdArray"></param>
        /// <param name="action"></param>
        public static void RemoveTeamManagers(string tid, string[] managerIdArray, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            var managersJson = NimUtility.Json.JsonParser.Serialize(managerIdArray);
            TeamNativeMethods.nim_team_remove_managers_async(tid, managersJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 移交群主
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="newOwnerId"></param>
        /// <param name="leaveTeam">是否在移交后退出群</param>
        /// <param name="action"></param>
        public static void TransferTeamAdmin(string tid, string newOwnerId, bool leaveTeam, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_transfer_team_async(tid, newOwnerId, leaveTeam, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 更新自己的群属性
        /// </summary>
        /// <param name="info"></param>
        /// <param name="action"></param>
        public static void UpdateMyTeamProperty(NIMTeamMemberInfo info, TeamChangedNotificationDelegate action)
        {
            var infoJson = info.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_update_my_property_async(infoJson, null, _teamChangedCallback, ptr);

        }

        /// <summary>
        /// 修改其他成员的群昵称
        /// </summary>
        /// <param name="info"></param>
        /// <param name="action"></param>
        public static void UpdateMemberNickName(NIMTeamMemberInfo info, TeamChangedNotificationDelegate action)
        {
            var infoJson = info.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_update_other_nick_async(infoJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 接受入群邀请
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="invitor"></param>
        /// <param name="action"></param>
        public static void AcceptTeamInvitation(string tid, string invitor, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_accept_invitation_async(tid, invitor, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 拒绝入群邀请
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="invitor"></param>
        /// <param name="reason"></param>
        /// <param name="action"></param>
        public static void RejectTeamInvitation(string tid, string invitor, string reason, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_reject_invitation_async(tid, invitor, reason, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 查询自己的群
        /// </summary>
        /// <param name="action"></param>
        public static void QueryAllMyTeams(QueryMyTeamsResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_query_all_my_teams_async(null, _queryAllMyTeamsCompleted, ptr);

        }
        [MonoPInvokeCallback(typeof(QueryMyTeamsDelegate))]
        private static void OnQueryAllMyTeamsCompleted(int teamCount, string result, string jsonExtension, IntPtr userData)
        {
            string[] idList = null;
            if (!string.IsNullOrEmpty(result))
            {
                idList = NimUtility.Json.JsonParser.Deserialize<string[]>(result);
            }
            NimUtility.DelegateConverter.InvokeOnce<QueryMyTeamsResultDelegate>(userData, teamCount, idList);
        }

        /// <summary>
        /// 查询所有有效群信息
        /// </summary>
        /// <param name="action"></param>
        public static void QueryMyValidTeamsInfo(QueryMyTeamsInfoResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            Dictionary<string, object> extDic = new Dictionary<string, object>();
            extDic["include_invalid"] = false;
            var ext = NimUtility.Json.JsonParser.Serialize(extDic);
            TeamNativeMethods.nim_team_query_all_my_teams_info_async(ext, _queryMyTeamsInfoCompleted, ptr);
        }

        /// <summary>
        /// 查询所有群信息，包含无效的群
        /// </summary>
        /// <param name="action"></param>
        public static void QueryAllMyTeamsInfo(QueryMyTeamsInfoResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            Dictionary<string, object> extDic = new Dictionary<string, object>();
            extDic["include_invalid"] = true;
            var ext = NimUtility.Json.JsonParser.Serialize(extDic);
            TeamNativeMethods.nim_team_query_all_my_teams_info_async(ext, _queryAllMyTeamsInfoCompleted, ptr);
        }

        [MonoPInvokeCallback(typeof(QueryMyTeamsDetailInfoDelegate))]
        private static void OnQueryMyTeamsInfoCompleted(int teamCount, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var infoList = NimUtility.Json.JsonParser.Deserialize<List<NIMTeamInfo>>(result);
                if (infoList != null)
                {
                    //var validTeams = infoList.Where((info) => { return info.TeamValid; });
					NimUtility.DelegateConverter.InvokeOnce<QueryMyTeamsInfoResultDelegate>(userData, (object)infoList.ToArray());
                }
                else
                {
                    NimUtility.DelegateConverter.InvokeOnce<QueryMyTeamsInfoResultDelegate>(userData, (object)new NIMTeamInfo[] { });
                }
            }

        }

        [MonoPInvokeCallback(typeof(QueryMyTeamsDetailInfoDelegate))]
        private static void OnQueryAllMyTeamsInfoCompleted(int teamCount, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var infoList = NimUtility.Json.JsonParser.Deserialize<List<NIMTeamInfo>>(result);
                if (infoList != null)
                {
                    NimUtility.DelegateConverter.InvokeOnce<QueryMyTeamsInfoResultDelegate>(userData, (object)infoList.ToArray());
                }
                else
                {
                    NimUtility.DelegateConverter.InvokeOnce<QueryMyTeamsInfoResultDelegate>(userData, (object)new NIMTeamInfo[] { });
                }
            }

        }

        /// <summary>
        /// 查询群成员信息
        /// </summary>
        /// <param name="tid">群ID</param>
        /// <param name="action"></param>
        public static void QueryTeamMembersInfo(string tid, QueryTeamMembersInfoResultDelegate action)
        {
            QueryTeamMembersInfo(tid, true, false, action);
        }

        /// <summary>
        ///查询群成员信息 
        /// </summary>
        /// <param name="tid">群ID</param>
        /// <param name="includeMemberInfo">是否查询成员详细信息</param>
        /// <param name="includeInvalidMember">是否包含无效成员</param>
        /// <param name="action"></param>
        public static void QueryTeamMembersInfo(string tid, bool includeMemberInfo, bool includeInvalidMember, QueryTeamMembersInfoResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            Dictionary<string, object> extDic = new Dictionary<string, object>();
            extDic["include_invalid"] = includeInvalidMember;
            var ext = NimUtility.Json.JsonParser.Serialize(extDic);
            TeamNativeMethods.nim_team_query_team_members_async(tid, includeMemberInfo, ext, _queryTeamMembersCompleted, ptr);
        }

        [MonoPInvokeCallback(typeof(QueryTeamMembersDelegate))]
        private static void OnQueryTeamMembersInfoCompleted(string tid, int memberCount, bool includeUserInfo, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var membersInfo = NimUtility.Json.JsonParser.Deserialize<NIMTeamMemberInfo[]>(result);
                NimUtility.DelegateConverter.InvokeOnce<QueryTeamMembersInfoResultDelegate>(userData, tid,memberCount,includeUserInfo,(object)membersInfo);
            }
        }

        /// <summary>
        /// 查询(单个)群成员信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <param name="action"></param>
        public static void QuerySingleMemberInfo(string tid, string uid, QuerySingleMemberResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_query_team_member_async(tid, uid, null, _querySingleMemberCompleted, ptr);
        }

        /// <summary>
        /// 查询(单个)群成员信息(同步版本，堵塞NIM内部线程，谨慎使用)
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static NIMTeamMemberInfo QuerySingleMemberInfo(string tid, string uid)
        {
            NIMTeamMemberInfo info = null;
            var ptr = TeamNativeMethods.nim_team_query_team_member_block(tid, uid);
            if (ptr != IntPtr.Zero)
            {
                NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
                var infoObj = marshaler.MarshalNativeToManaged(ptr);
                info = NIMTeamMemberInfo.Deserialize(infoObj.ToString());
                GlobalAPI.FreeStringBuffer(ptr);
            }
            return info;
        }
        [MonoPInvokeCallback(typeof(QuerySingleMemberDelegate))]
        private static void OnQuerySingleMemberCompleted(string tid, string userId, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var info = NIMTeamMemberInfo.Deserialize(result);
                NimUtility.DelegateConverter.InvokeOnce<QuerySingleMemberResultDelegate>(userData, info);
            }
        }

        /// <summary>
        /// 查询本地缓存的群信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="action"></param>
        public static void QueryCachedTeamInfo(string tid, QueryCachedTeamInfoResultDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_query_team_info_async(tid, null, _queryCachedTeamInfoCompleted, ptr);
        }
        [MonoPInvokeCallback(typeof(QueryTeamInfoDelegate))]
        private static void OnQueryCachedTeamInfoCompleted(string tid, string result, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var tinfo = NIMTeamInfo.Deserialize(result);
                NimUtility.DelegateConverter.InvokeOnce<QueryCachedTeamInfoResultDelegate>(userData, tid, tinfo);
            }
        }

        /// <summary>
        /// 在线查询群信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="action"></param>
        public static void QueryTeamInfoOnline(string tid, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_query_team_info_online_async(tid, null, _teamChangedCallback, ptr);
        }

        public static void SetMemberMuted(string tid, string memberId, bool muted, TeamChangedNotificationDelegate action)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(action);
            TeamNativeMethods.nim_team_mute_member_async(tid, memberId, muted, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 本地查询群信息(同步版本，堵塞NIM内部线程，谨慎使用)
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static NIMTeamInfo QueryCachedTeamInfo(string tid)
        {
            var ptr = TeamNativeMethods.nim_team_query_team_info_block(tid);
            if (ptr != IntPtr.Zero)
            {
                NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
                var tobj = marshaler.MarshalNativeToManaged(ptr);
                var tinfo = NIMTeamInfo.Deserialize(tobj.ToString());
                GlobalAPI.FreeStringBuffer(ptr);
                return tinfo;
            }
            return null;
        }
        
        /// <summary>
        /// 获取群禁言成员列表
        /// </summary>
        /// <param name="tid">群组id</param>
        /// <param name="cb">回调函数</param>
        public static void QueryMutedListOnlineAsync(string tid, QueryTeamMutedListDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            TeamNativeMethods.nim_team_query_mute_list_online_async(tid, null, QueryMutedListOnlineCb, ptr);
        }

        private static readonly nim_team_query_mute_list_cb_func QueryMutedListOnlineCb = OnQueryMutedlistCompleted;

        [MonoPInvokeCallback(typeof(nim_team_query_mute_list_cb_func))]
        private static void OnQueryMutedlistCompleted(int res_code, int member_count, string tid, string result, string json_extension, IntPtr user_data)
        {
            var members = NimUtility.Json.JsonParser.Deserialize<NIMTeamMemberInfo[]>(result);
            NimUtility.DelegateConverter.InvokeOnce<QueryTeamMutedListDelegate>(user_data, (ResponseCode)res_code, member_count, tid, members);
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

        private static nim_team_query_my_all_member_infos_cb_func QueryMyAllMemberInfoCallback = OnQueryMyAllMemberInfoCompleted;

        [MonoPInvokeCallback(typeof(nim_team_query_my_all_member_infos_cb_func))]
        private static void OnQueryMyAllMemberInfoCompleted(int team_count, string result, string json_extension, IntPtr user_data)
        {
            var list = NimUtility.Json.JsonParser.Deserialize<List<NIMTeamMemberInfo>>(result);
            NimUtility.DelegateConverter.InvokeOnce<QueryMyInfoInEachTeamDelegate>(user_data, (object)list);
        }

        /// <summary>
        /// 在自己加的所有群里，查找自己在每个群里的成员信息
        /// </summary>
        /// <param name="cb"></param>
        public static void QueryMyInfoInEachTeam(QueryMyInfoInEachTeamDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            TeamNativeMethods.nim_team_query_my_all_member_infos_async(null, QueryMyAllMemberInfoCallback, ptr);
        }

        /// <summary>
        /// 对群禁言/解除禁言
        /// </summary>
        /// <param name="tid">群组ID</param>
        /// <param name="value">禁言(true)或解除禁言(false)</param>
        /// <param name="cb">操作结果回调</param>
        public static void MuteTeam(string tid,bool value, TeamChangedNotificationDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            TeamNativeMethods.nim_team_mute_async(tid, value, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 发送群消息已读回执
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="msgs"></param>
        /// <param name="cb"></param>
        public static void MsgAckRead(string tid,List<NIMIMMessage> msgs, TeamChangedNotificationDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var msgJson = NimUtility.Json.JsonParser.Serialize(msgs);
            TeamNativeMethods.nim_team_msg_ack_read(tid, msgJson, null, _teamChangedCallback, ptr);
        }

        /// <summary>
        /// 获取群消息未读成员列表
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="msg"></param>
        /// <param name="cb"></param>
        public static void QueryMsgUnreadList(string tid,NIMIMMessage msg,TeamChangedNotificationDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var msgJson = msg.Serialize();
            TeamNativeMethods.nim_team_msg_query_unread_list(tid, msgJson, null, _teamChangedCallback, ptr);
        }

#endif
    }
}
