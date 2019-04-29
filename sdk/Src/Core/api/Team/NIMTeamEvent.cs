/** @file NIMTeamEvent.cs
  * @brief NIM SDK 群事件相关的定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NIM.Team
{
    public class NIMTeamEvent : NimUtility.NimJsonObject<NIMTeamEvent>
    {
        [JsonIgnore]
        public ResponseCode ResponseCode { get; set; }

        [JsonIgnore]
        public string TeamId { get; set; }

        [JsonIgnore]
        public NIMNotificationType NotificationType { get; set; }

        [JsonProperty("ids")]
        public List<string> IdCollection { get; set; }

        [JsonProperty("invalid_ids")]
        public List<string> InvalidIDList { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("team_info")]
        public NIMTeamInfo TeamInfo { get; set; }

        [JsonProperty("team_member")]
        public NIMTeamMemberInfo MemberInfo { get; set; }

        [JsonProperty("name_cards")]
        public List<User.UserNameCard> OperatorNameCards { get; set; }

        [JsonProperty("mute")]
        private int _mute { get; set; }

        [JsonIgnore]
        public bool IsMute
        {
            get { return _mute == 1; }
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        //群消息已读回执查询结果
        [JsonProperty("client_msg_id")]
        public string ClientMsgID { get; set; }

        [JsonProperty("read")]
        public List<string> ReadedMembersList { get; set; }

        [JsonProperty("unread")]
        public List<string> UnreadMembersListP { get; set; }
#endif
    }

    public class NIMTeamEventData : NimUtility.NimJsonObject<NIMTeamEventData>
    {
        /// <summary>
        /// 群通知类型
        /// </summary>
        [JsonProperty("id")]
        public NIMNotificationType NotificationId { get; set; }

        [JsonIgnore]
        public NIMTeamEvent TeamEvent { get; set; }

        [JsonIgnore]
        public List<NIMTeamEvent> TeamEvents { get; set; }

    }

    public class NIMTeamEventDataObject:NimUtility.NimJsonObject<NIMTeamEventDataObject>
    {
        /// <summary>
        /// 解析后的群通知信息
        /// </summary>
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public NIMTeamEvent TeamEvent { get; set; }
    }

    public class NIMTeamEventDataObjects : NimUtility.NimJsonObject<NIMTeamEventDataObjects>
    {
        /// <summary>
        /// 解析后群通知信息列表
        /// </summary>
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public List<NIMTeamEvent> TeamEventInfos { get; set; }
    }

    public class NIMTeamEventArgs : EventArgs
    {
        public NIMTeamEventData Data { get; set; }

        public NIMTeamEventArgs(NIMTeamEventData data)
        {
            Data = data;
        }
    }
}
