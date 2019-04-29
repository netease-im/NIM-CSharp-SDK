using System;
using System.Collections.Generic;

namespace NIMChatRoom
{
    /// <summary>
    /// 聊天室
    /// </summary>
    public class ChatRoomApi
    {
        /// <summary>
        /// 登录聊天室事件
        /// </summary>
        public static event ChatRoomLoginDelegate LoginHandler;

        /// <summary>
        /// 退出聊天室事件
        /// </summary>
        public static event ExitChatRoomDelegate ExitHandler;

        /// <summary>
        /// 聊天室连接状态更改委托
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <param name="state">连接状态</param>
        public delegate void LinkStateChangedDelegate(long roomId, NIMChatRoomLinkCondition state);

        /// <summary>
        /// 聊天室连接状态更改事件
        /// </summary>
        public static event LinkStateChangedDelegate LinkStateChanged;

        /// <summary>
        /// 接收到聊天室消息委托
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="message"></param>
        public delegate void ReceiveMessageDelegate(long roomId, Message message);

        /// <summary>
        /// 接收聊天室消息事件
        /// </summary>
        public static event ReceiveMessageDelegate ReceiveMessageHandler;

        /// <summary>
        /// 发送聊天室消息委托
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public delegate void SendMessageDelegate(long roomId, NIM.ResponseCode code, Message message);

        /// <summary>
        /// 发送聊天室消息事件
        /// </summary>
        public static event SendMessageDelegate SendMessageHandler;

        /// <summary>
        /// 接收聊天室通知委托
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="notification"></param>
        public delegate void ReceiveNotificationDelegate(long roomId, Notification notification);

        /// <summary>
        /// 接收聊天室通知事件
        /// </summary>
        public static event ReceiveNotificationDelegate ReceiveNotificationHandler;

