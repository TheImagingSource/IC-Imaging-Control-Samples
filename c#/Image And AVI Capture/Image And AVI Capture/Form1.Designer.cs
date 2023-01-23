namespace Image_And_AVI_Capture
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
            this.checkBoxAVIPause = new System.Windows.Forms.CheckBox();
            this.groupBoxAVICodec = new System.Windows.Forms.GroupBox();
            this.buttonAVIProperties = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonSnap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            this.groupBoxAVICodec.SuspendLayout();
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
            this.icImagingControl1.Location = new System.Drawing.Point(12, 10);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(633, 480);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.DeviceLost += new System.EventHandler<TIS.Imaging.ICImagingControl.DeviceLostEventArgs>(this.icImagingControl1_DeviceLost);
            // 
            // cmdLive
            // 
            this.cmdLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLive.Location = new System.Drawing.Point(651, 40);
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
            this.cmdProperties.Location = new System.Drawing.Point(732, 11);
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
            this.cmdDevice.Location = new System.Drawing.Point(651, 11);
            this.cmdDevice.Name = "cmdDevice";
            this.cmdDevice.Size = new System.Drawing.Size(75, 23);
            this.cmdDevice.TabIndex = 4;
            this.cmdDevice.Text = "Device";
            this.cmdDevice.UseVisualStyleBackColor = true;
            this.cmdDevice.Click += new System.EventHandler(this.cmdDevice_Click);
            // 
            // checkBoxAVIPause
            // 
            this.checkBoxAVIPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAVIPause.AutoSize = true;
            this.checkBoxAVIPause.Location = new System.Drawing.Point(732, 84);
            this.checkBoxAVIPause.Name = "checkBoxAVIPause";
            this.checkBoxAVIPause.Size = new System.Drawing.Size(76, 17);
            this.checkBoxAVIPause.TabIndex = 7;
            this.checkBoxAVIPause.Text = "AVI Pause";
            this.checkBoxAVIPause.UseVisualStyleBackColor = true;
            this.checkBoxAVIPause.CheckedChanged += new System.EventHandler(this.checkBoxAVIPause_CheckedChanged);
            // 
            // groupBoxAVICodec
            // 
            this.groupBoxAVICodec.Controls.Add(this.buttonAVIProperties);
            this.groupBoxAVICodec.Controls.Add(this.comboBox1);
            this.groupBoxAVICodec.Location = new System.Drawing.Point(651, 144);
            this.groupBoxAVICodec.Name = "groupBoxAVICodec";
            this.groupBoxAVICodec.Size = new System.Drawing.Size(157, 87);
            this.groupBoxAVICodec.TabIndex = 8;
            this.groupBoxAVICodec.TabStop = false;
            this.groupBoxAVICodec.Text = "AVI Codec";
            // 
            // buttonAVIProperties
            // 
            this.buttonAVIProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAVIProperties.Location = new System.Drawing.Point(6, 46);
            this.buttonAVIProperties.Name = "buttonAVIProperties";
            this.buttonAVIProperties.Size = new System.Drawing.Size(75, 23);
            this.buttonAVIProperties.TabIndex = 10;
            this.buttonAVIProperties.Text = "Properties";
            this.buttonAVIProperties.UseVisualStyleBackColor = true;
            this.buttonAVIProperties.Click += new System.EventHandler(this.buttonAVIProperties_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonSnap
            // 
            this.buttonSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSnap.Location = new System.Drawing.Point(651, 80);
            this.buttonSnap.Name = "buttonSnap";
            this.buttonSnap.Size = new System.Drawing.Size(75, 23);
            this.buttonSnap.TabIndex = 9;
            this.buttonSnap.Text = "Snap";
            this.buttonSnap.UseVisualStyleBackColor = true;
            this.buttonSnap.Click += new System.EventHandler(this.buttonSnap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 502);
            this.Controls.Add(this.buttonSnap);
            this.Controls.Add(this.groupBoxAVICodec);
            this.Controls.Add(this.checkBoxAVIPause);
            this.Controls.Add(this.cmdLive);
            this.Controls.Add(this.cmdProperties);
            this.Controls.Add(this.cmdDevice);
            this.Controls.Add(this.icImagingControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Image And AVI Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.groupBoxAVICodec.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Button cmdLive;
        private System.Windows.Forms.Button cmdProperties;
        private System.Windows.Forms.Button cmdDevice;
        private System.Windows.Forms.CheckBox checkBoxAVIPause;
        private System.Windows.Forms.GroupBox groupBoxAVICodec;
        private System.Windows.Forms.Button buttonAVIProperties;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonSnap;
    }
}

