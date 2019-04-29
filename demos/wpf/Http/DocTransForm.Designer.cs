namespace NIMDemo.Http
{
    partial class DocTransForm
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
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.submitFileBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.transIDtxtbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件";
            // 
            // filePathBox
            // 
            this.filePathBox.Location = new System.Drawing.Point(60, 32);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.Size = new System.Drawing.Size(266, 21);
            this.filePathBox.TabIndex = 1;
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(344, 30);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 2;
            this.selectFileBtn.Text = "选择文件";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // submitFileBtn
            // 
            this.submitFileBtn.Location = new System.Drawing.Point(13, 80);
            this.submitFileBtn.Name = "submitFileBtn";
            this.submitFileBtn.Size = new System.Drawing.Size(75, 23);
            this.submitFileBtn.TabIndex = 3;
            this.submitFileBtn.Text = "上传";
            this.submitFileBtn.UseVisualStyleBackColor = true;
            this.submitFileBtn.Click += new System.EventHandler(this.submitFileBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 128);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(404, 89);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // transIDtxtbox
            // 
            this.transIDtxtbox.Location = new System.Drawing.Point(60, 240);
            this.transIDtxtbox.Name = "transIDtxtbox";
            this.transIDtxtbox.Size = new System.Drawing.Size(359, 21);
            this.transIDtxtbox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 314);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "查询文档信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(133, 314);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "查询文档列表";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "url";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 277);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(359, 21);
            this.textBox1.TabIndex = 10;
            // 
            // DocTransForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 531);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.transIDtxtbox);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.submitFileBtn);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.filePathBox);
            this.Controls.Add(this.label1);
            this.Name = "DocTransForm";
            this.Text = "DocTransForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Button submitFileBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox transIDtxtbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}