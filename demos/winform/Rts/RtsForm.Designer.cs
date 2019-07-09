namespace NIMDemo
{
    partial class RtsForm
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
            if (disposing)
            {
                _peerDrawingGraphics.Dispose();
                _myDrawingGraphics.Dispose();
                _myPen.Dispose();
                _peerPen.Dispose();
                _sendDataTimer.Dispose();
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.promptLabel = new System.Windows.Forms.Label();
            this.audioCtrlBtn = new System.Windows.Forms.Button();
            this.panel_rts_working = new System.Windows.Forms.Panel();
            this.panel_rts_invite_or_notify = new System.Windows.Forms.Panel();
            this.btn_rts_cancel = new System.Windows.Forms.Button();
            this.lb_rts_prompt = new System.Windows.Forms.Label();
            this.lb_rts_member = new System.Windows.Forms.Label();
            this.btn_rts_refuse = new System.Windows.Forms.Button();
            this.btn_rts_accept = new System.Windows.Forms.Button();
            this.panel_rts_working.SuspendLayout();
            this.panel_rts_invite_or_notify.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(269, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "上一步";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(184, 463);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "全部清空";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(48, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 401);
            this.panel1.TabIndex = 1;
            // 
            // promptLabel
            // 
            this.promptLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.promptLabel.Location = new System.Drawing.Point(0, 0);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new System.Drawing.Size(490, 30);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "开始会话";
            this.promptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // audioCtrlBtn
            // 
            this.audioCtrlBtn.Location = new System.Drawing.Point(89, 463);
            this.audioCtrlBtn.Name = "audioCtrlBtn";
            this.audioCtrlBtn.Size = new System.Drawing.Size(80, 23);
            this.audioCtrlBtn.TabIndex = 0;
            this.audioCtrlBtn.Tag = 0;
            this.audioCtrlBtn.Text = "开启麦克风";
            this.audioCtrlBtn.UseVisualStyleBackColor = true;
            // 
            // panel_rts_working
            // 
            this.panel_rts_working.Controls.Add(this.panel1);
            this.panel_rts_working.Controls.Add(this.audioCtrlBtn);
            this.panel_rts_working.Controls.Add(this.button1);
            this.panel_rts_working.Controls.Add(this.promptLabel);
            this.panel_rts_working.Controls.Add(this.button2);
            this.panel_rts_working.Location = new System.Drawing.Point(12, 12);
            this.panel_rts_working.Name = "panel_rts_working";
            this.panel_rts_working.Size = new System.Drawing.Size(490, 494);
            this.panel_rts_working.TabIndex = 2;
            // 
            // panel_rts_invite_or_notify
            // 
            this.panel_rts_invite_or_notify.Controls.Add(this.btn_rts_cancel);
            this.panel_rts_invite_or_notify.Controls.Add(this.lb_rts_prompt);
            this.panel_rts_invite_or_notify.Controls.Add(this.lb_rts_member);
            this.panel_rts_invite_or_notify.Controls.Add(this.btn_rts_refuse);
            this.panel_rts_invite_or_notify.Controls.Add(this.btn_rts_accept);
            this.panel_rts_invite_or_notify.Location = new System.Drawing.Point(12, 12);
            this.panel_rts_invite_or_notify.Name = "panel_rts_invite_or_notify";
            this.panel_rts_invite_or_notify.Size = new System.Drawing.Size(490, 494);
            this.panel_rts_invite_or_notify.TabIndex = 3;
            // 
            // btn_rts_cancel
            // 
            this.btn_rts_cancel.Location = new System.Drawing.Point(209, 357);
            this.btn_rts_cancel.Name = "btn_rts_cancel";
            this.btn_rts_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_rts_cancel.TabIndex = 4;
            this.btn_rts_cancel.Text = "取消";
            this.btn_rts_cancel.UseVisualStyleBackColor = true;
            this.btn_rts_cancel.Click += new System.EventHandler(this.BtnRtsCancelClick);
            // 
            // lb_rts_prompt
            // 
            this.lb_rts_prompt.AutoSize = true;
            this.lb_rts_prompt.Location = new System.Drawing.Point(207, 230);
            this.lb_rts_prompt.Name = "lb_rts_prompt";
            this.lb_rts_prompt.Size = new System.Drawing.Size(41, 12);
            this.lb_rts_prompt.TabIndex = 3;
            this.lb_rts_prompt.Text = "label2";
            // 
            // lb_rts_member
            // 
            this.lb_rts_member.AutoSize = true;
            this.lb_rts_member.Location = new System.Drawing.Point(207, 146);
            this.lb_rts_member.Name = "lb_rts_member";
            this.lb_rts_member.Size = new System.Drawing.Size(41, 12);
            this.lb_rts_member.TabIndex = 2;
            this.lb_rts_member.Text = "label1";
            // 
            // btn_rts_refuse
            // 
            this.btn_rts_refuse.Location = new System.Drawing.Point(304, 357);
            this.btn_rts_refuse.Name = "btn_rts_refuse";
            this.btn_rts_refuse.Size = new System.Drawing.Size(75, 23);
            this.btn_rts_refuse.TabIndex = 1;
            this.btn_rts_refuse.Text = "拒绝";
            this.btn_rts_refuse.UseVisualStyleBackColor = true;
            this.btn_rts_refuse.Click += new System.EventHandler(this.BtnRtsRefuseClick);
            // 
            // btn_rts_accept
            // 
            this.btn_rts_accept.Location = new System.Drawing.Point(114, 357);
            this.btn_rts_accept.Name = "btn_rts_accept";
            this.btn_rts_accept.Size = new System.Drawing.Size(75, 23);
            this.btn_rts_accept.TabIndex = 0;
            this.btn_rts_accept.Text = "接受";
            this.btn_rts_accept.UseVisualStyleBackColor = true;
            this.btn_rts_accept.Click += new System.EventHandler(this.BtnRtsAcceptClick);
            // 
            // RtsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 510);
            this.Controls.Add(this.panel_rts_invite_or_notify);
            this.Controls.Add(this.panel_rts_working);
            this.Name = "RtsForm";
            this.Text = "白板演示";
            this.panel_rts_working.ResumeLayout(false);
            this.panel_rts_invite_or_notify.ResumeLayout(false);
            this.panel_rts_invite_or_notify.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label promptLabel;
        private System.Windows.Forms.Button audioCtrlBtn;
        private System.Windows.Forms.Panel panel_rts_working;
        private System.Windows.Forms.Panel panel_rts_invite_or_notify;
        private System.Windows.Forms.Label lb_rts_prompt;
        private System.Windows.Forms.Label lb_rts_member;
        private System.Windows.Forms.Button btn_rts_refuse;
        private System.Windows.Forms.Button btn_rts_accept;
        private System.Windows.Forms.Button btn_rts_cancel;
    }
}