using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
namespace NIMDemo
{
	public partial class MultiVChatForm : Form
	{
  
        private Graphics _multiVChatFormGraphics_pb01;
        private Graphics _multiVChatFormGraphics_pb02;
        private Graphics _multiVChatFormGraphics_pb03;
        private Graphics _multiVChatFormGraphics_pb04;
        private Dictionary<string, Graphics> _multichatlist = new Dictionary<string, Graphics>();//大小为4.
      

        static string kNIMDeviceDataUid = "uid"; 			    /**< 用户id int64 */
        static string kNIMDeviceDataAccount = "account";		/**< 用户账号 string */

        private List<string> _audioblacklist=new List<string>();//音频黑名单
        private List<string> _vedioblacklist=new List<string>();//视频黑名单

        NIMVChatOptHandler _audiosetblacklistop = null;
        NIMVChatOptHandler _vediosetblacklistop = null;

        private string _roomId;
     

        private ContextMenu cm = null;
        private MenuItem cm_item1 = null;
        private MenuItem cm_item2 = null;

		public MultiVChatForm(string roomId)
		{
			InitializeComponent();
            this._roomId = roomId;
       
            cm = new System.Windows.Forms.ContextMenu();

            this.Text += "房间号:" + roomId;
            rtb_multichat_info.Text += "欢迎进入房间\n";
            this.tb_roomid.Text = roomId;
            this.Load += MultiVChatForm_Load;
            this.FormClosed += MultiVChatForm_FormClosed;
            MultimediaHandler.GetInstance().PeopleStatusHandler += OnMultiChatStatus;
            this.lv_members.Items.Add(NIMDemo.Helper.UserHelper.SelfId, NIMDemo.Helper.UserHelper.SelfId, 0);
           
            
		}

        void AudioSetBlackListOP(bool ret, int code,string json_extension)
        {
            if(ret)
            {
                Action action = () =>
                {
                    MessageBox.Show("操作成功");
                };
                this.BeginInvoke(action);
            }

        }
        void OnMultiChatStatus(object sender, PeopleStatusEventAgrs args)
        {
            Action action = () =>
            {
                if (args.status == 0)
                {
                    lv_members.Items.Add(args.uid, args.uid, 0);
                    rtb_multichat_info.Text += args.uid + "进入房间\n";
                }
                else
                {
                    lv_members.Items.RemoveByKey(args.uid);
                    rtb_multichat_info.Text += args.uid + "离开房间\n";
                }
                    
             
            };
            this.BeginInvoke(action);
        }

        void MultiVChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetVChatCallbackNull();
            EndDevices();
           
            NIM.VChatAPI.End();

            _multiVChatFormGraphics_pb01.Dispose();
            _multiVChatFormGraphics_pb02.Dispose();
            _multiVChatFormGraphics_pb03.Dispose();
            _multiVChatFormGraphics_pb04.Dispose();

           
        }

        void SetVChatCallback()
        {
            //注册音频接收回调
            DeviceAPI.SetAudioReceiveDataCb(AudioDataReceiveCallBack, null);
            //注册视频接收回调
            DeviceAPI.SetVideoReceiveDataCb(VideoDataReceiveCallBack, null);
            //注册视频捕获回调
            DeviceAPI.SetVideoCaptureDataCb(VideoDataCaptureCallBack, null);

            _audiosetblacklistop = new NIMVChatOptHandler(AudioSetBlackListOP);
        }

        void SetVChatCallbackNull()
        {
            //注册音频接收回调
            DeviceAPI.SetAudioReceiveDataCb(null, null);
            //注册视频接收回调
            DeviceAPI.SetVideoReceiveDataCb(null, null);
            //注册视频捕获回调
            DeviceAPI.SetVideoCaptureDataCb(null, null);
        }


