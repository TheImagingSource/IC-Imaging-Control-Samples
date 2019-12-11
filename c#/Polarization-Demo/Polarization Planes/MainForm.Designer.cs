namespace PolarizationPlanes
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
            this.icImagingControl1 = new TIS.Imaging.ICImagingControl();
            this.cboPolarizationPlanes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.icImagingControl1.Location = new System.Drawing.Point(12, 42);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(758, 499);
            this.icImagingControl1.TabIndex = 1;
            // 
            // cboPolarizationPlanes
            // 
            this.cboPolarizationPlanes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPolarizationPlanes.FormattingEnabled = true;
            this.cboPolarizationPlanes.Location = new System.Drawing.Point(243, 12);
            this.cboPolarizationPlanes.Name = "cboPolarizationPlanes";
            this.cboPolarizationPlanes.Size = new System.Drawing.Size(198, 24);
            this.cboPolarizationPlanes.TabIndex = 2;
            this.cboPolarizationPlanes.SelectedIndexChanged += new System.EventHandler(this.cboPolarizationPlanes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Polarization Image Plane";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPolarizationPlanes);
            this.Controls.Add(this.icImagingControl1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Polarization Planes";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.ComboBox cboPolarizationPlanes;
        private System.Windows.Forms.Label label1;
    }
}

