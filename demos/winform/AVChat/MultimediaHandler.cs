using NIMDemo.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NIM;
namespace NIMDemo
{
    public class PeopleStatusEventAgrs : EventArgs
    {
        public long channel_id;
        public string uid;
        public int status;

        public PeopleStatusEventAgrs()
        {

        }
    }

    /// <summary>
    /// 音视频处理事件类
    /// </summary>
    public class MultimediaHandler
    {
        private long channel_id;
        private static NIM.NIMVChatSessionStatus _vchatHandlers;
        private static  Form _ownerMainForm = null;
        private static MultimediaHandler media_handler = null;
        public static EventHandler<VideoEventAgrs> ReceiveVideoFrameHandler;
        public static EventHandler<VideoEventAgrs> CapturedVideoFrameHandler;
        public EventHandler<PeopleStatusEventAgrs>   PeopleStatusHandler;
        

        public static MultimediaHandler GetInstance()
        {
            if(media_handler == null)
            {
                media_handler = new MultimediaHandler();
            }
            return media_handler;
        }

        private MultimediaHandler()
        {

        }

        public void Init(Form owner)
        {
            _ownerMainForm = owner;
            InitVChatInfo();
        }

        public  void GetVChatSessionStatusHander(out NIMVChatSessionStatus vchat_handlers)
        {
            vchat_handlers=_vchatHandlers;
        }

        public static void InitVChatInfo()
        {
            _vchatHandlers.onSessionStartRes = OnSessionStartRes;
            _vchatHandlers.onSessionInviteNotify = OnSessionInviteNotify;
            _vchatHandlers.onSessionCalleeAckRes = OnSessionCalleeAckRes;
            _vchatHandlers.onSessionCalleeAckNotify = OnSessionCalleeAckNotify;
            _vchatHandlers.onSessionControlRes = OnSessionControlRes;
            _vchatHandlers.onSessionControlNotify = OnSessionControlNotify;
            _vchatHandlers.onSessionConnectNotify = OnSessionConnectNotify;
            _vchatHandlers.onSessionMp4InfoStateNotify = OnSessionMp4InfoStateNotify;
            _vchatHandlers.onSessionPeopleStatus = OnSessionPeopleStatus;
            _vchatHandlers.onSessionNetStatus = OnSessionNetStatus;
            _vchatHandlers.onSessionHangupRes = OnSessionHangupRes;
            _vchatHandlers.onSessionHangupNotify = OnSessionHangupNotify;

            _vchatHandlers.onSessionSyncAckNotify = (channel_id, code, uid, mode, accept, time, client) =>
            {

            };

            //注册音视频会话交互回调
            NIM.VChatAPI.SetSessionStatusCb(_vchatHandlers);
            //注册音频接收数据回调
            NIM.DeviceAPI.SetAudioReceiveDataCb(AudioDataRecHandler, null);
            //注册视频接收数据回调
            NIM.DeviceAPI.SetVideoReceiveDataCb(VideoDataRecHandler, null);
            //注册视频采集数据回调
            NIM.DeviceAPI.SetVideoCaptureDataCb(VideoDataCaptureHandler, null);

            NIM.DeviceAPI.AddDeviceStatusCb(NIM.NIMDeviceType.kNIMDeviceTypeVideo, DeviceStatusHandler);
        }

        #region 音视频回调方法
        private static void OnSessionStartRes (long channel_id, int code)
        {
            Action action = () =>
            {
                if (code != 200)
                {
					MessageBox.Show("发起音视频聊天失败");
                }
            };
            _ownerMainForm.BeginInvoke(action);
        }

