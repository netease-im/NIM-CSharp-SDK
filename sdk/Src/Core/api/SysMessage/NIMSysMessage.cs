/** @file NIMSysMessage.cs
  * @brief NIM SDK提供的系统消息相关的定义 
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using Newtonsoft.Json;

namespace NIM.SysMessage
{
    public class NIMSysMessage : NimUtility.NimJsonObject<NIMSysMessage>
    {
        /// <summary>
        ///通知错误码 
        /// </summary>
        [JsonProperty("rescode")]
        public ResponseCode Response { get; set; }

        /// <summary>
        ///通知属性 
        /// </summary>
        [JsonProperty("feature")]
        public NIMMessageFeature Feature { get; set; }

        /// <summary>
        ///总计的通知未读数 
        /// </summary>
        [JsonProperty("unread_count")]
        int TotalUnread { get; set; }

        /// <summary>
        ///通知内容 
        /// </summary>
        [JsonProperty("content")]
        public NIMSysMessageContent Content { get; set; }

    }

    public class NIMSysMessageContent:NimUtility.NimJsonObject<NIMSysMessageContent>
    {
        /// <summary>
        /// 时间戳,选填
        /// </summary>
        [JsonProperty("msg_time")]
        public long Timetag { get; set; }

        /// <summary>
        /// 通知类型,如果值为<c>kNIMSysMsgTypeFriendAdd</c>通过Attach json串中"vt"键获取具体类型<see cref="NIM.Friend.NIMVerifyType "/>
        /// </summary>
        [JsonProperty("msg_type")]
        public NIMSysMsgType MsgType { get; set; }

        /// <summary>
        /// 接收者id,如果是个人,则是对方用户id,如果是群,则是群id,必填
        /// </summary>
        [JsonProperty("to_account")]
        public string ReceiverId { get; set; }

        /// <summary>
        /// 自己id,选填
        /// </summary>
        [JsonProperty("from_account")]
        public string SenderId { get; set; }

        /// <summary>
        /// 附言,按需填写
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 附件,按需填写
        /// </summary>
        [JsonProperty("attach")]
        public string Attachment { get; set; }

        /// <summary>
        /// 服务器消息id（自定义通知消息,必须填0）,发送方不需要填写
        /// </summary>
        [JsonProperty("msg_id")]
        public long Id { get; set; }

        /// <summary>
        /// (选填)自定义通知消息是否存离线:0-不存（只发给在线用户）,1-存（可发给离线用户）
        /// </summary>
        [JsonProperty("custom_save_flag")]
        public NIMMessageSettingStatus SupportOffline { get; set; }

        /// <summary>
        /// (选填)自定义通知消息推送文本，不填则不推送
        /// </summary>
        [JsonProperty("custom_apns_text")]
        public string PushContent { get; set; }

        /// <summary>
        /// 本地定义的系统消息状态,见NIMSysMsgStatus,发送方不需要填写
        /// </summary>
        [JsonProperty("log_status")]
        public NIMSysMsgStatus Status { get; set; }

        /// <summary>
        /// (可选)是否需要推送, 0:不需要,1:需要,默认1
        /// </summary>
        [JsonProperty("push_enable")]
        public NIMMessageSettingStatus NeedPush { get; set; }

        /// <summary>
        /// (可选)推送是否要做消息计数(角标)，0:不需要，1:需要，默认1
        /// </summary>
        [JsonProperty("push_need_badge")]
        public NIMMessageSettingStatus NeedPushCount { get; set; }

        /// <summary>
        /// (可选)推送需要前缀，0：不需要，1：需要，默认0
        /// </summary>
        [JsonProperty("push_prefix")]
        public NIMMessageSettingStatus NeedPushPrefix { get; set; }

        /// <summary>
        /// 本地定义的消息id,发送方必填,建议使用uuid
        /// </summary>
        [JsonProperty("client_msg_id")]
        public string ClientMsgId { get; set; }

        /// <summary>
        /// (可选)第三方自定义的推送属性，必须为可以解析为json的非格式化的字符串，长度2048
        /// </summary>
        [JsonProperty("push_payload")]
        public string CustomPushContent { get; set; }

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

        public string GenerateMsgId()
        {
            return NimUtility.Utilities.GenerateGuid();
        }

        public NIMSysMessageContent()
        {
            NeedPush = NIMMessageSettingStatus.kNIMMessageStatusSetted;
            NeedPushCount = NIMMessageSettingStatus.kNIMMessageStatusSetted;
            NeedPushPrefix = NIMMessageSettingStatus.kNIMMessageStatusNotSet;
            _antiSpamEnabled = 0;
        }
    }

    public class NIMSysMsgQueryResult : NimUtility.NimJsonObject<NIMSysMsgQueryResult>
    {
        [JsonIgnore]
        public int Count { get; set; }

        [JsonProperty("unread_count")]
        public int UnreadCount { get; private set; }

        [JsonProperty("content")]
        public NIMSysMessageContent[] MsgCollection { get;private set; }
    }

    public class NIMSysMsgEventArgs : EventArgs
    {
        public NIMSysMessage Message { get; private set; }

        public NIMSysMsgEventArgs(NIMSysMessage msg)
        {
            Message = msg;
        }
    }
}
