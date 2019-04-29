/** @file NIMVChatAPI.cs
  * @brief NIM VChat提供的音视频相关接口，相关功能调用前需要先Init()
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @modify leewp
  * @date 2015/12/8
  */

using System;
using System.Runtime.InteropServices;

namespace NIM
{

    /// <summary>
    /// 音视频相关的回调
    /// </summary>
    public struct NIMVChatSessionStatus
    {
        /// <summary>
        /// 发起结果回调
        /// </summary>
        public onSessionHandler onSessionStartRes;
        /// <summary>
        /// 邀请通知
        /// </summary>
        public onSessionInviteNotifyHandler onSessionInviteNotify;
        /// <summary>
        /// 邀请响应的结果回调
        /// </summary>
        public onSessionHandler onSessionCalleeAckRes;
        /// <summary>
        /// 发起后对方响应通知
        /// </summary>
        public onSessionCalleeAckNotifyHandler onSessionCalleeAckNotify;
        /// <summary>
        /// 控制操作结果回调
        /// </summary>
        public onSessionControlResHandler onSessionControlRes;
        /// <summary>
        /// 控制操作通知
        /// </summary>
        public onSessionControlNotifyHandler onSessionControlNotify;
        /// <summary>
        /// 链接通知
        /// </summary>
        public onSessionConnectNotifyHandler onSessionConnectNotify;
        /// <summary>
        /// 成员状态通知
        /// </summary>
        public onSessionPeopleStatusHandler onSessionPeopleStatus;
        /// <summary>
        /// 网络状态通知
        /// </summary>
        public onSessionNetStatusHandler onSessionNetStatus;
        /// <summary>
        /// 主动挂断结果回调
        /// </summary>
        public onSessionHandler onSessionHangupRes;
        /// <summary>
        /// 对方挂断通知
        /// </summary>
        public onSessionHandler onSessionHangupNotify;
        /// <summary>
        /// 本人其他端响应通知
        /// </summary>
        public onSessionSyncAckNotifyHandler onSessionSyncAckNotify;	
        /// <summary>
		/// 音量状态通知
		/// </summary>
		public onSessionVolumeNotifyHandler onSessionVolumeStateChanged; 
        /// <summary>
        /// 实时状态通知
        /// </summary>
        public onSessionRealtimeInfoNotifyHandler onSessionRealtimeStateNotify; 
        /// <summary>
        /// mp4状态通知
        /// </summary>
        public OnSessionMP4InfoNotifyHandler onSessionMp4InfoStateNotify;
        /// <summary>
        /// 音频录制状态通知
        /// </summary>
        public OnSessionAuRecordInfoNotifyHandler onSessionAuRecordInfoStateNotify;

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		/// <summary>
		/// 直播状态通知
		/// </summary>
		public OnSessionLiveStateInfoNotifyHandler onSessionLiveStateNotify;
#else 
        /// <summary>
        /// 通话中伴音事件状态通知 Unity android ios有效
        /// </summary>
        public onSessionHandler onSessionAuMixingEventNotify;
        /// <summary>
        /// 客户端网络类型发生了变化 Unity android ios有效
        /// </summary>
        public onSessionHandler onSessionNetConnectionTypeChangedNotify;
        /// <summary>
        /// 语音静音状态 Unity android ios有效
        /// </summary>
        public onSessionPeopleStatusHandler onSessionAudioMuteStatus;

#endif
    }

    public class VChatAPI
    {
       
        private static NIMVChatSessionStatus session_status;
		private static NimVchatCbFunc VChatStatusCb =VChatSessionStatusCallback;
		private static NimVchatOptCbFunc VChatNormalOptCb = OnNormalOpCompletedCallback;
		private static NimVchatOpt2CbFunc VChatOpt2Cb = OnVchatRoomCreatedCallback;


