#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_push_event_cb_func(int res_code,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string event_info_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_batch_push_event_cb_func(int res_code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string event_list_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_publish_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string event_info_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_subscribe_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string faild_list_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_unsubscribe_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string faild_list_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_batch_unsubscribe_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_query_subscribe_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string subscribe_list_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void nim_batch_query_subscribe_event_cb_func(int res_code, int event_type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string subscribe_list_json,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);
    class NIMSubscribeNativeMethods
    {
        
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_subscribe_event_reg_push_event_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_subscribe_event_reg_push_event_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_push_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_subscribe_event_reg_batch_push_event_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_subscribe_event_reg_batch_push_event_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_batch_push_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_publish_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_publish_event(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string event_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_publish_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_subscribe_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_subscribe_event(int event_type, long ttl, int sync_event,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string accid_list_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_subscribe_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_unsubscribe_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_unsubscribe_event(int event_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string accid_list_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_unsubscribe_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_batch_unsubscribe_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_batch_unsubscribe_event(int event_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_batch_unsubscribe_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_query_subscribe_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_query_subscribe_event(int event_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string accid_list_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_query_subscribe_event_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_batch_query_subscribe_event", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_batch_query_subscribe_event(int event_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            nim_batch_query_subscribe_event_cb_func cb,
            IntPtr user_data);
    }
}
#endif