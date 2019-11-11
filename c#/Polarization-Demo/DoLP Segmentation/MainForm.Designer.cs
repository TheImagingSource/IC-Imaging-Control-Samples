namespace DoLPSegmentation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDoLPThreshold = new System.Windows.Forms.TrackBar();
            this.lblDoLPThreshold = new System.Windows.Forms.Label();
            this.tbIntensityThreshold = new System.Windows.Forms.TrackBar();
            this.lblIntensityThreshold = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoLPThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensityThreshold)).BeginInit();
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
            this.icImagingControl1.Location = new System.Drawing.Point(12, 150);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(982, 559);
            this.icImagingControl1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "DoLP Threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Intensity Threshold";
            // 
            // tbDoLPThreshold
            // 
            this.tbDoLPThreshold.LargeChange = 8;
            this.tbDoLPThreshold.Location = new System.Drawing.Point(164, 26);
            this.tbDoLPThreshold.Maximum = 255;
            this.tbDoLPThreshold.Name = "tbDoLPThreshold";
            this.tbDoLPThreshold.Size = new System.Drawing.Size(190, 56);
            this.tbDoLPThreshold.TabIndex = 4;
            this.tbDoLPThreshold.TickFrequency = 16;
            this.tbDoLPThreshold.ValueChanged += new System.EventHandler(this.tbDoLPThreshold_ValueChanged);
            // 
            // lblDoLPThreshold
            // 
            this.lblDoLPThreshold.Location = new System.Drawing.Point(360, 26);
            this.lblDoLPThreshold.Name = "lblDoLPThreshold";
            this.lblDoLPThreshold.Size = new System.Drawing.Size(46, 24);
            this.lblDoLPThreshold.TabIndex = 5;
            this.lblDoLPThreshold.Text = "label3";
            this.lblDoLPThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbIntensityThreshold
            // 
            this.tbIntensityThreshold.LargeChange = 8;
            this.tbIntensityThreshold.Location = new System.Drawing.Point(164, 88);
            this.tbIntensityThreshold.Maximum = 255;
            this.tbIntensityThreshold.Name = "tbIntensityThreshold";
            this.tbIntensityThreshold.Size = new System.Drawing.Size(190, 56);
            this.tbIntensityThreshold.TabIndex = 6;
            this.tbIntensityThreshold.TickFrequency = 16;
            this.tbIntensityThreshold.ValueChanged += new System.EventHandler(this.tbIntensityThreshold_ValueChanged);
            // 
            // lblIntensityThreshold
            // 
            this.lblIntensityThreshold.Location = new System.Drawing.Point(360, 88);
            this.lblIntensityThreshold.Name = "lblIntensityThreshold";
            this.lblIntensityThreshold.Size = new System.Drawing.Size(46, 25);
            this.lblIntensityThreshold.TabIndex = 7;
            this.lblIntensityThreshold.Text = "label4";
            this.lblIntensityThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(430, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(564, 97);
            this.label3.TabIndex = 8;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblIntensityThreshold);
            this.Controls.Add(this.tbIntensityThreshold);
            this.Controls.Add(this.lblDoLPThreshold);
            this.Controls.Add(this.tbDoLPThreshold);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.icImagingControl1);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Polarization Demo: DoLP Segmentation";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDoLPThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensityThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbDoLPThreshold;
        private System.Windows.Forms.Label lblDoLPThreshold;
        private System.Windows.Forms.TrackBar tbIntensityThreshold;
        private System.Windows.Forms.Label lblIntensityThreshold;
        private System.Windows.Forms.Label label3;
    }
}

