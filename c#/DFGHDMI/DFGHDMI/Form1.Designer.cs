namespace DFGHDMI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInputBits = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInputFPS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInputHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInputWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSignalDetected = new System.Windows.Forms.CheckBox();
            this.chkSourceConnected = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfigure = new System.Windows.Forms.Button();
            this.btnUpdateHDMIInput = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFrameRate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVideoFormat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkFitToWindow = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // icImagingControl1
            // 
            this.icImagingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.icImagingControl1.AutoSize = true;
            this.icImagingControl1.BackColor = System.Drawing.Color.White;
            this.icImagingControl1.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke;
            this.icImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke;
            this.icImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded;
            this.icImagingControl1.LiveDisplayPosition = new System.Drawing.Point(0, 0);
            this.icImagingControl1.Location = new System.Drawing.Point(12, 223);
            this.icImagingControl1.Name = "icImagingControl1";
            this.icImagingControl1.Size = new System.Drawing.Size(1146, 438);
            this.icImagingControl1.TabIndex = 0;
            this.icImagingControl1.Resize += new System.EventHandler(this.icImagingControl1_Resize);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.60498F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39502F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 502F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1146, 205);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtInputBits);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInputFPS);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtInputHeight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtInputWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkSignalDetected);
            this.groupBox1.Controls.Add(this.chkSourceConnected);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 199);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HDMI Input";
            // 
            // txtInputBits
            // 
            this.txtInputBits.Location = new System.Drawing.Point(151, 168);
            this.txtInputBits.Name = "txtInputBits";
            this.txtInputBits.ReadOnly = true;
            this.txtInputBits.Size = new System.Drawing.Size(100, 22);
            this.txtInputBits.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Input Bit Depth";
            // 
            // txtInputFPS
            // 
            this.txtInputFPS.Location = new System.Drawing.Point(151, 140);
            this.txtInputFPS.Name = "txtInputFPS";
            this.txtInputFPS.ReadOnly = true;
            this.txtInputFPS.Size = new System.Drawing.Size(100, 22);
            this.txtInputFPS.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Input FPS";
            // 
            // txtInputHeight
            // 
            this.txtInputHeight.Location = new System.Drawing.Point(151, 112);
            this.txtInputHeight.Name = "txtInputHeight";
            this.txtInputHeight.ReadOnly = true;
            this.txtInputHeight.Size = new System.Drawing.Size(100, 22);
            this.txtInputHeight.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Input Height";
            // 
            // txtInputWidth
            // 
            this.txtInputWidth.Location = new System.Drawing.Point(151, 84);
            this.txtInputWidth.Name = "txtInputWidth";
            this.txtInputWidth.ReadOnly = true;
            this.txtInputWidth.Size = new System.Drawing.Size(100, 22);
            this.txtInputWidth.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Input Width";
            // 
            // chkSignalDetected
            // 
            this.chkSignalDetected.AutoCheck = false;
            this.chkSignalDetected.AutoSize = true;
            this.chkSignalDetected.Location = new System.Drawing.Point(151, 59);
            this.chkSignalDetected.Name = "chkSignalDetected";
            this.chkSignalDetected.Size = new System.Drawing.Size(18, 17);
            this.chkSignalDetected.TabIndex = 3;
            this.chkSignalDetected.UseVisualStyleBackColor = true;
            // 
            // chkSourceConnected
            // 
            this.chkSourceConnected.AutoCheck = false;
            this.chkSourceConnected.AutoSize = true;
            this.chkSourceConnected.Location = new System.Drawing.Point(151, 34);
            this.chkSourceConnected.Name = "chkSourceConnected";
            this.chkSourceConnected.Size = new System.Drawing.Size(18, 17);
            this.chkSourceConnected.TabIndex = 2;
            this.chkSourceConnected.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Signal Detected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Connected";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkFitToWindow);
            this.panel1.Controls.Add(this.btnConfigure);
            this.panel1.Controls.Add(this.btnUpdateHDMIInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(502, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 199);
            this.panel1.TabIndex = 1;
            // 
            // btnConfigure
            // 
            this.btnConfigure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConfigure.Location = new System.Drawing.Point(17, 73);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(103, 29);
            this.btnConfigure.TabIndex = 1;
            this.btnConfigure.Text = "Configure";
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // btnUpdateHDMIInput
            // 
            this.btnUpdateHDMIInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateHDMIInput.Location = new System.Drawing.Point(17, 34);
            this.btnUpdateHDMIInput.Name = "btnUpdateHDMIInput";
            this.btnUpdateHDMIInput.Size = new System.Drawing.Size(103, 29);
            this.btnUpdateHDMIInput.TabIndex = 0;
            this.btnUpdateHDMIInput.Text = "Update Input";
            this.btnUpdateHDMIInput.UseVisualStyleBackColor = true;
            this.btnUpdateHDMIInput.Click += new System.EventHandler(this.btnUpdateHDMIInput_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtFrameRate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtVideoFormat);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(646, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 199);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DFG/HDMI Output";
            // 
            // txtFrameRate
            // 
            this.txtFrameRate.Location = new System.Drawing.Point(104, 58);
            this.txtFrameRate.Name = "txtFrameRate";
            this.txtFrameRate.ReadOnly = true;
            this.txtFrameRate.Size = new System.Drawing.Size(162, 22);
            this.txtFrameRate.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Frame Rate";
            // 
            // txtVideoFormat
            // 
            this.txtVideoFormat.Location = new System.Drawing.Point(104, 30);
            this.txtVideoFormat.Name = "txtVideoFormat";
            this.txtVideoFormat.ReadOnly = true;
            this.txtVideoFormat.Size = new System.Drawing.Size(162, 22);
            this.txtVideoFormat.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Video Format";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Info;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(276, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 170);
            this.label9.TabIndex = 12;
            this.label9.Text = "This group box displays information about the input signal received by the DFG/HD" +
    "MI.\r\n\r\nPress \"Update Input\" to update after a reconfiguration of the source.";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Info;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(279, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(206, 170);
            this.label10.TabIndex = 13;
            this.label10.Text = "The video format defines the format of the data that the DFG/HDMI sends to the co" +
    "mputer.\r\n\r\nPress \"Configure\" to match it to the HDMI source signal.";
            // 
            // chkFitToWindow
            // 
            this.chkFitToWindow.AutoSize = true;
            this.chkFitToWindow.Location = new System.Drawing.Point(17, 143);
            this.chkFitToWindow.Name = "chkFitToWindow";
            this.chkFitToWindow.Size = new System.Drawing.Size(114, 21);
            this.chkFitToWindow.TabIndex = 2;
            this.chkFitToWindow.Text = "Fit to Window";
            this.chkFitToWindow.UseVisualStyleBackColor = true;
            this.chkFitToWindow.CheckedChanged += new System.EventHandler(this.chkFitToWindow_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.icImagingControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1188, 720);
            this.Name = "Form1";
            this.Text = "DFG/HDMI Format Selection";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icImagingControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TIS.Imaging.ICImagingControl icImagingControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInputBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInputFPS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInputHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInputWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSignalDetected;
        private System.Windows.Forms.CheckBox chkSourceConnected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpdateHDMIInput;
        private System.Windows.Forms.Button btnConfigure;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFrameRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVideoFormat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkFitToWindow;
    }
}

