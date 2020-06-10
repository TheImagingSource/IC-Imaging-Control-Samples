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
        Me.btnCaptureVideo = New System.Windows.Forms.Button
        Me.btnStopLive = New System.Windows.Forms.Button
        Me.btnStartLive = New System.Windows.Forms.Button
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCaptureVideo
        '
        Me.btnCaptureVideo.Location = New System.Drawing.Point(198, 195)
        Me.btnCaptureVideo.Name = "btnCaptureVideo"
        Me.btnCaptureVideo.Size = New System.Drawing.Size(86, 23)
        Me.btnCaptureVideo.TabIndex = 6
        Me.btnCaptureVideo.Text = "Capture Video"
        Me.btnCaptureVideo.UseVisualStyleBackColor = True
        '
        'btnStopLive
        '
        Me.btnStopLive.Location = New System.Drawing.Point(103, 195)
        Me.btnStopLive.Name = "btnStopLive"
        Me.btnStopLive.Size = New System.Drawing.Size(86, 23)
        Me.btnStopLive.TabIndex = 5
        Me.btnStopLive.Text = "Stop Live"
        Me.btnStopLive.UseVisualStyleBackColor = True
        '
        'btnStartLive
        '
        Me.btnStartLive.Location = New System.Drawing.Point(8, 195)
        Me.btnStartLive.Name = "btnStartLive"
        Me.btnStartLive.Size = New System.Drawing.Size(86, 23)
        Me.btnStartLive.TabIndex = 4
        Me.btnStartLive.Text = "Start Live"
        Me.btnStartLive.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(8, 11)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(275, 174)
        Me.IcImagingControl1.TabIndex = 7
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 230)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.btnCaptureVideo)
        Me.Controls.Add(Me.btnStopLive)
        Me.Controls.Add(Me.btnStartLive)
        Me.Name = "Form1"
        Me.Text = "Capturing a Video File"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents btnCaptureVideo As System.Windows.Forms.Button
    Private WithEvents btnStopLive As System.Windows.Forms.Button
    Private WithEvents btnStartLive As System.Windows.Forms.Button
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
