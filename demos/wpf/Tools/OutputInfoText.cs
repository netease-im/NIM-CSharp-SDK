using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo.Tools
{
    class OutputInfoText
    {
        private RichTextBox _richTextbox;
        private readonly InvokeActionWrapper _actionWrapper;
        public OutputInfoText(RichTextBox box)
        {
            _richTextbox = box;
            _actionWrapper = new InvokeActionWrapper(box);
        }

        public void ShowInfo(string info)
        {
            Action action = () =>
            {
                _richTextbox.AppendText(info + Environment.NewLine);
                _richTextbox.Select(_richTextbox.Text.Length, 0);
                _richTextbox.Focus();
            };
            _actionWrapper.InvokeAction(action);
        }

        public void ShowInfo(string format,params object[] args)
        {
            var info = string.Format(format, args);
            ShowInfo(info);
        }
    }
}
