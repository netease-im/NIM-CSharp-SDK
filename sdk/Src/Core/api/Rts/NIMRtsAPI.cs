/** @file NIMRtsAPI.cs
  * @brief NIM RTS提供的实时会话（数据通道）相关接口，如果需要用到音视频功能请使用NIMDeviceAPI.cs中相关接口，并调用NIM.VChatAPI.Init初始化
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @date 2015/12/8
  */
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
using System;
using NimUtility;
using NIM.NIMRts;

namespace NIM
{
    public class RtsAPI
    {
        private static readonly NimRtsStartCbFunc StartResCb = StartResCallback;
        private static readonly NimRtsAckResCbFunc AckResCb = AckResCallback;
        private static readonly NimRtsControlResCbFunc ControlResCb = ControlResCallback;
        private static readonly NimRtsHangupResCbFunc HangupResCb = HangupResCallback;

        /// <summary>
        ///     创建rts会话
        /// </summary>
        /// <param name="channelType">
        ///     通道类型
        ///     如要tcp+音视频，则channel_type=kNIMRtsChannelTypeTcp|kNIMRtsChannelTypeVchat，同时整个SDK只允许一个音视频通道存在（包括vchat）
        /// </param>
        /// <param name="uid">对方帐号</param>
        /// <param name="info">发起扩展参数</param>
        /// <param name="startResHandler">结果回调</param>
        public static void Start(NIMRtsChannelType channelType, string uid, RtsStartInfo info, StartResHandler startResHandler)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(startResHandler);
            var json = info.Serialize();
            RtsNativeMethods.nim_rts_start((int) channelType, uid, json, StartResCb, ptr);
        }

        private static void StartResCallback(int code, string sessionId, int channelType, string uid, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<StartResHandler>(code, sessionId, channelType, uid);
        }

