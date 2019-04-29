using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NIM
{
    public class NIMTipMessage : NIMIMMessage
    {
        public NIMTipMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeTips;
        }

        [JsonProperty(MessageBodyPath)]
        public string TextContent { get; set; }

        [JsonProperty(AttachmentPath)]
        public string Attachment { get; set; }
    }
}
