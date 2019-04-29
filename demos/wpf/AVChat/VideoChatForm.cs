using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;
using System.Runtime.InteropServices;
using NIMDemo.Helper;
using System.Threading;

namespace NIMDemo
{
    public partial class VideoChatForm : Form
    {
        private static VideoChatForm videoChatForm_ = null;

        private NIMVChatMp4RecordOptHandler _startcb = null;
        private NIMVChatMp4RecordOptHandler _stopcb = null;
        private NIMVChatAudioRecordOptHandler _start_audio_record_cb = null;
        private NIMVChatAudioRecordOptHandler _stop_audio_record_cb = null;
        private NIMVChatOptHandler _setvideoqualitycb = null;
        private NIMVChatOptHandler _set_custom_videocb = null;

        private bool accompany_ = false;//是否已开启伴奏
		private bool beauty_ = false;//是否已开启美颜
		private bool mute = false;
		private bool record = false;
		private bool audio_record = false;
       
       
        private VideoChatInfo vchat_info = new VideoChatInfo();
        private Graphics _peerRegionGraphics;
        private Graphics _mineRegionGraphics;

        private System.Timers.Timer sendCaptureScreenDataTimer_ = new System.Timers.Timer();

        public VideoChatInfo VchatInfo
        {
            get
            {
                return vchat_info;
            }
            set
            {
                vchat_info = value;
                ChangeVchatState(vchat_info.state);
            }
        }

        public static VideoChatForm GetInstance()
        {
            if(videoChatForm_==null)
            {
                videoChatForm_ = new VideoChatForm();
            }
            return videoChatForm_;
        }

        

        private VideoChatForm()
        {
            InitializeComponent();
			InitQuality();
			InitClipTypes();
			_startcb = new NIMVChatMp4RecordOptHandler(VChatRecordStartCallback);
            _start_audio_record_cb = new NIMVChatAudioRecordOptHandler(VChatAudioRecordCallback);
            _stop_audio_record_cb = new NIMVChatAudioRecordOptHandler(VChatAudioRecordCallback);

            this.Load += VideoChatForm_Load;
            this.FormClosed += VideoChatForm_FormClosed;

            sendCaptureScreenDataTimer_.Interval = 100;
            sendCaptureScreenDataTimer_.Elapsed += SendCustomVideoTick;
        }

		private void InitQuality()
		{
			foreach(var quality in Enum.GetValues(typeof(NIMVChatVideoQuality)))
			{
				cb_setquality.Items.Add(quality);				
			}
			if (cb_setquality.Items.Count > 0)
				cb_setquality.SelectedIndex = 0;
		}

		private void InitClipTypes()
		{
			foreach (var quality in Enum.GetValues(typeof(NIMVChatVideoFrameScaleType)))
			{
				cb_video_clip_.Items.Add(quality);
			}
			if (cb_video_clip_.Items.Count > 0)
				cb_video_clip_.SelectedIndex = 0;
		}
		private void VChatRecordStartCallback(bool ret, int code,string file,Int64 time,string json_extension)
		{
            Action action = () =>
            {
                if (ret)
                {
                    MessageBox.Show("开始录制MP4");
                }
                else
                {
                    MessageBox.Show("录制Mp4失败-错误码:" + code.ToString());
                }
            };
            this.BeginInvoke(action);
		}

        private void VideoChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sendCaptureScreenDataTimer_.Stop();
            switch(vchat_info.state)
            {
                case VChatState.kVChatNotify:
                    {
                        NIM.VChatAPI.CalleeAck(vchat_info.channel_id, false, null);
                    }
                    break;
                case VChatState.kVChatInvite:
                case VChatState.kVChating:
                    NIM.VChatAPI.End();
                    break;
                case VChatState.VChatEnd:
                    break;
            }
            
            {
                MultimediaHandler.ReceiveVideoFrameHandler -= OnReceivedVideoFrame;
                MultimediaHandler.CapturedVideoFrameHandler -= OnCapturedVideoFrame;
            }
           
            _peerRegionGraphics.Dispose();
            _mineRegionGraphics.Dispose();
            videoChatForm_ = null;
            vchat_info.channel_id = 0;
           // _multimediaHandler = null;
        }

