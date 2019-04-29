using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIM
{
    /// <summary>
    /// 消息撤回通知
    /// </summary>
    public class RecallNotification : NimUtility.NimJsonObject<RecallNotification>
    {
        /// <summary>
        /// 会话类型
        /// </summary>
        [JsonProperty("to_type")]
        public NIM.Session.NIMSessionType SessionType { get; set; }

        /// <summary>
        /// 消息发送方ID
        /// </summary>
        [JsonProperty("from_id")]
        public string SenderId { get; set; }

        /// <summary>
        /// 消息接收方ID
        /// </summary>
        [JsonProperty("to_id")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// 客户端消息ID
        /// </summary>
        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        /// <summary>
        /// 自定义通知文案
        /// </summary>
        [JsonProperty("notify")]
        public string NOtify { get; set; }

        /// <summary>
        /// 撤回操作的时间戳(毫秒)
        /// </summary>
        [JsonProperty("time")]
        public long Timetag { get; set; }

        /// <summary>
        /// 撤回通知种类
        /// </summary>
        [JsonProperty("feature")]
        public NIMMessageFeature Feature { get; set; }

        /// <summary>
        /// 撤回的消息本地是否存在,比如对方离线时发一条消息又撤回,对方上线收到离线撤回通知该tag为false
        /// </summary>
        [JsonProperty("msg_exist")]
        public bool MsgLocalExist { get; set; }

        /// <summary>
        /// 撤回的消息的时间戳(毫秒)
        /// </summary>
        [JsonProperty("msg_time")]
        public long MsgTimetag { get; set; }

        // <summary>
        /// 要撤回消息的发送者昵称
        /// </summary>
        [JsonProperty("from_nick")]
        public string Nickname { get; set; }

        // <summary>
        /// 操作者ID
        /// </summary>
        [JsonProperty("operator_id")]
        public string Operator { get; set; }
    }
}
