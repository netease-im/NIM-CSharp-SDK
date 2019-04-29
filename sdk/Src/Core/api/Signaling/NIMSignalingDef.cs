using NIM.Signaling.Native;
using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM.Signaling
{
    /// <summary>
    /// 频道类型
    /// </summary>
  public enum NIMSignalingType
    {
        /// <summary>
        /// 音频类型
        /// </summary>
        kNIMSignalingTypeAudio = 1,
        /// <summary>
        /// 视频类型
        /// </summary>
        kNIMSignalingTypeVideo = 2,
        /// <summary>
        /// 自定义
        /// </summary>
        kNIMSignalingTypeCustom = 3,
    }

    /// <summary>
    /// 创建信令接口的返回码
    /// </summary>
    public enum NIMSignalingCreateResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道已存在
        /// </summary>
        kChannelAlreadyExist = 10405,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown=20000
    }

    /// <summary>
    /// 关闭或者离开接口的返回码
    /// </summary>
    public enum NIMSignalingCloseOrLeaveResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 不在频道内
        /// </summary>
        kUserNotInChannel=10406,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 加入接口的返回码
    /// </summary>
    public enum NIMSignalingJoinResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 已经在频道内
        /// </summary>
        kUserInChannel = 10407,
        /// <summary>
        /// uid冲突
        /// </summary>
        kUidConflict = 10417,
        /// <summary>
        /// 频道人数超限
        /// </summary>
        kUserNumberLimited = 10419,
        /// <summary>
        /// 已经在频道内（自己的其他端）
        /// </summary>
        kInChannelByOtherDevice = 10420,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 呼叫接口的返回码
    /// </summary>
    public enum NIMSignalingCallResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道已存在
        /// </summary>
        kChannelAlreadyExist = 10405,
        /// <summary>
        /// 对方云信不在线，如果打开存离线开关，可认为发送成功，对方可收到离线消息
        /// </summary>
        kPeerOffline = 10201,
        /// <summary>
        /// 对方推送亦不可达，如果打开存离线开关，可认为发送成功，对方可收到离线消息;
        /// </summary>
        kPeerPushNotReach = 10202,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 邀请信令接口的返回码
    /// </summary>
    public enum NIMSignalingInviteResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 不在频道内（自己）
        /// </summary>
        kUserNotInChannel = 10406,
        /// <summary>
        /// 已经在频道内（对方）
        /// </summary>
        kPeerAlreadyInChannel = 10407,
        /// <summary>
        /// 频道人数超限
        /// </summary>
        kUserNumberLimited = 10419,
        /// <summary>
        /// 对方云信不在线，如果打开存离线开关，可认为发送成功，对方可收到离线消息
        /// </summary>
        kPeerOffline = 10201,
        /// <summary>
        /// 对方推送亦不可达，如果打开存离线开关，可认为发送成功，对方可收到离线消息;
        /// </summary>
        kPeerPushNotReach = 10202,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 取消邀请接口的返回码
    /// </summary>
    public enum NIMSignalingCancelInviteResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 邀请不存在或已过期
        /// </summary>
        kInviteNotExistOrExpire = 10408,
        /// <summary>
        /// 邀请已经拒绝
        /// </summary>
        kInviteAlreadyReject = 10409,
        /// <summary>
        /// 邀请已经接受了
        /// </summary>
        kInviteAlreadyAccept = 10410,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 拒绝或者接受邀请接口的返回码
    /// </summary>
    public enum NIMSignalingRejectOrAcceptResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 邀请不存在或已过期
        /// </summary>
        kInviteNotExistOrExpire = 10408,
        /// <summary>
        /// 邀请已经拒绝
        /// </summary>
        kInviteAlreadyReject = 10409,
        /// <summary>
        /// 邀请已经接受了
        /// </summary>
        kInviteAlreadyAccept = 10410,
        /// <summary>
        /// 对方云信不在线
        /// </summary>
        kPeerOffline = 10201,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 用户自定义控制指令接口的返回码
    /// </summary>
    public enum NIMSignalingControlResCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        kSucess = 200,
        /// <summary>
        /// 接口服务异常
        /// </summary>
        kAbnormal = 0,
        /// <summary>
        /// 超时
        /// </summary>
        kTimeout = 408,
        /// <summary>
        /// 频道不存在
        /// </summary>
        kChannelNotExist = 10404,
        /// <summary>
        /// 不在频道内
        /// </summary>
        kUserNotInChannel = 10406,
        /// <summary>
        /// 对方云信不在线
        /// </summary>
        kPeerOffline = 10201,
        /// <summary>
        /// 未知错误
        /// </summary>
        kUnknown = 20000
    }

    /// <summary>
    /// 频道属性
    /// </summary>
    public class NIMSignalingChannelInfo
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名
        /// </summary>
        public string channel_name_;
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        ///<summary>
        ///创建时传入的扩展字段
        ///</summary>  
        public   string channel_ext_;
        /// <summary>
        /// 创建时间点
        /// </summary>
        public long create_timestamp_;
        /// <summary>
        /// 失效时间点
        /// </summary>
        public long expire_timestamp_;
        /// <summary>
        /// 创建者的accid 
        /// </summary>
        public string creator_id_;
        /// <summary>
        /// 频道是否有效
        /// </summary>
        public bool invalid_;

        public override string ToString()
        {
            string info = string.Format("channel_info-channel_type:"+
                "{0},channel_name:{1},channel_id:{2},channel_ext{3},"+
                "create_timestamp:{4},expire_timestamp:{5},creator_id_:{6},invalid_:{7}",
                channel_type_,
                channel_name_,
                channel_id_,
                channel_ext_,
                create_timestamp_, 
                expire_timestamp_,
                creator_id_, 
                invalid_);
            return info;
        }

    }


    /// <summary>
    /// 成员属性
    /// </summary>
    public class NIMSignalingMemberInfo
    {
        /// <summary>
        /// 成员的 accid
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 成员的 uid，大于零有效，无效时服务器会分配随机频道内唯一的uid
        /// </summary>
        public long uid_;
        /// <summary>
        /// 加入时间点
        /// </summary>
        public ulong create_timestamp_;
        /// <summary>
        /// 失效时间点，失效后认为离开频道
        /// </summary>
        public ulong expire_timestamp_;

        public override string ToString()
        {
            string info = string.Format("member_info-account_id:{0},uid:{1},create_timestamp:{2},expire_timestamp{3}", 
                account_id_, 
                uid_, 
                create_timestamp_,
                expire_timestamp_);
            return info;
        }
    }



    /// <summary>
    /// 频道的详细信息，包含频道信息及成员列表
    /// </summary>
    public class NIMSignalingChannelDetailedinfo
    {
        /// <summary>
        /// 频道信息
        /// </summary>
        public NIMSignalingChannelInfo channel_info_;
        /// <summary>
        /// 频道内成员信息
        /// </summary>
        public List<NIMSignalingMemberInfo> members_;

        public NIMSignalingChannelDetailedinfo()
        {
            members_ = new List<NIMSignalingMemberInfo>();
        }
    }



    /// <summary>
    /// 推送属性
    /// </summary>
    public class NIMSignalingPushInfo
    {
        /// <summary>
        /// 是否需要推送，默认false
        /// </summary>
        public bool need_push_;
        /// <summary>
        /// 推送标题
        /// </summary>
        public string push_title_;
        /// <summary>
        /// 推送内容
        /// </summary>
        public string push_content_;
        /// <summary>
        /// 推送自定义字段
        /// </summary>
        public string push_payload_;
        /// <summary>
        /// 是否计入未读计数,默认false
        /// </summary>
        public bool need_badge_;

        public override string ToString()
        {
            string info = string.Format("nedd_push:{0},push_title:{1},push_content:{2},push_payload:{3},need_badeg:{4}", 
                need_push_,
                push_title_, 
                push_content_,
                push_payload_,
                need_badge_);
            return info;
        }
    }



    /// <summary>
    /// 频道事件，包含在线，同步，离线等
    /// </summary>
    public enum NIMSignalingEventType
    {
        /// <summary>
        /// 返回NIMSignalingNotityInfoClose，支持在线、离线通知
        /// </summary>
        kNIMSignalingEventTypeClose = 1,
        /// <summary>
        /// 返回NIMSignalingNotityInfoJoin，支持在线、离线通知
        /// </summary>
        kNIMSignalingEventTypeJoin = 2,
        /// <summary>
        /// 返回NIMSignalingNotityInfoInvite，支持在线、离线通知
        /// </summary>
        kNIMSignalingEventTypeInvite = 3,
        /// <summary>
        /// 返回NIMSignalingNotityInfoCancelInvite，支持在线、离线通知
        /// </summary>
        kNIMSignalingEventTypeCancelInvite = 4,
        /// <summary>
        /// 返回NIMSignalingNotityInfoReject，支持在线、多端同步、离线通知
        /// </summary>
        kNIMSignalingEventTypeReject = 5,
        /// <summary>
        /// 返回NIMSignalingNotityInfoAccept，支持在线、多端同步、离线通知
        /// </summary>
        kNIMSignalingEventTypeAccept = 6,
        /// <summary>
        /// 返回NIMSignalingNotityInfoLeave，支持在线、离线通知
        /// </summary>
        kNIMSignalingEventTypeLeave = 7,
        /// <summary>
        /// 返回NIMSignalingNotityInfoControl，支持在线通知
        /// </summary>
        kNIMSignalingEventTypeCtrl = 8,
    }


    /// <summary>
    /// 事件通知信息基类
    /// </summary>
    public class NIMSignalingNotityInfo
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public NIMSignalingEventType event_type_;
        /// <summary>
        /// 频道信息 
        /// </summary>
        public NIMSignalingChannelInfo channel_info_;
        /// <summary>
        /// 操作者
        /// </summary>
        public string from_account_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 操作的时间戳
        /// </summary>
        public ulong timestamp_;

        public override string ToString() 
        {
            string info = string.Format("NotifyInfo event_type:{0},from_account_id:{1},custom_info:{2},timestamp:{3} {4}",
                event_type_, from_account_id_, custom_info_, timestamp_, channel_info_==null?"":channel_info_.ToString());
            return info;
        }
    }


    /// <summary>
    /// 频道关闭事件通知信息，event_type_=kNIMSignalingEventTypeClose
    /// </summary>
    class NIMSignalingNotityInfoClose : NIMSignalingNotityInfo
    {
        public override string ToString()
        {
            string info = base.ToString();
            return info;
        }
    }

    /// <summary>
    /// 加入频道事件通知信息，event_type_=kNIMSignalingEventTypeJoin
    /// </summary>
    public class NIMSignalingNotityInfoJoin : NIMSignalingNotityInfo
    {
        /// <summary>
        ///  加入成员的信息，用于获得uid
        /// </summary>
        public NIMSignalingMemberInfo member_;

        public override string ToString()
        {
            string info = string.Format("NotifyJoinInfo {0},{1}", member_ == null ? "" : member_.ToString(), base.ToString());
            return info;
        }
    }

    /// <summary>
    /// 邀请事件通知信息，event_type_=kNIMSignalingEventTypeInvite
    /// </summary>
    public class NIMSignalingNotityInfoInvite : NIMSignalingNotityInfo
    {
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public string to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id，用于被邀请者回传request_id_作对应的回应操作
        /// </summary>
        public string request_id_;
        /// <summary>
        ///  推送信息
        /// </summary>
        public NIMSignalingPushInfo push_info_;

        public NIMSignalingNotityInfoInvite()
        {
            push_info_ = new NIMSignalingPushInfo();
        }

        public override string ToString()
        {
            string info = string.Format("NotifyCancelInviteInfo to_account_id:{0},request_id:{1},push_info:{2},{3}", to_account_id_, request_id_, push_info_.ToString(),base.ToString());
            return info;
        }
    }

    /// <summary>
    /// 取消邀请事件通知信息，event_type_=kNIMSignalingEventTypeCancelInvite
    /// </summary>
    public class NIMSignalingNotityInfoCancelInvite : NIMSignalingNotityInfo
    {
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public string to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public string request_id_;

        public override string ToString()
        {
            string info = string.Format("NotifyCancelInviteInfo to_account_id:{0},request_id:{1},{2}", to_account_id_, request_id_, base.ToString());
            return info;
        }
    }

    /// <summary>
    /// 拒绝邀请事件通知信息，event_type_=kNIMSignalingEventTypeReject
    /// </summary>
    public class NIMSignalingNotityInfoReject : NIMSignalingNotityInfo
    {
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public string to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public string request_id_;

        public override string ToString()
        {
            string info = string.Format("NotifyRejectInfo to_account_id:{0},request_id:{1},{2}", to_account_id_, request_id_, base.ToString());
            return info;
        }
    }

    /// <summary>
    /// 接收邀请事件通知信息，event_type_=kNIMSignalingEventTypeAccept
    /// </summary>
    public class NIMSignalingNotityInfoAccept : NIMSignalingNotityInfo
    {
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public string to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public string request_id_;

        public override string ToString()
        {
            string info=string.Format("NotifyAcceptInfo to_account_id:{0},request_id:{1},{2}", to_account_id_, request_id_,base.ToString());
            return info;
        }
    }

    /// <summary>
    /// 退出频道事件通知信息，event_type_=kNIMSignalingEventTypeLeave
    /// </summary>
    public class NIMSignalingNotityInfoLeave : NIMSignalingNotityInfo
    {

    }


    /// <summary>
    /// 控制事件通知信息，event_type_=kNIMSignalingEventTypeCtrl
    /// </summary>
    public class NIMSignalingNotityInfoControl : NIMSignalingNotityInfo
    {

    }


    /// <summary>
    /// 创建频道接口nim_signaling_create的传入参数
    /// </summary>
    public class NIMSignalingCreateParam
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名，可缺省
        /// </summary>
        public string channel_name_;
        /// <summary>
        /// 创建时传入的扩展字段，可缺省
        /// </summary>
        public string channel_ext_;
    }



    /// <summary>
    /// 创建频道结果回调信息
    /// </summary>
    public class NIMSignalingCreateResParam
    {
        /// <summary>
        /// 频道信息
        /// </summary>
        public NIMSignalingChannelInfo channel_info_;
    }


    /// <summary>
    /// 关闭频道接口nim_signaling_close的传入参数
    /// </summary>
    public class NIMSignalingCloseParam
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    }


    /// <summary>
    /// 加入频道接口nim_signaling_join的传入参数
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingJoinParam
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 操作者附加的自定义信息，透传给其他人，可缺省
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 自己在频道中对应的uid，大于零有效，无效时服务器会分配随机唯一的uid
        /// </summary>
        public long uid_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_; 
    }



    /// <summary>
    /// 加入频道结果回调信息
    /// </summary>
    public class NIMSignalingJoinResParam
    {
        /// <summary>
        /// 频道的详细信息 
        /// </summary>
        public NIMSignalingChannelDetailedinfo info_;

        public NIMSignalingJoinResParam()
        {
            info_ = new NIMSignalingChannelDetailedinfo();
        }
    }



    public class NIMSignalingAcceptResParam:NIMSignalingJoinResParam
    {

    }

    public class NIMSignalingCallResParam: NIMSignalingJoinResParam
    {

    }

    /// <summary>
    /// 离开频道接口nim_signaling_leave的传入参数
    /// </summary>
    public class NIMSignalingLeaveParam
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_; 
    }

    /// <summary>
    /// 呼叫接口nim_signaling_call的传入参数
    /// </summary>
    public class NIMSignalingCallParam
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他 
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名，可缺省
        /// </summary>
        public string channel_name_;
        /// <summary>
        /// 创建时传入的扩展字段，可缺省
        /// </summary>
        public string channel_ext_;
        /// <summary>
        /// 自己在频道中对应的uid，大于零有效，无效时服务器会分配随机唯一的uid
        /// </summary>
        public long uid_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public string account_id_;
        /// <summary>
        ///  邀请者邀请的请求id，需要邀请者填写，之后取消邀请、拒绝、接受需要复用该request_id_
        /// </summary>
        public string request_id_;
        /// <summary>
        /// 操作的扩展字段，透传给被邀请者，可缺省
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
        /// <summary>
        /// 推送属性
        /// </summary>
        public NIMSignalingPushInfo push_info_;

        public NIMSignalingCallParam()
        {
            push_info_ = new NIMSignalingPushInfo();
        }
    }

    /// <summary>
    /// 邀请接口nim_signaling_invite的传入参数
    /// </summary>
    public class NIMSignalingInviteParam
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 邀请者邀请的请求id，需要邀请者填写，之后取消邀请、拒绝、接受需要复用该request_id_
        /// </summary>
        public string request_id_;
        /// <summary>
        /// 操作的扩展字段，透传给被邀请者，可缺省
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
        /// <summary>
        /// 推送属性
        /// </summary>
        public NIMSignalingPushInfo push_info_;
        public NIMSignalingInviteParam()
        {
            push_info_ = new NIMSignalingPushInfo();
        }
    }

    /// <summary>
    /// 取消邀请接口nim_signaling_cancel_invite的传入参数
    /// </summary>
    public class NIMSignalingCancelInviteParam
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public string request_id_;
        /// <summary>
        /// 操作的扩展字段，可缺省
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_; 
    };

    /// <summary>
    /// 拒绝接口nim_signaling_reject的传入参数
    /// </summary>
    public class NIMSignalingRejectParam
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public string request_id_;
        /// <summary>
        ///  操作的扩展字段
        /// </summary>
        public string custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    }



    /// <summary>
    /// 接受邀请接口nim_signaling_accept的传入参数
    /// </summary>
    public class NIMSignalingAcceptParam
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 邀请者邀请的请求id 
        /// </summary>
        public string request_id_;
        /// <summary>
        /// 操作的扩展字段 
        /// </summary>
        public string accept_custom_info_;
        /// <summary>
        /// 是否存离线 
        /// </summary>
        public bool offline_enabled_;
        /// <summary>
        /// 是否加入，默认不打开，打开后后续参数uid_、join_custom_info_有效 
        /// </summary>
        public bool auto_join_;
        /// <summary>
        /// 自己在频道中对应的uid，大于零有效，无效时服务器会分配随机唯一的uid，可缺省填0 
        /// </summary>
        public long uid_;
        /// <summary>
        /// 加入频道的自定义扩展信息，将在加入频道通知中带给其他频道成员，可缺省 
        /// </summary>
        public string join_custom_info_; 
    }




    /// <summary>
    ///控制通知接口nim_signaling_control的传入参数 
    /// </summary>
    public class NIMSignalingControlParam
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public string channel_id_;
        /// <summary>
        /// 对方accid，如果为空，则通知所有人 
        /// </summary>
        public string account_id_;
        /// <summary>
        /// 操作的扩展字段 
        /// </summary>
        public string custom_info_;
    }

    /// <summary>
    /// 事件回调函数，用于在线通知和多端同步通知
    /// </summary>
    /// <param name="notify_info">事件回调的信息指针，根据NIMSignalingNotityInfo.event_type_指向对应NIMSignalingNotityInfo扩展，如NIMSignalingNotityInfoAccept</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    public delegate void NimSignalingNotifyHandler(NIMSignalingNotityInfo notify_info);

    /// <summary>
    /// 事件回调函数，用于离线通知
    /// </summary>
    /// <param name="info_list">事件回调的信息NIMSignalingNotityInfo指针的数组，根据NIMSignalingNotityInfo.event_type_指向对应NIMSignalingNotityInfo扩展，如NIMSignalingNotityInfoAccept</param>
    /// <param name="size">info_list数组的长度</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    public delegate void NimSignalingNotifyListHandler(List<NIMSignalingNotityInfo> notifys);

    /// <summary>
    /// 频道列表同步回调函数
    /// 在login或者relogin后，会通知该设备账号还未退出的频道列表，用于同步；如果没有在任何频道中，也会返回该同步通知，list为空
    /// </summary>
    /// <param name="info_list">频道的详细信息NIMSignalingChannelDetailedinfo指针的数组，可能为空</param>
    /// <param name="size">info_list数组的长度</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    public delegate void NimSignalingChannelsSyncHandler(List<NIMSignalingChannelDetailedinfo> channels);

    /// <summary>
    /// 频道成员变更同步回调函数
    /// 用于同步频道内的成员列表变更，当前该接口为定时接口，2分钟同步一次，成员有变化时才上报。
    /// 由于一些特殊情况，导致成员在离开或掉线前没有主动调用离开频道接口，使得该成员的离开没有对应的离开通知事件，由该回调接口【频道成员变更同步通知】告知用户。
    /// </summary>
    /// <param name="detailed_info">频道的详细信息</param>
    /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理</param>
    public delegate void  NimSignalingMembersSyncHandler(NIMSignalingChannelDetailedinfo detailed_info);

