using System;
using System.Runtime.InteropServices;

namespace NIM
{
    //多端推送设置/同步回调
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_client_multiport_push_config_cb_func(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string content,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_params,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_client_dnd_cb_func(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string content,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_params,
        IntPtr user_data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_client_cleanup2_cb_func([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_params, IntPtr user_data);
    class ClientNativeMethods
    {
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool nim_client_init([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string app_data_dir,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string app_install_dir,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_cleanup", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_cleanup([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_login", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_login([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string appKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string account, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string token, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonExtension, NIMGlobal.JsonTransportCb cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_relogin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_relogin([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonExtension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_logout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_logout(NIMLogoutType logout_type, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonExtension, NIMGlobal.JsonTransportCb cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_kick_other_client", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_kick_other_client([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_auto_relogin_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_auto_relogin_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_kickout_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_kickout_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_disconnect_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_disconnect_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_multispot_login_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_multispot_login_notify_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_kickout_other_client_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_kickout_other_client_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_reg_sync_multiport_push_config_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_reg_sync_multiport_push_config_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
            nim_client_multiport_push_config_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_set_multiport_push_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_set_multiport_push_config(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string switch_content,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
            nim_client_multiport_push_config_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_get_multiport_push_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_get_multiport_push_config([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
            nim_client_multiport_push_config_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_update_apns_token", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_update_apns_token([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string device_token);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_set_dnd_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_set_dnd_config([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string cfg_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
            nim_client_dnd_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_get_dnd_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_get_dnd_config(nim_client_dnd_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_get_login_state", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int nim_client_get_login_state([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr nim_client_version();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_client_cleanup2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_client_cleanup2(nim_client_cleanup2_cb_func cb,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension);
    }
}
