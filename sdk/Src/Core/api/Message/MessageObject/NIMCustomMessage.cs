using Newtonsoft.Json;

namespace NIM
{
    /// <summary>
    /// 自定义消息基类
    /// </summary>
    /// <typeparam name="T">自定义消息的实际类型</typeparam>
    public class NIMCustomMessage<T> : NIMIMMessage
    {
        public NIMCustomMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeCustom;
        }

        [JsonProperty(AttachmentPath)]
        public virtual T CustomContent { get; set; }

        [JsonProperty(MessageBodyPath)]
        public string Extention { get; set; }
    }
}