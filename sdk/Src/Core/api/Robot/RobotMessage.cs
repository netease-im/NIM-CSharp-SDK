using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NIM.Robot
{
    /// <summary>
    /// 向机器人账号发送的消息
    /// </summary>
    public class RobotMessage : NIMIMMessage
    {
        public RobotMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeRobot;
        }

        [JsonProperty(AttachmentPath)]
        public object Attach { get; set; }

        [JsonProperty(MessageBodyPath)]
        public string TextContent { get; set; }
    }

    public abstract class RobotMessageContent
    {
        [JsonProperty("type")]
        protected abstract string MsgType { get; }

        public string Text { get; set; }
        
    }

    /// <summary>
    /// 欢迎消息
    /// </summary>
    public class WelcomeMessage : RobotMessageContent
    {
        protected override string MsgType
        {
            get
            {
                return "00";
            }
        }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : RobotMessageContent
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        protected override string MsgType
        {
            get
            {
                return "01";
            }
        }
    }

    /// <summary>
    /// 内容跳转消息
    /// </summary>
    public class RedirectionMessage : RobotMessageContent
    {
        /// <summary>
        /// 返回消息类型为BOT时出现的link元素类型block中的params
        /// </summary>
        [JsonProperty("params")]
        public string Params { get; set; }

        /// <summary>
        /// 返回消息类型为BOT时出现的link元素类型block中的target
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        protected override string MsgType
        {
            get
            {
                return "03";
            }
        }
    }
}
