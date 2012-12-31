namespace IntelligentLevelEditor.Games.HexEditor
{
    partial class HexEditor
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
            this.hexbox = new Be.Windows.Forms.HexBox();
            this.SuspendLayout();
            // 
            // hexbox
            // 
            this.hexbox.BoldFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexbox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexbox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.hexbox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexbox.LineInfoVisible = true;
            this.hexbox.Location = new System.Drawing.Point(0, 0);
            this.hexbox.Name = "hexbox";
            this.hexbox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexbox.Size = new System.Drawing.Size(321, 153);
            this.hexbox.StringViewVisible = true;
            this.hexbox.TabIndex = 0;
            this.hexbox.UseFixedBytesPerLine = true;
            this.hexbox.VScrollBarVisible = true;
            // 
            // HexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hexbox);
            this.Name = "HexEditor";
            this.Size = new System.Drawing.Size(321, 153);
            this.ResumeLayout(false);

        }

        #endregion

        private Be.Windows.Forms.HexBox hexbox;



    }
}