        private static bool _chatRoomInitialized = false;
        /// <summary>
        /// 初始化聊天实模块
        /// </summary>
        public static void Init(NIMChatRoomConfig config = null)
        {
            if (_chatRoomInitialized)
                return;
            try
            {
                string json = config != null ? config.Serialize() : "";
                ChatRoomNativeMethods.nim_chatroom_init(json);
                RegisterLoginCallback();
                RegisterExitChatRoomCallback();
                RegisterLinkStateChangedCallback();
                RegisterReceiveMsgCallback();
                RegisterReceiveNotificationMsgCallback();
                RegisterSendMsgArcCallback();
            }
            catch(Exception e)
            {
                _chatRoomInitialized = false;
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            _chatRoomInitialized = true;
        }
        /// <summary>
        /// 获取匿名登录聊天室地址(仅供demo测试使用，用户需要自己的应用服务器来获取聊天室地址)
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="appKey"></param>
        /// <param name="callback"></param>
        public static void RequestLinkInfoWithAnonymous(long roomId,string appKey, RequestChatRoomLinkInfoDelegate callback)
        {
            if (string.IsNullOrEmpty(appKey))
                return;
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(callback);
            ChatRoomNativeMethods.nim_chatroom_request_link_service_with_anonymous(roomId, appKey, null, CallbackBridge.RequestChatRoomLinkInfoCallback, ptr);
        }


        public static void LoginWithAnonymous(long roomId, AnonymousInfo anonymityInfo, LoginData loginData = null)
        {
            string loginJson = string.Empty;
            if (loginData != null)
            {
                loginJson = loginData.Serialize();
            }

            if (anonymityInfo == null)
            {
                return;
            }

            string anonymity_info = anonymityInfo.Serialize();

            ChatRoomNativeMethods.nim_chatroom_enter_with_anoymity(roomId, anonymity_info, loginJson, null);
        }

        /// <summary>
        /// 登陆聊天室
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="request">聊天室登录信息(NIM SDK请求聊天室返回的数据)</param>
        /// <param name="loginData">聊天室可选信息</param>
        public static void Login(long roomId, string request, LoginData loginData = null)
        {
            string loginJson = string.Empty;
            if (loginData != null)
            {
                loginJson = loginData.Serialize();
            }
            ChatRoomNativeMethods.nim_chatroom_enter(roomId, request, loginJson, string.Empty);
        }

        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        public static void Exit(long roomId)
        {
            ChatRoomNativeMethods.nim_chatroom_exit(roomId, null);
        }

        /// <summary>
        /// 清理聊天室模块
        /// </summary>
        public static void Cleanup()
        {
            if (_chatRoomInitialized)
            {
                ChatRoomNativeMethods.nim_chatroom_cleanup(null);
                _chatRoomInitialized = false;
            }
        }

        private static readonly NimChatroomLoginCbFunc ParaseChatRoomLoginResult = OnLogin;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomLoginCbFunc))]
        private static void OnLogin(long roomId, int loginStep, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            if (LoginHandler != null)
            {
                ChatRoomLoginResultParam param = ChatRoomLoginResultParam.Deserialize(result);
                if (param != null)
                {
                    LoginHandler((NIMChatRoomLoginStep)loginStep, (NIM.ResponseCode)errorCode, param.RoomInfo, param.MemberInfo);
                }
                else
                {
                    LoginHandler((NIMChatRoomLoginStep)loginStep, (NIM.ResponseCode)errorCode, null, null);
                }
            }
        }

        private static void RegisterLoginCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_enter_cb(null, ParaseChatRoomLoginResult, IntPtr.Zero);
        }

        private static readonly NimChatroomExitCbFunc ParseExitChatRoomResult = OnExit;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomExitCbFunc))]
        private static void OnExit(long roomId, int errorCode, int exitType, string jsonExtension, IntPtr userData)
        {
            if (ExitHandler != null)
            {
                ExitHandler(roomId, (NIM.ResponseCode)errorCode, (NIMChatRoomExitReason)exitType);
            }
        }

        private static void RegisterExitChatRoomCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_exit_cb(null, ParseExitChatRoomResult, IntPtr.Zero);
        }

        private static readonly NimChatroomSendmsgAckCbFunc SendMsgAckCallback = OnSendMsgCompleted;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomSendmsgAckCbFunc))]
        private static void OnSendMsgCompleted(long roomId, int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            if (SendMessageHandler != null)
            {
                var message = NimUtility.Json.JsonParser.Deserialize<Message>(result);
                SendMessageHandler(roomId, (NIM.ResponseCode)errorCode, message);
            }
        }

        private static void RegisterSendMsgArcCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_send_msg_ack_cb(null, SendMsgAckCallback, IntPtr.Zero);
        }

        private static readonly NimChatroomReceiveMsgCbFunc ReceiveMessageCallback = OnReceiveMessage;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomReceiveMsgCbFunc))]
        private static void OnReceiveMessage(long roomId, string result, string jsonExtension, IntPtr userData)
        {
            if (ReceiveMessageHandler != null)
            {
                Message message = Message.Deserialize(result);
                ReceiveMessageHandler(roomId, message);
            }
        }

        private static void RegisterReceiveMsgCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_receive_msg_cb(null, ReceiveMessageCallback, IntPtr.Zero);
        }

        private static readonly NimChatroomReceiveNotificationCbFunc OnReceiveChatRoomNotificationMsg = OnReceiveNotification;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomReceiveNotificationCbFunc))]
        private static void OnReceiveNotification(long roomId, string result, string jsonExtension, IntPtr userData)
        {
            if (ReceiveNotificationHandler != null)
            {
                Notification notify = Notification.Deserialize(result);
                ReceiveNotificationHandler(roomId, notify);
            }
        }

        private static void RegisterReceiveNotificationMsgCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_receive_notification_cb(null, OnReceiveChatRoomNotificationMsg, IntPtr.Zero);
        }

        private static readonly NimChatroomLinkConditionCbFunc LinkStateChangedCallback = OnLinkStateChanged;

        [NIM.MonoPInvokeCallback(typeof(NimChatroomLinkConditionCbFunc))]
        private static void OnLinkStateChanged(long roomId, int condition, string jsonExtension, IntPtr userData)
        {
            if (LinkStateChanged != null)
            {
                LinkStateChanged(roomId, (NIMChatRoomLinkCondition)condition);
            }
        }

        private static void RegisterLinkStateChangedCallback()
        {
            ChatRoomNativeMethods.nim_chatroom_reg_link_condition_cb(null, LinkStateChangedCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 在线查询聊天室成员
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="memberType">成员类型</param>
        /// <param name="timeOffset">距离当前时间的时间戳</param>
        /// <param name="limit">查询数量</param>
        /// <param name="cb">查询结果委托</param>
        public static void QueryMembersOnline(long roomId, NIMChatRoomGetMemberType memberType, long timeOffset, int limit, QueryMembersResultDelegate cb)
        {
            QueryChatRoomMembersParam param = new QueryChatRoomMembersParam();
            param.MemberType = memberType;
            param.Count = limit;
            param.TimeOffset = timeOffset;
            string queryJsonParam = param.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_get_members_online_async(roomId, queryJsonParam, null, CallbackBridge.QueryMembersCallback, ptr);
        }

        /// <summary>
        /// 在线查询消息历史记录
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="startTimeStamp">起始时间戳</param>
        /// <param name="count">查询数量</param>
        /// <param name="cb">查询结果委托</param>
		public static void QueryMessageHistoryOnline(long roomId, long startTimeStamp, int count,bool reverse,List<NIMChatRoomMsgType> msgTypes ,QueryMessageHistoryResultDelegate cb)
        {
            QueryMessageHistoryParam param = new QueryMessageHistoryParam();
            param.Count = count;
            param.StartTime = startTimeStamp;
			param.Reverse = reverse;
			param.MsgTypes = msgTypes;
            string queryJsonParam = param.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_get_msg_history_online_async(roomId, queryJsonParam, null, CallbackBridge.QueryMessageLogCallback, ptr);
        }

        /// <summary>
        /// 设置成员身份标识
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="property">成员属性</param>
        /// <param name="cb">操作结果委托</param>
        public static void SetMemberPropertyOnline(long roomId, MemberProperty property, SetMemberPropertyDelegate cb)
        {
            if (property == null || string.IsNullOrEmpty(property.MemberId))
                throw new ArgumentException("MemberId can't be null or empty");
            var jsonParam = property.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_set_member_attribute_async(roomId, jsonParam, null, CallbackBridge.SetMemberPropertyCallback, ptr);
        }

        /// <summary>
        /// 获取聊天室信息
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="cb">查询结果委托</param>
        public static void GetRoomInfo(long roomId, GetRoomInfoDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_get_info_async(roomId, null, CallbackBridge.GetRoomInfoCallback, ptr);
        }
        /// <summary>
        /// 获取聊天室状态;
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <returns></returns>
        public static int GetLoginState(long roomId)
        {
            return ChatRoomNativeMethods.nim_chatroom_get_login_state(roomId, null);
        }

        /// <summary>
        /// 根据成员ID集合查询对应的成员信息
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="idCollection">聊天室成员ID集合</param>
        /// <param name="cb">查询结果委托</param>
        public static void QueryMemberInfosByIdCollection(long roomId, string[] idCollection, QueryMembersResultDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var idJson = NimUtility.Json.JsonParser.Serialize(idCollection);
            ChatRoomNativeMethods.nim_chatroom_get_members_by_ids_online_async(roomId, idJson, null, CallbackBridge.QueryMembersCallback, ptr);
        }

        /// <summary>
        /// 将指定成员移出聊天室
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="memberId">成员ID</param>
        /// <param name="notify">放到事件通知中的扩展字段</param>
        /// <param name="cb">操作结果委托</param>
        public static void RemoveMember(long roomId, string memberId, string notify, RemoveMemberDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_kick_member_async(roomId, memberId, notify, null, CallbackBridge.KickoutMemberCallback, ptr);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <param name="msg">消息内容</param>
        public static void SendMessage(long roomId, Message msg)
        {
            msg.ClientMsgId = NimUtility.Utilities.GenerateGuid();
            ChatRoomNativeMethods.nim_chatroom_send_msg(roomId, msg.Serialize(), null);
        }

        /// <summary>
        /// 临时禁言/解禁成员
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="accid">成员ID</param>
        /// <param name="duration">临时禁言时长（秒），解禁填0</param>
        /// <param name="cb">操作结果回调</param>
        /// <param name="notify">是否聊天室内广播通知</param>
        /// <param name="notify_ext">通知中的自定义字段，长度限制2048</param>
        public static void TempMuteMember(long roomId, string accid, long duration, TempMuteMemberDelegate cb, bool notify, string notify_ext)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_temp_mute_member_async(roomId, accid, duration, notify, notify_ext, null, CallbackBridge.TempMuteMemberCallback, ptr);
        }
        /// <summary>
        /// 更新聊天室信息;
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="info">只支持修改name,announcement,broadcast_url,ext字段修改</param>
        /// <param name="cb">操作结果回调</param>
        /// <param name="notify">是否聊天室内广播通知</param>
        /// <param name="notify_ext">通知中的自定义字段，长度限制2048</param>
        public static void UpdateRoomInfo(long roomId, ChatRoomInfo info, UpdateRoomInfoDelegate cb, bool notify, string notify_ext)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var json = info.Serialize();
            ChatRoomNativeMethods.nim_chatroom_update_room_info_async(roomId, json, notify, notify_ext, null, CallbackBridge.UpdateRoomInfoCallback, ptr);
        }
        /// <summary>
        /// 更新我的信息;
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="info">只支持修改nick,avatar,ext字段修改</param>
        /// <param name="cb">操作结果回调</param>
        /// <param name="notify">是否聊天室内广播通知</param>
        /// <param name="notify_ext">通知中的自定义字段，长度限制2048</param>
        public static void UpdateMyRoleInfo(long roomId, MemberInfo info, UpdateMyRoleDelegate cb, bool notify, string notify_ext,string jsonExt = null)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var json = info.Serialize();
            ChatRoomNativeMethods.nim_chatroom_update_my_role_async(roomId, json, notify, notify_ext, jsonExt, CallbackBridge.UpdateMyRoleCallback, ptr);
        }



		/// <summary>
		/// 排序列出所有麦序元素 
		/// </summary>
		/// <param name="roomId">房间号</param>
		/// <param name="cb"></param>
		/// <param name="json_extension"></param>
		public static void QueueListAsync(long roomId, ChatRoomQueueListDelegate cb, string json_extension = "")
        {
            //ChatRoomQueueListDelegate
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
          	ChatRoomNativeMethods.nim_chatroom_queue_list_async(roomId, json_extension, CallbackBridge.ChatroomQueueListCallback,ptr);
        }

        /// <summary>
        /// (聊天室管理员权限)删除麦序队列
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="cb"></param>
        /// <param name="json_extension"></param>
        public static void QueueDropAsync(long roomId, ChatRoomQueueDropDelegate cb, string json_extension = "")
        {
            //ChatRoomQueueDropDelegate
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_queue_drop_async(roomId, json_extension, CallbackBridge.ChatroomQueueDropCallback, ptr);
        }

        /// <summary>
        /// 取出麦序头元素 
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="element_key">需要取出的元素的UniqKey,长度限制128字节,传空传表示取出第一个元素</param>
        /// <param name="cb"></param>
        /// <param name="json_extension"></param>
        public static void QueuePollAsync(long roomId, string element_key, ChatRoomQueuePollDelegate cb, string json_extension = "")
        {
            //ChatRoomQueuePollDelegate
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_queue_poll_async(roomId, element_key, json_extension, CallbackBridge.ChatroomQueuePollCallback, ptr);
        }

        /// <summary>
        /// 新加(更新)麦序队列元素,如果element_key对应的元素已经在队列中存在了，那就是更新操作，如果不存在，就放到队列尾部 
        /// </summary>
        /// <param name="roomId">聊天室ID</param>
        /// <param name="element_key">新元素的UniqKey,长度限制128字节 </param>
        /// <param name="elemnet_value">新元素内容，长度限制4096字节 </param>
        /// <param name="cb"></param>
        /// <param name="json_extension"></param>
        public static void QueueOfferAsync(long roomId, string element_key, string elemnet_value, ChatRoomQueueOfferDelegate cb, string json_extension = "")
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_queue_offer_async(roomId, element_key, elemnet_value, json_extension, CallbackBridge.ChatroomQueueOfferCallback, ptr);
        }

        private static nim_chatroom_batch_update_cb BatchUpdataCallback;

        /// <summary>
        /// 批量更新队列信息
        /// </summary>
        /// <param name="roomID">聊天室ID</param>
        /// <param name="elementInfoJson">批量更新元素</param>
        /// <param name="needNotify">是否聊天室内广播通知</param>
        /// <param name="notify">通知中的自定义字段，长度限制2048</param>
        /// <param name="cb">回调函数</param>
        /// <param name="userData"></param>
        public static void UpdateQueue(long roomID, string elementInfoJson, bool needNotify, string notify, nim_chatroom_batch_update_cb cb, IntPtr userData)
        {
            BatchUpdataCallback = cb;
            ChatRoomNativeMethods.nim_chatroom_batch_upate_async(roomID, elementInfoJson, needNotify, notify, null, BatchUpdataCallback, userData);
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 查看麦序头元素
        /// </summary>
        /// <param name="roomID">聊天室ID</param>
        /// <param name="cb">回调函数</param>
        /// <param name="json"></param>
        public static void QueueHeaderAsync(long roomID, ChatRoomQueueHeaderDelegate cb, string json = null)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            ChatRoomNativeMethods.nim_chatroom_queue_header_async(roomID, json, CallbackBridge.ChatroomQueueHeaderCallback, ptr);
        }

        /// <summary>
        /// 设置Chatroom SDK统一的网络代理,不需要代理时，type设置为kNIMProxyNone，其余参数都传空字符串（端口设为0)
        /// </summary>
        /// <param name="type">代理类型</param>
        /// <param name="host">代理地址</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public static void SetProxy(NIMChatRoomProxyType type,string host,int port,string user,string password)
        {
            ChatRoomNativeMethods.nim_chatroom_set_proxy(type, host, port, user, password);
        }
#endif
    }
}