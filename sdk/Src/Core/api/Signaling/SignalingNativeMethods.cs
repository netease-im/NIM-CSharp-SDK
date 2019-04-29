using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM.Signaling.Native
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimSignalingNotifyCbFunc(IntPtr notify_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimSignalingNotifyListCbFunc(IntPtr info_list, int size, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimSignalingChannelsSyncCbFunc(IntPtr info_list, int size, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimSignalingMembersSyncCbFunc(IntPtr detailed_info, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NimSignalingOptCbFunc(int code, IntPtr opt_res_param, IntPtr user_data);

    class SignalingNativeMethods
    {
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reg_online_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_reg_online_notify_cb(NimSignalingNotifyCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reg_mutil_client_sync_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_reg_mutil_client_sync_notify_cb(NimSignalingNotifyCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reg_offline_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_reg_offline_notify_cb(NimSignalingNotifyListCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reg_channels_sync_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_reg_channels_sync_cb(NimSignalingChannelsSyncCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reg_offline_notify_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_reg_members_sync_cb(NimSignalingMembersSyncCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_create(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_close(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_join", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_join(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_leave", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_leave(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_call", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_call(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_invite", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_invite(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_cancel_invite", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_signaling_cancel_invite(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_reject", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        internal static extern void nim_signaling_reject(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_accept", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        internal static extern void nim_signaling_accept(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_signaling_control", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        internal static extern void nim_signaling_control(IntPtr param, NimSignalingOptCbFunc cb, IntPtr user_data);
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NIMSignalingCreateParam_C
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名，可缺省
        /// </summary>
        public IntPtr channel_name_;
        /// <summary>
        /// 创建时传入的扩展字段，可缺省
        /// </summary>
        public IntPtr channel_ext_;
    }

    /// <summary>
    /// 频道属性
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct NIMSignalingChannelInfo_C
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名
        /// </summary>
        public IntPtr channel_name_;
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        ///<summary>
        ///创建时传入的扩展字段
        ///</summary>  
        public IntPtr channel_ext_;
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
        public IntPtr creator_id_;
        /// <summary>
        /// 频道是否有效
        /// </summary>
        public bool invalid_;

    }

    /// <summary>
    /// 频道的详细信息，包含频道信息及成员列表
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    struct NIMSignalingChannelDetailedinfo_C
    {
        /// <summary>
        /// 频道信息
        /// </summary>
        public NIMSignalingChannelInfo_C channel_info_;
        /// <summary>
        /// 频道内成员信息数组
        /// </summary>
	    public System.IntPtr members_;
        /// <summary>
        /// 频道内成员信息数组大小
        /// </summary>
        public int member_size_;
    }

    /// <summary>
    /// 事件通知信息基类
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingNotityInfo_C
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public NIMSignalingEventType event_type_;
        /// <summary>
        /// 频道信息 
        /// </summary>
        public NIMSignalingChannelInfo_C channel_info_;
        /// <summary>
        /// 操作者
        /// </summary>
        public IntPtr from_account_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 操作的时间戳
        /// </summary>
        public ulong timestamp_;

    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingNotityInfoJoin_C : NIMSignalingNotityInfo_C
    {
        /// <summary>
        ///  加入成员的信息，用于获得uid
        /// </summary>
        public NIMSignalingMemberInfo_C member_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]

    public class NIMSignalingNotityInfoInvite_C : NIMSignalingNotityInfo_C
    {
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public IntPtr to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id，用于被邀请者回传request_id_作对应的回应操作
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        ///  推送信息
        /// </summary>
        public NIMSignalingPushInfo_C push_info_;

    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingNotityInfoCancelInvite_C : NIMSignalingNotityInfo_C
    {
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public IntPtr to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public IntPtr request_id_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingNotityInfoReject_C : NIMSignalingNotityInfo_C
    {
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public IntPtr to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public IntPtr request_id_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingNotityInfoAccept_C : NIMSignalingNotityInfo_C
    {
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public IntPtr to_account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public IntPtr request_id_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class NIMSignalingCloseParam_C
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    }


    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingJoinParam_C
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 操作者附加的自定义信息，透传给其他人，可缺省
        /// </summary>
        public IntPtr custom_info_;
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
    /// 创建频道结果回调信息
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingCreateResParam_C
    {
        /// <summary>
        /// 频道信息
        /// </summary>
        public NIMSignalingChannelInfo_C channel_info_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    class NIMSignalingJoinResParam_C
    {
        /// <summary>
        /// 频道的详细信息 
        /// </summary>
        public NIMSignalingChannelDetailedinfo_C info_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct NIMSignalingLeaveParam_C
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 操作的扩展字段
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    }

    /// <summary>
    /// 呼叫接口nim_signaling_call的传入参数
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingCallParam_C
    {
        /// <summary>
        /// 通话类型,1:音频;2:视频;3:其他 
        /// </summary>
        public NIMSignalingType channel_type_;
        /// <summary>
        /// 创建时传入的频道名，可缺省
        /// </summary>
        public IntPtr channel_name_;
        /// <summary>
        /// 创建时传入的扩展字段，可缺省
        /// </summary>
        public IntPtr channel_ext_;
        /// <summary>
        /// 自己在频道中对应的uid，大于零有效，无效时服务器会分配随机唯一的uid
        /// </summary>
        public long uid_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        ///  邀请者邀请的请求id，需要邀请者填写，之后取消邀请、拒绝、接受需要复用该request_id_
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        /// 操作的扩展字段，透传给被邀请者，可缺省
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
        /// <summary>
        /// 推送属性
        /// </summary>
        public NIMSignalingPushInfo_C push_info_;
    }

    /// <summary>
    /// 邀请接口nim_signaling_invite的传入参数
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingInviteParam_C
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        /// 邀请者邀请的请求id，需要邀请者填写，之后取消邀请、拒绝、接受需要复用该request_id_
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        /// 操作的扩展字段，透传给被邀请者，可缺省
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
        /// <summary>
        /// 推送属性
        /// </summary>
        public NIMSignalingPushInfo_C push_info_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingCancelInviteParam_C
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 被邀请者的账号
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        /// 操作的扩展字段，可缺省
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    };

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingRejectParam_C
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        /// 邀请者邀请的请求id
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        ///  操作的扩展字段
        /// </summary>
        public IntPtr custom_info_;
        /// <summary>
        /// 是否存离线
        /// </summary>
        public bool offline_enabled_;
    }


    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingAcceptParam_C
    {
        /// <summary>
        /// 服务器生成的频道id
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 邀请者的账号
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        /// 邀请者邀请的请求id 
        /// </summary>
        public IntPtr request_id_;
        /// <summary>
        /// 操作的扩展字段 
        /// </summary>
        public IntPtr accept_custom_info_;
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
        public IntPtr join_custom_info_;
    }

    /// <summary>
    ///控制通知接口nim_signaling_control的传入参数 
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingControlParam_C
    {
        /// <summary>
        /// 服务器生成的频道id 
        /// </summary>
        public IntPtr channel_id_;
        /// <summary>
        /// 对方accid，如果为空，则通知所有人 
        /// </summary>
        public IntPtr account_id_;
        /// <summary>
        /// 操作的扩展字段 
        /// </summary>
        public IntPtr custom_info_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingPushInfo_C
    {
        /// <summary>
        /// 是否需要推送，默认false
        /// </summary>
        public bool need_push_;
        /// <summary>
        /// 推送标题
        /// </summary>
        public IntPtr push_title_;
        /// <summary>
        /// 推送内容
        /// </summary>
        public IntPtr push_content_;
        /// <summary>
        /// 推送自定义字段
        /// </summary>
        public IntPtr push_payload_;
        /// <summary>
        /// 是否计入未读计数,默认false
        /// </summary>
        public bool need_badge_;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class NIMSignalingMemberInfo_C
    {
        /// <summary>
        /// 成员的 accid
        /// </summary>
        public IntPtr account_id_;
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
    }

    public class SignalHelper
    {
        public static NIMSignalingChannelInfo NIMSignalingChannelInfoFromC(NIMSignalingChannelInfo_C info_c)
        {
            NIMSignalingChannelInfo info = new NIMSignalingChannelInfo();

            info.channel_ext_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.channel_ext_));
            info.channel_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.channel_id_));
            info.channel_name_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.channel_name_));
            info.channel_type_ = info_c.channel_type_;
            info.create_timestamp_ = info_c.create_timestamp_;
            info.creator_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.creator_id_));
            info.expire_timestamp_ = info_c.expire_timestamp_;
            info.invalid_ = info_c.invalid_;



            return info;
        }

        public static NIMSignalingNotityInfo NIMSignalingNotityInfoFromC(NIMSignalingNotityInfo_C info_c)
        {
            NIMSignalingNotityInfo info = new NIMSignalingNotityInfo();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;
            return info;
        }

        public static NIMSignalingNotityInfoReject NIMSignalingNotityInfoFromC(NIMSignalingNotityInfoReject_C info_c)
        {
            NIMSignalingNotityInfoReject info = new NIMSignalingNotityInfoReject();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;

            info.to_account_id_= Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.to_account_id_));
            info.request_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.request_id_));
            return info;
        }

        public static NIMSignalingNotityInfoAccept NIMSignalingNotityInfoFromC(NIMSignalingNotityInfoAccept_C info_c)
        {
            NIMSignalingNotityInfoAccept info = new NIMSignalingNotityInfoAccept();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;

            info.to_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.to_account_id_));
            info.request_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.request_id_));
            return info;
        }

        public static NIMSignalingNotityInfoCancelInvite NIMSignalingNotityInfoFromC(NIMSignalingNotityInfoCancelInvite_C info_c)
        {
            NIMSignalingNotityInfoCancelInvite info = new NIMSignalingNotityInfoCancelInvite();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;

            info.to_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.to_account_id_));
            info.request_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.request_id_));
            return info;
        }

        public static NIMSignalingNotityInfoInvite NIMSignalingNotityInfoFromC(NIMSignalingNotityInfoInvite_C info_c)
        {
            NIMSignalingNotityInfoInvite info = new NIMSignalingNotityInfoInvite();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;

            info.to_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.to_account_id_));
            info.request_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.request_id_));
            info.push_info_ = NIMSignalingPushInfoFromC(info_c.push_info_);
            return info;
        }

        public static NIMSignalingNotityInfoJoin NIMSignalingNotityInfoFromC(NIMSignalingNotityInfoJoin_C info_c)
        {
            NIMSignalingNotityInfoJoin info = new NIMSignalingNotityInfoJoin();
            info.event_type_ = info_c.event_type_;
            info.from_account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.from_account_id_));
            info.custom_info_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(info_c.custom_info_));
            info.channel_info_ = NIMSignalingChannelInfoFromC(info_c.channel_info_);
            info.timestamp_ = info_c.timestamp_;

            info.member_ = NIMSignalingMemberFromC(info_c.member_);
            return info;
        }


        public static NIMSignalingMemberInfo NIMSignalingMemberFromC(NIMSignalingMemberInfo_C param)
        {
            NIMSignalingMemberInfo member_info = new NIMSignalingMemberInfo();
            if(param!=null)
            {
                member_info.account_id_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(param.account_id_));
                member_info.create_timestamp_ = param.create_timestamp_;
                member_info.expire_timestamp_ = param.expire_timestamp_;
                member_info.uid_ = param.uid_;
            }
            return member_info;
        }

        public static NIMSignalingPushInfo NIMSignalingPushInfoFromC(NIMSignalingPushInfo_C param)
        {
            NIMSignalingPushInfo push_info = new NIMSignalingPushInfo();
            if (param != null)
            {
                push_info.need_badge_ = param.need_badge_;
                push_info.push_title_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(param.push_title_));
                push_info.push_content_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(param.push_content_));
                push_info.push_payload_ = Convert.ToString(Utf8StringMarshaler.GetInstance("").MarshalNativeToManaged(param.push_payload_));
                push_info.need_push_ = param.need_push_;
            }
            return push_info;
        }

        public static NIMSignalingControlParam_C GetNativeNIMSignalingControlParam(NIMSignalingControlParam param)
        {
            NIMSignalingControlParam_C param_c = new NIMSignalingControlParam_C();
            if(param!=null)
            {
                param_c.account_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_);
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
            }
            return param_c;
        }

        public static NIMSignalingAcceptParam_C GetNativeNIMSignalingAcceptParam(NIMSignalingAcceptParam param)
        {
            NIMSignalingAcceptParam_C param_c = new NIMSignalingAcceptParam_C();
            if (param != null)
            {
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.account_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_);
                param_c.request_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.request_id_);
                param_c.accept_custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.accept_custom_info_);
                param_c.join_custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.join_custom_info_);
                param_c.uid_ = param.uid_;
                param_c.auto_join_ = param.auto_join_;
                param_c.offline_enabled_ = param.offline_enabled_;
            }
            return param_c;
        }

        public static NIMSignalingRejectParam_C GetNativeNIMSignalingRejectParam(NIMSignalingRejectParam param)
        {
            NIMSignalingRejectParam_C param_c = new NIMSignalingRejectParam_C();
            if(param!=null)
            {
                param_c.account_id_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_);
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
                param_c.offline_enabled_ = param.offline_enabled_;
                param_c.request_id_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.request_id_);
            }
            return param_c;
        }

        public static NIMSignalingCancelInviteParam_C GetNativeNIMSignalingCancelInviteParam(NIMSignalingCancelInviteParam param)
        {
            NIMSignalingCancelInviteParam_C param_c = new NIMSignalingCancelInviteParam_C();
            if(param!=null)
            {
                param_c.account_id_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_);
                param_c.channel_id_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_); ;
                param_c.custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
                param_c.offline_enabled_=param.offline_enabled_ ;
                param_c.request_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.request_id_);
            }
            return param_c;
        }

        public static NIMSignalingInviteParam_C GetNativeNIMSignalingInviteParam(NIMSignalingInviteParam param)
        {
            NIMSignalingInviteParam_C param_c = new NIMSignalingInviteParam_C();
            if(param!=null)
            {
                param_c.account_id_=Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_);
                param_c.channel_id_=Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_); 
                param_c.custom_info_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_); ;
                param_c.offline_enabled_ = param.offline_enabled_;
                param_c.push_info_ = GetNativeNIMSignalingPushInfo(param.push_info_);
                param_c.request_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.request_id_);
            }
            return param_c;
        }

        public static NIMSignalingCallParam_C GetNativeNIMSignalingCallParam(NIMSignalingCallParam param)
        {
            NIMSignalingCallParam_C param_c = new NIMSignalingCallParam_C();
            if(param!=null)
            {
                param_c.account_id_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.account_id_); ;
                param_c.channel_ext_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_ext_); ;
                param_c.channel_name_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_name_); ;
                param_c.channel_type_ = param.channel_type_;
                param_c.custom_info_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
                param_c.offline_enabled_ = param.offline_enabled_ ;
                param_c.push_info_ = GetNativeNIMSignalingPushInfo(param.push_info_);
                param_c.request_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.request_id_);
                param_c.uid_ = param.uid_;
            }
            return param_c;
        }

        public static NIMSignalingLeaveParam_C GetNativeNIMSignalingLeaveParam(NIMSignalingLeaveParam param)
        {
            NIMSignalingLeaveParam_C param_c = new NIMSignalingLeaveParam_C();
            if(param!=null)
            {
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.custom_info_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
                param_c.offline_enabled_ = param.offline_enabled_;
            }
            return param_c;
        }

        public static NIMSignalingCreateParam_C GetNativeNIMSignalingCreateParam(NIMSignalingCreateParam param)
        {
            NIMSignalingCreateParam_C param_c = new NIMSignalingCreateParam_C();
            if(param!=null)
            {
                param_c.channel_ext_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_ext_); 
                param_c.channel_name_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_name_); 
                param_c.channel_type_ = param.channel_type_;
                
            }
            return param_c;
        }


        public static NIMSignalingCloseParam_C GetNativeNIMSignalingCloseParam(NIMSignalingCloseParam param)
        {
            NIMSignalingCloseParam_C param_c = new NIMSignalingCloseParam_C();
            if (param != null)
            {
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.custom_info_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_);
                param_c.offline_enabled_ = param.offline_enabled_;
            }
            return param_c;
        }

        public static NIMSignalingJoinParam_C GetNativeNIMSignalingJoinParam(NIMSignalingJoinParam param)
        {
            NIMSignalingJoinParam_C param_c = new NIMSignalingJoinParam_C();
            if(param!=null)
            {
                param_c.channel_id_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.channel_id_);
                param_c.custom_info_= Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.custom_info_); 
                param_c.offline_enabled_ = param.offline_enabled_;
                param_c.uid_ = param.uid_;
            }
            return param_c;
        }
        public static NIMSignalingPushInfo_C GetNativeNIMSignalingPushInfo(NIMSignalingPushInfo param)
        {
            NIMSignalingPushInfo_C param_c = new NIMSignalingPushInfo_C();
            if (param != null)
            {
                param_c.need_badge_ = param.need_badge_;
                param_c.push_title_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.push_title_);
                param_c.push_content_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.push_content_);
                param_c.push_payload_ = Utf8StringMarshaler.GetInstance("").MarshalManagedToNative(param.push_payload_);
                param_c.need_push_ = param.need_push_;
            }
            return param_c;
        }
    }
}
