namespace NIMDemo
{
    partial class MessageExportForm
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
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(41, 31);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(114, 23);
            this.BtnExport.TabIndex = 0;
            this.BtnExport.Text = "导出消息到云端";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(269, 31);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(126, 23);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "从云端导入消息";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // MessageExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 225);
            this.Controls.Add(this.BtnImport);
            this.Controls.Add(this.BtnExport);
            this.Name = "MessageExportForm";
            this.Text = "MessageExport";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnImport;
    }
}