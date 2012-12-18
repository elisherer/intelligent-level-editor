using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IntelligentLevelEditor.Games;
using IntelligentLevelEditor.Games.Crashmo;
using IntelligentLevelEditor.Games.Pushmo;
using IntelligentLevelEditor.Properties;

namespace IntelligentLevelEditor
{
    partial class GameSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPyramids = new System.Windows.Forms.Button();
            this.btnCrashmo = new System.Windows.Forms.Button();
            this.btnPushmo = new System.Windows.Forms.Button();
            this.btnFreakyForms = new System.Windows.Forms.Button();
            this.btnDenpaMen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(107, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPyramids
            // 
            this.btnPyramids.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPyramids.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPyramids.Image = global::IntelligentLevelEditor.Properties.Resources.pyramids_logo;
            this.btnPyramids.Location = new System.Drawing.Point(85, 82);
            this.btnPyramids.Name = "btnPyramids";
            this.btnPyramids.Size = new System.Drawing.Size(137, 64);
            this.btnPyramids.TabIndex = 3;
            this.btnPyramids.Text = "Pyramids";
            this.btnPyramids.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPyramids.UseVisualStyleBackColor = true;
            this.btnPyramids.Click += new System.EventHandler(this.btnPyramids_Click);
            // 
            // btnCrashmo
            // 
            this.btnCrashmo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCrashmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCrashmo.Image = global::IntelligentLevelEditor.Properties.Resources.burger;
            this.btnCrashmo.Location = new System.Drawing.Point(163, 13);
            this.btnCrashmo.Name = "btnCrashmo";
            this.btnCrashmo.Size = new System.Drawing.Size(137, 63);
            this.btnCrashmo.TabIndex = 1;
            this.btnCrashmo.Text = "Crashmo / Fallblox";
            this.btnCrashmo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCrashmo.UseVisualStyleBackColor = true;
            this.btnCrashmo.Click += new System.EventHandler(this.btnCrashmo_Click);
            // 
            // btnPushmo
            // 
            this.btnPushmo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPushmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnPushmo.Image = global::IntelligentLevelEditor.Properties.Resources.strawberry;
            this.btnPushmo.Location = new System.Drawing.Point(10, 12);
            this.btnPushmo.Name = "btnPushmo";
            this.btnPushmo.Size = new System.Drawing.Size(137, 64);
            this.btnPushmo.TabIndex = 0;
            this.btnPushmo.Text = "Pushmo / Pullblox";
            this.btnPushmo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPushmo.UseVisualStyleBackColor = true;
            this.btnPushmo.Click += new System.EventHandler(this.btnPushmo_Click);
            // 
            // btnFreakyForms
            // 
            this.btnFreakyForms.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFreakyForms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnFreakyForms.Location = new System.Drawing.Point(228, 82);
            this.btnFreakyForms.Name = "btnFreakyForms";
            this.btnFreakyForms.Size = new System.Drawing.Size(70, 64);
            this.btnFreakyForms.TabIndex = 4;
            this.btnFreakyForms.Text = "Freaky Forms";
            this.btnFreakyForms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFreakyForms.UseVisualStyleBackColor = true;
            this.btnFreakyForms.Click += new System.EventHandler(this.btnFreakyForms_Click);
            // 
            // btnDenpaMen
            // 
            this.btnDenpaMen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDenpaMen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDenpaMen.Location = new System.Drawing.Point(10, 82);
            this.btnDenpaMen.Name = "btnDenpaMen";
            this.btnDenpaMen.Size = new System.Drawing.Size(70, 64);
            this.btnDenpaMen.TabIndex = 4;
            this.btnDenpaMen.Text = "DenpaMen";
            this.btnDenpaMen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDenpaMen.UseVisualStyleBackColor = true;
            this.btnDenpaMen.Click += new System.EventHandler(this.btnDenpaMen_Click);
            // 
            // GameSelect
            // 
            this.ClientSize = new System.Drawing.Size(310, 206);
            this.ControlBox = false;
            this.Controls.Add(this.btnDenpaMen);
            this.Controls.Add(this.btnFreakyForms);
            this.Controls.Add(this.btnPyramids);
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

        private Button btnPushmo;
        private Button btnCrashmo;
        private Button btnCancel;
        private Button btnPyramids;
        private Button btnFreakyForms;
        private Button btnDenpaMen;
    }
}