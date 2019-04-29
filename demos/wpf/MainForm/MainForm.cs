using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NIM;
using NIM.SysMessage;
using NIM.User;
using System.Threading;
using NIM.DataSync;

namespace NIMDemo
{
    enum MainFormExitType
    {
        Logout,
        Exit
    }

    public partial class MainForm : Form
    {
        private static string _selfId;
        private NIM.User.UserNameCard _selfNameCard;
        private readonly Dictionary<string, NIM.Friend.NIMFriendProfile> _friendsDictionary = new Dictionary<string, NIM.Friend.NIMFriendProfile>();
        private readonly HashSet<string> _blacklistSet = new HashSet<string>();
        private readonly HashSet<string> _mutedlistSet = new HashSet<string>();
        private readonly ListViewGroup[] _groups = new ListViewGroup[]
        {
            new ListViewGroup("friend", "好友"), new ListViewGroup("blacklist", "黑名单"), new ListViewGroup("mutedlist", "静音")
        };
        private readonly Team.TeamList _teamList = null;
        private readonly SessionList _sessionList = null;
        private RecentSessionList _recentSessionList = null;
        private MultimediaHandler _multimediaHandler = null;
        private RtsHandler _rtsHandler = null;
        private MainFormExitType _exitType = MainFormExitType.Exit;
        private readonly InvokeActionWrapper _actionWrapper;

        private MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            this.FormClosed += MainForm_FormClosed;
            RegisterClientCallback();
            listView1.ShowGroups = true;
            listView1.Groups.AddRange(_groups);
            _teamList = new Team.TeamList(TeamListView);
            _sessionList = new SessionList(chatListView);
            _recentSessionList = new RecentSessionList(recentSessionListbox);

            this.HandleCreated += MainForm_HandleCreated;
            _actionWrapper = new InvokeActionWrapper(this);
            tabControl1.Selected += TabControl1_Selected;

            NIM.NIMSubscribeApi.RegPushEventCb(OnSubscribedEventChanged);
            NIM.NIMSubscribeApi.RegBatchPushEventCb(OnBatchSubscribedEventChanged);
        }

        public MainForm(string id)
          : this()
        {
            _selfId = id;
            Helper.UserHelper.SelfId = id;
        }

        private void OnBatchSubscribedEventChanged(ResponseCode code, List<NIMEventInfo> infoList)
        {
            foreach (var item in infoList)
                OnSubscribedEventChanged(code, item);
        }

        class OnlineEvent
        {
            [Newtonsoft.Json.JsonProperty("online")]
            public List<NIMClientType> Clients { get; set; } 
        };

        private void OnSubscribedEventChanged(ResponseCode code, NIMEventInfo info)
        {
            this.Invoke(new Action(()=> 
            {
                int state = 0;
                if (code == ResponseCode.kNIMResSuccess)
                {
                    //在线状态事件
                    if (info.Type == 1)
                    {
                        //获取在线客户端列表
                        var t = NimUtility.Json.JsonParser.Deserialize<OnlineEvent>(info.NimConfig);
                        if(t != null && t.Clients.Count > 0)
                        {
                            state = 1;
                        }
                    }
                    UpdateFriendLoginState(info.PublisherID, state);
                }
            }));
        }

