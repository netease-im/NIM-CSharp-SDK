using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NIM
{
    internal static class MessageFactory
    {
        internal static NIMIMMessage CreateMessage(Newtonsoft.Json.Linq.JObject token)
        {
            if (!token.HasValues || token.Type != Newtonsoft.Json.Linq.JTokenType.Object)
                return null;
            var msgTypeToken = token.SelectToken(NIMIMMessage.MessageTypePath);
            if (msgTypeToken == null)
                throw new ArgumentException("message type must be seted:" + token.ToString(Formatting.None));
            var msgType = msgTypeToken.ToObject<NIMMessageType>();
            NIMIMMessage message = null;
            ConvertAttachStringToObject(token);
            var msgJsonValue = token.ToString(Formatting.None);
            switch (msgType)
            {
                case NIMMessageType.kNIMMessageTypeAudio:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMAudioMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeFile:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMFileMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeImage:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMImageMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeLocation:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMLocationMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeText:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMTextMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeVideo:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMVideoMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeNotification:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMTeamNotificationMessage>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeCustom:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMCustomMessage<object>>(msgJsonValue);
                    break;
                case NIMMessageType.kNIMMessageTypeTips:
                    ConvertAttachObjectToString(token);
                    msgJsonValue = token.ToString(Formatting.None);
                    message = NimUtility.Json.JsonParser.Deserialize<NIMTipMessage>(msgJsonValue);
                    break;
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
                case NIMMessageType.kNIMMessageTypeRobot:
                    message = Robot.ResponseMessage.Deserialize(token);
                    break;
#endif
                default:
                    message = NimUtility.Json.JsonParser.Deserialize<NIMUnknownMessage>(msgJsonValue);
                    ((NIMUnknownMessage) message).RawMessage = token.ToString(Formatting.None);
                    break;
            }
            return message;
        }

        internal static NIMIMMessage CreateMessage(string json)
        {
            try
            {
                var token = Newtonsoft.Json.Linq.JObject.Parse(json);
                return CreateMessage(token);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return null;
        }

        static void ConvertAttachStringToObject(Newtonsoft.Json.Linq.JObject token)
        {
            //处理"msg_attach"值，该json原始值是一个string，需要转换为json object
            var attachmentToken = token.SelectToken(NIMIMMessage.AttachmentPath);
            if (attachmentToken == null)
                return;

            if (attachmentToken.Type == Newtonsoft.Json.Linq.JTokenType.String)
            {
                var attachValue = attachmentToken.ToObject<string>();
                if (string.IsNullOrEmpty(attachValue))
                {
                    token.Remove(NIMIMMessage.AttachmentPath);
                    return;
                }
                try
                {
                    var newAttachToken = Newtonsoft.Json.Linq.JToken.Parse(attachValue);
                    attachmentToken.Replace(newAttachToken);
                }
                catch
                {
                    
                }
            }
            
        }

        //处理"msg_attach"值，发送消息的时候需要把json object 转换成string
        internal static void ConvertAttachObjectToString(Newtonsoft.Json.Linq.JToken token)
        {
            var attachmentToken = token.SelectToken(NIMIMMessage.AttachmentPath);
            if (attachmentToken == null)
                return;
            if (attachmentToken.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                var attachValue = attachmentToken.ToString(Formatting.None);
                attachmentToken.Replace(attachValue);
            }
        }
    }
}
