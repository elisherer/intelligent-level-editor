﻿namespace IntelligentLevelEditor.Games.Pyramids
{
    partial class PyramidsGridControl
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PyramidsGridControl
            // 
            this.Size = new System.Drawing.Size(288, 252);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CellClicked);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PyramidsGridControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CellHovered);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PyramidsGridControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
