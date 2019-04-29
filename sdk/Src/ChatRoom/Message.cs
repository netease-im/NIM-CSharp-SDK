using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NIMChatRoom
{
    /// <summary>
    /// 聊天室消息类型
    /// </summary>
    public enum NIMChatRoomMsgType
    {
        /// <summary>
        ///文本类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeText = 0,

        /// <summary>
        ///图片类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeImage = 1,

        /// <summary>
        ///声音类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeAudio = 2,

        /// <summary>
        ///视频类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeVideo = 3,

        /// <summary>
        ///位置类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeLocation = 4,

        /// <summary>
        ///活动室通知 
        /// </summary>
        kNIMChatRoomMsgTypeNotification = 5,

        /// <summary>
        ///文件类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeFile = 6,

        /// <summary>
        ///提醒类型消息 
        /// </summary>
        kNIMChatRoomMsgTypeTips = 10,

        /// <summary>
        /// 机器人消息
        /// </summary>
        kNIMChatRoomMsgTypeRobot,

        /// <summary>
        ///自定义消息 
        /// </summary>
        kNIMChatRoomMsgTypeCustom = 100,

        /// <summary>
        ///未知类型消息，作为默认值 
        /// </summary>
        kNIMChatRoomMsgTypeUnknown = 1000
    }

    /// <summary>
    /// 聊天室消息来源端
    /// </summary>
    public enum NIMChatRoomClientType
    {
        /// <summary>
        ///default,unset 
        /// </summary>
        kNIMChatRoomClientTypeDefault = 0,

        /// <summary>
        /// android 
        /// </summary>
        kNIMChatRoomClientTypeAndroid = 1,

        /// <summary>
        /// ios
        /// </summary>
        kNIMChatRoomClientTypeiOS = 2,

        /// <summary>
        /// PC
        /// </summary>
        kNIMChatRoomClientTypePCWindows = 4,

        /// <summary>
        /// WindowsPhone
        /// </summary>
        kNIMChatRoomClientTypeWindowsPhone = 8,

        /// <summary>
        /// web
        /// </summary>
        kNIMChatRoomClientTypeWeb = 16,

        /// <summary>
        /// RestAPI
        /// </summary>
        kNIMChatRoomClientTypeRestAPI = 32,

        /// <summary>
        /// Mac
        /// </summary>
        kNIMChatRoomClientTypeMacOS = 64
    }

    /// <summary>
    /// 聊天室消息
    /// </summary>
    public class Message : NimUtility.NimJsonObject<Message>
    {
        /// <summary>
        /// 消息所属的聊天室id(服务器填充)
        /// </summary>
        [JsonProperty("room_id")]
        public long RoomId { get; set; }

        /// <summary>
        /// 消息发送者的账号(服务器填充) 
        /// </summary>
        [JsonProperty("from_id")]
        public string SenderId { get; set; }

        /// <summary>
        /// 消息发送的时间戳(服务器填充)(毫秒)
        /// </summary>
        [JsonProperty("time")]
        public long TimeStamp { get; set; }

        /// <summary>
        /// 消息发送方客户端类型,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_client_type")]
        public NIMChatRoomClientType SenderClientType { get; set; }

        /// <summary>
        /// 消息发送方昵称
        /// </summary>
        [JsonProperty("from_nick")]
        public string SenderNickName { get; set; }

        /// <summary>
        /// 消息发送方头像,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_avator")]
        public string SenderAvator { get; set; }

        /// <summary>
        /// 消息发送方身份扩展字段,服务器填写,发送方不需要填写
        /// </summary>
        [JsonProperty("from_ext")]
        public string SenderExtension { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("msg_type")]
        public NIMChatRoomMsgType MessageType { get; set; }

        /// <summary>
        /// 消息内容,长度限制2048,如果约定的是json字符串，必须为可以解析为json的非格式化的字符串
        /// </summary>
        [JsonProperty("msg_attach")]
        public string MessageAttachment { get; set; }

        /// <summary>
        /// 客户端消息id
        /// </summary>
        [JsonProperty("client_msg_id")]
        public string ClientMsgId { get; set; }

        /// <summary>
        /// 消息重发标记位
        /// </summary>
        [JsonProperty("resend_flag")]
        public bool NeedResend { get; set; }

        /// <summary>
        /// 第三方扩展字段, 长度限制4096, 必须为可以解析为Json的非格式化的字符串
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

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

		/// <summary>
		/// (可选)该消息是否存储云端历史,可选，0:不是,1:是, 默认1
		/// </summary>
		[JsonProperty(PropertyName = "history_save")]
		public int SaveHistory { get; set; }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY


        /// <summary>
        /// (可选)文本消息内容（聊天室机器人文本消息）
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

#endif

        /// <summary>
        /// 媒体文件本地绝对路径（客户端）
        /// </summary>
        [JsonProperty("local_res_path")]
        public string LocalResourcePath { get; set; }

        /// <summary>
        ///  媒体文件ID（客户端）
        /// </summary>
        [JsonProperty("res_id")]
        public string LocalResourceId { get; set; }

        public Message()
        {
            _antiSpamEnabled = 0;
			SaveHistory = 1;
        }
    }

    internal class QueryMessageHistoryParam : NimUtility.NimJsonObject<QueryMessageHistoryParam>
    {
        /// <summary>
        /// 开始时间,单位毫秒
        /// </summary>
        [JsonProperty("start")]
        public long StartTime { get; set; }

        /// <summary>
        /// 本次返回的消息数量
        /// </summary>
        [JsonProperty("limit")]
        public int Count { get; set; }

        /// <summary>
        /// true:按时间正序起查，正序排列,false:按时间逆序起查，逆序排列
        /// </summary>
		[JsonProperty("reverse")]
		public bool Reverse { get; set;}

        /// <summary>
        /// 要查询的消息类型
        /// </summary>
		[JsonProperty("msgtypes")]
		public List<NIMChatRoomMsgType> MsgTypes { get; set;} 
    }
}
