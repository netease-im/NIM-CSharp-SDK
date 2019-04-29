using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM.DocTransition
{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
	/// <summary>
	/// nim callback function for doc trans result
	/// </summary>
	/// <param name="code">200为成功，其他为失败</param>
	/// <param name="info">json扩展数据，如果查询成功返回文档信息</param>
	public delegate void DocTransDelegate(int code, DocTransInfo info);

    public delegate void GetTransListDelegate(int code, DocTransInfoList list);

    public class DocTransApi
    {
        /// <summary>
        /// 注册册文档转换的结果的回调通知（服务器异步转换，客户端需要等待通知才知道转换结果）
        /// </summary>
        /// <param name="cb"></param>
        public static void RegisterNotifyCallback(DocTransDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            DocTransNativeMethods.nim_doctrans_reg_notify_cb(null, GlobalTransCallback, ptr);
        }

        private static nim_doctrans_opt_cb_func GlobalTransCallback = OnTransNotified;

        private static void OnTransNotified(int code, string jsonExt, IntPtr userData)
        {
            var info = DocTransInfo.Deserialize(jsonExt);
            NimUtility.DelegateConverter.Invoke<DocTransDelegate>(userData, code, info);
        }

        private static nim_doctrans_opt_cb_func DefaultTransCallback = OnDocTransitionCompleted;

        private static void OnDocTransitionCompleted(int code, string jsonExt, IntPtr userData)
        {
            DocTransInfo info = null;
            if (!string.IsNullOrEmpty(jsonExt))
                info = DocTransInfo.Deserialize(jsonExt);
            NimUtility.DelegateConverter.InvokeOnce<DocTransDelegate>(userData, code, info);
        }

        /// <summary>
        /// 根据文档id查询文档信息
        /// </summary>
        /// <param name="id">文档id</param>
        /// <param name="cb"></param>
        public static void GetTransitionInfo(string id, DocTransDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            DocTransNativeMethods.nim_doctrans_get_info(id, null, DefaultTransCallback, ptr);
        }

        /// <summary>
        /// 根据文档id查询文档信息
        /// </summary>
        /// <param name="id">查询的起始docId，若为空，表示从头开始查找，按照文档转码的发起时间降序排列</param>
        /// <param name="limit">查询的文档的最大数目，有最大值限制，目前为30</param>
        /// <param name="cb"></param>
        public static void GetTransitionInfoList(string id, int limit, GetTransListDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            DocTransNativeMethods.nim_doctrans_get_info_list(id, limit, null, GetTransListCb, ptr);
        }

        private static nim_doctrans_opt_cb_func GetTransListCb = OnGetTransListCompleted;

        private static void OnGetTransListCompleted(int code, string jsonExt, IntPtr userData)
        {
            var list = DocTransInfoList.Deserialize(jsonExt);
            NimUtility.DelegateConverter.InvokeOnce<GetTransListDelegate>(userData, code, list);
        }

        /// <summary>
        /// 根据文档id删除服务器记录，对于正在转码中的文档，删除后将不会收到转码结果的通知
        /// </summary>
        /// <param name="id">文档id</param>
        /// <param name="cb"></param>
        public static void DeleteTransition(string id, DocTransDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            DocTransNativeMethods.nim_doctrans_del_info(id, null, DefaultTransCallback, ptr);
        }

        /// <summary>
        /// 拼接文档源的下载地址
        /// </summary>
        /// <param name="urlPrefix">文档信息中的url前缀</param>
        /// <param name="fileType">文档源类型</param>
        /// <returns></returns>
        public static string GetSourceFileUrl(string urlPrefix, NIMDocTranscodingFileType fileType)
        {
            var ptr = DocTransNativeMethods.nim_doctrans_get_source_file_url(urlPrefix, fileType);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            string url = marshaler.MarshalNativeToManaged(ptr) as string;
            GlobalAPI.FreeBuffer(ptr);
            return url;
        }

        /// <summary>
        /// 拼接文档图片的下载地址
        /// </summary>
        /// <param name="urlPrefix">文档信息中的url前缀</param>
        /// <param name="imgType">文档转换的图片类型</param>
        /// <param name="quality">需要的图片清晰度</param>
        /// <param name="pageNum">图片页码（从1开始计算）</param>
        /// <returns></returns>
        public static string GetPageUrl(string urlPrefix, NIMDocTranscodingImageType imgType, NIMDocTranscodingQuality quality, int pageNum)
        {
            var ptr = DocTransNativeMethods.nim_doctrans_get_page_url(urlPrefix, imgType, quality, pageNum);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            string url = marshaler.MarshalNativeToManaged(ptr) as string;
            GlobalAPI.FreeBuffer(ptr);
            return url;
        }
    }

    public class DocTransInfo : NimUtility.NimJsonObject<DocTransInfo>
    {
        [JsonProperty("pic_info")]
        public List<PictureInfo> PictureList { get; set; }

        /// <summary>
        /// 标识ID
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// 转码文档名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 转码后的下载地址前缀
        /// </summary>
        [JsonProperty("url_prefix")]
        public string UrlPrefix { get; set; }

        /// <summary>
        /// 转码文档总页数
        /// </summary>
        [JsonProperty("page_num")]
        public int PageNum { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// 转码源文档的文件类型
        /// </summary>
        [JsonProperty("source_type")]
        public NIMDocTranscodingFileType SourceFileType { get; set; }

        /// <summary>
        /// 转码过程状态
        /// </summary>
        [JsonProperty("state")]
        public NIMDocTranscodingState TransitionState { get; set; }

        /// <summary>
        /// 转码目标图片的文件类型
        /// </summary>
        [JsonProperty("pic_type")]
        public NIMDocTranscodingImageType DestImageType { get; set; }

        /// <summary>
        /// 发起文档转码时的附带信息
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [JsonProperty("flag")]
        public NIMDocTranscodingFailFlag Result { get; set; }

        /// <summary>
        /// 文件续传状态
        /// </summary>
        [JsonProperty("upload_status")]
        public NIMDocContinueUploadState UploadStatus { get; set; }

        /// <summary>
        /// 上传文件的路径
        /// </summary>
        [JsonProperty("file_path")]
        public string SourceFilePath { get; set; }
    }

    /// <summary>
    /// 图片信息
    /// </summary>
    public class PictureInfo:NimUtility.NimJsonObject<PictureInfo>
    {
        /// <summary>
        /// 转码图像清晰度
        /// </summary>
        [JsonProperty("quality")]
        public int Quality { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// 图片大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }
    }

    public class DocTransInfoList:NimUtility.NimJsonObject<DocTransInfoList>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("infos")]
        public List<DocTransInfo> TransList { get; set; }
    }
#endif
}
