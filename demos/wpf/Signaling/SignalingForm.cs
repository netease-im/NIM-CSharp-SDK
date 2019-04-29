using NIM.Signaling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
    public partial class SignalingForm : Form
    {
        public SignalingForm()
        {
            InitializeComponent();

            RegSignalingCallbacks();
        }

        private void RegSignalingCallbacks()
        {
            NIMSignalingAPI.RegChannelsSyncCb(NimSignalingChannelsSyncCallback);
            NIMSignalingAPI.RegMembersSyncCb(NimSignalingMembersSyncCallback);
            NIMSignalingAPI.RegMutilClientSyncNotifyCb(NimSignalingNotifyCallback);
            NIMSignalingAPI.RegOfflineNotifyCb(NimSignalingNotifyListCallback);
            NIMSignalingAPI.RegOnlineNotifyCb(NimSignalingOnlineNotifyCallback);
        }

        private void NimSignalingChannelsSyncCallback(List<NIMSignalingChannelDetailedinfo> channels)
        {
            string info ="size:"+channels.Count+" ";
            foreach(var detailedinfo in channels)
            {
                info += "channel name:"+detailedinfo.channel_info_.channel_name_ 
                    + "createor_id"+detailedinfo.channel_info_.creator_id_;
            }

            PrintInfo(info);
        }

        private void NimSignalingMembersSyncCallback(NIMSignalingChannelDetailedinfo detailed_info)
        {
            string info = "";
            if (detailed_info != null&&detailed_info.channel_info_!=null)
            {
                Action act = () =>
                {
                    lbMembers.Items.Clear();
                    foreach (var member in detailed_info.members_)
                    {
                        lbMembers.Items.Add(member.account_id_);
                    }
                };
                this.BeginInvoke(act);
               info= "NimSignalingMembersSyncCallback channel name:" + detailed_info.channel_info_.channel_name_
                        + "createor_id" + detailed_info.channel_info_.creator_id_;
            }
            PrintInfo(info);
        }

        private void NimSignalingNotifyCallback(NIMSignalingNotityInfo notify_info)
        {
            switch (notify_info.event_type_)
            {

                case NIMSignalingEventType.kNIMSignalingEventTypeClose:
                case NIMSignalingEventType.kNIMSignalingEventTypeLeave:
                case NIMSignalingEventType.kNIMSignalingEventTypeCtrl:
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeJoin:
                    NIMSignalingNotityInfoJoin join_info = (NIMSignalingNotityInfoJoin)notify_info;
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeInvite:
                    NIMSignalingNotityInfoInvite invite_info = (NIMSignalingNotityInfoInvite)notify_info;
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeCancelInvite:
                    NIMSignalingNotityInfoCancelInvite cancel_invite_info = (NIMSignalingNotityInfoCancelInvite)notify_info;
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeReject:
                    NIMSignalingNotityInfoReject reject_invite_info = (NIMSignalingNotityInfoReject)notify_info;
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeAccept:
                    NIMSignalingNotityInfoAccept accept_invite_info = (NIMSignalingNotityInfoAccept)notify_info;
                    break;

            }
            string info = "NimSignalingNotifyCallback event type:" + notify_info.event_type_
                   + "createor_id" + notify_info.channel_info_.creator_id_
                   +"channel_id"+notify_info.channel_info_.channel_id_
                   +"channel_name"+ notify_info.channel_info_.channel_name_
                   + "from_account_id" + notify_info.from_account_id_;
            PrintInfo(info);
        }

        private void NimSignalingNotifyListCallback(List<NIMSignalingNotityInfo> notifys)
        {
            string info = "NimSignalingNotifyListCallback size:" + notifys.Count + " ";
            foreach (var notify_info in notifys)
            {
                 info += "event type:" + notify_info.event_type_
                  + "createor_id" + notify_info.channel_info_.creator_id_
                  + "channel_id" + notify_info.channel_info_.channel_id_
                  + "channel_name" + notify_info.channel_info_.channel_name_
                  + "from_account_id" + notify_info.from_account_id_;
            }

            PrintInfo(info);
        }

        private void NimSignalingOnlineNotifyCallback(NIMSignalingNotityInfo notify_info)
        {
            string info = "";
            switch (notify_info.event_type_)
            {

                case NIMSignalingEventType.kNIMSignalingEventTypeClose:
                case NIMSignalingEventType.kNIMSignalingEventTypeLeave:
                case NIMSignalingEventType.kNIMSignalingEventTypeCtrl:
                    info = notify_info.ToString();
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeJoin:
                    NIMSignalingNotityInfoJoin join_info = (NIMSignalingNotityInfoJoin)notify_info;
                    info = join_info.ToString();
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeInvite:
                    NIMSignalingNotityInfoInvite invite_info = (NIMSignalingNotityInfoInvite)notify_info;
                    info = invite_info.ToString();
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeCancelInvite:
                    NIMSignalingNotityInfoCancelInvite cancel_invite_info = (NIMSignalingNotityInfoCancelInvite)notify_info;
                    info = cancel_invite_info.ToString();
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeReject:
                    NIMSignalingNotityInfoReject reject_invite_info = (NIMSignalingNotityInfoReject)notify_info;
                    info = reject_invite_info.ToString();
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeAccept:
                    NIMSignalingNotityInfoAccept accept_invite_info = (NIMSignalingNotityInfoAccept)notify_info;
                    info = accept_invite_info.ToString();
                    break;

            }
            PrintInfo(info);

            switch (notify_info.event_type_)
            {
                case NIMSignalingEventType.kNIMSignalingEventTypeJoin:
                    LbMembersOpt(true, notify_info.from_account_id_);
                    break;
                case NIMSignalingEventType.kNIMSignalingEventTypeLeave:
                    LbMembersOpt(false, notify_info.from_account_id_);
                    break;
            }

           
        }


        private void LbMembersOpt(bool add,string account)
        {
            Action action = () =>
            {
                bool find_account = false;
                foreach (var item in lbMembers.Items)
                {
                    string text = lbMembers.GetItemText(item);
                    if (text.Equals(account))
                    {
                        find_account = true;
                        break;
                    }
                }
                if (add)
                {
                    if (!find_account)
                        lbMembers.Items.Add(account);
                }
                else
                {
                    lbMembers.Items.Remove(account);
                }
            };
            BeginInvoke(action);
        }

        private void NimSignalingOptCreateCallback(NIMSignalingCreateResCode code, NIMSignalingCreateResParam create_res_param)
        {
            string info = "signal create cb code:" + code.ToString();
            if (create_res_param != null && create_res_param.channel_info_ != null)
            {
                Action action = () =>
                  {
                      tbChannelId.Text = create_res_param.channel_info_.channel_id_;
                      tbCreator.Text = create_res_param.channel_info_.creator_id_;
                  };
                this.BeginInvoke(action);

                info = string.Format("signal create cb code:{0},cid:{1}", code, create_res_param.channel_info_.channel_id_);
            }
            PrintInfo(info);
        }
        private void NimSignalingOptCloseCallback(NIMSignalingCloseOrLeaveResCode code)
        {
            string info = string.Format("signal close cb code:{0}", code);
            PrintInfo(info);
        }

        private void NimSignalingOptJoinCallback(NIMSignalingJoinResCode code, NIMSignalingJoinResParam opt_res_param)
        {
            string info = "signal join cb code:" + code.ToString();
            if (opt_res_param != null && opt_res_param.info_ != null && opt_res_param.info_.channel_info_ != null)
            {
                Action act = () =>
                {
                    foreach (var member in opt_res_param.info_.members_)
                    {
                        bool already_exist = false;
                        foreach (var item in lbMembers.Items)
                        {
                            string text = lbMembers.GetItemText(item);
                            if (text.Equals(member.account_id_))
                            {
                                already_exist = true;
                                break;
                            }
                        }
                        if (!already_exist)
                            lbMembers.Items.Add(member.account_id_);
                    }
                };
                this.BeginInvoke(act);

                info = string.Format("signal join cb code:{0},channel_ext:{1},cid:{2}", code,
                   opt_res_param.info_.channel_info_.channel_ext_,
                   opt_res_param.info_.channel_info_.channel_id_);
            }
            PrintInfo(info);
        }

        private void NimSignalingOptLeaveCallback(NIMSignalingCloseOrLeaveResCode code)
        {
            string info = string.Format("signal leave cb code:{0}", code);
            PrintInfo(info);
            Action act = () =>
            {
                lbMembers.Items.Clear();
            };
            this.BeginInvoke(act);
        }

        private void NimSignalingOptInviteCallback(NIMSignalingInviteResCode code)
        {
            string info = string.Format("signal invite cb code:{0}", code);
            PrintInfo(info);
        }

        private void NimSignalingOptCancelInviteHandler(NIMSignalingCancelInviteResCode code)
        {
            string info = string.Format("signal cancel invite cb code:{0}", code);
            PrintInfo(info);
        }

        private void NimSignalingOptAcceptCallback(NIMSignalingRejectOrAcceptResCode code, NIMSignalingAcceptResParam opt_res_param)
        {
            string info = "signal accept cb code:" + code.ToString();
            if (opt_res_param != null&& opt_res_param.info_!=null&& opt_res_param.info_.channel_info_!=null)
            {
               info = string.Format("signal accept cb code:{0},chanel_ext:{1},cid:{2}", code,
              opt_res_param.info_.channel_info_.channel_ext_,
              opt_res_param.info_.channel_info_.channel_id_);
            }
          
            PrintInfo(info);
        }

        private void NimSignalingOptRejectCallback(NIMSignalingRejectOrAcceptResCode code)
        {
            string info = string.Format("signal reject  cb code:{0}", code);
            PrintInfo(info);
        }

        private void NimSignalingOptControlCallback(NIMSignalingControlResCode code)
        {
            string info = string.Format("signal control  cb code:{0}", code);
            PrintInfo(info);
        }

        private void NimSignalingOptCallCallback(NIMSignalingCallResCode code, NIMSignalingCallResParam opt_res_param)
        {
            string info = string.Format("signal call  cb code:{0},channel_ext:{1},creator:{2},cid:{3},invide:{4}", code,
                opt_res_param.info_.channel_info_.channel_ext_,
                opt_res_param.info_.channel_info_.creator_id_,
                opt_res_param.info_.channel_info_.channel_id_,
                opt_res_param.info_.channel_info_.invalid_);
            PrintInfo(info);
        }





        private void PrintInfo(string info)
        {
            Action action = () =>
              {
                  Rtb_Info.Text = info + "\n\n" + Rtb_Info.Text;
              };
            this.BeginInvoke(action);
        }


        private void Btn_Signaling_Create_Click(object sender, EventArgs e)
        {
            NIMSignalingCreateParam param = new NIMSignalingCreateParam();
            
            param.channel_name_ = tbChannelName.Text;
            param.channel_type_ = GetChannelType();
            param.channel_ext_ = tbChannelExt.Text;

            NIMSignalingAPI.SignalingCreate(param, NimSignalingOptCreateCallback);
        }

        private void Btn_Signaling_Close_Click(object sender, EventArgs e)
        {
            NIMSignalingCloseParam param = new NIMSignalingCloseParam();
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.custom_info_ = tbOptExt.Text;
            param.channel_id_ = tbChannelId.Text;
            NIMSignalingAPI.SignalingClose(param, NimSignalingOptCloseCallback);
        }

        private void Btn_Signaling_Join_Click(object sender, EventArgs e)
        {
            NIMSignalingJoinParam param = new NIMSignalingJoinParam();
            param.channel_id_ = tbChannelId.Text;
            param.custom_info_ = tbOptExt.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            if (!string.IsNullOrEmpty(tbUid.Text))
                param.uid_ = Convert.ToInt64(tbUid.Text);
            
            NIMSignalingAPI.Join(param, NimSignalingOptJoinCallback);
        }

        private void Btn_Signaling_Leave_Click(object sender, EventArgs e)
        {
            NIMSignalingLeaveParam param = new NIMSignalingLeaveParam();
            param.channel_id_ = tbChannelId.Text;
            param.custom_info_ = tbOptExt.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;

            NIMSignalingAPI.Leave(param, NimSignalingOptLeaveCallback);
        }

        private void Btn_Signaling_Invite_Click(object sender, EventArgs e)
        {
            NIMSignalingInviteParam param = new NIMSignalingInviteParam();
            param.account_id_ = tbToAccount.Text;
            param.channel_id_ = tbChannelId.Text ;
            param.custom_info_ = tbOptExt.Text;
            param.request_id_ = tbInviteId.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.push_info_.need_badge_ = cbNotReadCount.Checked;
            param.push_info_.need_push_ = cbOpenPush.Checked;
            param.push_info_.push_content_ = tbPushContent.Text;
            param.push_info_.push_payload_ = tbPayLoad.Text;
            param.push_info_.push_title_ = tbPushTitle.Text;
            
            NIMSignalingAPI.Invite(param, NimSignalingOptInviteCallback);
        }

        private void Btn_Signaling_Cancel_Invite_Click(object sender, EventArgs e)
        {
            NIMSignalingCancelInviteParam param = new NIMSignalingCancelInviteParam();
            param.account_id_ = tbToAccount.Text;
            param.channel_id_ = tbChannelId.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.request_id_ = tbInviteId.Text;
            param.custom_info_ = tbOptExt.Text;

            NIMSignalingAPI.CancelInvite(param, NimSignalingOptCancelInviteHandler);
        }

        private void Btn_Signaling_Accept_Click(object sender, EventArgs e)
        {
            NIMSignalingAcceptParam param = new NIMSignalingAcceptParam();
            param.accept_custom_info_ = tbOptExt.Text;
            param.account_id_ = tbToAccount.Text;
            param.auto_join_ = cbAutoJoinUid.Checked;
            param.channel_id_ = tbChannelId.Text;
            param.join_custom_info_ = btReserveExt.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.request_id_ = tbInviteId.Text;
            if (!string.IsNullOrEmpty(tbUid.Text))
            {
                param.uid_ = Convert.ToInt64(tbUid.Text);
            }

            
            NIMSignalingAPI.Accept(param, NimSignalingOptAcceptCallback);
        }

        private void Btn_Signaling_Reject_Click(object sender, EventArgs e)
        {
            NIMSignalingRejectParam param = new NIMSignalingRejectParam();
            param.account_id_ = tbToAccount.Text;
            param.channel_id_ = tbChannelId.Text;
            param.custom_info_ = tbOptExt.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.request_id_ = tbInviteId.Text;
            
            NIMSignalingAPI.Reject(param, NimSignalingOptRejectCallback);
        }

        private void Btn_Signaling_Control_Click(object sender, EventArgs e)
        {
            NIMSignalingControlParam param = new NIMSignalingControlParam();
            param.account_id_ = tbToAccount.Text;
            param.channel_id_ = tbChannelId.Text;
            param.custom_info_ = tbOptExt.Text;
            
            NIMSignalingAPI.Control(param, NimSignalingOptControlCallback);
        }

        private void Btn_Signaling_Call_Click(object sender, EventArgs e)
        {
            NIMSignalingCallParam param = new NIMSignalingCallParam();
            param.account_id_ = tbToAccount.Text;
            param.channel_ext_ = tbChannelExt.Text;
            param.channel_name_ = tbChannelName.Text;
            param.channel_type_ = GetChannelType();
            param.custom_info_ = tbOptExt.Text;
            param.request_id_ = tbInviteId.Text;
            param.offline_enabled_ = cbOfflineSupport.Checked;
            param.push_info_ = new NIMSignalingPushInfo();
            param.push_info_.need_push_ = cbOpenPush.Checked;
            param.push_info_.push_content_ = tbPushContent.Text;
            param.push_info_.push_payload_ = tbPayLoad.Text;
            param.push_info_.push_title_ = tbPushTitle.Text;
            param.push_info_.need_badge_ = cbNotReadCount.Checked;
            if (!string.IsNullOrEmpty(tbUid.Text))
            {
                param.uid_ = Convert.ToInt64(tbUid.Text);
            }


            NIMSignalingAPI.Call(param, NimSignalingOptCallCallback);
        }

        private NIMSignalingType GetChannelType()
        {
            NIMSignalingType type = NIMSignalingType.kNIMSignalingTypeAudio;
            switch (cbChannelType.SelectedIndex)
            {
                case 0:
                    type = NIMSignalingType.kNIMSignalingTypeAudio;
                    break;
                case 1:
                    type = NIMSignalingType.kNIMSignalingTypeVideo;
                    break;
                case 2:
                    type = NIMSignalingType.kNIMSignalingTypeCustom;
                    break;
            }
            return type;
        }

        private void SignalingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