        private void VideoChatForm_Load(object sender, EventArgs e)
        {
            _peerRegionGraphics = peerPicBox.CreateGraphics();
            _mineRegionGraphics = minePicBox.CreateGraphics();
            //if (_multimediaHandler != null)
            {
                MultimediaHandler.ReceiveVideoFrameHandler += OnReceivedVideoFrame;
                MultimediaHandler.CapturedVideoFrameHandler += OnCapturedVideoFrame;
            }
        }

        private void OnCapturedVideoFrame(object sender, VideoEventAgrs e)
        {
             //如果开启了美颜
			if(beauty_)
			{
                uint size=Convert.ToUInt32(e.Frame.Width*e.Frame.Height*3/2);
                //处理数据
                byte[] i420=NIMDemo.LivingStreamSDK.YUVHelper.ARGBToI420(e.Frame.Data,e.Frame.Width,e.Frame.Height);
                Beauty.Smooth.smooth_process(i420,e.Frame.Width,e.Frame.Height,10,0,200);
                e.Frame.Data = NIMDemo.LivingStreamSDK.YUVHelper.I420ToARGB(i420, e.Frame.Width, e.Frame.Height);
                try
                {
                    //发送自定义数据
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    ulong time = Convert.ToUInt64(ts.TotalMilliseconds);
                    NIMDemo.LivingStreamSDK.YUVHelper.i420Revert(ref i420,e.Frame.Width,e.Frame.Height);
                    IntPtr unmanagedPointer = Marshal.AllocHGlobal(i420.Length);
                    Marshal.Copy(i420, 0, unmanagedPointer, i420.Length);
                    NIM.DeviceAPI.CustomVideoData(time, unmanagedPointer, size, (uint)e.Frame.Width, (uint)e.Frame.Height,null);
                    Marshal.FreeHGlobal(unmanagedPointer);
                }
                catch(Exception ex)
                {

                }
                //本地显示数据 
                ShowVideoFrame(_mineRegionGraphics, minePicBox.Width, minePicBox.Height, e.Frame);
			}
            else
            { 
                ShowVideoFrame(_mineRegionGraphics, minePicBox.Width, minePicBox.Height, e.Frame);
            }
        }

        private void OnReceivedVideoFrame(object sender,VideoEventAgrs args)
        {
            ShowVideoFrame(_peerRegionGraphics, peerPicBox.Width, peerPicBox.Height, args.Frame);
        }

        void ShowVideoFrame(Graphics graphics,int w,int h, VideoFrame frame)
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.Default;
            var stream = frame.GetBmpStream();
            Bitmap img = new Bitmap(stream);
            //等比例缩放
            float sa = (float)w / frame.Width;
            float sb = (float)h / frame.Height;
            var scale = Math.Min(sa, sb);
            var newWidth = frame.Width*scale;
            var newHeight = frame.Height*scale;
            
