namespace NIMDemo
{
    partial class SignalingForm
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
            this.Btn_Signaling_Create = new System.Windows.Forms.Button();
            this.Btn_Signaling_Close = new System.Windows.Forms.Button();
            this.Btn_Signaling_Join = new System.Windows.Forms.Button();
            this.Btn_Signaling_Leave = new System.Windows.Forms.Button();
            this.Btn_Signaling_Call = new System.Windows.Forms.Button();
            this.Btn_Signaling_Invite = new System.Windows.Forms.Button();
            this.Btn_Signaling_Cancel_Invite = new System.Windows.Forms.Button();
            this.Btn_Signaling_Reject = new System.Windows.Forms.Button();
            this.Btn_Signaling_Control = new System.Windows.Forms.Button();
            this.Btn_Signaling_Accept = new System.Windows.Forms.Button();
            this.lbMembers = new System.Windows.Forms.ListBox();
            this.Rtb_Info = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbUid = new System.Windows.Forms.TextBox();
            this.tbCreator = new System.Windows.Forms.TextBox();
            this.tbChannelExt = new System.Windows.Forms.TextBox();
            this.tbChannelId = new System.Windows.Forms.TextBox();
            this.tbChannelName = new System.Windows.Forms.TextBox();
            this.cbChannelType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbNotReadCount = new System.Windows.Forms.CheckBox();
            this.cbOpenPush = new System.Windows.Forms.CheckBox();
            this.tbPayLoad = new System.Windows.Forms.TextBox();
            this.tbPushContent = new System.Windows.Forms.TextBox();
            this.tbPushTitle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbAutoJoinUid = new System.Windows.Forms.CheckBox();
            this.cbOfflineSupport = new System.Windows.Forms.CheckBox();
            this.btReserveExt = new System.Windows.Forms.TextBox();
            this.tbOptExt = new System.Windows.Forms.TextBox();
            this.tbInviteId = new System.Windows.Forms.TextBox();
            this.tbToAccount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Signaling_Create
            // 
            this.Btn_Signaling_Create.Location = new System.Drawing.Point(12, 30);
            this.Btn_Signaling_Create.Name = "Btn_Signaling_Create";
            this.Btn_Signaling_Create.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Create.TabIndex = 0;
            this.Btn_Signaling_Create.Text = "Create";
            this.Btn_Signaling_Create.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Create.Click += new System.EventHandler(this.Btn_Signaling_Create_Click);
            // 
            // Btn_Signaling_Close
            // 
            this.Btn_Signaling_Close.Location = new System.Drawing.Point(12, 63);
            this.Btn_Signaling_Close.Name = "Btn_Signaling_Close";
            this.Btn_Signaling_Close.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Close.TabIndex = 1;
            this.Btn_Signaling_Close.Text = "Close";
            this.Btn_Signaling_Close.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Close.Click += new System.EventHandler(this.Btn_Signaling_Close_Click);
            // 
            // Btn_Signaling_Join
            // 
            this.Btn_Signaling_Join.Location = new System.Drawing.Point(12, 96);
            this.Btn_Signaling_Join.Name = "Btn_Signaling_Join";
            this.Btn_Signaling_Join.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Join.TabIndex = 2;
            this.Btn_Signaling_Join.Text = "Join";
            this.Btn_Signaling_Join.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Join.Click += new System.EventHandler(this.Btn_Signaling_Join_Click);
            // 
            // Btn_Signaling_Leave
            // 
            this.Btn_Signaling_Leave.Location = new System.Drawing.Point(12, 129);
            this.Btn_Signaling_Leave.Name = "Btn_Signaling_Leave";
            this.Btn_Signaling_Leave.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Leave.TabIndex = 3;
            this.Btn_Signaling_Leave.Text = "Leave";
            this.Btn_Signaling_Leave.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Leave.Click += new System.EventHandler(this.Btn_Signaling_Leave_Click);
            // 
            // Btn_Signaling_Call
            // 
            this.Btn_Signaling_Call.Location = new System.Drawing.Point(12, 158);
            this.Btn_Signaling_Call.Name = "Btn_Signaling_Call";
            this.Btn_Signaling_Call.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Call.TabIndex = 4;
            this.Btn_Signaling_Call.Text = "Call";
            this.Btn_Signaling_Call.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Call.Click += new System.EventHandler(this.Btn_Signaling_Call_Click);
            // 
            // Btn_Signaling_Invite
            // 
            this.Btn_Signaling_Invite.Location = new System.Drawing.Point(12, 207);
            this.Btn_Signaling_Invite.Name = "Btn_Signaling_Invite";
            this.Btn_Signaling_Invite.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Invite.TabIndex = 5;
            this.Btn_Signaling_Invite.Text = "Invite";
            this.Btn_Signaling_Invite.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Invite.Click += new System.EventHandler(this.Btn_Signaling_Invite_Click);
            // 
            // Btn_Signaling_Cancel_Invite
            // 
            this.Btn_Signaling_Cancel_Invite.Location = new System.Drawing.Point(10, 240);
            this.Btn_Signaling_Cancel_Invite.Name = "Btn_Signaling_Cancel_Invite";
            this.Btn_Signaling_Cancel_Invite.Size = new System.Drawing.Size(86, 23);
            this.Btn_Signaling_Cancel_Invite.TabIndex = 6;
            this.Btn_Signaling_Cancel_Invite.Text = "CancelInvite";
            this.Btn_Signaling_Cancel_Invite.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Cancel_Invite.Click += new System.EventHandler(this.Btn_Signaling_Cancel_Invite_Click);
            // 
            // Btn_Signaling_Reject
            // 
            this.Btn_Signaling_Reject.Location = new System.Drawing.Point(10, 306);
            this.Btn_Signaling_Reject.Name = "Btn_Signaling_Reject";
            this.Btn_Signaling_Reject.Size = new System.Drawing.Size(86, 23);
            this.Btn_Signaling_Reject.TabIndex = 7;
            this.Btn_Signaling_Reject.Text = "Reject";
            this.Btn_Signaling_Reject.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Reject.Click += new System.EventHandler(this.Btn_Signaling_Reject_Click);
            // 
            // Btn_Signaling_Control
            // 
            this.Btn_Signaling_Control.Location = new System.Drawing.Point(12, 339);
            this.Btn_Signaling_Control.Name = "Btn_Signaling_Control";
            this.Btn_Signaling_Control.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Control.TabIndex = 8;
            this.Btn_Signaling_Control.Text = "Control";
            this.Btn_Signaling_Control.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Control.Click += new System.EventHandler(this.Btn_Signaling_Control_Click);
            // 
            // Btn_Signaling_Accept
            // 
            this.Btn_Signaling_Accept.Location = new System.Drawing.Point(12, 273);
            this.Btn_Signaling_Accept.Name = "Btn_Signaling_Accept";
            this.Btn_Signaling_Accept.Size = new System.Drawing.Size(84, 23);
            this.Btn_Signaling_Accept.TabIndex = 9;
            this.Btn_Signaling_Accept.Text = "Accept";
            this.Btn_Signaling_Accept.UseVisualStyleBackColor = true;
            this.Btn_Signaling_Accept.Click += new System.EventHandler(this.Btn_Signaling_Accept_Click);
            // 
            // lbMembers
            // 
            this.lbMembers.FormattingEnabled = true;
            this.lbMembers.ItemHeight = 12;
            this.lbMembers.Location = new System.Drawing.Point(431, 30);
            this.lbMembers.Name = "lbMembers";
            this.lbMembers.Size = new System.Drawing.Size(244, 148);
            this.lbMembers.TabIndex = 11;
            // 
            // Rtb_Info
            // 
            this.Rtb_Info.Location = new System.Drawing.Point(431, 207);
            this.Rtb_Info.Name = "Rtb_Info";
            this.Rtb_Info.Size = new System.Drawing.Size(244, 221);
            this.Rtb_Info.TabIndex = 13;
            this.Rtb_Info.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbUid);
            this.groupBox1.Controls.Add(this.tbCreator);
            this.groupBox1.Controls.Add(this.tbChannelExt);
            this.groupBox1.Controls.Add(this.tbChannelId);
            this.groupBox1.Controls.Add(this.tbChannelName);
            this.groupBox1.Controls.Add(this.cbChannelType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(101, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 151);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "频道信息";
            // 
            // tbUid
            // 
            this.tbUid.Location = new System.Drawing.Point(229, 47);
            this.tbUid.Name = "tbUid";
            this.tbUid.Size = new System.Drawing.Size(82, 21);
            this.tbUid.TabIndex = 13;
            // 
            // tbCreator
            // 
            this.tbCreator.Location = new System.Drawing.Point(229, 17);
            this.tbCreator.Name = "tbCreator";
            this.tbCreator.Size = new System.Drawing.Size(82, 21);
            this.tbCreator.TabIndex = 11;
            // 
            // tbChannelExt
            // 
            this.tbChannelExt.Location = new System.Drawing.Point(79, 109);
            this.tbChannelExt.Name = "tbChannelExt";
            this.tbChannelExt.Size = new System.Drawing.Size(232, 21);
            this.tbChannelExt.TabIndex = 10;
            // 
            // tbChannelId
            // 
            this.tbChannelId.Location = new System.Drawing.Point(79, 75);
            this.tbChannelId.Name = "tbChannelId";
            this.tbChannelId.Size = new System.Drawing.Size(79, 21);
            this.tbChannelId.TabIndex = 9;
            // 
            // tbChannelName
            // 
            this.tbChannelName.Location = new System.Drawing.Point(79, 45);
            this.tbChannelName.Name = "tbChannelName";
            this.tbChannelName.Size = new System.Drawing.Size(79, 21);
            this.tbChannelName.TabIndex = 8;
            // 
            // cbChannelType
            // 
            this.cbChannelType.FormattingEnabled = true;
            this.cbChannelType.Items.AddRange(new object[] {
            "音频",
            "视频",
            "自定义"});
            this.cbChannelType.Location = new System.Drawing.Point(79, 18);
            this.cbChannelType.Name = "cbChannelType";
            this.cbChannelType.Size = new System.Drawing.Size(79, 20);
            this.cbChannelType.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Uid";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "频道拓展";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "创建者";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "频道Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "频道名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "频道类型";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbNotReadCount);
            this.groupBox2.Controls.Add(this.cbOpenPush);
            this.groupBox2.Controls.Add(this.tbPayLoad);
            this.groupBox2.Controls.Add(this.tbPushContent);
            this.groupBox2.Controls.Add(this.tbPushTitle);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cbAutoJoinUid);
            this.groupBox2.Controls.Add(this.cbOfflineSupport);
            this.groupBox2.Controls.Add(this.btReserveExt);
            this.groupBox2.Controls.Add(this.tbOptExt);
            this.groupBox2.Controls.Add(this.tbInviteId);
            this.groupBox2.Controls.Add(this.tbToAccount);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(103, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 155);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作信息";
            // 
            // cbNotReadCount
            // 
            this.cbNotReadCount.AutoSize = true;
            this.cbNotReadCount.Location = new System.Drawing.Point(243, 132);
            this.cbNotReadCount.Name = "cbNotReadCount";
            this.cbNotReadCount.Size = new System.Drawing.Size(72, 16);
            this.cbNotReadCount.TabIndex = 17;
            this.cbNotReadCount.Text = "未读计数";
            this.cbNotReadCount.UseVisualStyleBackColor = true;
            // 
            // cbOpenPush
            // 
            this.cbOpenPush.AutoSize = true;
            this.cbOpenPush.Location = new System.Drawing.Point(173, 133);
            this.cbOpenPush.Name = "cbOpenPush";
            this.cbOpenPush.Size = new System.Drawing.Size(72, 16);
            this.cbOpenPush.TabIndex = 16;
            this.cbOpenPush.Text = "打开推送";
            this.cbOpenPush.UseVisualStyleBackColor = true;
            // 
            // tbPayLoad
            // 
            this.tbPayLoad.Location = new System.Drawing.Point(251, 77);
            this.tbPayLoad.Name = "tbPayLoad";
            this.tbPayLoad.Size = new System.Drawing.Size(58, 21);
            this.tbPayLoad.TabIndex = 15;
            // 
            // tbPushContent
            // 
            this.tbPushContent.Location = new System.Drawing.Point(251, 51);
            this.tbPushContent.Name = "tbPushContent";
            this.tbPushContent.Size = new System.Drawing.Size(58, 21);
            this.tbPushContent.TabIndex = 14;
            // 
            // tbPushTitle
            // 
            this.tbPushTitle.Location = new System.Drawing.Point(251, 21);
            this.tbPushTitle.Name = "tbPushTitle";
            this.tbPushTitle.Size = new System.Drawing.Size(58, 21);
            this.tbPushTitle.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(198, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "payload";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(192, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 11;
            this.label14.Text = "推送内容";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(192, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "推送标题";
            // 
            // cbAutoJoinUid
            // 
            this.cbAutoJoinUid.AutoSize = true;
            this.cbAutoJoinUid.Location = new System.Drawing.Point(84, 133);
            this.cbAutoJoinUid.Name = "cbAutoJoinUid";
            this.cbAutoJoinUid.Size = new System.Drawing.Size(90, 16);
            this.cbAutoJoinUid.TabIndex = 9;
            this.cbAutoJoinUid.Text = "自动加入uid";
            this.cbAutoJoinUid.UseVisualStyleBackColor = true;
            // 
            // cbOfflineSupport
            // 
            this.cbOfflineSupport.AutoSize = true;
            this.cbOfflineSupport.Location = new System.Drawing.Point(12, 133);
            this.cbOfflineSupport.Name = "cbOfflineSupport";
            this.cbOfflineSupport.Size = new System.Drawing.Size(72, 16);
            this.cbOfflineSupport.TabIndex = 8;
            this.cbOfflineSupport.Text = "离线支持";
            this.cbOfflineSupport.UseVisualStyleBackColor = true;
            // 
            // btReserveExt
            // 
            this.btReserveExt.Location = new System.Drawing.Point(77, 101);
            this.btReserveExt.Name = "btReserveExt";
            this.btReserveExt.Size = new System.Drawing.Size(109, 21);
            this.btReserveExt.TabIndex = 7;
            // 
            // tbOptExt
            // 
            this.tbOptExt.Location = new System.Drawing.Point(77, 75);
            this.tbOptExt.Name = "tbOptExt";
            this.tbOptExt.Size = new System.Drawing.Size(109, 21);
            this.tbOptExt.TabIndex = 6;
            // 
            // tbInviteId
            // 
            this.tbInviteId.Location = new System.Drawing.Point(77, 48);
            this.tbInviteId.Name = "tbInviteId";
            this.tbInviteId.Size = new System.Drawing.Size(109, 21);
            this.tbInviteId.TabIndex = 5;
            // 
            // tbToAccount
            // 
            this.tbToAccount.Location = new System.Drawing.Point(77, 21);
            this.tbToAccount.Name = "tbToAccount";
            this.tbToAccount.Size = new System.Drawing.Size(109, 21);
            this.tbToAccount.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "备用扩展";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "操作扩展";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "邀请ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "对方账号";
            // 
            // SignalingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 440);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Rtb_Info);
            this.Controls.Add(this.lbMembers);
            this.Controls.Add(this.Btn_Signaling_Accept);
            this.Controls.Add(this.Btn_Signaling_Control);
            this.Controls.Add(this.Btn_Signaling_Reject);
            this.Controls.Add(this.Btn_Signaling_Cancel_Invite);
            this.Controls.Add(this.Btn_Signaling_Invite);
            this.Controls.Add(this.Btn_Signaling_Call);
            this.Controls.Add(this.Btn_Signaling_Leave);
            this.Controls.Add(this.Btn_Signaling_Join);
            this.Controls.Add(this.Btn_Signaling_Close);
            this.Controls.Add(this.Btn_Signaling_Create);
            this.Name = "SignalingForm";
            this.Text = "独立信令";
            this.Load += new System.EventHandler(this.SignalingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Signaling_Create;
        private System.Windows.Forms.Button Btn_Signaling_Close;
        private System.Windows.Forms.Button Btn_Signaling_Join;
        private System.Windows.Forms.Button Btn_Signaling_Leave;
        private System.Windows.Forms.Button Btn_Signaling_Call;
        private System.Windows.Forms.Button Btn_Signaling_Invite;
        private System.Windows.Forms.Button Btn_Signaling_Cancel_Invite;
        private System.Windows.Forms.Button Btn_Signaling_Reject;
        private System.Windows.Forms.Button Btn_Signaling_Control;
        private System.Windows.Forms.Button Btn_Signaling_Accept;
        private System.Windows.Forms.ListBox lbMembers;
        private System.Windows.Forms.RichTextBox Rtb_Info;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbUid;
        private System.Windows.Forms.TextBox tbCreator;
        private System.Windows.Forms.TextBox tbChannelExt;
        private System.Windows.Forms.TextBox tbChannelId;
        private System.Windows.Forms.TextBox tbChannelName;
        private System.Windows.Forms.ComboBox cbChannelType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbNotReadCount;
        private System.Windows.Forms.CheckBox cbOpenPush;
        private System.Windows.Forms.TextBox tbPayLoad;
        private System.Windows.Forms.TextBox tbPushContent;
        private System.Windows.Forms.TextBox tbPushTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbAutoJoinUid;
        private System.Windows.Forms.CheckBox cbOfflineSupport;
        private System.Windows.Forms.TextBox btReserveExt;
        private System.Windows.Forms.TextBox tbOptExt;
        private System.Windows.Forms.TextBox tbInviteId;
        private System.Windows.Forms.TextBox tbToAccount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}