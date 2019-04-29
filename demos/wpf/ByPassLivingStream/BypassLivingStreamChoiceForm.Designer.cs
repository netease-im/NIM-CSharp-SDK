namespace NIMDemo
{
	partial class BypassLivingStreamChoiceForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_joinroom = new System.Windows.Forms.Button();
			this.tb_joinroomid = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rb_audio = new System.Windows.Forms.RadioButton();
			this.rb_video = new System.Windows.Forms.RadioButton();
			this.btn_choice_start_living = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_joinroom);
			this.groupBox1.Controls.Add(this.tb_joinroomid);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(24, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 83);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "观众";
			// 
			// btn_joinroom
			// 
			this.btn_joinroom.Location = new System.Drawing.Point(215, 30);
			this.btn_joinroom.Name = "btn_joinroom";
			this.btn_joinroom.Size = new System.Drawing.Size(75, 23);
			this.btn_joinroom.TabIndex = 2;
			this.btn_joinroom.Text = "观众测试";
			this.btn_joinroom.UseVisualStyleBackColor = true;
			this.btn_joinroom.Click += new System.EventHandler(this.btn_joinroom_Click);
			// 
			// tb_joinroomid
			// 
			this.tb_joinroomid.Location = new System.Drawing.Point(69, 30);
			this.tb_joinroomid.Name = "tb_joinroomid";
			this.tb_joinroomid.Size = new System.Drawing.Size(122, 21);
			this.tb_joinroomid.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "房间Id";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.btn_choice_start_living);
			this.groupBox2.Location = new System.Drawing.Point(24, 102);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(296, 100);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "主播";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rb_audio);
			this.groupBox3.Controls.Add(this.rb_video);
			this.groupBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.groupBox3.Location = new System.Drawing.Point(24, 32);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(167, 53);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			// 
			// rb_audio
			// 
			this.rb_audio.AutoSize = true;
			this.rb_audio.Location = new System.Drawing.Point(84, 21);
			this.rb_audio.Name = "rb_audio";
			this.rb_audio.Size = new System.Drawing.Size(71, 16);
			this.rb_audio.TabIndex = 1;
			this.rb_audio.TabStop = true;
			this.rb_audio.Text = "音频直播";
			this.rb_audio.UseVisualStyleBackColor = true;
			// 
			// rb_video
			// 
			this.rb_video.AutoSize = true;
			this.rb_video.Checked = true;
			this.rb_video.Location = new System.Drawing.Point(7, 21);
			this.rb_video.Name = "rb_video";
			this.rb_video.Size = new System.Drawing.Size(71, 16);
			this.rb_video.TabIndex = 0;
			this.rb_video.TabStop = true;
			this.rb_video.Text = "视频直播";
			this.rb_video.UseVisualStyleBackColor = true;
			// 
			// btn_choice_start_living
			// 
			this.btn_choice_start_living.Location = new System.Drawing.Point(215, 51);
			this.btn_choice_start_living.Name = "btn_choice_start_living";
			this.btn_choice_start_living.Size = new System.Drawing.Size(75, 23);
			this.btn_choice_start_living.TabIndex = 3;
			this.btn_choice_start_living.Text = "主播测试";
			this.btn_choice_start_living.UseVisualStyleBackColor = true;
			this.btn_choice_start_living.Click += new System.EventHandler(this.btn_choice_start_living_Click);
			// 
			// BypassLivingStreamChoiceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(335, 226);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "BypassLivingStreamChoiceForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "身份选择";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tb_joinroomid;
		private System.Windows.Forms.Button btn_joinroom;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton rb_audio;
		private System.Windows.Forms.RadioButton rb_video;
		private System.Windows.Forms.Button btn_choice_start_living;
	}
}