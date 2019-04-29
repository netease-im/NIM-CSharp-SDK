namespace NIM
{
    public class NIMUnknownMessage : NIMIMMessage
    {
        public NIMUnknownMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeUnknown;
        }

        public string RawMessage { get; set; }
    }
}