using Newtonsoft.Json;

namespace NIM
{
    public class NIMImageAttachment : NIMMessageAttachment
    {
        /// <summary>
        ///     图片宽度
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; set; }

        /// <summary>
        ///     图片高度
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; set; }
    }

    public class NIMImageMessage : NIMIMMessage
    {
        public NIMImageMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeImage;
        }

        [JsonProperty(AttachmentPath)]
        public NIMImageAttachment ImageAttachment { get; set; }
    }
}