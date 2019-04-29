namespace NIMDemo.Team
{
    partial class CreateAdvancedTeamForm
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
            this.teamMemberBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.teamNameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.teamIntroBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.joinModeCombox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.neddAgreeCombox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.inviteModeCombox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.modifyModeCombox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.modifyPropertyModeCombox = new System.Windows.Forms.ComboBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.teamIconSelectBtn = new System.Windows.Forms.Button();
            this.teamIconPathBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "群成员：";
            // 
            // teamMemberBox
            // 
            this.teamMemberBox.Location = new System.Drawing.Point(89, 12);
            this.teamMemberBox.Multiline = true;
            this.teamMemberBox.Name = "teamMemberBox";
            this.teamMemberBox.Size = new System.Drawing.Size(230, 82);
            this.teamMemberBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "(以 , 分隔)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "群头像(本地路径)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "群名称";
            // 
            // teamNameBox
            // 
            this.teamNameBox.Location = new System.Drawing.Point(89, 161);
            this.teamNameBox.Name = "teamNameBox";
            this.teamNameBox.Size = new System.Drawing.Size(230, 21);
            this.teamNameBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "群介绍";
            // 
            // teamIntroBox
            // 
            this.teamIntroBox.Location = new System.Drawing.Point(89, 195);
            this.teamIntroBox.Name = "teamIntroBox";
            this.teamIntroBox.Size = new System.Drawing.Size(230, 21);
            this.teamIntroBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "入群身份验证";
            // 
            // joinModeCombox
            // 
            this.joinModeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.joinModeCombox.FormattingEnabled = true;
            this.joinModeCombox.Items.AddRange(new object[] {
            "允许任何人",
            "需要验证消息",
            "不允许任何人申请加入"});
            this.joinModeCombox.Location = new System.Drawing.Point(143, 230);
            this.joinModeCombox.Name = "joinModeCombox";
            this.joinModeCombox.Size = new System.Drawing.Size(176, 20);
            this.joinModeCombox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "是否需要被邀请人同意";
            // 
            // neddAgreeCombox
            // 
            this.neddAgreeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neddAgreeCombox.FormattingEnabled = true;
            this.neddAgreeCombox.Items.AddRange(new object[] {
            "yes",
            "no"});
            this.neddAgreeCombox.Location = new System.Drawing.Point(198, 261);
            this.neddAgreeCombox.Name = "neddAgreeCombox";
            this.neddAgreeCombox.Size = new System.Drawing.Size(121, 20);
            this.neddAgreeCombox.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 299);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "邀请他人权限";
            // 
            // inviteModeCombox
            // 
            this.inviteModeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inviteModeCombox.FormattingEnabled = true;
            this.inviteModeCombox.Items.AddRange(new object[] {
            "管理员",
            "所有人"});
            this.inviteModeCombox.Location = new System.Drawing.Point(170, 290);
            this.inviteModeCombox.Name = "inviteModeCombox";
            this.inviteModeCombox.Size = new System.Drawing.Size(149, 20);
            this.inviteModeCombox.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "群资料修改权限";
            // 
            // modifyModeCombox
            // 
            this.modifyModeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modifyModeCombox.FormattingEnabled = true;
            this.modifyModeCombox.Items.AddRange(new object[] {
            "管理员",
            "所有人"});
            this.modifyModeCombox.Location = new System.Drawing.Point(170, 323);
            this.modifyModeCombox.Name = "modifyModeCombox";
            this.modifyModeCombox.Size = new System.Drawing.Size(149, 20);
            this.modifyModeCombox.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "群自定义属性修改权限";
            // 
            // modifyPropertyModeCombox
            // 
            this.modifyPropertyModeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modifyPropertyModeCombox.FormattingEnabled = true;
            this.modifyPropertyModeCombox.Items.AddRange(new object[] {
            "管理员",
            "所有人"});
            this.modifyPropertyModeCombox.Location = new System.Drawing.Point(170, 351);
            this.modifyPropertyModeCombox.Name = "modifyPropertyModeCombox";
            this.modifyPropertyModeCombox.Size = new System.Drawing.Size(149, 20);
            this.modifyPropertyModeCombox.TabIndex = 17;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(213, 441);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 18;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(74, 441);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 19;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // teamIconSelectBtn
            // 
            this.teamIconSelectBtn.Location = new System.Drawing.Point(130, 106);
            this.teamIconSelectBtn.Name = "teamIconSelectBtn";
            this.teamIconSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.teamIconSelectBtn.TabIndex = 22;
            this.teamIconSelectBtn.Text = "选择";
            this.teamIconSelectBtn.UseVisualStyleBackColor = true;
            this.teamIconSelectBtn.Click += new System.EventHandler(this.teamIconSelectBtn_Click);
            // 
            // teamIconPathBox
            // 
            this.teamIconPathBox.Location = new System.Drawing.Point(12, 134);
            this.teamIconPathBox.Name = "teamIconPathBox";
            this.teamIconPathBox.Size = new System.Drawing.Size(307, 21);
            this.teamIconPathBox.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 391);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "附言";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(170, 388);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 21);
            this.textBox1.TabIndex = 21;
            // 
            // CreateAdvancedTeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 486);
            this.Controls.Add(this.teamIconPathBox);
            this.Controls.Add(this.teamIconSelectBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.modifyPropertyModeCombox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.modifyModeCombox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.inviteModeCombox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.neddAgreeCombox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.joinModeCombox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.teamIntroBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.teamNameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.teamMemberBox);
            this.Controls.Add(this.label1);
            this.Name = "CreateAdvancedTeamForm";
            this.Text = "创建高级群";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox teamMemberBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox teamNameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox teamIntroBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox joinModeCombox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox neddAgreeCombox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox inviteModeCombox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox modifyModeCombox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox modifyPropertyModeCombox;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button teamIconSelectBtn;
        private System.Windows.Forms.TextBox teamIconPathBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
    }
}