/** @file NIMGlobalDef.cs
  * @brief NIM SDK提供的一些全局定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace NIM
{
    internal class NIMGlobal
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JsonTransportCb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonParams, IntPtr userData);
    }

    delegate void nim_global_net_detect_cb_func(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_params, 
        IntPtr user_data);


    /// <summary>
    /// 代理测试步骤
    /// </summary>
    public enum NIMProxyDetectStep
    {
        /// <summary>
        /// 探测代理有效性结束
        /// </summary>
        kNIMProxyDetectStepAllComplete = 5
    }

    /// <summary>
    ///     代理类型
    /// </summary>
    public enum NIMProxyType
    {
        /// <summary>
        ///     不使用代理
        /// </summary>
        kNIMProxyNone = 0,

        /// <summary>
        ///     HTTP 1.1 Proxy（暂不支持）
        /// </summary>
        kNIMProxyHttp11 = 1,

        /// <summary>
        ///     Socks4
        /// </summary>
        kNIMProxySocks4 = 4,

        /// <summary>
        ///     Socks4a
        /// </summary>
        kNIMProxySocks4a = 5,

        /// <summary>
        ///     Socks5
        /// </summary>
        kNIMProxySocks5 = 6
    }

    /// <summary>
    /// 网络探测错误
    /// </summary>
    public enum NetDetectionRes
    {
        /// <summary>
        ///流程错误 
        /// </summary>
        ProcessError = 0,
        /// <summary>
        ///成功 
        /// </summary>
        Success = 200,
        /// <summary>
        ///非法请求格式 
        /// </summary>
        InvalidRequest = 400,
        /// <summary>
        /// 请求数据不对
        /// </summary>
        DataError = 417,
        /// <summary>
        /// ip为内网ip
        /// </summary>
        InnerIP = 606,
        /// <summary>
        /// 频率超限
        /// </summary>
        OutOfLimit = 607,
        /// <summary>
        ///探测类型错误 
        /// </summary>
        TypeError = 20001,
        /// <summary>
        ///ip错误 
        /// </summary>
        IPError = 20002,
        /// <summary>
        ///sock错误 
        /// </summary>
        SocketError = 20003
    }

    /// <summary>
    /// sdk异常
    /// </summary>
    public enum NIMSDKException
    {
        /// <summary>
        /// 当前数据目录所在盘符空间紧张或用完 log: {"free_space" : %lf, "message":""}, free_space单位M
        /// </summary>
        kNIMSDKExceptionSpaceEmpty = 1,
    }

    /// <summary>
    /// 网络探测结果
    /// </summary>
    public class NetDetectResult : NimUtility.NimJsonObject<NetDetectResult>
    {
        [JsonProperty("loss")]
        public int Loss { get; set; }

        [JsonProperty("rttmax")]
        public int RTTMax { get; set; }

        [JsonProperty("rttmin")]
        public int RTTMin { get; set; }

        [JsonProperty("rttavg")]
        public int RTTAvg { get; set; }

        [JsonProperty("rttmdev")]
        public int RTTMdev { get; set; }

        [JsonProperty("detailinfo")]
        public string Info { get; set; }
    }

    /// <summary>
    /// sdk缓存文件类型
    /// </summary>
    public enum CacheFileType
    {
        /// <summary>
        /// 杂项文件
        /// </summary>
        Misc,
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Audio,
        /// <summary>
        /// 视频
        /// </summary>
        Video
    }

    public class CacheFileInfo:NimUtility.NimJsonObject<CacheFileInfo>
    {
        [JsonProperty("file_type")]
        public string FileType { get; set; }

        /// <summary>
        /// 文件夹路径
        /// </summary>
        [JsonProperty("file_path")]
        public string Path { get; set; }

        /// <summary>
        /// 缓存目录总大小(KB)
        /// </summary>
        [JsonProperty("total_size")]
        public long TotalSize { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        [JsonProperty("file_count")]
        public int FilesCount { get; set; }
    }
}