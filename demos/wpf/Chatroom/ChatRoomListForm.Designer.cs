namespace NIMDemo
{
    partial class ChatRoomListForm
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.membersListview = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.receivedmsgListbox = new System.Windows.Forms.ListBox();
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
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.membersListview);
            this.splitContainer1.Panel2.Controls.Add(this.receivedmsgListbox);
            this.splitContainer1.Size = new System.Drawing.Size(574, 506);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(265, 506);
            this.treeView1.TabIndex = 0;
            // 
            // membersListview
            // 
            this.membersListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.membersListview.Location = new System.Drawing.Point(5, 251);
            this.membersListview.Name = "membersListview";
            this.membersListview.Size = new System.Drawing.Size(297, 252);
            this.membersListview.TabIndex = 3;
            this.membersListview.UseCompatibleStateImageBehavior = false;
            this.membersListview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "聊天室成员";
            this.columnHeader1.Width = 285;
            // 
            // receivedmsgListbox
            // 
            this.receivedmsgListbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.receivedmsgListbox.FormattingEnabled = true;
            this.receivedmsgListbox.HorizontalScrollbar = true;
            this.receivedmsgListbox.ItemHeight = 12;
            this.receivedmsgListbox.Location = new System.Drawing.Point(0, 0);
            this.receivedmsgListbox.Name = "receivedmsgListbox";
            this.receivedmsgListbox.Size = new System.Drawing.Size(305, 232);
            this.receivedmsgListbox.TabIndex = 0;
            // 
            // ChatRoomListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 506);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ChatRoomListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "聊天室列表";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListBox receivedmsgListbox;
        private System.Windows.Forms.ListView membersListview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}