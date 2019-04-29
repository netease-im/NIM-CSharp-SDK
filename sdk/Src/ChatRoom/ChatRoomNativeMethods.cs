using System;
using System.Runtime.InteropServices;

namespace NIMChatRoom
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomRequestChatroomLinkInfoCbFunc(long room_id, int error_code,[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result,[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomLoginCbFunc(long room_id, int login_step, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomExitCbFunc(long room_id, int error_code, int exit_type, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomLinkConditionCbFunc(long room_id, int condition, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomSendmsgAckCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomReceiveMsgCbFunc(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomReceiveNotificationCbFunc(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomGetMembersCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomGetMsgCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomSetMemberAttributeCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomCloseCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomGetInfoCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimChatroomKickMemberCbFunc(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_chatroom_temp_mute_member_cb_func(long room_id, int error_code,
       [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (NimUtility.Utf8StringMarshaler))]
       string result,
       [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (NimUtility.Utf8StringMarshaler))]
       string json_extension,
       IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_chatroom_update_room_info_cb_func(long room_id, int error_code,
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
    IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_chatroom_update_my_role_cb_func(long room_id, int error_code,
[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_queue_offer_cb_func(long room_id, NIM.ResponseCode error_code,
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
     IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_queue_poll_cb_func(long room_id, NIM.ResponseCode error_code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_queue_list_cb_func(long room_id, NIM.ResponseCode error_code,
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result,
    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
    IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_queue_drop_cb_func(long room_id, NIM.ResponseCode error_code,
[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
 IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_queue_header_cb_func(long room_id, int error_code, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_chatroom_batch_update_cb(long room_id,int error_code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, 
        IntPtr user_data);

    internal static class ChatRoomNativeMethods
    {
        /// <summary>
        ///注册全局登录回调 
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_enter_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_enter_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomLoginCbFunc cb, IntPtr user_data);

        /// <summary>
        ///注册全局登出、被踢回调 
        /// </summary> 
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_exit_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_exit_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomExitCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 注册聊天室链接状况回调
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_link_condition_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_link_condition_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomLinkConditionCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 注册全局发送消息回执回调
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_send_msg_ack_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_send_msg_ack_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomSendmsgAckCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 注册全局接收消息回调
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_receive_msg_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_receive_msg_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomReceiveMsgCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 注册全局接收通知回调
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_reg_receive_notification_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_reg_receive_notification_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomReceiveNotificationCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 聊天室模块初始化
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_chatroom_init([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 获取匿名登录聊天室地址
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_request_link_service_with_anonymous", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_chatroom_request_link_service_with_anonymous(long room_id,  
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string  app_key,  
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string  json_extension,
            NimChatroomRequestChatroomLinkInfoCbFunc callback,
            IntPtr user_data);

        /// <summary>
        /// 匿名登录聊天室
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_enter_with_anoymity", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_chatroom_enter_with_anoymity(long room_id,
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string anonymity_info,
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string enter_info,
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 聊天室登录
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_enter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_chatroom_enter(long room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string request_login_data,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string login_info,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 聊天室登出
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_exit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_exit(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 聊天室模块清理
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_cleanup", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_cleanup([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 发送消息
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_send_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_send_msg(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string msg, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        /// <summary>
        /// 异步获取聊天室成员（分页）
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_get_members_online_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_get_members_online_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string parameters_json_str, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomGetMembersCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 异步获取消息历史（分页）
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_get_msg_history_online_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_get_msg_history_online_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string parameters_json_str, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomGetMsgCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 异步修改成员身份标识
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_set_member_attribute_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_set_member_attribute_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomSetMemberAttributeCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 异步获取当前聊天室信息
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_get_info_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_get_info_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomGetInfoCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 异步获取指定成员信息
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_get_members_by_ids_online_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_get_members_by_ids_online_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string ids_json_array_string, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomGetMembersCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 异步踢掉指定成员
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_kick_member_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_kick_member_async(long room_id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string id, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimChatroomKickMemberCbFunc cb, IntPtr user_data);

        /// <summary>
        /// 设置Chatroom SDK统一的网络代理。不需要代理时，type设置为kNIMProxyNone，其余参数都传空字符串（端口设为0）。有些代理不需要用户名和密码，相应参数也传空字符串。
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_set_proxy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_set_proxy(
            NIMChatRoomProxyType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string host,
            int port,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string user,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string password);

        /// <summary>
        /// 获取登陆状态
        /// </summary>

        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_get_login_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int nim_chatroom_get_login_state(
            long room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);
        /// <summary>
        /// 临时禁言/解禁 
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_temp_mute_member_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_temp_mute_member_async(long room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string id,
            long duration,
            bool need_notify,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_chatroom_temp_mute_member_cb_func cb,
            IntPtr user_data);

        /// <summary>
        ///  更新聊天室信息，目前只支持更新kNIMChatRoomInfoKeyName,kNIMChatRoomInfoKeyAnnouncement,kNIMChatRoomInfoKeyBroadcastUrl,kNIMChatRoomInfoKeyExt四个字段
        /// </summary>

        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_update_room_info_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_update_room_info_async(
            Int64 room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string room_info_json_str,
            bool need_notify,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_chatroom_update_room_info_cb_func cb,
            IntPtr user_data);

        /// <summary>
        ///  更新我的信息，目前只支持更新kNIMChatRoomMemberInfoKeyNick,kNIMChatRoomMemberInfoKeyAvatar,kNIMChatRoomMemberInfoKeyExt三个字段
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_update_my_role_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_update_my_role_async(
            Int64 room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string role_info_json_str,
            bool need_notify,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_chatroom_update_my_role_cb_func cb,
            IntPtr user_data);

        /// <summary>
        ///  (聊天室管理员权限)新加(更新)麦序队列元素,如果element_key对应的元素已经在队列中存在了，那就是更新操作，如果不存在，就放到队列尾部 
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_queue_offer_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_queue_offer_async(
        long room_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string element_key,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string element_value,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        nim_chatroom_queue_offer_cb_func cb,
        IntPtr user_data);


        /// <summary>
        ///  ((聊天室管理员权限)取出麦序头元素 
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_queue_poll_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_queue_poll_async(
        long room_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string element_key,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        nim_chatroom_queue_poll_cb_func cb,
        IntPtr user_data);

        /// <summary>
        ///  排序列出所有麦序元素 
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_queue_list_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_queue_list_async(
        long room_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        nim_chatroom_queue_list_cb_func cb,
        IntPtr user_data);

        /// <summary>
        /// (聊天室管理员权限)删除麦序队列
        /// </summary>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, EntryPoint = "nim_chatroom_queue_drop_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_queue_drop_async(
        long room_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        nim_chatroom_queue_drop_cb_func cb,
        IntPtr user_data);

        [DllImport(NIM.NativeConfig.ChatRoomNativeDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_batch_upate_async(
            long room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string element_info_json_str,
            bool need_notify,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify_ext,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_chatroom_batch_update_cb cb,
            IntPtr user_data);
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 查看麦序头元素
        /// </summary>
        /// <param name="room_id">聊天室ID</param>
        /// <param name="json_extension">扩展参数，备用</param>
        /// <param name="cb"></param>
        /// <param name="user_data"></param>
        [DllImport(NIM.NativeConfig.ChatRoomNativeDll,CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_chatroom_queue_header_async(long room_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, 
            nim_chatroom_queue_header_cb_func cb, IntPtr user_data);
#endif

    }
}