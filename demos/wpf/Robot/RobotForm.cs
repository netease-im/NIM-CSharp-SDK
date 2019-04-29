using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;

namespace NIMDemo
{
    public partial class RobotForm : Form
    {
        public RobotForm()
        {
            InitializeComponent();
            robotlistView.MouseUp += RobotlistView_MouseUp;
        }

        private void RobotlistView_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && robotlistView.SelectedItems.Count > 0)
            {
                var id = robotlistView.SelectedItems[0].Name;
               
                ContextMenu menu = new ContextMenu();

                menu.MenuItems.Add(new MenuItem("打招呼", (s, arg) => 
                {
                    NIM.Robot.WelcomeMessage msg = new NIM.Robot.WelcomeMessage();
                    msg.Text = "hello";
                    NIM.Robot.NIMRobotAPI.SendMessage(id, msg);
                }));
                menu.MenuItems.Add(new MenuItem("发消息", (s, arg) =>
                {
                    ChatForm chat = new ChatForm(id, NIM.Session.NIMSessionType.kNIMSessionTypeP2P, true);
                    chat.Show();
                }));

                menu.Show(robotlistView, new Point(e.X, e.Y));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var robots = NIM.Robot.NIMRobotAPI.QueryAllRobots();
            if (robots == null)
            {
                MessageBox.Show("get robots failed");
                return;
            }
            robotlistView.Items.Clear();
            if (robots == null)
                return;
            foreach (var r in robots)
            {
                ListViewItem item = new ListViewItem(r.RID);
                item.Name = r.Accid;
                item.SubItems.Add(r.Accid);
                item.SubItems.Add(r.Name);
                robotlistView.Items.Add(item);
            }
        }

        private void robotlistView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sendjumpbutton_Click(object sender, EventArgs e)
        {
            if(robotlistView.SelectedItems.Count > 0)
            {
                var id = robotlistView.SelectedItems[0].Name;
                NIM.Robot.RedirectionMessage msg = new NIM.Robot.RedirectionMessage();
                msg.Params = paramstextBox.Text;
                msg.Target = targettextBox.Text;
                msg.Text = textBox1.Text;
                NIM.Robot.NIMRobotAPI.SendMessage(id, msg);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NIM.Robot.NIMRobotAPI.GetRobots(0, OnQueryCompleted);
        }

        private void OnQueryCompleted(ResponseCode result, List<RobotInfo> robots)
        {
            Action action = () => 
            {
                robotlistView.Items.Clear();
                if (robots == null)
                    return;
                foreach (var r in robots)
                {
                    ListViewItem item = new ListViewItem(r.RID);
                    item.Name = r.Accid;
                    item.SubItems.Add(r.Accid);
                    item.SubItems.Add(r.Name);
                    robotlistView.Items.Add(item);
                }
            };
            this.Invoke(action);
        }
    }
}
