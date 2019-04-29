using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
    public partial class MsgInfoForm : Form
    {
        private MsgInfoForm()
        {
            InitializeComponent();
            viewAttachmentBtn.Enabled = false;
            downloadBtn.Enabled = false;
            this.Load += MsgInfoForm_Load;
        }
        private readonly NIM.NIMIMMessage _message = null;
        private NIM.NIMMessageAttachment _attachment = null;
        public MsgInfoForm(NIM.NIMIMMessage msg)
           : this()
        {
            _message = msg;
            InitFormContent();
        }

        private void MsgInfoForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = _message.Dump();
            CheckLocalResource();
            var activeForm = Form.ActiveForm;
            if (activeForm == null)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
                return;
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(activeForm.Location.X + activeForm.Size.Width + 20, activeForm.Location.Y + 100);
        }

        private string _lookupPath = null;
        void InitFormContent()
        {
            string uid = MainForm.SelfId;
            if (_message.MessageType == NIM.NIMMessageType.kNIMMessageTypeAudio)
            {
                var audioMsg = _message as NIM.NIMAudioMessage;
                _attachment = audioMsg.AudioAttachment;
                viewAttachmentBtn.Text = "播放";
                _lookupPath = NIM.ToolsAPI.GetUserSpecificAppDataDir(uid, NIM.NIMAppDataType.kNIMAppDataTypeAudio);
            }
            else if (_message.MessageType == NIM.NIMMessageType.kNIMMessageTypeImage)
            {
                var imageMsg = _message as NIM.NIMImageMessage;
                if (imageMsg != null)
                {
                    _attachment = imageMsg.ImageAttachment;
                    _lookupPath = NIM.ToolsAPI.GetUserSpecificAppDataDir(uid, NIM.NIMAppDataType.kNIMAppDataTypeImage);
                }
            }
            else if (_message.MessageType == NIM.NIMMessageType.kNIMMessageTypeFile)
            {
                var fileMsg = _message as NIM.NIMFileMessage;
                if (fileMsg != null)
                {
                    _attachment = fileMsg.FileAttachment;
                    _lookupPath = NIM.ToolsAPI.GetUserSpecificAppDataDir(uid, NIM.NIMAppDataType.kNIMAppDataTypeUnknownOtherRes);
                }
            } 
            
        }

        void CheckLocalResource()
        {
            if (_attachment == null)
            {
                downloadBtn.Enabled = false;
                return;
            }
            bool exist = false;
            string path = null;
            if (!string.IsNullOrEmpty(_message.LocalFilePath))
            {
                path = _message.LocalFilePath;
                exist = File.Exists(path);
            }
            if(!exist && !string.IsNullOrEmpty(_lookupPath))
            {
                path = Path.Combine(_lookupPath, _attachment.MD5);
                exist = File.Exists(path);
            }
            downloadBtn.Enabled = !exist;
            viewAttachmentBtn.Enabled = exist;
            if (exist)
            {
                _message.LocalFilePath = path;
                if (_message.MessageType == NIM.NIMMessageType.kNIMMessageTypeImage)
                {
                    pictureBox1.Image = Image.FromFile(path);
                }
            }
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            if (_attachment == null) return;
            viewAttachmentBtn.Enabled = false;
            NIM.Nos.NosAPI.DownloadMedia(_message, (a, b, c, d) =>
            {
                if (a == 200)
                {
                    Action<string> action = OnDownloadCompleted;
                    _message.LocalFilePath = b;
                    this.Invoke(action, b);
                }
            },
            (a) =>
            {
                
            });

            NIM.Nos.NosAPI.Download(_attachment.RemoteUrl, (a, b, c, d) => 
            {
                System.Diagnostics.Debug.WriteLine(new { A = a, B = b, C = c, D = d }.Dump());
            }, 
            (a) => 
            {
                System.Diagnostics.Debug.WriteLine(a.Dump());
            }, "user data");
        }

        void OnDownloadCompleted(string path)
        {
            if (_attachment is NIM.NIMImageAttachment)
                pictureBox1.Image = Image.FromFile(path);
            else
            {
                viewAttachmentBtn.Enabled = true;
            }
        }

        private void viewAttachmentBtn_Click(object sender, EventArgs e)
        {
            if (_message.MessageType != NIM.NIMMessageType.kNIMMessageTypeAudio)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                var path = _message.LocalFilePath;
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(path);
                dialog.FileName = path;
                dialog.ShowDialog();
            }
            else
            {
                var x = NIM.ToolsAPI.GetUserAppDataDir(MainForm.SelfId);
                if (!NIMAudio.AudioAPI.InitModule(""))
                {
                    return;
                }
                NIMAudio.AudioAPI.RegStartPlayCb((m, n, s, d) =>
                {
                    DemoTrace.WriteLine("开始播放:", m, n, s, d);
                });

                NIMAudio.AudioAPI.RegStopPlayCb((z, xx, c, v) =>
                {
                    DemoTrace.WriteLine("结束播放:",z, xx, c, v);
                });
                var audioMsg = _message as NIM.NIMAudioMessage;
                var ret = NIMAudio.AudioAPI.PlayAudio(_message.LocalFilePath, "", "", NIMAudio.NIMAudioType.kNIMAudioAAC);
            }
        }
    }
}
