using Newtonsoft.Json;

namespace NIM
{
    public class NIMVideoAttachment : NIMMessageAttachment
    {
        [JsonProperty("dur")]
        public int Duration { get; set; }

        [JsonProperty("w")]
        public int Width { get; set; }

        [JsonProperty("h")]
        public int Height { get; set; }
    }

    public class NIMVideoMessage : NIMIMMessage
    {
        public NIMVideoMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeVideo;
        }

        [JsonProperty(AttachmentPath)]
        public NIMVideoAttachment VideoAttachment { get; set; }
    }
}