namespace NIMDemo
{
    partial class VideoChatForm
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
            this.peerPicBox = new System.Windows.Forms.PictureBox();
            this.panel_invite = new System.Windows.Forms.Panel();
            this.lb_invite_info = new System.Windows.Forms.Label();
            this.btn_invite_cancel = new System.Windows.Forms.Button();
            this.panel_chating = new System.Windows.Forms.Panel();
            this.minePicBox = new System.Windows.Forms.PictureBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btn_screen_capture_ = new System.Windows.Forms.Button();
            this.btnSetMute = new System.Windows.Forms.Button();
            this.cb_video_clip_ = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_setting = new System.Windows.Forms.Button();
            this.btnRecordAudio = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_vid = new System.Windows.Forms.CheckBox();
            this.cb_setquality = new System.Windows.Forms.ComboBox();
            this.cb_ns = new System.Windows.Forms.CheckBox();
            this.btn_beauty = new System.Windows.Forms.Button();
            this.cb_aec = new System.Windows.Forms.CheckBox();
            this.btn_accompany = new System.Windows.Forms.Button();
            this.tb_player_path_ = new System.Windows.Forms.TextBox();
            this.panel_notify = new System.Windows.Forms.Panel();
            this.lb_notify_info = new System.Windows.Forms.Label();
            this.btn_refuse = new System.Windows.Forms.Button();
            this.btn_accept = new System.Windows.Forms.Button();
            this.panel_end = new System.Windows.Forms.Panel();
            this.lb_end_info = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peerPicBox)).BeginInit();
            this.panel_invite.SuspendLayout();
            this.panel_chating.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minePicBox)).BeginInit();
            this.panel_notify.SuspendLayout();
            this.panel_end.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.peerPicBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel_invite);
            this.splitContainer1.Panel2.Controls.Add(this.panel_chating);
            this.splitContainer1.Panel2.Controls.Add(this.panel_notify);
            this.splitContainer1.Panel2.Controls.Add(this.panel_end);
            this.splitContainer1.Size = new System.Drawing.Size(395, 776);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.TabIndex = 0;
            // 
            // peerPicBox
            // 
            this.peerPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peerPicBox.Location = new System.Drawing.Point(0, 0);
            this.peerPicBox.Name = "peerPicBox";
            this.peerPicBox.Size = new System.Drawing.Size(395, 445);
            this.peerPicBox.TabIndex = 0;
            this.peerPicBox.TabStop = false;
            // 
            // panel_invite
            // 
            this.panel_invite.Controls.Add(this.lb_invite_info);
            this.panel_invite.Controls.Add(this.btn_invite_cancel);
            this.panel_invite.Location = new System.Drawing.Point(0, 0);
            this.panel_invite.Name = "panel_invite";
            this.panel_invite.Size = new System.Drawing.Size(386, 307);
            this.panel_invite.TabIndex = 24;
            // 
            // lb_invite_info
            // 
            this.lb_invite_info.AutoSize = true;
            this.lb_invite_info.Location = new System.Drawing.Point(177, 122);
            this.lb_invite_info.Name = "lb_invite_info";
            this.lb_invite_info.Size = new System.Drawing.Size(41, 12);
            this.lb_invite_info.TabIndex = 1;
            this.lb_invite_info.Text = "label3";
            // 
            // btn_invite_cancel
            // 
            this.btn_invite_cancel.Location = new System.Drawing.Point(161, 202);
            this.btn_invite_cancel.Name = "btn_invite_cancel";
            this.btn_invite_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_invite_cancel.TabIndex = 0;
            this.btn_invite_cancel.Text = "挂断";
            this.btn_invite_cancel.UseVisualStyleBackColor = true;
            this.btn_invite_cancel.Click += new System.EventHandler(this.BtnInviteCancelClick);
            // 
            // panel_chating
            // 
            this.panel_chating.Controls.Add(this.minePicBox);
            this.panel_chating.Controls.Add(this.btnRecord);
            this.panel_chating.Controls.Add(this.btn_screen_capture_);
            this.panel_chating.Controls.Add(this.btnSetMute);
            this.panel_chating.Controls.Add(this.cb_video_clip_);
            this.panel_chating.Controls.Add(this.button1);
            this.panel_chating.Controls.Add(this.label2);
            this.panel_chating.Controls.Add(this.bt_setting);
            this.panel_chating.Controls.Add(this.btnRecordAudio);
            this.panel_chating.Controls.Add(this.label1);
            this.panel_chating.Controls.Add(this.cb_vid);
            this.panel_chating.Controls.Add(this.cb_setquality);
            this.panel_chating.Controls.Add(this.cb_ns);
            this.panel_chating.Controls.Add(this.btn_beauty);
            this.panel_chating.Controls.Add(this.cb_aec);
            this.panel_chating.Controls.Add(this.btn_accompany);
            this.panel_chating.Controls.Add(this.tb_player_path_);
            this.panel_chating.Location = new System.Drawing.Point(0, 0);
            this.panel_chating.Name = "panel_chating";
            this.panel_chating.Size = new System.Drawing.Size(384, 324);
            this.panel_chating.TabIndex = 22;
            // 
            // minePicBox
            // 
            this.minePicBox.Location = new System.Drawing.Point(148, 14);
            this.minePicBox.Name = "minePicBox";
            this.minePicBox.Size = new System.Drawing.Size(215, 140);
            this.minePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.minePicBox.TabIndex = 0;
            this.minePicBox.TabStop = false;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(9, 42);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(89, 23);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "录制MP4";
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btn_screen_capture_
            // 
            this.btn_screen_capture_.Location = new System.Drawing.Point(9, 282);
            this.btn_screen_capture_.Name = "btn_screen_capture_";
            this.btn_screen_capture_.Size = new System.Drawing.Size(89, 23);
            this.btn_screen_capture_.TabIndex = 20;
            this.btn_screen_capture_.Text = "桌面截屏";
            this.btn_screen_capture_.UseVisualStyleBackColor = true;
            this.btn_screen_capture_.Click += new System.EventHandler(this.BtnScreenCaptureClick);
            // 
            // btnSetMute
            // 
            this.btnSetMute.Location = new System.Drawing.Point(9, 95);
            this.btnSetMute.Name = "btnSetMute";
            this.btnSetMute.Size = new System.Drawing.Size(89, 23);
            this.btnSetMute.TabIndex = 2;
            this.btnSetMute.Text = "设置静音";
            this.btnSetMute.UseVisualStyleBackColor = true;
            this.btnSetMute.Click += new System.EventHandler(this.btnSetMute_Click_1);
            // 
            // cb_video_clip_
            // 
            this.cb_video_clip_.FormattingEnabled = true;
            this.cb_video_clip_.Location = new System.Drawing.Point(148, 251);
            this.cb_video_clip_.Name = "cb_video_clip_";
            this.cb_video_clip_.Size = new System.Drawing.Size(215, 20);
            this.cb_video_clip_.TabIndex = 19;
            this.cb_video_clip_.SelectedIndexChanged += new System.EventHandler(this.cb_video_clip_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "挂断";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "画面裁剪";
            // 
            // bt_setting
            // 
            this.bt_setting.Location = new System.Drawing.Point(9, 125);
            this.bt_setting.Name = "bt_setting";
            this.bt_setting.Size = new System.Drawing.Size(89, 23);
            this.bt_setting.TabIndex = 4;
            this.bt_setting.Text = "音视频设置";
            this.bt_setting.UseVisualStyleBackColor = true;
            this.bt_setting.Click += new System.EventHandler(this.bt_setting_Click);
            // 
            // btnRecordAudio
            // 
            this.btnRecordAudio.Location = new System.Drawing.Point(9, 68);
            this.btnRecordAudio.Name = "btnRecordAudio";
            this.btnRecordAudio.Size = new System.Drawing.Size(89, 23);
            this.btnRecordAudio.TabIndex = 17;
            this.btnRecordAudio.Text = "录制音频";
            this.btnRecordAudio.Click += new System.EventHandler(this.btnRecordAudio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "分辨率设置";
            // 
            // cb_vid
            // 
            this.cb_vid.AutoSize = true;
            this.cb_vid.Checked = true;
            this.cb_vid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_vid.Location = new System.Drawing.Point(291, 186);
            this.cb_vid.Name = "cb_vid";
            this.cb_vid.Size = new System.Drawing.Size(72, 16);
            this.cb_vid.TabIndex = 16;
            this.cb_vid.Text = "人言检测";
            this.cb_vid.UseVisualStyleBackColor = true;
            this.cb_vid.CheckedChanged += new System.EventHandler(this.cb_vid_CheckedChanged);
            // 
            // cb_setquality
            // 
            this.cb_setquality.FormattingEnabled = true;
            this.cb_setquality.Location = new System.Drawing.Point(148, 160);
            this.cb_setquality.Name = "cb_setquality";
            this.cb_setquality.Size = new System.Drawing.Size(215, 20);
            this.cb_setquality.TabIndex = 6;
            this.cb_setquality.SelectedIndexChanged += new System.EventHandler(this.cb_setquality_SelectedIndexChanged);
            // 
            // cb_ns
            // 
            this.cb_ns.AutoSize = true;
            this.cb_ns.Checked = true;
            this.cb_ns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ns.Location = new System.Drawing.Point(226, 186);
            this.cb_ns.Name = "cb_ns";
            this.cb_ns.Size = new System.Drawing.Size(48, 16);
            this.cb_ns.TabIndex = 15;
            this.cb_ns.Text = "降噪";
            this.cb_ns.UseVisualStyleBackColor = true;
            this.cb_ns.CheckedChanged += new System.EventHandler(this.cb_ns_CheckedChanged);
            // 
            // btn_beauty
            // 
            this.btn_beauty.Location = new System.Drawing.Point(9, 186);
            this.btn_beauty.Name = "btn_beauty";
            this.btn_beauty.Size = new System.Drawing.Size(89, 23);
            this.btn_beauty.TabIndex = 7;
            this.btn_beauty.Text = "美颜（开）";
            this.btn_beauty.UseVisualStyleBackColor = true;
            this.btn_beauty.Click += new System.EventHandler(this.btn_beauty_Click);
            // 
            // cb_aec
            // 
            this.cb_aec.AutoSize = true;
            this.cb_aec.Checked = true;
            this.cb_aec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_aec.Location = new System.Drawing.Point(148, 186);
            this.cb_aec.Name = "cb_aec";
            this.cb_aec.Size = new System.Drawing.Size(72, 16);
            this.cb_aec.TabIndex = 14;
            this.cb_aec.Text = "回音消除";
            this.cb_aec.UseVisualStyleBackColor = true;
            this.cb_aec.CheckedChanged += new System.EventHandler(this.cb_aec_CheckedChanged);
            // 
            // btn_accompany
            // 
            this.btn_accompany.Location = new System.Drawing.Point(9, 216);
            this.btn_accompany.Name = "btn_accompany";
            this.btn_accompany.Size = new System.Drawing.Size(89, 23);
            this.btn_accompany.TabIndex = 8;
            this.btn_accompany.Text = "伴奏（开）";
            this.btn_accompany.UseVisualStyleBackColor = true;
            this.btn_accompany.Click += new System.EventHandler(this.btn_accompany_Click);
            // 
            // tb_player_path_
            // 
            this.tb_player_path_.Location = new System.Drawing.Point(148, 216);
            this.tb_player_path_.Name = "tb_player_path_";
            this.tb_player_path_.Size = new System.Drawing.Size(215, 21);
            this.tb_player_path_.TabIndex = 9;
            this.tb_player_path_.Text = "播放器路径";
            // 
            // panel_notify
            // 
            this.panel_notify.Controls.Add(this.lb_notify_info);
            this.panel_notify.Controls.Add(this.btn_refuse);
            this.panel_notify.Controls.Add(this.btn_accept);
            this.panel_notify.Location = new System.Drawing.Point(0, 0);
            this.panel_notify.Name = "panel_notify";
            this.panel_notify.Size = new System.Drawing.Size(389, 321);
            this.panel_notify.TabIndex = 21;
            this.panel_notify.Visible = false;
            // 
            // lb_notify_info
            // 
            this.lb_notify_info.AutoSize = true;
            this.lb_notify_info.Location = new System.Drawing.Point(107, 70);
            this.lb_notify_info.Name = "lb_notify_info";
            this.lb_notify_info.Size = new System.Drawing.Size(41, 12);
            this.lb_notify_info.TabIndex = 2;
            this.lb_notify_info.Text = "label3";
            // 
            // btn_refuse
            // 
            this.btn_refuse.Location = new System.Drawing.Point(227, 155);
            this.btn_refuse.Name = "btn_refuse";
            this.btn_refuse.Size = new System.Drawing.Size(75, 23);
            this.btn_refuse.TabIndex = 1;
            this.btn_refuse.Text = "拒绝";
            this.btn_refuse.UseVisualStyleBackColor = true;
            this.btn_refuse.Click += new System.EventHandler(this.BtnRefuseClick);
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(34, 156);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(75, 23);
            this.btn_accept.TabIndex = 0;
            this.btn_accept.Text = "接听";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.BtnAcceptClick);
            // 
            // panel_end
            // 
            this.panel_end.Controls.Add(this.lb_end_info);
            this.panel_end.Location = new System.Drawing.Point(0, 0);
            this.panel_end.Name = "panel_end";
            this.panel_end.Size = new System.Drawing.Size(386, 321);
            this.panel_end.TabIndex = 23;
            this.panel_end.Visible = false;
            // 
            // lb_end_info
            // 
            this.lb_end_info.AutoSize = true;
            this.lb_end_info.Location = new System.Drawing.Point(143, 142);
            this.lb_end_info.Name = "lb_end_info";
            this.lb_end_info.Size = new System.Drawing.Size(41, 12);
            this.lb_end_info.TabIndex = 0;
            this.lb_end_info.Text = "label3";
            // 
            // VideoChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 776);
            this.Controls.Add(this.splitContainer1);
            this.Name = "VideoChatForm";
            this.Text = "音视频通话";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peerPicBox)).EndInit();
            this.panel_invite.ResumeLayout(false);
            this.panel_invite.PerformLayout();
            this.panel_chating.ResumeLayout(false);
            this.panel_chating.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minePicBox)).EndInit();
            this.panel_notify.ResumeLayout(false);
            this.panel_notify.PerformLayout();
            this.panel_end.ResumeLayout(false);
            this.panel_end.PerformLayout();
            this.ResumeLayout(false);

        }

		private void BtnSetMute_Click(object sender, System.EventArgs e)
		{
			NIM.VChatAPI.SetAudioMute(true);
		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox peerPicBox;
        private System.Windows.Forms.PictureBox minePicBox;
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnSetMute;
		private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button bt_setting;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cb_setquality;
		private System.Windows.Forms.Button btn_beauty;
        private System.Windows.Forms.Button btn_accompany;
        private System.Windows.Forms.TextBox tb_player_path_;
        private System.Windows.Forms.CheckBox cb_vid;
        private System.Windows.Forms.CheckBox cb_ns;
        private System.Windows.Forms.CheckBox cb_aec;
		private System.Windows.Forms.Button btnRecordAudio;
		private System.Windows.Forms.ComboBox cb_video_clip_;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_screen_capture_;
        private System.Windows.Forms.Label lb_notify_info;
        private System.Windows.Forms.Button btn_refuse;
        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.Panel panel_chating;
        private System.Windows.Forms.Panel panel_end;
        private System.Windows.Forms.Label lb_end_info;
        private System.Windows.Forms.Panel panel_notify;
        private System.Windows.Forms.Panel panel_invite;
        private System.Windows.Forms.Label lb_invite_info;
        private System.Windows.Forms.Button btn_invite_cancel;
    }
}