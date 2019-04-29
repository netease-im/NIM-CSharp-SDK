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
    public partial class InfomationForm : Form
    {
        public InfomationForm()
        {
            InitializeComponent();
        }

        public void ShowMessage(string msg)
        {
            Action action = () => { this.richTextBox1.Text = msg; };
            this.Invoke(action);
        }
    }
}
