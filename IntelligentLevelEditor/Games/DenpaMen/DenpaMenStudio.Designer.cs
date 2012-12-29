namespace IntelligentLevelEditor.Games.DenpaMen
{
    partial class DenpaMenStudio
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlDenpa = new System.Windows.Forms.Panel();
            this.lblBodyColor = new System.Windows.Forms.Label();
            this.tctlDenpa = new System.Windows.Forms.TabControl();
            this.tabAntenna = new System.Windows.Forms.TabPage();
            this.lvwAntennas = new System.Windows.Forms.ListView();
            this.lblAntenna = new System.Windows.Forms.Label();
            this.tabColors = new System.Windows.Forms.TabPage();
            this.lvwBodyColors = new System.Windows.Forms.ListView();
            this.tabHead = new System.Windows.Forms.TabPage();
            this.lblHeadShape = new System.Windows.Forms.Label();
            this.lvwHeadShape = new System.Windows.Forms.ListView();
            this.tabFace = new System.Windows.Forms.TabPage();
            this.tabFace2 = new System.Windows.Forms.TabPage();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.nudStats = new System.Windows.Forms.NumericUpDown();
            this.radioRegionUS = new System.Windows.Forms.RadioButton();
            this.radioRegionJP = new System.Windows.Forms.RadioButton();
            this.radioRegionEU = new System.Windows.Forms.RadioButton();
            this.tctlDenpa.SuspendLayout();
            this.tabAntenna.SuspendLayout();
            this.tabColors.SuspendLayout();
            this.tabHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDenpa
            // 
            this.pnlDenpa.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnlDenpa.BackColor = System.Drawing.Color.White;
            this.pnlDenpa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDenpa.Location = new System.Drawing.Point(614, 129);
            this.pnlDenpa.Name = "pnlDenpa";
            this.pnlDenpa.Size = new System.Drawing.Size(104, 182);
            this.pnlDenpa.TabIndex = 0;
            // 
            // lblBodyColor
            // 
            this.lblBodyColor.AutoSize = true;
            this.lblBodyColor.Location = new System.Drawing.Point(3, 13);
            this.lblBodyColor.Name = "lblBodyColor";
            this.lblBodyColor.Size = new System.Drawing.Size(61, 13);
            this.lblBodyColor.TabIndex = 7;
            this.lblBodyColor.Text = "Body Color:";
            // 
            // tctlDenpa
            // 
            this.tctlDenpa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tctlDenpa.Controls.Add(this.tabAntenna);
            this.tctlDenpa.Controls.Add(this.tabColors);
            this.tctlDenpa.Controls.Add(this.tabHead);
            this.tctlDenpa.Controls.Add(this.tabFace);
            this.tctlDenpa.Controls.Add(this.tabFace2);
            this.tctlDenpa.Location = new System.Drawing.Point(12, 78);
            this.tctlDenpa.Name = "tctlDenpa";
            this.tctlDenpa.SelectedIndex = 0;
            this.tctlDenpa.Size = new System.Drawing.Size(593, 351);
            this.tctlDenpa.TabIndex = 8;
            // 
            // tabAntenna
            // 
            this.tabAntenna.AutoScroll = true;
            this.tabAntenna.Controls.Add(this.lvwAntennas);
            this.tabAntenna.Controls.Add(this.lblAntenna);
            this.tabAntenna.Location = new System.Drawing.Point(4, 22);
            this.tabAntenna.Name = "tabAntenna";
            this.tabAntenna.Padding = new System.Windows.Forms.Padding(3);
            this.tabAntenna.Size = new System.Drawing.Size(585, 325);
            this.tabAntenna.TabIndex = 0;
            this.tabAntenna.Text = "Antenna";
            this.tabAntenna.UseVisualStyleBackColor = true;
            // 
            // lvwAntennas
            // 
            this.lvwAntennas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAntennas.HideSelection = false;
            this.lvwAntennas.Location = new System.Drawing.Point(3, 24);
            this.lvwAntennas.MultiSelect = false;
            this.lvwAntennas.Name = "lvwAntennas";
            this.lvwAntennas.Size = new System.Drawing.Size(579, 293);
            this.lvwAntennas.TabIndex = 10;
            this.lvwAntennas.UseCompatibleStateImageBehavior = false;
            // 
            // lblAntenna
            // 
            this.lblAntenna.AutoSize = true;
            this.lblAntenna.Location = new System.Drawing.Point(3, 8);
            this.lblAntenna.Name = "lblAntenna";
            this.lblAntenna.Size = new System.Drawing.Size(89, 13);
            this.lblAntenna.TabIndex = 9;
            this.lblAntenna.Text = "Antenna (Power):";
            // 
            // tabColors
            // 
            this.tabColors.AutoScroll = true;
            this.tabColors.Controls.Add(this.lvwBodyColors);
            this.tabColors.Controls.Add(this.lblBodyColor);
            this.tabColors.Location = new System.Drawing.Point(4, 22);
            this.tabColors.Name = "tabColors";
            this.tabColors.Size = new System.Drawing.Size(585, 325);
            this.tabColors.TabIndex = 2;
            this.tabColors.Text = "Colors";
            this.tabColors.UseVisualStyleBackColor = true;
            // 
            // lvwBodyColors
            // 
            this.lvwBodyColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwBodyColors.HideSelection = false;
            this.lvwBodyColors.Location = new System.Drawing.Point(3, 29);
            this.lvwBodyColors.MultiSelect = false;
            this.lvwBodyColors.Name = "lvwBodyColors";
            this.lvwBodyColors.Size = new System.Drawing.Size(579, 293);
            this.lvwBodyColors.TabIndex = 8;
            this.lvwBodyColors.UseCompatibleStateImageBehavior = false;
            this.lvwBodyColors.SelectedIndexChanged += new System.EventHandler(this.lvwBodyColors_SelectedIndexChanged);
            // 
            // tabHead
            // 
            this.tabHead.AutoScroll = true;
            this.tabHead.Controls.Add(this.lblHeadShape);
            this.tabHead.Controls.Add(this.lvwHeadShape);
            this.tabHead.Location = new System.Drawing.Point(4, 22);
            this.tabHead.Name = "tabHead";
            this.tabHead.Size = new System.Drawing.Size(585, 325);
            this.tabHead.TabIndex = 4;
            this.tabHead.Text = "Head";
            this.tabHead.UseVisualStyleBackColor = true;
            // 
            // lblHeadShape
            // 
            this.lblHeadShape.AutoSize = true;
            this.lblHeadShape.Location = new System.Drawing.Point(3, 13);
            this.lblHeadShape.Name = "lblHeadShape";
            this.lblHeadShape.Size = new System.Drawing.Size(70, 13);
            this.lblHeadShape.TabIndex = 1;
            this.lblHeadShape.Text = "Head Shape:";
            // 
            // lvwHeadShape
            // 
            this.lvwHeadShape.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwHeadShape.HideSelection = false;
            this.lvwHeadShape.Location = new System.Drawing.Point(3, 29);
            this.lvwHeadShape.MultiSelect = false;
            this.lvwHeadShape.Name = "lvwHeadShape";
            this.lvwHeadShape.Size = new System.Drawing.Size(579, 293);
            this.lvwHeadShape.TabIndex = 0;
            this.lvwHeadShape.UseCompatibleStateImageBehavior = false;
            this.lvwHeadShape.SelectedIndexChanged += new System.EventHandler(this.lvwHeadShape_SelectedIndexChanged);
            // 
            // tabFace
            // 
            this.tabFace.AutoScroll = true;
            this.tabFace.Location = new System.Drawing.Point(4, 22);
            this.tabFace.Name = "tabFace";
            this.tabFace.Size = new System.Drawing.Size(585, 325);
            this.tabFace.TabIndex = 3;
            this.tabFace.Text = "Face";
            this.tabFace.UseVisualStyleBackColor = true;
            // 
            // tabFace2
            // 
            this.tabFace2.AutoScroll = true;
            this.tabFace2.Location = new System.Drawing.Point(4, 22);
            this.tabFace2.Name = "tabFace2";
            this.tabFace2.Size = new System.Drawing.Size(585, 325);
            this.tabFace2.TabIndex = 5;
            this.tabFace2.Text = "Face 2";
            this.tabFace2.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(60, 14);
            this.txtName.MaxLength = 12;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(202, 20);
            this.txtName.TabIndex = 10;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(13, 46);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(41, 13);
            this.lblRegion.TabIndex = 11;
            this.lblRegion.Text = "Region";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(183, 49);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(31, 13);
            this.lblStats.TabIndex = 39;
            this.lblStats.Text = "Stats";
            // 
            // nudStats
            // 
            this.nudStats.Location = new System.Drawing.Point(220, 47);
            this.nudStats.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.nudStats.Name = "nudStats";
            this.nudStats.Size = new System.Drawing.Size(42, 20);
            this.nudStats.TabIndex = 40;
            this.nudStats.ValueChanged += new System.EventHandler(this.nudStats_ValueChanged);
            // 
            // radioRegionUS
            // 
            this.radioRegionUS.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioRegionUS.Checked = true;
            this.radioRegionUS.Image = global::IntelligentLevelEditor.Properties.Resources.flag_us;
            this.radioRegionUS.Location = new System.Drawing.Point(60, 40);
            this.radioRegionUS.Name = "radioRegionUS";
            this.radioRegionUS.Size = new System.Drawing.Size(34, 30);
            this.radioRegionUS.TabIndex = 41;
            this.radioRegionUS.TabStop = true;
            this.radioRegionUS.UseVisualStyleBackColor = true;
            this.radioRegionUS.CheckedChanged += new System.EventHandler(this.radioRegionUS_CheckedChanged);
            // 
            // radioRegionJP
            // 
            this.radioRegionJP.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioRegionJP.Image = global::IntelligentLevelEditor.Properties.Resources.flag_jp;
            this.radioRegionJP.Location = new System.Drawing.Point(94, 40);
            this.radioRegionJP.Name = "radioRegionJP";
            this.radioRegionJP.Size = new System.Drawing.Size(34, 30);
            this.radioRegionJP.TabIndex = 42;
            this.radioRegionJP.UseVisualStyleBackColor = true;
            this.radioRegionJP.CheckedChanged += new System.EventHandler(this.radioRegionJP_CheckedChanged);
            // 
            // radioRegionEU
            // 
            this.radioRegionEU.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioRegionEU.Image = global::IntelligentLevelEditor.Properties.Resources.flag_eu;
            this.radioRegionEU.Location = new System.Drawing.Point(128, 40);
            this.radioRegionEU.Name = "radioRegionEU";
            this.radioRegionEU.Size = new System.Drawing.Size(34, 30);
            this.radioRegionEU.TabIndex = 43;
            this.radioRegionEU.UseVisualStyleBackColor = true;
            this.radioRegionEU.CheckedChanged += new System.EventHandler(this.radioRegionEU_CheckedChanged);
            // 
            // DenpaMenStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioRegionEU);
            this.Controls.Add(this.radioRegionJP);
            this.Controls.Add(this.radioRegionUS);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.nudStats);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tctlDenpa);
            this.Controls.Add(this.pnlDenpa);
            this.Name = "DenpaMenStudio";
            this.Size = new System.Drawing.Size(734, 445);
            this.tctlDenpa.ResumeLayout(false);
            this.tabAntenna.ResumeLayout(false);
            this.tabAntenna.PerformLayout();
            this.tabColors.ResumeLayout(false);
            this.tabColors.PerformLayout();
            this.tabHead.ResumeLayout(false);
            this.tabHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDenpa;
        private System.Windows.Forms.Label lblBodyColor;
        private System.Windows.Forms.TabControl tctlDenpa;
        private System.Windows.Forms.TabPage tabAntenna;
        private System.Windows.Forms.TabPage tabColors;
        private System.Windows.Forms.TabPage tabHead;
        private System.Windows.Forms.TabPage tabFace;
        private System.Windows.Forms.TabPage tabFace2;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.NumericUpDown nudStats;
        private System.Windows.Forms.ListView lvwHeadShape;
        private System.Windows.Forms.Label lblHeadShape;
        private System.Windows.Forms.ListView lvwBodyColors;
        private System.Windows.Forms.ListView lvwAntennas;
        private System.Windows.Forms.Label lblAntenna;
        private System.Windows.Forms.RadioButton radioRegionUS;
        private System.Windows.Forms.RadioButton radioRegionJP;
        private System.Windows.Forms.RadioButton radioRegionEU;
    }
}
