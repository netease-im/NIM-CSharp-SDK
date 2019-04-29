namespace NIMDemo.Http
{
    partial class HttpForm
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
            this.uploadedFilePathTxt = new System.Windows.Forms.TextBox();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.uploadSpeedLabel = new System.Windows.Forms.Label();
            this.uploadPrgLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.totalSizeLabel = new System.Windows.Forms.Label();
            this.uploadRetLabel = new System.Windows.Forms.Label();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.avgSpeedLabel = new System.Windows.Forms.Label();
            this.actualSizeLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.downloadUrlTxt = new System.Windows.Forms.TextBox();
            this.downloadBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.downloadResultLabel = new System.Windows.Forms.Label();
            this.downloadPrgLabel12 = new System.Windows.Forms.Label();
            this.downloadPrgLabel = new System.Windows.Forms.Label();
            this.downloadSpeedLabel12 = new System.Windows.Forms.Label();
            this.downloadSpeedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件";
            // 
            // uploadedFilePathTxt
            // 
            this.uploadedFilePathTxt.Location = new System.Drawing.Point(57, 12);
            this.uploadedFilePathTxt.Name = "uploadedFilePathTxt";
            this.uploadedFilePathTxt.Size = new System.Drawing.Size(369, 21);
            this.uploadedFilePathTxt.TabIndex = 1;
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(440, 10);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(75, 23);
            this.selectFileBtn.TabIndex = 2;
            this.selectFileBtn.Text = "选择";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "上传速度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "进度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "结果";
            // 
            // uploadSpeedLabel
            // 
            this.uploadSpeedLabel.AutoSize = true;
            this.uploadSpeedLabel.Location = new System.Drawing.Point(87, 50);
            this.uploadSpeedLabel.Name = "uploadSpeedLabel";
            this.uploadSpeedLabel.Size = new System.Drawing.Size(41, 12);
            this.uploadSpeedLabel.TabIndex = 6;
            this.uploadSpeedLabel.Text = "label5";
            // 
            // uploadPrgLabel
            // 
            this.uploadPrgLabel.AutoSize = true;
            this.uploadPrgLabel.Location = new System.Drawing.Point(89, 79);
            this.uploadPrgLabel.Name = "uploadPrgLabel";
            this.uploadPrgLabel.Size = new System.Drawing.Size(41, 12);
            this.uploadPrgLabel.TabIndex = 7;
            this.uploadPrgLabel.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "/";
            // 
            // totalSizeLabel
            // 
            this.totalSizeLabel.AutoSize = true;
            this.totalSizeLabel.Location = new System.Drawing.Point(183, 80);
            this.totalSizeLabel.Name = "totalSizeLabel";
            this.totalSizeLabel.Size = new System.Drawing.Size(41, 12);
            this.totalSizeLabel.TabIndex = 9;
            this.totalSizeLabel.Text = "label8";
            // 
            // uploadRetLabel
            // 
            this.uploadRetLabel.AutoSize = true;
            this.uploadRetLabel.Location = new System.Drawing.Point(89, 108);
            this.uploadRetLabel.Name = "uploadRetLabel";
            this.uploadRetLabel.Size = new System.Drawing.Size(41, 12);
            this.uploadRetLabel.TabIndex = 10;
            this.uploadRetLabel.Text = "label9";
            // 
            // uploadBtn
            // 
            this.uploadBtn.Location = new System.Drawing.Point(14, 212);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(75, 23);
            this.uploadBtn.TabIndex = 11;
            this.uploadBtn.Text = "上传nos";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "平均速度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "总上传大小";
            // 
            // avgSpeedLabel
            // 
            this.avgSpeedLabel.AutoSize = true;
            this.avgSpeedLabel.Location = new System.Drawing.Point(89, 131);
            this.avgSpeedLabel.Name = "avgSpeedLabel";
            this.avgSpeedLabel.Size = new System.Drawing.Size(41, 12);
            this.avgSpeedLabel.TabIndex = 14;
            this.avgSpeedLabel.Text = "label8";
            // 
            // actualSizeLabel
            // 
            this.actualSizeLabel.AutoSize = true;
            this.actualSizeLabel.Location = new System.Drawing.Point(90, 156);
            this.actualSizeLabel.Name = "actualSizeLabel";
            this.actualSizeLabel.Size = new System.Drawing.Size(41, 12);
            this.actualSizeLabel.TabIndex = 15;
            this.actualSizeLabel.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "Url";
            // 
            // downloadUrlTxt
            // 
            this.downloadUrlTxt.Location = new System.Drawing.Point(89, 177);
            this.downloadUrlTxt.Name = "downloadUrlTxt";
            this.downloadUrlTxt.Size = new System.Drawing.Size(426, 21);
            this.downloadUrlTxt.TabIndex = 17;
            // 
            // downloadBtn
            // 
            this.downloadBtn.Enabled = false;
            this.downloadBtn.Location = new System.Drawing.Point(149, 212);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(75, 23);
            this.downloadBtn.TabIndex = 18;
            this.downloadBtn.Text = "下载";
            this.downloadBtn.UseVisualStyleBackColor = true;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 264);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "下载结果";
            // 
            // downloadResultLabel
            // 
            this.downloadResultLabel.AutoSize = true;
            this.downloadResultLabel.Location = new System.Drawing.Point(91, 263);
            this.downloadResultLabel.Name = "downloadResultLabel";
            this.downloadResultLabel.Size = new System.Drawing.Size(47, 12);
            this.downloadResultLabel.TabIndex = 20;
            this.downloadResultLabel.Text = "label10";
            // 
            // downloadPrgLabel12
            // 
            this.downloadPrgLabel12.AutoSize = true;
            this.downloadPrgLabel12.Location = new System.Drawing.Point(15, 296);
            this.downloadPrgLabel12.Name = "downloadPrgLabel12";
            this.downloadPrgLabel12.Size = new System.Drawing.Size(53, 12);
            this.downloadPrgLabel12.TabIndex = 21;
            this.downloadPrgLabel12.Text = "下载进度";
            // 
            // downloadPrgLabel
            // 
            this.downloadPrgLabel.AutoSize = true;
            this.downloadPrgLabel.Location = new System.Drawing.Point(91, 296);
            this.downloadPrgLabel.Name = "downloadPrgLabel";
            this.downloadPrgLabel.Size = new System.Drawing.Size(47, 12);
            this.downloadPrgLabel.TabIndex = 22;
            this.downloadPrgLabel.Text = "label10";
            // 
            // downloadSpeedLabel12
            // 
            this.downloadSpeedLabel12.AutoSize = true;
            this.downloadSpeedLabel12.Location = new System.Drawing.Point(15, 328);
            this.downloadSpeedLabel12.Name = "downloadSpeedLabel12";
            this.downloadSpeedLabel12.Size = new System.Drawing.Size(47, 12);
            this.downloadSpeedLabel12.TabIndex = 23;
            this.downloadSpeedLabel12.Text = "label10";
            // 
            // downloadSpeedLabel
            // 
            this.downloadSpeedLabel.AutoSize = true;
            this.downloadSpeedLabel.Location = new System.Drawing.Point(91, 327);
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            this.downloadSpeedLabel.Size = new System.Drawing.Size(47, 12);
            this.downloadSpeedLabel.TabIndex = 24;
            this.downloadSpeedLabel.Text = "label10";
            // 
            // HttpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 581);
            this.Controls.Add(this.downloadSpeedLabel);
            this.Controls.Add(this.downloadSpeedLabel12);
            this.Controls.Add(this.downloadPrgLabel);
            this.Controls.Add(this.downloadPrgLabel12);
            this.Controls.Add(this.downloadResultLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.downloadBtn);
            this.Controls.Add(this.downloadUrlTxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.actualSizeLabel);
            this.Controls.Add(this.avgSpeedLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.uploadRetLabel);
            this.Controls.Add(this.totalSizeLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.uploadPrgLabel);
            this.Controls.Add(this.uploadSpeedLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.uploadedFilePathTxt);
            this.Controls.Add(this.label1);
            this.Name = "HttpForm";
            this.Text = "HttpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uploadedFilePathTxt;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label uploadSpeedLabel;
        private System.Windows.Forms.Label uploadPrgLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label totalSizeLabel;
        private System.Windows.Forms.Label uploadRetLabel;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label avgSpeedLabel;
        private System.Windows.Forms.Label actualSizeLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox downloadUrlTxt;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label downloadResultLabel;
        private System.Windows.Forms.Label downloadPrgLabel12;
        private System.Windows.Forms.Label downloadPrgLabel;
        private System.Windows.Forms.Label downloadSpeedLabel12;
        private System.Windows.Forms.Label downloadSpeedLabel;
    }
}