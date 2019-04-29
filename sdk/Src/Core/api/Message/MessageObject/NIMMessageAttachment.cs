using Newtonsoft.Json;
using NimUtility;

namespace NIM
{
    public class NIMMessageAttachment : NimJsonObject<NIMMessageAttachment>
    {
        /// <summary>
        ///     文件内容MD5
        /// </summary>
        [JsonProperty("md5")]
        public string MD5 { get; set; }

        /// <summary>
        ///     文件大小
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        ///     上传云端后得到的文件下载地址
        /// </summary>
        [JsonProperty("url")]
        public string RemoteUrl { get; set; }

        /// <summary>
        ///     用于显示的文件名称
        /// </summary>
        [JsonProperty("name")]
        public string DisplayName { get; set; }

        /// <summary>
        ///     文件扩展名
        /// </summary>
        [JsonProperty("ext")]
        public string FileExtension { get; set; }

        [JsonProperty("res_id")]
        public string LocalResID { get; set; }

        /// <summary>
        /// (可选)发送含有附件的消息时使用的场景标签,Audio Image Video File 或者可以被SDK解析到本地文件路径的自定义消息
        /// </summary>
        [JsonProperty("upload_tag")]
        public string UploadTag { get; set; }
    }
}