        void UpdateFriendLoginState(string id, int state)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var item = listView1.Items[i] as ListViewItem;
                if (item.Group != _groups[0])
                    continue;
                if (item.Name.IndexOf(id) == 0)
                {
                    item.Text = string.Format("{0,-50}{1}", id, state == 1 ? "在线" : "离线");
                }
            }
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if(e.Action == TabControlAction.Selected)
            {
                if(e.TabPageIndex == 1)
                {
                    _teamList.LoadTeams();
                }
            }
        }

        /// <summary>
        /// 注册全局回调
        /// </summary>
        private void RegisterNimCallback()
        {
            NIM.Friend.FriendAPI.FriendProfileChangedHandler += OnFriendChanged;
            NIM.User.UserAPI.UserRelationshipListSyncHander += OnUserRelationshipSync;
            NIM.User.UserAPI.UserRelationshipChangedHandler += OnUserRelationshipChanged;
            NIM.User.UserAPI.UserNameCardChangedHandler += OnUserNameCardChanged;
            NIM.ClientAPI.RegMulitiportPushEnableChangedCb(SyncMultipushState);
            NIM.TalkAPI.OnReceiveMessageHandler += OnReceiveMessage;
            NIM.TalkAPI.RegRecallMessageCallback(OnRecallMessage);
            NIM.TalkAPI.RegReceiveBroadcastCb(OnReceiveBroadcast);
            NIM.TalkAPI.RegReceiveBroadcastMsgsCb(OnReceiveBroadMsgs);
            NIM.SysMessage.SysMsgAPI.ReceiveSysMsgHandler += OnReceivedSysNotification;
            NIM.DataSync.DataSyncAPI.RegCompleteCb(OnDataSyncCompleted);
        }

        private void MainForm_HandleCreated(object sender, EventArgs e)
        {
            NIM.Friend.FriendAPI.GetFriendsList(GetFriendResult);
            NIM.User.UserAPI.GetRelationshipList(GetUserRelationCompleted);
            NIM.User.UserAPI.GetUserNameCard(new List<string>() { SelfId }, (a) =>
            {
                if (a == null || !a.Any())
                    return;
                DisplayMyProfile(a[0]);
            });
            RegisterNimCallback();
            NIM.ClientAPI.IsMultiportPushEnabled(InitMultipushState);
            multipushCheckbox.CheckedChanged += MultipushCheckbox_CheckedChanged;
            //_teamList.LoadTeams();
            _sessionList.GetRecentSessionList();
            _recentSessionList.LoadSessionList();

            _rtsHandler = new RtsHandler(this);

            NIM.TalkAPI.RegReceiveBatchMessagesCb((list) => 
            {
                foreach(var m in list)
                {
                    DisplayReceivedMessage(m.MessageContent);
                }
            });

            this.Text = string.Format("{0}  [{1}]", this.Text, _selfId);

            InitVChatHandler();
        }

		private void InitVChatHandler()
		{
			System.Threading.ThreadPool.QueueUserWorkItem((args) =>
			{
				MultimediaHandler.GetInstance().Init(this);
			});
		}

		/// <summary>
		/// 撤回消息回调
		/// </summary>
		/// <param name="result"></param>
		/// <param name="notify"></param>
		private void OnRecallMessage(ResponseCode result, RecallNotification[] notify)
        {
            DemoTrace.WriteLine("撤回消息通知", result, notify.Dump());
        }

        private void OnDataSyncCompleted(NIMDataSyncType syncType, NIMDataSyncStatus status, string jsonAttachment)
        {
            if (syncType == NIMDataSyncType.kNIMDataSyncTypeTeamInfo)
            {
                _teamList.LoadTeams();
            }
            if(syncType == NIMDataSyncType.kNIMDataSyncTypeTeamUserList)
            {
                NIM.Team.TeamAPI.QueryTeamMembersInfo(jsonAttachment, true, false, (a, b, c, d) => 
                {

                });
            }
        }

        private void MultipushCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (multipushCheckbox.Checked)
                ClientAPI.EnableMultiportPush(SetMultipushState);
            else
                ClientAPI.DisableMultiportPush(SetMultipushState);
        }

        private void SetMultipushState(ResponseCode code, ConfigMultiportPushParam param)
        {
            _actionWrapper.InvokeAction(() =>
            {
                if (code != ResponseCode.kNIMResSuccess)
                {
                    MessageBox.Show("MultiportPush 操作失败:" + code.ToString());
                }
            });
        }

        private void InitMultipushState(ResponseCode code, ConfigMultiportPushParam param)
        {
            _actionWrapper.InvokeAction(() =>
            {
                if (code == ResponseCode.kNIMResSuccess)
                {
                    multipushCheckbox.Checked = param.Enabled;
                }
            });
        }

        private void SyncMultipushState(ResponseCode code, ConfigMultiportPushParam param)
        {
            _actionWrapper.InvokeAction(() =>
            {
                if (code == ResponseCode.kNIMResSuccess)
                {
                    multipushCheckbox.Checked = param.Enabled;
                }
                else
                {
                    MessageBox.Show("MultiportPush 操作失败:" + code.ToString());
                }
            });
        }

        private bool _notifyNetworkDisconnect = true;
        private bool _beKicked = false;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _notifyNetworkDisconnect = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_exitType == MainFormExitType.Exit)
            {
                    //退出前需要结束音视频设备，防止错误的数据上报
                    MultimediaHandler.EndDevices();
                    System.Threading.Thread.Sleep(500);


				if (!_beKicked)
                {
                    System.Threading.Semaphore s = new System.Threading.Semaphore(0, 1);
                    NIM.ClientAPI.Logout(NIM.NIMLogoutType.kNIMLogoutAppExit, (r) =>
                    {
                        s.Release();
                    });
                    //需要logout执行完才能退出程序
                    s.WaitOne(TimeSpan.FromSeconds(10));
                }
                Application.Exit();
            }
        }

        void GetFriendResult(NIM.Friend.NIMFriends ps)
        {
            Action action = UpdateFriendListView;
            if (ps == null)
            {
                return;
            }
            foreach (var item in ps.ProfileList)
            {
                _friendsDictionary[item.AccountId] = item;
            }
            _actionWrapper.InvokeAction(action);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.tabControl1.TabCount; i++)
            {
                //激活tabpage
                this.tabControl1.SelectTab(i);
            }
            this.tabControl1.SelectTab(0);
        }

        private void OnReceivedSysNotification(object sender, NIMSysMsgEventArgs e)
        {
            if (e.Message == null || e.Message.Content == null)
                return;
            DemoTrace.WriteLine("系统通知:" + e.Dump());
            if (e.Message.Content.MsgType == NIMSysMsgType.kNIMSysMsgTypeTeamInvite)
            {
                NIM.Team.TeamAPI.AcceptTeamInvitation(e.Message.Content.ReceiverId, e.Message.Content.SenderId, (x) =>
                {

                });
            }
            if(e.Message.Content.MsgType == NIMSysMsgType.kNIMSysMsgTypeFriendAdd)
            {
                var vt = Newtonsoft.Json.JsonConvert.DeserializeObject<FriendRequestVerify>(e.Message.Content.Attachment);
                //if(vt.VT == NIM.Friend.NIMVerifyType.kNIMVerifyTypeAsk)
                //    NIM.Friend.FriendAPI.ProcessFriendRequest(e.Message.Content.SenderId, NIM.Friend.NIMVerifyType.kNIMVerifyTypeReject, "sssssss", null);
            }
        }

        void DisplayReceivedMessage(NIMIMMessage msg)
        {
            var sid = msg.SenderID;
            var msgType = msg.MessageType;
            Action action = () =>
            {
                ListViewItem item = new ListViewItem(sid);
                if (msgType != NIMMessageType.kNIMMessageTypeText)
                    item.SubItems.Add(msgType.ToString());
                else
                {
                    var m = msg as NIM.NIMTextMessage;
                    item.SubItems.Add(m.TextContent);
                }
                item.Tag = msg;
                chatListView.Items.Add(item);
            };
            _actionWrapper.InvokeAction(action);
        }

        void OnReceiveMessage(object sender, NIMReceiveMessageEventArgs args)
        {
            DisplayReceivedMessage(args.Message.MessageContent);
            DemoTrace.WriteLine(args.Dump());
            if(args.Message.MessageContent.SessionType == NIM.Session.NIMSessionType.kNIMSessionTypeTeam)
            {
                var tid = args.Message.MessageContent.ReceiverID;
                var msgs = new List<NIMIMMessage> { args.Message.MessageContent };
                NIM.Team.TeamAPI.MsgAckRead(tid, msgs, (data) => 
                {

                });
            }
        }

        private void OnReceiveBroadMsgs(List<NIMBroadcastMessage> msg)
        {
            DemoTrace.WriteLine(msg.Dump());
        }

        private void OnReceiveBroadcast(NIMBroadcastMessage msg)
        {
            DemoTrace.WriteLine(msg.Dump());
        }

        private void OnUserNameCardChanged(object sender, UserNameCardChangedArgs e)
        {
            if (e != null && e.UserNameCardList != null)
            {
                DemoTrace.WriteLine("用户名片变更:" + e.UserNameCardList.Dump());
                var card = e.UserNameCardList.Find(c => c.AccountId == SelfId);
                DisplayMyProfile(card);
            }
        }

        private void OnUserRelationshipChanged(object sender, UserRelationshipChangedArgs e)
        {
            HashSet<string> tmp = e.ChangedType == NIMUserRelationshipChangeType.AddRemoveBlacklist ? _blacklistSet : _mutedlistSet;
            if (e.IsSetted)
                tmp.Add(e.AccountId);
            else
                tmp.Remove(e.AccountId);
            UpdateRelationshipView();
        }

        private void GetUserRelationCompleted(ResponseCode code, UserSpecialRelationshipItem[] list)
        {
            FillRelationshipSet(list);
        }

        private void OnUserRelationshipSync(object sender, UserRelationshipSyncArgs e)
        {
            FillRelationshipSet(e.Items);
        }

        void FillRelationshipSet(NIM.User.UserSpecialRelationshipItem[] items)
        {
            if (items == null)
                return;
            foreach (var item in items)
            {
                if (item.IsBlacklist)
                    _blacklistSet.Add(item.AccountId);
                if (item.IsMuted)
                    _mutedlistSet.Add(item.AccountId);
            }
            UpdateRelationshipView();
        }

        void OnFriendChanged(object sender, NIM.Friend.NIMFriendProfileChangedArgs args)
        {
            if (args.ChangedInfo == null)
                return;
            Action action = () =>
            {
                if (args.ChangedInfo.ChangedType == NIM.Friend.NIMFriendChangeType.kNIMFriendChangeTypeDel)
                {
                    var info = args.ChangedInfo as NIM.Friend.FriendDeletedInfo;
                    _friendsDictionary.Remove(info.AccountId);
                }
                if (args.ChangedInfo.ChangedType == NIM.Friend.NIMFriendChangeType.kNIMFriendChangeTypeRequest)
                {
                    var info = args.ChangedInfo as NIM.Friend.FriendRequestInfo;
                    if (info.VerifyType == NIM.Friend.NIMVerifyType.kNIMVerifyTypeAdd ||
                        info.VerifyType == NIM.Friend.NIMVerifyType.kNIMVerifyTypeAgree)
                    {
                        _friendsDictionary[info.AccountId] = new NIM.Friend.NIMFriendProfile() { AccountId = info.AccountId };
                    }
                }
                if (args.ChangedInfo.ChangedType == NIM.Friend.NIMFriendChangeType.kNIMFriendChangeTypeSyncList)
                {
                    var info = args.ChangedInfo as NIM.Friend.FriendListSyncInfo;
                    foreach (var i in info.ProfileCollection)
                    {

                        _friendsDictionary[i.AccountId] = i;
                    }
                }
                if (args.ChangedInfo.ChangedType == NIM.Friend.NIMFriendChangeType.kNIMFriendChangeTypeUpdate)
                {
                    var info = args.ChangedInfo as NIM.Friend.FriendUpdatedInfo;
                    _friendsDictionary[info.Profile.AccountId] = info.Profile;
                }
                UpdateFriendListView();
            };
            _actionWrapper.InvokeAction(action);
        }

        void UpdateFriendListView()
        {
            listView1.BeginUpdate();
            foreach (var pair in _friendsDictionary)
            {
                if (listView1.Items.ContainsKey(pair.Key + "_0"))
                {

                }
                else
                {
                    AddFriendListItem(pair.Value);
                }
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var item = listView1.Items[i] as ListViewItem;
                if (item.Group != _groups[0])
                    continue;
                if (!_friendsDictionary.ContainsKey(item.Tag.ToString()))
                {
                    listView1.Items.RemoveByKey(item.Name);
                    NIM.NIMSubscribeApi.UnSubscribe(1, new List<string> { item.Tag.ToString() }, null);
                }
            }
            listView1.EndUpdate();
        }

        void UpdateRelationshipView()
        {
            Action action = () =>
            {
                UpdateRelationshipView(_blacklistSet, "1");
                UpdateRelationshipView(_mutedlistSet, "2");
            };
            _actionWrapper.InvokeAction(action);
        }

        void UpdateRelationshipView(HashSet<string> set, string group)
        {
            listView1.BeginUpdate();
            foreach (var key in set)
            {
                NIM.Friend.NIMFriendProfile profile = null;
                if (_friendsDictionary.ContainsKey(key))
                    profile = _friendsDictionary[key];
                else
                {
                    profile = new NIM.Friend.NIMFriendProfile();
                    profile.AccountId = key;
                }
                if (listView1.Items.ContainsKey(key + "_" + group))
                {
                    listView1.Items.RemoveByKey(key + "_" + group);
                }
                AddFriendListItem(profile, int.Parse(group));
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var item = listView1.Items[i] as ListViewItem;
                if (item.Group != _groups[int.Parse(group)])
                    continue;
                if (!set.Contains(item.Tag.ToString()))
                    listView1.Items.RemoveByKey(item.Name);
            }
            listView1.EndUpdate();
        }

        void AddFriendListItem(NIM.Friend.NIMFriendProfile profile, int group = 0)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.Add(profile.AccountId);
            item.Text = profile.AccountId;
            item.Tag = profile.AccountId;
            item.Name = string.Format("{0}_{1}", profile.AccountId, group);
            item.Group = _groups[group];
            listView1.Items.Add(item);
            UpdateFriendLoginState(profile.AccountId, 2);
            NIM.NIMSubscribeApi.Subscribe(1, 24 * 60 * 60, NIMEventSubscribeSyncEventType.kNIMEventSubscribeSyncTypeSync, new List<string> { profile.AccountId },
                (ResponseCode code, int type, List<string> failedIDList) =>
                {

                });
        }

        private void RegisterClientCallback()
        {
            NIM.ClientAPI.RegDisconnectedCb(() =>
            {
                if (_notifyNetworkDisconnect)
                    MessageBox.Show("网络连接断开，网络恢复后后会自动重连");
            });

            NIM.ClientAPI.RegKickoutCb((r) =>
            {
                _beKicked = true;
                DemoTrace.WriteLine(r.Dump());
                MessageBox.Show(r.Dump(), "被踢下线，请注意账号安全");
                Action action = () =>
                {
                    LogoutBtn_Click(null, null);
                };
                _actionWrapper.InvokeAction(action);
            });

            NIM.ClientAPI.RegAutoReloginCb((r) =>
            {
                if (r.Code == ResponseCode.kNIMResSuccess && r.LoginStep == NIMLoginStep.kNIMLoginStepLogin)
                {
                    MessageBox.Show("重连成功");
                }
            });
            ClientCallbacks.Register();
        }

        void DisplayMyProfile(NIM.User.UserNameCard card)
        {
            if (card == null)
                return;
            _selfNameCard = card;
            _actionWrapper.InvokeAction(() =>
            {
                IDLabel.Text = card.AccountId;
                NameLabel.Text = card.NickName;
                SigLabel.Text = card.Signature;

                ThreadPool.QueueUserWorkItem((arg) => 
                {
                    if (!string.IsNullOrEmpty(card.IconUrl))
                    {
                        var url = Uri.UnescapeDataString(card.IconUrl);
                        if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                        {
                            try
                            {
                                var stream = System.Net.WebRequest.Create(card.IconUrl).GetResponse().GetResponseStream();
                                _actionWrapper.InvokeAction(() =>
                                {
                                    if (stream != null)
                                        IconPictureBox.Image = Image.FromStream(stream);
                                });
                            }
                            catch(Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine("set user icon failed:%s", e.Message);
                            }
                           
                            
                        }
                    }
                });
            });
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var x = listView1.SelectedItems;
            if (x != null && x.Count > 0)
            {
                string id = x[0].Tag.ToString();
                new ChatForm(id).Show();
            }
        }

        public static string SelfId
        {
            get { return _selfId; }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            //if (!_beKicked)
            {
                _notifyNetworkDisconnect = false;
                NIM.ClientAPI.Logout(NIM.NIMLogoutType.kNIMLogoutChangeAccout, (r) =>
                {
                    Thread thread = new Thread(new ThreadStart(OnLogoutWithChangeAcountModeFinished));
                    thread.Start();
                });
            }
        }

        private void OnLogoutWithChangeAcountModeFinished()
        {
            NIM.Friend.FriendAPI.FriendProfileChangedHandler -= OnFriendChanged;
            NIM.User.UserAPI.UserRelationshipListSyncHander -= OnUserRelationshipSync;
            NIM.User.UserAPI.UserRelationshipChangedHandler -= OnUserRelationshipChanged;
            NIM.User.UserAPI.UserNameCardChangedHandler -= OnUserNameCardChanged;

            NIM.TalkAPI.OnReceiveMessageHandler -= OnReceiveMessage;
            NIM.SysMessage.SysMsgAPI.ReceiveSysMsgHandler -= OnReceivedSysNotification;

			Action action = ChangeAccount;
            _actionWrapper.InvokeAction(action);
        }

        void ChangeAccount()
        {
            this.Hide();
            LoginForm.Instance.Show();
            CloseForm(MainFormExitType.Logout);
        }

        //好友列表右键菜单
        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            ContextMenu cm = new ContextMenu();
            if (listView1.SelectedItems.Count > 0)
            {
                var id = listView1.SelectedItems[0].Tag.ToString();

                cm.MenuItems.Add("查看详情", (s, args) =>
                {
                    new FriendProfileForm(id).Show();
                });

                cm.MenuItems.Add("删除", (s, args) =>
                {
                    NIM.Friend.FriendAPI.DeleteFriend(id, (a, b, c) =>
                    {
                        if (a != 200)
                        {
                            MessageBox.Show("删除失败");
                        }
                    });
                });

                cm.MenuItems.Add("删除(删除备注)", (s, args) => 
                {
                    NIM.Friend.FriendAPI.DeleteFriend(id, true, (a, b, c) => 
                    {
                        MessageBox.Show("DeleteFriend result" + a.ToString());
                    });
                });

                bool isBlacklist = _blacklistSet.Contains(id);
                string m3 = isBlacklist ? "取消黑名单" : "设置黑名单";

                cm.MenuItems.Add(m3, (o, ex) =>
                {
                    NIM.User.UserAPI.SetBlacklist(id, !isBlacklist, (a, b, c, d, e1) =>
                    {

                    });
                });

                bool muted = _mutedlistSet.Contains(id);
                string m4 = muted ? "取消静音" : "静音";
                cm.MenuItems.Add(m4, (o, ex) =>
                {
                    NIM.User.UserAPI.SetUserMuted(id, !muted, (a, b, c, d, e1) =>
                    {

                    });
                });

                cm.MenuItems.Add("是否好友", (o, ex) =>
                {
                    var ret = NIM.Friend.FriendAPI.IsActiveFriend(id);
                    DemoTrace.WriteLine("{0} is friend:{1}", id, ret);
                });
            }
            else
            {
                var item = CreateAddFriendMenuItem();
                cm.MenuItems.Add(item);
            }
            cm.Show(listView1, e.Location);
        }

        MenuItem CreateAddFriendMenuItem()
        {
            MenuItem item = new MenuItem("添加好友", (s, args) =>
            {
                Form form = new Form();
                form.Size = new Size(300, 200);
                TextBox box = new TextBox();
                box.Location = new Point(10, 10);
                box.Size = new Size(160, 50);
                Button b = new Button();
                b.Location = new Point(70, 70);
                b.Text = "添加";
                form.Controls.Add(box);
                form.Controls.Add(b);
                b.Click += (ss, a) =>
                {
                    if (!string.IsNullOrEmpty(box.Text))
                    {
                        NIM.Friend.FriendAPI.ProcessFriendRequest(box.Text, NIM.Friend.NIMVerifyType.kNIMVerifyTypeAdd, "加我加我",
                            (aa, bb, cc) =>
                            {
                                if (aa != 200)
                                {
                                    MessageBox.Show("添加失败:" + aa.ToString());
                                }
                            });
                    }
                };
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            });
            return item;
        }

        void DownCallback(int a, string b, string c, string d)
        {

        }


        private void MyProfileBtn_Click(object sender, EventArgs e)
        {
            ObjectPropertyInfoForm form = new ObjectPropertyInfoForm();
            form.Text = "我的信息";
            form.TargetObject = _selfNameCard;
            form.UpdateObjectAction = (o) =>
            {
                NIM.User.UserAPI.UpdateMyCard(o as UserNameCard, (a) =>
                {
                    if (a == ResponseCode.kNIMResSuccess)
                    {
                        UserAPI.GetUserNameCard(new List<string>() { SelfId }, (ret) =>
                          {
                              if (ret.Any())
                              {
                                  _selfNameCard = ret[0];
                                  DisplayMyProfile(_selfNameCard);
                              }
                          });
                    }
                });
            };
            form.Show();
        }

        private void chatRoomBtn_Click(object sender, EventArgs e)
        {
            var appkey = NimUtility.ConfigReader.GetAppKey();
            NIMChatRoom.AvailableRooms s = new NIMChatRoom.AvailableRooms(appkey);
            var list = s.Search();
            ChatRoomListForm form = new ChatRoomListForm(list);
            form.Show();

        }

        private void CloseForm(MainFormExitType exitType)
        {
            this._exitType = exitType;
            this.Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            CloseForm(MainFormExitType.Exit);
        }

        private void sysMsgBtn_Click(object sender, EventArgs e)
        {
            new SysMsgForm().Show();
        }

        private void btn_livingstream_Click(object sender, EventArgs e)
        {
#if WIN32
			new LivingStreamForm().Show();
#elif WIN64
            MessageBox.Show("目前直播暂时不支持64位");
#endif
        }

        private void OnMenuClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //语音
            if (e.ClickedItem.MergeIndex == 0)
            {
                Audio.AudioForm form = new Audio.AudioForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            //白板
            if (e.ClickedItem.MergeIndex == 1)
            {
                Rts.RtsConference conf = new Rts.RtsConference();
                conf.StartPosition = FormStartPosition.CenterScreen;
                conf.Show();
            }
            if (e.ClickedItem.MergeIndex == 2)
            {
                MessageBox.Show(NIM.ClientAPI.GetLoginState().ToString());
            }
            //http
            if (e.ClickedItem.MergeIndex == 3)
            {
                Http.HttpForm form = new Http.HttpForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            //DocTrans
            if (e.ClickedItem.MergeIndex == 4)
            {
                Http.DocTransForm form = new Http.DocTransForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            if (e.ClickedItem.MergeIndex == 5)
            {
                NIM.VChatAPI.DetectNetwork(null, OnNetDetection);
            }
            if (e.ClickedItem.MergeIndex == 7)
            {
                RobotForm form = new RobotForm();
                form.Show();
            }
        }

        private void OnNetDetection(bool ret, int code, string json_extension)
        {
            DemoTrace.WriteLine(string.Format("网络测试:{0} code = {1},result = {2}", ret, code, json_extension));
        }

        private void EventSubsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void NetDetectClicked(object sender, EventArgs e)
        {
            NIMVChatNetDetectJsonEx json = new NIMVChatNetDetectJsonEx();
            json.DetectTime = 10000;
            json.DetectType =0;
            
            NIM.VChatAPI.DetectNetwork(json, OnNetDetection);
        }

        private void Btn_Signaling_Click(object sender, EventArgs e)
        {
            SignalingForm signalingForm = new SignalingForm();
            signalingForm.Show();
        }

        private void Btn_Msg_Export_Click(object sender, EventArgs e)
        {
            MessageExportForm msgExportForm = new MessageExportForm();
            msgExportForm.Show();
        }
    }
}
