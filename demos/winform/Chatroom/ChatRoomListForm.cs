using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIMChatRoom;

namespace NIMDemo
{
    public partial class ChatRoomListForm : Form
    {
        private readonly ListViewGroup[] _groups = new ListViewGroup[] {new ListViewGroup("solid", "固定成员"), new ListViewGroup("temp", "临时成员")};
        private ChatRoomListForm()
        {
            InitializeComponent();
            this.Load += ChatRoomListForm_Load;
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            NIMChatRoom.ChatRoomApi.Init();
            this.FormClosed += ChatRoomListForm_FormClosed;
            membersListview.Groups.AddRange(_groups);
            membersListview.MouseUp += MembersListview_MouseUp;
        }

        private void MembersListview_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (membersListview.SelectedItems.Count > 0)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem item1 = new MenuItem("临时禁言", (o, args) =>
                    {
                        var accid = membersListview.SelectedItems[0].Name;
                        ChatRoomApi.TempMuteMember((long) membersListview.Tag, accid, 120,
                            (a, b, c) =>
                            {
                                DemoTrace.WriteLine(new {Op = "临时禁言", RoomId = a, Result = b, Member = c}.Dump());
                            }, true, accid + " 被禁言120s");
                    });
                    MenuItem item2 = new MenuItem("解除临时禁言", (o, args) =>
                    {
                        var accid = membersListview.SelectedItems[0].Name;
                        ChatRoomApi.TempMuteMember((long)membersListview.Tag, accid, 0,
                            (a, b, c) =>
                            {
                                DemoTrace.WriteLine(new { Op = "解除临时禁言", RoomId = a, Result = b, Member = c }.Dump());
                            }, true, accid + " 被解除临时禁言");
                    });
                    menu.MenuItems.Add(item1);
                    menu.MenuItems.Add(item2);
                    menu.Show(membersListview, e.Location);
                }
            }
        }

        private readonly HashSet<long> _joinedRoomIdSet = new HashSet<long>();

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right || string.IsNullOrEmpty(e.Node.Name))
                return;
            var roomId = long.Parse(e.Node.Name);
            ContextMenu menu = new ContextMenu();
            MenuItem item1 = null;
            if (!_joinedRoomIdSet.Contains(roomId))
            {
                item1 = new MenuItem("进入聊天室", (s, arg) =>
                {
                    NIM.Plugin.ChatRoom.RequestLoginInfo(roomId, (response, authResult) =>
                    {
                        if (response == NIM.ResponseCode.kNIMResSuccess)
                        {
                            NIMChatRoom.LoginData data = new NIMChatRoom.LoginData();
                            data.Icon = "http://image.baidu.com/search/detail?ct=503316480&z=&tn=baiduimagedetail&ipn=d&word=%E5%AE%A0%E7%89%A9&step_word=&ie=utf-8&in=&cl=2&lm=-1&st=-1&cs=69099631,2544305882&os=4219122898,2688632801&simid=&pn=0&rn=1&di=0&ln=1000&fr=&fmq=1461830763275_R_D&fm=&ic=0&s=undefined&se=&sme=&tab=0&width=&height=&face=undefined&is=&istype=2&ist=&jit=&bdtype=-1&gsm=0&objurl=http%3A%2F%2Fimg.taopic.com%2Fuploads%2Fallimg%2F110906%2F1382-110Z611025585.jpg&rpstart=0&rpnum=0";
                            data.Nick = "【C# Client】";
                            NIMChatRoom.ChatRoomApi.Login(roomId, authResult, data);
                        }
                    });
                });
            }
            else
            {
                item1 = new MenuItem("离开聊天室", (s, arg) =>
                {
                    NIMChatRoom.ChatRoomApi.Exit(roomId);
                });

                MenuItem item2 = new MenuItem("聊天室信息", (s, arg) =>
                {
                    NIMChatRoom.ChatRoomApi.GetRoomInfo(roomId, (a, b, c) =>
                    {
                        Action action = () =>
                        {
                            ObjectPropertyInfoForm form = new ObjectPropertyInfoForm();
                            form.TargetObject = c;
                            form.Show();
                        };
                        this.Invoke(action);
                    });
                });

                MenuItem item3 = new MenuItem("聊天室成员", (s, arg) =>
                {
                    QueryMembers(roomId);
                });

                MenuItem item4 = new MenuItem("消息历史", (s, arg) =>
                {
                    NIMChatRoom.ChatRoomApi.QueryMessageHistoryOnline(roomId, 0, 50, false, 
                        new List<NIMChatRoomMsgType> { NIMChatRoomMsgType.kNIMChatRoomMsgTypeText }, 
                        (a, b, c) =>
                     {
                         if (b == NIM.ResponseCode.kNIMResSuccess)
                             OutputForm.SetText(c.Dump());
                     });

                });

                MenuItem item5 = new MenuItem("发送测试消息", (s, arg) =>
                {
                    NIMChatRoom.Message msg = new NIMChatRoom.Message();
                    msg.MessageType = NIMChatRoom.NIMChatRoomMsgType.kNIMChatRoomMsgTypeText;
                    msg.MessageAttachment = "这是一条测试消息 " + DateTime.Now.ToString() + " " + new Random().NextDouble().ToString();
                    NIMChatRoom.ChatRoomApi.SendMessage(roomId, msg);
                });

                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);
                menu.MenuItems.Add(item4);
                menu.MenuItems.Add(item5);

                menu.MenuItems.Add("发送文件", (s, arg) => 
                {
                    var files = System.IO.Directory.GetFiles(".");
                    if(files != null && files.Any())
                    {
                        NIM.Nos.NosAPI.Upload(files[0], (ret,url)=> 
                        {
                            if(ret == 200)
                            {
                                NIM.NIMMessageAttachment attach = new NIM.NIMMessageAttachment();
                                attach.RemoteUrl = url;
                                attach.DisplayName = System.IO.Path.GetFileName(files[0]);
                                attach.FileExtension = System.IO.Path.GetExtension(files[0]);
                                NIMChatRoom.Message msg = new NIMChatRoom.Message();
                                msg.MessageType = NIMChatRoomMsgType.kNIMChatRoomMsgTypeFile;
                                msg.RoomId = roomId;
                                msg.MessageAttachment = attach.Serialize();
                                NIMChatRoom.ChatRoomApi.SendMessage(roomId, msg);
                            }
                        }, null);
                    }
                });
            }

            menu.MenuItems.Add(item1);
            menu.Show(treeView1, e.Location);
        }

        void QueryMembers(long roomId)
        {
            membersListview.Items.Clear();
            membersListview.Tag = roomId;
            NIMChatRoom.ChatRoomApi.QueryMembersOnline(roomId, NIMChatRoom.NIMChatRoomGetMemberType.kNIMChatRoomGetMemberTypeSolid, 0, 10, (a, b, c) =>
            {
                OnQueryMembersCompleted(a, b, c, NIMChatRoom.NIMChatRoomGetMemberType.kNIMChatRoomGetMemberTypeSolid);
            });

            NIMChatRoom.ChatRoomApi.QueryMembersOnline(roomId, NIMChatRoom.NIMChatRoomGetMemberType.kNIMChatRoomGetMemberTypeTemp, 0, 10, (a, b, c) =>
            {
                OnQueryMembersCompleted(a, b, c, NIMChatRoom.NIMChatRoomGetMemberType.kNIMChatRoomGetMemberTypeTemp);
            });
        }

        void AddMemberToListview(NIMChatRoom.MemberInfo member, NIMChatRoom.NIMChatRoomGetMemberType type)
        {
            Action action = () =>
            {
                ListViewItem item = new ListViewItem();
                item.Name = member.MemberId;
                var text = string.IsNullOrEmpty(member.Nick) ? member.MemberId : member.Nick;
                item.SubItems.Add(text);
                item.Text = text;
                item.Group = _groups[(int)type];
                membersListview.Items.Add(item);
            };
            this.Invoke(action);
        }

        void OnQueryMembersCompleted(long roomId, NIM.ResponseCode errorCode, MemberInfo[] members, NIMChatRoom.NIMChatRoomGetMemberType type)
        {
            if (errorCode == NIM.ResponseCode.kNIMResSuccess)
            {
                OutputForm.SetText(members.Dump());
                if (members == null)
                    return;
                foreach (var member in members)
                {
                    AddMemberToListview(member, type);
                }
            }
            else
            {
                MessageBox.Show("查询聊天室成员失败:" + errorCode.ToString());
            } 
        }

        void SendTextMsg(long roomId,string text)
        {
            NIMChatRoom.Message msg = new NIMChatRoom.Message();
            msg.MessageType = NIMChatRoom.NIMChatRoomMsgType.kNIMChatRoomMsgTypeText;
            msg.RoomId = roomId;
            msg.MessageAttachment = text;
            NIMChatRoom.ChatRoomApi.SendMessage(roomId, msg);
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            long roomId;
            if (long.TryParse(e.Node.Name, out roomId))
            {
                NIMChatRoom.ChatRoomApi.QueryMembersOnline(roomId, NIMChatRoom.NIMChatRoomGetMemberType.kNIMChatRoomGetMemberTypeSolid, 0, 10, (a, b, c) =>
                {

                });
            }
        }

        public ChatRoomListForm(List<NIMChatRoom.ChatRoomInfo> list)
            : this()
        {
            _chatrommList = list;
        }

        private void ChatRoomListForm_Load(object sender, EventArgs e)
        {
            InitChatRoomlistTreeView();
            NIMChatRoom.ChatRoomApi.LoginHandler += ChatRoomApi_LoginHandler;
            NIMChatRoom.ChatRoomApi.ExitHandler += ChatRoomApi_ExitHandler;
            NIMChatRoom.ChatRoomApi.SendMessageHandler += ChatRoomApi_SendMessageHandler;
            NIMChatRoom.ChatRoomApi.ReceiveNotificationHandler += ChatRoomApi_ReceiveNotificationHandler;
            NIMChatRoom.ChatRoomApi.ReceiveMessageHandler += ChatRoomApi_ReceiveMessageHandler;
        }

        private void ChatRoomListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            NIMChatRoom.ChatRoomApi.LoginHandler -= ChatRoomApi_LoginHandler;
            NIMChatRoom.ChatRoomApi.ExitHandler -= ChatRoomApi_ExitHandler;
            NIMChatRoom.ChatRoomApi.SendMessageHandler -= ChatRoomApi_SendMessageHandler;
            NIMChatRoom.ChatRoomApi.ReceiveNotificationHandler -= ChatRoomApi_ReceiveNotificationHandler;
            NIMChatRoom.ChatRoomApi.ReceiveMessageHandler -= ChatRoomApi_ReceiveMessageHandler;
            NIMChatRoom.ChatRoomApi.Cleanup();
        }
        private void ChatRoomApi_ReceiveMessageHandler(long roomId, NIMChatRoom.Message message)
        {
            if(message.MessageType == NIMChatRoomMsgType.kNIMChatRoomMsgTypeRobot)
            {
                OutputForm.SetText("机器人消息:\n" + message.MessageAttachment + "\n" + message.Body);
            }
            else
            {
                string item = string.Format("{0}:\r\n{1}\r\n", message.SenderNickName, message.MessageAttachment);
                Action action = () =>
                {
                    this.receivedmsgListbox.Items.Add(item);
                };
                this.Invoke(action);
            }
        }
        private void ChatRoomApi_ReceiveNotificationHandler(long roomId, NIMChatRoom.Notification notification)
        {
            MessageBox.Show(notification.Dump(), "聊天室通知:" + roomId);
        }

        private void ChatRoomApi_SendMessageHandler(long roomId, NIM.ResponseCode code, NIMChatRoom.Message message)
        {
            if (code != NIM.ResponseCode.kNIMResSuccess)
            {
                MessageBox.Show("聊天室消息发送失败");
            } 
        }

        private void ChatRoomApi_ExitHandler(long roomId, NIM.ResponseCode errorCode, NIMChatRoom.NIMChatRoomExitReason reason)
        {
            if (errorCode == NIM.ResponseCode.kNIMResSuccess)
            {
                _joinedRoomIdSet.Remove(roomId);
            }
        }

        private void ChatRoomApi_LoginHandler(NIMChatRoom.NIMChatRoomLoginStep loginStep, NIM.ResponseCode errorCode, NIMChatRoom.ChatRoomInfo roomInfo, NIMChatRoom.MemberInfo memberInfo)
        {
            if (loginStep == NIMChatRoom.NIMChatRoomLoginStep.kNIMChatRoomLoginStepRoomAuthOver && errorCode == NIM.ResponseCode.kNIMResSuccess)
            {
                _joinedRoomIdSet.Add(roomInfo.RoomId);
            }
            if (errorCode != NIM.ResponseCode.kNIMResSuccess)
            {
                MessageBox.Show(loginStep.ToString() + " " + errorCode.ToString(), "进入聊天室出错");
            }
        }

        private readonly List<NIMChatRoom.ChatRoomInfo> _chatrommList;

        void InitChatRoomlistTreeView()
        {
            TreeNode root = new TreeNode("聊天室");
            if (_chatrommList != null)
            {
                foreach (var info in _chatrommList)
                {
                    TreeNode node = new TreeNode(info.RoomName + " - " + info.RoomId);
                    node.Name = info.RoomId.ToString();
                    root.Nodes.Add(node);
                }
            }
            treeView1.Nodes.Add(root);
        }
    }
}
