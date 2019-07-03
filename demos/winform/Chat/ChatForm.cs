using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NIM;
using System.Runtime.InteropServices;

namespace NIMDemo
{
    public partial class ChatForm : Form
    {
        private readonly string _peerId = null;
        private readonly NIM.Session.NIMSessionType _sessionType;
        private bool _fileDroped = false;
        private NIMVChatOpt2Handler _createroomcb = null;
        private NIMVChatOpt2Handler _joinroomcb = null;
        string room_name = "154145";
        private string _lastSendedMsgId = null;
        private bool _isRobot = false;

        private ChatForm()
        {
            InitializeComponent();
            NIM.TalkAPI.OnReceiveMessageHandler += ReceiveMessageHandler;
            NIM.TalkAPI.OnSendMessageCompleted += SendMessageResultHandler;
            this.FormClosed += new FormClosedEventHandler(ChatForm_FormClosed);
            this.textBox1.DragEnter += TextBox1_DragEnter;
            this.textBox1.DragDrop += TextBox1_DragDrop;

            _createroomcb = new NIMVChatOpt2Handler(CreateMultiVChatRoomCallback);
            _joinroomcb = new NIMVChatOpt2Handler(JoinMultiVChatRoomCallback);
        }

        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            textBox1.Text = path;
            _fileDroped = true;
        }

        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        public ChatForm(string id, NIM.Session.NIMSessionType st = NIM.Session.NIMSessionType.kNIMSessionTypeP2P,bool isRobot = false)
            : this()
        {
            _peerId = id;
            room_name = "1554554";
            _sessionType = st;
            if (st != NIM.Session.NIMSessionType.kNIMSessionTypeP2P)
            {
                testMediaBtn.Visible = false;
                testRtsBtn.Visible = false;
            }
            _isRobot = isRobot;
            if (_isRobot)
            {
                base.Text = string.Format("与 机器人-{0} 聊天中", _peerId);
            }
            else
            {
                base.Text = string.Format("与 {0} 聊天中", _peerId);
            }
        }

