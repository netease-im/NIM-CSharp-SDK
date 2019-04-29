using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NimUtility;
using NimUtility.Json;
using NIM.Messagelog;
using NIM.Session;

namespace NIM
{
    public abstract class NIMIMMessage : NimJsonObject<NIMIMMessage>
    {
        #region 协议定义字段
        /// <summary>
        /// 会话类型
        /// </summary>
        [JsonProperty("to_type")]
        public NIMSessionType SessionType { get; set; }

        /// <summary>
        /// 消息发送方id,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_id")]
        public string SenderID { get; set; }

        /// <summary>
        /// 消息接收方id,给自己发送消息时填写自己id
        /// </summary>
        [JsonProperty("to_accid")]
        public string ReceiverID { get; set; }

        /// <summary>
        /// 消息发送方客户端类型,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_client_type")]
        public NIMClientType SenderClientType { get; set; }

        /// <summary>
        /// 消息发送方设备id,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_device_id")]
        public string SenderDeviceId { get; private set; }

        /// <summary>
        /// 消息发送方昵称,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_nick")]
        public string SenderNickname { get; private set; }

        /// <summary>
        /// 消息时间戳（毫秒）
        /// </summary>
        [JsonProperty(PropertyName = "time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long TimeStamp { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty(MessageTypePath)]
        public NIMMessageType MessageType { get; set; }

        /// <summary>
        /// 消息ID（客户端）
        /// </summary>
        [JsonProperty(ClientMessageId)]
        public string ClientMsgID { get; set; }

        /// <summary>
        /// 服务器端消息id
        /// </summary>
        [JsonProperty("server_msg_id")]
        public long ServerMsgId { get; private set; }

        /// <summary>
        /// 消息重发标记位,第一次发送0,重发1
        /// </summary>
        [JsonProperty("resend_flag")]
        public int ResendFlag { get; set; }

        /// <summary>
        /// (可选)推送是否要做消息计数(角标) 默认 True
        /// </summary>
        [JsonProperty(PropertyName = "push_need_badge")]
        public bool NeedCounting { get; set; }

        /// <summary>
        /// 第三方扩展字段, 格式不限，长度限制1024
        /// </summary>
        [JsonProperty(PropertyName = "server_ext")]
        public string ServerExtension { get; set; }

        /// <summary>
        /// 自定义的推送属性，限制非格式化的json string，长度2048
        /// </summary>
        [JsonProperty(PropertyName = "push_payload")]
        public string PushPayload { get; set; }

        /// <summary>
        /// 自定义推送文案，长度限制200字节
        /// </summary>
        [JsonProperty("push_content")]
        public string PushContent { get; set; }

        /// <summary>
        /// (可选)是否需要推送 默认 True
        /// </summary>
        [JsonProperty(PropertyName = "push_enable")]
        public bool NeedPush { get; set; }

        /// <summary>
        /// (可选)推送是否需要前缀 默认 True
        /// </summary>
        [JsonProperty(PropertyName = "push_prefix")]
        public bool NeedPushNick { get; set; }

		/// <summary>
		/// (可选)该消息是否存储云端历史,可选,默认 True
		/// </summary>
		[JsonProperty(PropertyName = "cloud_history")]
		public bool ServerSaveHistory { get; set; }

		/// <summary>
		/// (可选)该消息是否支持漫游,可选, 默认 True
		/// </summary>
		[JsonProperty(PropertyName = "roam_msg")]
		public bool Roaming { get; set; }

		/// <summary>
		/// (可选)该消息是否支持发送者多端同步,可选, 默认 True 
		/// </summary>
		[JsonProperty(PropertyName = "sync_msg")]
		public bool MultiSync { get; set; }

        /// <summary>
        /// (可选)该消息是否抄送,0:不支持,1:支持,默认按照app的路由开关
        /// </summary>
        [JsonProperty(PropertyName = "routable_msg")]
        public bool Routable { get; set; }

        /// <summary>
        /// (可选)消息是否要存离线,0:不需要, 1:需要，默认1
        /// </summary>
        [JsonProperty(PropertyName = "offline_msg")]
        public bool SavedOffline { get; set; }

        [JsonProperty(PropertyName = "anti_spam_enable")]
        private int _antiSpamEnabled { get; set; }

        /// <summary>
        /// 是否需要过易盾反垃圾,默认false
        /// </summary>
        [JsonIgnore]
        public bool AntiSpamEnabled
        {
            get { return _antiSpamEnabled == 1; }
            set { _antiSpamEnabled = value ? 1 : 0; }
        }

        /// <summary>
        /// (可选)开发者自定义的反垃圾字段,长度限制：5000字符, 格式为json string,{"type" : 1:文本，2：图片，3视频, "data" : "文本内容or图片地址or视频地址"}
        /// </summary>
        [JsonProperty(PropertyName = "anti_spam_content")]
        public string AntiSpamContent { get; set; }


        [JsonProperty(PropertyName = "client_anti_spam_hitting")]
        private int? _clientAntiSpamHitting { get; set; }

        /// <summary>
        /// 是否命中客户端反垃圾
        /// </summary>
        [JsonIgnore]
        public bool ClientAntiSpamHitting
        {
            get
            {
                if (_clientAntiSpamHitting == null)
                    return false;
                return _clientAntiSpamHitting.Value > 0;
            }
            set
            {
                if (value)
                    _clientAntiSpamHitting = 1;
                else
                    _clientAntiSpamHitting = null;
            }
        }
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// (可选)用户配置的对某些单条消息另外的反垃圾的业务ID
        /// </summary>
        [JsonProperty(PropertyName = "anti_spam_business_id")]
        public string AntiSpamBizID { get; set; }

        /// <summary>
        /// (可选) 单条消息是否使用易盾反垃圾 0:(在开通易盾的情况下)不过易盾反垃圾而是通用反垃圾 其他都是按照原来的规则
        /// </summary>
        [JsonProperty(PropertyName = "anti_spam_using_yidun")]
        public int AntiSpamUsingYiDun { get; set; }

        /// <summary>
        /// 群消息是否需要已读业务，0：不需要，1：需要
        /// </summary>
        [JsonProperty("team_msg_ack")]
        public int NeedTeamAck { get; set; }

#endif
        #endregion

        #region 客户端定义字段

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 是否已经发送群消息已读回执
        /// </summary>
        [JsonProperty("team_msg_ack_sent")]
        public bool TeamMsgAckSent { get; set; }

        /// <summary>
        /// 群消息未读数
        /// </summary>
        [JsonProperty("team_msg_unread_count")]
        public int TeamMsgUnreadCount { get; set; }
#endif

        /// <summary>
        /// 多媒体消息资源本地绝对路径,SDK本地维护,发送多媒体消息时必填
        /// </summary>
        [JsonProperty("local_res_path")]
        public string LocalFilePath { get; set; }

        /// <summary>
        /// 会话id,发送方选填,接收方收到的是消息发送方id
        /// </summary>
        [JsonProperty("talk_id")]
        public string TalkID { get; set; }

        /// <summary>
        /// 多媒体资源id,发送方选填,接收方收到的是客户端消息id
        /// </summary>
        [JsonProperty("res_id")]
        public string ResourceId { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        [JsonProperty("log_status")]
        public NIMMsgLogStatus MsgLogStatus { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        [JsonProperty("log_sub_status")]
        public NIMMsgLogSubStatus MsgLogSubStatus { get; set; }

        /// <summary>
        /// 本地扩展内容
        /// </summary>
        [JsonProperty("local_ext")]
        public string LocalExtension { get; set; }

        #endregion

        protected NIMIMMessage()
        {
            NeedCounting = true;
            NeedPushNick = true;
            NeedPush = true;
            SavedOffline = true;
            Routable = true;
            ServerSaveHistory = true;
            Roaming = true;
            MultiSync = true;
            _antiSpamEnabled = 0;
        }

        public override string Serialize()
        {
            if (string.IsNullOrEmpty(ClientMsgID))
                ClientMsgID = Utilities.GenerateGuid();
            string jsonValue = base.Serialize();
            var rootObj = JObject.Parse(jsonValue);
            MessageFactory.ConvertAttachObjectToString(rootObj.Root);
            var newValue = rootObj.ToString(Formatting.None, new JsonBoolConverter());
            return newValue;
        }

        internal const string MessageTypePath = "msg_type";
        internal const string AttachmentPath = "msg_attach";
        internal const string MessageBodyPath = "msg_body";
        internal const string ClientMessageId = "client_msg_id";
    }

    /// <summary>
    /// 群强推消息
    /// </summary>
    public class TeamForecePushMessage
    {
        public List<string> ReceiverList { get; set; }
        public string Content { get; set; }

        public bool IsForcePush { get; private set; }

        public const string PushListKey = "force_push_list";
        public const string PushContentKey = "force_push_content";
        public const string ForcePushEnabledKey = "is_force_push";

        public string Serialize(NIMIMMessage nimMsg)
        {
            var nimMsgJson = nimMsg.Serialize();
            var dic = JsonParser.FromJson(nimMsgJson);
            if (ReceiverList == null)
                dic[PushListKey] = string.Empty;
            else
            {
                dic[PushListKey] = ReceiverList;
                dic[PushListKey] = JsonParser.Serialize(dic[PushListKey]);
            }
            if (!string.IsNullOrEmpty(Content))
                dic[PushContentKey] = Content;
            else
                dic[PushContentKey] = string.Empty;
            
            dic[ForcePushEnabledKey] = 1;
            IsForcePush = true;
            var json = JsonParser.Serialize(dic);
            return json;
        }

        public static TeamForecePushMessage Deserialize(JObject obj)
        {
            TeamForecePushMessage result = null;

            JToken token = obj.SelectToken(ForcePushEnabledKey);
            if(token != null)
            {
                result = new TeamForecePushMessage();
                result.IsForcePush = token.ToObject<int>() == 0 ? false : true;
                token = obj.SelectToken(PushContentKey);
                result.Content = token == null ? null : token.ToObject<string>();
                token = obj.SelectToken(PushListKey);
                if(token != null)
                {
                    var list = token.ToObject<string>();
                    result.ReceiverList = JsonParser.Deserialize<List<string>>(list);
                }
                
            }
            return result;
        }
    }

    /// <summary>
    /// 消息属性设置
    /// </summary>
    public class NIMMessageSetting : NimJsonObject<NIMMessageSetting>
    {
        /// <summary>
        /// 该消息是否存储云端历史
        /// </summary>
        [JsonProperty(PropertyName = "cloud_history")]
        public bool ServerSaveHistory { get; set; }

        /// <summary>
        /// 该消息是否支持漫游
        /// </summary>
        [JsonProperty(PropertyName = "roam_msg")]
        public bool Roaming { get; set; }

        /// <summary>
        /// 该消息是否支持发送者多端同步
        /// </summary>
        [JsonProperty(PropertyName = "sync_msg")]
        public bool MultiSync { get; set; }

        /// <summary>
        /// 消息是否要存离线
        /// </summary>
        [JsonProperty(PropertyName = "offline_msg")]
        public bool OfflineStorage { get; set; }

        /// <summary>
        /// 是否支持抄送
        /// </summary>
        [JsonProperty(PropertyName = "routable_msg")]
        public bool Routable { get; set; }

        /// <summary>
        /// 是否需要推送
        /// </summary>
        [JsonProperty(PropertyName = "push_enable")]
        public bool NeedPush { get; set; }

        /// <summary>
        /// 是否要做消息计数
        /// </summary>
        [JsonProperty(PropertyName = "push_need_badge")]
        public bool NeedCounting { get; set; }

        /// <summary>
        /// 需要推送前缀
        /// </summary>
        [JsonProperty(PropertyName = "push_prefix")]
        public bool NeedPushPrefix { get; set; }

        /// <summary>
        /// 自定义推送文案，长度限制200字节
        /// </summary>
        [JsonProperty("push_content")]
        public string PushContent { get; set; }

        /// <summary>
        /// 自定义的推送属性，限制非格式化的json string，长度2048
        /// </summary>
        [JsonProperty(PropertyName = "push_payload")]
        public string PushPayload { get; set; }

        [JsonProperty(PropertyName = TeamForecePushMessage.ForcePushEnabledKey)]
        private int _teamForcePush { get; set; }

        public bool TeamForcePush {
            get { return _teamForcePush == 1; }
            set { _teamForcePush = value ? 1 : 0; }
        }

        [JsonProperty(PropertyName = "anti_spam_enable")]
        private int _antiSpamEnabled { get; set; }

        /// <summary>
        /// 是否需要过易盾反垃圾,默认false
        /// </summary>
        [JsonIgnore]
        public bool AntiSpamEnabled
        {
            get { return _antiSpamEnabled == 1; }
            set { _antiSpamEnabled = value ? 1 : 0; }
        }

        /// <summary>
        /// (可选)开发者自定义的反垃圾字段,长度限制：5000字符 
        /// </summary>
        [JsonProperty(PropertyName = "anti_spam_content")]
        public string AntiSpamContent { get; set; }

        public NIMMessageSetting()
        {
            ServerSaveHistory = true;
            Roaming = true;
            MultiSync = true;
            OfflineStorage = true;
            Routable = true;
            NeedPush = true;
            NeedCounting = true;
            NeedPushPrefix = true;
            _antiSpamEnabled = 0;
        }

    }
}
