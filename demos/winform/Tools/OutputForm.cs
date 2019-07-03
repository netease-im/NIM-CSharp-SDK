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
    public partial class OutputForm : Form
    {
        private OutputForm()
        {
            InitializeComponent();
            Instance = this;
            this.FormClosed += OutputForm_FormClosed;
        }

        private void OutputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }

        private static OutputForm _instance;

        public static OutputForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputForm();
                return _instance;
            }
            private set { _instance = value; }
        }

        public void SetOutput(string text)
        {
            Action action = () =>
            {
                this.richTextBox1.AppendText(text);
                this.richTextBox1.AppendText(System.Environment.NewLine);
                //滚到最后
                this.richTextBox1.Select(richTextBox1.TextLength, 0);
                this.richTextBox1.Focus();

            };

            this.richTextBox1.Invoke(action);
        }

        public static void SetText(string text)
        {
            if (_instance != null)
            {
                _instance.SetOutput(text);
            }
        }
    }
}