        void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            NIM.TalkAPI.OnReceiveMessageHandler -= ReceiveMessageHandler;
            NIM.TalkAPI.OnSendMessageCompleted -= SendMessageResultHandler;
        }

        private void SendMessageToFriend(object sender, EventArgs e)
        {
            string message = this.textBox1.Text;

            if(_isRobot)
            {
                NIM.Robot.TextMessage msg = new NIM.Robot.TextMessage();
                msg.Content = message;
                msg.Text = message;
                NIM.Robot.NIMRobotAPI.SendMessage(_peerId, msg);
                return;
            } 

            if (_fileDroped)
            {
                _fileDroped = false;
                SendFile(message);
            }
            else
            {
                NIM.NIMTextMessage textMsg = new NIM.NIMTextMessage();
                textMsg.SessionType = _sessionType;
                textMsg.ReceiverID = _peerId;
                textMsg.TextContent = message;
                textMsg.PushContent = "云信测试消息--*--";
                if(textMsg.SessionType==NIM.Session.NIMSessionType.kNIMSessionTypeTeam)
                {
                    //群消息已读业务
                    textMsg.NeedTeamAck = 1;
                }
                NIM.TalkAPI.SendMessage(textMsg);
            }
            this.listBox1.Items.Add(message);
            this.textBox1.Text = string.Empty;
        }
        void SendFile(string path)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            string extension = System.IO.Path.GetExtension(path);

            if (IsImageFile(path))
            {
                NIM.NIMImageMessage imageMsg = new NIMImageMessage();
                imageMsg.LocalFilePath = path;
                imageMsg.ImageAttachment = new NIMImageAttachment();
                imageMsg.ImageAttachment.DisplayName = fileName;
                imageMsg.ImageAttachment.FileExtension = extension;
                imageMsg.ReceiverID = _peerId;
                imageMsg.SessionType = _sessionType;
                using (var i = Image.FromFile(path))
                {
                    imageMsg.ImageAttachment.Height = i.Height;
                    imageMsg.ImageAttachment.Width = i.Width;
                }
                TalkAPI.SendMessage(imageMsg);
            }
            else
            {
                NIM.NIMFileMessage fileMsg = new NIMFileMessage();
                fileMsg.LocalFilePath = path;
                fileMsg.FileAttachment = new NIMMessageAttachment();
                fileMsg.FileAttachment.DisplayName = fileName;
                fileMsg.FileAttachment.FileExtension = extension;
                fileMsg.ReceiverID = _peerId;
                fileMsg.SessionType = _sessionType;
                NIM.TalkAPI.SendMessage(fileMsg, (uploaded, total, obj) => 
                {
                    OutputForm.Instance.SetOutput(string.Format("upload file:{0} {1}/{2}",path,uploaded,total));
                });
            }
        }

        bool IsImageFile(string path)
        {
            int[] imageBytes = { 0x8950, 0xFFD8, 0x4D42 };
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var b1 = fs.ReadByte();
                var b2 = fs.ReadByte();
                var r = (b1 << 8) + b2;
                return imageBytes.Contains(r);
            }
        }

        void SendMessageResultHandler(object sender, MessageArcEventArgs args)
        {
            if(args.ArcInfo.Response == ResponseCode.kNIMResSuccess && args.ArcInfo.TalkId == _peerId)
            {
                _lastSendedMsgId = args.ArcInfo.MsgId;
            }
            if (args.ArcInfo.Response == ResponseCode.kNIMResSuccess || args.ArcInfo.TalkId != _peerId)
                return;
            
            Action action = () =>
            {
                MessageBox.Show(args.Dump(), "发送失败");
            };
            this.Invoke(action);
        }

        void ReceiveMessageHandler(object sender, NIM.NIMReceiveMessageEventArgs args)
        {
            if (args.Message.MessageContent.SessionType == NIM.Session.NIMSessionType.kNIMSessionTypeP2P && args.Message.MessageContent.SenderID != _peerId)
                return;
            if (args.Message.MessageContent.SessionType == NIM.Session.NIMSessionType.kNIMSessionTypeTeam && args.Message.MessageContent.ReceiverID != _peerId)
                return;
            //处理自定义消息
            if (args.Message != null && args.Message.MessageContent.MessageType == NIMMessageType.kNIMMessageTypeCustom)
            {
                var m = args.Message.MessageContent as NIMCustomMessage<object>;
                //CustomContent 是一个JObject对象，可以根据Extention字段或者其他方法来决定如何解析
                var jObj = m.CustomContent as JObject;
                if (m.Extention == "1")
                {
                    var x = jObj.ToObject<CustomMessageContent>();
                    if (x != null)
                    {

                    }
                }
            }
            if (args.Message != null && args.Message.MessageContent.MessageType == NIM.NIMMessageType.kNIMMessageTypeText)
            {
                Action action = () =>
                {
                    var m = args.Message.MessageContent as NIM.NIMTextMessage;
                    this.listBox1.Items.Add(new string(' ', 50) + m.TextContent);
                };
                this.Invoke(action);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VideoChatForm vchatForm = VideoChatForm.GetInstance();
            RtsForm rtsForm = RtsForm.GetInstance();
            //当前不存在白板会话和音视频会话，才能开启新的会话
            if(rtsForm.RtsState==RtsFormState.kRtsInit&& vchatForm.VchatInfo.state==VChatState.kVChatUnknow)
            {
                VideoChatInfo vchat_info = vchatForm.VchatInfo;
                vchat_info.state = VChatState.kVChatInvite;
                vchat_info.uid = _peerId;
                vchat_info.chat_mode = NIMVideoChatMode.kNIMVideoChatModeVideo;
                vchatForm.VchatInfo = vchat_info;
                vchatForm.Show();
            }
            else
            {
                if (rtsForm.RtsState != RtsFormState.kRtsInit)
                {
                    rtsForm.Activate();
                }
                else if (vchatForm.VchatInfo.state != VChatState.kVChatUnknow)
                {
                    vchatForm.Activate();
                }
            }
        }

        private void QueryMsglogBtn_Click(object sender, EventArgs e)
        {
            //var f = new InfomationForm();
            //f.Load += (s, ex) =>
            //{
            //    NIM.Messagelog.MessagelogAPI.QueryMsglogOnline(_peerId, 
            //        _sessionType, 100, 0, 0, 0, false, false,
            //        (a, b, c, d) =>
            //        {
            //            var x = d.Dump();
            //            f.ShowMessage(x);
            //        });
            //};
            //f.Show();
            MessageLogForm form = new MessageLogForm();
            form.Show();
        }

        private void CreateMultiVChatRoomCallback(int code, long channel_id, string json_extension)
		{
            if (code == 200)
            {
				NIM.NIMJoinRoomJsonEx joinRoomJsonEx = new NIMJoinRoomJsonEx();
                CustomLayout layout_json=new CustomLayout();
                layout_json.Hostarea = new HostArea();
                layout_json.Background = new BackGround();
                joinRoomJsonEx.Layout = layout_json.Serialize();
                //创建房间成功,将内容抛至UI线程  
                Action action = () =>
                    {
						if (VChatAPI.JoinRoom(NIMVideoChatMode.kNIMVideoChatModeVideo, room_name, joinRoomJsonEx, _joinroomcb))
						{
							//调用成功
						}
						else
						{
							string info = String.Format("加入房间-{0}-失败", room_name);
							MessageBox.Show(info);
						}

					};
                    this.Invoke(action);
            }
            else
            {
                Action action = () =>
                {
                    MessageBox.Show("创建房间失败-错误码：" + code.ToString());
                };
                this.BeginInvoke(action);

            }
		}

		private void JoinMultiVChatRoomCallback(int code, Int64 channel_id, string json_extension)
		{
			if (code==200)
			{
                System.Diagnostics.Debug.WriteLine("进入房间：" + json_extension);
                //进入房间成功
                Action action = () =>
                {
                    MultiVChatForm vchat = new MultiVChatForm(room_name);
                    vchat.Show();
                };
                this.Invoke(action);
			}
			else
			{
                Action action = () =>
                {
                    MessageBox.Show("加入房间失败-错误码:" + code.ToString());
                };
                this.BeginInvoke(action);
				
			}
		}
        private void button3_Click(object sender, EventArgs e)
        {
            //System.Threading.ThreadPool.QueueUserWorkItem((obj) =>
            //{
                RtsTest();
            //});
        }

        public void RtsTest()
        {
            VideoChatForm vchatForm = VideoChatForm.GetInstance();
            RtsForm rtsForm = RtsForm.GetInstance();


            //当前不存在白板会话和音视频会话，才能开启新的会话
            if (rtsForm.RtsState == RtsFormState.kRtsInit && vchatForm.VchatInfo.state == VChatState.kVChatUnknow)
            {
                rtsForm.Show();
                rtsForm.SetRtsInfo("", _peerId, (NIM.NIMRts.NIMRtsChannelType.kNIMRtsChannelTypeTcp | NIM.NIMRts.NIMRtsChannelType.kNIMRtsChannelTypeVchat));
                rtsForm.RtsState = RtsFormState.kRtsInvite;
            }
            else
            {
                if(rtsForm.RtsState!=RtsFormState.kRtsInit)
                {
                    rtsForm.Activate();
                }
                if(vchatForm.VchatInfo.state != VChatState.kVChatUnknow)
                {
                    vchatForm.Activate();
                }
            }
            //rtsForm.SetRtsInfo

        }

        private void SysMsgBtn_Click(object sender, EventArgs e)
        {
            NIM.SysMessage.NIMSysMessageContent content = new NIM.SysMessage.NIMSysMessageContent();
            content.ClientMsgId = Guid.NewGuid().ToString();
            content.ReceiverId = _peerId;
            if (_sessionType == NIM.Session.NIMSessionType.kNIMSessionTypeP2P)
                content.MsgType = NIM.SysMessage.NIMSysMsgType.kNIMSysMsgTypeCustomP2PMsg;
            else if (_sessionType == NIM.Session.NIMSessionType.kNIMSessionTypeTeam)
                content.MsgType = NIM.SysMessage.NIMSysMsgType.kNIMSysMsgTypeCustomTeamMsg;
            content.Attachment = textBox1.Text;
            NIM.SysMessage.SysMsgAPI.SendCustomMessage(content);

            //发送自定义消息
            //ImageTextMessage msg = new ImageTextMessage();
            //msg.CustomContent.Text = "这是一条图文消息:hello nim";
            //msg.LocalFilePath = @"C:\Users\鹏\Pictures\13349232980.jpg";
            //msg.ReceiverID = _peerId;
            //NIM.Nos.NosAPI.Upload(msg.LocalFilePath, (a, b) =>
            //{
            //    msg.CustomContent.ImagePath = b;
            //    NIM.TalkAPI.SendMessage(msg);
            //}, null);

            //var obj = new { name = "",count=123,abc="aaaaaaaaaa"};
            //var json = JsonConvert.SerializeObject(obj, Formatting.None);
            //NIMTipMessage tipMsg = new NIMTipMessage();
            //tipMsg.ReceiverID = _peerId;
            //tipMsg.TextContent = "this is a tip message";
            //tipMsg.Attachment = json;
            //NIM.TalkAPI.SendMessage(tipMsg);
        }

        private void SysmsgLogBtn_Click(object sender, EventArgs e)
        {
            NIM.SysMessage.SysMsgAPI.QueryMessage(100, 0, (r) =>
            {
                Action action = () =>
                {
                    InfomationForm f = new InfomationForm();
                    f.Load += (s, ex) =>
                    {
                        var x = r.Dump();
                        f.ShowMessage(x);
                    };
                    f.Show();
                };
                this.Invoke(action);
            });
        }

        void InvokeOnMainThread(Action action)
        {
            this.BeginInvoke(action);
        }

        private void btn_createmultiroom_Click(object sender, EventArgs e)
        {
            string custom_info = "你们好";
            //string json_extension = "";
            room_name = Guid.NewGuid().ToString("N");

			//
            VChatAPI.CreateRoom(room_name, custom_info, null, _createroomcb);
        }

        private void btn_joinmultiroom_Click(object sender, EventArgs e)
        {
            InputRoomIdForm form = new InputRoomIdForm();
            form.ShowDialog();
        }

        private void recallMsgBtn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_lastSendedMsgId))
            {
                NIM.TalkAPI.RecallMessage(_lastSendedMsgId,"撤回消息", OnRecallMessageCompleted);
            }
        }

        private void OnRecallMessageCompleted(ResponseCode result, RecallNotification[] notify)
        {
            DemoTrace.WriteLine("撤回消息:" + result.ToString()+" " + notify.Dump());
        }
    }

    class CustomMessageContent
    {
        [Newtonsoft.Json.JsonProperty("text")]
        public string Text { get; set; }

        [Newtonsoft.Json.JsonProperty("imagepath")]
        public string ImagePath { get; set; }
    }

    class ImageTextMessage : NIMCustomMessage<CustomMessageContent>
    {
        public override CustomMessageContent CustomContent { get; set; }

        public ImageTextMessage()
        {
            MessageType = NIMMessageType.kNIMMessageTypeCustom;
            CustomContent = new CustomMessageContent();
            Extention = "1";
        }
    }

   
}
