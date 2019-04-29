using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NIM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
	public partial class BypassLivingStreamForm : Form
	{
		private string room_id_;//房间号
		private string room_name_; //房间名
		private bool master_;//是否是主播
		private string rt_push_url_;//推流地址
		private string rt_pull_url_;//拉流地址
		private bool video_interaction_;
		private bool audio_interaction_;
		private string room_enter_token_;
		private BypassLSCommon.InactionType inaction_type_;
		private BypassLSCommon.InactionType living_model_;
		 

		public void SetPullUrl(string pull_url)
		{ 
			rt_pull_url.Text = pull_url;
		}

		public void SetRoomName(string roomname)
		{
			room_name_ = roomname;
		}
		
		public void SetInactionType(BypassLSCommon.InactionType type)
		{
			inaction_type_ = type;
		}
		public BypassLivingStreamForm(bool ismaster)
		{
			InitializeComponent();
			master_ = ismaster;
			if(master_)
			{
				btn_audio_interact.Visible = false;
				btn_video_interact.Visible = false;
			}
			else
			{
				btn_audio_interact.Visible = true;
				btn_video_interact.Visible = true;
			}
			video_interaction_ = false;
			audio_interaction_ = false;
		}

		public void RequestEnter(string roomid)
		{
			NIMChatRoom.ChatRoomApi.LoginHandler += ChatRoomApi_LoginHandler;
			room_id_ = roomid;
			string json_info = "";
			StringWriter sw = new StringWriter();
			JsonWriter writer = new JsonTextWriter(sw);
			writer.WriteStartObject();
			writer.WritePropertyName("type");
			writer.WriteValue(Convert.ToInt16(living_model_));
			writer.WritePropertyName("meetingName");
			writer.WriteValue(room_name_);
			writer.WriteEndObject();
			writer.Flush();
			json_info = sw.GetStringBuilder().ToString();

			NIM.Plugin.RequestChatRoomLoginInfoDelegate cb = (code, result) =>
			{
				if(code!= ResponseCode.kNIMResSuccess)
				{
					Action action = ()=>{
						MessageBox.Show("进入房间失败");
					};
					this.BeginInvoke(action);
				}
				else
				{
					Action action = () =>
					{
						rt_prompt_info.Text += "进入房间成功\n";
					};
					this.BeginInvoke(action);
					if (!String.IsNullOrEmpty(result))
					{
						room_enter_token_ = result;
						NIMChatRoom.ChatRoomApi.Login(Convert.ToInt64(room_id_), room_enter_token_);
					}
				}
			};
			NIM.Plugin.ChatRoom.RequestLoginInfo(Convert.ToInt64(roomid), cb, json_info);
		}

		private void ChatRoomApi_LoginHandler(NIMChatRoom.NIMChatRoomLoginStep loginStep, ResponseCode errorCode, NIMChatRoom.ChatRoomInfo roomInfo, NIMChatRoom.MemberInfo memberInfo)
		{
			if (loginStep != NIMChatRoom.NIMChatRoomLoginStep.kNIMChatRoomLoginStepRoomAuthOver)
				return;
			if (errorCode != ResponseCode.kNIMResSuccess && errorCode != ResponseCode.kNIMResTimeoutError)
			{
				this.RequestEnter(room_id_);
				return;

			}
			else
			{
				EnteredOperate(errorCode, roomInfo, memberInfo);
			}
		}


		//进入房间之后的处理
		private void EnteredOperate(ResponseCode error_code,NIMChatRoom.ChatRoomInfo roomInfo,NIMChatRoom.MemberInfo memberInfo)
		{
			if(error_code!=ResponseCode.kNIMResSuccess)
			{
				Close();
				return;
			}
			
			if(0==roomInfo.RoomId)
			{
				return;
			}
			room_id_ = roomInfo.RoomId.ToString();
			room_name_ = roomInfo.RoomName;
			if (!IsDisposed &&this.IsHandleCreated)
			{
				this.Invoke(new Action(() => { this.Text = room_name_; }));
			}
			RequestAddress(room_id_,Helper.UserHelper.SelfId);
			InitChatRoomQueueInfo();

		}

		private void btn_start_ls_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(rt_push_url_))
			{
				MessageBox.Show("推流地址不能为空!");
				return;
			}
			NIMDeviceInfoList cameraDeviceinfolist = NIM.DeviceAPI.GetDeviceList(NIM.NIMDeviceType.kNIMDeviceTypeVideo);
			if (cameraDeviceinfolist == null)
			{
				MessageBox.Show("没有摄像头,无法直播！！！");  
				return;
			}

		}

		#region 麦序相关操作
		private void InitChatRoomQueueInfo()
		{
			//NIMChatRoom.ChatRoomApi
			//如果是主播，先清楚掉麦序的队列
			if (master_)
			{
// 				NIMChatRoom.ChatRoomQueueDropDelegate queuedropcb = (roomid, errorCode) =>
// 				{
// 
// 				};
// 				NIMChatRoom.ChatRoomApi.QueueDropAsync(Convert.ToInt64(room_id_), queuedropcb);
			}
// 			NIMChatRoom.ChatRoomQueueListDelegate queuelistcb = (roomid, errorCode, result, json_extension, userdata) =>
// 			  {
// 
// 			  };
			//NIMChatRoom.ChatRoomApi.QueueListAsync(Convert.ToInt64(room_id_), queuelistcb);
		}

		//从聊天室队列中添加连麦请求
		private void RequestPushMicLink(string roomid,string uid,string ext,BypassLSCommon.InactionType type)
		{
			string url = "https://app.netease.im/api/chatroom/pushMicLink";
			string app_key ="6f49e3f759ccd47810b445444eebc090";
			string body = "roomid=" + roomid
						 + "&uid=" + uid
						 + "&ext=" + ext;


			int bodySize = body.Length;
			NIMHttp.NimHttpDef.ResponseCb responseCb =(userData, result,  responseCode, responseContent) =>
			{
				JObject jo= (JObject)JsonConvert.DeserializeObject(responseContent);
				if (responseCode == ResponseCode.kNIMResSuccess)
				{
					Action act = () =>
					{
						rt_prompt_info.Text += Helper.UserHelper.SelfId + "加入麦序队列成功\n";
						if (video_interaction_)
						{
							btn_video_interact.Text = "取消视频互动";
							video_interaction_ = true;
						}
						if (audio_interaction_)
						{
							btn_audio_interact.Text = "取消音频互动";
							audio_interaction_ = true;
						}
					};
					this.BeginInvoke(act);
				}
				else
				{
					if (video_interaction_)
					{
						btn_video_interact.Text = "视频互动";
						video_interaction_ = false;
					}
					if (audio_interaction_)
					{
						audio_interaction_ = false;
						btn_audio_interact.Text = "音频互动";
					}
				}
			};
			IntPtr request=NIMHttp.HttpAPI.CreateRequest(url, body, bodySize, responseCb, IntPtr.Zero);
			NIMHttp.HttpAPI.AddHeader(request, "Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
			NIMHttp.HttpAPI.AddHeader(request, "appKey", app_key);
			NIMHttp.HttpAPI.PostRequest(request);
		}

		//从聊天室队列中取出连麦请求
		private void RequestPopMicLink(string roomid,string uid)
		{
			string url = "https://app.netease.im/api/chatroom/popMicLink";
			string app_key = "6f49e3f759ccd47810b445444eebc090";
			string body = "roomid=" + roomid
						 + "&uid=" + uid;

			int bodySize = body.Length;
			NIMHttp.NimHttpDef.ResponseCb responseCb = (userData, result, responseCode, responseContent) =>
			{
				JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
				if (responseCode == ResponseCode.kNIMResSuccess)
				{
					Action act = () =>
					{
						this.rt_prompt_info.Text += "取消进入队列成功\n";
						if (video_interaction_)
						{
							btn_video_interact.Text = "视频互动";
							video_interaction_ = false;
						}

						if(audio_interaction_)
						{
							btn_audio_interact.Text = "音频互动";
							audio_interaction_ = false;
						}
					};
					this.BeginInvoke(act);
				}
			};
			IntPtr request = NIMHttp.HttpAPI.CreateRequest(url, body, bodySize, responseCb, IntPtr.Zero);
			NIMHttp.HttpAPI.AddHeader(request, "Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
			NIMHttp.HttpAPI.AddHeader(request, "appKey", app_key);
			NIMHttp.HttpAPI.PostRequest(request);
		}

		//查询聊天室地址
		private void RequestAddress(string roomid, string uid)
		{
			string url = "https://app.netease.im/api/chatroom/requestAddress";
			string app_key = "6f49e3f759ccd47810b445444eebc090";

			StringWriter sw = new StringWriter();
			JsonWriter writer = new JsonTextWriter(sw);
			writer.WriteStartObject();
			writer.WritePropertyName("roomid");
			writer.WriteValue(roomid);
			writer.WritePropertyName("uid");
			writer.WriteValue(uid);
			writer.WriteEndObject();
			writer.Flush();
			string body = sw.GetStringBuilder().ToString();
			int bodySize = body.Length;
			NIMHttp.NimHttpDef.ResponseCb responseCb = (userData, result, responseCode, responseContent) =>
			{
				try
				{
					if (responseCode == ResponseCode.kNIMResSuccess)
					{
						JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
						int json_res = Convert.ToInt32(jo["res"]);

						if (json_res == Convert.ToInt32(ResponseCode.kNIMResSuccess))
						{
							rt_pull_url_ = jo["msg"]["live"]["rtmpPullUrl"].ToString();
							Action act = () =>
							{
								rt_pull_url.Text = rt_pull_url_;
							};
							if (IsDisposed || !this.IsHandleCreated) return;
							this.Invoke(act);
						}
					}
				}
				catch(Exception ex)
				{

				}
			};
			IntPtr request = NIMHttp.HttpAPI.CreateRequest(url, body, bodySize, responseCb, IntPtr.Zero);
			NIMHttp.HttpAPI.AddHeader(request, "Content-Type", "application/json;charset=utf-8");
			NIMHttp.HttpAPI.AddHeader(request, "appKey", app_key);
			NIMHttp.HttpAPI.PostRequest(request);
		}

		#endregion

		private void btn_video_interact_Click(object sender, EventArgs e)
    	{
			if (audio_interaction_) return;
			if (!video_interaction_)
			{
				StringWriter sw = new StringWriter();
				using (JsonWriter jw = new JsonTextWriter(sw))
				{
					jw.WriteStartObject();
					jw.WritePropertyName("style");
					jw.WriteValue(BypassLSCommon.InactionType.kVedio);
					jw.WritePropertyName("info");
					//jw.WriteStartArray();

					StringWriter sw_temp = new StringWriter();
					using (JsonWriter jw_temp = new JsonTextWriter(sw_temp))
					{
						jw_temp.WriteStartObject();
						jw_temp.WritePropertyName("nick");
						jw_temp.WriteValue(" ");
						jw_temp.WritePropertyName("avater_default");
						jw_temp.WriteValue("avater_default");
					}
					string info = sw_temp.GetStringBuilder().ToString();
					jw.WriteValue(info);
					//jw.WriteEndArray();
					jw.WriteEndObject();
				}
				string ext = sw.GetStringBuilder().ToString();
				video_interaction_ = true;
				RequestPushMicLink(room_id_, Helper.UserHelper.SelfId, ext, BypassLSCommon.InactionType.kVedio);
			}
			else
			{
				RequestPopMicLink(room_id_, Helper.UserHelper.SelfId);
			}

		}

		
		private void btn_query_queue_Click(object sender, EventArgs e)
		{
			NIMChatRoom.ChatRoomApi.QueueListAsync(Convert.ToInt64(room_id_), (room_id, error_code, result) =>
			{
				if (error_code == ResponseCode.kNIMResSuccess)
				{
					Action act = () =>
					{
						rt_prompt_info.Text += "查询队列信息:\n" + "error_code"+error_code+"\n"+result + "\n";
					};
					this.Invoke(act);
				}
			});
		}

		private void btn_clear_queue_Click(object sender, EventArgs e)
		{
			NIMChatRoom.ChatRoomApi.QueueDropAsync(Convert.ToInt64(room_id_), ( room_id, error_code) =>
			{
				Action act = () =>
				{
					rt_prompt_info.Text += "清除队列信息:\n" + "error_code:" + error_code + "\n";
				};
				this.Invoke(act);
			});
		}

		private void btn_poll_element_Click(object sender, EventArgs e)
		{
			NIMChatRoom.ChatRoomApi.QueuePollAsync(Convert.ToInt64(room_id_),tb_opt_key.Text,(room_id,error_code, result) =>
			{
				Action act = () =>
				  {
					  rt_prompt_info.Text += "取出一个元素：\n" + "error_code:"+error_code+"\n"+"result:" + result + "\n";
				  };
				this.Invoke(act);
			});
		}

		private void btn_addorupdate_queue_Click(object sender, EventArgs e)
		{
			NIMChatRoom.ChatRoomApi.QueueOfferAsync(Convert.ToInt64(room_id_), tb_key_.Text, tb_value_.Text, (room_id,error_code) =>
			{
				Action act = () =>
				  {
					  rt_prompt_info.Text += "增加或更新一个队列元素：\n" + "error_code:" + error_code.ToString() + "\n";
				  };
				this.Invoke(act);
			});
		}

		private void BypassLivingStreamForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			NIMChatRoom.ChatRoomApi.Exit(Convert.ToInt64(room_id_));
		}

		private void btn_audio_interact_Click(object sender, EventArgs e)
		{
			if (video_interaction_) return;
			if (!audio_interaction_)
			{
				StringWriter sw = new StringWriter();
				using (JsonWriter jw = new JsonTextWriter(sw))
				{
					//旁路直播的交互协议由上层来定
					jw.WriteStartObject();
					jw.WritePropertyName("style");
					jw.WriteValue(BypassLSCommon.InactionType.kAudio);
					jw.WritePropertyName("info");

					StringWriter sw_temp = new StringWriter();
					using (JsonWriter jw_temp = new JsonTextWriter(sw_temp))
					{
						
						jw_temp.WriteStartObject();
						jw_temp.WritePropertyName("nick");
						jw_temp.WriteValue(" "); 
						jw_temp.WritePropertyName("avater_default");
						jw_temp.WriteValue("avater_default");
					}
					string info = sw_temp.GetStringBuilder().ToString();
					jw.WriteValue(info);
					//jw.WriteEndArray();
					jw.WriteEndObject();
				}
				string ext = sw.GetStringBuilder().ToString();
				audio_interaction_ = true;
				RequestPushMicLink(room_id_, Helper.UserHelper.SelfId, ext, BypassLSCommon.InactionType.kAudio);
			}
			else
			{
				RequestPopMicLink(room_id_, Helper.UserHelper.SelfId);
			}
		}
	}
}
