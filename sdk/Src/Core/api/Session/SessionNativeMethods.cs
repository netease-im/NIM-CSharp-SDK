using System;
using System.Runtime.InteropServices;
using NimUtility;

namespace NIM.Session
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimSessionChangeCbFunc(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string result, 
        int total_unread_counts,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimSessionQueryRecentSessionCbFunc(int total_unread_count,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    internal static class SessionNativeMethods
    {
        //引用C中的方法（考虑到不同平台下的C接口引用方式差异，如[DllImport("__Internal")]，[DllImport("nimapi")]等） 

        #region NIM C SDK native methods

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_reg_change_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_reg_change_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
            NimSessionChangeCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_query_all_recent_session_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_query_all_recent_session_async(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
            NimSessionQueryRecentSessionCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_delete_recent_session_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_delete_recent_session_async(int to_type, string id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
            NimSessionChangeCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_delete_all_recent_session_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_delete_all_recent_session_async(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
            NimSessionChangeCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_set_unread_count_zero_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_set_unread_count_zero_async(int to_type, string id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension,
            NimSessionChangeCbFunc cb, IntPtr user_data);

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_reset_all_unread_count_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_reset_all_unread_count_async([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            NimSessionChangeCbFunc cb, 
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_set_top", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_set_top(NIMSessionType to_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string id,
            bool top,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            NimSessionChangeCbFunc cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_session_set_extend_data", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_session_set_extend_data(NIMSessionType to_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string data,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            NimSessionChangeCbFunc cb,
            IntPtr user_data);
#endif

        #endregion
    }
}