        private static void OnSessionInviteNotify(long channel_id, string uid, int mode, long time,string custom_info)
        {
            if (GetInstance().channel_id != 0 && channel_id != GetInstance().channel_id)
            {
                NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
                NIM.VChatAPI.CalleeAck(channel_id, false, info);
            }
            else
            {
                VideoChatForm vform = VideoChatForm.GetInstance();
                RtsForm rtsForm = RtsForm.GetInstance();
                VideoChatInfo vchat_info = vform.VchatInfo;
                //当前不存在rts会话和音视频会话,才能开启新的会话
                if (rtsForm.RtsState==RtsFormState.kRtsInit&&vchat_info.state == VChatState.kVChatUnknow)
                {
                    vchat_info.channel_id = channel_id;
                    vchat_info.uid = uid;
                    vchat_info.state = VChatState.kVChatNotify;
                    vchat_info.chat_mode = (NIMVideoChatMode)mode;
                    vform.VchatInfo = vchat_info;
                    Action a = () =>
                    {
                        vform.Show();
                    };
                    _ownerMainForm.BeginInvoke(a);
                }
                else
                {
                    NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
                    NIM.VChatAPI.CalleeAck(channel_id, false, info);
                }

            }

        }

        private static void OnSessionCalleeAckRes(long channel_id, int code)
        {
            
        }

        private static void OnSessionCalleeAckNotify(long channel_id, string uid, int mode, bool accept)
        {
            if (accept)
            {
                DemoTrace.WriteLine("对方接听");
            }
            else
            {
                NIMDemo.VideoChatForm vchat_form = VideoChatForm.GetInstance();
                if(vchat_form.VchatInfo.state==VChatState.kVChatInvite)
                {
                    VideoChatInfo vchat_info = vchat_form.VchatInfo;
                    vchat_info.state = VChatState.kVChatInviteRefuse;
                    vchat_form.VchatInfo = vchat_info;
                }
            }
        }

        private static void OnSessionControlRes(long channel_id, int code, int type)
        {

        }

        private static void OnSessionControlNotify(long channel_id, string uid, int type)
        {
			if (Enum.IsDefined(typeof(NIM.NIMVChatControlType),type))
			{
				NIM.NIMVChatControlType control_type = (NIM.NIMVChatControlType)type;
				switch(control_type)
				{
					case NIMVChatControlType.kNIMTagControlOpenAudio:
						break;
					case NIMVChatControlType.kNIMTagControlCloseAudio:
						break;
					case NIMVChatControlType.kNIMTagControlOpenVideo:
						break;
					case NIMVChatControlType.kNIMTagControlCloseVideo:
						break;
					case NIMVChatControlType.kNIMTagControlAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlAgreeAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlRejectAudioToVideo:
						break;
					case NIMVChatControlType.kNIMTagControlVideoToAudio:
						break;
					case NIMVChatControlType.kNIMTagControlBusyLine:
						{
							NIM.VChatAPI.End();
						}
						break;
					case NIMVChatControlType.kNIMTagControlCamaraNotAvailable:
						break;
					case NIMVChatControlType.kNIMTagControlEnterBackground:
						break;
					case NIMVChatControlType.kNIMTagControlReceiveStartNotifyFeedback:
						break;
					case NIMVChatControlType.kNIMTagControlMp4StartRecord:
						break;
					case NIMVChatControlType.kNIMTagControlMp4StopRecord:
						break;
				}
			}
        }


        private  static  void OnSessionMp4InfoStateNotify(long channel_id,int code, NIMVChatMP4State mp4_info)
        {
            string log = "";
            if (mp4_info != null)
            {
                if (mp4_info.MP4_Close != null)
                {
                    log += "close:channel_id:" + channel_id.ToString() + " file_path:" + mp4_info.MP4_Close.FilePath + " duration:" + mp4_info.MP4_Close.Duration;
                }
                if (mp4_info.MP4_Start != null)
                {
                    log += "start:channel_id:" + channel_id.ToString() + "file_path:" + mp4_info.MP4_Start.FilePath + " duration:" + mp4_info.MP4_Start.Duration;
                }
            }
            DemoTrace.WriteLine("SessionMp4Info " + log);
        }
            
