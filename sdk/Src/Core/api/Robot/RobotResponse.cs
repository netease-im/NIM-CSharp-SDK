using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NIM.Robot
{
    /// <summary>
    /// 接收到的机器人消息
    /// </summary>
    public class ResponseMessage : NIMIMMessage
    {
        [JsonProperty(AttachmentPath)]
        public RobotMessageAttach Attach { get; set; }

        public static ResponseMessage Deserialize(JObject msgObj)
        {
            string msgJsonValue = msgObj.ToString(Formatting.None);
            ResponseMessage msg = NimUtility.Json.JsonParser.Deserialize<Robot.ResponseMessage>(msgJsonValue);
            var token = msgObj.SelectToken(AttachmentPath);
            if (token != null)
            {
                token = token.SelectToken("robotMsg");
            }
            if (token != null)
            {
                if (msg.Attach.Response.Type == RobotResponseType.Bot)
                {
                    var res = token.ToString(Formatting.None);
                    msg.Attach.Response = token.ToObject<BotResponse>();
                }
                else if (msg.Attach.Response.Type == RobotResponseType.FAQ)
                {
                    msg.Attach.Response = token.ToObject<FAQResponse>();
                }
            }

            return msg;
        }
    }

    public class RobotMessageAttach
    {
        [JsonProperty("clientMsgId")]
        public string ClientMsgID { get; set; }

        [JsonProperty("robotAccid")]
        public string Accid { get; set; }

        [JsonProperty("msgOut")]
        public bool MsgOut { get; set; }

        [JsonProperty("robotMsg")]
        public ResponseContent Response { get; set; }
    }

    public class ResponseContent
    {
        [JsonProperty("s")]
        public int Status { get; set; }

        [JsonProperty("flag")]
        private string _flag
        {
            get { return Type.ToString(); }
            set
            {
                if (string.Compare(value, "bot", true) == 0)
                    Type = RobotResponseType.Bot;
                else if (string.Compare(value, "faq", true) == 0)
                    Type = RobotResponseType.FAQ;
                else
                    Type = RobotResponseType.Unknown;
            }
        }

        [JsonIgnore]
        public RobotResponseType Type { get; set; }

    }

    /// <summary>
    /// 机器人响应消息类型
    /// </summary>
    public enum RobotResponseType
    {
        Unknown,
        FAQ,
        Bot
    }

    public enum BotResponseType
    {
        Text = 1,
        Image = 2,
        Replay = 3,
        Template = 11
    }

    public class BotMessageItem
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("type")]
        private string _type
        {
            get { return Type.ToString("d2"); }
            set
            {
                var tmp = int.Parse(value);
                Type = (BotResponseType)tmp;
            }
        }

        [JsonIgnore]
        public BotResponseType Type { get; set; }
    }

    /// <summary>
    /// bot 消息
    /// </summary>
    public class BotResponse: ResponseContent
    {
        [JsonProperty("message")]
        public List<BotMessageItem> Items { get; set; }
    }

    public class FAQMessageItem
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("groupId")]
        public string GroupID { get; set; }
    }

    public enum FAQAnswerType
    {
        /// <summary>
        /// 能匹配到问答对库中一个相似度高的问题
        /// </summary>
        OneHighMatch = 1,

        /// <summary>
        /// 能匹配到问答对库中若干个相似度高的问题
        /// </summary>
        SomeHighMatch,

        /// <summary>
        /// 能匹配到问答对库中若干个相似度较低的问题
        /// </summary>
        SomeLowMatch,

        /// <summary>
        /// 不能匹配问题
        /// </summary>
        NoMatch,

        /// <summary>
        /// 能匹配到寒暄库中一个相似度高的问题
        /// </summary>
        Other
    }

    /// <summary>
    /// faq 回复消息
    /// </summary>
    public class FAQMessage
    {
        [JsonProperty("match")]
        public List<FAQMessageItem> Items { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        public FAQAnswerType AnswerType { get; set; }
    }

    public class FAQResponse : ResponseContent
    {
        [JsonProperty("message")]
        public FAQMessage Message { get; set; }
    }
}
