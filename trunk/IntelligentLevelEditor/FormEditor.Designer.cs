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
            this.menuFileNewPushmo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNewCrashmo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuQRCode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpCheckNow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tbEditor = new System.Windows.Forms.ToolStrip();
            this.tbEditorSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblShift = new System.Windows.Forms.Label();
            this.grpThumb = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.lblToolMessage = new System.Windows.Forms.Label();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.lblSelectedColor = new System.Windows.Forms.Label();
            this.radColor9 = new System.Windows.Forms.RadioButton();
            this.radColor8 = new System.Windows.Forms.RadioButton();
            this.radColor7 = new System.Windows.Forms.RadioButton();
            this.radColor6 = new System.Windows.Forms.RadioButton();
            this.radColor5 = new System.Windows.Forms.RadioButton();
            this.radColor4 = new System.Windows.Forms.RadioButton();
            this.radColor3 = new System.Windows.Forms.RadioButton();
            this.radColor2 = new System.Windows.Forms.RadioButton();
            this.radColor1 = new System.Windows.Forms.RadioButton();
            this.radColor0 = new System.Windows.Forms.RadioButton();
            this.pnlSwitches = new System.Windows.Forms.Panel();
            this.radSwitch9 = new System.Windows.Forms.RadioButton();
            this.radSwitch8 = new System.Windows.Forms.RadioButton();
            this.radSwitch7 = new System.Windows.Forms.RadioButton();
            this.radSwitch6 = new System.Windows.Forms.RadioButton();
            this.radSwitch5 = new System.Windows.Forms.RadioButton();
            this.radSwitch4 = new System.Windows.Forms.RadioButton();
            this.radSwitch3 = new System.Windows.Forms.RadioButton();
            this.radSwitch2 = new System.Windows.Forms.RadioButton();
            this.radSwitch1 = new System.Windows.Forms.RadioButton();
            this.radSwitch0 = new System.Windows.Forms.RadioButton();
            this.pnlManholes = new System.Windows.Forms.Panel();
            this.chkManholeSelect = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwCheckForUpdates = new System.ComponentModel.BackgroundWorker();
            this.gridControl = new IntelligentLevelEditor.Games.Pushmo.PushmoGridControl();
            this.tbtnPencilTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnPipetteTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnFillTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnFlagTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnSwitchTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnManholeTool = new System.Windows.Forms.ToolStripButton();
            this.tbtnCloudTool = new System.Windows.Forms.ToolStripButton();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.btnShiftRight = new System.Windows.Forms.Button();
            this.btnShiftUp = new System.Windows.Forms.Button();
            this.btnShiftDown = new System.Windows.Forms.Button();
            this.btnShiftLeft = new System.Windows.Forms.Button();
            this.picShow = new System.Windows.Forms.PictureBox();
            this.radColorA = new System.Windows.Forms.RadioButton();
            this.btnEditPalette = new System.Windows.Forms.Button();
            this.btnDeleteSwitch = new System.Windows.Forms.Button();
            this.btnDeleteManhole = new System.Windows.Forms.Button();
            this.radManhole4 = new System.Windows.Forms.RadioButton();
            this.radManhole3 = new System.Windows.Forms.RadioButton();
            this.radManhole2 = new System.Windows.Forms.RadioButton();
            this.radManhole1 = new System.Windows.Forms.RadioButton();
            this.radManhole0 = new System.Windows.Forms.RadioButton();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeRead = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeMake = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQRCodeMakeCard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tbEditor.SuspendLayout();
            this.grpThumb.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.pnlColors.SuspendLayout();
            this.pnlSwitches.SuspendLayout();
            this.pnlManholes.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).BeginInit();
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
            this.menuStrip.Size = new System.Drawing.Size(714, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileNewPushmo,
            this.menuFileNewCrashmo,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.menuFileSep0,
            this.menuFileImport,
            this.menuFileSep1,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(36, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileNewPushmo
            // 
            this.menuFileNewPushmo.Name = "menuFileNewPushmo";
            this.menuFileNewPushmo.Size = new System.Drawing.Size(188, 22);
            this.menuFileNewPushmo.Text = "&New Pushmo";
            this.menuFileNewPushmo.Click += new System.EventHandler(this.menuFileNewPushmo_Click);
            // 
            // menuFileNewCrashmo
            // 
            this.menuFileNewCrashmo.Name = "menuFileNewCrashmo";
            this.menuFileNewCrashmo.Size = new System.Drawing.Size(188, 22);
            this.menuFileNewCrashmo.Text = "New &Crashmo";
            this.menuFileNewCrashmo.Click += new System.EventHandler(this.menuFileNewCrashmo_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.Size = new System.Drawing.Size(188, 22);
            this.menuFileSaveAs.Text = "Save bin &As...";
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // menuFileSep0
            // 
            this.menuFileSep0.Name = "menuFileSep0";
            this.menuFileSep0.Size = new System.Drawing.Size(185, 6);
            // 
            // menuFileImport
            // 
            this.menuFileImport.Name = "menuFileImport";
            this.menuFileImport.Size = new System.Drawing.Size(188, 22);
            this.menuFileImport.Text = "&Import from image...";
            this.menuFileImport.Click += new System.EventHandler(this.menuFileImport_Click);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(185, 6);
            // 
            // menuQRCode
            // 
            this.menuQRCode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuQRCodeRead,
            this.menuQRCodeSep0,
            this.menuQRCodeMake,
            this.menuQRCodeMakeCard});
            this.menuQRCode.Name = "menuQRCode";
            this.menuQRCode.Size = new System.Drawing.Size(67, 20);
            this.menuQRCode.Text = "&QR Code";
            // 
            // menuQRCodeSep0
            // 
            this.menuQRCodeSep0.Name = "menuQRCodeSep0";
            this.menuQRCodeSep0.Size = new System.Drawing.Size(205, 6);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpCheckUpdates,
            this.menuHelpCheckNow,
            this.menuHelpLanguage,
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
            this.menuHelpCheckNow.Name = "menuHelpCheckNow";
            this.menuHelpCheckNow.Size = new System.Drawing.Size(235, 22);
            this.menuHelpCheckNow.Text = "&Check for updates now...";
            this.menuHelpCheckNow.Click += new System.EventHandler(this.menuHelpCheckNow_Click);
            // 
            // menuHelpLanguage
            // 
            this.menuHelpLanguage.Name = "menuHelpLanguage";
            this.menuHelpLanguage.Size = new System.Drawing.Size(235, 22);
            this.menuHelpLanguage.Text = "Change &Language...";
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
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer.Panel1.Controls.Add(this.gridControl);
            this.splitContainer.Panel1.Controls.Add(this.tbEditor);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.chkGrid);
            this.splitContainer.Panel2.Controls.Add(this.lblShift);
            this.splitContainer.Panel2.Controls.Add(this.btnShiftRight);
            this.splitContainer.Panel2.Controls.Add(this.btnShiftUp);
            this.splitContainer.Panel2.Controls.Add(this.btnShiftDown);
            this.splitContainer.Panel2.Controls.Add(this.btnShiftLeft);
            this.splitContainer.Panel2.Controls.Add(this.grpThumb);
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer.Panel2.Controls.Add(this.grpOptions);
            this.splitContainer.Size = new System.Drawing.Size(714, 479);
            this.splitContainer.SplitterDistance = 488;
            this.splitContainer.TabIndex = 2;
            // 
            // tbEditor
            // 
            this.tbEditor.AutoSize = false;
            this.tbEditor.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbEditor.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tbEditor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tbEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnPencilTool,
            this.tbtnPipetteTool,
            this.tbtnFillTool,
            this.tbtnFlagTool,
            this.tbtnSwitchTool,
            this.tbtnManholeTool,
            this.tbtnCloudTool,
            this.tbEditorSep1});
            this.tbEditor.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tbEditor.Location = new System.Drawing.Point(0, 0);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.Size = new System.Drawing.Size(32, 479);
            this.tbEditor.TabIndex = 4;
            // 
            // tbEditorSep1
            // 
            this.tbEditorSep1.Name = "tbEditorSep1";
            this.tbEditorSep1.Size = new System.Drawing.Size(30, 6);
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(132, 87);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(62, 13);
            this.lblShift.TabIndex = 9;
            this.lblShift.Text = "Shift image:";
            // 
            // grpThumb
            // 
            this.grpThumb.Controls.Add(this.picShow);
            this.grpThumb.Location = new System.Drawing.Point(132, 4);
            this.grpThumb.Name = "grpThumb";
            this.grpThumb.Size = new System.Drawing.Size(74, 75);
            this.grpThumb.TabIndex = 7;
            this.grpThumb.TabStop = false;
            this.grpThumb.Text = "Thumbnail:";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid.Location = new System.Drawing.Point(3, 249);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(216, 205);
            this.propertyGrid.TabIndex = 3;
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.lblToolMessage);
            this.grpOptions.Controls.Add(this.pnlColors);
            this.grpOptions.Controls.Add(this.pnlSwitches);
            this.grpOptions.Controls.Add(this.pnlManholes);
            this.grpOptions.Location = new System.Drawing.Point(3, 4);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(115, 239);
            this.grpOptions.TabIndex = 6;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Tool Options:";
            // 
            // lblToolMessage
            // 
            this.lblToolMessage.AutoSize = true;
            this.lblToolMessage.Location = new System.Drawing.Point(12, 24);
            this.lblToolMessage.Name = "lblToolMessage";
            this.lblToolMessage.Size = new System.Drawing.Size(50, 13);
            this.lblToolMessage.TabIndex = 1;
            this.lblToolMessage.Text = "Message";
            this.lblToolMessage.Visible = false;
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.lblSelectedColor);
            this.pnlColors.Controls.Add(this.radColorA);
            this.pnlColors.Controls.Add(this.radColor9);
            this.pnlColors.Controls.Add(this.btnEditPalette);
            this.pnlColors.Controls.Add(this.radColor8);
            this.pnlColors.Controls.Add(this.radColor7);
            this.pnlColors.Controls.Add(this.radColor6);
            this.pnlColors.Controls.Add(this.radColor5);
            this.pnlColors.Controls.Add(this.radColor4);
            this.pnlColors.Controls.Add(this.radColor3);
            this.pnlColors.Controls.Add(this.radColor2);
            this.pnlColors.Controls.Add(this.radColor1);
            this.pnlColors.Controls.Add(this.radColor0);
            this.pnlColors.Location = new System.Drawing.Point(6, 16);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(70, 210);
            this.pnlColors.TabIndex = 0;
            // 
            // lblSelectedColor
            // 
            this.lblSelectedColor.BackColor = System.Drawing.Color.Black;
            this.lblSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSelectedColor.Location = new System.Drawing.Point(8, 3);
            this.lblSelectedColor.Name = "lblSelectedColor";
            this.lblSelectedColor.Size = new System.Drawing.Size(54, 24);
            this.lblSelectedColor.TabIndex = 4;
            // 
            // radColor9
            // 
            this.radColor9.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor9.BackColor = System.Drawing.Color.SeaGreen;
            this.radColor9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor9.Location = new System.Drawing.Point(38, 156);
            this.radColor9.Name = "radColor9";
            this.radColor9.Size = new System.Drawing.Size(24, 24);
            this.radColor9.TabIndex = 2;
            this.radColor9.Tag = "9";
            this.radColor9.UseVisualStyleBackColor = false;
            this.radColor9.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor8
            // 
            this.radColor8.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor8.BackColor = System.Drawing.Color.MediumPurple;
            this.radColor8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor8.Location = new System.Drawing.Point(8, 156);
            this.radColor8.Name = "radColor8";
            this.radColor8.Size = new System.Drawing.Size(24, 24);
            this.radColor8.TabIndex = 2;
            this.radColor8.Tag = "8";
            this.radColor8.UseVisualStyleBackColor = false;
            this.radColor8.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor7
            // 
            this.radColor7.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor7.BackColor = System.Drawing.Color.YellowGreen;
            this.radColor7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor7.Location = new System.Drawing.Point(38, 126);
            this.radColor7.Name = "radColor7";
            this.radColor7.Size = new System.Drawing.Size(24, 24);
            this.radColor7.TabIndex = 2;
            this.radColor7.Tag = "7";
            this.radColor7.UseVisualStyleBackColor = false;
            this.radColor7.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor6
            // 
            this.radColor6.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor6.BackColor = System.Drawing.Color.CornflowerBlue;
            this.radColor6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor6.Location = new System.Drawing.Point(8, 126);
            this.radColor6.Name = "radColor6";
            this.radColor6.Size = new System.Drawing.Size(24, 24);
            this.radColor6.TabIndex = 2;
            this.radColor6.Tag = "6";
            this.radColor6.UseVisualStyleBackColor = false;
            this.radColor6.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor5
            // 
            this.radColor5.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor5.BackColor = System.Drawing.Color.Gold;
            this.radColor5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor5.Location = new System.Drawing.Point(38, 96);
            this.radColor5.Name = "radColor5";
            this.radColor5.Size = new System.Drawing.Size(24, 24);
            this.radColor5.TabIndex = 2;
            this.radColor5.Tag = "5";
            this.radColor5.UseVisualStyleBackColor = false;
            this.radColor5.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor4
            // 
            this.radColor4.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radColor4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor4.Location = new System.Drawing.Point(8, 96);
            this.radColor4.Name = "radColor4";
            this.radColor4.Size = new System.Drawing.Size(24, 24);
            this.radColor4.TabIndex = 2;
            this.radColor4.Tag = "4";
            this.radColor4.UseVisualStyleBackColor = false;
            this.radColor4.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor3
            // 
            this.radColor3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor3.BackColor = System.Drawing.Color.OrangeRed;
            this.radColor3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor3.Location = new System.Drawing.Point(38, 66);
            this.radColor3.Name = "radColor3";
            this.radColor3.Size = new System.Drawing.Size(24, 24);
            this.radColor3.TabIndex = 2;
            this.radColor3.Tag = "3";
            this.radColor3.UseVisualStyleBackColor = false;
            this.radColor3.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor2
            // 
            this.radColor2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor2.BackColor = System.Drawing.Color.White;
            this.radColor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor2.Location = new System.Drawing.Point(8, 66);
            this.radColor2.Name = "radColor2";
            this.radColor2.Size = new System.Drawing.Size(24, 24);
            this.radColor2.TabIndex = 2;
            this.radColor2.Tag = "2";
            this.radColor2.UseVisualStyleBackColor = false;
            this.radColor2.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor1
            // 
            this.radColor1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor1.BackColor = System.Drawing.Color.Red;
            this.radColor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor1.Location = new System.Drawing.Point(38, 36);
            this.radColor1.Name = "radColor1";
            this.radColor1.Size = new System.Drawing.Size(24, 24);
            this.radColor1.TabIndex = 2;
            this.radColor1.Tag = "1";
            this.radColor1.UseVisualStyleBackColor = false;
            this.radColor1.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // radColor0
            // 
            this.radColor0.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColor0.BackColor = System.Drawing.Color.Black;
            this.radColor0.Checked = true;
            this.radColor0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColor0.Location = new System.Drawing.Point(8, 36);
            this.radColor0.Name = "radColor0";
            this.radColor0.Size = new System.Drawing.Size(24, 24);
            this.radColor0.TabIndex = 2;
            this.radColor0.TabStop = true;
            this.radColor0.Tag = "0";
            this.radColor0.UseVisualStyleBackColor = false;
            this.radColor0.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // pnlSwitches
            // 
            this.pnlSwitches.Controls.Add(this.btnDeleteSwitch);
            this.pnlSwitches.Controls.Add(this.radSwitch9);
            this.pnlSwitches.Controls.Add(this.radSwitch8);
            this.pnlSwitches.Controls.Add(this.radSwitch7);
            this.pnlSwitches.Controls.Add(this.radSwitch6);
            this.pnlSwitches.Controls.Add(this.radSwitch5);
            this.pnlSwitches.Controls.Add(this.radSwitch4);
            this.pnlSwitches.Controls.Add(this.radSwitch3);
            this.pnlSwitches.Controls.Add(this.radSwitch2);
            this.pnlSwitches.Controls.Add(this.radSwitch1);
            this.pnlSwitches.Controls.Add(this.radSwitch0);
            this.pnlSwitches.Location = new System.Drawing.Point(6, 19);
            this.pnlSwitches.Name = "pnlSwitches";
            this.pnlSwitches.Size = new System.Drawing.Size(87, 210);
            this.pnlSwitches.TabIndex = 2;
            this.pnlSwitches.Visible = false;
            // 
            // radSwitch9
            // 
            this.radSwitch9.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch9.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch9.Location = new System.Drawing.Point(38, 124);
            this.radSwitch9.Name = "radSwitch9";
            this.radSwitch9.Size = new System.Drawing.Size(24, 24);
            this.radSwitch9.TabIndex = 2;
            this.radSwitch9.Tag = "9";
            this.radSwitch9.UseVisualStyleBackColor = false;
            this.radSwitch9.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch8
            // 
            this.radSwitch8.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch8.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch8.Location = new System.Drawing.Point(8, 124);
            this.radSwitch8.Name = "radSwitch8";
            this.radSwitch8.Size = new System.Drawing.Size(24, 24);
            this.radSwitch8.TabIndex = 2;
            this.radSwitch8.Tag = "8";
            this.radSwitch8.UseVisualStyleBackColor = false;
            this.radSwitch8.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch7
            // 
            this.radSwitch7.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch7.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch7.Location = new System.Drawing.Point(38, 94);
            this.radSwitch7.Name = "radSwitch7";
            this.radSwitch7.Size = new System.Drawing.Size(24, 24);
            this.radSwitch7.TabIndex = 2;
            this.radSwitch7.Tag = "7";
            this.radSwitch7.UseVisualStyleBackColor = false;
            this.radSwitch7.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch6
            // 
            this.radSwitch6.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch6.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch6.Location = new System.Drawing.Point(8, 94);
            this.radSwitch6.Name = "radSwitch6";
            this.radSwitch6.Size = new System.Drawing.Size(24, 24);
            this.radSwitch6.TabIndex = 2;
            this.radSwitch6.Tag = "6";
            this.radSwitch6.UseVisualStyleBackColor = false;
            this.radSwitch6.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch5
            // 
            this.radSwitch5.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch5.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch5.Location = new System.Drawing.Point(38, 64);
            this.radSwitch5.Name = "radSwitch5";
            this.radSwitch5.Size = new System.Drawing.Size(24, 24);
            this.radSwitch5.TabIndex = 2;
            this.radSwitch5.Tag = "5";
            this.radSwitch5.UseVisualStyleBackColor = false;
            this.radSwitch5.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch4
            // 
            this.radSwitch4.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch4.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch4.Location = new System.Drawing.Point(8, 64);
            this.radSwitch4.Name = "radSwitch4";
            this.radSwitch4.Size = new System.Drawing.Size(24, 24);
            this.radSwitch4.TabIndex = 2;
            this.radSwitch4.Tag = "4";
            this.radSwitch4.UseVisualStyleBackColor = false;
            this.radSwitch4.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch3
            // 
            this.radSwitch3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch3.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch3.Location = new System.Drawing.Point(38, 34);
            this.radSwitch3.Name = "radSwitch3";
            this.radSwitch3.Size = new System.Drawing.Size(24, 24);
            this.radSwitch3.TabIndex = 2;
            this.radSwitch3.Tag = "3";
            this.radSwitch3.UseVisualStyleBackColor = false;
            this.radSwitch3.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch2
            // 
            this.radSwitch2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch2.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch2.Location = new System.Drawing.Point(8, 34);
            this.radSwitch2.Name = "radSwitch2";
            this.radSwitch2.Size = new System.Drawing.Size(24, 24);
            this.radSwitch2.TabIndex = 2;
            this.radSwitch2.Tag = "2";
            this.radSwitch2.UseVisualStyleBackColor = false;
            this.radSwitch2.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch1
            // 
            this.radSwitch1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch1.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch1.Location = new System.Drawing.Point(38, 4);
            this.radSwitch1.Name = "radSwitch1";
            this.radSwitch1.Size = new System.Drawing.Size(24, 24);
            this.radSwitch1.TabIndex = 2;
            this.radSwitch1.Tag = "1";
            this.radSwitch1.UseVisualStyleBackColor = false;
            this.radSwitch1.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // radSwitch0
            // 
            this.radSwitch0.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSwitch0.BackColor = System.Drawing.SystemColors.Control;
            this.radSwitch0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radSwitch0.Checked = true;
            this.radSwitch0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radSwitch0.Location = new System.Drawing.Point(8, 4);
            this.radSwitch0.Name = "radSwitch0";
            this.radSwitch0.Size = new System.Drawing.Size(24, 24);
            this.radSwitch0.TabIndex = 2;
            this.radSwitch0.TabStop = true;
            this.radSwitch0.Tag = "0";
            this.radSwitch0.UseVisualStyleBackColor = false;
            this.radSwitch0.CheckedChanged += new System.EventHandler(this.RadioSwitchCheckedChanged);
            // 
            // pnlManholes
            // 
            this.pnlManholes.Controls.Add(this.chkManholeSelect);
            this.pnlManholes.Controls.Add(this.btnDeleteManhole);
            this.pnlManholes.Controls.Add(this.radManhole4);
            this.pnlManholes.Controls.Add(this.radManhole3);
            this.pnlManholes.Controls.Add(this.radManhole2);
            this.pnlManholes.Controls.Add(this.radManhole1);
            this.pnlManholes.Controls.Add(this.radManhole0);
            this.pnlManholes.Location = new System.Drawing.Point(6, 19);
            this.pnlManholes.Name = "pnlManholes";
            this.pnlManholes.Size = new System.Drawing.Size(106, 210);
            this.pnlManholes.TabIndex = 2;
            this.pnlManholes.Visible = false;
            // 
            // chkManholeSelect
            // 
            this.chkManholeSelect.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkManholeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkManholeSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.chkManholeSelect.Location = new System.Drawing.Point(38, 168);
            this.chkManholeSelect.Name = "chkManholeSelect";
            this.chkManholeSelect.Size = new System.Drawing.Size(24, 24);
            this.chkManholeSelect.TabIndex = 5;
            this.chkManholeSelect.Text = "1/2";
            this.chkManholeSelect.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripColor,
            this.stripPosition});
            this.statusStrip.Location = new System.Drawing.Point(0, 481);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(714, 22);
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
            this.stripPosition.Size = new System.Drawing.Size(699, 17);
            this.stripPosition.Spring = true;
            this.stripPosition.Text = "Position: (0,0)";
            this.stripPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bwCheckForUpdates
            // 
            this.bwCheckForUpdates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckForUpdates_DoWork);
            this.bwCheckForUpdates.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCheckForUpdates_RunWorkerCompleted);
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.BackColor = System.Drawing.Color.Transparent;
            this.gridControl.Image = ((System.Drawing.Image)(resources.GetObject("gridControl.Image")));
            this.gridControl.Location = new System.Drawing.Point(35, 4);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(450, 450);
            this.gridControl.TabIndex = 3;
            this.gridControl.TabStop = false;
            this.gridControl.GridCellClick += new IntelligentLevelEditor.Games.Pushmo.PushmoGridControl.GridCellHandler(this.GridControlGridCellClick);
            this.gridControl.GridCellHover += new IntelligentLevelEditor.Games.Pushmo.PushmoGridControl.GridCellHandler(this.GridControlGridCellHover);
            this.gridControl.GridCellHoverDown += new IntelligentLevelEditor.Games.Pushmo.PushmoGridControl.GridCellHandler(this.GridControlGridCellHoverDown);
            // 
            // tbtnPencilTool
            // 
            this.tbtnPencilTool.Checked = true;
            this.tbtnPencilTool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbtnPencilTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnPencilTool.Image = global::IntelligentLevelEditor.Properties.Resources.tool_pencil;
            this.tbtnPencilTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPencilTool.Name = "tbtnPencilTool";
            this.tbtnPencilTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnPencilTool.Tag = "0";
            this.tbtnPencilTool.Text = "Pencil";
            this.tbtnPencilTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnPipetteTool
            // 
            this.tbtnPipetteTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnPipetteTool.Image = global::IntelligentLevelEditor.Properties.Resources.tool_pipette;
            this.tbtnPipetteTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPipetteTool.Name = "tbtnPipetteTool";
            this.tbtnPipetteTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnPipetteTool.Tag = "1";
            this.tbtnPipetteTool.Text = "Pipette";
            this.tbtnPipetteTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnFillTool
            // 
            this.tbtnFillTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFillTool.Image = global::IntelligentLevelEditor.Properties.Resources.paintcan;
            this.tbtnFillTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFillTool.Name = "tbtnFillTool";
            this.tbtnFillTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnFillTool.Tag = "2";
            this.tbtnFillTool.Text = "Flood Fill";
            this.tbtnFillTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnFlagTool
            // 
            this.tbtnFlagTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFlagTool.Image = global::IntelligentLevelEditor.Properties.Resources.tool_flag;
            this.tbtnFlagTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFlagTool.Name = "tbtnFlagTool";
            this.tbtnFlagTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnFlagTool.Tag = "3";
            this.tbtnFlagTool.Text = "Flag";
            this.tbtnFlagTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnSwitchTool
            // 
            this.tbtnSwitchTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSwitchTool.Image = global::IntelligentLevelEditor.Properties.Resources.ruby;
            this.tbtnSwitchTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSwitchTool.Name = "tbtnSwitchTool";
            this.tbtnSwitchTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnSwitchTool.Tag = "4";
            this.tbtnSwitchTool.Text = "Pullout Switch";
            this.tbtnSwitchTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnManholeTool
            // 
            this.tbtnManholeTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnManholeTool.Image = global::IntelligentLevelEditor.Properties.Resources.ladder;
            this.tbtnManholeTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnManholeTool.Name = "tbtnManholeTool";
            this.tbtnManholeTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnManholeTool.Tag = "5";
            this.tbtnManholeTool.Text = "Manhole";
            this.tbtnManholeTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // tbtnCloudTool
            // 
            this.tbtnCloudTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCloudTool.Image = global::IntelligentLevelEditor.Properties.Resources.tool_cloud;
            this.tbtnCloudTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCloudTool.Name = "tbtnCloudTool";
            this.tbtnCloudTool.Size = new System.Drawing.Size(30, 28);
            this.tbtnCloudTool.Tag = "6";
            this.tbtnCloudTool.Text = "Cloud";
            this.tbtnCloudTool.Click += new System.EventHandler(this.TbtnToolClick);
            // 
            // chkGrid
            // 
            this.chkGrid.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGrid.AutoSize = true;
            this.chkGrid.Checked = true;
            this.chkGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGrid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkGrid.Image = global::IntelligentLevelEditor.Properties.Resources.ico_grid;
            this.chkGrid.Location = new System.Drawing.Point(128, 181);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(82, 23);
            this.chkGrid.TabIndex = 10;
            this.chkGrid.Text = "Show Grid";
            this.chkGrid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // btnShiftRight
            // 
            this.btnShiftRight.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.arrow_right;
            this.btnShiftRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShiftRight.FlatAppearance.BorderSize = 0;
            this.btnShiftRight.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnShiftRight.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnShiftRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftRight.Location = new System.Drawing.Point(177, 127);
            this.btnShiftRight.Name = "btnShiftRight";
            this.btnShiftRight.Size = new System.Drawing.Size(24, 24);
            this.btnShiftRight.TabIndex = 8;
            this.btnShiftRight.UseVisualStyleBackColor = true;
            this.btnShiftRight.Click += new System.EventHandler(this.ShiftButtonClick);
            // 
            // btnShiftUp
            // 
            this.btnShiftUp.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.arrow_up;
            this.btnShiftUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShiftUp.FlatAppearance.BorderSize = 0;
            this.btnShiftUp.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnShiftUp.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnShiftUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftUp.Location = new System.Drawing.Point(156, 103);
            this.btnShiftUp.Name = "btnShiftUp";
            this.btnShiftUp.Size = new System.Drawing.Size(24, 24);
            this.btnShiftUp.TabIndex = 8;
            this.btnShiftUp.UseVisualStyleBackColor = true;
            this.btnShiftUp.Click += new System.EventHandler(this.ShiftButtonClick);
            // 
            // btnShiftDown
            // 
            this.btnShiftDown.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.arrow_down;
            this.btnShiftDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShiftDown.FlatAppearance.BorderSize = 0;
            this.btnShiftDown.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnShiftDown.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnShiftDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftDown.Location = new System.Drawing.Point(156, 151);
            this.btnShiftDown.Name = "btnShiftDown";
            this.btnShiftDown.Size = new System.Drawing.Size(24, 24);
            this.btnShiftDown.TabIndex = 8;
            this.btnShiftDown.UseVisualStyleBackColor = true;
            this.btnShiftDown.Click += new System.EventHandler(this.ShiftButtonClick);
            // 
            // btnShiftLeft
            // 
            this.btnShiftLeft.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.arrow_left;
            this.btnShiftLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShiftLeft.FlatAppearance.BorderSize = 0;
            this.btnShiftLeft.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnShiftLeft.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnShiftLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftLeft.Location = new System.Drawing.Point(135, 127);
            this.btnShiftLeft.Name = "btnShiftLeft";
            this.btnShiftLeft.Size = new System.Drawing.Size(24, 24);
            this.btnShiftLeft.TabIndex = 8;
            this.btnShiftLeft.UseVisualStyleBackColor = true;
            this.btnShiftLeft.Click += new System.EventHandler(this.ShiftButtonClick);
            // 
            // picShow
            // 
            this.picShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picShow.Location = new System.Drawing.Point(20, 24);
            this.picShow.Name = "picShow";
            this.picShow.Size = new System.Drawing.Size(34, 34);
            this.picShow.TabIndex = 4;
            this.picShow.TabStop = false;
            // 
            // radColorA
            // 
            this.radColorA.Appearance = System.Windows.Forms.Appearance.Button;
            this.radColorA.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.transparentBackground;
            this.radColorA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radColorA.Location = new System.Drawing.Point(8, 186);
            this.radColorA.Name = "radColorA";
            this.radColorA.Size = new System.Drawing.Size(24, 24);
            this.radColorA.TabIndex = 3;
            this.radColorA.Tag = "10";
            this.radColorA.UseVisualStyleBackColor = false;
            this.radColorA.CheckedChanged += new System.EventHandler(this.RadioColorCheckedChange);
            // 
            // btnEditPalette
            // 
            this.btnEditPalette.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.palette;
            this.btnEditPalette.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditPalette.FlatAppearance.BorderSize = 0;
            this.btnEditPalette.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnEditPalette.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnEditPalette.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditPalette.Location = new System.Drawing.Point(38, 186);
            this.btnEditPalette.Name = "btnEditPalette";
            this.btnEditPalette.Size = new System.Drawing.Size(24, 24);
            this.btnEditPalette.TabIndex = 8;
            this.btnEditPalette.UseVisualStyleBackColor = true;
            this.btnEditPalette.Click += new System.EventHandler(this.btnEditPalette_Click);
            // 
            // btnDeleteSwitch
            // 
            this.btnDeleteSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteSwitch.Image = global::IntelligentLevelEditor.Properties.Resources.bin_closed;
            this.btnDeleteSwitch.Location = new System.Drawing.Point(22, 168);
            this.btnDeleteSwitch.Name = "btnDeleteSwitch";
            this.btnDeleteSwitch.Size = new System.Drawing.Size(24, 24);
            this.btnDeleteSwitch.TabIndex = 3;
            this.btnDeleteSwitch.UseVisualStyleBackColor = true;
            this.btnDeleteSwitch.Click += new System.EventHandler(this.btnDeleteSwitch_Click);
            // 
            // btnDeleteManhole
            // 
            this.btnDeleteManhole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeleteManhole.Image = global::IntelligentLevelEditor.Properties.Resources.bin_closed;
            this.btnDeleteManhole.Location = new System.Drawing.Point(8, 168);
            this.btnDeleteManhole.Name = "btnDeleteManhole";
            this.btnDeleteManhole.Size = new System.Drawing.Size(24, 24);
            this.btnDeleteManhole.TabIndex = 3;
            this.btnDeleteManhole.UseVisualStyleBackColor = true;
            this.btnDeleteManhole.Click += new System.EventHandler(this.btnDeleteManhole_Click);
            // 
            // radManhole4
            // 
            this.radManhole4.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManhole4.BackColor = System.Drawing.SystemColors.Control;
            this.radManhole4.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.ladder_purple;
            this.radManhole4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radManhole4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radManhole4.Location = new System.Drawing.Point(8, 124);
            this.radManhole4.Name = "radManhole4";
            this.radManhole4.Size = new System.Drawing.Size(24, 24);
            this.radManhole4.TabIndex = 2;
            this.radManhole4.Tag = "4";
            this.radManhole4.UseVisualStyleBackColor = false;
            this.radManhole4.CheckedChanged += new System.EventHandler(this.RadioManholeCheckedChanged);
            // 
            // radManhole3
            // 
            this.radManhole3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManhole3.BackColor = System.Drawing.SystemColors.Control;
            this.radManhole3.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.ladder_green;
            this.radManhole3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radManhole3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radManhole3.Location = new System.Drawing.Point(8, 94);
            this.radManhole3.Name = "radManhole3";
            this.radManhole3.Size = new System.Drawing.Size(24, 24);
            this.radManhole3.TabIndex = 2;
            this.radManhole3.Tag = "3";
            this.radManhole3.UseVisualStyleBackColor = false;
            this.radManhole3.CheckedChanged += new System.EventHandler(this.RadioManholeCheckedChanged);
            // 
            // radManhole2
            // 
            this.radManhole2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManhole2.BackColor = System.Drawing.SystemColors.Control;
            this.radManhole2.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.ladder_yellow;
            this.radManhole2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radManhole2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radManhole2.Location = new System.Drawing.Point(8, 64);
            this.radManhole2.Name = "radManhole2";
            this.radManhole2.Size = new System.Drawing.Size(24, 24);
            this.radManhole2.TabIndex = 2;
            this.radManhole2.Tag = "2";
            this.radManhole2.UseVisualStyleBackColor = false;
            this.radManhole2.CheckedChanged += new System.EventHandler(this.RadioManholeCheckedChanged);
            // 
            // radManhole1
            // 
            this.radManhole1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManhole1.BackColor = System.Drawing.SystemColors.Control;
            this.radManhole1.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.ladder_blue;
            this.radManhole1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radManhole1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radManhole1.Location = new System.Drawing.Point(8, 34);
            this.radManhole1.Name = "radManhole1";
            this.radManhole1.Size = new System.Drawing.Size(24, 24);
            this.radManhole1.TabIndex = 2;
            this.radManhole1.Tag = "1";
            this.radManhole1.UseVisualStyleBackColor = false;
            this.radManhole1.CheckedChanged += new System.EventHandler(this.RadioManholeCheckedChanged);
            // 
            // radManhole0
            // 
            this.radManhole0.Appearance = System.Windows.Forms.Appearance.Button;
            this.radManhole0.BackColor = System.Drawing.SystemColors.Control;
            this.radManhole0.BackgroundImage = global::IntelligentLevelEditor.Properties.Resources.ladder_red;
            this.radManhole0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radManhole0.Checked = true;
            this.radManhole0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radManhole0.Location = new System.Drawing.Point(8, 4);
            this.radManhole0.Name = "radManhole0";
            this.radManhole0.Size = new System.Drawing.Size(24, 24);
            this.radManhole0.TabIndex = 2;
            this.radManhole0.TabStop = true;
            this.radManhole0.Tag = "0";
            this.radManhole0.UseVisualStyleBackColor = false;
            this.radManhole0.CheckedChanged += new System.EventHandler(this.RadioManholeCheckedChanged);
            // 
            // menuFileNew
            // 
            this.menuFileNew.Image = global::IntelligentLevelEditor.Properties.Resources.page_white;
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuFileNew.Size = new System.Drawing.Size(188, 22);
            this.menuFileNew.Text = "&New...";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Image = global::IntelligentLevelEditor.Properties.Resources.folder;
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpen.Size = new System.Drawing.Size(188, 22);
            this.menuFileOpen.Text = "&Open bin...";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Image = global::IntelligentLevelEditor.Properties.Resources.disk;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSave.Size = new System.Drawing.Size(188, 22);
            this.menuFileSave.Text = "&Save bin";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Image = global::IntelligentLevelEditor.Properties.Resources.door_in;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuFileExit.Size = new System.Drawing.Size(188, 22);
            this.menuFileExit.Text = "E&xit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuQRCodeRead
            // 
            this.menuQRCodeRead.Image = global::IntelligentLevelEditor.Properties.Resources.folder_picture;
            this.menuQRCodeRead.Name = "menuQRCodeRead";
            this.menuQRCodeRead.Size = new System.Drawing.Size(208, 22);
            this.menuQRCodeRead.Text = "&Read from image...";
            this.menuQRCodeRead.Click += new System.EventHandler(this.menuQRCodeRead_Click);
            // 
            // menuQRCodeMake
            // 
            this.menuQRCodeMake.Image = global::IntelligentLevelEditor.Properties.Resources.barcode2d;
            this.menuQRCodeMake.Name = "menuQRCodeMake";
            this.menuQRCodeMake.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuQRCodeMake.Size = new System.Drawing.Size(208, 22);
            this.menuQRCodeMake.Text = "Make &QR Code...";
            this.menuQRCodeMake.Click += new System.EventHandler(this.menuQRCodeMake_Click);
            // 
            // menuQRCodeMakeCard
            // 
            this.menuQRCodeMakeCard.Image = global::IntelligentLevelEditor.Properties.Resources.layout_content;
            this.menuQRCodeMakeCard.Name = "menuQRCodeMakeCard";
            this.menuQRCodeMakeCard.Size = new System.Drawing.Size(208, 22);
            this.menuQRCodeMakeCard.Text = "&Make a QR Card...";
            this.menuQRCodeMakeCard.Click += new System.EventHandler(this.menuQRCodeMakeCard_Click);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 503);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(730, 541);
            this.Name = "FormEditor";
            this.Text = "Intelligent Level Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tbEditor.ResumeLayout(false);
            this.tbEditor.PerformLayout();
            this.grpThumb.ResumeLayout(false);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.pnlColors.ResumeLayout(false);
            this.pnlSwitches.ResumeLayout(false);
            this.pnlManholes.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuFileNewPushmo;
        private System.Windows.Forms.ToolStripMenuItem menuFileNewCrashmo;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator menuFileSep0;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuQRCode;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeRead;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox picShow;
        private PushmoGridControl gridControl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripPosition;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.ToolStripStatusLabel stripColor;
        private System.Windows.Forms.ToolStrip tbEditor;
        private System.Windows.Forms.ToolStripButton tbtnPencilTool;
        private System.Windows.Forms.ToolStripButton tbtnFillTool;
        private System.Windows.Forms.ToolStripButton tbtnFlagTool;
        private System.Windows.Forms.ToolStripButton tbtnSwitchTool;
        private System.Windows.Forms.ToolStripButton tbtnManholeTool;
        private System.Windows.Forms.ToolStripButton tbtnPipetteTool;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.RadioButton radColorA;
        private System.Windows.Forms.RadioButton radColor9;
        private System.Windows.Forms.RadioButton radColor8;
        private System.Windows.Forms.RadioButton radColor7;
        private System.Windows.Forms.RadioButton radColor6;
        private System.Windows.Forms.RadioButton radColor5;
        private System.Windows.Forms.RadioButton radColor4;
        private System.Windows.Forms.RadioButton radColor3;
        private System.Windows.Forms.RadioButton radColor2;
        private System.Windows.Forms.RadioButton radColor1;
        private System.Windows.Forms.RadioButton radColor0;
        private System.Windows.Forms.Label lblToolMessage;
        private System.Windows.Forms.Label lblSelectedColor;
        private System.Windows.Forms.Panel pnlSwitches;
        private System.Windows.Forms.RadioButton radSwitch9;
        private System.Windows.Forms.RadioButton radSwitch8;
        private System.Windows.Forms.RadioButton radSwitch7;
        private System.Windows.Forms.RadioButton radSwitch6;
        private System.Windows.Forms.RadioButton radSwitch5;
        private System.Windows.Forms.RadioButton radSwitch4;
        private System.Windows.Forms.RadioButton radSwitch3;
        private System.Windows.Forms.RadioButton radSwitch2;
        private System.Windows.Forms.RadioButton radSwitch1;
        private System.Windows.Forms.RadioButton radSwitch0;
        private System.Windows.Forms.ToolStripSeparator tbEditorSep1;
        private System.Windows.Forms.Button btnDeleteSwitch;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox grpThumb;
        private System.Windows.Forms.Panel pnlManholes;
        private System.Windows.Forms.Button btnDeleteManhole;
        private System.Windows.Forms.RadioButton radManhole4;
        private System.Windows.Forms.RadioButton radManhole3;
        private System.Windows.Forms.RadioButton radManhole2;
        private System.Windows.Forms.RadioButton radManhole1;
        private System.Windows.Forms.RadioButton radManhole0;
        private System.Windows.Forms.CheckBox chkManholeSelect;
        private System.Windows.Forms.ToolStripSeparator menuQRCodeSep0;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeMakeCard;
        private System.Windows.Forms.ToolStripMenuItem menuQRCodeMake;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.Button btnShiftRight;
        private System.Windows.Forms.Button btnShiftUp;
        private System.Windows.Forms.Button btnShiftDown;
        private System.Windows.Forms.Button btnShiftLeft;
        private System.Windows.Forms.ToolStripMenuItem menuFileImport;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Button btnEditPalette;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.ToolStripMenuItem menuHelpLanguage;
        private System.ComponentModel.BackgroundWorker bwCheckForUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuHelpCheckUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuHelpCheckNow;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep0;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnCloudTool;

    }
}

