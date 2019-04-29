using System;
using System.Runtime.InteropServices;
using NimUtility;

namespace NIM.Nos
{
    public enum NIMNosInitConfigResultType
    {
        /// <summary>
        ///  自定义tag数量超过最大数量
        /// </summary>
        kNIMNosInitConfResTypeTagCountOF = 0,
        /// <summary>
        /// 所有tag初始成功
        /// </summary>
        kNIMNosInitConfResTypeSuccess,
        /// <summary>
        /// 部分tag初始化成功，失败的tag及错误码可以解析json_result来取得
        /// </summary>
        kNIMNosInitConfResTypePartSuccessful,
        /// <summary>
        /// 所有tag初始化失败
        /// </summary>
        kNIMNosInitConfResTypeFailure,
    };

    /// <summary>
    ///     下载结果回调
    /// </summary>
    /// <param name="rescode">下载结果，一切正常200</param>
    /// <param name="file_path">下载资源文件本地绝对路径</param>
    /// <param name="call_id">如果下载的是消息中的资源，则为消息所属的会话id，否则为空</param>
    /// <param name="res_id">如果下载的是消息中的资源，则为消息id，否则为空</param>
    /// <param name="json_extension">json扩展数据（备用）</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理！</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DownloadCb(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string file_path,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string call_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string res_id,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    /// <summary>
    ///     下载进度回调
    /// </summary>
    /// <param name="downloaded_size">已下载数据大小</param>
    /// <param name="file_size">文件大小</param>
    /// <param name="json_extension">json扩展数据（备用）</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理！</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DownloadPrgCb(long downloaded_size, long file_size,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    /// <summary>
    ///     上传结果回调
    /// </summary>
    /// <param name="rescode">上传结果，一切正常200</param>
    /// <param name="url">url地址</param>
    /// <param name="json_extension">json扩展数据（备用）</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理！</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UploadCb(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string url,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    /// <summary>
    ///     上传进度回调
    /// </summary>
    /// <param name="uploaded_size">已上传数据大小</param>
    /// <param name="file_size">文件大小</param>
    /// <param name="json_extension">json扩展数据（备用）</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理！</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UploadPrgCb(long uploaded_size, long file_size,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DownloadSpeedCb(long download_speed,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DownloadInfoCb(long actual_download_size, long download_speed,
       [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
       IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UploadSpeedCb(long upload_speed,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void UploadInfoCb(long actual_upload_size, long upload_speed,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
       IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void InitConfigCb(NIMNosInitConfigResultType type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
        IntPtr user_data);

    /// <summary>
    /// nim callback function for safe url to origin url
    /// </summary>
    /// <param name="rescode">返回的错误码，200成功，404，传入的安全链接(短链)不存在，或不是有效的安全链接(短链)</param>
    /// <param name="originUrl">源链(长链)</param>
    /// <param name="userData"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SafeUrlConverterCb(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string originUrl,
        IntPtr userData);

    internal static class NosNativeMethods
    {
        //引用C中的方法（考虑到不同平台下的C接口引用方式差异，如[DllImport("__Internal")]，[DllImport("nimapi")]等） 

        #region NIM C SDK native methods

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_init_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_init_config(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_tags,
            InitConfigCb cb,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_upload2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_upload2([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string local_file,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string tag,
            UploadCb res_cb,
            IntPtr res_user_data,
            UploadPrgCb prg_cb,
            IntPtr prg_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_reg_download_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_reg_download_cb(DownloadCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_download_media", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_download_media(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_msg,
            DownloadCb res_cb, IntPtr res_user_data, DownloadPrgCb prg_cb, IntPtr prg_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_stop_download_media", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_stop_download_media(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_msg);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_upload", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_upload(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string local_file,
            UploadCb res_cb, IntPtr res_user_data, UploadPrgCb prg_cb, IntPtr prg_user_data);



        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_download", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_download(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string nos_url,
            DownloadCb res_cb, IntPtr res_user_data, DownloadPrgCb prg_cb, IntPtr prg_user_data);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_reg_upload_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_reg_upload_cb(UploadCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_set_quick_trans(int quick_trans);
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_upload_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_upload_ex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string local_file,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            UploadCb res_cb, IntPtr res_user_data,
            UploadPrgCb prg_cb, IntPtr prg_user_data,
            UploadSpeedCb speed_cb, IntPtr speed_user_data,
            UploadInfoCb info_cb, IntPtr info_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_upload_ex2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_upload_ex2(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string local_file,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string tag,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
        UploadCb res_cb, IntPtr res_user_data,
        UploadPrgCb prg_cb, IntPtr prg_user_data,
        UploadSpeedCb speed_cb, IntPtr speed_user_data,
        UploadInfoCb info_cb, IntPtr info_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_download_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_download_ex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string nos_url,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            DownloadCb res_cb, IntPtr res_user_data,
            DownloadPrgCb prg_cb, IntPtr prg_user_data,
            DownloadSpeedCb speed_cb, IntPtr speed_user_data,
            DownloadInfoCb info_cb, IntPtr info_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_download_media_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_download_media_ex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_msg,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
             DownloadCb res_cb, IntPtr res_user_data,
            DownloadPrgCb prg_cb, IntPtr prg_user_data,
            DownloadSpeedCb speed_cb, IntPtr speed_user_data,
            DownloadInfoCb info_cb, IntPtr info_user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_stop_upload_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_stop_upload_ex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string task_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_stop_download_ex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_stop_download_ex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string task_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_nos_safeurl_to_originurl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_nos_safeurl_to_originurl(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string url,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string jonsExt,
            SafeUrlConverterCb cb,IntPtr ptr);
#endif

        #endregion
    }
}