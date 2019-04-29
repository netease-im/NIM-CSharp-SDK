using System;

namespace NIMChatRoom
{
    //如果错误码为kResRoomLocalNeedRequestAgain，聊天室重连机制结束，则需要向IM服务器重新请求进入该聊天室权限
    public delegate void RequestChatRoomLinkInfoDelegate(long roomId, NIM.ResponseCode errorCode, string[] linkAddrs);

    public delegate void ChatRoomLoginDelegate(NIMChatRoomLoginStep loginStep, NIM.ResponseCode errorCode, ChatRoomInfo roomInfo, MemberInfo memberInfo);

    public delegate void ExitChatRoomDelegate(long roomId, NIM.ResponseCode errorCode, NIMChatRoomExitReason reason);

    public delegate void QueryMembersResultDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo[] members);

    public delegate void QueryMessageHistoryResultDelegate(long roomId, NIM.ResponseCode errorCode, Message[] messages);

    public delegate void SetMemberPropertyDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo info);

    public delegate void CloseRoomDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void RemoveMemberDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void UpdateRoomInfoDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void UpdateMyRoleDelegate(long roomId, NIM.ResponseCode errorCode);

    public delegate void GetRoomInfoDelegate(long roomId, NIM.ResponseCode errorCode, ChatRoomInfo info);

    public delegate void TempMuteMemberDelegate(long roomId, NIM.ResponseCode errorCode, MemberInfo info);

    public delegate void ChatRoomQueueListDelegate(long room_id, NIM.ResponseCode error_code, string result);

    public delegate void ChatRoomQueueDropDelegate(long room_id, NIM.ResponseCode error_code);

    public delegate void ChatRoomQueuePollDelegate(long room_id, NIM.ResponseCode error_code, string result);

    public delegate void ChatRoomQueueOfferDelegate(long room_id, NIM.ResponseCode error_code);

    public delegate void ChatRoomQueueHeaderDelegate(long room_id,NIM.ResponseCode error_code,string result);

    internal static class CallbackBridge
    {
        public static readonly NimChatroomRequestChatroomLinkInfoCbFunc RequestChatRoomLinkInfoCallback = OnRequestChatRoomLinkInfoCallback;
         [NIM.MonoPInvokeCallback(typeof(NimChatroomRequestChatroomLinkInfoCbFunc))]
        private static void OnRequestChatRoomLinkInfoCallback(long room_id, int error_code, string result, string json_extension, IntPtr user_data)
        {
            string[] addrs = null;
            if (error_code == (int)NIM.ResponseCode.kNIMResSuccess)
            {
                addrs = NimUtility.Json.JsonParser.Deserialize<string[]>(result);
            }

            NimUtility.DelegateConverter.InvokeOnce<RequestChatRoomLinkInfoDelegate>(user_data, room_id, (NIM.ResponseCode)error_code, addrs);
        }

        public static readonly NimChatroomGetMembersCbFunc QueryMembersCallback = OnQueryMembersCompleted;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomGetMembersCbFunc))]
        private static void OnQueryMembersCompleted(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            MemberInfo[] members = null;
            if (errorCode == (int)NIM.ResponseCode.kNIMResSuccess)
            {
                members = NimUtility.Json.JsonParser.Deserialize<MemberInfo[]>(result);
            }
            NimUtility.DelegateConverter.InvokeOnce<QueryMembersResultDelegate>(userData, roomId, (NIM.ResponseCode)errorCode, members);
        }

        public static readonly NimChatroomGetMsgCbFunc QueryMessageLogCallback = OnQueryMessageLogCompleted;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomGetMsgCbFunc))]
        private static void OnQueryMessageLogCompleted(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            Message[] messages = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
            {
                messages = NimUtility.Json.JsonParser.Deserialize<Message[]>(result);
            }
            NimUtility.DelegateConverter.InvokeOnce<QueryMessageHistoryResultDelegate>(userData, roomId, code, messages);
        }

        public static readonly NimChatroomSetMemberAttributeCbFunc SetMemberPropertyCallback = OnSetMemberProperty;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomSetMemberAttributeCbFunc))]
        private static void OnSetMemberProperty(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            MemberInfo mi = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
                mi = NimUtility.Json.JsonParser.Deserialize<MemberInfo>(result);
            NimUtility.DelegateConverter.InvokeOnce<SetMemberPropertyDelegate>(userData, roomId, code, mi);
        }

        public static readonly NimChatroomCloseCbFunc RoomClosedCallback = OnChatRoomClosed;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomCloseCbFunc))]
        private static void OnChatRoomClosed(long roomId, int errorCode, string jsonExtension, IntPtr userData)
        {
            NimUtility.DelegateConverter.InvokeOnce<CloseRoomDelegate>(userData, roomId, (NIM.ResponseCode)errorCode);
        }

        public static readonly NimChatroomGetInfoCbFunc GetRoomInfoCallback = OnGetRoomInfo;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomGetInfoCbFunc))]
        private static void OnGetRoomInfo(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            ChatRoomInfo roomInfo = null;
            var code = (NIM.ResponseCode)errorCode;
            if (code == NIM.ResponseCode.kNIMResSuccess)
                roomInfo = NimUtility.Json.JsonParser.Deserialize<ChatRoomInfo>(result);
            NimUtility.DelegateConverter.InvokeOnce<GetRoomInfoDelegate>(userData, roomId, code, roomInfo);
        }

        public static readonly NimChatroomKickMemberCbFunc KickoutMemberCallback = OnKickoutOthers;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomKickMemberCbFunc))]
        private static void OnKickoutOthers(long roomId, int errorCode, string jsonExtension, IntPtr userData)
        {
            NimUtility.DelegateConverter.InvokeOnce<RemoveMemberDelegate>(userData, roomId, (NIM.ResponseCode)errorCode);
        }

        public static readonly nim_chatroom_temp_mute_member_cb_func TempMuteMemberCallback = OnTemporaryMuteMember;

        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_temp_mute_member_cb_func))]
        private static void OnTemporaryMuteMember(long roomId, int resCode, string result, string jsonExt, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                //var info = MemberInfo.Deserialize(result);
				MemberInfo info = string.IsNullOrEmpty(result) ? null : NimUtility.Json.JsonParser.Deserialize<MemberInfo>(result);
                NimUtility.DelegateConverter.InvokeOnce<TempMuteMemberDelegate>(userData, roomId, (NIM.ResponseCode)resCode, info);
            }
        }

        public static readonly nim_chatroom_update_room_info_cb_func UpdateRoomInfoCallback = OnUpdateRoomInfoResult;
        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_update_room_info_cb_func))]
        private static void OnUpdateRoomInfoResult(long roomId, int resCode,string jsonExt, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<UpdateRoomInfoDelegate>(userData, roomId, (NIM.ResponseCode)resCode);
            }
        }
        public static readonly nim_chatroom_update_my_role_cb_func UpdateMyRoleCallback = OnUpdateMyRoleResult;
        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_update_my_role_cb_func))]
        private static void OnUpdateMyRoleResult(long roomId, int resCode, string jsonExt, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<UpdateMyRoleDelegate>(userData, roomId, (NIM.ResponseCode)resCode);
            }
        }
        public static readonly nim_chatroom_queue_list_cb_func ChatroomQueueListCallback = OnQueryMICList;

        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_queue_list_cb_func))]
        private static void OnQueryMICList(long room_id, NIM.ResponseCode error_code, string result, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueListDelegate>(user_data, room_id, error_code, result);
            }
        }

        public static readonly nim_chatroom_queue_drop_cb_func ChatroomQueueDropCallback = OnDropMICQueue;

        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_queue_drop_cb_func))]
        private static void OnDropMICQueue(long room_id, NIM.ResponseCode error_code, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueDropDelegate>(user_data, room_id, error_code);
            }
        }

        public static readonly nim_chatroom_queue_poll_cb_func ChatroomQueuePollCallback = OnPopMICQueue;


        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_queue_poll_cb_func))]
        private static void OnPopMICQueue(long room_id, NIM.ResponseCode error_code, string result, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueuePollDelegate>(user_data, room_id, error_code, result);
            }
        }

        public static readonly nim_chatroom_queue_offer_cb_func ChatroomQueueOfferCallback = OnQueueOffer;

        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_queue_offer_cb_func))]
        private static void OnQueueOffer(long room_id, NIM.ResponseCode error_code, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueOfferDelegate>(user_data, room_id, error_code);
            }
        }

        public static readonly nim_chatroom_queue_header_cb_func ChatroomQueueHeaderCallback = OnQueueHeader;

        [NIM.MonoPInvokeCallback(typeof(nim_chatroom_queue_header_cb_func))]
        private static void OnQueueHeader(long room_id, int error_code, string result, string json_extension, IntPtr user_data)
        {
            NimUtility.DelegateConverter.InvokeOnce<ChatRoomQueueHeaderDelegate>(user_data, room_id, (NIM.ResponseCode)error_code, result);
        }
    }
}
