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
            this.lblCred.Size = new System.Drawing.Size(341, 130);
            this.lblCred.TabIndex = 3;
            this.lblCred.Text = resources.GetString("lblCred.Text");
            // 
            // btnThanks
            // 
            this.btnThanks.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnThanks.Location = new System.Drawing.Point(271, 223);
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
            this.line0.Size = new System.Drawing.Size(343, 2);
            this.line0.TabIndex = 9;
            this.line0.TabStop = false;
            // 
            // lblPun
            // 
            this.lblPun.AutoSize = true;
            this.lblPun.Location = new System.Drawing.Point(285, 47);
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
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 258);
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

    }
}