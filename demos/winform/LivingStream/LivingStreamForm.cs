using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIMDemo.LivingStreamSDK;
using System.Threading;
using NIMDemo.AVChat;
using System.Drawing.Drawing2D;
//using NIMDemo.MainForm;
using NIM;
using NimUtility;

namespace NIMDemo
{
	public partial class LivingStreamForm : Form
	{
        private bool beauty_ = false;
        private string pullurl_ = "";
		Graphics graphics = null;
		public LivingStreamForm()
		{
			InitializeComponent();
			graphics = pb_livingstream.CreateGraphics();
		}

        public bool Beauty
        {
            get { return beauty_; }
            set
            {
                beauty_ = value;
            }
        }

		private void btn_Click(object sender, EventArgs e)
		{
			AVDevicesSettingForm form = new AVDevicesSettingForm();
			form.ShowDialog();
		}

		private void btn_bypass_Click(object sender, EventArgs e)
		{
			string appkey = ConfigReader.GetAppKey();
			if (appkey==null||!appkey.Equals("6f49e3f759ccd47810b445444eebc090"))
			{
				MessageBox.Show("请将appkey更改为6f49e3f759ccd47810b445444eebc090！");
				return; 
			}

			if (nimSDKHelper.session != null)
			{
				nimSDKHelper.session.DoStopLiveStream();
				nimSDKHelper.session.ClearSession();
				nimSDKHelper.session = null;
				btn_ls.Text = "开始直播";
			}
			BypassLivingStreamChoiceForm choiceform = new BypassLivingStreamChoiceForm();
			choiceform.ShowDialog();
		}

		public void ShowVideoFrame(VideoFrame frame)
		{
			int w = pb_livingstream.Width;
			int h = pb_livingstream.Height;
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

        public string  GetPushUrl()
        {
            return pullurl_;
        }

		private void btn_ls_Click(object sender, EventArgs e)
		{
			if(String.IsNullOrEmpty(rt_push_url.Text))
			{
				MessageBox.Show("推流地址不能为空!");
				return;
			}
			NIMDeviceInfoList cameraDeviceinfolist = NIM.DeviceAPI.GetDeviceList(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
			if(cameraDeviceinfolist==null)
			{
				MessageBox.Show("没有摄像头,无法直播！！！");
				return;
			}

            pullurl_ = rt_push_url.Text;
			if (nimSDKHelper.session == null)
			{
				nimSDKHelper.form = this;
				nimSDKHelper.session = new LsSession();
                nimSDKHelper.session.InitSession(false, pullurl_); 
				nimSDKHelper.session.DoStartLiveStream();
				btn_ls.Text = "结束直播";
			}
			else
			{
				nimSDKHelper.session.DoStopLiveStream();
				nimSDKHelper.session.ClearSession();
				nimSDKHelper.session = null;
				btn_ls.Text = "开始直播";
			}
		}

        private void btn_beauty_Click(object sender, EventArgs e)
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
	}
}
