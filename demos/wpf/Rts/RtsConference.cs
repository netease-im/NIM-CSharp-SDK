using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.Rts
{
    public partial class RtsConference : Form
    {
        private readonly InvokeActionWrapper _actionWrapper;
        Tools.OutputInfoText _outputTools;
        public RtsConference()
        {
            InitializeComponent();
            _actionWrapper = new InvokeActionWrapper(this);
            _outputTools = new Tools.OutputInfoText(richTextBox1);
            NIM.RtsAPI.SetReceiveDataCallback(OnReceiveData);
        }

        private void OnReceiveData(string sessionId, int channelType, string uid, IntPtr data, int size)
        {
            var bytes = new byte[size];
            int i = 0;
            while(i<size)
            {
                bytes[i] = Marshal.ReadByte(data, i);
                i++;
            }
            var txt = System.Text.Encoding.Default.GetString(bytes);
            var info = string.Format("收到消息:\r\nsessionId:{0}\r\nchannel:{1}\r\nuid:{2}\r\ndata:{3}",
                sessionId, channelType, uid, txt);
            _outputTools.ShowInfo(info);
        }

        private void createConfBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
                NIM.RtsAPI.CreateConference(textBox1.Text, textBox2.Text, OnConfCreated);
        }

        private void OnConfCreated(int code, string json_extension, IntPtr user_data)
        {

            string info = string.Format("创建多人白板:\r\nres:{0}\r\ninfo:{1}", code, json_extension);
            _outputTools.ShowInfo(info);
        }

        private void joinConfBtn_Click(object sender, EventArgs e)
        {
            NIM.RtsAPI.JoinConference(textBox1.Text, null, OnJoinConf);
        }

        private void OnJoinConf(int code, string session_id, string json_extension, IntPtr user_data)
        {

            string info;
            if (code == 200)
            {
                info = string.Format("加入成功:\r\nsession id:{0}\r\ncustom info:{1}", session_id, json_extension);
                Action action = () => 
                {
                    sessionIdTxt.Text = session_id;
         
                };
                _actionWrapper.InvokeAction(action);
            }
            else
            {
                info = string.Format("加入失败:{0} \r\n{1}", code, json_extension);
            }
            _outputTools.ShowInfo(info);
        }

        private void sendDataBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sessionIdTxt.Text))
            {
                var content = contentTxt.Text;
                var bytes = System.Text.Encoding.Default.GetBytes(content);
                var len = System.Text.Encoding.Default.GetByteCount(content);
                var ptr = Marshal.AllocCoTaskMem(len);
                int i = 0;
                while (i < len)
                {
                    Marshal.WriteByte(ptr, i, bytes[i]);
                    i++;
                }

                NIM.RtsAPI.SendData(sessionIdTxt.Text, NIM.NIMRts.NIMRtsChannelType.kNIMRtsChannelTypeTcp, ptr, len);
            }
        }
    }
}
