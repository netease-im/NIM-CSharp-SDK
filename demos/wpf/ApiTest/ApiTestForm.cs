using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NIM;
using NIM.Messagelog;

namespace NIMDemo
{
    public partial class ApiTestForm : Form
    {
        private ApiTestForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            MethhodBindingFlags = BindingFlags.Public | BindingFlags.Static;
        }

        public ApiTestForm(Type type)
            : this()
        {
            _targetType = type;
            this.Load += ApiTestForm_Load;
        }

        public ApiTestForm(object obj)
            : this(obj.GetType())
        {
            _targetObject = obj;
            MethhodBindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
        }

        private readonly Type _targetType = null;
        private readonly object _targetObject = null;
        private MethodInfo[] _methodCollection = null;
        private MethodInfo _currentMethod = null;
        public BindingFlags MethhodBindingFlags { get; set; }
        private void ApiTestForm_Load(object sender, EventArgs e)
        {
            InitMethodCollection();
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
        }

        private void InitMethodCollection()
        {
            if (_targetType == null)
                return;
            _methodCollection = _targetType.GetMethods(MethhodBindingFlags);
            TreeNode root = new TreeNode();
            root.Text = _targetType.FullName;
            root.Name = _targetType.Name;
            TreeNode staticNode = null, instanceNode = null, cur = null;
            foreach (var m in _methodCollection)
            {
                if (m.IsStatic)
                {
                    if (staticNode == null)
                    {
                        staticNode = new TreeNode("Static");
                        root.Nodes.Add(staticNode);
                    }
                    cur = staticNode;
                }
                else
                {
                    if (instanceNode == null)
                    {
                        instanceNode = new TreeNode("Instance");
                        root.Nodes.Add(instanceNode);
                    }
                    cur = instanceNode;
                }
                TreeNode node = new TreeNode(m.Name);
                node.Name = m.Name;
                cur.Nodes.Add(node);
            }
            treeView1.Nodes.Add(root);
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            _currentMethod = _methodCollection.FirstOrDefault(c => c.Name == e.Node.Name);
            if (_currentMethod == null) return;
            var ps = _currentMethod.GetParameters();
            int left = 20, top = 20;
            panel1.Controls.Clear();
            AddLabel(panel1, "Method:", ref left, ref top);
            string methodDesc = string.Format("({0}) {1}", _currentMethod.ReturnType.Name, _currentMethod.Name);
            AddLabel(panel1, methodDesc, ref left, ref top);
            AddLabel(panel1, "Parameters:", ref left, ref top);
            foreach (var p in ps)
            {
                AddLabel(panel1, string.Format("{0} ({1})", p.Name, p.ParameterType.Name), ref left, ref top);
                var control = ParseParameterInfo(p);
                control.Height = 40;
                control.Width = 200;
                control.Location = new Point(left, top);
                top += 42;
                panel1.Controls.Add(control);
            }
        }

        void AddLabel(Panel parent, string text, ref int left, ref int top)
        {
            const int labelHeight = 20;
            const int labelWidth = 300;
            Label label = new Label();
            label.Text = text;
            label.Width = labelWidth;
            label.Height = labelHeight;
            label.Location = new Point(left, top);
            parent.Controls.Add(label);
            top += (labelHeight + 4);
        }

        protected virtual Control ParseParameterInfo(ParameterInfo p)
        {
            Control ctrl = null;
            if (p.ParameterType.IsEnum)
            {
                var names = Enum.GetNames(p.ParameterType);
                ComboBox comboBox = new ComboBox();
                comboBox.Items.AddRange(names);
                comboBox.Text = names[0];
                ctrl = comboBox;
            }
            else if (p.ParameterType == typeof (bool))
            {
                ComboBox comboBox = new ComboBox();
                comboBox.Items.AddRange(new object[] {false, true});
                comboBox.Text = comboBox.Items[0].ToString();
                ctrl = comboBox;
            }
            else
            {
                ctrl = new TextBox();
            }
            ctrl.Name = p.Name;
            return ctrl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentMethod == null)
                return;
            richTextBox1.Clear();
            var ps = GenerateParams();
            try
            {
                var r = _currentMethod.Invoke(_targetObject, ps);
                ShowOperationResult(r);
            }
            catch (Exception ex)
            {
                ShowOperationResult(ex.ToString());
            }
            
        }

        protected virtual object GenerateParamerte(Type paramType,string value)
        {
            object targetObj = null;
            if (paramType == typeof (string))
               targetObj = value;
            else if (paramType == typeof (bool))
                targetObj = bool.Parse(value);
            else if (paramType == typeof (int) || paramType == typeof (long))
            {
                targetObj = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
            }
            else if (paramType.IsEnum)
            {
                targetObj = Enum.Parse(paramType, value);
            }
            else if (paramType.IsArray)
            {
                var elementType = paramType.GetElementType();
                if (string.IsNullOrEmpty(value) || elementType != typeof(string))
                    targetObj = null;
                else
                {
                    var raw = SplitString(value);
                    targetObj = paramType == typeof (string[]) ? raw : null;
                }
            }
            return targetObj;
        }

        string[] SplitString(string s)
        {
            string pattern = @"(\b\w+\b)\s*";
            var reg = new System.Text.RegularExpressions.Regex(pattern);
            var mc = reg.Matches(s);
            if (mc.Count == 0)
                return null;
            string[] ret = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                ret[i] = mc[i].Groups[1].Value;
            }
            return ret;
        } 

        protected virtual object[] GenerateParams()
        {
            List<object> list = new List<object>();
            var ps = _currentMethod.GetParameters();
            for (int i = 0; i < ps.Count(); i++)
            {
                var value = panel1.Controls.Find(ps[i].Name, false)[0].Text;
                var obj = GenerateParamerte(ps[i].ParameterType, value);
                list.Add(obj);
            }
            return list.ToArray();
        }

        protected void ShowOperationResult(object obj)
        {
            var output = obj.Dump();
            Action action = () =>
            {
                this.richTextBox1.Text = output;
            };
            this.Invoke(action);
        }
    }
}