        /// <summary>
        ///     回复收到的邀请
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="channelType">通道类型,暂时无效</param>
        /// <param name="accept">是否接受</param>
        /// <param name="info">接受时的发起信息扩展参数</param>
        /// <param name="ackResHandler">结果回调</param>
        public static void Ack(string sessionId, NIMRtsChannelType channelType, bool accept, RtsStartInfo info, AckResHandler ackResHandler)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(ackResHandler);
            var json = info == null ? null : info.Serialize();
            RtsNativeMethods.nim_rts_ack(sessionId, (int) channelType, accept, json, AckResCb, ptr);
        }

        private static void AckResCallback(int code, string sessionId, int channelType, bool accept, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<AckResHandler>(code, sessionId, channelType, accept);
        }

        /// <summary>
        ///     会话控制（透传）
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="info">透传内容</param>
        /// <param name="controlResHandler">结果回调</param>
        public static void Control(string sessionId, string info, ControlResHandler controlResHandler)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(controlResHandler);
            RtsNativeMethods.nim_rts_control(sessionId, info, "", ControlResCb, ptr);
        }

        private static void ControlResCallback(int code, string sessionId, string info, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<ControlResHandler>(code, sessionId, info);
        }

        /// <summary>
        ///     修改音视频的模式
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="mode">音频模式或视频模式</param>
        public static void SetVChatMode(string sessionId, NIMRtsVideoChatMode mode)
        {
            RtsNativeMethods.nim_rts_set_vchat_mode(sessionId, (int) mode, "");
        }

        /// <summary>
        ///     结束会话
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="hangupResHandler">结果回调</param>
        public static void Hangup(string sessionId, HangupResHandler hangupResHandler)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(hangupResHandler);
            RtsNativeMethods.nim_rts_hangup(sessionId, "", HangupResCb, ptr);
        }

        private static void HangupResCallback(int code, string sessionId, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<HangupResHandler>(code, sessionId);
        }

        /// <summary>
        ///     发送数据，暂时支持tcp通道，建议发送频率在20Hz以下
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="channelType">通道类型</param>
        /// <param name="data">发送数据</param>
        /// <param name="size">data的数据长度</param>
        /// <param name="info">RtsSendDataInfo 发送数据时的json封装类,默认为空</param>
        public static void SendData(string sessionId, NIMRtsChannelType channelType, IntPtr data, int size,RtsSendDataInfo info=null)
        {
            string json_extension = "";
            if(info!=null)
            {
                json_extension = info.Serialize();
            }
            RtsNativeMethods.nim_rts_send_data(sessionId, (int) channelType, data, size, json_extension);
        }

        /// <summary>
        /// 创建一个多人数据通道房间（后续需要主动调用加入接口进入房间）
        /// </summary>
        /// <param name="name">房间名</param>
        /// <param name="custom_info">自定义的房间信息（加入房间的时候会返回）</param>
        /// <param name="cb"></param>
        public static void CreateConference(string name, string custom_info, NimRtsCreateCbFunc cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_create_conf(name, custom_info, null, CreateRtsConfCallback, ptr);
        }

        private static NimRtsCreateCbFunc CreateRtsConfCallback = OnCreateConfCompleted;

        private static void OnCreateConfCompleted(int code, string json_extension, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<NimRtsCreateCbFunc>(user_data, code, json_extension, IntPtr.Zero);
        }

        /// <summary>
        /// 加入一个多人房间（进入房间后成员变化等，等同点对点nim_vchat_cb_func）
        /// </summary>
        /// <param name="name">房间名</param>
        /// <param name="json_extension">扩展可选参数kNIMRtsDataRecord,kNIMRtsSessionId， 如{"data_record":1, "session_id":"b76e2b7ae065224499e4d7138d643961"}</param>
        /// <param name="cb"></param>
        public static void JoinConference(string name, string json_extension, NimRtsJoinCbFunc cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_join_conf(name, json_extension, JoinConfCallback, ptr);
        }

        private static NimRtsJoinCbFunc JoinConfCallback = OnJoinConfCompleted;

        private static void OnJoinConfCompleted(int code, string session_id, string json_extension, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<NimRtsJoinCbFunc>(user_data, code, session_id, json_extension, IntPtr.Zero);
        }

        /// <summary>
        ///  设置SDK白板的网络代理，暂时只支持socks5代理，全局代理接口也能设置音视频的代理，两接口没有优先级区别。
        ///  不需要代理时，type设置为kNIMProxyNone，其余参数都传空字符串（端口设为0）。有些代理不需要用户名和密码，相应参数也传空字符串。
        /// </summary>
        /// <param name="type">代理类型，见NIMProxyType定义,其中音视频和白板暂时只支持kNIMProxySocks5代理</param>
        /// <param name="host">代理地址</param>
        /// <param name="port">代理端口</param>
        /// <param name="user">代理用户名</param>
        /// <param name="password">代理密码</param>
        public static void SetProxy(NIMProxyType type, string host, int port, string user, string password)
        {
            RtsNativeMethods.nim_rts_set_proxy(type, host, port, user, password);
        }

        /// <summary>
        /// 重连接口
        /// </summary>
        /// <param name="session_id">会话id</param>
        /// <param name="channel_type">通道类型，暂时只支持白板类型</param>
        /// <param name="json_extension">无效的扩展字段</param>
        /// <param name="cb">返回重连操作结果</param>
        private static void ReLogin(string session_id, int channel_type, string json_extension, NimRtsOptCbFunc cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_relogin(session_id, channel_type, json_extension, OnReLoginCallback, ptr);
        }
        private static NimRtsOptCbFunc ReLoginCallback = OnReLoginCallback;

        private static void OnReLoginCallback(int code,string session_id,int channel_type,string json_extension,IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<NimRtsOptCbFunc>(user_data, code, session_id, channel_type, json_extension, IntPtr.Zero);
        }

        #region 设置rts的通知回调

        public static void SetStartNotifyCallback(OnStartNotify cb)
        {
            var ptr1 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_start_notify_cb_func(StartNotifyCb, ptr1);
        }

        public static void SetAckNotifyCallback(OnAckNotify cb)
        {
            var ptr2 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_ack_notify_cb_func(AckNotifyCb, ptr2);
        }

        public static void SetSyncAckNotifyCallback(OnSyncAckNotify cb)
        {
            var ptr3 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_sync_ack_notify_cb_func(SyncAckNotifyCb, ptr3);
        }

        public static void SetConnectionNotifyCallback(OnConnectNotify cb)
        {
            var ptr4 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_connect_notify_cb_func(ConnectNotifyCb, ptr4);
        }

        public static void SetMemberChangedNotifyCallback(OnMemberNotify cb)
        {
            var ptr5 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_member_change_cb_func(MemberChangeCb, ptr5);
        }

        public static void SetControlNotifyCallback(OnControlNotify cb)
        {
            var ptr6 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_control_notify_cb_func(ControlNotifyCb, ptr6);
        }

        public static void SetHungupNotify(OnHangupNotify cb)
        {
            var ptr7 = DelegateConverter.ConvertToIntPtr(cb);
            RtsNativeMethods.nim_rts_set_hangup_notify_cb_func(HangupNotifyCb, ptr7);
        }

        public static void SetReceiveDataCallback(OnRecData callback)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(callback);
            RtsNativeMethods.nim_rts_set_rec_data_cb_func(RecDataCb, ptr);
        }

     
        private static readonly NimRtsStartNotifyCbFunc StartNotifyCb = StartNotifyCallback;
        private static readonly NimRtsAckNotifyCbFunc AckNotifyCb = AckNotifyCallback;
        private static readonly NimRtsSyncAckNotifyCbFunc SyncAckNotifyCb = SyncAckNotifyCallback;
        private static readonly NimRtsConnectNotifyCbFunc ConnectNotifyCb = ConnectNotifyCallback;
        private static readonly NimRtsMemberChangeCbFunc MemberChangeCb = MemberChangeCallback;
        private static readonly NimRtsControlNotifyCbFunc ControlNotifyCb = ControlNotifyCallback;
        private static readonly NimRtsHangupNotifyCbFunc HangupNotifyCb = HangupNotifyCallback;
        private static readonly NimRtsRecDataCbFunc RecDataCb = RecDataCallback;

        private static void StartNotifyCallback(string sessionId, int channelType, string uid, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<OnStartNotify>(sessionId, channelType, uid, jsonExtension);
        }

        private static void AckNotifyCallback(string sessionId, int channelType, bool accept, string uid, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<OnAckNotify>(sessionId, channelType, accept, uid);
        }

        private static void SyncAckNotifyCallback(string sessionId, int channelType, bool accept, string jsonExtension, IntPtr userData)
        {
            var info = RtsSyncAckInfo.Deserialize(jsonExtension);
            userData.Invoke<OnSyncAckNotify>(sessionId, channelType, accept, info.client);
        }

        private static void ConnectNotifyCallback(string sessionId, int channelType, int code, string jsonExtension, IntPtr userData)
        {
            //NIMRts.RtsConnectInfo info = NIMRts.RtsConnectInfo.Deserialize(jsonExtension);
            userData.Invoke<OnConnectNotify>(sessionId, channelType, code);
        }

        private static void MemberChangeCallback(string sessionId, int channelType, int type, string uid, string jsonExtension, IntPtr userData)
        {
            RtsMemberChangeInfo info = RtsMemberChangeInfo.Deserialize(jsonExtension);
            userData.Invoke<OnMemberNotify>(sessionId, channelType, type, uid, info);
        }

        private static void ControlNotifyCallback(string sessionId, string info, string uid, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<OnControlNotify>(sessionId, info, uid);
        }

        private static void HangupNotifyCallback(string sessionId, string uid, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<OnHangupNotify>(sessionId, uid);
        }

        private static void RecDataCallback(string sessionId, int type, string uid, IntPtr data, int size, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<OnRecData>(sessionId, type, uid, data, size);
        }

#endregion
    }
}
#endif