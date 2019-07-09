using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;
using NIM.Session;

namespace NIMDemo
{
    class SessionList
    {
        private readonly ListView _targetListView = null;
        public SessionList(ListView listView)
        {
            _targetListView = listView;
            _targetListView.MouseUp += _targetListView_MouseUp;
            SessionAPI.RecentSessionChangedHandler += OnSessionChanged;
        }

        void OnSessionChanged(object sender, SessionChangedEventArgs args)
        {
            DemoTrace.WriteLine(args.Dump());
        }

        private void _targetListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var selectedItems = _targetListView.SelectedItems;
                if (selectedItems.Count == 0)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem item1 = new MenuItem("清空未读数", (s, arg) =>
                    {
                        NIM.Session.SessionAPI.ResetAllUnreadCount((int rescode, SessionInfo result, int totalUnreadCounts) =>
                        {
                            DemoTrace.WriteLine(string.Format("清空会话未读数:{0}", rescode));
                        });
                    });
                    contextMenu.MenuItems.Add(item1);
                    contextMenu.Show(_targetListView, e.Location);
                    return;
                }
                var msg = selectedItems[0].Tag as NIM.NIMIMMessage;
                if (selectedItems.Count > 0)
                {
                    ShowContextMenu(e.Location,selectedItems[0]);
                }
            }
        }

        void ShowContextMenu(Point location, ListViewItem listItem)
        {
            ContextMenu contextMenu = new ContextMenu();
            var msg = listItem.Tag as NIM.NIMIMMessage;
            MenuItem item1 = new MenuItem("查看详情", (s, e) =>
            {
                if (msg != null)
                {
                    MsgInfoForm form = new MsgInfoForm(msg);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.Show();

                }
            });
            MenuItem item2 = new MenuItem("清空未读数", (s, e) =>
            {
                if (msg != null)
                {
                    NIM.Session.SessionAPI.SetUnreadCountZero(NIMSessionType.kNIMSessionTypeP2P, msg.SenderID,
                        (a, b, c) =>
                        {
                            DemoTrace.WriteLine("清空未读数:" + c.ToString());
                        });

                }
            });

            MenuItem item3 = new MenuItem("查询最近会话", (s, e) =>
            {
                if (msg != null)
                {
                    NIM.Session.SessionAPI.QueryAllRecentSession((x, y) =>
                    {
                        DemoTrace.WriteLine("total count:" + x.ToString() + " " + y.Dump());
                    });
                }
            });

            MenuItem item4 = new MenuItem("群消息回执", (s, e) =>
            {
                List<NIMIMMessage> msgs = new List<NIMIMMessage>();
                msg.NeedTeamAck = 1;
                msgs.Add(msg);
                NIM.Team.TeamAPI.MsgAckRead(msg.ReceiverID, msgs, (data) =>
                {

                });
            });

            MenuItem item5 = new MenuItem("群消息未读成员", (s, e) => 
            {
                msg.NeedTeamAck = 1;
                NIM.Team.TeamAPI.QueryMsgUnreadList(msg.ReceiverID, msg, (data) => 
                {

                });
            });

            contextMenu.MenuItems.Add(item1);
            contextMenu.MenuItems.Add(item2);
            contextMenu.MenuItems.Add(item3);
            contextMenu.MenuItems.Add(item4);
            contextMenu.MenuItems.Add(item5);
            contextMenu.Show(_targetListView, location);
        }

        public void GetRecentSessionList()
        {
            SessionAPI.QueryAllRecentSession(OnQuerySessionListCompleted);
        }

        void OnQuerySessionListCompleted(int count, SesssionInfoList session)
        {
            DemoTrace.WriteLine(session.Dump());
            ImageList il = new ImageList();
            ListViewItem item = new ListViewItem();
            
        }
    }
}
