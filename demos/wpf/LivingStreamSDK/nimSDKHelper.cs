using Newtonsoft.Json.Linq;
using NIM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.LivingStreamSDK
{
	class nimSDKHelper
	{
		public static LsSession session;
		public static LivingStreamForm form;
		public static void Init()
		{
			//注册视频采集数据回调
			//JObject jo = new JObject();
			//jo.Add(new JProperty("subtype", NIMVideoSubType.kNIMVideoSubTypeI420));
			//string json_extention = jo.ToString();
			NIM.NIMVChatCustomAudioJsonEx audioJsonEx = new NIMVChatCustomAudioJsonEx();
			NIM.NIMVChatCustomVideoJsonEx videoJsonEx = new NIMVChatCustomVideoJsonEx();
            videoJsonEx.VideoSubType = Convert.ToInt32(NIMVideoSubType.kNIMVideoSubTypeI420);
			NIM.DeviceAPI.SetVideoCaptureDataCb(VideoDataCaptureHandler, videoJsonEx);
			NIM.DeviceAPI.SetAudioCaptureDataCb(AudioDataCaptureHandler, audioJsonEx);
		}
		public static void StartDevices()
		{
			NIM.StartDeviceResultHandler handle = (type, ret) =>
			{
				System.Diagnostics.Debug.WriteLine(type.ToString() + ":" + ret.ToString());
			};
			NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0,null,handle);//开启麦克风
			NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0,null,handle);//开启扬声器播放对方语音
            NIMDeviceInfoList device_list = NIM.DeviceAPI.GetDeviceList(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
            if (device_list.DeviceList.Count > 0)
                NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo, device_list.DeviceList[0].Path, 0, null, handle);//开启摄像头
		}
		public static void EndDevices()
		{
			NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
			NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
			NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
		}


		private static void VideoDataCaptureHandler(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
		{
			if (session != null)
			{
                if(session.Height!=height&&session.Width!=width)
                {  
                    session.DoStopLiveStream();
                    session.ClearSession();
                  
                    session = new LsSession();
                    session.Width = Convert.ToInt32(width);
                    session.Height = Convert.ToInt32(height);
                    session.InitSession(false, form.GetPushUrl());
                    nimSDKHelper.session.DoStartLiveStream();

                    return;
                }
				byte[] yuv = new byte[size];
                Marshal.Copy(data, yuv, 0, (int)size);
                if(form.Beauty)
                {
                    Beauty.ColorBalance.colorbalance_yuv_u8(yuv, size, 10, 240);
                    Beauty.Smooth.smooth_process(yuv, (int)width,(int)height, 10, 0, 200);
                }

                byte[] rgb = YUVHelper.I420ToARGBRevert(yuv, (int)width, (int)height);
         
				int framesize = (int)(width * height * 4);
				VideoFrame frame = new VideoFrame(rgb, (int)width, (int)height, framesize, (long)time);
				form.ShowVideoFrame(frame);
				if (form.Beauty)
					Marshal.Copy(yuv, 0, data, (int)size);
				session.SendVideoFrame(data, (int)size);
			}
		}

		private static void AudioDataCaptureHandler(UInt64 time, IntPtr data, uint size, int rate)
		{
			if (session != null)
			{
				session.SendAudioFrame(data,(int)size,rate);
			}
		}


	}
}
