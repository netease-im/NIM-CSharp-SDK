namespace NIMDemo.Audio
{
    partial class AudioForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.filePathTxt = new System.Windows.Forms.TextBox();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.stopPlayBtn = new System.Windows.Forms.Button();
            this.getDevicesBtn = new System.Windows.Forms.Button();
            this.captureBtn = new System.Windows.Forms.Button();
            this.stopCaptureBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "音频文件";
            // 
            // filePathTxt
            // 
            this.filePathTxt.Location = new System.Drawing.Point(97, 70);
            this.filePathTxt.Name = "filePathTxt";
            this.filePathTxt.Size = new System.Drawing.Size(321, 21);
            this.filePathTxt.TabIndex = 1;
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(443, 69);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 2;
            this.selectFileBtn.Text = "选择";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Location = new System.Drawing.Point(25, 117);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(75, 23);
            this.playBtn.TabIndex = 3;
            this.playBtn.Text = "播放";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // stopPlayBtn
            // 
            this.stopPlayBtn.Location = new System.Drawing.Point(25, 161);
            this.stopPlayBtn.Name = "stopPlayBtn";
            this.stopPlayBtn.Size = new System.Drawing.Size(75, 23);
            this.stopPlayBtn.TabIndex = 4;
            this.stopPlayBtn.Text = "停止";
            this.stopPlayBtn.UseVisualStyleBackColor = true;
            this.stopPlayBtn.Click += new System.EventHandler(this.stopPlayBtn_Click);
            // 
            // getDevicesBtn
            // 
            this.getDevicesBtn.Location = new System.Drawing.Point(158, 117);
            this.getDevicesBtn.Name = "getDevicesBtn";
            this.getDevicesBtn.Size = new System.Drawing.Size(96, 23);
            this.getDevicesBtn.TabIndex = 5;
            this.getDevicesBtn.Text = "获取播放设备";
            this.getDevicesBtn.UseVisualStyleBackColor = true;
            this.getDevicesBtn.Click += new System.EventHandler(this.getDevicesBtn_Click);
            // 
            // captureBtn
            // 
            this.captureBtn.Location = new System.Drawing.Point(158, 161);
            this.captureBtn.Name = "captureBtn";
            this.captureBtn.Size = new System.Drawing.Size(75, 23);
            this.captureBtn.TabIndex = 6;
            this.captureBtn.Text = "录制";
            this.captureBtn.UseVisualStyleBackColor = true;
            this.captureBtn.Click += new System.EventHandler(this.captureBtn_Click);
            // 
            // stopCaptureBtn
            // 
            this.stopCaptureBtn.Location = new System.Drawing.Point(271, 160);
            this.stopCaptureBtn.Name = "stopCaptureBtn";
            this.stopCaptureBtn.Size = new System.Drawing.Size(75, 23);
            this.stopCaptureBtn.TabIndex = 7;
            this.stopCaptureBtn.Text = "停止录制";
            this.stopCaptureBtn.UseVisualStyleBackColor = true;
            this.stopCaptureBtn.Click += new System.EventHandler(this.stopCaptureBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 213);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(506, 277);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "播放和录制音频默认都是aac格式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "音频格式";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "aac",
            "amr"});
            this.comboBox1.Location = new System.Drawing.Point(227, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // AudioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 528);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.stopCaptureBtn);
            this.Controls.Add(this.captureBtn);
            this.Controls.Add(this.getDevicesBtn);
            this.Controls.Add(this.stopPlayBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.filePathTxt);
            this.Controls.Add(this.label1);
            this.Name = "AudioForm";
            this.Text = "语音";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePathTxt;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button stopPlayBtn;
        private System.Windows.Forms.Button getDevicesBtn;
        private System.Windows.Forms.Button captureBtn;
        private System.Windows.Forms.Button stopCaptureBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}