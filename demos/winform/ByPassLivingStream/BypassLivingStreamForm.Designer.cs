namespace NIMDemo
{
	partial class BypassLivingStreamForm
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rt_pull_url = new System.Windows.Forms.RichTextBox();
			this.btn_audio_interact = new System.Windows.Forms.Button();
			this.btn_video_interact = new System.Windows.Forms.Button();
			this.btn_query_queue = new System.Windows.Forms.Button();
			this.btn_clear_queue = new System.Windows.Forms.Button();
			this.btn_poll_element = new System.Windows.Forms.Button();
			this.btn_addorupdate_queue = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.rt_prompt_info = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tb_key_ = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_value_ = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tb_opt_key = new System.Windows.Forms.TextBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rt_pull_url);
			this.groupBox2.Location = new System.Drawing.Point(520, 25);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(225, 74);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "拉流地址";
			// 
			// rt_pull_url
			// 
			this.rt_pull_url.Location = new System.Drawing.Point(7, 21);
			this.rt_pull_url.Name = "rt_pull_url";
			this.rt_pull_url.ReadOnly = true;
			this.rt_pull_url.Size = new System.Drawing.Size(212, 47);
			this.rt_pull_url.TabIndex = 0;
			this.rt_pull_url.Text = "";
			// 
			// btn_audio_interact
			// 
			this.btn_audio_interact.Location = new System.Drawing.Point(12, 282);
			this.btn_audio_interact.Name = "btn_audio_interact";
			this.btn_audio_interact.Size = new System.Drawing.Size(103, 23);
			this.btn_audio_interact.TabIndex = 4;
			this.btn_audio_interact.Text = "音频互动";
			this.btn_audio_interact.UseVisualStyleBackColor = true;
			this.btn_audio_interact.Click += new System.EventHandler(this.btn_audio_interact_Click);
			// 
			// btn_video_interact
			// 
			this.btn_video_interact.Location = new System.Drawing.Point(12, 240);
			this.btn_video_interact.Name = "btn_video_interact";
			this.btn_video_interact.Size = new System.Drawing.Size(103, 23);
			this.btn_video_interact.TabIndex = 5;
			this.btn_video_interact.Text = "视频互动";
			this.btn_video_interact.UseVisualStyleBackColor = true;
			this.btn_video_interact.Click += new System.EventHandler(this.btn_video_interact_Click);
			// 
			// btn_query_queue
			// 
			this.btn_query_queue.Location = new System.Drawing.Point(12, 34);
			this.btn_query_queue.Name = "btn_query_queue";
			this.btn_query_queue.Size = new System.Drawing.Size(114, 23);
			this.btn_query_queue.TabIndex = 6;
			this.btn_query_queue.Text = "查询麦序信息";
			this.btn_query_queue.UseVisualStyleBackColor = true;
			this.btn_query_queue.Click += new System.EventHandler(this.btn_query_queue_Click);
			// 
			// btn_clear_queue
			// 
			this.btn_clear_queue.Location = new System.Drawing.Point(12, 82);
			this.btn_clear_queue.Name = "btn_clear_queue";
			this.btn_clear_queue.Size = new System.Drawing.Size(114, 23);
			this.btn_clear_queue.TabIndex = 7;
			this.btn_clear_queue.Text = "清除麦序信息";
			this.btn_clear_queue.UseVisualStyleBackColor = true;
			this.btn_clear_queue.Click += new System.EventHandler(this.btn_clear_queue_Click);
			// 
			// btn_poll_element
			// 
			this.btn_poll_element.Location = new System.Drawing.Point(12, 124);
			this.btn_poll_element.Name = "btn_poll_element";
			this.btn_poll_element.Size = new System.Drawing.Size(114, 23);
			this.btn_poll_element.TabIndex = 8;
			this.btn_poll_element.Text = "取出麦序元素";
			this.btn_poll_element.UseVisualStyleBackColor = true;
			this.btn_poll_element.Click += new System.EventHandler(this.btn_poll_element_Click);
			// 
			// btn_addorupdate_queue
			// 
			this.btn_addorupdate_queue.Location = new System.Drawing.Point(12, 172);
			this.btn_addorupdate_queue.Name = "btn_addorupdate_queue";
			this.btn_addorupdate_queue.Size = new System.Drawing.Size(114, 23);
			this.btn_addorupdate_queue.TabIndex = 9;
			this.btn_addorupdate_queue.Text = "添加或者更新元素";
			this.btn_addorupdate_queue.UseVisualStyleBackColor = true;
			this.btn_addorupdate_queue.Click += new System.EventHandler(this.btn_addorupdate_queue_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(526, 106);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "提示信息";
			// 
			// rt_prompt_info
			// 
			this.rt_prompt_info.Location = new System.Drawing.Point(527, 130);
			this.rt_prompt_info.Name = "rt_prompt_info";
			this.rt_prompt_info.ReadOnly = true;
			this.rt_prompt_info.Size = new System.Drawing.Size(218, 194);
			this.rt_prompt_info.TabIndex = 12;
			this.rt_prompt_info.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(158, 177);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 13;
			this.label2.Text = "key:";
			// 
			// tb_key_
			// 
			this.tb_key_.Location = new System.Drawing.Point(187, 174);
			this.tb_key_.Name = "tb_key_";
			this.tb_key_.Size = new System.Drawing.Size(100, 21);
			this.tb_key_.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(293, 177);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 15;
			this.label3.Text = "value:";
			// 
			// tb_value_
			// 
			this.tb_value_.Location = new System.Drawing.Point(334, 172);
			this.tb_value_.Name = "tb_value_";
			this.tb_value_.Size = new System.Drawing.Size(100, 21);
			this.tb_value_.TabIndex = 16;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(158, 129);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 17;
			this.label4.Text = "key:";
			// 
			// tb_opt_key
			// 
			this.tb_opt_key.Location = new System.Drawing.Point(194, 124);
			this.tb_opt_key.Name = "tb_opt_key";
			this.tb_opt_key.Size = new System.Drawing.Size(93, 21);
			this.tb_opt_key.TabIndex = 18;
			// 
			// BypassLivingStreamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(757, 376);
			this.Controls.Add(this.tb_opt_key);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tb_value_);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tb_key_);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.rt_prompt_info);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_addorupdate_queue);
			this.Controls.Add(this.btn_poll_element);
			this.Controls.Add(this.btn_clear_queue);
			this.Controls.Add(this.btn_query_queue);
			this.Controls.Add(this.btn_video_interact);
			this.Controls.Add(this.btn_audio_interact);
			this.Controls.Add(this.groupBox2);
			this.Name = "BypassLivingStreamForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BypassLivingStreamForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BypassLivingStreamForm_FormClosed);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btn_audio_interact;
		private System.Windows.Forms.Button btn_video_interact;
		private System.Windows.Forms.RichTextBox rt_pull_url;
		private System.Windows.Forms.Button btn_query_queue;
		private System.Windows.Forms.Button btn_clear_queue;
		private System.Windows.Forms.Button btn_poll_element;
		private System.Windows.Forms.Button btn_addorupdate_queue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rt_prompt_info;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tb_key_;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tb_value_;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tb_opt_key;
	}
}