using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIMDemo.LivingStreamSDK
{
	class HNLSSERVICE
	{
		int iUnused;
	}

	class LsManage
	{
		public static bool Init()
		{
			return true;
		}
		public static bool Exit()
		{
			return true;
		}
	}

	class LsSession
	{
		#region 成员
		private IntPtr pls_client_ = IntPtr.Zero;
		private struct_NLSS_PARAM ls_para_;
		private int width_ = 640;
		private int height_ = 480;
		private bool live_streaming_;

		#endregion
        public int Width
        {   
            get { return width_; }
            set { width_ = value; }
        }

        public int Height
        {
            get { return height_; }
            set { height_ = value; }
        }


		public bool InitSession(bool screen, string url)
		{
			if(pls_client_!=IntPtr.Zero)
			{
				ClearSession();
			}


            string path = NIM.ToolsAPI.GetLocalAppDataDir() + @"NIMCSharpDemo\NIM\NIM_LS";

            IntPtr ptr = new IntPtr();
			int ls_ret=LSClientNativeMethods.Nlss_Create(path,out ptr);
			System.Diagnostics.Debug.Assert(ls_ret == 0);
			ls_para_.stAudioParam.enInType = enum_NLSS_AUDIOIN_TYPE.EN_NLSS_AUDIOIN_RAWDATA;
			ls_para_.stVideoParam.enInType = enum_NLSS_VIDEOIN_TYPE.EN_NLSS_VIDEOIN_RAWDATA;

			pls_client_ = new IntPtr(ptr.ToInt64());
			int nSizeOfParam = Marshal.SizeOf(ls_para_);
			IntPtr ptr_ls = Marshal.AllocHGlobal(nSizeOfParam);
			int ls_ret_default = -1;
			try
			{
				Marshal.StructureToPtr(ls_para_, ptr_ls, false);
				ls_ret_default = LSClientNativeMethods.Nlss_GetDefaultParam(pls_client_, ptr_ls);
				ls_para_ = (struct_NLSS_PARAM)Marshal.PtrToStructure(ptr_ls, typeof(struct_NLSS_PARAM));
			}
			catch(Exception ex)
			{

			}
			finally
			{
				Marshal.FreeHGlobal(ptr_ls);
			}
			System.Diagnostics.Debug.Assert(ls_ret_default == 0);

			ls_para_.paOutUrl = url;

			//音频
			ls_para_.stAudioParam.iInSamplerate = 44100;
			ls_para_.stAudioParam.iInnumOfChannels = 1;
			ls_para_.stAudioParam.iInBitsPerSample = 16;
			ls_para_.stAudioParam.enInFmt = enum_NLSS_AUDIOIN_FMT.EN_NLSS_AUDIOIN_FMT_S16;

			//视频
			if(width_==1280)
			{
				//width_ = 1280;
				//height_ = 720;
				ls_para_.stVideoParam.iOutBitrate = 1500000;
			}
			else
			{
				//width_ = 640;
				//height_ = 480;
				ls_para_.stVideoParam.iOutBitrate = 700000;
			}
			ls_para_.stVideoParam.stInCustomVideo.enVideoInFmt = enum_NLSS_VIDEOIN_FMT.EN_NLSS_VIDEOIN_FMT_I420;
			ls_para_.stVideoParam.stInCustomVideo.iInWidth = width_;
			ls_para_.stVideoParam.stInCustomVideo.iInHeight = height_;
			ls_para_.stVideoParam.stInCustomVideo.iYStride = width_;
			ls_para_.stVideoParam.stInCustomVideo.iUVStride = width_ / 2;

			int nSizeOfParam2 = Marshal.SizeOf(ls_para_);
			IntPtr ptr_ls2 = Marshal.AllocHGlobal(nSizeOfParam2);

			try
			{
				Marshal.StructureToPtr(ls_para_, ptr_ls2, false);
				struct_NLSS_PARAM param = (struct_NLSS_PARAM)Marshal.PtrToStructure(ptr_ls2, typeof(struct_NLSS_PARAM));
				int ls_ret_init = LSClientNativeMethods.Nlss_InitParam(pls_client_, ptr_ls2);
				System.Diagnostics.Debug.Assert(ls_ret_init==0);
			}
			catch(Exception ex)
			{

			}
			finally
			{
				Marshal.FreeHGlobal(ptr_ls2);
			}
			
			nimSDKHelper.Init();
			return true;
		}

		public bool OnStartPreview()
		{
			LSClientNativeMethods.Nlss_StartVideoPreview(pls_client_);
			return true;
		}

		//开始直播
		public void DoStartLiveStream()
		{
			bool ret = false;
			if (live_streaming_)
			{
				ret = true;
			}
			else if (pls_client_ != IntPtr.Zero)
			{
				int ls_ret = LSClientNativeMethods.Nlss_StartLiveStream(pls_client_);
				System.Diagnostics.Debug.Assert(ls_ret == 0);

				if (ls_ret == 0)
				{
					live_streaming_ = true;
					nimSDKHelper.StartDevices();
				}
			}
		}

		//结束直播
		public void DoStopLiveStream()
		{
			if(live_streaming_&&pls_client_!=IntPtr.Zero)
			{
				LSClientNativeMethods.Nlss_StopLiveStream(pls_client_);
				live_streaming_ = false;
				nimSDKHelper.EndDevices();
			}
		}

		//发送视频帧
		public void SendVideoFrame(IntPtr data,int size)
		{
			int ret = -1;
			if (pls_client_ != IntPtr.Zero && live_streaming_)
			{
				int errorCode = 0;
				try
				{
					ret = LSClientNativeMethods.Nlss_SendCustomVideoData(pls_client_, data, size, ref errorCode);
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
			}
		}

		//发送音频帧
		public void SendAudioFrame(IntPtr data, int size, int rate)
		{
			if(pls_client_!=IntPtr.Zero&&live_streaming_)
			{
				if(data!=IntPtr.Zero)
				{
					int errorCode = 0;
					try
					{
						int ret = LSClientNativeMethods.Nlss_SendCustomAudioData(pls_client_, data,size,rate,ref errorCode);
						System.Diagnostics.Debug.Assert(errorCode == 0);
					}
					catch(Exception)
					{

					}
				}
			}
		}

		public void ClearSession()
		{
			if(pls_client_!=IntPtr.Zero)
			{
				if(live_streaming_)
				{
					LSClientNativeMethods.Nlss_StopLiveStream(pls_client_);
				}
				LSClientNativeMethods.Nlss_UninitParam(pls_client_);
				LSClientNativeMethods.Nlss_Destroy(pls_client_);
			}
		}
	}
}
