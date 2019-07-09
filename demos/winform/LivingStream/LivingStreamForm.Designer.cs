namespace NIMDemo
{
	partial class LivingStreamForm
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
            this.btn_ls = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rt_push_url = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb_livingstream = new System.Windows.Forms.PictureBox();
            this.btn = new System.Windows.Forms.Button();
            this.btn_bypass = new System.Windows.Forms.Button();
            this.btn_beauty = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_livingstream)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ls
            // 
            this.btn_ls.Location = new System.Drawing.Point(437, 311);
            this.btn_ls.Name = "btn_ls";
            this.btn_ls.Size = new System.Drawing.Size(119, 23);
            this.btn_ls.TabIndex = 0;
            this.btn_ls.Text = "开始直播";
            this.btn_ls.UseVisualStyleBackColor = true;
            this.btn_ls.Click += new System.EventHandler(this.btn_ls_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "推流地址:";
            // 
            // rt_push_url
            // 
            this.rt_push_url.Location = new System.Drawing.Point(71, 313);
            this.rt_push_url.Name = "rt_push_url";
            this.rt_push_url.Size = new System.Drawing.Size(336, 23);
            this.rt_push_url.TabIndex = 2;
            this.rt_push_url.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pb_livingstream);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 278);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频展示区域";
            // 
            // pb_livingstream
            // 
            this.pb_livingstream.Location = new System.Drawing.Point(7, 21);
            this.pb_livingstream.Name = "pb_livingstream";
            this.pb_livingstream.Size = new System.Drawing.Size(377, 241);
            this.pb_livingstream.TabIndex = 0;
            this.pb_livingstream.TabStop = false;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(437, 240);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(119, 23);
            this.btn.TabIndex = 4;
            this.btn.Text = "音视频设置";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_bypass
            // 
            this.btn_bypass.Location = new System.Drawing.Point(437, 200);
            this.btn_bypass.Name = "btn_bypass";
            this.btn_bypass.Size = new System.Drawing.Size(119, 23);
            this.btn_bypass.TabIndex = 5;
            this.btn_bypass.Text = "麦序接口测试";
            this.btn_bypass.UseVisualStyleBackColor = true;
            this.btn_bypass.Click += new System.EventHandler(this.btn_bypass_Click);
            // 
            // btn_beauty
            // 
            this.btn_beauty.Location = new System.Drawing.Point(437, 277);
            this.btn_beauty.Name = "btn_beauty";
            this.btn_beauty.Size = new System.Drawing.Size(119, 23);
            this.btn_beauty.TabIndex = 6;
            this.btn_beauty.Text = "美颜（开）";
            this.btn_beauty.UseVisualStyleBackColor = true;
            this.btn_beauty.Click += new System.EventHandler(this.btn_beauty_Click);
            // 
            // LivingStreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 348);
            this.Controls.Add(this.btn_beauty);
            this.Controls.Add(this.btn_bypass);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rt_push_url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ls);
            this.Name = "LivingStreamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "直播相关";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_livingstream)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_ls;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rt_push_url;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn;
		private System.Windows.Forms.Button btn_bypass;
		private System.Windows.Forms.PictureBox pb_livingstream;
        private System.Windows.Forms.Button btn_beauty;
	}
}