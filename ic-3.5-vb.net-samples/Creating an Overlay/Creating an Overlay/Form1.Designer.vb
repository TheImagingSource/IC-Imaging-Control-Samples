<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnBestFit = New System.Windows.Forms.RadioButton()
        Me.btnGrayscale = New System.Windows.Forms.RadioButton()
        Me.btnColor = New System.Windows.Forms.RadioButton()
        Me.chkPPDisplay = New System.Windows.Forms.CheckBox()
        Me.chkPPSink = New System.Windows.Forms.CheckBox()
        Me.chkPPDevice = New System.Windows.Forms.CheckBox()
        Me.cmdStartStop = New System.Windows.Forms.Button()
        Me.cmdSettings = New System.Windows.Forms.Button()
        Me.cmdDevice = New System.Windows.Forms.Button()
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(485, 19)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(64, 13)
        Me.label2.TabIndex = 22
        Me.label2.Text = "Color Mode:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(285, 19)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(72, 13)
        Me.label1.TabIndex = 21
        Me.label1.Text = "Path Position:"
        '
        'btnBestFit
        '
        Me.btnBestFit.AutoSize = True
        Me.btnBestFit.Location = New System.Drawing.Point(561, 63)
        Me.btnBestFit.Name = "btnBestFit"
        Me.btnBestFit.Size = New System.Drawing.Size(60, 17)
        Me.btnBestFit.TabIndex = 20
        Me.btnBestFit.TabStop = True
        Me.btnBestFit.Text = "Best Fit"
        Me.btnBestFit.UseVisualStyleBackColor = True
        '
        'btnGrayscale
        '
        Me.btnGrayscale.AutoSize = True
        Me.btnGrayscale.Location = New System.Drawing.Point(561, 39)
        Me.btnGrayscale.Name = "btnGrayscale"
        Me.btnGrayscale.Size = New System.Drawing.Size(72, 17)
        Me.btnGrayscale.TabIndex = 19
        Me.btnGrayscale.TabStop = True
        Me.btnGrayscale.Text = "Grayscale"
        Me.btnGrayscale.UseVisualStyleBackColor = True
        '
        'btnColor
        '
        Me.btnColor.AutoSize = True
        Me.btnColor.Location = New System.Drawing.Point(561, 17)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(49, 17)
        Me.btnColor.TabIndex = 18
        Me.btnColor.TabStop = True
        Me.btnColor.Text = "Color"
        Me.btnColor.UseVisualStyleBackColor = True
        '
        'chkPPDisplay
        '
        Me.chkPPDisplay.AutoSize = True
        Me.chkPPDisplay.Location = New System.Drawing.Point(369, 63)
        Me.chkPPDisplay.Name = "chkPPDisplay"
        Me.chkPPDisplay.Size = New System.Drawing.Size(60, 17)
        Me.chkPPDisplay.TabIndex = 17
        Me.chkPPDisplay.Text = "Display"
        Me.chkPPDisplay.UseVisualStyleBackColor = True
        '
        'chkPPSink
        '
        Me.chkPPSink.AutoSize = True
        Me.chkPPSink.Location = New System.Drawing.Point(369, 40)
        Me.chkPPSink.Name = "chkPPSink"
        Me.chkPPSink.Size = New System.Drawing.Size(47, 17)
        Me.chkPPSink.TabIndex = 16
        Me.chkPPSink.Text = "Sink"
        Me.chkPPSink.UseVisualStyleBackColor = True
        '
        'chkPPDevice
        '
        Me.chkPPDevice.AutoSize = True
        Me.chkPPDevice.Location = New System.Drawing.Point(369, 17)
        Me.chkPPDevice.Name = "chkPPDevice"
        Me.chkPPDevice.Size = New System.Drawing.Size(60, 17)
        Me.chkPPDevice.TabIndex = 15
        Me.chkPPDevice.Text = "Device"
        Me.chkPPDevice.UseVisualStyleBackColor = True
        '
        'cmdStartStop
        '
        Me.cmdStartStop.Location = New System.Drawing.Point(176, 13)
        Me.cmdStartStop.Name = "cmdStartStop"
        Me.cmdStartStop.Size = New System.Drawing.Size(75, 23)
        Me.cmdStartStop.TabIndex = 14
        Me.cmdStartStop.Text = "Start"
        Me.cmdStartStop.UseVisualStyleBackColor = True
        '
        'cmdSettings
        '
        Me.cmdSettings.Location = New System.Drawing.Point(95, 13)
        Me.cmdSettings.Name = "cmdSettings"
        Me.cmdSettings.Size = New System.Drawing.Size(75, 23)
        Me.cmdSettings.TabIndex = 13
        Me.cmdSettings.Text = "Settings"
        Me.cmdSettings.UseVisualStyleBackColor = True
        '
        'cmdDevice
        '
        Me.cmdDevice.Location = New System.Drawing.Point(14, 13)
        Me.cmdDevice.Name = "cmdDevice"
        Me.cmdDevice.Size = New System.Drawing.Size(75, 23)
        Me.cmdDevice.TabIndex = 12
        Me.cmdDevice.Text = "Device"
        Me.cmdDevice.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke
        Me.IcImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke
        Me.IcImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(15, 89)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(628, 471)
        Me.IcImagingControl1.TabIndex = 23
        '
        'Timer1
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 572)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnBestFit)
        Me.Controls.Add(Me.btnGrayscale)
        Me.Controls.Add(Me.btnColor)
        Me.Controls.Add(Me.chkPPDisplay)
        Me.Controls.Add(Me.chkPPSink)
        Me.Controls.Add(Me.chkPPDevice)
        Me.Controls.Add(Me.cmdStartStop)
        Me.Controls.Add(Me.cmdSettings)
        Me.Controls.Add(Me.cmdDevice)
        Me.Name = "Form1"
        Me.Text = "Creating an Overlay"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnBestFit As System.Windows.Forms.RadioButton
    Private WithEvents btnGrayscale As System.Windows.Forms.RadioButton
    Private WithEvents btnColor As System.Windows.Forms.RadioButton
    Private WithEvents chkPPDisplay As System.Windows.Forms.CheckBox
    Private WithEvents chkPPSink As System.Windows.Forms.CheckBox
    Private WithEvents chkPPDevice As System.Windows.Forms.CheckBox
    Private WithEvents cmdStartStop As System.Windows.Forms.Button
    Private WithEvents cmdSettings As System.Windows.Forms.Button
    Private WithEvents cmdDevice As System.Windows.Forms.Button
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
