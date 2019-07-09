using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
	public partial class BypassLivingStreamChoiceForm : Form
	{
		public BypassLivingStreamChoiceForm()
		{
			InitializeComponent();
			NIMChatRoom.ChatRoomApi.Init();
		}



		private void btn_joinroom_Click(object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(tb_joinroomid.Text))
			{
				BypassLivingStreamForm form = new BypassLivingStreamForm(false);
				Action act = () =>
				{
					form.ShowDialog();
				};
				this.BeginInvoke(act);
				form.RequestEnter(tb_joinroomid.Text);
			}
			else
			{
				MessageBox.Show("房间号不能为空！");
			}
		}

		private void btn_choice_start_living_Click(object sender, EventArgs e)
		{

			BypassLSCommon.InactionType inactionType = BypassLSCommon.InactionType.kAudio;
			if(rb_audio.Checked)
			{
				inactionType = BypassLSCommon.InactionType.kAudio;
			}
			if(rb_video.Checked)
			{
				inactionType = BypassLSCommon.InactionType.kVedio;
			}

			CreateMyRoomInfo();

		}

		private void CreateMyRoomInfo()
		{
			string roomname = Guid.NewGuid().ToString("N");
			string api_addr = "https://app.netease.im/api/chatroom/hostEntrance";
			string app_key = "6f49e3f759ccd47810b445444eebc090";
			string ext = "";
			StringWriter sw = new StringWriter();
			JsonWriter writer = new JsonTextWriter(sw);
			writer.WriteStartObject();
			if(rb_audio.Checked)
			{
				writer.WritePropertyName("type");
				writer.WriteValue(Convert.ToInt16(BypassLSCommon.InactionType.kAudio));
			}
			else
			{
				writer.WritePropertyName("type");
				writer.WriteValue(Convert.ToInt16(BypassLSCommon.InactionType.kVedio));
			}
			writer.WritePropertyName("meetingName");
			writer.WriteValue(roomname);
			writer.WriteEndObject();
			writer.Flush();
			ext = sw.GetStringBuilder().ToString();
			
			string body = "uid=" + Helper.UserHelper.SelfId
						 + "&ext=" + ext;
			int bodySize = body.Length;

			NIMHttp.NimHttpDef.ResponseCb responseCb = (userData, result, responseCode, responseContent) =>
			{
				if (!result || responseCode !=NIM.ResponseCode.kNIMResSuccess)
				{
					Action action = () =>
					  {
						  MessageBox.Show("进入房间失败");
					  };
					this.Invoke(action);
				}
				else
				{
					JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
					int res = Convert.ToInt16(jo["res"]);
					if (res != 200)
					{
						Action action = () =>
						{
							MessageBox.Show("进入房间出错,errorcode:" + res.ToString());
						};
						this.Invoke(action);
					}
					else
					{
						string roomid = jo["msg"]["roomid"].ToString();
						string rtmpPullUrl = jo["msg"]["live"]["rtmpPullUrl"].ToString();
						string pushUrl = jo["msg"]["live"]["pushUrl"].ToString();

						Action action = () =>
						{
							BypassLivingStreamForm form = new BypassLivingStreamForm(true);
							form.SetPullUrl(rtmpPullUrl);
							Action act = () =>
							{
								form.ShowDialog();
							};
							this.BeginInvoke(act);
							form.RequestEnter(roomid);
						};
						this.BeginInvoke(action);
					}
				}
			};
		
			IntPtr request = NIMHttp.HttpAPI.CreateRequest(api_addr, body, bodySize, responseCb, IntPtr.Zero);
			NIMHttp.HttpAPI.AddHeader(request, "Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
			NIMHttp.HttpAPI.AddHeader(request, "appKey", app_key);
			NIMHttp.HttpAPI.PostRequest(request);
		}
	}
}
