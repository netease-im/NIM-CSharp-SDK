namespace NIMDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.BtnMsgOpt = new System.Windows.Forms.Button();
			this.Btn_Signaling = new System.Windows.Forms.Button();
			this.btn_net_detect = new System.Windows.Forms.Button();
			this.multipushCheckbox = new System.Windows.Forms.CheckBox();
			this.btn_livingstream = new System.Windows.Forms.Button();
			this.sysMsgBtn = new System.Windows.Forms.Button();
			this.exitBtn = new System.Windows.Forms.Button();
			this.chatRoomBtn = new System.Windows.Forms.Button();
			this.MyProfileBtn = new System.Windows.Forms.Button();
			this.LogoutBtn = new System.Windows.Forms.Button();
			this.SigLabel = new System.Windows.Forms.Label();
			this.NameLabel = new System.Windows.Forms.Label();
			this.IDLabel = new System.Windows.Forms.Label();
			this.IconPictureBox = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.AudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LoginStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.DocTransToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EventSubsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.TeamListView = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.chatListView = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.recentSessionListbox = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
			this.splitContainer1.Panel1.Controls.Add(this.BtnMsgOpt);
			this.splitContainer1.Panel1.Controls.Add(this.Btn_Signaling);
			this.splitContainer1.Panel1.Controls.Add(this.btn_net_detect);
			this.splitContainer1.Panel1.Controls.Add(this.multipushCheckbox);
			this.splitContainer1.Panel1.Controls.Add(this.btn_livingstream);
			this.splitContainer1.Panel1.Controls.Add(this.sysMsgBtn);
			this.splitContainer1.Panel1.Controls.Add(this.exitBtn);
			this.splitContainer1.Panel1.Controls.Add(this.chatRoomBtn);
			this.splitContainer1.Panel1.Controls.Add(this.MyProfileBtn);
			this.splitContainer1.Panel1.Controls.Add(this.LogoutBtn);
			this.splitContainer1.Panel1.Controls.Add(this.SigLabel);
			this.splitContainer1.Panel1.Controls.Add(this.NameLabel);
			this.splitContainer1.Panel1.Controls.Add(this.IDLabel);
			this.splitContainer1.Panel1.Controls.Add(this.IconPictureBox);
			this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(408, 728);
			this.splitContainer1.SplitterDistance = 203;
			this.splitContainer1.TabIndex = 0;
			// 
			// BtnMsgOpt
			// 
			this.BtnMsgOpt.Location = new System.Drawing.Point(188, 40);
			this.BtnMsgOpt.Name = "BtnMsgOpt";
			this.BtnMsgOpt.Size = new System.Drawing.Size(118, 23);
			this.BtnMsgOpt.TabIndex = 16;
			this.BtnMsgOpt.Text = "消息历史导入导出";
			this.BtnMsgOpt.UseVisualStyleBackColor = true;
			this.BtnMsgOpt.Click += new System.EventHandler(this.Btn_Msg_Export_Click);
			// 
			// Btn_Signaling
			// 
			this.Btn_Signaling.Location = new System.Drawing.Point(110, 106);
			this.Btn_Signaling.Name = "Btn_Signaling";
			this.Btn_Signaling.Size = new System.Drawing.Size(75, 23);
			this.Btn_Signaling.TabIndex = 15;
			this.Btn_Signaling.Text = "信令";
			this.Btn_Signaling.UseVisualStyleBackColor = true;
			this.Btn_Signaling.Click += new System.EventHandler(this.Btn_Signaling_Click);
			// 
			// btn_net_detect
			// 
			this.btn_net_detect.Location = new System.Drawing.Point(110, 136);
			this.btn_net_detect.Name = "btn_net_detect";
			this.btn_net_detect.Size = new System.Drawing.Size(75, 23);
			this.btn_net_detect.TabIndex = 14;
			this.btn_net_detect.Text = "网络探测";
			this.btn_net_detect.UseVisualStyleBackColor = true;
			this.btn_net_detect.Click += new System.EventHandler(this.NetDetectClicked);
			// 
			// multipushCheckbox
			// 
			this.multipushCheckbox.AutoSize = true;
			this.multipushCheckbox.Location = new System.Drawing.Point(113, 44);
			this.multipushCheckbox.Name = "multipushCheckbox";
			this.multipushCheckbox.Size = new System.Drawing.Size(72, 16);
			this.multipushCheckbox.TabIndex = 12;
			this.multipushCheckbox.Text = "多端推送";
			this.multipushCheckbox.UseVisualStyleBackColor = true;
			// 
			// btn_livingstream
			// 
			this.btn_livingstream.Location = new System.Drawing.Point(188, 72);
			this.btn_livingstream.Name = "btn_livingstream";
			this.btn_livingstream.Size = new System.Drawing.Size(75, 23);
			this.btn_livingstream.TabIndex = 11;
			this.btn_livingstream.Text = "直播";
			this.btn_livingstream.UseVisualStyleBackColor = true;
			this.btn_livingstream.Click += new System.EventHandler(this.btn_livingstream_Click);
			// 
			// sysMsgBtn
			// 
			this.sysMsgBtn.Location = new System.Drawing.Point(188, 106);
			this.sysMsgBtn.Name = "sysMsgBtn";
			this.sysMsgBtn.Size = new System.Drawing.Size(75, 23);
			this.sysMsgBtn.TabIndex = 10;
			this.sysMsgBtn.Text = "系统消息";
			this.sysMsgBtn.UseVisualStyleBackColor = true;
			this.sysMsgBtn.Click += new System.EventHandler(this.sysMsgBtn_Click);
			// 
			// exitBtn
			// 
			this.exitBtn.Location = new System.Drawing.Point(326, 69);
			this.exitBtn.Name = "exitBtn";
			this.exitBtn.Size = new System.Drawing.Size(75, 23);
			this.exitBtn.TabIndex = 9;
			this.exitBtn.Text = "退出";
			this.exitBtn.UseVisualStyleBackColor = true;
			this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
			// 
			// chatRoomBtn
			// 
			this.chatRoomBtn.Location = new System.Drawing.Point(188, 135);
			this.chatRoomBtn.Name = "chatRoomBtn";
			this.chatRoomBtn.Size = new System.Drawing.Size(75, 23);
			this.chatRoomBtn.TabIndex = 8;
			this.chatRoomBtn.Text = "聊天室";
			this.chatRoomBtn.UseVisualStyleBackColor = true;
			this.chatRoomBtn.Click += new System.EventHandler(this.chatRoomBtn_Click);
			// 
			// MyProfileBtn
			// 
			this.MyProfileBtn.Location = new System.Drawing.Point(110, 72);
			this.MyProfileBtn.Name = "MyProfileBtn";
			this.MyProfileBtn.Size = new System.Drawing.Size(75, 23);
			this.MyProfileBtn.TabIndex = 7;
			this.MyProfileBtn.Text = "我的名片";
			this.MyProfileBtn.UseVisualStyleBackColor = true;
			this.MyProfileBtn.Click += new System.EventHandler(this.MyProfileBtn_Click);
			// 
			// LogoutBtn
			// 
			this.LogoutBtn.Location = new System.Drawing.Point(326, 40);
			this.LogoutBtn.Name = "LogoutBtn";
			this.LogoutBtn.Size = new System.Drawing.Size(75, 23);
			this.LogoutBtn.TabIndex = 6;
			this.LogoutBtn.Text = "注销";
			this.LogoutBtn.UseVisualStyleBackColor = true;
			this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
			// 
			// SigLabel
			// 
			this.SigLabel.AutoSize = true;
			this.SigLabel.Location = new System.Drawing.Point(12, 176);
			this.SigLabel.Name = "SigLabel";
			this.SigLabel.Size = new System.Drawing.Size(29, 12);
			this.SigLabel.TabIndex = 3;
			this.SigLabel.Text = "签名";
			// 
			// NameLabel
			// 
			this.NameLabel.AutoSize = true;
			this.NameLabel.Location = new System.Drawing.Point(12, 146);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(29, 12);
			this.NameLabel.TabIndex = 2;
			this.NameLabel.Text = "昵称";
			// 
			// IDLabel
			// 
			this.IDLabel.AutoSize = true;
			this.IDLabel.Location = new System.Drawing.Point(12, 117);
			this.IDLabel.Name = "IDLabel";
			this.IDLabel.Size = new System.Drawing.Size(17, 12);
			this.IDLabel.TabIndex = 1;
			this.IDLabel.Text = "ID";
			// 
			// IconPictureBox
			// 
			this.IconPictureBox.Location = new System.Drawing.Point(0, 27);
			this.IconPictureBox.Name = "IconPictureBox";
			this.IconPictureBox.Size = new System.Drawing.Size(86, 79);
			this.IconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.IconPictureBox.TabIndex = 0;
			this.IconPictureBox.TabStop = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AudioToolStripMenuItem,
            this.rTSToolStripMenuItem,
            this.LoginStateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.DocTransToolStripMenuItem,
            this.EventSubsToolStripMenuItem,
            this.toolStripMenuItem2});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(408, 25);
			this.menuStrip1.TabIndex = 13;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuClicked);
			// 
			// AudioToolStripMenuItem
			// 
			this.AudioToolStripMenuItem.MergeIndex = 0;
			this.AudioToolStripMenuItem.Name = "AudioToolStripMenuItem";
			this.AudioToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.AudioToolStripMenuItem.Text = "语音";
			// 
			// rTSToolStripMenuItem
			// 
			this.rTSToolStripMenuItem.MergeIndex = 1;
			this.rTSToolStripMenuItem.Name = "rTSToolStripMenuItem";
			this.rTSToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
			this.rTSToolStripMenuItem.Text = "RTS";
			// 
			// LoginStateToolStripMenuItem
			// 
			this.LoginStateToolStripMenuItem.MergeIndex = 2;
			this.LoginStateToolStripMenuItem.Name = "LoginStateToolStripMenuItem";
			this.LoginStateToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
			this.LoginStateToolStripMenuItem.Text = "登录状态";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.MergeIndex = 3;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 21);
			this.toolStripMenuItem1.Text = "http";
			// 
			// DocTransToolStripMenuItem
			// 
			this.DocTransToolStripMenuItem.MergeIndex = 4;
			this.DocTransToolStripMenuItem.Name = "DocTransToolStripMenuItem";
			this.DocTransToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
			this.DocTransToolStripMenuItem.Text = "文档转换";
			// 
			// EventSubsToolStripMenuItem
			// 
			this.EventSubsToolStripMenuItem.MergeIndex = 6;
			this.EventSubsToolStripMenuItem.Name = "EventSubsToolStripMenuItem";
			this.EventSubsToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
			this.EventSubsToolStripMenuItem.Text = "订阅事件";
			this.EventSubsToolStripMenuItem.Click += new System.EventHandler(this.EventSubsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.MergeIndex = 7;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(56, 21);
			this.toolStripMenuItem2.Text = "Robot";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(408, 521);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listView1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(400, 495);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "好友";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(3, 3);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(394, 489);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ID";
			this.columnHeader1.Width = 360;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.TeamListView);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(400, 495);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "群组";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// TeamListView
			// 
			this.TeamListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
			this.TeamListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TeamListView.FullRowSelect = true;
			this.TeamListView.Location = new System.Drawing.Point(3, 3);
			this.TeamListView.Name = "TeamListView";
			this.TeamListView.Size = new System.Drawing.Size(394, 489);
			this.TeamListView.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.TeamListView.TabIndex = 0;
			this.TeamListView.UseCompatibleStateImageBehavior = false;
			this.TeamListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "群ID";
			this.columnHeader2.Width = 196;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "群名称";
			this.columnHeader3.Width = 206;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.chatListView);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(400, 495);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "消息";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// chatListView
			// 
			this.chatListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
			this.chatListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatListView.Location = new System.Drawing.Point(3, 3);
			this.chatListView.Name = "chatListView";
			this.chatListView.Size = new System.Drawing.Size(394, 489);
			this.chatListView.TabIndex = 0;
			this.chatListView.UseCompatibleStateImageBehavior = false;
			this.chatListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Width = 106;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Width = 206;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.recentSessionListbox);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(400, 495);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "会话";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// recentSessionListbox
			// 
			this.recentSessionListbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.recentSessionListbox.FormattingEnabled = true;
			this.recentSessionListbox.ItemHeight = 12;
			this.recentSessionListbox.Location = new System.Drawing.Point(3, 3);
			this.recentSessionListbox.Name = "recentSessionListbox";
			this.recentSessionListbox.Size = new System.Drawing.Size(394, 489);
			this.recentSessionListbox.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(408, 728);
			this.Controls.Add(this.splitContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "云信Demo";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox IconPictureBox;
        private System.Windows.Forms.Label SigLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button MyProfileBtn;
        private System.Windows.Forms.ListView TeamListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView chatListView;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox recentSessionListbox;
        private System.Windows.Forms.Button chatRoomBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button sysMsgBtn;
		private System.Windows.Forms.Button btn_livingstream;
        private System.Windows.Forms.CheckBox multipushCheckbox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rTSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DocTransToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EventSubsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button btn_net_detect;
        private System.Windows.Forms.Button Btn_Signaling;
        private System.Windows.Forms.Button BtnMsgOpt;
    }
}

