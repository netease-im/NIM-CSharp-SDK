/** @file NIMGlobalAPI.cs
  * @brief NIM SDK提供的一些全局接口
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Runtime.InteropServices;
using NimUtility;

namespace NIM
{
    
    public delegate void NimWriteLogDelegate(int level, string log);

    public delegate void NimNetworkDetectionDelegate(NetDetectionRes error, NetDetectResult result, IntPtr userData);

    public delegate void NimProxyDetectionDelegate(bool connection, NIMProxyDetectStep step);

    public delegate void DeleteCacheFileDelegate(ResponseCode code);

    public delegate void GetCacheFileInfoDelegate(CacheFileInfo info);

    public class GlobalAPI
    {
        /// <summary>
        ///     释放SDK内部分配的内存
        /// </summary>
        /// <param name="str">由SDK内部分配内存的字符串</param>
        public static void FreeStringBuffer(IntPtr str)
        {
            NIMGlobalNativeMethods.nim_global_free_str_buf(str);
        }

        /// <summary>
        ///     释放SDK内部分配的内存
        /// </summary>
        /// <param name="data">由SDK内部分配的内存</param>
        public static void FreeBuffer(IntPtr data)
        {
            NIMGlobalNativeMethods.nim_global_free_buf(data);
        }

        public static void SetSdkLogCallback(NimWriteLogDelegate cb)
        {
#if !UNITY_ANDROID
            IntPtr ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMGlobalNativeMethods.nim_global_reg_sdk_log_cb(null, NimSdkLogCb, ptr);
#endif
        }

        private static readonly NIMGlobalNativeMethods.nim_sdk_log_cb_func NimSdkLogCb = WriteSdkLog;

        [MonoPInvokeCallback(typeof(NIMGlobalNativeMethods.nim_sdk_log_cb_func))]
        private static void WriteSdkLog(int log_level, string log, IntPtr user_data)
        {
            DelegateConverter.Invoke<NimWriteLogDelegate>(user_data, log_level, log);
        }


#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        ///     设置SDK统一的网络代理。不需要代理时，type设置为kNIMProxyNone，其余参数都传空字符串（端口设为0）。有些代理不需要用户名和密码，相应参数也传空字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public static void SetProxy(NIMProxyType type, string host, int port, string user, string password)
        {
            NIMGlobalNativeMethods.nim_global_set_proxy(type, host, port, user, password);
        }

        static NIMGlobalNativeMethods.nim_global_detect_proxy_cb_func ProxyDetectionCallback = OnProxyDetectionCompleted;

        private static void OnProxyDetectionCompleted(bool network_connect, NIMProxyDetectStep step, string json_params, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<NimProxyDetectionDelegate>(user_data, network_connect, step);
        }

        /// <summary>
        /// 测试代理
        /// </summary>
        /// <param name="type">代理类型</param>
        /// <param name="host">代理地址</param>
        /// <param name="port">代理端口</param>
        /// <param name="user">代理用户名</param>
        /// <param name="password">代理密码</param>
        /// <param name="cb"></param>
        public static void DetectProxy(NIMProxyType type, string host, int port, string user, string password, NimProxyDetectionDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMGlobalNativeMethods.nim_global_detect_proxy(type, host, port, user, password, ProxyDetectionCallback, ptr);
        }


        static nim_sdk_exception_cb_func ExceptionReportCb = ExceptionReport;

        private static void ExceptionReport(NIMSDKException exception, string log, IntPtr user_data)
        {
            DelegateConverter.Invoke<nim_sdk_exception_cb_func>(user_data, exception, log, IntPtr.Zero);
        }

        /// <summary>
        /// 注册输出系统环境异常的回调
        /// </summary>
        /// <param name="cb"></param>
        public static void RegExceptionReportCb(nim_sdk_exception_cb_func cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMGlobalNativeMethods.nim_global_reg_exception_report_cb(null, ExceptionReportCb, ptr);
        }

        static nim_sdk_del_cache_file_cb_func DeleteCacheFileCb = OnDeleteCacheCompleted;

        [MonoPInvokeCallback(typeof(nim_sdk_del_cache_file_cb_func))]
        private static void OnDeleteCacheCompleted(ResponseCode rescode, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<DeleteCacheFileDelegate>(user_data, rescode); 
        }

        /// <summary>
        /// 删除sdk缓存文件
        /// </summary>
        /// <param name="accountID">查询的账号ID</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="endtime">查询时间截止点（查询全部填0）</param>
        /// <param name="cb"></param>
        public static void DeleteCacheFile(string accountID,CacheFileType fileType,long endtime, DeleteCacheFileDelegate cb)
        {
            string fileTypeStr = GetCacheFileTypeDesc(fileType);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMGlobalNativeMethods.nim_global_del_sdk_cache_file_async(accountID, fileTypeStr, endtime, null, DeleteCacheFileCb, ptr);
        }

        static nim_sdk_get_cache_file_info_cb_func GetCacheFileInfoCb = OnGetCacheFileInfoCompleted;

        [MonoPInvokeCallback(typeof(nim_sdk_get_cache_file_info_cb_func))]
        private static void OnGetCacheFileInfoCompleted(string info, IntPtr user_data)
        {
           var cacheInfo = CacheFileInfo.Deserialize(info);
            DelegateConverter.InvokeOnce<GetCacheFileInfoDelegate>(user_data, cacheInfo);
        }

        /// <summary>
        /// 获取sdk缓存文件信息
        /// </summary>
        /// <param name="accountID">查询的账号ID</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="endtime">查询时间截止点（查询全部填0)</param>
        /// <param name="cb"></param>
        public static void GetCacheFileInfo(string accountID,CacheFileType fileType,long endtime, GetCacheFileInfoDelegate cb)
        {
            string fileTypeStr = GetCacheFileTypeDesc(fileType);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMGlobalNativeMethods.nim_global_get_sdk_cache_file_info_async(accountID, fileTypeStr, endtime, null, GetCacheFileInfoCb, ptr);
        }

        private static string GetCacheFileTypeDesc(CacheFileType fileType)
        {
            string fileTypeStr = string.Empty;
            switch (fileType)
            {
                case CacheFileType.Audio:
                    fileTypeStr = "audio";
                    break;
                case CacheFileType.Image:
                    fileTypeStr = "image";
                    break;
                case CacheFileType.Misc:
                    fileTypeStr = "res";
                    break;
                case CacheFileType.Video:
                    fileTypeStr = "video";
                    break;
            }
            return fileTypeStr;
        }
        
#endif

    }
}