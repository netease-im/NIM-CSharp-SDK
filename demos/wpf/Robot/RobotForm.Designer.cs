namespace NIMDemo
{
    partial class RobotForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sendjumpbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.targettextBox = new System.Windows.Forms.TextBox();
            this.paramstextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.robotlistView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.sendjumpbutton);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.targettextBox);
            this.splitContainer1.Panel1.Controls.Add(this.paramstextBox);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.robotlistView);
            this.splitContainer1.Size = new System.Drawing.Size(431, 525);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "text";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(245, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 21);
            this.textBox1.TabIndex = 6;
            // 
            // sendjumpbutton
            // 
            this.sendjumpbutton.Location = new System.Drawing.Point(323, 106);
            this.sendjumpbutton.Name = "sendjumpbutton";
            this.sendjumpbutton.Size = new System.Drawing.Size(96, 23);
            this.sendjumpbutton.TabIndex = 5;
            this.sendjumpbutton.Text = "发送跳转消息";
            this.sendjumpbutton.UseVisualStyleBackColor = true;
            this.sendjumpbutton.Click += new System.EventHandler(this.sendjumpbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "target";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "params";
            // 
            // targettextBox
            // 
            this.targettextBox.Location = new System.Drawing.Point(245, 42);
            this.targettextBox.Name = "targettextBox";
            this.targettextBox.Size = new System.Drawing.Size(174, 21);
            this.targettextBox.TabIndex = 2;
            // 
            // paramstextBox
            // 
            this.paramstextBox.Location = new System.Drawing.Point(245, 15);
            this.paramstextBox.Name = "paramstextBox";
            this.paramstextBox.Size = new System.Drawing.Size(174, 21);
            this.paramstextBox.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "获取所有机器人";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // robotlistView
            // 
            this.robotlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.robotlistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.robotlistView.Location = new System.Drawing.Point(0, 0);
            this.robotlistView.Name = "robotlistView";
            this.robotlistView.Size = new System.Drawing.Size(431, 376);
            this.robotlistView.TabIndex = 0;
            this.robotlistView.UseCompatibleStateImageBehavior = false;
            this.robotlistView.View = System.Windows.Forms.View.Details;
            this.robotlistView.SelectedIndexChanged += new System.EventHandler(this.robotlistView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "RID";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Accid";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 180;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(28, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "查询全部机器人";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RobotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 525);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RobotForm";
            this.Text = "机器人";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView robotlistView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button sendjumpbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox targettextBox;
        private System.Windows.Forms.TextBox paramstextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}