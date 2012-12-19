namespace IntelligentLevelEditor
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblCred = new System.Windows.Forms.Label();
            this.btnThanks = new System.Windows.Forms.Button();
            this.line0 = new System.Windows.Forms.GroupBox();
            this.lblPun = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.linkZXing = new System.Windows.Forms.LinkLabel();
            this.lblZXing = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkIcons = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(70, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(135, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Intelligent Level Editor";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(72, 34);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(60, 13);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "by elisherer";
            // 
            // lblCred
            // 
            this.lblCred.AutoSize = true;
            this.lblCred.Location = new System.Drawing.Point(12, 78);
            this.lblCred.Name = "lblCred";
            this.lblCred.Size = new System.Drawing.Size(326, 91);
            this.lblCred.TabIndex = 3;
            this.lblCred.Text = resources.GetString("lblCred.Text");
            // 
            // btnThanks
            // 
            this.btnThanks.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnThanks.Location = new System.Drawing.Point(377, 306);
            this.btnThanks.Name = "btnThanks";
            this.btnThanks.Size = new System.Drawing.Size(74, 23);
            this.btnThanks.TabIndex = 4;
            this.btnThanks.Text = "Thanks";
            this.btnThanks.UseVisualStyleBackColor = true;
            // 
            // line0
            // 
            this.line0.Location = new System.Drawing.Point(8, 67);
            this.line0.Name = "line0";
            this.line0.Size = new System.Drawing.Size(443, 2);
            this.line0.TabIndex = 9;
            this.line0.TabStop = false;
            // 
            // lblPun
            // 
            this.lblPun.AutoSize = true;
            this.lblPun.Location = new System.Drawing.Point(381, 47);
            this.lblPun.Name = "lblPun";
            this.lblPun.Size = new System.Drawing.Size(66, 13);
            this.lblPun.TabIndex = 11;
            this.lblPun.Text = "ile by eli... lol";
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(9, 12);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(48, 48);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picIcon.TabIndex = 10;
            this.picIcon.TabStop = false;
            // 
            // linkZXing
            // 
            this.linkZXing.AutoSize = true;
            this.linkZXing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkZXing.Location = new System.Drawing.Point(109, 252);
            this.linkZXing.Name = "linkZXing";
            this.linkZXing.Size = new System.Drawing.Size(160, 13);
            this.linkZXing.TabIndex = 12;
            this.linkZXing.TabStop = true;
            this.linkZXing.Text = "http://code.google.com/p/zxing";
            this.linkZXing.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // lblZXing
            // 
            this.lblZXing.AutoSize = true;
            this.lblZXing.Location = new System.Drawing.Point(12, 252);
            this.lblZXing.Name = "lblZXing";
            this.lblZXing.Size = new System.Drawing.Size(91, 13);
            this.lblZXing.TabIndex = 13;
            this.lblZXing.Text = "ZXing QR Library:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Some Icons:";
            // 
            // linkIcons
            // 
            this.linkIcons.AutoSize = true;
            this.linkIcons.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkIcons.Location = new System.Drawing.Point(109, 274);
            this.linkIcons.Name = "linkIcons";
            this.linkIcons.Size = new System.Drawing.Size(140, 13);
            this.linkIcons.TabIndex = 15;
            this.linkIcons.TabStop = true;
            this.linkIcons.Text = "http://www.icondrawer.com";
            this.linkIcons.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "SimplePaletteQuantizer: (Importing images using XiaolinWu)";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.Location = new System.Drawing.Point(109, 228);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(341, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.codeproject.com/KB/recipes/SimplePaletteQuantizer.aspx";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 341);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkIcons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblZXing);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.linkZXing);
            this.Controls.Add(this.lblPun);
            this.Controls.Add(this.line0);
            this.Controls.Add(this.btnThanks);
            this.Controls.Add(this.lblCred);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblCred;
        private System.Windows.Forms.Button btnThanks;
        private System.Windows.Forms.GroupBox line0;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblPun;
        private System.Windows.Forms.LinkLabel linkZXing;
        private System.Windows.Forms.Label lblZXing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkIcons;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;

    }
}