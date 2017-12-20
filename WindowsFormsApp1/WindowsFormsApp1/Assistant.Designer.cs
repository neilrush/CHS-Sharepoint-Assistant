namespace WindowsFormsApp1
{
    partial class assistant
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
            this.uxRun = new System.Windows.Forms.Button();
            this.uxLoadFolder = new System.Windows.Forms.Button();
            this.uxConsole = new System.Windows.Forms.TextBox();
            this.uxOpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uxPath = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.uxFileLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.LoadFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.uxRemoveLast = new System.Windows.Forms.ToolStripMenuItem();
            this.uxClear = new System.Windows.Forms.ToolStripMenuItem();
            this.Tools = new System.Windows.Forms.ToolStripDropDownButton();
            this.uxAddDates = new System.Windows.Forms.ToolStripMenuItem();
            this.uxReformatDates = new System.Windows.Forms.ToolStripMenuItem();
            this.uxFolderList = new System.Windows.Forms.ListBox();
            this.uxShowConsole = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxRun
            // 
            this.uxRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxRun.Location = new System.Drawing.Point(490, 111);
            this.uxRun.Name = "uxRun";
            this.uxRun.Size = new System.Drawing.Size(75, 23);
            this.uxRun.TabIndex = 1;
            this.uxRun.Text = "Run";
            this.uxRun.UseVisualStyleBackColor = true;
            this.uxRun.Click += new System.EventHandler(this.uxRun_Click);
            // 
            // uxLoadFolder
            // 
            this.uxLoadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uxLoadFolder.Location = new System.Drawing.Point(286, 111);
            this.uxLoadFolder.Name = "uxLoadFolder";
            this.uxLoadFolder.Size = new System.Drawing.Size(75, 23);
            this.uxLoadFolder.TabIndex = 2;
            this.uxLoadFolder.Text = "Load Folder";
            this.uxLoadFolder.UseVisualStyleBackColor = true;
            this.uxLoadFolder.Click += new System.EventHandler(this.uxLoadFolder_Click);
            // 
            // uxConsole
            // 
            this.uxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.uxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uxConsole.ForeColor = System.Drawing.SystemColors.Window;
            this.uxConsole.Location = new System.Drawing.Point(12, 168);
            this.uxConsole.Multiline = true;
            this.uxConsole.Name = "uxConsole";
            this.uxConsole.ReadOnly = true;
            this.uxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uxConsole.Size = new System.Drawing.Size(553, 0);
            this.uxConsole.TabIndex = 0;
            this.uxConsole.Text = "uxConsole";
            this.uxConsole.Visible = false;
            // 
            // uxPath
            // 
            this.uxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxPath.Location = new System.Drawing.Point(12, 111);
            this.uxPath.Name = "uxPath";
            this.uxPath.Size = new System.Drawing.Size(268, 20);
            this.uxPath.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxFileLabel,
            this.Tools});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(577, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // uxFileLabel
            // 
            this.uxFileLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadFolder,
            this.uxRemoveLast,
            this.uxClear});
            this.uxFileLabel.Name = "uxFileLabel";
            this.uxFileLabel.Size = new System.Drawing.Size(38, 22);
            this.uxFileLabel.Text = "File";
            // 
            // LoadFolder
            // 
            this.LoadFolder.Name = "LoadFolder";
            this.LoadFolder.Size = new System.Drawing.Size(177, 22);
            this.LoadFolder.Text = "Load Folder";
            this.LoadFolder.Click += new System.EventHandler(this.LoadFolder_Click);
            // 
            // uxRemoveLast
            // 
            this.uxRemoveLast.Name = "uxRemoveLast";
            this.uxRemoveLast.Size = new System.Drawing.Size(177, 22);
            this.uxRemoveLast.Text = "Remove Last Folder";
            this.uxRemoveLast.Click += new System.EventHandler(this.uxRemoveLast_Click);
            // 
            // uxClear
            // 
            this.uxClear.Name = "uxClear";
            this.uxClear.Size = new System.Drawing.Size(177, 22);
            this.uxClear.Text = "Clear Folders";
            this.uxClear.Click += new System.EventHandler(this.uxClear_Click);
            // 
            // Tools
            // 
            this.Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxAddDates,
            this.uxReformatDates});
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(49, 22);
            this.Tools.Text = "Tools";
            // 
            // uxAddDates
            // 
            this.uxAddDates.CheckOnClick = true;
            this.uxAddDates.Name = "uxAddDates";
            this.uxAddDates.Size = new System.Drawing.Size(155, 22);
            this.uxAddDates.Text = "Add Dates";
            // 
            // uxReformatDates
            // 
            this.uxReformatDates.CheckOnClick = true;
            this.uxReformatDates.Name = "uxReformatDates";
            this.uxReformatDates.Size = new System.Drawing.Size(155, 22);
            this.uxReformatDates.Text = "Reformat Dates";
            // 
            // uxFolderList
            // 
            this.uxFolderList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxFolderList.FormattingEnabled = true;
            this.uxFolderList.Location = new System.Drawing.Point(13, 28);
            this.uxFolderList.Name = "uxFolderList";
            this.uxFolderList.Size = new System.Drawing.Size(553, 69);
            this.uxFolderList.TabIndex = 8;
            // 
            // uxShowConsole
            // 
            this.uxShowConsole.Location = new System.Drawing.Point(13, 139);
            this.uxShowConsole.Name = "uxShowConsole";
            this.uxShowConsole.Size = new System.Drawing.Size(95, 23);
            this.uxShowConsole.TabIndex = 9;
            this.uxShowConsole.Text = "Show Console";
            this.uxShowConsole.UseVisualStyleBackColor = true;
            this.uxShowConsole.Click += new System.EventHandler(this.uxShowConsole_Click);
            // 
            // assistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(577, 170);
            this.Controls.Add(this.uxShowConsole);
            this.Controls.Add(this.uxFolderList);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.uxPath);
            this.Controls.Add(this.uxLoadFolder);
            this.Controls.Add(this.uxRun);
            this.Controls.Add(this.uxConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(593, 209);
            this.Name = "assistant";
            this.ShowIcon = false;
            this.Text = "SharePoint Assistant";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button uxRun;
        private System.Windows.Forms.Button uxLoadFolder;
        private System.Windows.Forms.TextBox uxConsole;
        private System.Windows.Forms.FolderBrowserDialog uxOpenFolderDialog;
        private System.Windows.Forms.TextBox uxPath;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton uxFileLabel;
        private System.Windows.Forms.ToolStripMenuItem LoadFolder;
        private System.Windows.Forms.ToolStripMenuItem uxRemoveLast;
        private System.Windows.Forms.ToolStripMenuItem uxClear;
        private System.Windows.Forms.ListBox uxFolderList;
        private System.Windows.Forms.ToolStripDropDownButton Tools;
        private System.Windows.Forms.ToolStripMenuItem uxAddDates;
        private System.Windows.Forms.ToolStripMenuItem uxReformatDates;
        private System.Windows.Forms.Button uxShowConsole;
    }
}

