
using System;
using System.Runtime.InteropServices;

namespace NIM
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimVchatCbFunc(NIMVideoChatSessionType type, long channel_id, int code,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
           IntPtr user_data);//通知客户端收到一条新聊天室通知消息

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimVchatOptCbFunc(bool ret, int code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
        IntPtr user_data); //操作回调，通用的操作回调接口


    /// <summary>
    /// NIM 操作回调，通用的操作回调接口
    /// </summary>
    /// <param name="code">code 结果代码，code==200表示成功</param>
    /// <param name="channel_id">通道id</param>
    /// <param name="json_extension"> 扩展字段</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimVchatOpt2CbFunc(int code, long channel_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
        IntPtr user_data); //操作回调，通用的操作回调接口
    
    /// <summary>
    /// NIM MP4操作回调，实际的开始录制和结束都会在NIMVChatHanler中返回
    /// </summary>
    /// <param name="ret">结果代码，true表示成功</param>
    /// <param name="code">对应NIMVChatMp4RecordCode,用于获取失败时的错误原因</param>
    /// <param name="file">文件路径</param>
    /// <param name="time">录制结束时有效，对应毫秒级的录制时长</param>
    /// <param name="json_extension">无效扩展字段</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimVchatMp4RecordOptCbFunc(bool ret, int code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string file,
        Int64 time,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
        IntPtr user_data);

    /// <summary>
    /// 音频录制操作回调，实际的开始录制和结束都会在NimVchatCbFunc中返回
    /// </summary>
    /// <param name="ret">结果代码，true表示成功</param>
    /// <param name="code">对应NIMVChatAudioRecordCode，用于获得失败时的错误原因</param>
    /// <param name="file">文件路径</param>
    /// <param name="time">录制结束时有效，对应毫秒级的录制时长</param>
    /// <param name="json_extension">无效扩展字段</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理！</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void NimVchatAudioRecordOptCbFunc(bool ret, int code,
	[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string file,
	Int64 time,
	[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
	IntPtr user_data);

    class VChatNativeMethods
    {
        //引用C中的方法（考虑到不同平台下的C接口引用方式差异，如[DllImport("__Internal")]，[DllImport("nimapi")]等） 
        #region NIM C SDK native methods


        //初始化NIM VCHAT,需要在SDK的nim_client_init成功之后
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

#if UNITY_ANDROID
        internal static extern bool nim_vchat_init(IntPtr json_extension);
#else
        internal static extern bool nim_vchat_init(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension);
#endif

        //清理NIM VCHAT，需要在SDK的nim_client_cleanup之前
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_cleanup", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_cleanup(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension);

        //设置通话回调函数
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_cb_func", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_cb_func(NimVchatCbFunc cb, IntPtr user_data);

        //启动通话
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_start(NIMVideoChatMode mode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string apns_text,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string custom_info,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, IntPtr user_data);

        //设置通话模式
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_talking_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_set_talking_mode(NIMVideoChatMode mode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension);

        //回应邀请
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_callee_ack", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_callee_ack(long channel_id, bool accept,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, IntPtr user_data);

        //通话控制
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_control", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_control(long channel_id, NIMVChatControlType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, IntPtr user_data);

        //结束通话
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_end", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_end([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension);

        //设置观众模式（多人模式下），全局有效（重新发起时也生效），观众模式能减少运行开销
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_viewer_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_viewer_mode(bool viewer);

        //获取当前是否是观众模式
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_get_viewer_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_get_viewer_mode();

        //设置音频静音，全局有效（重新发起时也生效）
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_mute(bool muted);

        //获取音频静音状态
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_audio_mute_enabled", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_audio_mute_enabled();

        //设置单个成员的黑名单状态，当前通话有效(只能设置进入过房间的成员)
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_member_in_blacklist", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_member_in_blacklist(
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string uid, bool add, bool audio,
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
             NimVchatOptCbFunc cb,
             IntPtr user_data);


		//创建一个多人房间（后续需要主动调用加入接口进入房间）
		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_create_room", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_create_room(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string room_name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string custom_info,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOpt2CbFunc cb,
            IntPtr user_data);

        //加入一个多人房间（进入房间后成员变化等，等同点对点NimVchatCbFunc）
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_join_room", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_join_room(
            NIMVideoChatMode mode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string room_name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
             NimVchatOpt2CbFunc cb,
             IntPtr user_data);

		//通话中修改自定义音视频数据模式
		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_custom_data", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void nim_vchat_set_custom_data(bool custom_audio, bool custom_video,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOptCbFunc cb,
			IntPtr user_data);
        //开始录制MP4文件，一次只允许一个录制文件，在通话开始的时候才有实际数据
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start_record", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_start_record(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string path,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatMp4RecordOptCbFunc cb,
            IntPtr user_data);

        //停止录制mp4文件
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_stop_record", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_stop_record(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatMp4RecordOptCbFunc cb,
            IntPtr user_data);

        //开始录制音频文件
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start_audio_record", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_start_audio_record(
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string path,
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
         NimVchatAudioRecordOptCbFunc cb,
         IntPtr user_data);

        //停止录制音频文件
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_stop_audio_record", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_stop_audio_record(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatAudioRecordOptCbFunc cb,
            IntPtr user_data);

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        //对端画面自动旋转
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_rotate_remote_video", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_set_rotate_remote_video(bool rotate);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_rotate_remote_video_enabled", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_rotate_remote_video_enabled();


		//通话中修改分辨率
		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_video_quality", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_video_quality(NIMVChatVideoQuality video_quality,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOptCbFunc cb,
            IntPtr user_data);

        //实时设置视频发送帧率上限
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_frame_rate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_frame_rate(NIMVChatVideoFrameRate frame_rate,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOptCbFunc cb,
            IntPtr user_data);

        //通话中修改视频码率，有效区间[100kb,2000kb],如果设置video_bitrate为0则取默认码率
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_video_bitrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_video_bitrate(int video_bitrate,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
           NimVchatOptCbFunc cb,
           IntPtr user_data);

        

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_update_rtmp_url", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_update_rtmp_url(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string rtmp_url,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOptCbFunc cb,
            IntPtr user_data);

		/* 3.6.0 sdk无此接口
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_streaming_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_streaming_mode(bool streaming,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
           NIMVChatOptHandler cb,
           IntPtr user_data);
		  */

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_net_detect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong nim_vchat_net_detect(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            NimVchatOptCbFunc cb, IntPtr user_data);

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_video_frame_scale", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void nim_vchat_set_video_frame_scale(NIMVChatVideoFrameScaleType type);

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_get_video_frame_scale_type", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int nim_vchat_get_video_frame_scale_type();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_select_video_adaptive_strategy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_select_video_adaptive_strategy(NIMVChatVideoEncodeMode mode, 
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimVchatOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_uid_picture_as_main", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_uid_picture_as_main([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string uid,
          [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimVchatOptCbFunc cb, IntPtr user_data);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_relogin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_relogin([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NimVchatOptCbFunc cb, IntPtr user_data);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_play_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_play_mute(bool muted);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_audio_play_mute_enabled", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_audio_play_mute_enabled();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_proxy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_set_proxy(NIMProxyType type, 
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string host, int port, 
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string user, 
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string password);

#endif

        #endregion
    }
}