        private static NimVchatMp4RecordOptCbFunc  VChatMP4RecordOptCb = OnMP4RecordOpCompletedCallback;
		private static NimVchatAudioRecordOptCbFunc VChatAudioRecordStartCb = OnAudioRecordStartCallback;
		private static NimVchatAudioRecordOptCbFunc VChatAudioRecordStopCb = OnAudioRecordStopCallback;


        [MonoPInvokeCallback(typeof(NimVchatOptCbFunc))]
        private static void OnNormalOpCompletedCallback(bool ret, int code, string json_extension, IntPtr user_data)
		{
			NimUtility.DelegateConverter.Invoke<NIMVChatOptHandler>(user_data, ret, code, json_extension);
		}

        [MonoPInvokeCallback(typeof(NimVchatCbFunc))]
        private static void VChatSessionStatusCallback(NIMVideoChatSessionType type, long channel_id, int code, string json_extension, IntPtr user_data)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("type:" + type.ToString() + "json: " + json_extension);
                if (json_extension == null)
                {
                    return;
                }
                NIMVChatSessionInfo info = null;
                switch (type)
                {
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeStartRes:
                        {
                            if (session_status.onSessionStartRes != null)
                            {
                                session_status.onSessionStartRes(channel_id, code);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeInviteNotify:
                        {
                            if (session_status.onSessionInviteNotify != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionInviteNotify(channel_id, info.Uid, info.Type, info.Time, info.CustomInfo);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeCalleeAckRes:
                        {
                            if (session_status.onSessionCalleeAckRes != null)
                            {
                                session_status.onSessionCalleeAckRes(channel_id, code);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeCalleeAckNotify:
                        {
                            if (session_status.onSessionCalleeAckNotify != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionCalleeAckNotify(channel_id, info.Uid, info.Type, info.Accept > 0);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeControlRes:
                        {
                            if (session_status.onSessionControlRes != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionControlRes(channel_id, code, info.Type);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeControlNotify:
                        {
                            if (session_status.onSessionControlNotify != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionControlNotify(channel_id, info.Uid, info.Type);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeConnect:
                        {
                            if (session_status.onSessionConnectNotify != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                    session_status.onSessionConnectNotify(channel_id, code, info.RecordFile, info.VideoRecordFile, info.Time, info.NIMVChatTrafficStatRX, info.NIMVChatTrafficStatTX);
                                else
                                    session_status.onSessionConnectNotify(channel_id, code, null, null, 0, 0L, 0L);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypePeopleStatus:
                        {
                            if (session_status.onSessionPeopleStatus != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    if (code == Convert.ToInt32(NIMVideoChatSessionStatus.kNIMVideoChatSessionStatusLeaved))
                                    {
                                        if (info.Status == Convert.ToInt32(NIMVideoChatUserLeftType.kNIMVChatUserLeftTimeout))
                                        {
                                            code = Convert.ToInt32(NIMVideoChatSessionStatus.kNIMVideoChatSessionStatusTimeOutLeaved);
                                        }
                                    }
                                    session_status.onSessionPeopleStatus(channel_id, info.Uid, code);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeNetStatus:
                        {
                            if (session_status.onSessionNetStatus != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionNetStatus(channel_id, code, info.Uid);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeHangupRes:
                        {
                            if (session_status.onSessionHangupRes != null)
                            {
                                session_status.onSessionHangupRes(channel_id, code);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeHangupNotify:
                        {
                            if (session_status.onSessionHangupNotify != null)
                            {
                                session_status.onSessionHangupNotify(channel_id, code);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeSyncAckNotify:
                        {
                            if (session_status.onSessionSyncAckNotify != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionSyncAckNotify(channel_id, code, info.Uid, info.Type, info.Accept > 0, info.Time, info.Client);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeMp4Notify:
                        {
                            if (session_status.onSessionMp4InfoStateNotify != null)
                            {
                                NIMVChatMP4State mp4_info = NIMVChatMP4State.Deserialize(json_extension);
                                session_status.onSessionMp4InfoStateNotify(channel_id, code, mp4_info);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeInfoNotify:
                        if (session_status.onSessionRealtimeStateNotify != null)
                        {
                            var state = NIMVChatRealtimeState.Deserialize(json_extension);
                            session_status.onSessionRealtimeStateNotify(channel_id, code, state);
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeVolumeNotify:
                        if (session_status.onSessionVolumeStateChanged != null)
                        {
                            var volume = NIMVchatAudioVolumeState.Deserialize(json_extension);
                            session_status.onSessionVolumeStateChanged(channel_id, code, volume);
                        }
                        break;

                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeAuRecordNotify:
                        {
                            if (session_status.onSessionAuRecordInfoStateNotify != null)
                            {
                                NIMVChatAuRecordState record_state = NIMVChatAuRecordState.Deserialize(json_extension);
                                session_status.onSessionAuRecordInfoStateNotify(channel_id, code, record_state);
                            }
                        }
                        break;
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
                case NIMVideoChatSessionType.kNIMVideoChatSessionTypeLiveState:
					{
						if (session_status.onSessionLiveStateNotify != null)
						{
							var state = NIMVChatLiveState.Deserialize(json_extension);
							session_status.onSessionLiveStateNotify(channel_id, code, state);
						}
					}
					break;
#else
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeAuMixingEventNotify:
                        {
                            if (session_status.onSessionAuMixingEventNotify != null)
                            {
                                session_status.onSessionAuMixingEventNotify(channel_id, code);
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeAudioMuteStatus:
                        {
                            if (session_status.onSessionAudioMuteStatus != null)
                            {
                                info = NIMVChatSessionInfo.Deserialize(json_extension);
                                if (info != null)
                                {
                                    session_status.onSessionAudioMuteStatus(channel_id,info.Uid,code);
                                }
                            }
                        }
                        break;
                    case NIMVideoChatSessionType.kNIMVideoChatSessionTypeNetConnectionTypeChangedNotify:
                        {
                            if (session_status.onSessionNetConnectionTypeChangedNotify != null)
                            {
                                session_status.onSessionNetConnectionTypeChangedNotify(channel_id, code);
                            }
                        }
                        break;
#endif
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("exception:"+ex.ToString());
            }
            
        }

        [MonoPInvokeCallback(typeof(NimVchatMp4RecordOptCbFunc))]
        private static void OnMP4RecordOpCompletedCallback(bool ret, int code, string file, long time, string json_extension, IntPtr user_data)
		{
			NimUtility.DelegateConverter.Invoke<NIMVChatMp4RecordOptHandler>(user_data, ret, code, file, time, json_extension);
		}

        [MonoPInvokeCallback(typeof(NimVchatAudioRecordOptCbFunc))]
        private static void OnAudioRecordStartCallback(bool ret, int code, string file, Int64 time, string json_extension, IntPtr user_data)
		{
			NimUtility.DelegateConverter.Invoke<NIMVChatAudioRecordOptHandler>(user_data, ret, code, file, time, json_extension);
		}

        [MonoPInvokeCallback(typeof(NimVchatAudioRecordOptCbFunc))]
        private static void OnAudioRecordStopCallback(bool ret, int code, string file, Int64 time, string json_extension, IntPtr user_data)
		{
			NimUtility.DelegateConverter.Invoke<NIMVChatAudioRecordOptHandler>(user_data, ret, code, file, time, json_extension);
		}


        [MonoPInvokeCallback(typeof(NimVchatOpt2CbFunc))]
        private static void OnVchatRoomCreatedCallback(int code, long channel_id, string json_extension, IntPtr user_data)
		{
			NimUtility.DelegateConverter.Invoke<NIMVChatOpt2Handler>(user_data, code, channel_id, json_extension);
		}


#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// VCHAT初始化，需要在SDK的Client.Init成功之后
        /// </summary>
        /// <param name="path">服务器配置文件路径 </param>
        /// <returns>初始化结果，如果是false则以下所有接口调用无效</returns>
        public static bool Init(string server_setting_path)
		{
            string info = "";
            if (!String.IsNullOrEmpty(server_setting_path))
            {
				NIMVChatResourceJsonEx json = new NIMVChatResourceJsonEx();
				json.Path = server_setting_path;
				info = json.Serialize();
			}
            return VChatNativeMethods.nim_vchat_init(info);
        }
#else
        /// <summary>
        /// VCHAT初始化，需要在SDK的Client.Init成功之后
        /// </summary>
        /// <param name="path">nrtc等资源库路径，Unity下pc有效</param>
        /// <param name="context">android 上下文,Unity下android有效</param>
        /// <returns>初始化结果，如果是false则以下所有接口调用无效</returns>
        public static bool Init(string path,IntPtr context)
		{

            string info = "";
            if (!String.IsNullOrEmpty(path))
            {
				NIMVChatResourceJsonEx json = new NIMVChatResourceJsonEx();
				json.Path = path;
				info = json.Serialize();
			}
			
#if UNITY_ANDROID
            return VChatNativeMethods.nim_vchat_init(context);
#else
            return VChatNativeMethods.nim_vchat_init(info);
#endif

        }
#endif



        /// <summary>
        /// VCHAT释放，需要在SDK的Client.Cleanup之前
        /// </summary>
        /// <returns>无返回值</returns>
        public static void Cleanup()
		{
            VChatNativeMethods.nim_vchat_cleanup("");
        }

        /// <summary>
        /// 设置统一的通话回调或者服务器通知
        /// </summary>
        /// <param name="session">回调通知对象</param>
        /// <returns>无返回值</returns>
        public static void SetSessionStatusCb(NIMVChatSessionStatus session)
		{
            session_status = session;
			VChatNativeMethods.nim_vchat_set_cb_func(VChatStatusCb, IntPtr.Zero);
        }

        /// <summary>
        /// 启动点对点通话
        /// </summary>
        /// <param name="mode">启动音视频通话类型</param>
        /// <param name="apns_text">自定义推送字段，填空用默认推送</param>
        /// <param name="info">json扩展封装类，见NIMVChatInfo</param>
        /// <param name="customInfo">自定义信息</param> 
        /// <returns> bool true 调用成功，false 调用失败可能有正在进行的通话</returns>
        public static bool Start(NIMVideoChatMode mode,string apns_text,NIMVChatInfo info,string customInfo = null)
        {
            if (info == null)
				info = new NIMVChatInfo();
			string json_extension = info.Serialize();
            return VChatNativeMethods.nim_vchat_start(mode, apns_text, customInfo, json_extension, IntPtr.Zero);
        }

		/// <summary>
		/// 设置通话模式，在更改通话模式后，通知底层
		/// </summary>
		/// <param name="mode">音视频通话类型</param>
		/// <returns>true 调用成功，false 调用失败</returns>
		/// 
		public static bool SetMode(NIMVideoChatMode mode)
        {
            return VChatNativeMethods.nim_vchat_set_talking_mode(mode, "");
        }

		/// <summary>
		/// 回应音视频通话邀请
		/// </summary>
		/// <param name="channel_id">音视频通话通道id</param>
		/// <param name="accept">true 接受，false 拒绝</param>
		/// <param name="info">json扩展封装类，见NIMVChatInfo</param>
		/// <returns>bool true 调用成功，false 调用失败（可能channel_id无匹配，如要接起另一路通话前先结束当前通话）</returns>
		public static bool CalleeAck(long channel_id, bool accept, NIMVChatInfo info)
        {
            if (info == null)
				info = new NIMVChatInfo();
            string json_extension = info.Serialize();
            //string debug_info = string.Format("callee ack.cid:{0},accept:{1},info:{2}", channel_id, accept, json_extension);
            //System.Diagnostics.Debug.WriteLine(debug_info);
            return VChatNativeMethods.nim_vchat_callee_ack(channel_id, accept, json_extension, IntPtr.Zero);
        }

		/// <summary>
		/// 音视频通话控制操作
		/// </summary>
		/// <param name="channel_id">音视频通话通道id</param>
		/// <param name="type">操作类型</param>
		/// <returns>bool true 调用成功，false 调用失败</returns>
		public static bool ChatControl(long channel_id, NIMVChatControlType type)
        {
            return VChatNativeMethods.nim_vchat_control(channel_id, type, "", IntPtr.Zero);
        }

		/// <summary>
		/// 结束通话(需要主动在通话结束后调用，用于底层挂断和清理数据)
		/// </summary>
		/// <param name="jsonExtension">可扩展添加session_id，用于关闭对应的通话，如果session_id缺省.则关闭当前通话.如.{"session_id":"leewp"}</param>
		/// <returns>无返回值</returns>
		public static void End(string jsonExtension="")
        {
            VChatNativeMethods.nim_vchat_end(jsonExtension);
        }

        /// <summary>
        /// 设置观众模式（多人模式下），全局有效（重新发起时也生效）
        /// </summary>
        /// <param name="viewer">是否观众模式</param>
        /// <returns>无返回值</returns>
        public static void SetViewerMode(bool viewer)
        {
            VChatNativeMethods.nim_vchat_set_viewer_mode(viewer);
        }

        /// <summary>
        /// 获取当前是否是观众模式
        /// </summary>
        /// <returns>bool true 观众模式，false 非观众模式</returns>
        public static bool GetViewerMode()
        {
            return VChatNativeMethods.nim_vchat_get_viewer_mode();
        }

		/// <summary>
		/// 设置音频静音，全局有效（重新发起时也生效）
		/// </summary>
		/// <param name="muted">true 静音，false 不静音</param>
		/// <returns>无返回值</returns>
		public static void SetAudioMute(bool muted)
        {
            VChatNativeMethods.nim_vchat_set_audio_mute(muted);
        }

        /// <summary>
        /// 获取音频静音状态
        /// </summary>
        /// <returns>bool true 静音，false 不静音</returns>
        public static bool GetAudioMuteEnabled()
        {
            return VChatNativeMethods.nim_vchat_audio_mute_enabled();
        }



		/// <summary>
		/// 设置单个成员的黑名单状态，即是否显示对方的音频或视频数据，当前通话有效(只能设置进入过房间的成员)
		/// </summary>
		/// <param name="uid">uid成员 account</param>
		/// <param name="add">true:添加到黑名单.false:从黑名单中移除</param>
		/// <param name="audio">ture:表示音频黑名单.false:表示视频黑名单</param>
		/// <param name="json_extension">无效扩展字段</param>
		/// <param name="cb">返回的json_extension无效</param>
		/// <returns>无返回值</returns>
		public static void SetMemberInBlackList(string uid, bool add, bool audio, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_set_member_in_blacklist(uid, add, audio, json_extension, VChatNormalOptCb, ptr);
        }


        /// <summary>
        ///创建一个多人房间（后续需要主动调用加入接口进入房间） 
        /// </summary>
        /// <param name="room_name">房间名</param>
        /// <param name="custom_info">自定义的房间信息（加入房间的时候会返回）</param>
        /// <param name="createRoomInfo">json封装类，见NIMCreateRoomJsonEx</param>
        /// <param name="cb">结果回调</param>
        /// <returns>无返回值</returns>
        public static void CreateRoom(string room_name, string custom_info, NIMCreateRoomJsonEx createRoomInfo, NIMVChatOpt2Handler cb)
        {
            string json_extension = null;
			if(createRoomInfo!=null)
			{
				json_extension = createRoomInfo.Serialize();
			}
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_create_room(room_name, custom_info, json_extension, VChatOpt2Cb, ptr);
        }

        /// <summary>
        /// 加入一个多人房间（进入房间后成员变化等，等同点对点NIMVChatHander）
        /// </summary>
        /// <param name="mode">音视频通话类型</param>
        /// <param name="room_name">房间名</param>
        /// <param name="joinRoomInfo">json封装类，见NIMJoinRoomJsonEx</param>
        /// <param name="cb">cb 结果回调,返回的json_extension扩展字段中包含 "custom_info","session_id"</param>
        /// <returns>bool true 调用成功，false 调用失败可能有正在进行的通话</returns>
        public static bool JoinRoom(NIMVideoChatMode mode, string room_name, NIMJoinRoomJsonEx joinRoomInfo, NIMVChatOpt2Handler cb)
        {
            if (joinRoomInfo == null)
            {
                joinRoomInfo = new NIMJoinRoomJsonEx();
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
                CustomLayout layout=new CustomLayout();
                layout.Hostarea = new HostArea();
                layout.Background = new BackGround();
                joinRoomInfo.Layout = layout.Serialize();
#endif
            }
            string	json_extension = joinRoomInfo.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            return VChatNativeMethods.nim_vchat_join_room(mode, room_name, json_extension, VChatOpt2Cb, ptr);

        }

		/// <summary>
		/// 通话中修改自定义音视频数据模式
		/// </summary>
		/// <param name="custom_audio">true:表示使用自定义的音频数据.false:表示不使用</param>
		/// <param name="custom_video">true:表示使用自定义的视频数据.false:表示不使用</param>
		/// <param name="json_extension">无效扩展字段</param>
		/// <param name="cb">cb 结果回调</param>
		/// <returns>无返回值</returns>
		public static void SetCustomData(bool custom_audio, bool custom_video, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_set_custom_data(custom_audio, custom_video, json_extension, VChatNormalOptCb, ptr);
        }

        /// <summary>
        /// 开始录制MP4，，同一个成员一次只允许一个MP4录制文件，在通话开始的时候才有实际数据
        /// </summary>
        /// <param name="path">文件录制路径</param>
        /// <param name="recordInfo">json扩展封装类，见NIMVChatMP4RecordJsonEx</param>
        /// <param name="cb">结果回调</param>
        /// <returns>无返回值</returns>
        public static void StartRecord(string path, NIMVChatMP4RecordJsonEx recordInfo, NIMVChatMp4RecordOptHandler cb)
        {
            if (recordInfo == null)
                recordInfo = new NIMVChatMP4RecordJsonEx();
            string json_extension = recordInfo.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_start_record(path, json_extension, VChatMP4RecordOptCb, ptr);
        }

        /// <summary>
        /// 停止录制MP4
        /// </summary>
        /// <param name="recordInfo">json扩展封装类，见NIMVChatMP4RecordJsonEx</param>
        /// <param name="cb">结果回调</param>
        /// <returns>无返回值</returns>
        public static void StopRecord(NIMVChatMP4RecordJsonEx recordInfo, NIMVChatMp4RecordOptHandler cb)
        {
            if (recordInfo == null)
                recordInfo = new NIMVChatMP4RecordJsonEx();
            string json_extension = recordInfo.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_stop_record(json_extension, VChatMP4RecordOptCb, ptr);
        }

        /// <summary>
        /// 开始录制音频文件，一次只允许一个音频录制文件
        /// </summary>
        /// <param name="path">文件录制路径</param>
        /// <param name="cb">结果回调</param>
        /// <returns>无返回值</returns>
        public static void StartAudioRecord(string path, NIMVChatAudioRecordOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_start_audio_record(path, "", VChatAudioRecordStartCb, ptr);
        }

        /// <summary>
        /// 停止录制音频文件
        /// </summary>
        /// <param name="cb">结果回调</param>
        /// <returns>无返回值</returns>
        public static void StopAudioRecord(NIMVChatAudioRecordOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_stop_audio_record("", VChatAudioRecordStopCb, ptr);
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 设置不自动旋转对方画面，默认打开，全局有效（重新发起时也生效）
        /// </summary>
        /// <param name="rotate"> true 自动旋转，false 不旋转</param>
        /// <returns>无返回值</returns>
        public static void SetRotateRemoteVideo(bool rotate)
        {
            VChatNativeMethods.nim_vchat_set_rotate_remote_video(rotate);
        }

		/// <summary>
		/// 获取自动旋转对方画面设置状态
		/// </summary>
		/// <param name="rotate"></param>
		/// <returns>true 自动旋转，false 不旋转</returns>
		public static bool IsRotateRemoteVideo()
        {
            return VChatNativeMethods.nim_vchat_rotate_remote_video_enabled();
        }


		/// <summary>
		/// 通话中修改直播推流地址（主播有效）
		/// </summary>
		/// <param name="rtmp_url">新的rtmp推流地址</param>
		/// <param name="json_extension">无效扩展字段</param>
		/// <param name="cb">结果回调,返回的json_extension无效</param>
		/// <returns>无返回值</returns>
		public static void UpdateRtmpUrl(string rtmp_url, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_update_rtmp_url(rtmp_url, json_extension, VChatNormalOptCb, ptr);
        }

        /// <summary>
        /// 通话中修改发送画面分辨率，发送的分辨率限制只对上限限制，如果数据源小于发送分辨率，不会进行放大
        /// </summary>
        /// <param name="video_quality"> 分辨率模式</param>
        /// <param name="json_extension">无效扩展字段</param>
        /// <param name="cb">结果回调，返回的json_extension无效</param>
        /// <returns>无返回值</returns>
        public static void SetVideoQuality(NIMVChatVideoQuality video_quality, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_set_video_quality(video_quality, json_extension, VChatNormalOptCb, ptr);
        }

		/// <summary>
		/// 实时设置视频发送帧率上限
		/// </summary>
		/// <param name="frame_rate">帧率类型 见NIMVChatVideoFrameRate定义</param>
		/// <param name="json_extension">json_extension  无效备用</param>
		/// <param name="cb">cb 结果回调</param>
		/// <returns>无返回值</returns>
		public static void SetFrameRate(NIMVChatVideoFrameRate frame_rate, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_set_frame_rate(frame_rate, json_extension, VChatNormalOptCb, ptr);
        }

		/// <summary>
		/// 音视频网络探测接口,需要在sdk初始化时带上app key
		/// </summary>
		/// <param name="json_extension">扩展参数，允许用户设置探测时间限制kNIMNetDetectTimeLimit，及探测类型kNIMNetDetectType</param>
		/// <param name="cb">操作结果的回调函数</param>
		/// <returns>探测任务id</returns>
		/// 回调函数json_extension keys:
		/// "task_id":uint64 任务id
		/// "loss":int 丢包率百分比
		/// "rttmax":int rtt 最大值
		/// "rttmin":int rtt 最小值
		/// "rttavg":int rtt 平均值
		/// "rttmdev":int rtt 偏差值 mdev
		/// "detailinfo":string 扩展信息
		/// </param>
		/// <param name="user_data"></param>
		/// <returns>探测任务id
		/// 200:成功
		/// 0:流程错误
		/// 400:非法请求格式
		/// 417:请求数据不对
		/// 606:ip为内网ip
		/// 607:频率超限
		/// 20001:探测类型错误
		/// 20002:ip错误
		/// 20003:sock错误
		/// </returns>
		public static ulong DetectNetwork(NIMVChatNetDetectJsonEx json, NIMVChatOptHandler cb)
        {
            string json_str = "";
            if (json != null)
                json_str = json.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            var ret = VChatNativeMethods.nim_vchat_net_detect(json_str, VChatNormalOptCb, ptr);
            return ret;
        }

		/// <summary>
		/// 设置发送时视频画面的长宽比例裁剪模式，裁剪的时候不改变横竖屏（重新发起时也生效）
		/// </summary>
		/// <param name="type">裁剪模式NIMVChatVideoFrameScaleType</param>
		/// <returns>无返回值</returns>
		public static void SetVideoFrameScale(NIMVChatVideoFrameScaleType type)
		{
			VChatNativeMethods.nim_vchat_set_video_frame_scale(type);
		}

		/// <summary>
		/// 获取视频画面的裁剪模式
		/// </summary>
		/// <returns>当前的裁剪模式NIMVChatVideoFrameScaleType</returns>
		public static NIMVChatVideoFrameScaleType GetVideoFrameScale()
		{
			int ret = VChatNativeMethods.nim_vchat_get_video_frame_scale_type();
			return (NIMVChatVideoFrameScaleType)Enum.ToObject(typeof(NIMVChatVideoFrameScaleType), ret);
		}

        /// <summary>
        /// 通话中修改视频编码模式
        /// </summary>
        /// <param name="mode">选用的策略模式</param>
        /// <param name="json_extension">无效扩展字段</param>
        /// <param name="cb">回调函数,code 见</param>
        public static void NIMVChatSelectVideoAdaptiveStrategy(NIMVChatVideoEncodeMode mode, string json_extension, NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_select_video_adaptive_strategy(mode, json_extension, VChatNormalOptCb, ptr);
        }

        /// <summary>
        ///  互动直播设置uid为房间主画面
        /// </summary>
        /// <param name="uid">用户uid</param>
        /// <param name="json_extension">无效扩展字段</param>
        /// <param name="cb">回调函数</param>
        public static void NIMVChatSetUidPictureAsMain(string uid,string json_extension,NIMVChatOptHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_set_uid_picture_as_main(uid, json_extension, VChatNormalOptCb, ptr);
        }

        /// <summary>
        /// 音视频通话重新连接，用于底层链接在上层认为异常时尝试重连
        /// </summary>
        /// <param name="json_extension">可扩展添加kNIMVChatSessionId，用于指定对应的通话</param>
        /// <param name="cb">操作结果的回调函数，当通话通话不存在或通话</param>
        public static void NIMVChatRelogin(string sessionid, NIMVChatOptHandler cb)
        {
            
            NIMVChatInfo vchatinfo = new NIMVChatInfo();
            vchatinfo.SessionId = sessionid;
            string json_extension = vchatinfo.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            VChatNativeMethods.nim_vchat_relogin(json_extension, VChatNormalOptCb, ptr);
        }

        /// <summary>
        /// 设置播放对端音频静音，全局有效（重新发起时也生效）；此开关打开不播放，但不影响解码及录制
        /// </summary>
        /// <param name="muted"> muted true 静音，false 不静音</param>
        public static void NIMVChatSetAudioPlayMute(bool muted)
        {
            VChatNativeMethods.nim_vchat_set_audio_play_mute(muted);
        }

        /// <summary>
        /// 获取播放对端音频静音状态
        /// </summary>
        /// <returns> bool true 静音，false 不静音</returns>
        public static bool NIMVChatAudioPlayMuteEnabled()
        {
            return VChatNativeMethods.nim_vchat_audio_play_mute_enabled();
        }

        /// <summary>
        ///  设置SDK音视频的网络代理，暂时只支持socks5代理，全局代理接口也能设置音视频的代理，两接口没有优先级区别。
        ///  不需要代理时，type设置为kNIMProxyNone，其余参数都传空字符串（端口设为0）。有些代理不需要用户名和密码，相应参数也传空字符串。
        /// </summary>
        /// <param name="type">代理类型，见NIMProxyType定义,其中音视频和白板暂时只支持kNIMProxySocks5代理</param>
        /// <param name="host">代理地址</param>
        /// <param name="port">代理端口</param>
        /// <param name="user">代理用户名</param>
        /// <param name="password">代理密码</param>
        public static void NIMVChatSetProxy(NIMProxyType type,string host,int port,string user,string password)
        {
            VChatNativeMethods.nim_vchat_set_proxy(type, host, port, user, password);
        }
#endif
    }
}