        void MultiVChatForm_Load(object sender, EventArgs e)
        {
  
            _multiVChatFormGraphics_pb01 = pb_multivchat_01.CreateGraphics();
            _multiVChatFormGraphics_pb02 = pb_multivchat_02.CreateGraphics();
            _multiVChatFormGraphics_pb03 = pb_multivchat_03.CreateGraphics();
            _multiVChatFormGraphics_pb04 = pb_multivchat_04.CreateGraphics();

            SetVChatCallback();

        }

       


        void ShowVideoFrame(Graphics graphics, int w, int h, VideoFrame frame)
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.Default;
            var stream = frame.GetBmpStream();
            Bitmap img = new Bitmap(stream);
            //等比例缩放
            float sa = (float)w / frame.Width;
            float sb = (float)h / frame.Height;
            var scale = Math.Min(sa, sb);
            var newWidth = frame.Width * scale;
            var newHeight = frame.Height * scale;
            stream.Dispose();
            graphics.DrawImage(img, new Rectangle((int)(w - newWidth) / 2, (int)(h - newHeight) / 2, (int)newWidth, (int)newHeight),
                new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            img.Dispose();
        }

        private void StartVChat()
        {

        }

        private void btn_videosetting_Click(object sender, System.EventArgs e)
        {
            AVChat.AVDevicesSettingForm form = new AVChat.AVDevicesSettingForm();
            form.ShowDialog();
        }

   

        private void btn_create_multiroom_Click(object sender, System.EventArgs e)
        {

        }

        //收到视频数据回调
        private void VideoDataReceiveCallBack(UInt64 time, IntPtr data, UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
            VideoFrame frame = new VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
            string account=ParseCbJsonExtension(json_extension);

            if (_multichatlist.Keys.Contains(account))
            {
                Debug.Assert(_multichatlist[account] != null);
                Action action = () =>
                {
                    ShowVideoFrame(_multichatlist[account], pb_multivchat_02.Width, pb_multivchat_02.Height, frame);
                };
                this.Invoke(action);
               
            }
            else  
            {
                if(_multichatlist.Count<4)
                {
                    Graphics g = null;
                    //if (!_multichatlist.Values.Contains(_multiVChatFormGraphics_pb01))
                    //{
                    //    g = _multiVChatFormGraphics_pb01;
                    //}
                    //else
                        if(!_multichatlist.Values.Contains(_multiVChatFormGraphics_pb02))
                    {
                        g = _multiVChatFormGraphics_pb02;
                    }
                    else if(!_multichatlist.Values.Contains(_multiVChatFormGraphics_pb03))
                    {
                        g = _multiVChatFormGraphics_pb03;
                    }
                    else if(!_multichatlist.Values.Contains(_multiVChatFormGraphics_pb04))
                    {
                        g = _multiVChatFormGraphics_pb04;
                    }
                    if (g != null)
                    {
                        _multichatlist.Add(account, g);
                        Action action = () =>
                            {
                                ShowVideoFrame(_multichatlist[account], pb_multivchat_02.Width, pb_multivchat_02.Height, frame);
                            };
                        this.Invoke(action);
                     
                    }
                }
            }
        }

        //收到音频数据回调
        private void AudioDataReceiveCallBack(UInt64 time, IntPtr data, UInt32 size, Int32 rate)
        {

        }

        //捕获视频回调
        private void VideoDataCaptureCallBack(UInt64 time, IntPtr data,UInt32 size, UInt32 width, UInt32 height, string json_extension)
        {
           
            Action action = () =>
            {
                VideoFrame frame = new VideoFrame(data, (int)width, (int)height, (int)size, (long)time);
                ShowVideoFrame(_multiVChatFormGraphics_pb01, pb_multivchat_01.Width, pb_multivchat_01.Height, frame);
            };
            this.Invoke(action);
           
        }


        private string ParseCbJsonExtension(string jsonContent)
        {
            JObject jObj = JObject.Parse(jsonContent);
            var uidToken = jObj.SelectToken(kNIMDeviceDataUid);
            var accountToken = jObj.SelectToken(kNIMDeviceDataAccount);
            if (uidToken == null || accountToken == null)
                return null;

            string account = accountToken.ToObject<string>();
          
            return account;
        }

