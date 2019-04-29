using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.Team
{
    public partial class TeamMembersForm : Form
    {
        public TeamMembersForm()
        {
            InitializeComponent();
            
        }

        private HashSet<string> _memberCollection = new HashSet<string>();

        private Dictionary<string, NIM.Team.NIMTeamMemberInfo> _teamMmebers = new Dictionary<string, NIM.Team.NIMTeamMemberInfo>();
        private string _teamId;
        public TeamMembersForm(string tid)
            : this()
        {
            _teamId = tid;
            this.Load += TeamMembersForm_Load;
        }

        private void TeamMembersForm_Load(object sender, EventArgs e)
        {
            treeView1.MouseUp += TreeView1_MouseUp;
            NIM.Team.TeamAPI.QueryTeamMembersInfo(_teamId, (tid,coutn,hasUinfo,info) =>
            {
                if (info != null)
                {
                    foreach (var i in info)
                    {
                        if(i.Type != NIM.Team.NIMTeamUserType.kNIMTeamUserTypeLocalWaitAccept)
                        {
                            _memberCollection.Add(i.AccountId);
                            _teamMmebers[i.AccountId] = i;
                        }
                      
                    }
                    UpdateTreeView();
                }
            });
        }

        private void TreeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (treeView1.Nodes.Count == 0)
                return;
            var root = treeView1.Nodes[0];
            if (e.Button == MouseButtons.Left)
            {
                var node = treeView1.SelectedNode;
                if (node != null && node.Level > 0)
                {
                    NIM.Team.TeamAPI.QuerySingleMemberInfo(_teamId, node.Name, (info) =>
                    {
                        Action action = () =>
                        {
                            this.flowLayoutPanel1.Controls.Clear();
                            ObjectPropertyInfoForm.CreateFormContent(info, this.flowLayoutPanel1);
                        };
                        this.Invoke(action);
                    });
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                List<TreeNode> treeNodes = new List<TreeNode>();
                foreach (var n in root.Nodes)
                {
                    TreeNode nd = n as TreeNode;
                    if (nd.Checked)
                    {
                        treeNodes.Add(nd);
                    }
                }
                if (treeNodes.Count == 1)
                {
                    var node = treeNodes[0];
                    MenuItem menu = new MenuItem("移出群", (s, args) =>
                    {
                        NIM.Team.TeamAPI.KickMemberOutFromTeam(_teamId, new string[] { node.Name }, (a) =>
                          {
                              if (a.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                              {
                                  foreach (var id in a.TeamEvent.IdCollection)
                                  {
                                      _memberCollection.Remove(id);
                                      _teamMmebers.Remove(id);
                                  }
                                  UpdateTreeView();
                              }
                              else
                              {
                                  MessageBox.Show("操作失败:" + a.TeamEvent.ResponseCode.ToString());
                              }
                          });
                    });
                    ContextMenu m = new ContextMenu();
                    string uid = node.Name;
                    if (_teamMmebers.ContainsKey(uid))
                    {
                        var memInfo = _teamMmebers[uid];
                        bool normalMember = memInfo.Type == NIM.Team.NIMTeamUserType.kNIMTeamUserTypeNomal;
                        string txt = normalMember ? "设为管理员" : "取消管理员";
                        MenuItem item = new MenuItem(txt, (s, args) =>
                        {
                            if (normalMember)
                                NIM.Team.TeamAPI.AddTeamManagers(_teamId, new string[] { uid }, (ret) =>
                                  {
                                      DemoTrace.WriteLine(txt, ret.Dump());
                                  });
                            else
                                NIM.Team.TeamAPI.RemoveTeamManagers(_teamId, new string[] { uid }, (ret) =>
                                  {
                                      DemoTrace.WriteLine(txt, ret.Dump());
                                  });
                        });
                        var text1 = memInfo.IsMuted ? "取消禁言" : "禁言";
                        MenuItem item1 = new MenuItem(text1, (s, args) =>
                        {
                            if (memInfo.IsMuted)
                            {
                                NIM.Team.TeamAPI.SetMemberMuted(_teamId, uid, false, (ret) =>
                                {
                                    DemoTrace.WriteLine(text1, ret.Dump());
                                    if (ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                                    {
                                        memInfo.SetMuted(false);
                                        _teamMmebers[uid] = memInfo;
                                    }
                                });
                            }
                            else
                            {
                                NIM.Team.TeamAPI.SetMemberMuted(_teamId, uid, true, (ret) =>
                                {
                                    DemoTrace.WriteLine(text1, ret.Dump());
                                    if (ret.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                                    {
                                        memInfo.SetMuted(true);
                                        _teamMmebers[uid] = memInfo;
                                    }
                                });
                            }
                        });
                        MenuItem item2 = new MenuItem("修改群昵称", (s, args) =>
                        {
                            var member = NIM.Team.TeamAPI.QuerySingleMemberInfo(_teamId, uid);
                            member.NickName = "nickname:" + new Random().Next();
                            NIM.Team.TeamAPI.UpdateMemberNickName(member, null);
                        });
                        m.MenuItems.Add(item);
                        var myInfo = _teamMmebers[MainForm.SelfId];
                        if (myInfo.Type == NIM.Team.NIMTeamUserType.kNIMTeamUserTypeCreator || myInfo.Type == NIM.Team.NIMTeamUserType.kNIMTeamUserTypeManager)
                        {
                            m.MenuItems.Add(item1);
                            m.MenuItems.Add(item2);
                        }
                    }
                    m.MenuItems.Add(menu);
                    m.Show(treeView1, e.Location);
                }
                else if (treeNodes.Count > 1)
                {
                    MenuItem menu = new MenuItem("群推送测试", (s, args) =>
                    {
                        NIM.TeamForecePushMessage forceMsg = new NIM.TeamForecePushMessage();
                        forceMsg.Content = string.Format("群推送测试:" + DateTime.Now.ToLongTimeString());
                        if (treeNodes.Count < root.Nodes.Count)
                        {
                            forceMsg.ReceiverList = new List<string>();
                            foreach (TreeNode t in treeNodes)
                                forceMsg.ReceiverList.Add(t.Text);
                        }
                        NIM.NIMTextMessage msg = new NIM.NIMTextMessage();
                        msg.ReceiverID = root.Text;
                        msg.SessionType = NIM.Session.NIMSessionType.kNIMSessionTypeTeam;
                        msg.TextContent = "群推送消息..." + new Random().NextDouble().ToString();
                        NIM.TalkAPI.SendTeamFrocePushMessage(msg, forceMsg);
                    });
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(menu);
                    m.Show(treeView1, e.Location);
                }
                else
                {
                    MenuItem menu = new MenuItem("邀请入群", (s, args) =>
                    {
                        CreateTeamForm form = new CreateTeamForm(FormType.InviteMemeber);
                        form.MembersIDSelected = (n, i) =>
                        {
                            NIM.Team.TeamAPI.Invite(_teamId, i, n, (r) =>
                            {
                                if (r.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResTeamInviteSuccess)
                                {
                                    MessageBox.Show("邀请成功，等待对方验证: " + r.TeamEvent.ResponseCode.ToString());
                                }
                                else if(r.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess)
                                {
                                    foreach (var id in r.TeamEvent.IdCollection)
                                    {
                                        NIM.Team.TeamAPI.QuerySingleMemberInfo(_teamId, id, (ret) =>
                                        {
                                            _teamMmebers[id] = ret;
                                        });
                                    }
                                    UpdateTreeView();
                                }
                                else
                                {
                                    MessageBox.Show("邀请失败：" + r.TeamEvent.ResponseCode.ToString());
                                }
                            });
                        };
                        form.Show();
                    });
                    ContextMenu m = new ContextMenu();

                    m.MenuItems.Add(menu);
                    m.Show(treeView1, e.Location);
                }
            }
        }

        private TreeNode _rootNode = null;
        void UpdateTreeView()
        {
            Action action = () =>
            {
                treeView1.BeginUpdate();
                if (_rootNode == null)
                {
                    _rootNode = new TreeNode(_teamId);
                    treeView1.Nodes.Add(_rootNode);
                }
                foreach (var i in _memberCollection)
                {
                    if (!_rootNode.Nodes.Find(i, true).Any())
                    {
                        TreeNode node = new TreeNode(i);
                        node.Name = i;
                        _rootNode.Nodes.Add(node);
                    }
                }

                for(int i = 0;i< _rootNode.Nodes.Count;i++)
                {
                    TreeNode tn = _rootNode.Nodes[i];
                    if (!_memberCollection.Contains(tn.Name))
                    {
                        _rootNode.Nodes.RemoveByKey(tn.Name);
                    }
                }
                treeView1.EndUpdate();
            };
           
            this.Invoke(action);
        }
    }
}