            graphics.DrawImage(img, new Rectangle((int)(w-newWidth)/2, (int)(h-newHeight)/2, (int)newWidth,(int)newHeight), 
                new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
			img.Dispose();
			stream.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void btnRecord_Click(object sender, EventArgs e)
		{
			Random random = new Random();
			string path = Application.StartupPath + @"\" + random.Next().ToString() + @".mp4";
			string json_extension = "";
			record = !record;
			if(record)
			{
				btnRecord.Text = "停止MP4录制";
				NIMVChatMP4RecordJsonEx recordInfo = new NIMVChatMP4RecordJsonEx();
				recordInfo.RecordPeopleType = 1;//混录
				NIM.VChatAPI.StartRecord(path, recordInfo, _startcb);
			}
			else
			{
				btnRecord.Text = "开始MP4录制";
				NIM.VChatAPI.StopRecord(null, _stopcb);
			}
			
		}

		private void btnSetMute_Click_1(object sender, EventArgs e)
		{
			if (mute)
				btnSetMute.Text = "取消静音";
			else
				btnSetMute.Text = "设置静音";
			NIM.VChatAPI.SetAudioMute(mute);
			mute = !mute;
		}

        private void bt_setting_Click(object sender, EventArgs e)
        {
            AVChat.AVDevicesSettingForm form = new AVChat.AVDevicesSettingForm();
            form.ShowDialog();
        }

		private void cb_setquality_SelectedIndexChanged(object sender, EventArgs e)
		{
			_setvideoqualitycb = new NIMVChatOptHandler((ret, code, json) =>
			{
				//ret  true
				//设置成功
			});
			NIMVChatVideoQuality quality =(NIMVChatVideoQuality)((ComboBox)sender).SelectedItem;
			NIM.VChatAPI.SetVideoQuality(quality, "", _setvideoqualitycb);
		}

		private void btn_beauty_Click(object sender, EventArgs e)
		{
            if (_set_custom_videocb == null)
            {
                _set_custom_videocb = new NIMVChatOptHandler((ret, code, json) =>
                {
                    if (ret)
                    {
                        beauty_ = !beauty_;
                        Action action = () =>
                        {
                            if (beauty_)
                                btn_beauty.Text = "美颜(关)";
                            else
                                btn_beauty.Text = "美颜(开)";
                        };
                        this.Invoke(action);

                    }
                    //ret  true
                    //设置成功
                });
            }
            NIM.VChatAPI.SetCustomData(false, !beauty_, "", _set_custom_videocb);     
		}

        private void btn_accompany_Click(object sender, EventArgs e)
        {
#if WIN32
            StartDeviceResultHandler cb = (type, ret) =>
            {
                if (ret)
                {
                    accompany_ = !accompany_;
                    Action action = () =>
                    {
                        if (accompany_)
                            btn_accompany.Text = "伴奏(关)";
                    };
                    this.Invoke(action);
                }
            };
            if (!accompany_)
                NIM.DeviceAPI.StartDevice(NIMDeviceType.kNIMDeviceTypeAudioHook, tb_player_path_.Text, 0,null,cb);
            else
            {
                NIM.DeviceAPI.EndDevice(NIMDeviceType.kNIMDeviceTypeAudioHook);
                btn_accompany.Text = "伴奏(开)";
            }
#elif WIN64
            MessageBox.Show("目前64位没有伴奏功能");
#endif
            }

     

        private void cb_aec_CheckedChanged(object sender, EventArgs e)
        {
            NIM.DeviceAPI.SetAudioProcessInfo(cb_aec.Checked, cb_ns.Checked, cb_vid.Checked);

        }

        private void cb_ns_CheckedChanged(object sender, EventArgs e)
        {
            NIM.DeviceAPI.SetAudioProcessInfo(cb_aec.Checked, cb_ns.Checked, cb_vid.Checked);
        }

        private void cb_vid_CheckedChanged(object sender, EventArgs e)
        {
            NIM.DeviceAPI.SetAudioProcessInfo(cb_aec.Checked, cb_ns.Checked, cb_vid.Checked);
        }

		private void btnRecordAudio_Click(object sender, EventArgs e)
		{
			Random random = new Random();
			string path = Application.StartupPath + @"\" + random.Next().ToString() + @".aac";
			string json_extension = "";
			audio_record = !audio_record;
			if (audio_record)
			{
				
				btnRecordAudio.Text = "停止录音";
				NIM.VChatAPI.StartAudioRecord(path, _start_audio_record_cb);
			}
			else
			{

				btnRecordAudio.Text = "录制音频";
				NIM.VChatAPI.StopAudioRecord(_start_audio_record_cb);
			}
		}

		private void VChatAudioRecordCallback(bool ret, int code, string file, Int64 time, string json_extension)
		{
			if (ret)
			{
				if (audio_record)
					MessageBox.Show("开始录制");
				else
					MessageBox.Show("结束录制");
			}
			else
			{
				if (audio_record)
					MessageBox.Show("开始录制操作失败-错误码:" + code.ToString());
				else
					MessageBox.Show("结束录制操作失败-错误码:" + code.ToString());

			}
		}

		private void cb_video_clip_SelectedIndexChanged(object sender, EventArgs e)
		{
			NIMVChatVideoFrameScaleType scale_type = (NIMVChatVideoFrameScaleType)((ComboBox)sender).SelectedItem;
			NIM.VChatAPI.SetVideoFrameScale(scale_type);
		}

        private void BtnScreenCaptureClick(object sender, EventArgs e)
        {
          
            if (sendCaptureScreenDataTimer_.Enabled)
            {
                NIM.VChatAPI.SetCustomData(false, false, "", null);
                sendCaptureScreenDataTimer_.Stop();
            }
            else
            {
                NIM.VChatAPI.SetCustomData(false, true, "", null);
                sendCaptureScreenDataTimer_.Start();
            }
        }

        private void SendCustomVideoTick(object sender, EventArgs e)
        {
            int width = 0;
            int height = 0;
            int size = 0;
            byte[] screen_datas = ScreenCaptureHelper.GetScreenCapture(out width, out height);
            size = width * height * 3;
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(screen_datas.Length);
            Marshal.Copy(screen_datas, 0, unmanagedPointer, screen_datas.Length);
            NIMCustomVideoDataInfo info = new NIMCustomVideoDataInfo();
            info.VideoSubType = Convert.ToInt32(NIMVideoSubType.kNIMVideoSubTypeRGB);
            NIM.DeviceAPI.CustomVideoData(0, unmanagedPointer, Convert.ToUInt32(size), (uint)width, (uint)height, info);
            Marshal.FreeHGlobal(unmanagedPointer);
        }

        private void BtnAcceptClick(object sender, EventArgs e)
        {
            NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
            NIM.VChatAPI.CalleeAck(vchat_info.channel_id, true, info);
        }

        private void BtnRefuseClick(object sender, EventArgs e)
        {
            NIM.NIMVChatInfo info = new NIM.NIMVChatInfo();
            NIM.VChatAPI.CalleeAck(vchat_info.channel_id, false, info);
            Close();
        }

        private void ChangeVchatState(VChatState state)
        {
            switch(state)
            {
                case VChatState.kVChatInvite:
                    {
                        panel_invite.Visible = true;
                        panel_notify.Visible = false;
                        panel_chating.Visible = false;
                        panel_end.Visible = false;
                        lb_invite_info.Text = "正在呼叫" + vchat_info.uid + " ，请稍等";
                        btn_invite_cancel.Enabled = true;
                        CallFriend(vchat_info.uid);
                    }
                    break;
                case VChatState.kVChatInviteRefuse:
                    {
                        panel_invite.Visible = true;
                        panel_notify.Visible = false;
                        panel_chating.Visible = false;
                        panel_end.Visible = false;
                        lb_invite_info.Text = "对方已拒绝";
                        btn_invite_cancel.Enabled = false;
                        TimerCloseForm();
                    }
                    break;
                case VChatState.kVChatNotify:
                    {
                        panel_invite.Visible = false;
                        panel_notify.Visible = true;
                        panel_chating.Visible = false;
                        panel_end.Visible = false;
                        lb_notify_info.Text = vchat_info.uid +
                            (vchat_info.chat_mode == NIMVideoChatMode.kNIMVideoChatModeAudio ? "向你发来音频通话" : "向你发来视频通话");
                    }
                    break;
                case VChatState.kVChating:
                    {
                        panel_invite.Visible = false;
                        panel_notify.Visible = false;
                        panel_chating.Visible = true;
                        panel_end.Visible = false;
                    }
                    break;
                case VChatState.VChatEnd:
                    {
                        panel_invite.Visible = false;
                        panel_notify.Visible = false;
                        panel_chating.Visible = false;
                        panel_end.Visible = true;
                        lb_end_info.Text = "对方已挂断";
                        TimerCloseForm();
                    }
                    break;
            }
        }

        /// <summary>
        /// 定时关闭窗口
        /// </summary>
        private void TimerCloseForm()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000; //5s后关闭窗口
            timer.Elapsed += TimerClose;
            timer.Start();
        }


        private void TimerClose(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            Close();
            timer.Stop();
        }

        private void BtnInviteCancelClick(object sender, EventArgs e)
        {
            lb_invite_info.Text = "正在取消中....";
            NIM.VChatAPI.End();
            TimerCloseForm();
        }

        private void CallFriend(string peerId)
        {
            NIMVChatInfo info = new NIMVChatInfo();
            info.Uids = new System.Collections.Generic.List<string>();
            info.Uids.Add(peerId);
            VChatAPI.Start(NIMVideoChatMode.kNIMVideoChatModeVideo, "C# demo呼叫", info);//邀请test_id进行语音通话
        }
    }



    public enum VChatState
    {
        kVChatUnknow = 0,
        kVChatInvite,
        kVChatInviteRefuse,
        kVChatNotify,
        kVChating,
        VChatEnd,
    }

    public class VideoChatInfo
    {
        public VChatState state;
        public Int64 channel_id;
        public string uid;
        public NIMVideoChatMode chat_mode;

        public VideoChatInfo()
        {
            state = VChatState.kVChatUnknow;
            channel_id = 0;
            uid = "";
            chat_mode = NIMVideoChatMode.kNIMVideoChatModeAudio;
        }
    }
}
