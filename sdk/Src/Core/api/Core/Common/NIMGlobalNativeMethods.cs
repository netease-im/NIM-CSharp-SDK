using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void nim_sdk_exception_cb_func(NIMSDKException exception,
           [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string log,
           IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_sdk_del_cache_file_cb_func(ResponseCode rescode, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_sdk_get_cache_file_info_cb_func([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string info,
        IntPtr user_data);

    class NIMGlobalNativeMethods
    {
        #region NIM C SDK native methods

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void nim_sdk_log_cb_func(int log_level,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string log,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_global_free_str_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_free_str_buf(IntPtr str);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_global_free_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_free_buf(IntPtr data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_global_reg_sdk_log_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_reg_sdk_log_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string jsonExt,
            nim_sdk_log_cb_func cb, IntPtr data);


#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void nim_global_detect_proxy_cb_func(bool network_connect, NIMProxyDetectStep step,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_params,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_global_set_proxy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_set_proxy(NIMProxyType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string host,
            int port,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string user,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string password);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_global_detect_proxy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_detect_proxy(NIMProxyType type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string host, 
            int port,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string user,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string password, 
            nim_global_detect_proxy_cb_func cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_reg_exception_report_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension, 
            nim_sdk_exception_cb_func cb, 
            IntPtr user_data);


        /** @fn void nim_global_get_sdk_cache_file_info_async(const char *login_id, const char *file_type, int64_t end_timestamp, const char *json_extension, nim_sdk_get_cache_file_info_cb_func cb, const void *user_data);
* 获取sdk缓存文件信息
* @param[in] login_id 查询的账号ID
* @param[in] file_type 文件类型，常量定义见nim_global_def.h 查询SDK文件缓存信息文件类型file_type
* @param[in] end_timestamp  查询时间截止点（查询全部填0）
* @param[in] json_extension json扩展参数（备用，目前不需要）
* @param[in] cb nim_sdk_get_cache_file_info_cb_func回调函数定义见nim_global_def.h
* @param[in] user_data APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理！
* @return void 无返回值
*/
        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_get_sdk_cache_file_info_async([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string login_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string file_type, 
            long end_timestamp, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            nim_sdk_get_cache_file_info_cb_func cb, 
            IntPtr user_data);

        /** @fn void nim_global_del_sdk_cache_file_async(const char *login_id, const char *file_type, int64_t end_timestamp, const char *json_extension, nim_sdk_del_cache_file_cb_func cb, const void *user_data);
        * 删除sdk缓存文件
        * @param[in] login_id 查询的账号ID
        * @param[in] file_type 文件类型，常量定义见nim_global_def.h 查询SDK文件缓存信息文件类型file_type
        * @param[in] end_timestamp  删除时间截止点（查询全部填0）
        * @param[in] json_extension json扩展参数（备用，目前不需要）
        * @param[in] cb nim_sdk_del_cache_file_cb_func回调函数定义见nim_global_def.h
        * @param[in] user_data APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理！
        * @return void 无返回值
*/
        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_global_del_sdk_cache_file_async([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string login_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string file_type, 
            long end_timestamp,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension, 
            nim_sdk_del_cache_file_cb_func cb, 
            IntPtr user_data);

#endif
        #endregion
    }
}
