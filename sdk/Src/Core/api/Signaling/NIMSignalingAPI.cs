using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIM.Signaling.Native;
using System.Runtime.InteropServices;

namespace NIM.Signaling
{
    public class NIMSignalingAPI
    {
        private static NimSignalingNotifyCbFunc NimSignalNotifyCb = NimSignalingNotifyCallback;
        private static NimSignalingNotifyListCbFunc NimSignalingNotifyListCb = NimSignalingNotifyListCallback;
        private static NimSignalingChannelsSyncCbFunc NimSignalingChannelsSyncCb = NimSignalingChannelsSyncCallback;
        private static NimSignalingMembersSyncCbFunc NimSignalingMembersSyncCb = NimSignalingMembersSyncCallback;
        private static NimSignalingOptCbFunc NimSignalingJoinCb = NimSignalingOptJoinCallback;
        private static NimSignalingOptCbFunc NimSignalingCreateCb = NimSignalingOptCreateCallback;
        private static NimSignalingOptCbFunc NimSignalingLeaveCb = NimSignalingOptLeaveCallback;
        private static NimSignalingOptCbFunc NimSignalingCloseCb = NimSignalingOptCloseCallback;
        private static NimSignalingOptCbFunc NimSignalingCallCb = NimSignalingOptCallCallback;
        private static NimSignalingOptCbFunc NimSignalingInviteCb = NimSignalingOptInviteCallback;
        private static NimSignalingOptCbFunc NimSignalingCancelInviteCb = NimSignalingOptCancelInviteCallback;
        private static NimSignalingOptCbFunc NimSignalingRejectCb = NimSignalingOptRejectCallback;
        private static NimSignalingOptCbFunc NimSignalingAcceptCb = NimSignalingOptAcceptCallback;
        private static NimSignalingOptCbFunc NimSignalingControlCb = NimSignalingOptControlCallback;

        /// <summary>
        /// 注册独立信令的在线通知回调接口
        /// </summary>
        /// <param name="cb">cb 结果回调见NimSignalingDef.cs</param>
        public static  void RegOnlineNotifyCb(NimSignalingNotifyHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            SignalingNativeMethods.nim_signaling_reg_online_notify_cb(NimSignalNotifyCb, ptr);
        }

        /// <summary>
        /// 注册独立信令的多端同步通知回调接口，用于通知信令相关的多端同步通知。比如自己在手机端接受邀请，PC端会同步收到这个通知
        /// </summary>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void RegMutilClientSyncNotifyCb(NimSignalingNotifyHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            SignalingNativeMethods.nim_signaling_reg_mutil_client_sync_notify_cb(NimSignalNotifyCb, ptr);
        }

