using System.Collections.Generic;
using Newtonsoft.Json;
using NimUtility;
using NIM.Team;

namespace NIM
{
    public class NIMTeamNotification : NimJsonObject<NIMTeamNotification>
    {
        [JsonProperty("ids")]
        public List<string> IdCollection { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tinfo")]
        public NIMTeamInfo TeamInfo { get; set; }

        [JsonProperty("team_member")]
        public NIMTeamMemberInfo MemberInfo { get; set; }

        [JsonProperty("duration")]
        public int NetCallDuration { get; set; }

        [JsonProperty("calltype")]
        public int NetCallType { get; set; }

        [JsonProperty("time")]
        public long NetCallTime { get; set; }

    }


    public class NotificationData
    {
        [JsonProperty("data")]
        public NIMTeamNotification Data { get; set; }

        [JsonProperty("id")]
        public NIMNotificationType NotificationId { get; set; }
    }


    public class NIMTeamNotificationMessage : NIMIMMessage
    {
        [JsonProperty(AttachmentPath)]
        public NotificationData NotifyMsgData { get; set; }
    }


}