namespace NIMDemo.AVChat
{
    partial class AVDevicesSettingForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cb_camera = new System.Windows.Forms.ComboBox();
            this.cb_microphone = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_audiooutdevice = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_audioin = new System.Windows.Forms.TrackBar();
            this.tb_audioout = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tb_audioin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_audioout)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "摄像头:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "麦克风:";
            // 
            // cb_camera
            // 
            this.cb_camera.FormattingEnabled = true;
            this.cb_camera.Location = new System.Drawing.Point(77, 65);
            this.cb_camera.Name = "cb_camera";
            this.cb_camera.Size = new System.Drawing.Size(156, 20);
            this.cb_camera.TabIndex = 2;
            this.cb_camera.SelectedIndexChanged += new System.EventHandler(this.cb_camera_SelectedIndexChanged);
            // 
            // cb_microphone
            // 
            this.cb_microphone.FormattingEnabled = true;
            this.cb_microphone.Location = new System.Drawing.Point(77, 125);
            this.cb_microphone.Name = "cb_microphone";
            this.cb_microphone.Size = new System.Drawing.Size(156, 20);
            this.cb_microphone.TabIndex = 3;
            this.cb_microphone.SelectedIndexChanged += new System.EventHandler(this.cb_microphone_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "播放器:";
            // 
            // cb_audiooutdevice
            // 
            this.cb_audiooutdevice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_audiooutdevice.FormattingEnabled = true;
            this.cb_audiooutdevice.Location = new System.Drawing.Point(77, 185);
            this.cb_audiooutdevice.Name = "cb_audiooutdevice";
            this.cb_audiooutdevice.Size = new System.Drawing.Size(160, 20);
            this.cb_audiooutdevice.TabIndex = 7;
            this.cb_audiooutdevice.SelectedIndexChanged += new System.EventHandler(this.cb_audiooutdevice_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "音量:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "音量:";
            // 
            // tb_audioin
            // 
            this.tb_audioin.LargeChange = 20;
            this.tb_audioin.Location = new System.Drawing.Point(360, 125);
            this.tb_audioin.Maximum = 255;
            this.tb_audioin.Name = "tb_audioin";
            this.tb_audioin.Size = new System.Drawing.Size(130, 45);
            this.tb_audioin.TabIndex = 10;
            this.tb_audioin.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_audioin.ValueChanged += new System.EventHandler(this.tb_audioin_ValueChanged);
            // 
            // tb_audioout
            // 
            this.tb_audioout.LargeChange = 20;
            this.tb_audioout.Location = new System.Drawing.Point(360, 192);
            this.tb_audioout.Maximum = 255;
            this.tb_audioout.Name = "tb_audioout";
            this.tb_audioout.Size = new System.Drawing.Size(130, 45);
            this.tb_audioout.TabIndex = 11;
            this.tb_audioout.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_audioout.ValueChanged += new System.EventHandler(this.tb_audioout_ValueChanged);
            // 
            // AVDevicesSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 240);
            this.Controls.Add(this.tb_audioout);
            this.Controls.Add(this.tb_audioin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_audiooutdevice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_microphone);
            this.Controls.Add(this.cb_camera);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AVDevicesSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音视频设置";
            ((System.ComponentModel.ISupportInitialize)(this.tb_audioin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_audioout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_camera;
        private System.Windows.Forms.ComboBox cb_microphone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_audiooutdevice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tb_audioin;
        private System.Windows.Forms.TrackBar tb_audioout;
    }
}