        void StartDevices()
        {
            NIM.StartDeviceResultHandler handle = (type, ret) =>
            {

            };
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0,null, handle);//开启麦克风
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0,null, handle);//开启扬声器播放对方语音
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo, "", 0,null, handle);//开启摄像头
        }
        public void EndDevices()
        {
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
        }
        private static string select_id_ = "";
        private void lv_members_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lv_members.SelectedItems.Count > 0)
                {
                    select_id_ = lv_members.SelectedItems[0].Text;
                    if (select_id_.Equals(NIMDemo.Helper.UserHelper.SelfId))
                        return;
                    bool isBlacklist = _audioblacklist.Contains(select_id_);
                    if(cm_item1==null)
                    {
                        cm_item1 = new MenuItem();
                        cm.MenuItems.Add(cm_item1);
                        _audiosetblacklistop = new NIMVChatOptHandler(
                             (ret, code, json_extension) =>
                                {
                                    if (ret)
                                    {
                                        bool blackmember = _audioblacklist.Contains(select_id_);
                                        if (!blackmember)
                                        {
                                            Action action = () =>
                                            {
                                                rtb_multichat_info.Text += select_id_ + "加入音频黑名单成功\n";
                                                _audioblacklist.Add(select_id_);
                                            };
                                            this.BeginInvoke(action);
                                        }
                                        else
                                        {
                                            Action action = () =>
                                            {
                                                rtb_multichat_info.Text+= select_id_ + "取消音频黑名单成功\n";
                                                _audioblacklist.Remove(select_id_);

                                            };
                                            this.BeginInvoke(action);
                                        }
                                    }
                                    else
                                    {

                                    }

                                }
                            );
                        cm_item1.Click += (o, ex) =>
                        {
                            isBlacklist = _audioblacklist.Contains(select_id_);
                            VChatAPI.SetMemberInBlackList(select_id_, !isBlacklist, true, "",
                               _audiosetblacklistop);
                        };
                    }

                    cm_item1.Text= isBlacklist ? "取消音频黑名单" : "加入音频黑名单";
                   
                    bool muted = _vedioblacklist.Contains(select_id_);
                    if(cm_item2==null)
                    {
                        cm_item2 = new MenuItem();
                        cm.MenuItems.Add(cm_item2);
                        _vediosetblacklistop = new NIMVChatOptHandler(
                            (ret, code, json_extension) =>
                              {
                                  if (ret)
                                  {
                                      bool blackmember = _vedioblacklist.Contains(select_id_);
                                      if (!blackmember)
                                      {
                                          Action action = () =>
                                          {
                                              rtb_multichat_info.Text += select_id_ + "加入视频黑名单成功\n";
                                              _vedioblacklist.Add(select_id_);
                                          };
                                          this.BeginInvoke(action);
                                      }
                                      else
                                      {
                                          Action action = () =>
                                          {
                                              rtb_multichat_info.Text += select_id_ + "取消视频黑名单成功\n";
                                              _vedioblacklist.Remove(select_id_);
                                          };
                                          this.BeginInvoke(action);
                                      }
                                  }
                                  else
                                  {
                                      Action action = () =>
                                      {
                                          MessageBox.Show("操作失败");
                                          //_vedioblacklist.Remove(id);
                                      };
                                      this.BeginInvoke(action);
                                  }
                              }
                            
                            );
                        cm_item2.Click += (o, ex) =>
                        {
                            muted = _vedioblacklist.Contains(select_id_);
                            VChatAPI.SetMemberInBlackList(select_id_, !muted, false, "",
                              _vediosetblacklistop);
                        };
                    }

                    cm_item2.Text = muted ? "取消视频黑名单" : "加入视频黑名单";

                    cm.Show(lv_members, e.Location);
                }
            }
        }

	}
}