//     /// <summary>
//     /// 操作回调，通用的操作回调接口
//     /// eg.control ,cancel_invite reject,invite,leave,close
//     /// </summary>
//     /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
//     public delegate void NimSignalingOptHandler(int code);

    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// eg.control ,cancel_invite reject,invite,leave,close
    /// </summary>
    /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
    public delegate void NimSignalingOptControlHandler(NIMSignalingControlResCode code);

    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// eg.control ,cancel_invite reject,invite,leave,close
    /// </summary>
    /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
    public delegate void NimSignalingOptCancelInviteHandler(NIMSignalingCancelInviteResCode code);

    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// eg.control ,cancel_invite reject,invite,leave,close
    /// </summary>
    /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
    public delegate void NimSignalingOptInviteHandler(NIMSignalingInviteResCode code);
    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// eg.control ,cancel_invite reject,invite,leave,close
    /// </summary>
    /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
    public delegate void NimSignalingOptRejectHandler(NIMSignalingRejectOrAcceptResCode code);
    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// eg.control ,cancel_invite reject,invite,leave,close
    /// </summary>
    /// <param name="code"> 操作返回码NIMResCode，见NIMResponseCode.cs</param>
    public delegate void NimSignalingOptCloseOrLeaveHandler(NIMSignalingCloseOrLeaveResCode code);
    /// <summary>
    /// 呼叫操作回调
    /// </summary>
    /// <param name="code">操作返回码NIMResCode，见NIMResponseCode.cs</param>
    /// <param name="opt_res_param">结果回调信息</param>
    public delegate void NimSignalingOptCallHandler(NIMSignalingCallResCode code, NIMSignalingCallResParam opt_res_param);

    /// <summary>
    /// 接受操作回调
    /// </summary>
    /// <param name="code">操作返回码NIMResCode，见NIMResponseCode.cs</param>
    /// <param name="opt_res_param">结果回调信息</param>
    public delegate void NimSignalingOptAcceptHandler(NIMSignalingRejectOrAcceptResCode code, NIMSignalingAcceptResParam opt_res_param);

    /// <summary>
    /// 加入操作回调
    /// </summary>
    /// <param name="code">操作返回码NIMResCode，见NIMResponseCode.cs</param>
    /// <param name="opt_res_param">结果回调信息</param>
    public delegate void NimSignalingOptJoinHandler(NIMSignalingJoinResCode code, NIMSignalingJoinResParam opt_res_param);

    /// <summary>
    /// 创建操作回调
    /// </summary>
    /// <param name="code">操作返回码NIMResCode，见NIMResponseCode.cs</param>
    /// <param name="opt_res_param">结果回调信息</param>
    public delegate void NimSignalingOptCreateHandler(NIMSignalingCreateResCode code, NIMSignalingCreateResParam opt_res_param);
   
}
