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
    public enum FormType
    {
        None,
        CreateTeam,
        InviteMemeber
    }

    public partial class CreateTeamForm : Form
    {
        public CreateTeamForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public CreateTeamForm(FormType type)
            : this()
        {
            _formType = type;
            label2.Text = _formType == FormType.CreateTeam ? "群名称：" : "附言：";
        }

        private FormType _formType;
        public Action<string, string[]> MembersIDSelected { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_formType == FormType.None)
                return;
            string txt = richTextBox1.Text;
            string name = textBox1.Text;
            var ret = txt.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries);
            if (ret.Any() && MembersIDSelected != null)
            {
                MembersIDSelected(name, ret);
            }
            this.Close();
        }
    }
}
