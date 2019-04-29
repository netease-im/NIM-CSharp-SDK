using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NIM
{
    /// <summary>
    /// 广播消息
    /// </summary>
    public class NIMBroadcastMessage:NimUtility.NimJsonObject<NIMBroadcastMessage>
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty("id")]
        public long MsgID { get; set; }

        /// <summary>
        /// 发送者accid,可能不存在
        /// </summary>
        [JsonProperty("from_accid")]
        public string Sender { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("time")]
        public long Timetag { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
