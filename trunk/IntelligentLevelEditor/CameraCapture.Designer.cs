using IntelligentLevelEditor.Capture;

namespace IntelligentLevelEditor
{
    partial class CameraCapture
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
            this.pictureCamera = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeviceStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeviceStop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeviceSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDeviceSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeviceSelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeviceSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDeviceCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoSource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVideoCompression = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCamera)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureCamera
            // 
            this.pictureCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureCamera.Location = new System.Drawing.Point(0, 24);
            this.pictureCamera.Name = "pictureCamera";
            this.pictureCamera.Size = new System.Drawing.Size(640, 480);
            this.pictureCamera.TabIndex = 1;
            this.pictureCamera.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDevice,
            this.configurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(640, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip";
            // 
            // menuDevice
            // 
            this.menuDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDeviceStart,
            this.menuDeviceStop,
            this.menuDeviceSeparator0,
            this.menuDeviceSelect,
            this.menuDeviceSeparator1,
            this.menuDeviceCancel});
            this.menuDevice.Name = "menuDevice";
            this.menuDevice.Size = new System.Drawing.Size(54, 20);
            this.menuDevice.Text = "&Device";
            // 
            // menuDeviceStart
            // 
            this.menuDeviceStart.Name = "menuDeviceStart";
            this.menuDeviceStart.Size = new System.Drawing.Size(152, 22);
            this.menuDeviceStart.Text = "&Start";
            this.menuDeviceStart.Click += new System.EventHandler(this.menuDeviceStart_Click);
            // 
            // menuDeviceStop
            // 
            this.menuDeviceStop.Enabled = false;
            this.menuDeviceStop.Name = "menuDeviceStop";
            this.menuDeviceStop.Size = new System.Drawing.Size(152, 22);
            this.menuDeviceStop.Text = "S&top";
            this.menuDeviceStop.Click += new System.EventHandler(this.menuDeviceStop_Click);
            // 
            // menuDeviceSeparator0
            // 
            this.menuDeviceSeparator0.Name = "menuDeviceSeparator0";
            this.menuDeviceSeparator0.Size = new System.Drawing.Size(149, 6);
            // 
            // menuDeviceSelect
            // 
            this.menuDeviceSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDeviceSelectNone});
            this.menuDeviceSelect.Name = "menuDeviceSelect";
            this.menuDeviceSelect.Size = new System.Drawing.Size(152, 22);
            this.menuDeviceSelect.Text = "Select &Device";
            // 
            // menuDeviceSelectNone
            // 
            this.menuDeviceSelectNone.Enabled = false;
            this.menuDeviceSelectNone.Name = "menuDeviceSelectNone";
            this.menuDeviceSelectNone.Size = new System.Drawing.Size(133, 22);
            this.menuDeviceSelectNone.Text = "No Devices";
            // 
            // menuDeviceSeparator1
            // 
            this.menuDeviceSeparator1.Name = "menuDeviceSeparator1";
            this.menuDeviceSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // menuDeviceCancel
            // 
            this.menuDeviceCancel.Name = "menuDeviceCancel";
            this.menuDeviceCancel.Size = new System.Drawing.Size(152, 22);
            this.menuDeviceCancel.Text = "&Cancel";
            this.menuDeviceCancel.Click += new System.EventHandler(this.menuDeviceCancel_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVideoSource,
            this.menuVideoFormat,
            this.menuVideoDisplay,
            this.menuVideoCompression});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.configurationToolStripMenuItem.Text = "&Video";
            // 
            // menuVideoSource
            // 
            this.menuVideoSource.Enabled = false;
            this.menuVideoSource.Name = "menuVideoSource";
            this.menuVideoSource.Size = new System.Drawing.Size(186, 22);
            this.menuVideoSource.Text = "Video &Source...";
            this.menuVideoSource.Click += new System.EventHandler(this.menuVideoSource_Click);
            // 
            // menuVideoFormat
            // 
            this.menuVideoFormat.Enabled = false;
            this.menuVideoFormat.Name = "menuVideoFormat";
            this.menuVideoFormat.Size = new System.Drawing.Size(186, 22);
            this.menuVideoFormat.Text = "Video &Format...";
            this.menuVideoFormat.Click += new System.EventHandler(this.menuVideoFormat_Click);
            // 
            // menuVideoDisplay
            // 
            this.menuVideoDisplay.Enabled = false;
            this.menuVideoDisplay.Name = "menuVideoDisplay";
            this.menuVideoDisplay.Size = new System.Drawing.Size(186, 22);
            this.menuVideoDisplay.Text = "Video &Display...";
            this.menuVideoDisplay.Click += new System.EventHandler(this.menuVideoDisplay_Click);
            // 
            // menuVideoCompression
            // 
            this.menuVideoCompression.Enabled = false;
            this.menuVideoCompression.Name = "menuVideoCompression";
            this.menuVideoCompression.Size = new System.Drawing.Size(186, 22);
            this.menuVideoCompression.Text = "Video &Compression...";
            this.menuVideoCompression.Click += new System.EventHandler(this.menuVideoCompression_Click);
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 504);
            this.Controls.Add(this.pictureCamera);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CameraCapture";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Capture from Camera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraCapture_FormClosing);
            this.Load += new System.EventHandler(this.CameraCapture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCamera)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureCamera;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuDevice;
        private System.Windows.Forms.ToolStripMenuItem menuDeviceStart;
        private System.Windows.Forms.ToolStripMenuItem menuDeviceStop;
        private System.Windows.Forms.ToolStripSeparator menuDeviceSeparator0;
        private System.Windows.Forms.ToolStripMenuItem menuDeviceSelect;
        private System.Windows.Forms.ToolStripSeparator menuDeviceSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuDeviceCancel;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuVideoSource;
        private System.Windows.Forms.ToolStripMenuItem menuVideoFormat;
        private System.Windows.Forms.ToolStripMenuItem menuVideoDisplay;
        private System.Windows.Forms.ToolStripMenuItem menuVideoCompression;
        private System.Windows.Forms.ToolStripMenuItem menuDeviceSelectNone;
    }
}