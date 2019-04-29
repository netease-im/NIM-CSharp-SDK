using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM.DocTransition
{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_doctrans_opt_cb_func(int code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string jsonExt, 
        IntPtr userData);

    class DocTransNativeMethods
    {
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_reg_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_doctrans_reg_notify_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string jsonExt, 
            nim_doctrans_opt_cb_func cb, 
            IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_get_info", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_doctrans_get_info(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string jsonExt, 
            nim_doctrans_opt_cb_func cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_get_info_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_doctrans_get_info_list(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string id, 
            int limit,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string jsonExt, 
            nim_doctrans_opt_cb_func cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_del_info", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_doctrans_del_info(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string jsonExt, 
            nim_doctrans_opt_cb_func cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_get_source_file_url", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr nim_doctrans_get_source_file_url(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string urlPrefix, 
            NIMDocTranscodingFileType fileType);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_doctrans_get_page_url", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr nim_doctrans_get_page_url(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string urlPrefix, 
            NIMDocTranscodingImageType img_type, NIMDocTranscodingQuality quality, int page_num);
    }
#endif
}