        /// <summary>
        /// 注册独立信令的离线通知回调接口
        /// 需要用户在调用相关接口时，打开存离线的开关。如果用户已经接收消息，该通知会在服务器标记已读，之后不会再收到该消息。
        /// </summary>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void RegOfflineNotifyCb(NimSignalingNotifyListHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            SignalingNativeMethods.nim_signaling_reg_offline_notify_cb(NimSignalingNotifyListCb, ptr);
        }

        /// <summary>
        /// 注册独立信令的频道列表同步回调接口
        /// 在login或者relogin后，会通知该设备账号还未退出的频道列表，用于同步；如果没有在任何频道中，也会返回该同步通知，list为空
        /// </summary>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void RegChannelsSyncCb(NimSignalingChannelsSyncHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            SignalingNativeMethods.nim_signaling_reg_channels_sync_cb(NimSignalingChannelsSyncCb, ptr);
        }

        /// <summary>
        /// 注册独立信令的频道成员变更同步回调接口
        /// 用于同步频道内的成员列表变更，当前该接口为定时接口，2分钟同步一次，成员有变化时才上报。
        /// </summary>
        /// <param name="cb">cb 结果回调见NimSignalingDef.cs</param>
        public static void RegMembersSyncCb(NimSignalingMembersSyncHandler cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            SignalingNativeMethods.nim_signaling_reg_members_sync_cb(NimSignalingMembersSyncCb, ptr);
        }

        /// <summary>
        /// 独立信令 创建频道
        /// 该接口用户创建频道，同一时刻频道名互斥，不能重复创建。但如果频道名缺省，服务器会自动分配频道id。
        /// 对于频道在创建后如果没人加入，有效期2小时，当有成员加入后会自动延续频道有效期。当主动关闭频道或者最后一个成员退出后2小时后频道销毁。
        /// </summary>
        /// <param name="param">创建频道的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void SignalingCreate(NIMSignalingCreateParam param, NimSignalingOptCreateHandler cb)
        {
            NIMSignalingCreateParam_C param_c = SignalHelper.GetNativeNIMSignalingCreateParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_create(param_ptr, NimSignalingCreateCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingCreateResCode.kAbnormal, null);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 关闭销毁频道
        /// 整个通话结束，如果只单人退出请调用NimSignalingLeave接口
        /// 该接口可以由创建者和频道内所有成员调用，无权限限制
        /// 调用该接口成功后，其他所有频道内的成员都回收到频道结束的通知，被动离开频道。此时其他成员不需要调用离开接口，也不会收到别人的离开通知。
        /// </summary>
        /// <param name="param">关闭频道的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void SignalingClose(NIMSignalingCloseParam param, NimSignalingOptCloseOrLeaveHandler cb)
        {
            NIMSignalingCloseParam_C param_c = SignalHelper.GetNativeNIMSignalingCloseParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_close(param_ptr, NimSignalingCloseCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingCloseOrLeaveResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 加入频道接口
        /// </summary>
        /// <param name="param">加入频道的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void Join(NIMSignalingJoinParam param, NimSignalingOptJoinHandler cb)
        {
            NIMSignalingJoinParam_C param_c = SignalHelper.GetNativeNIMSignalingJoinParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_join(param_ptr, NimSignalingJoinCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingJoinResCode.kAbnormal, null);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 离开频道接口
        /// </summary>
        /// <param name="param">离开频道的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void Leave(NIMSignalingLeaveParam param, NimSignalingOptCloseOrLeaveHandler cb)
        {
            NIMSignalingLeaveParam_C param_c = SignalHelper.GetNativeNIMSignalingLeaveParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_leave(param_ptr, NimSignalingLeaveCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingCloseOrLeaveResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
           
        }

        /// <summary>
        /// 独立信令 呼叫接口
        /// 用于用户新开一个频道并邀请对方加入频道，如果返回码不是200、10201、10202时，sdk会主动关闭频道，标记接口调用失败
        /// 该接口为组合接口，等同于用户先创建频道，成功后加入频道并邀请对方
        /// </summary>
        /// <param name="param">呼叫的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void Call(NIMSignalingCallParam param, NimSignalingOptCallHandler cb)
        {
            NIMSignalingCallParam_C param_c = SignalHelper.GetNativeNIMSignalingCallParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_call(param_ptr, NimSignalingCallCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingCallResCode.kAbnormal, null);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 邀请接口
        /// 该接口用于邀请对方加入频道，邀请者必须是创建者或者是频道中成员。
        /// 如果需要对离线成员邀请，可以打开离线邀请开关并填写推送信息。被邀请者在线后通过离线通知接收到该邀请，并通过频道信息中的invalid_字段判断频道的有效性，也可以对所有离线消息处理后判断该邀请是否被取消。
        /// </summary>
        /// <param name="param">邀请的传入参数，其中邀请标识由用户生成，在取消邀请和邀请通知及接受拒绝时做对应</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs，其中opt_res_param无效</param>
        public static void Invite(NIMSignalingInviteParam param, NimSignalingOptInviteHandler cb)
        {
            NIMSignalingInviteParam_C param_c = SignalHelper.GetNativeNIMSignalingInviteParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_invite(param_ptr, NimSignalingInviteCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingInviteResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 取消邀请接口
        /// </summary>
        /// <param name="param">取消邀请的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        /// <param name="user_data">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理</param>
        public static void CancelInvite(NIMSignalingCancelInviteParam param, NimSignalingOptCancelInviteHandler cb)
        {
            NIMSignalingCancelInviteParam_C param_c = SignalHelper.GetNativeNIMSignalingCancelInviteParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_cancel_invite(param_ptr, NimSignalingCancelInviteCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingCancelInviteResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 拒绝邀请接口
        /// 拒绝邀请后用户也可以通过加入频道接口加入频道，接口的使用由用户的业务决定
        /// </summary>
        /// <param name="param">拒绝邀请的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void Reject(NIMSignalingRejectParam param, NimSignalingOptRejectHandler cb)
        {
            NIMSignalingRejectParam_C param_c = SignalHelper.GetNativeNIMSignalingRejectParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_reject(param_ptr, NimSignalingRejectCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingRejectOrAcceptResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }

        }

        /// <summary>
        /// 独立信令 接受邀请接口
        /// 不开自动加入开关：该接口只接受邀请并告知邀请者，并同步通知自己的其他在线设备，但不会主动加入频道，需要单独调用加入接口
        /// 打开自动加入开关：该接口为组合接口，等同于先调用接受邀请，成功后再加入频道。
        /// 该接口在打开自动加入开关后是组合接口，对应的通知会有2个，接收邀请通知和加入通知
        /// </summary>
        /// <param name="param">接受邀请的传入参数</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs，其中opt_res_param在打开自动加入开关，并成功后有效</param>
        public static void Accept(NIMSignalingAcceptParam param, NimSignalingOptAcceptHandler cb)
        {
            NIMSignalingAcceptParam_C param_c = SignalHelper.GetNativeNIMSignalingAcceptParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_accept(param_ptr, NimSignalingAcceptCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingRejectOrAcceptResCode.kAbnormal, null);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        /// <summary>
        /// 独立信令 用户自定义控制指令接口
        /// 该接口用于在频道中透传一些自定义指令，协助频道管理
        /// </summary>
        /// <param name="param">控制通知的传入参数，接收方to_填空为群发，只支持在线通知</param>
        /// <param name="cb">结果回调见NimSignalingDef.cs</param>
        public static void Control(NIMSignalingControlParam param, NimSignalingOptControlHandler cb)
        {
            NIMSignalingControlParam_C param_c = SignalHelper.GetNativeNIMSignalingControlParam(param);
            int nSizeOfParam = Marshal.SizeOf(param_c);
            IntPtr param_ptr = Marshal.AllocHGlobal(nSizeOfParam);
            try
            {
                Marshal.StructureToPtr(param_c, param_ptr, false);
                var user_ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
                SignalingNativeMethods.nim_signaling_control(param_ptr, NimSignalingControlCb, user_ptr);
            }
            catch (Exception)
            {
                if (cb != null)
                    cb.Invoke(NIMSignalingControlResCode.kAbnormal);
            }
            finally
            {
                Marshal.FreeHGlobal(param_ptr);
            }
        }

        private static void NimSignalingNotifyCallback(IntPtr notify_info, IntPtr user_data)
        {
            NIMSignalingNotityInfo info = NotityInfoFromIntPtr(notify_info);
            NimUtility.DelegateConverter.Invoke<NimSignalingNotifyHandler>(user_data, info);
        }

        private static void NimSignalingNotifyListCallback(IntPtr info_list, int size, IntPtr user_data)
        {
            List<NIMSignalingNotityInfo> notifys = new List<NIMSignalingNotityInfo>();
            
            if(info_list!=IntPtr.Zero)
            {
                for (int i = 0; i < size; i++)
                {
                    IntPtr p_data = NimUtility.IntPtrExtensions.Increment(info_list, i * Marshal.SizeOf(typeof(IntPtr)));
                    IntPtr src_data = NimUtility.IntPtrExtensions.CreateFromeIntPtr(p_data);
                    NIMSignalingNotityInfo info = NotityInfoFromIntPtr(src_data);
                    if (info != null)
                        notifys.Add(info);
                }
            }

            NimUtility.DelegateConverter.Invoke<NimSignalingNotifyListHandler>(user_data, notifys);
        }

        private static NIMSignalingNotityInfo NotityInfoFromIntPtr(IntPtr src_data)
        {
            NIMSignalingNotityInfo_C info_c = (NIMSignalingNotityInfo_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfo_C));
            NIMSignalingNotityInfo info = null;
            object notity_objtect = null;
            switch (info_c.event_type_)
            {
                case NIMSignalingEventType.kNIMSignalingEventTypeClose:
                case NIMSignalingEventType.kNIMSignalingEventTypeLeave:
                case NIMSignalingEventType.kNIMSignalingEventTypeCtrl:
                    notity_objtect = info_c;
                    info = SignalHelper.NIMSignalingNotityInfoFromC(info_c);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeJoin:
                    notity_objtect = (NIMSignalingNotityInfoJoin_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfoJoin_C));
                    info = SignalHelper.NIMSignalingNotityInfoFromC((NIMSignalingNotityInfoJoin_C)notity_objtect);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeInvite:
                    notity_objtect = (NIMSignalingNotityInfoInvite_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfoInvite_C));
                    info = SignalHelper.NIMSignalingNotityInfoFromC((NIMSignalingNotityInfoInvite_C)notity_objtect);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeCancelInvite:
                    notity_objtect = (NIMSignalingNotityInfoCancelInvite_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfoCancelInvite_C));
                    info = SignalHelper.NIMSignalingNotityInfoFromC((NIMSignalingNotityInfoCancelInvite_C)notity_objtect);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeReject:
                    notity_objtect = (NIMSignalingNotityInfoReject_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfoReject_C));
                    info = SignalHelper.NIMSignalingNotityInfoFromC((NIMSignalingNotityInfoReject_C)notity_objtect);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeAccept:
                    notity_objtect = (NIMSignalingNotityInfoAccept_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingNotityInfoAccept_C));
                    info = SignalHelper.NIMSignalingNotityInfoFromC((NIMSignalingNotityInfoAccept_C)notity_objtect);
                    break;
            }
            return info;
        }

        private static void NimSignalingChannelsSyncCallback(IntPtr info_list, int size, IntPtr user_data)
        {
            List<NIMSignalingChannelDetailedinfo> channels = new List<NIMSignalingChannelDetailedinfo>();
            if(info_list!=IntPtr.Zero)
            {
                for(int i=0;i<size;i++)
                {
                    NIMSignalingChannelDetailedinfo channel_detailed_info = new NIMSignalingChannelDetailedinfo();

                    IntPtr detailed_ptr = NimUtility.IntPtrExtensions.Increment(info_list, i * Marshal.SizeOf(typeof(NIMSignalingChannelDetailedinfo_C)));
                    NIMSignalingChannelDetailedinfo_C detailed_info = (NIMSignalingChannelDetailedinfo_C)Marshal.PtrToStructure(detailed_ptr, typeof(NIMSignalingChannelDetailedinfo_C));

                    for (int j = 0; j < detailed_info.member_size_; j++)
                    {
                        IntPtr src_data = NimUtility.IntPtrExtensions.Increment(detailed_info.members_, j * Marshal.SizeOf(typeof(NIMSignalingMemberInfo_C)));
                        NIMSignalingMemberInfo_C member_info_c = (NIMSignalingMemberInfo_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingMemberInfo_C));
                        NIMSignalingMemberInfo member_info = SignalHelper.NIMSignalingMemberFromC(member_info_c);
                        channel_detailed_info.members_.Add(member_info);
                    }

                    channel_detailed_info.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(detailed_info.channel_info_);
                    channels.Add(channel_detailed_info);
                }
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingChannelsSyncHandler>(user_data, channels);
        }

        private static void NimSignalingMembersSyncCallback(IntPtr detailed_info, IntPtr user_data)
        {
            NIMSignalingChannelDetailedinfo channel_detailed_info = new NIMSignalingChannelDetailedinfo();


            if (detailed_info != IntPtr.Zero)
            {
                NIMSignalingChannelDetailedinfo_C res_param = (NIMSignalingChannelDetailedinfo_C)Marshal.PtrToStructure(detailed_info, typeof(NIMSignalingChannelDetailedinfo_C));
                channel_detailed_info.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(res_param.channel_info_);
                for (int i = 0; i < res_param.member_size_; i++)
                {
                    IntPtr src_data = NimUtility.IntPtrExtensions.Increment(res_param.members_, i * Marshal.SizeOf(typeof(NIMSignalingMemberInfo_C)));
                    NIMSignalingMemberInfo_C member_info_c = (NIMSignalingMemberInfo_C)Marshal.PtrToStructure(src_data,typeof(NIMSignalingMemberInfo_C));
                    NIMSignalingMemberInfo member_info = SignalHelper.NIMSignalingMemberFromC(member_info_c);
                    channel_detailed_info.members_.Add(member_info);
                }
            }
            
            NimUtility.DelegateConverter.Invoke<NimSignalingMembersSyncHandler>(user_data, channel_detailed_info);
        }

        private static void NimSignalingOptJoinCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingJoinResParam res_param = new NIMSignalingJoinResParam();

            NIMSignalingJoinResParam_C res_param_c = (NIMSignalingJoinResParam_C)Marshal.PtrToStructure(opt_res_param, typeof(NIMSignalingJoinResParam_C));
            res_param.info_.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(res_param_c.info_.channel_info_);
            for (int i=0;i<res_param_c.info_.member_size_;i++)
            {
                IntPtr src_data = NimUtility.IntPtrExtensions.Increment(res_param_c.info_.members_, i * Marshal.SizeOf(typeof(NIMSignalingMemberInfo_C)));
                NIMSignalingMemberInfo_C member_info_c = (NIMSignalingMemberInfo_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingMemberInfo_C));
                NIMSignalingMemberInfo member_info = SignalHelper.NIMSignalingMemberFromC(member_info_c);
                res_param.info_.members_.Add(member_info);
            }

            NimUtility.DelegateConverter.Invoke<NimSignalingOptJoinHandler>(user_data,code,res_param);
        }

        private static void NimSignalingOptCreateCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingCreateResParam res_param = new NIMSignalingCreateResParam();
            if(opt_res_param!=IntPtr.Zero)
            {
                NIMSignalingCreateResParam_C res_param_c = (NIMSignalingCreateResParam_C)Marshal.PtrToStructure(opt_res_param, typeof(NIMSignalingCreateResParam_C));
                res_param.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(res_param_c.channel_info_);
            }

            NimUtility.DelegateConverter.Invoke<NimSignalingOptCreateHandler>(user_data, code, res_param);
        }

        private static void NimSignalingOptLeaveCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingCloseOrLeaveResCode res_code = NIMSignalingCloseOrLeaveResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingCloseOrLeaveResCode) Enum.Parse(typeof(NIMSignalingCloseOrLeaveResCode), code.ToString());
            }
            catch
            {
                res_code= NIMSignalingCloseOrLeaveResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptCloseOrLeaveHandler>(user_data, res_code);
        }

        private static void NimSignalingOptCloseCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingCloseOrLeaveResCode res_code = NIMSignalingCloseOrLeaveResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingCloseOrLeaveResCode)Enum.Parse(typeof(NIMSignalingCloseOrLeaveResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingCloseOrLeaveResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptCloseOrLeaveHandler>(user_data, res_code);
        }

        private static void NimSignalingOptCallCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingCallResParam call_res_param = new NIMSignalingCallResParam();
            call_res_param.info_ = new NIMSignalingChannelDetailedinfo();
            NIMSignalingJoinResParam_C res_param_c = (NIMSignalingJoinResParam_C)Marshal.PtrToStructure(opt_res_param, typeof(NIMSignalingJoinResParam_C));
            if(res_param_c!=null)
            {
                call_res_param.info_.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(res_param_c.info_.channel_info_);
                for (int i = 0; i < res_param_c.info_.member_size_; i++)
                {
                    IntPtr src_data = NimUtility.IntPtrExtensions.Increment(res_param_c.info_.members_, i * Marshal.SizeOf(typeof(NIMSignalingMemberInfo_C)));
                    NIMSignalingMemberInfo_C member_info_c = (NIMSignalingMemberInfo_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingMemberInfo_C));
                    NIMSignalingMemberInfo member_info = SignalHelper.NIMSignalingMemberFromC(member_info_c);


                    call_res_param.info_.members_.Add(member_info);
                }
            }

            NIMSignalingCallResCode res_code = NIMSignalingCallResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingCallResCode)Enum.Parse(typeof(NIMSignalingCallResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingCallResCode.kUnknown;
            }

            NimUtility.DelegateConverter.Invoke<NimSignalingOptCallHandler>(user_data, res_code, call_res_param);
        }

        private static void NimSignalingOptInviteCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingInviteResCode res_code = NIMSignalingInviteResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingInviteResCode)Enum.Parse(typeof(NIMSignalingInviteResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingInviteResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptInviteHandler>(user_data, res_code);
        }

        private static void NimSignalingOptCancelInviteCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingCancelInviteResCode res_code = NIMSignalingCancelInviteResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingCancelInviteResCode)Enum.Parse(typeof(NIMSignalingCancelInviteResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingCancelInviteResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptCancelInviteHandler>(user_data, res_code);
        }

        private static void NimSignalingOptRejectCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingRejectOrAcceptResCode res_code = NIMSignalingRejectOrAcceptResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingRejectOrAcceptResCode)Enum.Parse(typeof(NIMSignalingRejectOrAcceptResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingRejectOrAcceptResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptRejectHandler>(user_data, res_code);
        }

        private static void NimSignalingOptAcceptCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingAcceptResParam accept_res_param = new NIMSignalingAcceptResParam();
            accept_res_param.info_ = new NIMSignalingChannelDetailedinfo();
            NIMSignalingJoinResParam_C res_param_c = (NIMSignalingJoinResParam_C)Marshal.PtrToStructure(opt_res_param, typeof(NIMSignalingJoinResParam_C));
            if (res_param_c != null)
            {
                accept_res_param.info_.channel_info_ = SignalHelper.NIMSignalingChannelInfoFromC(res_param_c.info_.channel_info_);
                for (int i = 0; i < res_param_c.info_.member_size_; i++)
                {
                    IntPtr src_data = NimUtility.IntPtrExtensions.Increment(res_param_c.info_.members_, i * Marshal.SizeOf(typeof(NIMSignalingMemberInfo_C)));
                    NIMSignalingMemberInfo_C member_info_c = (NIMSignalingMemberInfo_C)Marshal.PtrToStructure(src_data, typeof(NIMSignalingMemberInfo_C));
                    NIMSignalingMemberInfo member_info = SignalHelper.NIMSignalingMemberFromC(member_info_c);
                    accept_res_param.info_.members_.Add(member_info);
                }
            }

            NIMSignalingRejectOrAcceptResCode res_code = NIMSignalingRejectOrAcceptResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingRejectOrAcceptResCode)Enum.Parse(typeof(NIMSignalingRejectOrAcceptResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingRejectOrAcceptResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptAcceptHandler>(user_data, res_code, accept_res_param);
        }

        private static void NimSignalingOptControlCallback(int code, IntPtr opt_res_param, IntPtr user_data)
        {
            NIMSignalingControlResCode res_code = NIMSignalingControlResCode.kUnknown;
            try
            {
                res_code = (NIMSignalingControlResCode)Enum.Parse(typeof(NIMSignalingControlResCode), code.ToString());
            }
            catch
            {
                res_code = NIMSignalingControlResCode.kUnknown;
            }
            NimUtility.DelegateConverter.Invoke<NimSignalingOptControlHandler>(user_data, res_code);
        }
    }


}
