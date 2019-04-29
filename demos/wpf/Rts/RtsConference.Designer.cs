namespace NIMDemo.Rts
{
    partial class RtsConference
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
            this.createConfBtn = new System.Windows.Forms.Button();
            this.joinConfBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.sendDataBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sessionIdTxt = new System.Windows.Forms.TextBox();
            this.contentTxt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // createConfBtn
            // 
            this.createConfBtn.Location = new System.Drawing.Point(2, 98);
            this.createConfBtn.Name = "createConfBtn";
            this.createConfBtn.Size = new System.Drawing.Size(75, 23);
            this.createConfBtn.TabIndex = 0;
            this.createConfBtn.Text = "创建";
            this.createConfBtn.UseVisualStyleBackColor = true;
            this.createConfBtn.Click += new System.EventHandler(this.createConfBtn_Click);
            // 
            // joinConfBtn
            // 
            this.joinConfBtn.Location = new System.Drawing.Point(2, 143);
            this.joinConfBtn.Name = "joinConfBtn";
            this.joinConfBtn.Size = new System.Drawing.Size(75, 23);
            this.joinConfBtn.TabIndex = 1;
            this.joinConfBtn.Text = "加入";
            this.joinConfBtn.UseVisualStyleBackColor = true;
            this.joinConfBtn.Click += new System.EventHandler(this.joinConfBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(2, 331);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(395, 204);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 21);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "房间名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "附加信息";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 50);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(216, 21);
            this.textBox2.TabIndex = 6;
            // 
            // sendDataBtn
            // 
            this.sendDataBtn.Location = new System.Drawing.Point(313, 290);
            this.sendDataBtn.Name = "sendDataBtn";
            this.sendDataBtn.Size = new System.Drawing.Size(75, 23);
            this.sendDataBtn.TabIndex = 7;
            this.sendDataBtn.Text = "发送数据";
            this.sendDataBtn.UseVisualStyleBackColor = true;
            this.sendDataBtn.Click += new System.EventHandler(this.sendDataBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "发送内容：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "SessionID";
            // 
            // sessionIdTxt
            // 
            this.sessionIdTxt.Location = new System.Drawing.Point(181, 144);
            this.sessionIdTxt.Name = "sessionIdTxt";
            this.sessionIdTxt.Size = new System.Drawing.Size(207, 21);
            this.sessionIdTxt.TabIndex = 11;
            // 
            // contentTxt
            // 
            this.contentTxt.Location = new System.Drawing.Point(15, 213);
            this.contentTxt.Name = "contentTxt";
            this.contentTxt.Size = new System.Drawing.Size(373, 71);
            this.contentTxt.TabIndex = 12;
            this.contentTxt.Text = "";
            // 
            // RtsConference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 547);
            this.Controls.Add(this.contentTxt);
            this.Controls.Add(this.sessionIdTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sendDataBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.joinConfBtn);
            this.Controls.Add(this.createConfBtn);
            this.Name = "RtsConference";
            this.Text = "多人白板";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createConfBtn;
        private System.Windows.Forms.Button joinConfBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button sendDataBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sessionIdTxt;
        private System.Windows.Forms.RichTextBox contentTxt;
    }
}