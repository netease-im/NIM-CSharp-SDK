using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM.Session;

namespace NIMDemo
{
    class RecentSessionList
    {
        private readonly ListBox _sessionListBox = null;
        private readonly List<SessionInfo> _sessionsCollection = new List<SessionInfo>();
        private readonly InvokeActionWrapper _actionWrapper;
        private int _pinnedSessionsCount = 0;

        public RecentSessionList(ListBox box)
        {
            _sessionListBox = box;
            _sessionListBox.ItemHeight = 40;
            _sessionListBox.DrawMode = DrawMode.OwnerDrawVariable;
            _sessionListBox.DrawItem += _sessionListBox_DrawItem;
            SessionAPI.RecentSessionChangedHandler += OnSessionChanged;
            _sessionListBox.MouseUp += _sessionListBox_MouseUp;
            _actionWrapper = new InvokeActionWrapper(box);
        }

        private void _sessionListBox_MouseUp(object sender, MouseEventArgs e)
        {
            var box = sender as ListBox;
            var item = box.SelectedItem as SessionInfo;
            if (e.Button == MouseButtons.Right)
            {
                if (item != null)
                    ShowSessionItemMenu(item, e.Location);
                else
                {
                    ShowDeleteAllSessionMenu(e.Location);
                }
            }
        }

        void ShowSessionItemMenu(SessionInfo info, Point location)
        {
            ContextMenu menu = new ContextMenu();

            MenuItem item1 = new MenuItem("删除", (s, e) =>
            {
                NIM.Session.SessionAPI.DeleteRecentSession(info.SessionType, info.Id, (a, b, c) =>
                {
                    DeleteSession(info);
                });
            });
            MenuItem item2 = new MenuItem("标记为已读", (s, e) =>
            {
                NIM.Session.SessionAPI.SetUnreadCountZero(info.SessionType, info.Id, (a, b, c) =>
                {
                    NIM.Session.SessionAPI.SetUnreadCountZero(info.SessionType, info.Id, null);
                    Update(info, b);
                });
            });

            MenuItem item3 = new MenuItem("查看", (s, e) =>
            {
                NIM.Messagelog.MessagelogAPI.QuerylogById(info.MsgId, (a, b, c) =>
                {
                    if (a != NIM.ResponseCode.kNIMResSuccess || c == null)
                        return;
                    Action action = () =>
                    {
                        MsgInfoForm form = new MsgInfoForm(c);
                        form.Show();
                    };
                    _sessionListBox.Invoke(action);
                });
            });

            string itemText = info.IsPinnedOnTop ? "取消置顶" : "置顶";
            MenuItem item4 = new MenuItem(itemText, (s, e) =>
            {
                NIM.Session.SessionAPI.PinSessionOnTop(info.SessionType, info.Id, !info.IsPinnedOnTop, (a, b, c) =>
                {
                    DemoTrace.WriteLine("会话置顶:", a, b.Id, b.IsPinnedOnTop);
                    if (a == 200)
                        Update(info, b);
                });
            });

            menu.MenuItems.Add(item1);
            if (info.Status == NIM.Messagelog.NIMMsgLogStatus.kNIMMsgLogStatusUnread)
                menu.MenuItems.Add(item2);
            menu.MenuItems.Add(item3);
            menu.MenuItems.Add(item4);
            menu.Show(_sessionListBox, location);
        }

        void ShowDeleteAllSessionMenu(Point location)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem item = new MenuItem("清空会话", (s, e) =>
            {
                NIM.Session.SessionAPI.DeleteAllRecentSession((a, b, c) =>
                {
                    DeleteAll();
                });
            });
            menu.MenuItems.Add(item);
            menu.Show(_sessionListBox, location);
        }

        private void OnSessionChanged(object sender, SessionChangedEventArgs e)
        {
            if (e.Info.Command == NIMSessionCommand.kNIMSessionCommandAdd ||
                e.Info.Command == NIMSessionCommand.kNIMSessionCommandUpdate)
            {
                var x = _sessionsCollection.Find(c => c.Id == e.Info.Id);
                if (x == null)
                {
                    _sessionsCollection.Insert(0, e.Info);
                    InsertSession(e.Info);
                }
                else
                {
                    var i = _sessionsCollection.IndexOf(x);
                    _sessionsCollection[i] = e.Info;
                    Update(x, e.Info);
                }

            }
            if (e.Info.Command == NIMSessionCommand.kNIMSessionCommandRemove)
            {

            }
        }

