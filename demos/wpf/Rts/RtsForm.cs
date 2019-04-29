using NIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace NIMDemo
{
    public partial class RtsForm : Form
    {
        private static RtsForm _rtsForm = null;

        private string _sessionId;
        private string _peerId;
        private NIM.NIMRts.NIMRtsChannelType _channelType;

        private bool _drawing = false;
        private Graphics _peerDrawingGraphics;
        private Graphics _myDrawingGraphics;
        private readonly Pen _myPen = new Pen(Color.Blue,3);
        private readonly Pen _peerPen = new Pen(Color.Brown,3);
        private readonly PaintingRecord _selfPaintingRecord = new PaintingRecord();
        private readonly PaintingRecord _peerPaintingRecord = new PaintingRecord();
        private readonly System.Windows.Forms.Timer _sendDataTimer;
        private RtsFormState rts_state_ = RtsFormState.kRtsInit;
     
        public RtsFormState RtsState
        {
            get
            {
                return rts_state_;
            }
            set
            {
                rts_state_ = value;
                Action action = () =>
                {
                    UpdateRtsFormState();
                };
                this.BeginInvoke(action);
            }
        }

        private RtsForm()
        {
            InitializeComponent();
            this.Load += RtsForm_Load;
            this.FormClosed += RtsForm_FormClosed;
            panel1.MouseMove += Panel1_MouseMove;
            panel1.MouseDown += Panel1_MouseDown;
            panel1.MouseUp += Panel1_MouseUp;

            _sendDataTimer = new System.Windows.Forms.Timer();
            _sendDataTimer.Interval = 100;
            _sendDataTimer.Tick += _sendDataTimer_Tick;

            this.button1.Click += Button1_Click;
            this.button2.Click += Button2_Click;
            this.audioCtrlBtn.Click += AudioCtrlBtn_Click;
        }

        public static  RtsForm GetInstance()
        {
            if (_rtsForm == null)
            {
                _rtsForm = new RtsForm();
            }
            return _rtsForm;
        }

        public void SetRtsInfo(string id,string uid, NIM.NIMRts.NIMRtsChannelType channel_type)
        {
            _sessionId = id;
            _peerId = uid;
            _channelType = channel_type;
        }

        private void AudioCtrlBtn_Click(object sender, EventArgs e)
        {
            var tag = audioCtrlBtn.Tag.ToString();
            string open = "开启音频";
            string close = "关闭音频";
            var i = tag == "0" ? open : close;
            if (tag == "0")
            {
				NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn, "", 0,null, null); 
            }
            else
            {
                NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
            }
            NIM.RtsAPI.Control(_sessionId, i, (int code, string sessionId, string info) =>
            {
                if (code == 200)
                {
                    Func<string, int> func = (ret) =>
                    {
                        if (info == open)
                        {
                            audioCtrlBtn.Text = "关闭麦克风";
                            audioCtrlBtn.Tag = 1;
                        }
                        if (info == close)
                        {
                            audioCtrlBtn.Text = "开启麦克风";
                            audioCtrlBtn.Tag = 0;
                        }
                        return 0;
                    };
                    this.audioCtrlBtn.BeginInvoke(func, info);
                }
            });
        }

        void SendData()
        {
            var cmdStr = _selfPaintingRecord.CreateCommand();
            if (string.IsNullOrEmpty(cmdStr))
                return;
            var ptr = Marshal.StringToHGlobalAnsi(cmdStr);
            NIM.RtsAPI.SendData(_sessionId, NIM.NIMRts.NIMRtsChannelType.kNIMRtsChannelTypeTcp, ptr, cmdStr.Length);
        }

        void SendControlCommand(CommandType type)
        {
            string cmdStr = string.Format("{0}:0,0;", (int) type);
            var ptr = Marshal.StringToHGlobalAnsi(cmdStr);
            NIM.RtsAPI.SendData(_sessionId, NIM.NIMRts.NIMRtsChannelType.kNIMRtsChannelTypeTcp, ptr, cmdStr.Length);
        }

        void ClearGraph()
        {
            _peerDrawingGraphics.Clear(panel1.BackColor);
            _peerPaintingRecord.Clear();
            _myDrawingGraphics.Clear(panel1.BackColor);
            _selfPaintingRecord.Clear();
        }

        private void RtsForm_Load(object sender, EventArgs e)
        {
            NIM.RtsAPI.SetReceiveDataCallback(OnReceiveRtsData);
            _peerDrawingGraphics = panel1.CreateGraphics();
            _myDrawingGraphics = panel1.CreateGraphics();
            _selfPaintingRecord.BaseSize = panel1.Size;
            SetRtsNotifyCallback();
            NIM.DeviceAPI.StartDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat, "", 0,null,null);//开启扬声器播放对方语音
        }

        void OnReceiveRtsData(string sessionId, int channelType, string uid, IntPtr data, int size)
        {
            if (_sessionId != sessionId)
                return;
            var content = Marshal.PtrToStringAnsi(data, size);
            System.Diagnostics.Debug.WriteLine(content);
            var lines = content.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            var s = _peerPaintingRecord.Count;
            foreach (var item in lines)
            {
                var cmd = PaintCommand.Create(item, panel1.Size);
                if (cmd == null || cmd.Type == CommandType.DrawOpPktId)
                    continue;
                _peerPaintingRecord.Add(cmd);
            }
            var c = _peerPaintingRecord.Count;
            if (c <= 0)
                return;
            CommandType lastCmdType = _peerPaintingRecord[c - 1].Type;
            if (lastCmdType == CommandType.DrawOpUndo)
            {
                _peerPaintingRecord.Revert();
                _peerDrawingGraphics.Clear(panel1.BackColor);
                _myDrawingGraphics.Clear(panel1.BackColor);
                DoDraw(_selfPaintingRecord, 0, _selfPaintingRecord.Count - 1, _myDrawingGraphics, _myPen);
                DoDraw(_peerPaintingRecord, 0, _peerPaintingRecord.Count - 1, _peerDrawingGraphics, _peerPen);
            }
            else if (lastCmdType == CommandType.DrawOpClear)
            {
                SendControlCommand(CommandType.DrawOpClearCb);
                ClearGraph();
            }
            else if (lastCmdType == CommandType.DrawOpClearCb)
            {
                ClearGraph();
            }
            else if(lastCmdType == CommandType.DrawOpStart || lastCmdType == CommandType.DrawOpMove || lastCmdType == CommandType.DrawOpEnd)
            {
                DoDraw(_peerPaintingRecord, s, _peerPaintingRecord.Count - 1, _peerDrawingGraphics, _peerPen);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("unknow data type:{0}", content);
            }
        }

        void DoDraw(PaintingRecord record, int startIndex, int endIndex, Graphics graphics, Pen pen)
        {
            Action action = () =>
            {
                Draw(record, startIndex, endIndex, graphics, pen);
            };
            this.BeginInvoke(action);
        }

        byte[] ReadData(IntPtr data, int size)
        {
            byte[] buffer = new byte[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = Marshal.ReadByte(data, i);
            }
            return buffer;
        }

        void Draw(PaintingRecord record, int startIndex, int endIndex, Graphics graphics, Pen pen)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            PointF? start = null;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var command = record[i];
                if (i >= 1)
                {
                    var ls = record[i - 1];
                    if (ls.Type != CommandType.DrawOpEnd)
                        start = new PointF(Math.Abs(ls.Coord.X), Math.Abs(ls.Coord.Y));
                    else
                    {
                        graphicsPath.StartFigure();
                        start = null;
                    }
                }
                PointF point = new PointF(Math.Abs(command.Coord.X), Math.Abs(command.Coord.Y));
                if (start != null)
                    graphicsPath.AddLine(start.Value, point);
                else
                {

                }
            }
            graphics.DrawPath(pen, graphicsPath);
        }

        //清空
        private void Button2_Click(object sender, EventArgs e)
        {
            SendControlCommand(CommandType.DrawOpClear);
        }

        //上一步
        private void Button1_Click(object sender, EventArgs e)
        {
            _selfPaintingRecord.Revert();
            _myDrawingGraphics.Clear(panel1.BackColor);
            _peerDrawingGraphics.Clear(panel1.BackColor);
            DoDraw(_selfPaintingRecord, 0, _selfPaintingRecord.Count - 1, _myDrawingGraphics, _myPen);
            DoDraw(_peerPaintingRecord, 0, _peerPaintingRecord.Count - 1, _peerDrawingGraphics, _peerPen);
            SendControlCommand(CommandType.DrawOpUndo);
        }

        private void _sendDataTimer_Tick(object sender, EventArgs e)
        {
            SendData();
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _drawing = false;
                _selfPaintingRecord.Add(CommandType.DrawOpEnd, e.X, e.Y);
                SendData();
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _drawing = true;
                _selfPaintingRecord.Add(CommandType.DrawOpStart, e.X, e.Y);
            }
        }


        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !_drawing) return;
            _selfPaintingRecord.Add(CommandType.DrawOpMove, e.X, e.Y);
            var count = _selfPaintingRecord.Count;
            if (count >= 2)
            {
                Draw(_selfPaintingRecord, count - 2, count - 1, _myDrawingGraphics, _myPen);
            }
        }

        private void RtsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _sendDataTimer.Stop();
            NIM.RtsAPI.Hangup(_sessionId, (a, b) =>
            {

            });
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioIn);
            NIM.DeviceAPI.EndDevice(NIM.NIMDeviceType.kNIMDeviceTypeAudioOutChat);
            System.Threading.Thread.Sleep(2000);
            _rtsForm = null;
            
        }

        public void SetRtsNotifyCallback()
        {
            NIM.RtsAPI.SetHungupNotify((string sessionId, string uid) =>
            {
                if (sessionId == _sessionId)
                    RtsState = RtsFormState.kRtsExitOrRefuse;
               
            });

            NIM.RtsAPI.SetConnectionNotifyCallback((string sessionId, int channelType, int code) =>
            {
                
            });

            NIM.RtsAPI.SetControlNotifyCallback((string sessionId, string info, string uid) =>
            {
                if (sessionId != _sessionId) return;
                SetPromptTip(uid + " " + info);
            });
        }

        void SetPromptTip(string tip)
        {
            Action action = () => { this.promptLabel.Text = tip; };
            this.promptLabel.Invoke(action);
        }

        void Invoke(Action action)
        {
            this.BeginInvoke(action);
        }

        private void BtnRtsAcceptClick(object sender, EventArgs e)
        {
            NIM.RtsAPI.Ack(_sessionId, _channelType, true, null, Response);
        }

        private void BtnRtsRefuseClick(object sender, EventArgs e)
        {
            NIM.RtsAPI.Ack(_sessionId, _channelType, false, null, Response);
        }

        private void UpdateRtsFormState()
        {
            switch(rts_state_)
            {
                case RtsFormState.kRtsInvite:
                    {
  
                        panel_rts_working.Visible = false;
                        panel_rts_invite_or_notify.Visible = true;

                        lb_rts_member.Text = _peerId;
                        lb_rts_prompt.Text = "正在邀请对方,请稍后";
                        btn_rts_cancel.Visible = true;
                        btn_rts_accept.Visible = false;
                        btn_rts_refuse.Visible = false;
                        CallRts();
                    }
                    break;
                case RtsFormState.kRtsNotify:
                    {
                        panel_rts_working.Visible = false;
                        panel_rts_working.Visible = true;

                        lb_rts_member.Text = _peerId;
                        lb_rts_prompt.Text = "邀请你加入白板";
                        btn_rts_accept.Visible = true;
                        btn_rts_refuse.Visible = true;
                        btn_rts_cancel.Visible = false;
                    }
                    break;
                case RtsFormState.kRtsExitOrRefuse:
                    {
                        panel_rts_working.Visible = false;
                        panel_rts_invite_or_notify.Visible = true;

                        lb_rts_member.Text = _peerId;
                        lb_rts_prompt.Text = "正在退出";
                        btn_rts_accept.Visible = false;
                        btn_rts_refuse.Visible = false;
                        btn_rts_cancel.Visible = false;
                        _sendDataTimer.Stop();
                        TimerCloseForm();
                    }
                    break;
                case RtsFormState.kRtsContected:
                    {
                        panel_rts_invite_or_notify.Visible = false;
                        panel_rts_working.Visible = true;
                        promptLabel.Text = _peerId + " " + promptLabel.Text;

                       _sendDataTimer.Start();
                    }
                    break;
            }
        }


        private void  AckNotifyCallback(string sessionId, int channelType, bool accept, string uid)
        {
            if (!accept)
            {
                RtsState = RtsFormState.kRtsExitOrRefuse;
            }
            else
            {
                RtsState = RtsFormState.kRtsContected;
            }
        }

        private void StartResCallback(int code, string sessionId, int channelType, string uid)
        {
            if(code==200)
            {
                _sessionId = sessionId;
                RtsState = RtsFormState.kRtsInvite;
            }
            else
            {
                RtsState = RtsFormState.kRtsIniveteFailed;
            }
        }

        private void CallRts()
        {
            NIM.NIMRts.RtsStartInfo info = new NIM.NIMRts.RtsStartInfo();
            info.ApnsText = "123";
            info.CustomInfo = "456";
            RtsAPI.SetAckNotifyCallback(AckNotifyCallback);
            RtsAPI.Start(_channelType, _peerId, info,StartResCallback);
        }

        
        void Response(int code, string sessionId, int channelType, bool accept)
        {
            if (accept && code == (int)NIM.ResponseCode.kNIMResSuccess)
            {
                _sessionId = sessionId;
                RtsState = RtsFormState.kRtsContected;
            }
            else
            {
                RtsState = RtsFormState.kRtsExitOrRefuse;
            }
        }

        private void BtnRtsCancelClick(object sender, EventArgs e)
        {
            NIM.RtsAPI.Hangup(_sessionId,null);
            RtsState = RtsFormState.kRtsExitOrRefuse;
        }

        /// <summary>
        /// 定时关闭窗口
        /// </summary>
        private void TimerCloseForm()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 3000; //5s后关闭窗口
            timer.Elapsed += TimerClose;
            timer.Start();
        }

        private void TimerClose(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            Close();
            timer.Stop();
        }
    }

    public enum RtsFormState
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        kRtsInit=0,
        /// <summary>
        /// rts 邀请他人
        /// </summary>
        kRtsInvite,
        /// <summary>
        /// 发起邀请失败
        /// </summary>
        kRtsIniveteFailed,
        /// <summary>
        /// rts 收到对方邀请
        /// </summary>
        kRtsNotify,
        /// <summary>
        /// rts 会话已经连接
        /// </summary>
        kRtsContected,
        /// <summary>
        /// rts 退出
        /// </summary>
        kRtsExitOrRefuse
    }
}
