using Newtonsoft.Json;

namespace NIM
{
    public class NIMLocationMsgInfo
    {
        [JsonProperty("title")]
        public string Description { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class NIMLocationMessage : NIMIMMessage
    {
        public NIMLocationMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeLocation;
        }

        [JsonProperty(AttachmentPath)]
        public NIMLocationMsgInfo LocationInfo { get; set; }
    }
}