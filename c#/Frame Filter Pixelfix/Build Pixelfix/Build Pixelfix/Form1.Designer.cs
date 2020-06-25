namespace Build_Pixelfix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            this.cmdDevice = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.gbGain = new System.Windows.Forms.GroupBox();
            this.tbGain = new System.Windows.Forms.TrackBar();
            this.Threshold = new System.Windows.Forms.GroupBox();
            this.tbThreshold = new System.Windows.Forms.TrackBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbLiveDisplay = new System.Windows.Forms.PictureBox();
            this.gBRadioButtons = new System.Windows.Forms.GroupBox();
            this.radioBtn_ShowHotpixel = new System.Windows.Forms.RadioButton();
            this.radioBtn_FixHotPixel = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            this.gbGain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGain)).BeginInit();
            this.Threshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveDisplay)).BeginInit();
            this.gBRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // icImagingControl1
            // 
            this.icImagingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.icImagingControl1.BackColor = System.Drawing.Color.White;
            this.icImagingControl1.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
            this.icImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.icImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.icImagingControl1.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Location = new System.Drawing.Point(28, 40);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(102, 174);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.Visible = false;
            this.icImagingControl1.DeviceLost += new System.EventHandler<TIS.Imaging.ICImagingControl.DeviceLostEventArgs>(this.icImagingControl1_DeviceLost);
            // 
            // cmdDevice
            // 
            this.cmdDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDevice.Location = new System.Drawing.Point(875, 11);
            this.cmdDevice.Name = "cmdDevice";
            this.cmdDevice.Size = new System.Drawing.Size(75, 23);
            this.cmdDevice.TabIndex = 0;
            this.cmdDevice.Text = "Device";
            this.cmdDevice.UseVisualStyleBackColor = true;
            this.cmdDevice.Click += new System.EventHandler(this.cmdDevice_Click);
            // 
            // btn_Help
            // 
            this.btn_Help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Help.Location = new System.Drawing.Point(875, 40);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(75, 23);
            this.btn_Help.TabIndex = 1;
            this.btn_Help.Text = "Help";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            // 
            // gbGain
            // 
            this.gbGain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGain.Controls.Add(this.tbGain);
            this.gbGain.Location = new System.Drawing.Point(743, 103);
            this.gbGain.Name = "gbGain";
            this.gbGain.Size = new System.Drawing.Size(206, 75);
            this.gbGain.TabIndex = 9;
            this.gbGain.TabStop = false;
            this.gbGain.Text = "Gain";
            // 
            // tbGain
            // 
            this.tbGain.Location = new System.Drawing.Point(6, 19);
            this.tbGain.Maximum = 100;
            this.tbGain.Name = "tbGain";
            this.tbGain.Size = new System.Drawing.Size(194, 45);
            this.tbGain.TabIndex = 0;
            this.tbGain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbGain.ValueChanged += new System.EventHandler(this.tbGain_ValueChanged);
            // 
            // Threshold
            // 
            this.Threshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Threshold.Controls.Add(this.tbThreshold);
            this.Threshold.Location = new System.Drawing.Point(749, 184);
            this.Threshold.Name = "Threshold";
            this.Threshold.Size = new System.Drawing.Size(78, 285);
            this.Threshold.TabIndex = 10;
            this.Threshold.TabStop = false;
            this.Threshold.Text = "Threshold";
            // 
            // tbThreshold
            // 
            this.tbThreshold.Location = new System.Drawing.Point(6, 19);
            this.tbThreshold.Maximum = 255;
            this.tbThreshold.Name = "tbThreshold";
            this.tbThreshold.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbThreshold.Size = new System.Drawing.Size(45, 258);
            this.tbThreshold.TabIndex = 0;
            this.tbThreshold.Value = 100;
            this.tbThreshold.ValueChanged += new System.EventHandler(this.tbThreshold_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(875, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbLiveDisplay
            // 
            this.pbLiveDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLiveDisplay.Location = new System.Drawing.Point(12, 12);
            this.pbLiveDisplay.Name = "pbLiveDisplay";
            this.pbLiveDisplay.Size = new System.Drawing.Size(725, 550);
            this.pbLiveDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLiveDisplay.TabIndex = 12;
            this.pbLiveDisplay.TabStop = false;
            // 
            // gBRadioButtons
            // 
            this.gBRadioButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gBRadioButtons.Controls.Add(this.radioBtn_ShowHotpixel);
            this.gBRadioButtons.Controls.Add(this.radioBtn_FixHotPixel);
            this.gBRadioButtons.Location = new System.Drawing.Point(833, 184);
            this.gBRadioButtons.Name = "gBRadioButtons";
            this.gBRadioButtons.Size = new System.Drawing.Size(117, 71);
            this.gBRadioButtons.TabIndex = 13;
            this.gBRadioButtons.TabStop = false;
            this.gBRadioButtons.Text = "Display Options";
            // 
            // radioBtn_ShowHotpixel
            // 
            this.radioBtn_ShowHotpixel.AutoSize = true;
            this.radioBtn_ShowHotpixel.Checked = true;
            this.radioBtn_ShowHotpixel.Location = new System.Drawing.Point(17, 42);
            this.radioBtn_ShowHotpixel.Name = "radioBtn_ShowHotpixel";
            this.radioBtn_ShowHotpixel.Size = new System.Drawing.Size(93, 17);
            this.radioBtn_ShowHotpixel.TabIndex = 1;
            this.radioBtn_ShowHotpixel.TabStop = true;
            this.radioBtn_ShowHotpixel.Text = "Show Hotpixel";
            this.radioBtn_ShowHotpixel.UseVisualStyleBackColor = true;
            // 
            // radioBtn_FixHotPixel
            // 
            this.radioBtn_FixHotPixel.AutoSize = true;
            this.radioBtn_FixHotPixel.Location = new System.Drawing.Point(17, 19);
            this.radioBtn_FixHotPixel.Name = "radioBtn_FixHotPixel";
            this.radioBtn_FixHotPixel.Size = new System.Drawing.Size(79, 17);
            this.radioBtn_FixHotPixel.TabIndex = 0;
            this.radioBtn_FixHotPixel.Text = "Fix Hotpixel";
            this.radioBtn_FixHotPixel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 580);
            this.Controls.Add(this.gBRadioButtons);
            this.Controls.Add(this.pbLiveDisplay);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Threshold);
            this.Controls.Add(this.gbGain);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.cmdDevice);
            this.Controls.Add(this.icImagingControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Build Pixelfix";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.gbGain.ResumeLayout(false);
            this.gbGain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGain)).EndInit();
            this.Threshold.ResumeLayout(false);
            this.Threshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLiveDisplay)).EndInit();
            this.gBRadioButtons.ResumeLayout(false);
            this.gBRadioButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Button cmdDevice;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.GroupBox gbGain;
        private System.Windows.Forms.TrackBar tbGain;
        private System.Windows.Forms.GroupBox Threshold;
        private System.Windows.Forms.TrackBar tbThreshold;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbLiveDisplay;
        private System.Windows.Forms.GroupBox gBRadioButtons;
        private System.Windows.Forms.RadioButton radioBtn_ShowHotpixel;
        private System.Windows.Forms.RadioButton radioBtn_FixHotPixel;
    }
}

