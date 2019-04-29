namespace NIMDemo
{
    partial class ChatForm
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
            this.recallMsgBtn = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_joinmultiroom = new System.Windows.Forms.Button();
            this.btn_createmultiroom = new System.Windows.Forms.Button();
            this.SysmsgLogBtn = new System.Windows.Forms.Button();
            this.SysMsgBtn = new System.Windows.Forms.Button();
            this.QueryMsglogBtn = new System.Windows.Forms.Button();
            this.testRtsBtn = new System.Windows.Forms.Button();
            this.testMediaBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.recallMsgBtn);
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_joinmultiroom);
            this.splitContainer1.Panel2.Controls.Add(this.btn_createmultiroom);
            this.splitContainer1.Panel2.Controls.Add(this.SysmsgLogBtn);
            this.splitContainer1.Panel2.Controls.Add(this.SysMsgBtn);
            this.splitContainer1.Panel2.Controls.Add(this.QueryMsglogBtn);
            this.splitContainer1.Panel2.Controls.Add(this.testRtsBtn);
            this.splitContainer1.Panel2.Controls.Add(this.testMediaBtn);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(501, 460);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // recallMsgBtn
            // 
            this.recallMsgBtn.Location = new System.Drawing.Point(392, 187);
            this.recallMsgBtn.Name = "recallMsgBtn";
            this.recallMsgBtn.Size = new System.Drawing.Size(97, 23);
            this.recallMsgBtn.TabIndex = 2;
            this.recallMsgBtn.Text = "撤回";
            this.recallMsgBtn.UseVisualStyleBackColor = true;
            this.recallMsgBtn.Click += new System.EventHandler(this.recallMsgBtn_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(14, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(475, 160);
            this.listBox1.TabIndex = 0;
            // 
            // btn_joinmultiroom
            // 
            this.btn_joinmultiroom.Location = new System.Drawing.Point(289, 197);
            this.btn_joinmultiroom.Name = "btn_joinmultiroom";
            this.btn_joinmultiroom.Size = new System.Drawing.Size(87, 23);
            this.btn_joinmultiroom.TabIndex = 10;
            this.btn_joinmultiroom.Text = "加入多人会议";
            this.btn_joinmultiroom.UseVisualStyleBackColor = true;
            this.btn_joinmultiroom.Click += new System.EventHandler(this.btn_joinmultiroom_Click);
            // 
            // btn_createmultiroom
            // 
            this.btn_createmultiroom.AutoSize = true;
            this.btn_createmultiroom.Location = new System.Drawing.Point(289, 166);
            this.btn_createmultiroom.Name = "btn_createmultiroom";
            this.btn_createmultiroom.Size = new System.Drawing.Size(87, 25);
            this.btn_createmultiroom.TabIndex = 9;
            this.btn_createmultiroom.Text = "创建多人会议";
            this.btn_createmultiroom.UseVisualStyleBackColor = true;
            this.btn_createmultiroom.Click += new System.EventHandler(this.btn_createmultiroom_Click);
            // 
            // SysmsgLogBtn
            // 
            this.SysmsgLogBtn.Location = new System.Drawing.Point(93, 197);
            this.SysmsgLogBtn.Name = "SysmsgLogBtn";
            this.SysmsgLogBtn.Size = new System.Drawing.Size(84, 23);
            this.SysmsgLogBtn.TabIndex = 8;
            this.SysmsgLogBtn.Text = "系统消息记录";
            this.SysmsgLogBtn.UseVisualStyleBackColor = true;
            this.SysmsgLogBtn.Click += new System.EventHandler(this.SysmsgLogBtn_Click);
            // 
            // SysMsgBtn
            // 
            this.SysMsgBtn.Location = new System.Drawing.Point(183, 168);
            this.SysMsgBtn.Name = "SysMsgBtn";
            this.SysMsgBtn.Size = new System.Drawing.Size(97, 23);
            this.SysMsgBtn.TabIndex = 7;
            this.SysMsgBtn.Text = "发送自定义通知";
            this.SysMsgBtn.UseVisualStyleBackColor = true;
            this.SysMsgBtn.Click += new System.EventHandler(this.SysMsgBtn_Click);
            // 
            // QueryMsglogBtn
            // 
            this.QueryMsglogBtn.Location = new System.Drawing.Point(12, 197);
            this.QueryMsglogBtn.Name = "QueryMsglogBtn";
            this.QueryMsglogBtn.Size = new System.Drawing.Size(75, 23);
            this.QueryMsglogBtn.TabIndex = 6;
            this.QueryMsglogBtn.Text = "消息记录";
            this.QueryMsglogBtn.UseVisualStyleBackColor = true;
            this.QueryMsglogBtn.Click += new System.EventHandler(this.QueryMsglogBtn_Click);
            // 
            // testRtsBtn
            // 
            this.testRtsBtn.Location = new System.Drawing.Point(93, 168);
            this.testRtsBtn.Name = "testRtsBtn";
            this.testRtsBtn.Size = new System.Drawing.Size(84, 23);
            this.testRtsBtn.TabIndex = 5;
            this.testRtsBtn.Text = "测试Rts";
            this.testRtsBtn.UseVisualStyleBackColor = true;
            this.testRtsBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // testMediaBtn
            // 
            this.testMediaBtn.Location = new System.Drawing.Point(12, 168);
            this.testMediaBtn.Name = "testMediaBtn";
            this.testMediaBtn.Size = new System.Drawing.Size(75, 23);
            this.testMediaBtn.TabIndex = 4;
            this.testMediaBtn.Text = "测试音视频";
            this.testMediaBtn.UseVisualStyleBackColor = true;
            this.testMediaBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SendMessageToFriend);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(12, 24);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(477, 128);
            this.textBox1.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 460);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button testMediaBtn;
        private System.Windows.Forms.Button testRtsBtn;
        private System.Windows.Forms.Button QueryMsglogBtn;
        private System.Windows.Forms.Button SysMsgBtn;
        private System.Windows.Forms.Button SysmsgLogBtn;
        private System.Windows.Forms.Button btn_joinmultiroom;
        private System.Windows.Forms.Button btn_createmultiroom;
        private System.Windows.Forms.Button recallMsgBtn;
    }
}