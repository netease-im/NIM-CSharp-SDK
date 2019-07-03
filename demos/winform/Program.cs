using System;
using System.Windows.Forms;
using NIMDemo.Helper;
using NimUtility;

namespace NIMDemo
{
	static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;        
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
			InitIM();
			InitVChat();
			Application.Run(new LoginForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string cur_time = DateTime.Now.ToString("yyyyMMddhhmmss");
            string dump_name = "demo_dump_" + cur_time + ".dmp";
            DumpHelper.TryDump(dump_name);
			MessageBox.Show("CurrentDomain_UnhandledException!");
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
			NIM.VChatAPI.Cleanup();
			NIM.ClientAPI.Cleanup();
		}

		private static bool InitIM()
		{
			//读取配置信息,用户可以自己定义配置文件格式和读取方式，使用默认配置项config设置为null即可
			var config = ConfigReader.GetSdkConfig();
			if (config == null)
				config = new NimConfig();
			if (config.CommonSetting == null)
				config.CommonSetting = new SdkCommonSetting();
			//群消息已读功能,如需开启请提前咨询技术支持或销售
			config.CommonSetting.TeamMsgAckEnabled = true;
			if (String.IsNullOrEmpty(config.AppKey))
			{
				MessageBox.Show("请设置app key");
				return false;
			}
			if (!NIM.ClientAPI.Init(config.AppKey, "NIMCSharpDemo", null, config))
			{
				MessageBox.Show("NIM init failed!");
				return false;
			}
			return true;
		}

		private static void InitVChat()
		{
			if (!NIM.VChatAPI.Init(""))
			{
				MessageBox.Show("NIM VChatAPI init failed!");
			}
		}
	}
}
