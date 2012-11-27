using IntelligentLevelEditor.Games.Pushmo;

namespace IntelligentLevelEditor
{
    partial class FormEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeRead = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuQRCodeMake = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeMakeCard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpCheckNow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwCheckForUpdates = new System.ComponentModel.BackgroundWorker();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuQRCode,
            this.menuHelp,
            this.testToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(744, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.menuFileSep0,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(36, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Image = global::IntelligentLevelEditor.Properties.Resources.ico_page_white;
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuFileNew.Size = new System.Drawing.Size(178, 22);
            this.menuFileNew.Text = "&New...";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Image = global::IntelligentLevelEditor.Properties.Resources.ico_folder;
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpen.Size = new System.Drawing.Size(178, 22);
            this.menuFileOpen.Text = "&Open bin...";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Enabled = false;
            this.menuFileSave.Image = global::IntelligentLevelEditor.Properties.Resources.ico_disk;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSave.Size = new System.Drawing.Size(178, 22);
            this.menuFileSave.Text = "&Save bin";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Enabled = false;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.Size = new System.Drawing.Size(178, 22);
            this.menuFileSaveAs.Text = "Save bin &As...";
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // menuFileSep0
            // 
            this.menuFileSep0.Name = "menuFileSep0";
            this.menuFileSep0.Size = new System.Drawing.Size(175, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Image = global::IntelligentLevelEditor.Properties.Resources.ico_door_in;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuFileExit.Size = new System.Drawing.Size(178, 22);
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuQRCode
            // 
            this.menuQRCode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuQRCodeRead,
            this.menuQRCodeCapture,
            this.menuQRCodeSep0,
            this.menuQRCodeMake,
            this.menuQRCodeMakeCard});
            this.menuQRCode.Name = "menuQRCode";
            this.menuQRCode.Size = new System.Drawing.Size(67, 20);
            this.menuQRCode.Text = "&QR Code";
            // 
            // menuQRCodeRead
            // 
            this.menuQRCodeRead.Image = global::IntelligentLevelEditor.Properties.Resources.ico_folder_picture;
            this.menuQRCodeRead.Name = "menuQRCodeRead";
            this.menuQRCodeRead.Size = new System.Drawing.Size(210, 22);
            this.menuQRCodeRead.Text = "&Read from image...";
            this.menuQRCodeRead.Click += new System.EventHandler(this.menuQRCodeRead_Click);
            // 
            // menuQRCodeCapture
            // 
            this.menuQRCodeCapture.Name = "menuQRCodeCapture";
            this.menuQRCodeCapture.Size = new System.Drawing.Size(210, 22);
            this.menuQRCodeCapture.Text = "&Capture from Webcam...";
            this.menuQRCodeCapture.Click += new System.EventHandler(this.menuQRCodeCapture_Click);
            // 
            // menuQRCodeSep0
            // 
            this.menuQRCodeSep0.Name = "menuQRCodeSep0";
            this.menuQRCodeSep0.Size = new System.Drawing.Size(207, 6);
            // 
            // menuQRCodeMake
            // 
            this.menuQRCodeMake.Enabled = false;
            this.menuQRCodeMake.Image = global::IntelligentLevelEditor.Properties.Resources.ico_barcode2d;
            this.menuQRCodeMake.Name = "menuQRCodeMake";
            this.menuQRCodeMake.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuQRCodeMake.Size = new System.Drawing.Size(210, 22);
            this.menuQRCodeMake.Text = "Make &QR Code...";
            this.menuQRCodeMake.Click += new System.EventHandler(this.menuQRCodeMake_Click);
            // 
            // menuQRCodeMakeCard
            // 
            this.menuQRCodeMakeCard.Enabled = false;
            this.menuQRCodeMakeCard.Image = global::IntelligentLevelEditor.Properties.Resources.ico_layout_content;
            this.menuQRCodeMakeCard.Name = "menuQRCodeMakeCard";
            this.menuQRCodeMakeCard.Size = new System.Drawing.Size(210, 22);
            this.menuQRCodeMakeCard.Text = "&Make a QR Card...";
            this.menuQRCodeMakeCard.Click += new System.EventHandler(this.menuQRCodeMakeCard_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpCheckUpdates,
            this.menuHelpCheckNow,
            this.menuHelpSep0,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(43, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpCheckUpdates
            // 
            this.menuHelpCheckUpdates.CheckOnClick = true;
            this.menuHelpCheckUpdates.Name = "menuHelpCheckUpdates";
            this.menuHelpCheckUpdates.Size = new System.Drawing.Size(235, 22);
            this.menuHelpCheckUpdates.Text = "Check for updates on &startup";
            this.menuHelpCheckUpdates.Click += new System.EventHandler(this.menuHelpCheckUpdates_Click);
            // 
            // menuHelpCheckNow
            // 
            this.menuHelpCheckNow.Image = global::IntelligentLevelEditor.Properties.Resources.ico_magnifier;
            this.menuHelpCheckNow.Name = "menuHelpCheckNow";
            this.menuHelpCheckNow.Size = new System.Drawing.Size(235, 22);
            this.menuHelpCheckNow.Text = "&Check for updates now...";
            this.menuHelpCheckNow.Click += new System.EventHandler(this.menuHelpCheckNow_Click);
            // 
            // menuHelpSep0
            // 
            this.menuHelpSep0.Name = "menuHelpSep0";
            this.menuHelpSep0.Size = new System.Drawing.Size(232, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(235, 22);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripColor,
            this.stripPosition});
            this.statusStrip.Location = new System.Drawing.Point(0, 480);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(744, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // stripColor
            // 
            this.stripColor.Name = "stripColor";
            this.stripColor.Size = new System.Drawing.Size(0, 17);
            // 
            // stripPosition
            // 
            this.stripPosition.Name = "stripPosition";
            this.stripPosition.Size = new System.Drawing.Size(729, 17);
            this.stripPosition.Spring = true;
            this.stripPosition.Text = "Position: (0,0)";
            this.stripPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bwCheckForUpdates
            // 
            this.bwCheckForUpdates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckForUpdates_DoWork);
            this.bwCheckForUpdates.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCheckForUpdates_RunWorkerCompleted);
            // 
            // pnlEditor
            // 
            this.pnlEditor.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(0, 24);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(744, 456);
            this.pnlEditor.TabIndex = 4;
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 502);
            this.Controls.Add(this.pnlEditor);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(760, 540);
            this.Name = "FormEditor";
            this.Text = "Intelligent Level Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator menuFileSep0;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuQRCode;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeRead;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripPosition;
        private System.Windows.Forms.ToolStripStatusLabel stripColor;
        private System.Windows.Forms.ToolStripSeparator menuQRCodeSep0;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeMakeCard;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeMake;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.ComponentModel.BackgroundWorker bwCheckForUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuHelpCheckUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuHelpCheckNow;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep0;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeCapture;

    }
}

