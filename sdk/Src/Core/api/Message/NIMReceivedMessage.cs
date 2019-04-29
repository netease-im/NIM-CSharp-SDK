/** @file NIMReceivedMessage.cs
  * @brief NIM SDK 收发消息相关的定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using Newtonsoft.Json;
using NimUtility;

namespace NIM
{
    public class NIMReceivedMessage : NimJsonObject<NIMReceivedMessage>
    {
        internal const string MessageContentPath = "content";
        internal const string ResCodePath = "rescode";
        internal const string FeaturePath = "feature";

        [JsonProperty(MessageContentPath)]
        public NIMIMMessage MessageContent { get; set; }

        [JsonProperty(FeaturePath)]
        public NIMMessageFeature Feature { get; set; }

        [JsonProperty(ResCodePath)]
        public ResponseCode ResponseCode { get; set; }

        [JsonIgnore]
        public TeamForecePushMessage TeamPushMsg { get; set; }

        public new static NIMReceivedMessage Deserialize(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;
            var obj = Newtonsoft.Json.Linq.JObject.Parse(content);
            return Deserialize(obj);
        }

        public static NIMReceivedMessage Deserialize(Newtonsoft.Json.Linq.JObject obj)
        {
            NIMReceivedMessage msg = new NIMReceivedMessage();
            var resCode = obj.SelectToken(NIMReceivedMessage.ResCodePath);
            var feature = obj.SelectToken(NIMReceivedMessage.FeaturePath);
            var token = obj.SelectToken(NIMReceivedMessage.MessageContentPath);
            if (resCode != null)
                msg.ResponseCode = resCode.ToObject<ResponseCode>();
            if (feature != null)
                msg.Feature = feature.ToObject<NIMMessageFeature>();

            if (token != null && token.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                var contentObj = token.ToObject<Newtonsoft.Json.Linq.JObject>();
                msg.TeamPushMsg = TeamForecePushMessage.Deserialize(contentObj);
                var realMsg = MessageFactory.CreateMessage(contentObj);
                msg.MessageContent = realMsg;
            }
            return msg;
        }
    }

    public class ReceiveMessageArgs : EventArgs
    {
        public ReceiveMessageArgs(NIMIMMessage msg)
        {
            Message = msg;
        }

        public NIMIMMessage Message { get; set; }
    }

    public class NIMReceiveMessageEventArgs : EventArgs
    {
        public NIMReceiveMessageEventArgs(NIMReceivedMessage msg)
        {
            Message = msg;
        }

        public NIMReceivedMessage Message { get; set; }
    }

    public class MessageAck : NimJsonObject<MessageAck>
    {
        /// <summary>
        ///     会话ID
        /// </summary>
        [JsonProperty("talk_id")]
        public string TalkId { get; set; }

        /// <summary>
        ///     消息ID
        /// </summary>
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        ///     错误码
        /// </summary>
        [JsonProperty("rescode")]
        public ResponseCode Response { get; set; }

        /// <summary>
        ///     客户端反垃圾
        /// </summary>
        [JsonProperty("client_anti_spam")]
        private int _clientAntiSpam { get; set; }

        [JsonIgnore]
        public bool ClientAntiSpam
        {
            get { return _clientAntiSpam > 0; }
        }

    }

    public class MessageArcEventArgs : EventArgs
    {
        public MessageArcEventArgs(MessageAck arc)
        {
            ArcInfo = arc;
        }

        public MessageAck ArcInfo { get; private set; }
    }
}