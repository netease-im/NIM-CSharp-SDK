using Newtonsoft.Json;
using NimUtility;

namespace NIM.Friend
{
    public interface INIMFriendChangedInfo
    {
        [JsonIgnore]
        NIMFriendChangeType ChangedType { get; }
    }

    public class FriendDeletedInfo : NimJsonObject<FriendDeletedInfo>, INIMFriendChangedInfo
    {
        [JsonProperty("accid")]
        public string AccountId { get; set; }

        public NIMFriendChangeType ChangedType
        {
            get { return NIMFriendChangeType.kNIMFriendChangeTypeDel; }
        }
    }

    public class FriendRequestInfo : NimJsonObject<FriendRequestInfo>, INIMFriendChangedInfo
    {
        [JsonProperty("accid")]
        public string AccountId { get; set; }

        [JsonProperty("type")]
        public NIMVerifyType VerifyType { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        public NIMFriendChangeType ChangedType
        {
            get { return NIMFriendChangeType.kNIMFriendChangeTypeRequest; }
        }
    }

    public class FriendListSyncInfo : NimJsonObject<FriendListSyncInfo>, INIMFriendChangedInfo
    {
        [JsonProperty("list")]
        public NIMFriendProfile[] ProfileCollection { get; set; }

        public NIMFriendChangeType ChangedType
        {
            get { return NIMFriendChangeType.kNIMFriendChangeTypeSyncList; }
        }
    }

    public class FriendUpdatedInfo : NimJsonObject<FriendUpdatedInfo>, INIMFriendChangedInfo
    {
        [JsonProperty("info")]
        public NIMFriendProfile Profile { get; set; }

        public NIMFriendChangeType ChangedType
        {
            get { return NIMFriendChangeType.kNIMFriendChangeTypeUpdate; }
        }
    }
}