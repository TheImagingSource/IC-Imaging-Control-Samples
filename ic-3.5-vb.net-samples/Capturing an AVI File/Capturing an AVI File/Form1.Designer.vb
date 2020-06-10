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
        Me.cmdStopLive = New System.Windows.Forms.Button
        Me.cmdStartLive = New System.Windows.Forms.Button
        Me.cmdCaptureAVI = New System.Windows.Forms.Button
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdStopLive
        '
        Me.cmdStopLive.Location = New System.Drawing.Point(123, 178)
        Me.cmdStopLive.Name = "cmdStopLive"
        Me.cmdStopLive.Size = New System.Drawing.Size(92, 23)
        Me.cmdStopLive.TabIndex = 7
        Me.cmdStopLive.Text = "Stop Live"
        Me.cmdStopLive.UseVisualStyleBackColor = True
        '
        'cmdStartLive
        '
        Me.cmdStartLive.Location = New System.Drawing.Point(8, 178)
        Me.cmdStartLive.Name = "cmdStartLive"
        Me.cmdStartLive.Size = New System.Drawing.Size(92, 23)
        Me.cmdStartLive.TabIndex = 6
        Me.cmdStartLive.Text = "Start Live"
        Me.cmdStartLive.UseVisualStyleBackColor = True
        '
        'cmdCaptureAVI
        '
        Me.cmdCaptureAVI.Location = New System.Drawing.Point(239, 178)
        Me.cmdCaptureAVI.Name = "cmdCaptureAVI"
        Me.cmdCaptureAVI.Size = New System.Drawing.Size(92, 23)
        Me.cmdCaptureAVI.TabIndex = 5
        Me.cmdCaptureAVI.Text = "Capture AVI"
        Me.cmdCaptureAVI.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(9, 9)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(321, 159)
        Me.IcImagingControl1.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 213)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.cmdStopLive)
        Me.Controls.Add(Me.cmdStartLive)
        Me.Controls.Add(Me.cmdCaptureAVI)
        Me.Name = "Form1"
        Me.Text = "Capturing an AVI File"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents cmdStopLive As System.Windows.Forms.Button
    Private WithEvents cmdStartLive As System.Windows.Forms.Button
    Private WithEvents cmdCaptureAVI As System.Windows.Forms.Button
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
