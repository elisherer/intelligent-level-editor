namespace IntelligentLevelEditor.Games.FreakyForms
{
    partial class FreakyFormsStudio
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
            this.textName = new System.Windows.Forms.TextBox();
            this.textCatchphrase = new System.Windows.Forms.TextBox();
            this.listviewElements = new System.Windows.Forms.ListView();
            this.textAuthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(79, 12);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(136, 20);
            this.textName.TabIndex = 0;
            // 
            // textCatchphrase
            // 
            this.textCatchphrase.Location = new System.Drawing.Point(79, 38);
            this.textCatchphrase.Name = "textCatchphrase";
            this.textCatchphrase.Size = new System.Drawing.Size(136, 20);
            this.textCatchphrase.TabIndex = 1;
            // 
            // listviewElements
            // 
            this.listviewElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewElements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listviewElements.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listviewElements.GridLines = true;
            this.listviewElements.Location = new System.Drawing.Point(6, 72);
            this.listviewElements.Name = "listviewElements";
            this.listviewElements.Size = new System.Drawing.Size(514, 269);
            this.listviewElements.TabIndex = 2;
            this.listviewElements.UseCompatibleStateImageBehavior = false;
            this.listviewElements.View = System.Windows.Forms.View.Details;
            // 
            // textAuthor
            // 
            this.textAuthor.Location = new System.Drawing.Point(245, 14);
            this.textAuthor.Name = "textAuthor";
            this.textAuthor.Size = new System.Drawing.Size(129, 20);
            this.textAuthor.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "by";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Catchphrase:";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value";
            this.columnHeader3.Width = 600;
            // 
            // FreakyFormsStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textAuthor);
            this.Controls.Add(this.listviewElements);
            this.Controls.Add(this.textCatchphrase);
            this.Controls.Add(this.textName);
            this.Name = "FreakyFormsStudio";
            this.Size = new System.Drawing.Size(523, 344);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textCatchphrase;
        private System.Windows.Forms.ListView listviewElements;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox textAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
