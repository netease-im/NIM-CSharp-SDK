using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIMDemo;

namespace NIMDemo
{
    class RtsHandler
    {
        Form _form;

        public RtsHandler(Form form)
        {
            _form = form;
            RegisterRtsCallback();
        }
        void RegisterRtsCallback()
        {
            NIM.RtsAPI.SetStartNotifyCallback(OnReceiveSessionRequest);
        }

        void OnReceiveSessionRequest(string sessionId, int channelType, string uid, string custom)
        {
            InvokeOnForm(() =>
            {
                VideoChatForm vchatForm = VideoChatForm.GetInstance();
                RtsForm rtsForm = RtsForm.GetInstance();
                //当前不存在白板会话和音视频会话，才能开启新的会话
                if (rtsForm.RtsState == RtsFormState.kRtsInit && vchatForm.VchatInfo.state == VChatState.kVChatUnknow)
                {
                    rtsForm.Show();
                    rtsForm.RtsState = RtsFormState.kRtsNotify;
                    rtsForm.SetRtsInfo(sessionId, uid, (NIM.NIMRts.NIMRtsChannelType)channelType);
                }
                else
                {
                    NIM.RtsAPI.Ack(sessionId, (NIM.NIMRts.NIMRtsChannelType)channelType, false, null, null);
                }
               
            });
        }



        void ReceiveData(string sessionId, int channelType, string uid, IntPtr data, int size)
        {
            
        }

        void NotifyHangup(string sessionId, string uid)
        {
            
        }

        void InvokeOnForm(Action action)
        {
            _form.BeginInvoke(action);
        }
    }
}
