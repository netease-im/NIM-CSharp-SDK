using Newtonsoft.Json;

namespace NIM
{
    public class NIMAudioAttachment : NIMMessageAttachment
    {
        [JsonProperty("dur")]
        public int Duration { get; set; }
    }

    public class NIMAudioMessage : NIMIMMessage
    {
        public NIMAudioMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeAudio;
        }

        [JsonProperty(AttachmentPath)]
        public NIMAudioAttachment AudioAttachment { get; set; }
    }
}