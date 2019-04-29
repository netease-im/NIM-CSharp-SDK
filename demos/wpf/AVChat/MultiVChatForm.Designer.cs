namespace NIMDemo
{
	partial class MultiVChatForm
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
            this.components = new System.ComponentModel.Container();
            this.btn_joinmultiroom = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pb_multivchat_01 = new System.Windows.Forms.PictureBox();
            this.pb_multivchat_02 = new System.Windows.Forms.PictureBox();
            this.pb_multivchat_03 = new System.Windows.Forms.PictureBox();
            this.pb_multivchat_04 = new System.Windows.Forms.PictureBox();
            this.gb_multichat_info = new System.Windows.Forms.GroupBox();
            this.rtb_multichat_info = new System.Windows.Forms.RichTextBox();
            this.gb_video = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv_members = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_roomid = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_03)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_04)).BeginInit();
            this.gb_multichat_info.SuspendLayout();
            this.gb_video.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_joinmultiroom
            // 
            this.btn_joinmultiroom.Location = new System.Drawing.Point(514, 369);
            this.btn_joinmultiroom.Name = "btn_joinmultiroom";
            this.btn_joinmultiroom.Size = new System.Drawing.Size(86, 23);
            this.btn_joinmultiroom.TabIndex = 1;
            this.btn_joinmultiroom.Text = "音视频设置";
            this.btn_joinmultiroom.UseVisualStyleBackColor = true;
            this.btn_joinmultiroom.Click += new System.EventHandler(this.btn_videosetting_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pb_multivchat_01
            // 
            this.pb_multivchat_01.Location = new System.Drawing.Point(11, 30);
            this.pb_multivchat_01.Name = "pb_multivchat_01";
            this.pb_multivchat_01.Size = new System.Drawing.Size(190, 126);
            this.pb_multivchat_01.TabIndex = 2;
            this.pb_multivchat_01.TabStop = false;
            // 
            // pb_multivchat_02
            // 
            this.pb_multivchat_02.Location = new System.Drawing.Point(245, 30);
            this.pb_multivchat_02.Name = "pb_multivchat_02";
            this.pb_multivchat_02.Size = new System.Drawing.Size(173, 126);
            this.pb_multivchat_02.TabIndex = 3;
            this.pb_multivchat_02.TabStop = false;
            // 
            // pb_multivchat_03
            // 
            this.pb_multivchat_03.Location = new System.Drawing.Point(11, 195);
            this.pb_multivchat_03.Name = "pb_multivchat_03";
            this.pb_multivchat_03.Size = new System.Drawing.Size(190, 126);
            this.pb_multivchat_03.TabIndex = 4;
            this.pb_multivchat_03.TabStop = false;
            // 
            // pb_multivchat_04
            // 
            this.pb_multivchat_04.Location = new System.Drawing.Point(239, 186);
            this.pb_multivchat_04.Name = "pb_multivchat_04";
            this.pb_multivchat_04.Size = new System.Drawing.Size(179, 126);
            this.pb_multivchat_04.TabIndex = 5;
            this.pb_multivchat_04.TabStop = false;
            // 
            // gb_multichat_info
            // 
            this.gb_multichat_info.Controls.Add(this.rtb_multichat_info);
            this.gb_multichat_info.Location = new System.Drawing.Point(507, 10);
            this.gb_multichat_info.Name = "gb_multichat_info";
            this.gb_multichat_info.Size = new System.Drawing.Size(200, 169);
            this.gb_multichat_info.TabIndex = 8;
            this.gb_multichat_info.TabStop = false;
            this.gb_multichat_info.Text = "消息记录";
            // 
            // rtb_multichat_info
            // 
            this.rtb_multichat_info.Location = new System.Drawing.Point(0, 20);
            this.rtb_multichat_info.Name = "rtb_multichat_info";
            this.rtb_multichat_info.Size = new System.Drawing.Size(200, 144);
            this.rtb_multichat_info.TabIndex = 0;
            this.rtb_multichat_info.Text = "";
            // 
            // gb_video
            // 
            this.gb_video.Controls.Add(this.pb_multivchat_01);
            this.gb_video.Controls.Add(this.pb_multivchat_02);
            this.gb_video.Controls.Add(this.pb_multivchat_04);
            this.gb_video.Controls.Add(this.pb_multivchat_03);
            this.gb_video.Location = new System.Drawing.Point(23, 10);
            this.gb_video.Name = "gb_video";
            this.gb_video.Size = new System.Drawing.Size(424, 327);
            this.gb_video.TabIndex = 9;
            this.gb_video.TabStop = false;
            this.gb_video.Text = "视频展示区域";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lv_members);
            this.groupBox1.Location = new System.Drawing.Point(507, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 141);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "会议成员（黑名单操作）";
            // 
            // lv_members
            // 
            this.lv_members.Location = new System.Drawing.Point(7, 21);
            this.lv_members.Name = "lv_members";
            this.lv_members.Size = new System.Drawing.Size(193, 114);
            this.lv_members.TabIndex = 0;
            this.lv_members.UseCompatibleStateImageBehavior = false;
            this.lv_members.View = System.Windows.Forms.View.List;
            this.lv_members.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_members_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "房间号：";
            // 
            // tb_roomid
            // 
            this.tb_roomid.Location = new System.Drawing.Point(91, 369);
            this.tb_roomid.Name = "tb_roomid";
            this.tb_roomid.Size = new System.Drawing.Size(356, 21);
            this.tb_roomid.TabIndex = 12;
            // 
            // MultiVChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 409);
            this.Controls.Add(this.tb_roomid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_video);
            this.Controls.Add(this.gb_multichat_info);
            this.Controls.Add(this.btn_joinmultiroom);
            this.Name = "MultiVChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多人音视频";
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_03)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_multivchat_04)).EndInit();
            this.gb_multichat_info.ResumeLayout(false);
            this.gb_video.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

      
		#endregion

        private System.Windows.Forms.Button btn_joinmultiroom;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pb_multivchat_01;
        private System.Windows.Forms.PictureBox pb_multivchat_02;
        private System.Windows.Forms.PictureBox pb_multivchat_03;
        private System.Windows.Forms.PictureBox pb_multivchat_04;
        private System.Windows.Forms.GroupBox gb_multichat_info;
        private System.Windows.Forms.RichTextBox rtb_multichat_info;
        private System.Windows.Forms.GroupBox gb_video;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lv_members;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_roomid;
	}
}