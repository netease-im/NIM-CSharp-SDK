using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NIM;

namespace NimDemo
{
    public class Proxy
    {
        [JsonProperty("valid")]
        public bool IsValid { get; set; }

        [JsonProperty("type")]
        public NIMProxyType Type { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("port")]
        public ushort Port { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public partial class ProxySettingForm : Form
    {
        public ProxySettingForm()
        {
            InitializeComponent();
            this.Load += ProxySettingForm_Load;
        }

        private void ProxySettingForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(SavedPath))
            {
                var content = File.ReadAllText(SavedPath);
                var proxy = JsonConvert.DeserializeObject<Proxy>(content);
                comboBox1.SelectedIndex = (int) proxy.Type - 3;
                hostTextBox.Text = proxy.Host;
                portTextBox.Text = proxy.Port.ToString();
                unameTextBox.Text = proxy.UserName;
                pwdTextBox.Text = proxy.Password;
            }
        }

        private static readonly string SavedPath = "proxy.json";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0 && !string.IsNullOrEmpty(hostTextBox.Text) && !string.IsNullOrEmpty(portTextBox.Text))
            {
                NIMProxyType proxyType = (NIMProxyType) (comboBox1.SelectedIndex + 3);
                Proxy proxy = new Proxy();
                proxy.Type = proxyType;
                proxy.Host = hostTextBox.Text;
                proxy.Port = ushort.Parse(portTextBox.Text);
                proxy.UserName = unameTextBox.Text;
                proxy.Password = pwdTextBox.Text;
                proxy.IsValid = true;
                SaveProxySetting(proxy);
                this.Close();
            }
            else
            {
                MessageBox.Show("代理无效，请重新设置");
            }

        }

        static void SaveProxySetting(Proxy pxySetting)
        {
            var str = JsonConvert.SerializeObject(pxySetting);
            File.WriteAllText(SavedPath, str);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetSettingStatus(false);
            this.Close();
        }

        public static Proxy GetProxySetting()
        {
            if (!File.Exists(SavedPath))
                return null;
            var content = File.ReadAllText(SavedPath);
            var proxy = JsonConvert.DeserializeObject<Proxy>(content);
            return proxy;
        }

        public static void SetSettingStatus(bool valid)
        {
           var x = GetProxySetting();
            if (x != null)
            {
                x.IsValid = valid;
                SaveProxySetting(x);
            }
        }
    }
}
