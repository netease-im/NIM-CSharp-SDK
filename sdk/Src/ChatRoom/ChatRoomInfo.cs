using Newtonsoft.Json;

namespace NIMChatRoom
{
    /// <summary>
    /// 聊天室信息
    /// </summary>
    public class ChatRoomInfo : NimUtility.NimJsonObject<ChatRoomInfo>
    {
        /// <summary>
        ///聊天室ID 
        /// </summary>
        [JsonProperty("id")]
        public long RoomId { get; set; }

        /// <summary>
        ///聊天室名称 
        /// </summary>
        [JsonProperty("name")]
        public string RoomName { get; set; }

        /// <summary>
        ///聊天室公告 
        /// </summary>
        [JsonProperty("announcement")]
        public string Announcement { get; set; }

        /// <summary>
        ///视频直播流地址 
        /// </summary>
        [JsonProperty("broadcast_url")]
        public string BroadcastUrl { get; set; }

        /// <summary>
        ///聊天室创建者账号 
        /// </summary>
        [JsonProperty("creator_id")]
        public string CreatorId { get; set; }

        /// <summary>
        ///聊天室有效标记, 1:有效,0:无效 
        /// </summary>
        [JsonProperty("valid_flag")]
        public int Valid { get; set; }

        /// <summary>
        ///第三方扩展字段, 必须为可以解析为Json的非格式化的字符串, 长度4k
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

        /// <summary>
        /// 在线人数
        /// </summary>
        [JsonProperty("online_count")]
        public int OnlineMembersCount { get; set; }

        [JsonProperty("mute_all")]
        public int _isMuted { get; set; }

        /// <summary>
        /// 聊天室禁言标志
        /// </summary>
        [JsonIgnore]
        public bool IsMuted
        {
            get { return _isMuted == 1; }
            set { _isMuted = 0; }
        }

        public ChatRoomInfo()
        {
            RoomId = 0;
            Valid = 0;
        }
    }
}
