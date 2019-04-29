/** @file NIMFriendAPI.cs
  * @brief NIM SDK提供的friend接口
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using NimUtility;
using NIM.Friend.Delegate;


namespace NIM.Friend
{
    public class FriendAPI
    {
        private static FriendInfoChangedDelegate _friendInfoChangedHandler;
        private static GetFriendProfileDelegate _getFriendProfileCompleted;
        public static EventHandler<NIMFriendProfileChangedArgs> FriendProfileChangedHandler;

        private static readonly GetFriendsListDelegate OnGetFriendListCompleted = GetFriendListCompleted;

        [MonoPInvokeCallback(typeof(GetFriendsListDelegate))]
        private static void GetFriendListCompleted(int resCode, string retJson, string je, IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                var friends = string.IsNullOrEmpty(retJson) ? null : NIMFriends.Deserialize(retJson);
                ptr.InvokeOnce<GetFriendsListResultDelegate>(friends);
            }

        }

        public static void RegisterCallbacks()
        {
            _friendInfoChangedHandler = OnFriendInfoChanged;
            _getFriendProfileCompleted = OnGetFriendProfileCompleted;
            RegFriendInfoChangedCb();
        }

        /// <summary>
        ///     获取缓存好友列表
        /// </summary>
        /// <param name="cb"></param>
        public static void GetFriendsList(GetFriendsListResultDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);

            FriendNativeMethods.nim_friend_get_list("", OnGetFriendListCompleted, ptr);
        }

        /// <summary>
        ///     统一注册好友变更通知回调函数（多端同步添加、删除、更新，好友列表同步）
        /// </summary>
        private static void RegFriendInfoChangedCb()
        {
            FriendNativeMethods.nim_friend_reg_changed_cb("", _friendInfoChangedHandler, IntPtr.Zero);
        }

        /// <summary>
        ///     添加、验证好友
        /// </summary>
        /// <param name="accid">对方账号</param>
        /// <param name="verifyType">验证类型</param>
        /// <param name="msg"></param>
        /// <param name="cb">操作结果回调</param>
        public static void ProcessFriendRequest(string accid, NIMVerifyType verifyType, string msg, FriendOperationDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            FriendNativeMethods.nim_friend_request(accid, verifyType, msg, null, ProcessFriendRequestDelegate, ptr);
        }

        private static readonly FriendOperationDelegate ProcessFriendRequestDelegate = OnProcessFriendRequest;

        [MonoPInvokeCallback(typeof(FriendOperationDelegate))]
        private static void OnProcessFriendRequest(int resCode, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<FriendOperationDelegate>(resCode, jsonExtension, IntPtr.Zero);
        }

        /// <summary>
        ///     获取缓存好友信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="cb"></param>
        public static void GetFriendProfile(string accountId, GetFriendProfileResultDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            FriendNativeMethods.nim_friend_get_profile(accountId, null, _getFriendProfileCompleted, ptr);
        }
        [MonoPInvokeCallback(typeof(GetFriendProfileDelegate))]
        private static void OnGetFriendProfileCompleted(string accid, string profileJson, string jsonExtension, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                NIMFriendProfile profile = null;
                if (!string.IsNullOrEmpty(profileJson))
                    profile = NIMFriendProfile.Deserialize(profileJson);
                userData.Invoke<GetFriendProfileResultDelegate>(accid, profile);
            }
        }

        /// <summary>
        ///     删除好友
        /// </summary>
        /// <param name="accid">对方账号</param>
        /// <param name="cb">操作结果回调</param>
        public static void DeleteFriend(string accid, FriendOperationDelegate cb)
        {
            IntPtr ptr = DelegateConverter.ConvertToIntPtr(cb);
            FriendNativeMethods.nim_friend_delete(accid, null, DeleteFriendDelegate, ptr);
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="accid">对方账号</param>
        /// <param name="deleteAlias">是否删除好友备注</param>
        /// <param name="cb">操作结果回调</param>
        public static void DeleteFriend(string accid, bool deleteAlias, FriendOperationDelegate cb)
        {
            IntPtr ptr = DelegateConverter.ConvertToIntPtr(cb);
            string jsonExt = Newtonsoft.Json.JsonConvert.SerializeObject(new { delete_alias = deleteAlias });
            FriendNativeMethods.nim_friend_delete(accid, jsonExt, DeleteFriendDelegate, ptr);
        }

        private static readonly FriendOperationDelegate DeleteFriendDelegate = OnDeleteFriendCompleted;

        [MonoPInvokeCallback(typeof(FriendOperationDelegate))]
        private static void OnDeleteFriendCompleted(int resCode, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<FriendOperationDelegate>(resCode, jsonExtension, IntPtr.Zero);
        }

        /// <summary>
        ///     更新好友资料
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="cb"></param>
        public static void UpdateFriendInfo(NIMFriendProfile profile, FriendOperationDelegate cb)
        {
            if (profile == null || string.IsNullOrEmpty(profile.AccountId))
                throw new ArgumentException("profile or accountid can't be null");
            var jsonParam = profile.Serialize();
            IntPtr ptr = DelegateConverter.ConvertToIntPtr(cb);
            FriendNativeMethods.nim_friend_update(jsonParam, null, UpdateFriendDelegate, ptr);
        }

        private static readonly FriendOperationDelegate UpdateFriendDelegate = OnUpdateFriendCompleted;

        [MonoPInvokeCallback(typeof(FriendOperationDelegate))]
        private static void OnUpdateFriendCompleted(int resCode, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<FriendOperationDelegate>(resCode, jsonExtension, IntPtr.Zero);
        }

        [MonoPInvokeCallback(typeof(FriendInfoChangedDelegate))]
        private static void OnFriendInfoChanged(NIMFriendChangeType type, string resultJson, string jsonExtension, IntPtr userData)
        {
            if (FriendProfileChangedHandler != null)
            {
                INIMFriendChangedInfo IChangedInfo = null;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    switch (type)
                    {
                        case NIMFriendChangeType.kNIMFriendChangeTypeDel:
                            IChangedInfo = FriendDeletedInfo.Deserialize(resultJson);
                            break;
                        case NIMFriendChangeType.kNIMFriendChangeTypeRequest:
                            IChangedInfo = FriendRequestInfo.Deserialize(resultJson);
                            break;
                        case NIMFriendChangeType.kNIMFriendChangeTypeSyncList:
                            IChangedInfo = FriendListSyncInfo.Deserialize(resultJson);
                            break;
                        case NIMFriendChangeType.kNIMFriendChangeTypeUpdate:
                            IChangedInfo = FriendUpdatedInfo.Deserialize(resultJson);
                            break;
                    }
                }
                var args = new NIMFriendProfileChangedArgs(IChangedInfo);
                FriendProfileChangedHandler(null, args);
            }
        }

        /// <summary>
        /// 在本地缓存数据中查询accid是否为自己的好友
        /// </summary>
        /// <param name="accid">好友id</param>
        /// <returns>当正向和反向好友关系都为好友时返回true</returns>
        public static bool IsActiveFriend(string accid)
        {
            return FriendNativeMethods.nim_friend_query_friendship_block(accid, null);
        }
    }
}