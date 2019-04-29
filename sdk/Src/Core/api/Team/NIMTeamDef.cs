/** @file NIMTeamDef.cs
  * @brief NIM SDK team相关的定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using Newtonsoft.Json;

namespace NIM.Team
{
    /// <summary>
    /// 群类型
    /// </summary>
    public enum NIMTeamType
    {
        /// <summary>
        /// 普通群
        /// </summary>
        kNIMTeamTypeNormal = 0,

        /// <summary>
        /// 高级群
        /// </summary>
        kNIMTeamTypeAdvanced = 1
    }

    /// <summary>
    /// 群成员类型
    /// </summary>
    public enum NIMTeamUserType
    {
        /// <summary>
        /// 普通成员
        /// </summary>
        kNIMTeamUserTypeNomal = 0,

        /// <summary>
        /// 创建者
        /// </summary>
        kNIMTeamUserTypeCreator = 1,

        /// <summary>
        /// 管理员
        /// </summary>
        kNIMTeamUserTypeManager = 2,

        /// <summary>
        /// 申请加入用户
        /// </summary>
        kNIMTeamUserTypeApply = 3,

        /// <summary>
        ///本地记录等待正在入群的用户 
        /// </summary>
        kNIMTeamUserTypeLocalWaitAccept = 100
    }

    /// <summary>
    /// 群允许加入类型
    /// </summary>
    public enum NIMTeamJoinMode
    {
        /// <summary>
        /// 不用验证
        /// </summary>
        kNIMTeamJoinModeNoAuth = 0,

        /// <summary>
        /// 需要验证
        /// </summary>
        kNIMTeamJoinModeNeedAuth = 1,

        /// <summary>
        /// 拒绝所有人入群
        /// </summary>
        kNIMTeamJoinModeRejectAll = 2
    }

    /// <summary>
    /// 被邀请人同意方式
    /// </summary>
    public enum NIMTeamBeInviteMode
    {
        /// <summary>
        ///需要同意 
        /// </summary>
        kNIMTeamBeInviteModeNeedAgree = 0,

        /// <summary>
        ///不需要同意 
        /// </summary>
        kNIMTeamBeInviteModeNotNeedAgree = 1
    }

    /// <summary>
    /// 谁可以邀请他人入群
    /// </summary>
    public enum NIMTeamInviteMode
    {
        /// <summary>
        /// 管理员
        /// </summary>
        kNIMTeamInviteModeManager = 0,

        /// <summary>
        ///所有人 
        /// </summary>
        kNIMTeamInviteModeEveryone = 1
    }

    /// <summary>
    /// 谁可以修改群资料
    /// </summary>
    public enum NIMTeamUpdateInfoMode
    {
        /// <summary>
        ///管理员 
        /// </summary>
        kNIMTeamUpdateInfoModeManager = 0,

        /// <summary>
        ///所有人 
        /// </summary>
        kNIMTeamUpdateInfoModeEveryone = 1
    }

    /// <summary>
    /// 谁可以更新群自定义属性
    /// </summary>
    public enum NIMTeamUpdateCustomMode
    {
        /// <summary>
        ///管理员 
        /// </summary>
        kNIMTeamUpdateCustomModeManager = 0,

        /// <summary>
        ///所有人 
        /// </summary>
        kNIMTeamUpdateCustomModeEveryone = 1
    }

    enum NIMTeamBitsConfigMask
    {
        kNIMTeamBitsConfigMaskMuteNotify = 1 << 0
    }

    /// <summary>
    /// 群组禁言类型
    /// </summary>
    public enum NIMTeamMuteType
    {
        /// <summary>
        /// 不禁言
        /// </summary>
        kNIMTeamMuteTypeNone = 0,
        /// <summary>
        /// 普通成员禁言
        /// </summary>
        kNIMTeamMuteTypeNomalMute = 1,  
        /// <summary>
        /// 全部禁言
        /// </summary>
        kNIMTeamMuteTypeAllMute = 3
    }

    /// <summary>
    /// 群组信息
    /// </summary>
    public class NIMTeamInfo : NimUtility.NimJsonObject<NIMTeamInfo>
    {
        /// <summary>
        /// 群id
        /// </summary>
        [JsonProperty("tid")]
        public string TeamId { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 群类型
        /// </summary>
        [JsonProperty("type")]
        public NIMTeamType? TeamType { get; set; }

        /// <summary>
        /// 群创建者
        /// </summary>
        [JsonProperty("creator")]
        public string OwnerId { get; set; }

        /// <summary>
        /// 群等级
        /// </summary>
        [JsonProperty("level")]
        public int? Level { get; set; }

        /// <summary>
        /// 群性质,长度限制：6000字符
        /// </summary>
        [JsonProperty("prop")]
        public string Property { get; set; }

        [JsonProperty("valid")]
        private object IsValid { get; set; }

        /// <summary>
        /// 群成员数量
        /// </summary>
        [JsonProperty("member_count")]
        public int? MembersCount { get; set; }

        /// <summary>
        /// 群列表时间戳(毫秒)
        /// </summary>
        [JsonProperty("list_timetag")]
        public long? MemberListTimetag { get; set; }

        /// <summary>
        /// 群创建时间戳(毫秒)
        /// </summary>
        [JsonProperty("create_timetag")]
        public long? CreatedTimetag { get; set; }

        /// <summary>
        /// 群信息上次更新时间戳(毫秒)
        /// </summary>
        [JsonProperty("update_timetag")]
        public long? UpdatedTimetag { get; set; }

        [JsonProperty("member_valid")]
        private object IsMemberValid { get; set; }

        /// <summary>
        /// 群介绍,长度限制：255字符
        /// </summary>
        [JsonProperty("intro")]
        public string Introduce { get; set; }

        /// <summary>
        /// 群公告,长度限制：5000字符
        /// </summary>
        [JsonProperty("announcement")]
        public string Announcement { get; set; }

        /// <summary>
        /// 入群模式
        /// </summary>
        [JsonProperty("join_mode")]
        public NIMTeamJoinMode? JoinMode { get; set; }

        ///// <summary>
        ///// 群属性配置 
        ///// </summary>
        //[JsonProperty("bits")]
        //private long? ConfigBits { get; set; }

        /// <summary>
        /// 第三方扩展字段（仅负责存储和透传）
        /// </summary>
        [JsonProperty("custom")]
        public string Custom { get; set; }

        /// <summary>
        /// 第三方服务器扩展字段（该配置项只能通过服务器接口设置，对客户端只读）
        /// </summary>
        [JsonProperty("server_custom")]
        public string ServerCustom { get;private set; }

        /// <summary>
        /// 群头像
        /// </summary>
        [JsonProperty("icon")]
        public string TeamIcon { get; set; }

        /// <summary>
        /// 被邀请人同意方式，属性本身只有群主管理员可以修改 
        /// </summary>
        [JsonProperty("be_invite_mode")]
        public NIMTeamBeInviteMode? BeInvitedMode { get; set; }

        /// <summary>
        /// 谁可以邀请他人入群，属性本身只有群主管理员可以修改
        /// </summary>
        [JsonProperty("invite_mode")]
        public NIMTeamInviteMode? InvitedMode { get; set; }

        /// <summary>
        /// 谁可以修改群资料，属性本身只有群主管理员可以修改
        /// </summary>
        [JsonProperty("update_info_mode")]
        public NIMTeamUpdateInfoMode? UpdateMode { get; set; }

        /// <summary>
        /// 谁可以更新群自定义属性，属性本身只有群主管理员可以修改
        /// </summary>
        [JsonProperty("update_custom_mode")]
        public NIMTeamUpdateCustomMode? UpdateCustomMode { get; set; }

        /// <summary>
        /// 群全员禁言标记 0:未禁言，1:禁言
        /// </summary>
        [JsonProperty("mute_all")]
        public int MuteAll { get; set; }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 群禁言类型
        /// </summary>
        [JsonProperty("mute_type")]
        public NIMTeamMuteType? MuteType { get; set; }
#endif


        /// <summary>
        /// 群有效性
        /// </summary>
        [JsonIgnore]
        public bool TeamValid
        {
            get { return IsValid != null && int.Parse(IsValid.ToString()) > 0; }
        }

        [JsonIgnore]
        public bool TeamMemberValid
        {
            get { return IsMemberValid != null && int.Parse(IsMemberValid.ToString()) > 0; }
        }

        public NIMTeamInfo()
        {
            IsValid = 1;
            IsMemberValid = 1;
            TeamType = NIMTeamType.kNIMTeamTypeNormal;
        }
    }

    /// <summary>
    /// 群成员信息
    /// </summary>
    public class NIMTeamMemberInfo : NimUtility.NimJsonObject<NIMTeamMemberInfo>
    {
        [JsonProperty("type")]
        public NIMTeamUserType Type { get; set; }

        [JsonProperty("valid")]
        public bool IsValid { get; set; }

        [JsonProperty("create_timetag")]
        public long CreatedTimetag { get; set; }

        [JsonProperty("update_timetag")]
        public long UpdatedTimetag { get; set; }

        [JsonProperty("tid")]
        public string TeamId { get; set; }

        [JsonProperty("accid")]
        public string AccountId { get; set; }

        [JsonProperty("nick")]
        public string NickName { get; set; }

        [JsonProperty("bits")]
        public long ConfigBits { get; set; }

        [JsonProperty("custom")]
        public string Custom { get; set; }

        [JsonProperty("mute")]
        private int Muted { get; set; }

        [JsonIgnore]
        public bool IsMuted {
            get { return Muted == 1; }
        }

        public void SetMuted(bool flag)
        {
            Muted = flag ? 1 : 0;
        }

        /// <summary>
        /// 消息提醒
        /// </summary>
        [JsonIgnore]
        public bool NotifyNewMessage
        {
            get { return (ConfigBits & (int)NIMTeamBitsConfigMask.kNIMTeamBitsConfigMaskMuteNotify) == 0; }
            set
            {
                if (value)
                    ConfigBits &= (~(int)NIMTeamBitsConfigMask.kNIMTeamBitsConfigMaskMuteNotify);
                else
                    ConfigBits |= (int)NIMTeamBitsConfigMask.kNIMTeamBitsConfigMaskMuteNotify;
            }
        }
    }
}
