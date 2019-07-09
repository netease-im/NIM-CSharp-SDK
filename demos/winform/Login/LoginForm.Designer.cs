namespace NIMDemo
{
    partial class LoginForm
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.PwdTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.UserNameComboBox = new System.Windows.Forms.ComboBox();
			this.ProxyCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// PwdTextBox
			// 
			this.PwdTextBox.Location = new System.Drawing.Point(100, 111);
			this.PwdTextBox.Name = "PwdTextBox";
			this.PwdTextBox.PasswordChar = '*';
			this.PwdTextBox.Size = new System.Drawing.Size(137, 21);
			this.PwdTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "用户名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(42, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "密码";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 301);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(284, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			this.toolStripProgressBar1.Step = 30;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(88, 190);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(114, 32);
			this.button1.TabIndex = 5;
			this.button1.Text = "登陆";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnLoginButtonClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 249);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 12);
			this.label3.TabIndex = 6;
			// 
			// UserNameComboBox
			// 
			this.UserNameComboBox.FormattingEnabled = true;
			this.UserNameComboBox.Location = new System.Drawing.Point(100, 68);
			this.UserNameComboBox.Name = "UserNameComboBox";
			this.UserNameComboBox.Size = new System.Drawing.Size(137, 20);
			this.UserNameComboBox.TabIndex = 7;
			this.UserNameComboBox.SelectedIndexChanged += new System.EventHandler(this.UserNameComboBox_SelectedIndexChanged);
			// 
			// ProxyCheckBox
			// 
			this.ProxyCheckBox.AutoSize = true;
			this.ProxyCheckBox.Location = new System.Drawing.Point(206, 265);
			this.ProxyCheckBox.Name = "ProxyCheckBox";
			this.ProxyCheckBox.Size = new System.Drawing.Size(72, 16);
			this.ProxyCheckBox.TabIndex = 9;
			this.ProxyCheckBox.Text = "使用代理";
			this.ProxyCheckBox.UseVisualStyleBackColor = true;
			// 
			// LoginForm
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 323);
			this.Controls.Add(this.ProxyCheckBox);
			this.Controls.Add(this.UserNameComboBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PwdTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "登录";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PwdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox UserNameComboBox;
        private System.Windows.Forms.CheckBox ProxyCheckBox;
    }
}