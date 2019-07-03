using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;
using NIM.Messagelog;
using NIM.Team;

namespace NIMDemo.Team
{
    public class TeamList
    {
        private readonly ListView _teamListView = null;
        private readonly InvokeActionWrapper _actionWrapper;

        public TeamList(ListView list)
        {
            _teamListView = list;
            _teamListView.MouseUp += _teamListView_MouseUp;
            _teamListView.DoubleClick += _teamListView_DoubleClick;
            NIM.Team.TeamAPI.TeamEventNotificationHandler += OnTeamEventNotify;
            _actionWrapper = new InvokeActionWrapper(list);
            list.View = View.Details;
        }

        private void _teamListView_DoubleClick(object sender, EventArgs e)
        {
            if (_teamListView.SelectedItems.Count > 0)
            {
                var item = _teamListView.SelectedItems[0];
                new ChatForm(item.Name, NIM.Session.NIMSessionType.kNIMSessionTypeTeam).Show();
            }
        }

        private void OnTeamEventNotify(object sender, NIMTeamEventArgs e)
        {
            if (e.Data.TeamEvent!=null&&e.Data.TeamEvent.NotificationType == NIMNotificationType.kNIMNotificationIdLocalGetTeamList)
            {
                string tid = e.Data.TeamEvent.TeamId;
                AddTeamItem(tid);
            }
        }

