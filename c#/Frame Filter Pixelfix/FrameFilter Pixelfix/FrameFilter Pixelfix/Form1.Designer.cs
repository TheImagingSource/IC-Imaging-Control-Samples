namespace FrameFilter_Pixelfix
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
            this.cmdLive = new System.Windows.Forms.Button();
            this.cmdProperties = new System.Windows.Forms.Button();
            this.cmdDevice = new System.Windows.Forms.Button();
            this.chBox_ApplyPixelFix = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
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
            this.icImagingControl1.Location = new System.Drawing.Point(8, 11);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(648, 480);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.DeviceLost += new System.EventHandler<TIS.Imaging.ICImagingControl.DeviceLostEventArgs>(this.icImagingControl1_DeviceLost);
            // 
            // cmdLive
            // 
            this.cmdLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLive.Location = new System.Drawing.Point(666, 71);
            this.cmdLive.Name = "cmdLive";
            this.cmdLive.Size = new System.Drawing.Size(75, 23);
            this.cmdLive.TabIndex = 6;
            this.cmdLive.Text = "Start";
            this.cmdLive.UseVisualStyleBackColor = true;
            this.cmdLive.Click += new System.EventHandler(this.cmdLive_Click);
            // 
            // cmdProperties
            // 
            this.cmdProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdProperties.Location = new System.Drawing.Point(666, 41);
            this.cmdProperties.Name = "cmdProperties";
            this.cmdProperties.Size = new System.Drawing.Size(75, 23);
            this.cmdProperties.TabIndex = 5;
            this.cmdProperties.Text = "Properties";
            this.cmdProperties.UseVisualStyleBackColor = true;
            this.cmdProperties.Click += new System.EventHandler(this.cmdProperties_Click);
            // 
            // cmdDevice
            // 
            this.cmdDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDevice.Location = new System.Drawing.Point(665, 11);
            this.cmdDevice.Name = "cmdDevice";
            this.cmdDevice.Size = new System.Drawing.Size(75, 23);
            this.cmdDevice.TabIndex = 4;
            this.cmdDevice.Text = "Device";
            this.cmdDevice.UseVisualStyleBackColor = true;
            this.cmdDevice.Click += new System.EventHandler(this.cmdDevice_Click);
            // 
            // chBox_ApplyPixelFix
            // 
            this.chBox_ApplyPixelFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chBox_ApplyPixelFix.AutoSize = true;
            this.chBox_ApplyPixelFix.Location = new System.Drawing.Point(662, 100);
            this.chBox_ApplyPixelFix.Name = "chBox_ApplyPixelFix";
            this.chBox_ApplyPixelFix.Size = new System.Drawing.Size(87, 17);
            this.chBox_ApplyPixelFix.TabIndex = 7;
            this.chBox_ApplyPixelFix.Text = "Apply Pixelfix";
            this.chBox_ApplyPixelFix.UseVisualStyleBackColor = true;
            this.chBox_ApplyPixelFix.CheckedChanged += new System.EventHandler(this.chBox_ApplyPixelFix_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 502);
            this.Controls.Add(this.chBox_ApplyPixelFix);
            this.Controls.Add(this.cmdLive);
            this.Controls.Add(this.cmdProperties);
            this.Controls.Add(this.cmdDevice);
            this.Controls.Add(this.icImagingControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FrameFilter Pixelfix";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Button cmdLive;
        private System.Windows.Forms.Button cmdProperties;
        private System.Windows.Forms.Button cmdDevice;
        private System.Windows.Forms.CheckBox chBox_ApplyPixelFix;
    }
}

