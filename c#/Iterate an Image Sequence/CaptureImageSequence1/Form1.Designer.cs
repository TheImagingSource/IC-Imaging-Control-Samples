namespace CaptureImageSequence1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnDevice = new System.Windows.Forms.Button();
            this.sldImages = new System.Windows.Forms.TrackBar();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnLiveVideo = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAutoRepeat = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.lblSliderStart = new System.Windows.Forms.Label();
            this.lblSliderEnd = new System.Windows.Forms.Label();
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            ((System.ComponentModel.ISupportInitialize)(this.sldImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDevice
            // 
            this.btnDevice.Location = new System.Drawing.Point(410, 12);
            this.btnDevice.Name = "btnDevice";
            this.btnDevice.Size = new System.Drawing.Size(75, 23);
            this.btnDevice.TabIndex = 0;
            this.btnDevice.Text = "Device";
            this.btnDevice.UseVisualStyleBackColor = true;
            this.btnDevice.Click += new System.EventHandler(this.btnDevice_Click);
            // 
            // sldImages
            // 
            this.sldImages.Location = new System.Drawing.Point(12, 326);
            this.sldImages.Name = "sldImages";
            this.sldImages.Size = new System.Drawing.Size(392, 45);
            this.sldImages.TabIndex = 4;
            this.sldImages.Scroll += new System.EventHandler(this.sldImages_Scroll);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(410, 41);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnLiveVideo
            // 
            this.btnLiveVideo.Location = new System.Drawing.Point(410, 70);
            this.btnLiveVideo.Name = "btnLiveVideo";
            this.btnLiveVideo.Size = new System.Drawing.Size(75, 23);
            this.btnLiveVideo.TabIndex = 2;
            this.btnLiveVideo.Text = "Start Live";
            this.btnLiveVideo.UseVisualStyleBackColor = true;
            this.btnLiveVideo.Click += new System.EventHandler(this.btnLiveVideo_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAutoRepeat
            // 
            this.btnAutoRepeat.Location = new System.Drawing.Point(410, 326);
            this.btnAutoRepeat.Name = "btnAutoRepeat";
            this.btnAutoRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnAutoRepeat.TabIndex = 5;
            this.btnAutoRepeat.Text = "Auto Repeat";
            this.btnAutoRepeat.UseVisualStyleBackColor = true;
            this.btnAutoRepeat.Click += new System.EventHandler(this.btnAutoRepeat_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(410, 113);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 3;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // lblSliderStart
            // 
            this.lblSliderStart.AutoSize = true;
            this.lblSliderStart.Location = new System.Drawing.Point(15, 312);
            this.lblSliderStart.Name = "lblSliderStart";
            this.lblSliderStart.Size = new System.Drawing.Size(13, 13);
            this.lblSliderStart.TabIndex = 6;
            this.lblSliderStart.Text = "1";
            // 
            // lblSliderEnd
            // 
            this.lblSliderEnd.AutoSize = true;
            this.lblSliderEnd.Location = new System.Drawing.Point(386, 312);
            this.lblSliderEnd.Name = "lblSliderEnd";
            this.lblSliderEnd.Size = new System.Drawing.Size(13, 13);
            this.lblSliderEnd.TabIndex = 7;
            this.lblSliderEnd.Text = "0";
            // 
            // icImagingControl1
            // 
            this.icImagingControl1.BackColor = System.Drawing.Color.White;
            this.icImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.icImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.icImagingControl1.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Location = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(399, 296);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.ImageAvailable += new System.EventHandler<TIS.Imaging.ICImagingControl.ImageAvailableEventArgs>(this.icImagingControl1_ImageAvailable);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 367);
            this.Controls.Add(this.icImagingControl1);
            this.Controls.Add(this.lblSliderEnd);
            this.Controls.Add(this.lblSliderStart);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnAutoRepeat);
            this.Controls.Add(this.btnLiveVideo);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.sldImages);
            this.Controls.Add(this.btnDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Iterate an Image Sequence";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sldImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDevice;
        private System.Windows.Forms.TrackBar sldImages;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLiveVideo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAutoRepeat;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label lblSliderStart;
        private System.Windows.Forms.Label lblSliderEnd;
        private TIS.Imaging.ICImagingControl icImagingControl1;
    }
}