        public void LoadTeams()
        {
            NIM.Team.TeamAPI.QueryAllMyTeamsInfo((list) =>
            {
                _actionWrapper.InvokeAction(()=> 
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count(); i++)
                        {
                            UpdateTeamItem(list[i]);
                        }
                    }
                });
            });
        }

        private void _teamListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            var items = _teamListView.SelectedItems;
            ContextMenu contextMenu = new ContextMenu();
            if (items.Count == 0)
            {
                MenuItem mi = new MenuItem("创建群", (s, args) =>
                {
                    CreateTeamForm f = new CreateTeamForm(FormType.CreateTeam);
                    f.MembersIDSelected = (n, ids) =>
                    {
                        NIM.Team.NIMTeamInfo i = new NIM.Team.NIMTeamInfo();
                        i.TeamType = NIMTeamType.kNIMTeamTypeNormal;
                        i.Name = n;
                        
                        NIM.Team.TeamAPI.CreateTeam(i, ids, "", (ret) =>
                        {
                            if (ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                            {
                                AddTeamItem(ret.TeamEvent.TeamId);
                                if(ret.TeamEvent.InvalidIDList != null && ret.TeamEvent.InvalidIDList.Count > 0)
                                {
                                    var failed = ret.TeamEvent.InvalidIDList.Aggregate((a, b) => { return a + "  " + b; });
                                    MessageBox.Show(string.Format("{0} 加入群 {1} 失败", failed, ret.TeamEvent.TeamId));
                                }
                            }
                            else
                            {
                                MessageBox.Show("创建群失败:" + ret.TeamEvent.ResponseCode.ToString());
                            }
                        });
                    };
                    f.Show();
                });
                MenuItem mi2 = new MenuItem("创建高级群", (s, args) =>
                {
                    CreateAdvancedTeamForm f = new CreateAdvancedTeamForm(this);
                    f.Show();
                });

                MenuItem mi3 = new MenuItem("查询自己在群中的信息", (s, args) => 
                {
                    NIM.Team.TeamAPI.QueryMyInfoInEachTeam((list) => 
                    {

                    });
                });

                contextMenu.MenuItems.Add(mi);
                contextMenu.MenuItems.Add(mi2);
                contextMenu.MenuItems.Add(mi3);
            }
            else
            {
                var tid = items[0].Name;
                var tinfo = NIM.Team.TeamAPI.QueryCachedTeamInfo(tid);
                if (tinfo.TeamType == NIMTeamType.kNIMTeamTypeAdvanced)
                {
                    var mt = new MenuItem("解散群", (s, args) =>
                     {
                         NIM.Team.TeamAPI.DismissTeam(tid, (ret) =>
                         {
                             if (ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                             {
                                 RemoveTeamItem(tid);
                             }
                             else
                             {
                                 MessageBox.Show("解散群失败:" + ret.TeamEvent.ResponseCode.ToString());
                             }
                         });
                     });
                    contextMenu.MenuItems.Add(mt);
                }

                var m1 = new MenuItem("退群", (s, args) =>
                {
                    NIM.Team.TeamAPI.LeaveTeam(tid, (ret) =>
                    {
                        if (ret != null && ret.TeamEvent != null && ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                        {
                            RemoveTeamItem(tid);
                        }
                        else
                        {
                            MessageBox.Show("退出群失败:" + ret.TeamEvent.ResponseCode.ToString());
                        }
                    });
                });

                MenuItem m3 = new MenuItem("群信息", (s, args) =>
                {
                    Action<NIM.Team.NIMTeamInfo, string> action = (info, text) =>
                    {
                        ObjectPropertyInfoForm form = new ObjectPropertyInfoForm();
                        form.TargetObject = info;
                        form.Text = text;
                        form.UpdateObjectAction = (obj) =>
                        {
                            var newInfo = obj as NIMTeamInfo;
                            var tmp = newInfo;// new NIMTeamInfo();
                            //tmp.TeamId = newInfo.TeamId;
                            //tmp.Name = newInfo.Name;
                            //tmp.Announcement = newInfo.Announcement;
                            //tmp.Custom = newInfo.Custom;
                            //tmp.TeamType = newInfo.TeamType;
                            //tmp.MuteAll = newInfo.MuteAll;
                            NIM.Team.TeamAPI.UpdateTeamInfo(tid, tmp, (ret) =>
                            {
                                if (ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                                {
                                    UpdateTeamItem(ret.TeamEvent.TeamInfo);
                                }
                            });
                        };
                        form.Show();
                    };
                    Action<NIM.Team.NIMTeamInfo, string> show = (info, text) =>
                    {
                        _actionWrapper.InvokeAction(action, info, text);
                    };
                    NIM.Team.TeamAPI.QueryCachedTeamInfo(tid, (id, info) =>
                    {
                        show(info, "本地群信息");
                    });
                    NIM.Team.TeamAPI.QueryTeamInfoOnline(tid, (info) =>
                    {
                        show(info.TeamEvent.TeamInfo, "在线群信息");
                    });
                });
                MenuItem m4 = new MenuItem("群成员", (s, args) =>
                {
                    new TeamMembersForm(tid).Show();
                });

                MenuItem m5 = new MenuItem("聊天记录", (s, args) =>
                {
                    NIM.Messagelog.MessagelogAPI.QueryMsglogOnline(tid, NIM.Session.NIMSessionType.kNIMSessionTypeTeam, 10, 0, 0, 0, false, false,
                        (ResponseCode code, string accountId, NIM.Session.NIMSessionType sType, MsglogQueryResult result) =>
                        {
                            DemoTrace.WriteLine(result.Dump());
                        });
                });

                MenuItem m6 = new MenuItem("获取禁言列表", (s, args) =>
                {
                    NIM.Team.TeamAPI.QueryMutedListOnlineAsync(tid, (res, count, id, members) =>
                    {
                        DemoTrace.WriteLine("禁言列表:", res, count, id, members.Dump());
                    });
                });
                contextMenu.MenuItems.AddRange(new MenuItem[] { m1, m3, m4, m5, m6 });
            }
            contextMenu.Show(_teamListView, e.Location);
        }

        void UpdateTeamItem(NIM.Team.NIMTeamInfo info)
        {
            if (info == null || !info.TeamValid)
                return;
            _actionWrapper.InvokeAction(() => 
            {
                var items = _teamListView.Items.Find(info.TeamId, true);
                if (items != null && items.Any())
                {
                    foreach (var i in items)
                        i.SubItems[1].Text = info.Name;
                }
                else
                {
                    ListViewItem item = new ListViewItem(info.TeamId);
                    item.Name = info.TeamId;
                    item.SubItems.Add(info.Name);
                    _teamListView.Items.Add(item);
                   
                }
            });
        }

        public void AddTeamItem(string tid)
        {
            NIM.Team.TeamAPI.QueryCachedTeamInfo(tid, (id, ret) =>
            {
                UpdateTeamItem(ret);
            });
        }

        void RemoveTeamItem(string tid)
        {
            Action action = () =>
            {
                var index = _teamListView.Items.IndexOfKey(tid);
                if (index >= 0)
                    _teamListView.Items.RemoveAt(index);
            };
            _actionWrapper.InvokeAction(action);
        }
    }
   
}
