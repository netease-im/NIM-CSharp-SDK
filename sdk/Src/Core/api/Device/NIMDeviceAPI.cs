
using Newtonsoft.Json.Linq;
/** @file NIMDeviceAPI.cs
* @brief NIM VChat提供的音视频设备相关接口，使用前请先调用NIMVChatAPI.cs中Init
* @copyright (c) 2015, NetEase Inc. All rights reserved
* @author gq
* @date 2015/12/8
*/
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM
{
    #region 委托定义
    /// <summary>
    /// 启动设备委托
    /// </summary>
    /// <param name="type">设备类型</param>
    /// <param name="ret">是否成功</param>
    public delegate void StartDeviceResultHandler(NIMDeviceType type, bool ret);

    /// <summary>
    /// 设备状态通知
    /// </summary>
    /// <param name="type">设备类型</param>
    /// <param name="status">NIMDeviceStatus 的或值</param>
    /// <param name="devicePath">重启设备时的设备路径</param>
    public delegate void DeviceStatusHandler(NIMDeviceType type, uint status, string devicePath);

    /// <summary>
    /// 视频数据委托
    /// </summary>
    /// <param name="time">毫秒级时间戳</param>
    /// <param name="data">数据指针， ARGB</param>
    /// <param name="size">数据长途sizeof(char)</param>
    /// <param name="width">画面宽</param>
    /// <param name="height">画面高</param>
    /// <param name="json_extension">Json string kNIMVideoSubType（缺省为kNIMVideoSubTypeARGB），收到对方视频数据返回kNIMDeviceDataUid和kNIMDeviceDataAccount</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理！</param>
    public delegate void VideoDataHandler(ulong time, IntPtr data, uint size, uint width, uint height, string json_extension);

    /// <summary>
    /// 音频数据委托
    /// </summary>
    /// <param name="time">毫秒级时间戳</param>
    /// <param name="data">数据指针， PCM</param>
    /// <param name="size">数据长途sizeof(char)</param>
    /// <param name="rate">PCM数据的采样频率</param>
    public delegate void AudioDataHandler(ulong time, IntPtr data, uint size, int rate);

    /// <summary>
    /// 音频数据监听接口
    /// </summary>
    /// <param name="time">时间毫秒级，暂时无效</param>
    /// <param name="data"> 音频数据pcm格式</param>
    /// <param name="size">data的数据长度</param>
    /// <param name="channels"> 通道数</param>
    /// <param name="rate">采样频</param>
    /// <param name="volume">音量值0-100</param>
    /// <param name="json_extension">扩展</param>
    public delegate void AudioDataExHandler(ulong time, IntPtr data, uint size, int channels, int rate, int volume, string json_extension);

    /// <summary>
    /// 音频数据委托
    /// </summary>
    /// <param name="time">毫秒级时间戳</param>
    /// <param name="data">数据指针， PCM</param>
    /// <param name="size">数据长途sizeof(char)</param>
    /// <param name="rate">PCM数据的采样频率</param>
    public delegate void AudioDataSyncHandler(IntPtr data,ref ulong size, int rate);
    #endregion
    public class DeviceAPI
	{
		private static readonly nim_vchat_start_device_cb_func StartDeviceCb = StartDeviceCallback;
		private static readonly nim_vchat_start_device_cb_func StartExtendCameraCb = StartExtendCamerCallback;
		private static readonly nim_vchat_audio_data_cb_func AudioDataCb = AudioDataCallback;
        private static readonly nim_vchat_enum_device_devpath_sync_cb_func GetDeviceListCb = GetDeviceListCallback;
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		
		private static readonly nim_vchat_device_status_cb_func DeviceStatusCb = DeviceStatusCallback;
		private static readonly nim_vchat_video_data_cb_func VideoDataCb = VideoDataCallback;
		private static readonly nim_vchat_audio_data_cb_func_ex AudioDataExCb = AudioDataExCallback;
#endif
        private static NIMDeviceInfoList _deviceList = null;

#region callback
		private static void GetDeviceListCallback(bool ret, NIMDeviceType type, string jsonExtension, IntPtr userData)
		{
			_deviceList = null;
			if (ret)
			{
				_deviceList = NIMDeviceInfoList.Deserialize(jsonExtension);

			}
		}
		private static void DeviceStatusCallback(NIMDeviceType type, uint status, string devicePath, string jsonExtension, IntPtr userData)
		{
			NimUtility.DelegateConverter.Invoke<DeviceStatusHandler>(userData,type,status,devicePath);
		}

        [MonoPInvokeCallback(typeof(nim_vchat_start_device_cb_func))]
        private static void StartDeviceCallback(NIMDeviceType type, bool ret, string jsonExtension, IntPtr userData)
		{
			try
			{
				NimUtility.DelegateConverter.Invoke<StartDeviceResultHandler>(userData, type, ret);
			}
			catch
			{

			}

		}

		private static void StartExtendCamerCallback(NIMDeviceType type, bool ret, string jsonExtension, IntPtr userData)
		{
			NimUtility.DelegateConverter.Invoke<StartDeviceResultHandler>(userData, type, ret);
		}

        [MonoPInvokeCallback(typeof(nim_vchat_audio_data_cb_func))]
        private static void AudioDataCallback(ulong time, IntPtr data, uint size, string jsonExtension, IntPtr userData)
		{
			if (userData != IntPtr.Zero)
			{
				try
				{
					NIMCustomAudioDataInfo info = NIMCustomAudioDataInfo.Deserialize(jsonExtension);
					NimUtility.DelegateConverter.Invoke<AudioDataHandler>(userData, time, data, size, info.SampleRate);
				}
				catch
				{

				}
			}
		}

		private static void VideoDataCallback(ulong time, IntPtr data, uint size, uint width, uint height, string jsonExtension, IntPtr userData)
		{
			if (userData != IntPtr.Zero)
			{
				try
				{
					NimUtility.DelegateConverter.Invoke<VideoDataHandler>(userData, time, data, size, width, height, jsonExtension);
				}
				catch
				{

				}

			}

		}


		private static void AudioDataExCallback(ulong time, IntPtr data, uint size, int channels, int rate, int volume,string jsonExtension, IntPtr userData)
		{
			if (userData != IntPtr.Zero)
			{
				try
				{
					NimUtility.DelegateConverter.Invoke<AudioDataExHandler>(userData, time, data, size, channels, rate, volume, jsonExtension);
				}
				catch
				{

				}
			}
		}
#endregion

		/// <summary>
		/// 启动设备，同一NIMDeviceType下设备将不重复启动，不同的设备会先关闭前一个设备开启新设备
		/// </summary>
		/// <param name="type">设备类型</param>
		/// <param name="devicePath">设备路径对应</param>
		/// <param name="fps">摄像头为采样频率（一般传电源频率取50）,其他NIMDeviceType无效（麦克风采样频率由底层控制，播放器采样频率也由底层控制）</param>
		/// <param name="StartDeviceInfo">启动设备json封装类</param>
		/// <param name="handler">回调</param>
		/// <returns>无返回值</returns>
		public static void StartDevice(NIMDeviceType type, string devicePath, uint fps, NIMStartDeviceJsonEX StartDeviceInfo,StartDeviceResultHandler handler)
		{

            if (StartDeviceInfo==null)
			{
				StartDeviceInfo = new NIMStartDeviceJsonEX();
			}
			string json_info = StartDeviceInfo.Serialize();
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_start_device(type, devicePath, fps, json_info, StartDeviceCb, ptr);
        }

        /// <summary>
        /// 结束设备
        /// </summary>
        /// <param name="type">设备类型</param>
        /// <returns>无返回值</returns>
        public static void EndDevice(NIMDeviceType type)
		{
            DeviceNativeMethods.nim_vchat_end_device(type, "");
        }

        /// <summary>
        /// 监听采集音频数据（可以不监听，通过启动设备kNIMDeviceTypeAudioOut由底层播放）
        /// </summary>
        /// <param name="handler">回调</param>
        /// <param name="audioJsonEx">json封装类，SampleRate有效,(要求返回的音频数据为指定的采样频，缺省为0使用默认采样频</param>
        /// <returns>无返回值</returns>
        public static void SetAudioCaptureDataCb(AudioDataHandler handler, NIMVChatCustomAudioJsonEx audioJsonEx)
		{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY || UNITY_STANDALONE_WIN
            string audioInfo = "";
			if(audioJsonEx!=null)
				audioInfo = audioJsonEx.Serialize();
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_set_audio_data_cb(true, audioInfo, AudioDataCb, ptr);
#else
#endif
        }

        /// <summary>
        /// 监听接收音频数据（可以不监听，通过启动设备kNIMDeviceTypeAudioOutChat由底层播放）
        /// </summary>
        /// <param name="handler">回调</param>
        /// <returns>无返回值</returns>
        public static void SetAudioReceiveDataCb(AudioDataHandler handler,NIMVChatCustomAudioJsonEx audioJsonEx)
		{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY|| UNITY_STANDALONE_WIN
            string audioInfo = "";
			if(audioJsonEx!=null)
			{
				audioInfo = audioJsonEx.Serialize();
			}
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_set_audio_data_cb(false, audioInfo, AudioDataCb, ptr);
#else
#endif
        }

        /// <summary>
        /// 自定义音频数据接口, 采样位深只支持16或32（Unity PC只支持16),kNIMDeviceSampleRate支持8000，16000，32000，44100.PC下有效
        /// </summary>
        /// <param name="time">时间毫秒级</param>
        /// <param name="data">音频数据pcm格式</param>
        /// <param name="size">data的数据长度 sizeof(char)</param>
        /// <param name="info">拓展json封装类</param>
        /// <returns>bool true 调用成功，false 调用失败</returns>
        public static bool CustomAudioData(ulong time, IntPtr data, uint size, NIMCustomAudioDataInfo info)
		{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY || UNITY_STANDALONE_WIN
            string jsonExtension = "";
			if (info != null)
				jsonExtension = info.Serialize();
			return DeviceNativeMethods.nim_vchat_custom_audio_data(time, data, size, jsonExtension);
#else
            return false;
#endif
        }

        /// <summary>
        /// 遍历设备
        /// </summary>
        /// <param name="type">设备类型</param>
        /// <returns>NIMDeviceInfoList 设备属性列表</returns>
        public static NIMDeviceInfoList GetDeviceList(NIMDeviceType type)
        {
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY||UNITY_STANDALONE_WIN
            DeviceNativeMethods.nim_vchat_enum_device_devpath(type, "", GetDeviceListCb, IntPtr.Zero);
            return _deviceList;
#else
            return null;
#endif
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		/// <summary>
		/// 启动辅助的摄像头，摄像头数据通过SetVideoCaptureDataCb设置采集回调返回，不直接通过视频通话发送给对方，并且不参与设备监听检测
		/// </summary>
		/// <param name="id">摄像头标识，用于开关及数据回调时的对应，不能为空。（同一id下设备将不重复启动，如果设备device_path不同会先关闭前一个设备开启新设备）</param>
		/// <param name="device_path">设备路径</param>
		/// <param name="fps">摄像头为采样频率</param>
		/// <param name="json_extension">打开摄像头是允许设置 kNIMDeviceWidth 和 kNIMDeviceHeight，并取最接近设置值的画面模式</param>
		/// <param name="handler">回调</param>
		///<returns>无返回值</returns>
		public static void StartExtendCamera(string id, string device_path, uint fps, string json_extension, StartDeviceResultHandler handler)
		{
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_start_extend_camera(id, device_path, fps, json_extension, StartExtendCameraCb, ptr);
		}

		/// <summary>
		/// 结束辅助摄像头
		/// </summary>
		/// <param name="id">摄像头标识id，如果为空，则关闭所有辅助摄像头</param>
		/// <returns>无返回值</returns>
		public static void StopExtendCamera(string id)
		{
			DeviceNativeMethods.nim_vchat_stop_extend_camera(id, "");
		}

		/// <summary>
		/// 添加设备监听（摄像头和麦克风） 注意监听设备后底层会定时检查设备情况，在不需要监听后请移除
		/// </summary>
		/// <param name="type">设备类型（kNIMDeviceTypeAudioIn和kNIMDeviceTypeVideo有效）</param>
		/// <param name="handler">回调</param>
		/// <returns>无返回值</returns>
		public static void AddDeviceStatusCb(NIMDeviceType type, DeviceStatusHandler handler)
		{
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_add_device_status_cb(type, DeviceStatusCb, ptr);
		}

		/// <summary>
		/// 移除设备监听（摄像头和麦克风）
		/// </summary>
		/// <param name="type">设备类型</param>
		/// <returns>无返回值</returns>
		public static void RemoveDeviceStatusCb(NIMDeviceType type)
		{
			DeviceNativeMethods.nim_vchat_remove_device_status_cb(type);
		}

		/// <summary>
		/// 监听采集的视频数据
		/// </summary>
		/// <param name="handler">回调</param>
        /// <param name="videoJsonEx">json 参数封装类</param>
		/// <returns>无返回值</returns>
		public static void SetVideoCaptureDataCb(VideoDataHandler handler, NIMVChatCustomVideoJsonEx videoJsonEx)
		{
			string json_extension = "";
			if(videoJsonEx==null)
			{
				videoJsonEx = new NIMVChatCustomVideoJsonEx();
				videoJsonEx.VideoSubType = Convert.ToInt32(NIMVideoSubType.kNIMVideoSubTypeARGB);
			}
			json_extension = videoJsonEx.Serialize();
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_set_video_data_cb(true, json_extension, VideoDataCb, ptr);
		}

		/// <summary>
		/// 监听接收的视频数据
		/// </summary>
		/// <param name="handler">回调</param>
        /// <param name="videoJsonEx">json参数封装类</param>
		/// <returns>无返回值</returns>
		public static void SetVideoReceiveDataCb(VideoDataHandler handler, NIMVChatCustomVideoJsonEx videoJsonEx)
		{
            string json_extension = "";
            if (videoJsonEx == null)
            {
                videoJsonEx = new NIMVChatCustomVideoJsonEx();
                videoJsonEx.VideoSubType = Convert.ToInt32(NIMVideoSubType.kNIMVideoSubTypeARGB);
            }
            json_extension = videoJsonEx.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
			DeviceNativeMethods.nim_vchat_set_video_data_cb(false, json_extension, VideoDataCb, ptr);
		}

		/// <summary>
		/// 音频采集音量，默认255
		/// </summary>
		public static byte AudioCaptureVolumn
		{
			get { return DeviceNativeMethods.nim_vchat_get_audio_volumn(true); }
			set { DeviceNativeMethods.nim_vchat_set_audio_volumn(value, true); }
		}

		/// <summary>
		/// 音频播放音量，默认255
		/// </summary>
		public static byte AudioPlayVolumn
		{
			get { return DeviceNativeMethods.nim_vchat_get_audio_volumn(false); }
			set { DeviceNativeMethods.nim_vchat_set_audio_volumn(value, false); }
		}

		/// <summary>
		/// 设置麦克风音量自动调节, 默认不自动调节
		/// </summary>
		public static bool AudioCaptureAutoVolumn
		{
			get { return DeviceNativeMethods.nim_vchat_get_audio_input_auto_volumn(); }
			set { DeviceNativeMethods.nim_vchat_set_audio_input_auto_volumn(value); }
		}



				/// <summary>
		/// 自定义视频数据接口
		/// </summary>
		/// <param name="time">时间毫秒级</param>
		/// <param name="data">视频数据yuv420格式</param>
		/// <param name="size">data的数据长度 sizeof(char)</param>
		/// <param name="width">画面宽度</param>
		/// <param name="height">画面高度</param>
		/// <param name="info">扩展Json封装类</param>
		/// <returns> bool true 调用成功，false 调用失败</returns>
		public static bool CustomVideoData(ulong time, IntPtr data, uint size, uint width, uint height, NIMCustomVideoDataInfo info)
		{
			string jsonExtension = "";
			if (info != null)
				jsonExtension = info.Serialize();
			return DeviceNativeMethods.nim_vchat_custom_video_data(time, data, size, width, height, jsonExtension);
		}

		/// <summary>
		/// 设置底层针对麦克风采集数据处理开关接口，默认全开（此接口是全局接口，在sdk初始化后设置一直有效）
		/// </summary>
		/// <param name="aec">true 标识打开回音消除功能，false 标识关闭</param>
		/// <param name="ns">true 标识打开降噪功能，false 标识关闭</param>
		/// <param name="vid">true 标识打开人言检测功能，false 标识关闭</param>
		/// <returns>无返回值</returns>
		public static void SetAudioProcessInfo(bool aec, bool ns, bool vid)
		{
			DeviceNativeMethods.nim_vchat_set_audio_process_info(aec, ns, vid);
		}

        /// <summary>
        ///  监听音频数据（可以不监听，通过启动设备kNIMDeviceTypeAudioOut和kNIMDeviceTypeAudioOutChat由底层播放）
        /// </summary>
        /// <param name="type">type 指定NIMAudioDataCbType，监听伴音数据时，一旦监听，底层将不再混音</param>
        /// <param name="json_extension">json_extension 参考NIMAudioDataCbType的说明</param>
        /// <param name="handler">回调</param>
        public static void SetAudioDataCbEx(NIMAudioDataCbType type, string json_extension, AudioDataExHandler handler)
		{
			var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            int audio_data_type = Convert.ToInt32(type);
			DeviceNativeMethods.nim_vchat_set_audio_data_cb_ex(audio_data_type, json_extension, AudioDataExCallback, ptr);
		}

        /// <summary>
        /// 设置底层针对麦克风采集数据处理开关接口，默认关闭啸叫检测（此接口是全局接口，在sdk初始化后设置一直有效）
        /// </summary>
        /// <param name="work">true 标识打开啸叫检测功能，false 标识关闭</param>
        /// v5.5接口取消
        //  public static void SetAudioHowlingSuppression(bool work)
        //  {
        //      DeviceNativeMethods.nim_vchat_set_audio_howling_suppression(work);
        //  }

        /// <summary>
        /// 自定义音频伴音数据接口，不需要打开自定义数据开关, 采样时间必须为10ms的整数倍, 采样位深只支持16
        /// </summary>
        /// <param name="id">伴音数据id</param>
        /// <param name="time">time 时间毫秒级</param>
        /// <param name="data">音频数据pcm格式</param>
        /// <param name="size">data的数据长度</param>
        /// <param name="rate">音频采样频</param>
        /// <param name="channels">声道数</param>
        /// <param name="json_extension">json_extension 扩展</param>
        /// <returns>bool true 调用成功，false 调用失败</returns>
        public static bool NimVchatAccompanyingSound(Byte id, UInt64 time, IntPtr data, UInt32 size, UInt32 rate, UInt32 channels,string json_extension)
        {
            return DeviceNativeMethods.nim_vchat_accompanying_sound(id, time, data, size, rate, channels, json_extension);
        }
#else
        private static readonly nim_vchat_audio_data_sync_cb_func AudioDataSyncCb = AudioDataSyncCallback;

        [MonoPInvokeCallback(typeof(nim_vchat_audio_data_sync_cb_func))]
        private static ulong AudioDataSyncCallback(IntPtr data, ulong size, double sample_rate, IntPtr userData)
        {
            if (userData != IntPtr.Zero)
            {
                try
                {
                    AudioDataSyncHandler AudioDataSyncCb = NimUtility.DelegateConverter.ConvertFromIntPtr<AudioDataSyncHandler>(userData);
                    if (AudioDataSyncCb!=null)
                        AudioDataSyncCb(data, ref size, Convert.ToInt32(sample_rate));
                }
                catch
                {

                }
            }
            return size;
        }

        /// <summary>
        /// 监听采集音频数据 同步操作,Android ios有效
        /// </summary>
        /// <param name="handler">回调</param>
        /// <returns>无返回值</returns>
        public static void SetAudioCaptureDataSyncCb(AudioDataSyncHandler handler)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            DeviceNativeMethods.nim_vchat_set_audio_data_sync_cb(AudioDataSyncCb,"",ptr);

        }

        /// <summary>
        /// 设置扬声器 android,ios有效
        /// </summary>
        /// <param name="speaker_on">是否开启扬声器 true:开启</param>
        public static void SetSpeaker(bool speaker_on)
        {
            DeviceNativeMethods.nim_vchat_set_speaker(speaker_on);
        }

        /// <summary>
        /// 扬声器状态 android,ios有效
        /// </summary>
        /// <returns>扬声器是否开启，true:开启</returns>
        public static bool SpeakerEnabled()
        {
            return DeviceNativeMethods.nim_vchat_speaker_enabled();
        }

        /// <summary>
        /// 设置麦克风静音 android,ios有效
        /// </summary>
        /// <param name="mute">是否静音 true:静音</param>
        public static void SetMicrophoneMute(bool mute)
        {
            DeviceNativeMethods.nim_vchat_set_microphone_mute(mute);
        }

        /// <summary>
        /// 获得麦克风静音状态 
        /// </summary>
        /// <returns>是否静音 true:静音</returns>
        public static bool IsMicroPhoneMute()
        {
            return DeviceNativeMethods.nim_vchat_is_microphone_mute();
        }

        /// <summary>
        /// 开启混音 Android,ios有效
        /// </summary>
        /// <param name="filePath">可播放文件地址</param>
        /// <param name="loopback">是否循环播放 true:开启</param>
        /// <param name="replace">是否替换采集声音数据,true:开启</param>
        /// <param name="cycle">循环次数</param>
        /// <param name="volume">混音音量值 [0f-1f]</param>
        public static void StartAudioMixing(String filePath, bool loopback, bool replace, int cycle, float volume)
        {
            DeviceNativeMethods.nim_vchat_start_audio_mixing(filePath, loopback, replace, cycle, volume);
        }

        /// <summary>
        /// 暂停混音 Android,ios有效
        /// </summary>
        /// <returns>true:接口调用成功</returns>
        public static bool PauseAudioMixing()
        {
            return DeviceNativeMethods.nim_vchat_pause_audio_mixing();
        }

        /// <summary>
        /// 恢复混音 Android,ios有效
        /// </summary>
        /// <returns>true:接口调用成功</returns>
        public static bool ResumeAudioMixing()
        {
            return DeviceNativeMethods.nim_vchat_resume_audio_mixing();
        }

        /// <summary>
        /// 结束混音 Android,ios有效
        /// </summary>
        /// <returns>true:接口调用成功</returns>
        public static bool StopAudioMixing()
        {
            return DeviceNativeMethods.nim_vchat_stop_audio_mixing();
        }

        /// <summary>
        /// 设置混音音量 Android,ios有效
        /// </summary>
        /// <param name="volume">音量值 [0f-1f]</param>
        /// <returns></returns>
        public static bool SetAudioMixingVolume(float volume)
        {
            return DeviceNativeMethods.nim_vchat_set_audio_mixing_volume(volume);
        }

        /// <summary>
        /// 设置耳返音量 Android,ios有效
        /// </summary>
        /// <param name="volume">耳返音量值 [0f-1f]</param>
        /// <returns></returns>
        public static bool SetEarphoneMonitorAudioVolume(float volume)
        {
            return DeviceNativeMethods.nim_vchat_set_earphone_monitor_audio_volume(volume);
        }
#endif

    }
}
