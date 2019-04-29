using Newtonsoft.Json;

namespace NIMChatRoom
{
    /// <summary>
    /// 聊天室通知类型
    /// </summary>
    public enum NIMChatRoomNotificationId
    {
        /// <summary>
        ///成员进入聊天室 
        /// </summary>
        kNIMChatRoomNotificationIdMemberIn = 301,

        /// <summary>
        ///成员离开聊天室 
        /// </summary>
        kNIMChatRoomNotificationIdMemberExit = 302,

        /// <summary>
        ///成员被加黑 
        /// </summary>
        kNIMChatRoomNotificationIdAddBlack = 303,

        /// <summary>
        ///成员被取消黑名单 
        /// </summary>
        kNIMChatRoomNotificationIdRemoveBlack = 304,

        /// <summary>
        ///成员被设置禁言 
        /// </summary>
        kNIMChatRoomNotificationIdAddMute = 305,

        /// <summary>
        ///成员被取消禁言 
        /// </summary>
        kNIMChatRoomNotificationIdRemoveMute = 306,

        /// <summary>
        ///设置为管理员 
        /// </summary>
        kNIMChatRoomNotificationIdAddManager = 307,

        /// <summary>
        ///取消管理员 
        /// </summary>
        kNIMChatRoomNotificationIdRemoveManager = 308,

        /// <summary>
        ///成员设定为固定成员 
        /// </summary>
        kNIMChatRoomNotificationIdAddFixed = 309,

        /// <summary>
        ///成员取消固定成员 
        /// </summary>
        kNIMChatRoomNotificationIdRemoveFixed = 310,

        /// <summary>
        ///聊天室被关闭了 
        /// </summary>
        kNIMChatRoomNotificationIdClosed = 311,

        /// <summary>
        ///聊天室信息被更新了 
        /// </summary>
        kNIMChatRoomNotificationIdInfoUpdated = 312,

        /// <summary>
        ///成员被踢了 
        /// </summary>
        kNIMChatRoomNotificationIdMemberKicked = 313,

        /// <summary>
        /// 临时禁言 
        /// </summary>
        kNIMChatRoomNotificationIdMemberTempMute = 314,

        /// <summary>
        /// 主动解除临时禁言 
        /// </summary>
        kNIMChatRoomNotificationIdMemberTempUnMute = 315,

        /// <summary>
        /// 成员主动更新了聊天室内的角色信息(仅指nick/avator/ext)
        /// </summary>
        kNIMChatRoomNotificationIdMyRoleUpdated = 316,

        /// <summary>
        ///聊天室被禁言了,只有管理员可以发言,其他人都处于禁言状态 
        /// </summary>
        kNIMChatRoomNotificationIdRoomMuted = 318,

        /// <summary>
        ///聊天室解除全体禁言状态 
        /// </summary>
        kNIMChatRoomNotificationIdRoomDeMuted = 319
    }

    public class Notification : NimUtility.NimJsonObject<Notification>
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        [JsonProperty("id")]
        public NIMChatRoomNotificationId Type { get; set; }

        [JsonProperty("data")]
        public Data InnerData { get; set; }

        public class Data
        {
            /// <summary>
            /// 上层开发自定义的事件通知扩展字段, 必须为可以解析为Json的非格式化的字符串
            /// </summary>
            [JsonProperty("ext")]
            public string Extension { get; set; }

            /// <summary>
            /// 操作者的账号id
            /// </summary>
            [JsonProperty("operator")]
            public string OperatorId { get; set; }

            /// <summary>
            /// 操作者的账号nick
            /// </summary>
            [JsonProperty("opeNick")]
            public string OperatorNick { get; set; }

            /// <summary>
            /// 被操作者的账号nick列表
            /// </summary>
            [JsonProperty("tarNick")]
            public string[] TargetAccountsNick { get; set; }

            /// <summary>
            /// 被操作者的accid列表
            /// </summary>
            [JsonProperty("target")]
            public string[] TargetIdCollection { get; set; }

            /// <summary>
            /// 当通知为临时禁言相关时有该值，禁言时kNIMChatRoomNotificationIdMemberTempMute代表本次禁言的时长(秒)，
            /// 解禁时kNIMChatRoomNotificationIdMemberTempUnMute代表本次禁言剩余时长(秒)
            /// </summary>
            [JsonProperty("muteDuration")]
            public long MuteDuration { get; set; }

            /// <summary>
            /// 当通知为kNIMChatRoomNotificationIdMemberIn才有，代表是否禁言状态，1:是 缺省或0:不是
            /// </summary>
            [JsonProperty("muted")]
            public int Muted { get; set; }

            /// <summary>
            /// 当通知为kNIMChatRoomNotificationIdMemberIn才有，代表是否临时禁言状态，1:是 缺省或0:不是
            /// </summary>
            [JsonProperty("tempMuted")]
            public int TempMuted { get; set; }

            /// <summary>
            /// 当通知为kNIMChatRoomNotificationIdMemberIn，代表临时禁言时长(秒)， 其他通知事件不带该数据
            /// </summary>
            [JsonProperty("muteTtl")]
            public long TempMutedDuration { get; set; }
        }
    }
}