        private static void OnSessionConnectNotify(long channel_id, int code, string record_file, string video_record_file,long chat_time,ulong chat_rx,ulong chat_tx)
        {
            DemoTrace.WriteLine("Session Connect channel_id:" + channel_id.ToString() +
                " code:" + code.ToString() + " record_file:" + record_file + " video_record_file" + video_record_file + 
                "chat_time:" + chat_time.ToString() + "chat_rx:" + chat_rx.ToString() + "chat_tx" + chat_tx.ToString());
            if (code == 200)
            {
                Action a = () =>
                {
                    GetInstance().channel_id = channel_id;
                    VideoChatForm vform = VideoChatForm.GetInstance();
                    VideoChatInfo vchatInfo = vform.VchatInfo;
                    if (vchatInfo.state != VChatState.kVChatUnknow
                    && vchatInfo.state != VChatState.VChatEnd
                    && vchatInfo.state != VChatState.kVChatInviteRefuse)
                    {
                        vchatInfo.state = VChatState.kVChating;
                        vform.VchatInfo = vchatInfo;
                    }
                };
                _ownerMainForm.BeginInvoke(a);
                StartDevices();

            }
            else
            {
                NIM.VChatAPI.End();
                GetInstance().channel_id = 0;
            }
        }

        private static void OnSessionPeopleStatus(long channel_id, string uid, int status)
        {
			DemoTrace.WriteLine("SessionPeopleStatus channel_id:" + channel_id.ToString() + " status:" + status.ToString() + " uid:" + uid);
            if(GetInstance().PeopleStatusHandler!=null)
            {
                PeopleStatusEventAgrs args = new PeopleStatusEventAgrs();
                args.channel_id = channel_id;
                args.uid = uid;
                args.status = status;
                GetInstance().PeopleStatusHandler(GetInstance(), args);
            }
		}

        private static void OnSessionNetStatus(long channel_id, int status,string uid)
        {
			DemoTrace.WriteLine("SessionNetStatus channel_id:" + channel_id.ToString() + " status:" + status.ToString() + " uid:" + uid);
		}

        private static void OnSessionHangupRes(long channel_id, int code)
        {
            EndDevices();
        }

        private static void OnSessionHangupNotify(long channel_id, int code)
        {
            EndDevices();
			if (code == 200)
			{
				Action action = () =>
				{
					//MessageBox.Show("已挂断");

					VideoChatForm vform = VideoChatForm.GetInstance();
                    VideoChatInfo vchat_info = vform.VchatInfo;
                    vchat_info.state = VChatState.VChatEnd;
                    vform.VchatInfo = vchat_info;

				};
				_ownerMainForm.BeginInvoke(action);
			}
        }
        #endregion

       

		private static void DeviceStatusHandler(NIM.NIMDeviceType type,uint status,string devicePath)
		{

		}

       private static void AudioDataRecHandler(UInt64 time, IntPtr data, UInt32 size, Int32 rate)
        {

        }


        //捕获视频帧回调函数
        private static void VideoDataCaptureHandler(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
			VideoFrame frame = new VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
			try
			{
				if (CapturedVideoFrameHandler != null)
				{
					CapturedVideoFrameHandler(_ownerMainForm, new VideoEventAgrs(frame));
				}
			}
			catch
			{
				return;
			}

        }

        //收到视频帧回调函数
       private static void VideoDataRecHandler(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
			VideoFrame frame = new VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
			try
			{
				if (ReceiveVideoFrameHandler != null)
				{
					ReceiveVideoFrameHandler(_ownerMainForm, new VideoEventAgrs(frame));
				}
			}
			catch
			{
				return;
			}
        }


        private static void StartDevices()
        {
            NIM.StartDeviceResultHandler handle = (type, ret) =>
            {
                System.Diagnostics.Debug.WriteLine(type.ToString() + ":" + ret.ToString());
            };
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0, null,handle);//开启麦克风
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0,null, handle);//开启扬声器播放对方语音
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo, "", 0,null, handle);//开启摄像头
        }
        public static void EndDevices()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeSoundcardCapturer);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioHook);
        }
    }
}
