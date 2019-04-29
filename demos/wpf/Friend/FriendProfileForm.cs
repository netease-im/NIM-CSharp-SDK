using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
    public partial class FriendProfileForm : Form
    {
        public FriendProfileForm()
        {
            InitializeComponent();
        }

        private string _userId;
        private NIM.Friend.NIMFriendProfile _profile;
        private NIM.User.UserNameCard _userCard;
        private Panel _contentPanel;
        private Panel _cardPanel;
        private int _formHeight = 0;
        public FriendProfileForm(string id)
            :this()
        {
            _userId = id;
            this.Load += UserCard_Load;
        }

        private void UserCard_Load(object sender, EventArgs e)
        {
            NIM.Friend.FriendAPI.GetFriendProfile(_userId, (id, ret) =>
            {
                Action<NIM.Friend.NIMFriendProfile> action = (profile) =>
                {
                    _profile = profile;
                    if(profile != null)
                    {
                        var p1 = CreateFormContent(profile);
                        _contentPanel = p1;
                        Button btn1 = new Button();
                        btn1.Dock = DockStyle.Bottom;
                        btn1.Text = "更新";
                        btn1.Click += (s, args) =>
                        {
                            UpdateFriendProfile();
                        };
                        p1.Height += 50;
                        p1.Controls.Add(btn1);
                        this.splitContainer1.Panel1.Controls.Add(p1);
                        this.splitContainer1.Panel1.SetBounds(0, 0, p1.Width, p1.Height);
                    }
                };
                this.Invoke(action,ret);
            });

            NIM.User.UserAPI.QueryUserNameCardOnline(new List<string>() {_userId}, (ret) =>
            {
                if (ret == null || !ret.Any())
                {
                    return;
                }
                _userCard = ret[0];
                Action action = () =>
                {
                    var p = CreateFormContent(_userCard);
                    _cardPanel = p;
                    //Button btn1 = new Button();
                    //btn1.Dock = DockStyle.Bottom;
                    //btn1.Text = "更新";
                    //btn1.Click += (s, args) =>
                    //{
                    //    UpdateCard();
                    //};
                    //p.Controls.Add(btn1);
                    this.splitContainer1.Panel2.Controls.Add(p);
                    this.splitContainer1.Panel2.SetBounds(0, 0, p.Width, p.Height);
                };
                this.Invoke(action);
            });
        }

        Panel CreateFormContent(object profile)
        {
            var contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            int width = this.Width;
            int height = this.Height;
            Point startPoint = new Point(20,20);
            var ps = profile.GetType().GetProperties();
            Size labelSz = new Size(120, 30);
            Size boxSz = new Size(200, 30);
            int countPerRow = 2;
            int rowHeight = 30;
            Point ctrlLocaltion = startPoint;
            int elementsCount = ps.Count();
            contentPanel.Height = (elementsCount/countPerRow + elementsCount % countPerRow)*rowHeight + 60;
            contentPanel.Width = (boxSz.Width + labelSz.Width + 20)*countPerRow + 30;

            for (int i = 0; i<elementsCount; i++)
            {
                var p = ps[i];
                if (i > 0 && i%countPerRow == 0)
                {
                    ctrlLocaltion.X = startPoint.X;
                    ctrlLocaltion.Y += rowHeight;
                }
                Label label = new Label();
                label.Text = p.Name;
                label.Location = ctrlLocaltion;
                label.Size = labelSz;
                ctrlLocaltion.X += labelSz.Width + 5;
                TextBox box = new TextBox();
                box.Size = boxSz;
                box.Location = ctrlLocaltion;
                box.Name = p.Name;
                ctrlLocaltion.X += boxSz.Width + 10;
                var value = p.GetValue(profile, null);
                box.Text = value == null ? "null" : value.ToString();
                contentPanel.Controls.Add(label);
                contentPanel.Controls.Add(box);
            }
            if (this.Width < contentPanel.Width)
            {
                this.Width = contentPanel.Width + 40;
            }
            _formHeight += contentPanel.Height;
            this.Height = _formHeight + 80;
            return contentPanel;
        }

        void UpdateFriendProfile()
        {
            if (_profile != null && _contentPanel != null)
            {
                var c = _contentPanel.Controls.Find("Alias", false);
                _profile.Alias = c[0].Text;
                NIM.Friend.FriendAPI.UpdateFriendInfo(_profile, (a, b, cc) =>
                {
                    if (a == 200)
                    {
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                });
            }
        }

        void UpdateCard()
        {
            if (_userCard != null && _cardPanel != null)
            {
                var c = _cardPanel.Controls.Find("NickName", false);
                _userCard.NickName = c[0].Text;
                NIM.User.UserAPI.UpdateMyCard(_userCard, (a) =>
                {
                    if (a == NIM.ResponseCode.kNIMResSuccess)
                    {
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                });
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (_profile != null && _contentPanel != null)
            {
                var c = _contentPanel.Controls.Find("Alias", false);
                _profile.Alias = c[0].Text;
                NIM.Friend.FriendAPI.UpdateFriendInfo(_profile, (a, b, cc) =>
                {
                    if (a == 200)
                    {
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                });
            }
        }
    }
}