        private void _sessionListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var target = sender as ListBox;
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            if (e.Index == -1 || e.Index >= target.Items.Count)
            {
                return;
            }
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;
            var item = target.Items[e.Index] as NIM.Session.SessionInfo;
            
            // Determine the color of the brush to draw each item based  
            // on the index of the item to draw. 
            switch (item.Status)
            {
                case NIM.Messagelog.NIMMsgLogStatus.kNIMMsgLogStatusUnread:
                    myBrush = Brushes.Red;
                    break;
            }
            if(item.IsPinnedOnTop)
            {
                myBrush = Brushes.LightGreen;
            }
            string sessionId = "";
            string content = "";
            if (item.SessionType == NIM.Session.NIMSessionType.kNIMSessionTypeTeam)
            {
                sessionId = "[群]:";
                content = item.Sender + ":";
            }
            else if (item.SessionType == NIM.Session.NIMSessionType.kNIMSessionTypeP2P)
            {
                sessionId = "[好友]:";
            }

            sessionId += item.Id;
            if (item.MsgType != NIM.NIMMessageType.kNIMMessageTypeText)
            {
                content += string.Format("[{0}]", item.MsgType.ToString());
            }
            content += item.Content;
            Rectangle top = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Size.Width, e.Bounds.Height / 2));
            Rectangle b = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y + e.Bounds.Height / 2, e.Bounds.Size.Width, e.Bounds.Size.Height / 2);
            // Draw the current item text based on the current Font  
            // and the custom brush settings.
            e.Graphics.DrawString(sessionId, e.Font, myBrush, top, StringFormat.GenericDefault);
            e.Graphics.DrawString(content, e.Font, Brushes.Blue, b, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        public void LoadSessionList()
        {
            NIM.Session.SessionAPI.QueryAllRecentSession((a, b) =>
            {
                if (b == null || b.SessionList == null) return;
                foreach (var item in b.SessionList)
                {
                    _sessionsCollection.Add(item);
                    InsertSession(item);
                }
            });
        }

        void InsertSession(SessionInfo info)
        {
            Action action = () =>
            {
                _pinnedSessionsCount += info.IsPinnedOnTop ? 1 : 0;
                _sessionListBox.Items.Insert(info.IsPinnedOnTop ? 0 : _pinnedSessionsCount, info);
                _sessionListBox.Invalidate();
            };
            _actionWrapper.InvokeAction(action);
        }

        void Update(SessionInfo oldInfo, SessionInfo newInfo)
        {
            Action action = () =>
            {
                if(oldInfo.IsPinnedOnTop != newInfo.IsPinnedOnTop)
                {
                    DeleteSession(oldInfo);
                    InsertSession(newInfo);
                }
                else
                {
                    int index = 0;
                    foreach (var info in _sessionListBox.Items)
                    {
                        var si = info as SessionInfo;
                        if (si != null && si.Id == oldInfo.Id)
                        {
                            _sessionListBox.Items[index] = newInfo;
                        }
                        index++;
                    }
                }
                _sessionListBox.Invalidate();
            };
            _actionWrapper.InvokeAction(action);
        }

        void DeleteSession(SessionInfo info)
        {
            Action action = () =>
            {
                _sessionsCollection.Remove(info);
                if (info.IsPinnedOnTop)
                    _pinnedSessionsCount--;
                _sessionListBox.Items.Remove(info);
                _sessionListBox.Invalidate();
            };
            _actionWrapper.InvokeAction(action);
        }

        void DeleteAll()
        {
            _sessionsCollection.Clear();
            _pinnedSessionsCount = 0;
            Action action = () =>
            {
                _sessionListBox.Items.Clear();
                _sessionListBox.Invalidate();
            };
            _actionWrapper.InvokeAction(action);
        }

        void UpdateSessionList()
        {
            _sessionsCollection.Sort((x, y) => x.Timetag == y.Timetag ? 0 : (x.Timetag > y.Timetag ? -1 : 1));
        }
    }
}
