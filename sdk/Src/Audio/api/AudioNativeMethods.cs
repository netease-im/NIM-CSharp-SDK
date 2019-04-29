using System.Runtime.InteropServices;

namespace NIMAudio
{
    public static class NIMAudio
    {
        /// <summary>
        /// 操作结果回调
        /// </summary>
        /// <param name="resCode">操作结果，一切正常200</param>
        /// <param name="filePath">播放文件绝对路径</param>
        /// <param name="callId">用以定位资源的一级ID，可选</param>
        /// <param name="resId">用以定位资源的二级ID，可选</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ResCodeIdCb(int resCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string filePath,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string callId,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string resId);

        /// <summary>
        /// 操作结果回调
        /// </summary>
        /// <param name="resCode">操作结果，一切正常200</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NIMResCodeCb(int resCode);


        /// <summary>
        /// 操作回调
        /// </summary>
        /// <param name="resCode">操作结果，一切正常200</param>
        /// <param name="call_id">用以定位资源的一级ID，可选</param>
        /// <param name="res_id">用以定位资源的二级ID，可选</param>
        /// <param name="file_path">文件绝对路径</param>
        /// <param name="file_ext">文件扩展名</param>
        /// <param name="file_size">文件大小</param>
        /// <param name="audio_duration">语音时长</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NIMStopCaptureCb(int resCode,
             [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string call_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string res_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string file_path,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string file_ext,
            int file_size,
            int audio_duration);

        /// <summary>
        /// 获取录音设备操作回调
        /// </summary>
        /// <param name="resCode">操作结果，一切正常200</param>
        /// <param name="deviecs">设备列表</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NIMEnumCaptureDevicesCb(int resCode,
            [MarshalAs(UnmanagedType.LPWStr)]string deviecs);
    }

    class AudioNativeMethods
    {
        //引用C中的方法
        #region NIM Audio C SDK native methods

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_uninit_module", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_uninit_module();

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_stop_play_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_stop_play_audio();

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_stop_capture", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_stop_capture();

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_start_play_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_start_play_cb(NIMAudio.ResCodeIdCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_stop_play_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_stop_play_cb(NIMAudio.ResCodeIdCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_start_capture_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_start_capture_cb(NIMAudio.NIMResCodeCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_stop_capture_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_stop_capture_cb(NIMAudio.NIMStopCaptureCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_cancel_audio_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_cancel_audio_cb(NIMAudio.NIMResCodeCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_reg_enum_capture_device_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_reg_enum_capture_device_cb(NIMAudio.NIMEnumCaptureDevicesCb cb);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_enum_capture_device", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_enum_capture_device();

#if NIM_WIN_DESKTOP_ONLY_SDK
        //pc 接口路径使用宽字符
        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_init_module", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_init_module([MarshalAs(UnmanagedType.LPWStr)]string user_data_parent_path);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_play_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_play_audio(
           [MarshalAs(UnmanagedType.LPWStr)]string filePath,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string callerId,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string resId,
           int format);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_start_capture", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_start_capture(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string call_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string res_id,
            int audio_format,
            int volume,
            [MarshalAs(UnmanagedType.LPWStr)]string device
            );

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_cancel_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_cancel_audio([MarshalAs(UnmanagedType.LPWStr)]string file_path);
#else
        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_init_module", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_init_module(
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string user_data_parent_path);

         [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_play_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_play_audio(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string filePath,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string callerId,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string resId,
            int format);

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_start_capture", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_start_capture(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string call_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string res_id,
            int audio_format,
            int volume,
            int loudness,
            [MarshalAs(UnmanagedType.LPWStr)]string device
            );

        [DllImport(NIM.NativeConfig.NIMAudioNativeDLL, EntryPoint = "nim_audio_cancel_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_audio_cancel_audio(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string file_path
            );
#endif

        #endregion
    }
}
