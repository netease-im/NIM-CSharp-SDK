
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM
{
    static class DeviceNativeMethods
    {
        //引用C中的方法（考虑到不同平台下的C接口引用方式差异，如[DllImport("__Internal")]，[DllImport("nimapi")]等） 
        #region NIM C SDK native methods


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start_device", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_start_device(NIMDeviceType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string device_path, uint fps,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_vchat_start_device_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_end_device", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_end_device(NIMDeviceType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

  
		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_data_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_data_cb(bool capture,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_vchat_audio_data_cb_func cb, IntPtr user_data);

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY || UNITY_STANDALONE_WIN
		//自定义音频数据
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_custom_audio_data", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_custom_audio_data(ulong time, IntPtr data, uint size,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_enum_device_devpath", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_enum_device_devpath(NIMDeviceType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_vchat_enum_device_devpath_sync_cb_func cb, IntPtr user_data);
#endif

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_volumn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_volumn(byte volumn, bool capture);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_get_audio_volumn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte nim_vchat_get_audio_volumn(bool capture);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_input_auto_volumn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_input_auto_volumn(bool auto_volumn);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_get_audio_input_auto_volumn", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_get_audio_input_auto_volumn();

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_video_data_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_video_data_cb(bool capture,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_vchat_video_data_cb_func cb, IntPtr user_data);



		 [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_add_device_status_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_add_device_status_cb(NIMDeviceType type, nim_vchat_device_status_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_remove_device_status_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_remove_device_status_cb(NIMDeviceType type);

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start_extend_camera", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void nim_vchat_start_extend_camera(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string id,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string device_path,
			uint fps,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
			nim_vchat_start_device_cb_func cb,
			IntPtr user_data);

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_stop_extend_camera", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void nim_vchat_stop_extend_camera(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string id,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

		//自定义视频数据
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_custom_video_data", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_custom_video_data(ulong time, IntPtr data, uint size, uint width, uint height,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_process_info", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_process_info(bool aec, bool ns, bool vid);

		[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_data_cb_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void nim_vchat_set_audio_data_cb_ex(int type,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, nim_vchat_audio_data_cb_func_ex cb, IntPtr user_data);


//         [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_howling_suppression", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
//         internal static extern void nim_vchat_set_audio_howling_suppression(bool work);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_accompanying_sound", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_accompanying_sound( Byte id,UInt64 time,IntPtr data,UInt32 size,UInt32 rate,UInt32 channels, 
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);
#else
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_data_sync_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_audio_data_sync_cb(nim_vchat_audio_data_sync_cb_func cb,
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_speaker", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_speaker(bool speak_on);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_speaker_enabled", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_speaker_enabled();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_microphone_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_vchat_set_microphone_mute(bool mute);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_is_microphone_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_is_microphone_mute();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_start_audio_mixing", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_start_audio_mixing([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] String file_path,
            bool loopback, bool replace, int cycle, float volume);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_pause_audio_mixing", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_pause_audio_mixing();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_resume_audio_mixing", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_resume_audio_mixing();


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_stop_audio_mixing", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_stop_audio_mixing();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_earphone_monitor_audio_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_set_earphone_monitor_audio_volume(float volume);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_vchat_set_audio_mixing_volume", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_vchat_set_audio_mixing_volume(float volume);

#endif

        #endregion
    }
}
