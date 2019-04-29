using System;
using System.Windows.Forms;
using NimDemo;
using NimUtility;

namespace NIMDemo
{
	public partial class LoginForm : Form
    {
        private AccountCollection _accounts;
        private string _userName;
        private string _password;
        private static LoginForm _instance;

		public static LoginForm Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LoginForm();
				return _instance;
			}
			private set
			{
				_instance = value;
			}
		}

		public LoginForm()
        {
            InitializeComponent();
            this.Load += OnLoginFormLoaded;
            this.VisibleChanged += OnLoginFormVisibleChanged;
            Instance = this;
        }

        private void OnLoginFormVisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                InitLoginAccount();
        }

        private void OnLoginFormLoaded(object s, EventArgs e)
        {
            OutputForm.Instance.Show();
            var ps = ProxySettingForm.GetProxySetting();
            ProxyCheckBox.Checked = (ps != null && ps.IsValid);
            ProxyCheckBox.CheckedChanged += ProxyCheckBox_CheckedChanged;
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var ps = ProxySettingForm.GetProxySetting();
            if (ps != null && ps.IsValid)
            {
                NIM.GlobalAPI.SetProxy(ps.Type, ps.Host, ps.Port, ps.UserName, ps.Password);
            }
            _userName = UserNameComboBox.Text;
            _password = PwdTextBox.Text;
            //使用明文密码或者其他加密方式请修改此处代码
            var password = NIM.ToolsAPI.GetMd5(_password);
            if (!string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(password))
            {
                toolStripProgressBar1.Value = 0;
                label3.Text = "";
				handle_result = HandleLoginResult;
                NIM.ClientAPI.Login(ConfigReader.GetAppKey(), _userName, password, handle_result);
            }
        }
        private NIM.ClientAPI.LoginResultDelegate handle_result ;

        void HandleLogoutResult(NIM.NIMLogoutResult result)
        {
			DemoTrace.WriteLine(String.Format("logout: {0}", result.Code));
		}

		/// <summary>
		/// 发布登录通知
		/// </summary>
		void PublishLoginEvent()
        {
            NIM.NIMEventInfo info = new NIM.NIMEventInfo();
            info.Value = 200000;
            info.Type = 1;
            info.Sync = 1;
            info.ClientMsgID = Guid.NewGuid().ToString();
            info.BroadcastType = 1;
            info.ValidityPeriod = 24 * 60 * 60;
            NIM.NIMSubscribeApi.Publish(info, (ret, eventInfo) => 
            {
                System.Diagnostics.Debug.Assert(ret == NIM.ResponseCode.kNIMResSuccess);
            });
        }

        /// <summary>
        /// 登录结果处理
        /// </summary>
        /// <param name="result"></param>
        private void HandleLoginResult(NIM.NIMLoginResult result)
        {
            DemoTrace.WriteLine(result.LoginStep.ToString());
            Action action = () =>
            {
                toolStripProgressBar1.PerformStep();

                this.label3.Text = string.Format("{0}  {1}", result.LoginStep, result.Code);
                if (result.LoginStep == NIM.NIMLoginStep.kNIMLoginStepLogin)
                {
                    toolStripProgressBar1.Value = 100;
                    if (result.Code == NIM.ResponseCode.kNIMResSuccess)
                    {
                        this.Hide();
                        new MainForm(_userName).Show();
                        toolStripProgressBar1.Value = 0;
                        this.label3.Text = "";
                        System.Threading.ThreadPool.QueueUserWorkItem((s) =>
                        {
                            SaveLoginAccount();
                        });
                        //PublishLoginEvent();
                        
                    }
                    else
                    {
                        NIM.ClientAPI.Logout(NIM.NIMLogoutType.kNIMLogoutChangeAccout, HandleLogoutResult);
                    }
                }
            };
            this.Invoke(action);
        }

        private void SaveLoginAccount()
        {
            if (_accounts == null)
                _accounts = new AccountCollection();
            var index = _accounts.IndexOf(_userName);
            if (index != -1)
            {
                _accounts.List[index].Password = _password;
                _accounts.LastIndex = index;
            }
            else
            {
                Account account = new Account();
                account.Name = _userName;
                account.Password = _password;
                _accounts.List.Insert(0, account);
                _accounts.LastIndex = 0;
            }
            AccountManager.SaveLoginAccounts(_accounts);
        }

        private void InitLoginAccount()
        {
            _accounts = AccountManager.GetAccountList();
            if (_accounts != null)
            {
                foreach (var item in _accounts.List)
                {
                    if (!UserNameComboBox.Items.Contains(item.Name))
                        UserNameComboBox.Items.Add(item.Name);
                }
                UserNameComboBox.Text = _accounts.List[_accounts.LastIndex].Name;
                PwdTextBox.Text = _accounts.List[_accounts.LastIndex].Password;
            }
        }

        private void UserNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_accounts == null)
                return;
            var text = UserNameComboBox.Text;
            var account = _accounts.Find(text);
            if (account != null)
                PwdTextBox.Text = account.Password;
        }

        private void ProxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ProxyCheckBox.Checked)
            {
                new ProxySettingForm().ShowDialog();
                var ps = ProxySettingForm.GetProxySetting();
                if (ps == null || !ps.IsValid)
                {
                    Action action = () => { this.ProxyCheckBox.Checked = false; };
                    ProxyCheckBox.Invoke(action);
                }
            }
            else
            {
                ProxySettingForm.SetSettingStatus(false);
            }
        }
	}
}
