using IntelligentLevelEditor.WebCam;

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
            this.webcamCapture = new IntelligentLevelEditor.WebCam.WebCamCapture();
            this.pictureCamera = new System.Windows.Forms.PictureBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // webcamCapture
            // 
            this.webcamCapture.CaptureHeight = 240;
            this.webcamCapture.CaptureWidth = 320;
            this.webcamCapture.FrameNumber = ((ulong)(0ul));
            this.webcamCapture.Location = new System.Drawing.Point(0, 0);
            this.webcamCapture.Name = "webcamCapture";
            this.webcamCapture.Size = new System.Drawing.Size(173, 121);
            this.webcamCapture.TabIndex = 0;
            this.webcamCapture.TimeToCapture_milliseconds = 100;
            this.webcamCapture.ImageCaptured += new IntelligentLevelEditor.WebCam.WebCamCapture.WebCamEventHandler(this.WebCamCaptureImageCaptured);
            // 
            // pictureCamera
            // 
            this.pictureCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureCamera.Location = new System.Drawing.Point(11, 8);
            this.pictureCamera.Name = "pictureCamera";
            this.pictureCamera.Size = new System.Drawing.Size(427, 240);
            this.pictureCamera.TabIndex = 1;
            this.pictureCamera.TabStop = false;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStop.Location = new System.Drawing.Point(11, 254);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(78, 29);
            this.btnStartStop.TabIndex = 2;
            this.btnStartStop.Text = "Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(360, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 291);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.pictureCamera);
            this.Controls.Add(this.webcamCapture);
            this.Name = "CameraCapture";
            this.Text = "CameraCapture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraCapture_FormClosing);
            this.Load += new System.EventHandler(this.CameraCapture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WebCamCapture webcamCapture;
        private System.Windows.Forms.PictureBox pictureCamera;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnCancel;
    }
}