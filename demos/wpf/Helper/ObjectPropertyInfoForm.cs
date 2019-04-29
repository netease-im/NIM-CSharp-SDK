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
    public partial class ObjectPropertyInfoForm : Form
    {
        public ObjectPropertyInfoForm()
        {
            InitializeComponent();

            this.Load += ObjectPropertyInfoForm_Load;
        }

        public object TargetObject { get; set; }
        private object _resultObj;
        public Action<object> UpdateObjectAction { get; set; }

        private static string DefaultEmptyString = "null";

        private void ObjectPropertyInfoForm_Load(object sender, EventArgs e)
        {
            if (UpdateObjectAction == null)
                button1.Visible = false;
            if (TargetObject != null)
                CreateFormContent(TargetObject,flowLayoutPanel1);
        }


        void GetObject()
        {
            var ps = TargetObject.GetType().GetProperties();
            _resultObj = Activator.CreateInstance(TargetObject.GetType());
            foreach (var p in ps)
            {
                if(p.CanWrite)
                    p.SetValue(_resultObj, p.GetValue(TargetObject, null), null); 
                if (p.PropertyType != typeof (string))
                    continue;
                var ctrl = flowLayoutPanel1.Controls.Find(p.Name, true);
                if (ctrl.Any())
                {
                    var value = ctrl[0].Text;
                    if (!string.IsNullOrEmpty(value) && value != DefaultEmptyString)
                        p.SetValue(_resultObj, value, null);
                }
            }
        }

        public static void CreateFormContent(object profile ,FlowLayoutPanel containerPanel)
        {
            var ps = profile.GetType().GetProperties();
            int elementsCount = ps.Count();
            Size labelSz = new Size(80, 30);
            Size boxSz = new Size(200, 30);
            
            for (int i = 0; i < elementsCount; i++)
            {
                Point ctrlLocaltion = new Point(0, 0);
                var p = ps[i];
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
                box.Text = value == null ? DefaultEmptyString : value.ToString();
                Panel panel = new Panel();
                panel.Controls.Add(label);
                panel.Controls.Add(box);
                panel.Width = ctrlLocaltion.X;
                panel.Height = labelSz.Height;
                containerPanel.Controls.Add(panel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetObject();
            UpdateObjectAction.DynamicInvoke(_resultObj);
        }
    }
}
