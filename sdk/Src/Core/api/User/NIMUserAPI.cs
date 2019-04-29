/** @file NIMUserAPI.cs
  * @brief NIM SDK提供的用户相关接口
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NimUtility;
using NimUtility.Json;
using NIM.User.Delegate;

namespace NIM.User
{
    /// <summary>
    ///     获取好友关系结果回调
    /// </summary>
    /// <param name="code"></param>
    /// <param name="list"></param>
    public delegate void GetUserRelationshipResuleDelegate(ResponseCode code, UserSpecialRelationshipItem[] list);

    /// <summary>
    ///     获取用户名片结果回调
    /// </summary>
    /// <param name="list"></param>
    public delegate void GetUserNameCardResultDelegate(UserNameCard[] list);

    /// <summary>
    ///     更新名片结果回调
    /// </summary>
    /// <param name="response"></param>
    public delegate void UpdateNameCardResultDelegate(ResponseCode response);

    public class UserAPI
    {
        private static SyncMutedAndBlacklistDelegate _syncMutedAndBlacklistCompleted;
        private static UserNameCardChangedDelegate _userNameCardChanged;
        private static GetUserNameCardDelegate _getUserNameCardCompleted;
        private static UpdateUserNameCardDelegate _updateNameCardCompleted;

        public static EventHandler<UserNameCardChangedArgs> UserNameCardChangedHandler;
        public static EventHandler<UserRelationshipSyncArgs> UserRelationshipListSyncHander;
        public static EventHandler<UserRelationshipChangedArgs> UserRelationshipChangedHandler;

        private static readonly UserSpecialRelationshipChangedDelegate OnRelationshipChanged = OnRelationshipChangedCallback;

        [MonoPInvokeCallback(typeof(UserSpecialRelationshipChangedDelegate))]
        static void OnRelationshipChangedCallback(NIMUserRelationshipChangeType type, string result, string je, IntPtr ptr)
        {
            if (type == NIMUserRelationshipChangeType.SyncMuteAndBlackList)
            {
                if (UserRelationshipListSyncHander != null)
                {
                    UserSpecialRelationshipItem[] items = null;
                    if (!string.IsNullOrEmpty(result))
                        items = JsonParser.Deserialize<UserSpecialRelationshipItem[]>(result);
                    var args = new UserRelationshipSyncArgs(items);
                    UserRelationshipListSyncHander(null, args);
                }
            }
            else
            {
                if (UserRelationshipChangedHandler != null)
                {
                    var obj = JObject.Parse(result);
                    var idToken = obj.SelectToken("accid");
                    var valueToken = obj.SelectToken(type == NIMUserRelationshipChangeType.AddRemoveBlacklist ? "black" : "mute");
                    var id = idToken.ToObject<string>();
                    var value = valueToken.ToObject<bool>();
                    var args = new UserRelationshipChangedArgs(type, id, value);
                    UserRelationshipChangedHandler(null, args);
                }
            }
        }

        internal static void RegisterCallbacks()
        {
            _syncMutedAndBlacklistCompleted = OnGetUserRelationshipCompleted;
            _userNameCardChanged = OnUserNameCardChanged;
            _getUserNameCardCompleted = OnGetUserNameCardCompleted;
            _updateNameCardCompleted = OnNameCardUpdated;
            RegSpecialRelationshipChangedCb();
            RegUserNameCardChangedCb();
        }

        /// <summary>
        ///     统一注册用户属性变更通知回调函数（本地、多端同步黑名单、静音名单变更）
        /// </summary>
        /// <param name="cb">操作结果回调</param>
        private static void RegSpecialRelationshipChangedCb()
        {
            UserNativeMethods.nim_user_reg_special_relationship_changed_cb(null, OnRelationshipChanged, IntPtr.Zero);
        }

        /// <summary>
        ///     设置、取消设置黑名单.
        /// </summary>
        /// <param name="accountId"> 好友id.</param>
        /// <param name="inBlacklist">if set to <c>true</c> [set_black].</param>
        /// <param name="cb">操作结果回调.</param>
        public static void SetBlacklist(string accountId, bool inBlacklist, UserOperationDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            UserNativeMethods.nim_user_set_black(accountId, inBlacklist, null, ModifyBlacklistDelegate, ptr);
        }

        private static readonly UserOperationDelegate ModifyBlacklistDelegate = OnModifyBlacklist;

        [MonoPInvokeCallback(typeof(UserOperationDelegate))]
        private static void OnModifyBlacklist(ResponseCode response, string accid, bool opt, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<UserOperationDelegate>(response, accid, opt, jsonExtension, IntPtr.Zero);
        }

        /// <summary>
        ///     设置、取消设置静音名单
        /// </summary>
        /// <param name="accountId">好友id</param>
        /// <param name="isMuted">取消或设置</param>
        /// <param name="cb">操作结果回调</param>
        public static void SetUserMuted(string accountId, bool isMuted, UserOperationDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            UserNativeMethods.nim_user_set_mute(accountId, isMuted, null, ModifyMutedlistDelegate, ptr);
        }

        private static readonly UserOperationDelegate ModifyMutedlistDelegate = OnModifyMutedlist;

        [MonoPInvokeCallback(typeof(UserOperationDelegate))]
        private static void OnModifyMutedlist(ResponseCode response, string accid, bool opt, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<UserOperationDelegate>(response, accid, opt, jsonExtension, IntPtr.Zero);
        }

        /// <summary>
        ///     获取用户关系列表(黑名单和静音列表)
        /// </summary>
        /// <param name="resultDelegate"></param>
        public static void GetRelationshipList(GetUserRelationshipResuleDelegate resultDelegate)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(resultDelegate);
            UserNativeMethods.nim_user_get_mute_blacklist("Blacklist", _syncMutedAndBlacklistCompleted, ptr);
        }
        [MonoPInvokeCallback(typeof(SyncMutedAndBlacklistDelegate))]
        private static void OnGetUserRelationshipCompleted(ResponseCode response, string blacklistJson, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                var items = JsonParser.Deserialize<UserSpecialRelationshipItem[]>(blacklistJson);
                userData.InvokeOnce<GetUserRelationshipResuleDelegate>(response, (object)items);
            }
        }

        /// <summary>
        ///     统一注册用户名片变更通知回调函数
        /// </summary>
        private static void RegUserNameCardChangedCb()
        {
            UserNativeMethods.nim_user_reg_user_name_card_changed_cb(null, _userNameCardChanged, IntPtr.Zero);
        }

        [MonoPInvokeCallback(typeof(UserNameCardChangedDelegate))]
        private static void OnUserNameCardChanged(string resultJson, string jsonExtension, IntPtr userData)
        {
            if (string.IsNullOrEmpty(resultJson))
                return;
            var cards = JsonParser.Deserialize<List<UserNameCard>>(resultJson);
            if (UserNameCardChangedHandler != null)
            {
                UserNameCardChangedHandler(null, new UserNameCardChangedArgs(cards));
            }
        }

        /// <summary>
        ///     获取本地的指定帐号的用户名片
        /// </summary>
        /// <param name="accountIdList"></param>
        /// <param name="resultDelegate"></param>
        public static void GetUserNameCard(List<string> accountIdList, GetUserNameCardResultDelegate resultDelegate)
        {
            var idJsonParam = JsonParser.Serialize(accountIdList);
            var ptr = DelegateConverter.ConvertToIntPtr(resultDelegate);
            UserNativeMethods.nim_user_get_user_name_card(idJsonParam, null, _getUserNameCardCompleted, ptr);
        }

        [MonoPInvokeCallback(typeof(GetUserNameCardDelegate))]
        private static void OnGetUserNameCardCompleted(string resultJson, string jsonExtension, IntPtr userData)
        {
            var cards = JsonParser.Deserialize<UserNameCard[]>(resultJson);
            userData.InvokeOnce<GetUserNameCardResultDelegate>((object)cards);
        }

        /// <summary>
        ///     在线查询指定帐号的用户名片
        /// </summary>
        /// <param name="accountIdList"></param>
        /// <param name="resultDelegate"></param>
        public static void QueryUserNameCardOnline(List<string> accountIdList, GetUserNameCardResultDelegate resultDelegate)
        {
            var idJsonParam = JsonParser.Serialize(accountIdList);
            var ptr = DelegateConverter.ConvertToIntPtr(resultDelegate);
            UserNativeMethods.nim_user_get_user_name_card_online(idJsonParam, null, _getUserNameCardCompleted, ptr);
        }

        /// <summary>
        ///     更新用户名片
        /// </summary>
        /// <param name="card"></param>
        /// <param name="d"></param>
        public static void UpdateMyCard(UserNameCard card, UpdateNameCardResultDelegate d)
        {
            var json = card.Serialize();
            var ptr = DelegateConverter.ConvertToIntPtr(d);
            UserNativeMethods.nim_user_update_my_user_name_card(json, null, _updateNameCardCompleted, ptr);
        }

        [MonoPInvokeCallback(typeof(UpdateUserNameCardDelegate))]
        private static void OnNameCardUpdated(ResponseCode response, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<UpdateNameCardResultDelegate>(response);
        }

        /// <summary>
        /// 更新推送证书名和token
        /// </summary>
        /// <param name="certificateName">在云信管理后台配置的证书名</param>
        /// <param name="token">推送token</param>
        /// <param name="type">仅iOS需要 1 表示pushkit，0 表示apns</param>
        public static void UpdatePushToken(string certificateName,string token,int type)
        {
            UserNativeMethods.nim_user_update_push_token(certificateName, token, type);
        }
    }
}