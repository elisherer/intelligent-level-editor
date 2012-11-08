namespace IntelligentLevelEditor
{
    partial class GameSelect
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
            this.btnPushmo = new System.Windows.Forms.Button();
            this.btnCrashmo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPushmo
            // 
            this.btnPushmo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPushmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnPushmo.Location = new System.Drawing.Point(10, 12);
            this.btnPushmo.Name = "btnPushmo";
            this.btnPushmo.Size = new System.Drawing.Size(137, 64);
            this.btnPushmo.TabIndex = 0;
            this.btnPushmo.Text = "Pushmo / Pullblox";
            this.btnPushmo.UseVisualStyleBackColor = true;
            this.btnPushmo.Click += new System.EventHandler(this.btnPushmo_Click);
            // 
            // btnCrashmo
            // 
            this.btnCrashmo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCrashmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnCrashmo.Location = new System.Drawing.Point(163, 13);
            this.btnCrashmo.Name = "btnCrashmo";
            this.btnCrashmo.Size = new System.Drawing.Size(137, 63);
            this.btnCrashmo.TabIndex = 1;
            this.btnCrashmo.Text = "Crashmo / Fallblox";
            this.btnCrashmo.UseVisualStyleBackColor = true;
            this.btnCrashmo.Click += new System.EventHandler(this.btnCrashmo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(109, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GameSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 166);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCrashmo);
            this.Controls.Add(this.btnPushmo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GameSelect";
            this.ShowInTaskbar = false;
            this.Text = "Select a game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPushmo;
        private System.Windows.Forms.Button btnCrashmo;
        private System.Windows.Forms.Button btnCancel;
    }
}