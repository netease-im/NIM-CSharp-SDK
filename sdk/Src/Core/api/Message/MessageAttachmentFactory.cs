using Newtonsoft.Json.Linq;

namespace NIM.Message
{
    internal static class MessageAttachmentFactory
    {
        public static NIMMessageAttachment CreateAttachment(NIMMessageType type, string attachmentJson)
        {
            NIMMessageAttachment attach = null;
            try
            {
                var jObj = JObject.Parse(attachmentJson);
                switch (type)
                {
                    case NIMMessageType.kNIMMessageTypeFile:
                        attach = jObj.ToObject<NIMMessageAttachment>();
                        break;
                    case NIMMessageType.kNIMMessageTypeImage:
                        attach = jObj.ToObject<NIMImageAttachment>();
                        break;
                    case NIMMessageType.kNIMMessageTypeAudio:
                        attach = jObj.ToObject<NIMAudioAttachment>();
                        break;
                    case NIMMessageType.kNIMMessageTypeVideo:
                        attach = jObj.ToObject<NIMVideoAttachment>();
                        break;
                }
                return attach;
            }
            catch
            {
                return null;
            }
        }
    }
}