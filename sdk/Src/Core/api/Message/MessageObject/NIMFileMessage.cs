using Newtonsoft.Json;

namespace NIM
{
    public class NIMFileMessage : NIMIMMessage
    {
        public NIMFileMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeFile;
        }

        [JsonProperty(AttachmentPath)]
        public NIMMessageAttachment FileAttachment { get; set; }
    }
}