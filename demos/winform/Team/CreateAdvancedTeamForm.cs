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
    public partial class CreateAdvancedTeamForm : Form
    {
        private readonly TeamList _teamlist;
        public CreateAdvancedTeamForm(TeamList teamList)
        {
            InitializeComponent();
            joinModeCombox.SelectedIndex = 1;
            neddAgreeCombox.SelectedIndex = 0;
            inviteModeCombox.SelectedIndex = 0;
            modifyModeCombox.SelectedIndex = 0;
            modifyPropertyModeCombox.SelectedIndex = 0;
            _teamlist = teamList;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Action<string> createTeam = (url) =>
            {
                NIM.Team.NIMTeamInfo tinfo = new NIM.Team.NIMTeamInfo();
                tinfo.Name = teamNameBox.Text;
                tinfo.Introduce = teamIntroBox.Text;
                tinfo.TeamIcon = url; ;
                tinfo.JoinMode = (NIM.Team.NIMTeamJoinMode)joinModeCombox.SelectedIndex;
                tinfo.BeInvitedMode = (NIM.Team.NIMTeamBeInviteMode)neddAgreeCombox.SelectedIndex;
                tinfo.InvitedMode = (NIM.Team.NIMTeamInviteMode)inviteModeCombox.SelectedIndex;
                tinfo.UpdateMode = (NIM.Team.NIMTeamUpdateInfoMode)modifyModeCombox.SelectedIndex;
                tinfo.UpdateCustomMode = (NIM.Team.NIMTeamUpdateCustomMode)modifyPropertyModeCombox.SelectedIndex;
                tinfo.TeamType = NIM.Team.NIMTeamType.kNIMTeamTypeAdvanced;
                string[] uids = teamMemberBox.Text.Replace("\r\n", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (uids.Any())
                {
                    NIM.Team.TeamAPI.CreateTeam(tinfo, uids, textBox1.Text, (a) =>
                    {
                        DemoTrace.WriteLine(a.Dump());
                        if (a.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResSuccess
                        || a.TeamEvent.ResponseCode == NIM.ResponseCode.kNIMResTeamInviteSuccess)
                        {
                            _teamlist.AddTeamItem(a.TeamEvent.TeamId);
                        }
                    });
                }
            };
            if (!string.IsNullOrEmpty(teamIconPathBox.Text))
            {
                NIM.Nos.NosAPI.Upload(teamIconPathBox.Text, (x, y) =>
                {
                    var url = x == 200 ? y : "";
                    this.Invoke(createTeam, url);
                }, null);
            }
            else
            {
                createTeam("");
            }
        }

        private void teamIconSelectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "图像|*.jpg;*.png;*.jpeg";
            var ret = dialog.ShowDialog();
            teamIconPathBox.Text = dialog.FileName;
        }
    }
}
