using System;
using Newtonsoft.Json;

namespace NIM
{
    public class NIMTextMessage : NIMIMMessage
    {
        public NIMTextMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeText;
        }

        [JsonProperty(MessageBodyPath)]
        public string TextContent { get; set; }
    }
}