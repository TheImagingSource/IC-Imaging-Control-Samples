<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class writeavi
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
        Me.chkPause = New System.Windows.Forms.CheckBox
        Me.cmdStartCapture = New System.Windows.Forms.Button
        Me.cmdStopCapture = New System.Windows.Forms.Button
        Me.cmdFilename = New System.Windows.Forms.Button
        Me.cmdShowPropertyPage = New System.Windows.Forms.Button
        Me.cboVideoCodec = New System.Windows.Forms.ComboBox
        Me.txtFilename = New System.Windows.Forms.MaskedTextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'chkPause
        '
        Me.chkPause.AutoSize = True
        Me.chkPause.Location = New System.Drawing.Point(165, 75)
        Me.chkPause.Name = "chkPause"
        Me.chkPause.Size = New System.Drawing.Size(56, 17)
        Me.chkPause.TabIndex = 17
        Me.chkPause.Text = "Pause"
        Me.chkPause.UseVisualStyleBackColor = True
        '
        'cmdStartCapture
        '
        Me.cmdStartCapture.Location = New System.Drawing.Point(8, 72)
        Me.cmdStartCapture.Name = "cmdStartCapture"
        Me.cmdStartCapture.Size = New System.Drawing.Size(86, 23)
        Me.cmdStartCapture.TabIndex = 16
        Me.cmdStartCapture.Text = "Start Capture"
        Me.cmdStartCapture.UseVisualStyleBackColor = True
        '
        'cmdStopCapture
        '
        Me.cmdStopCapture.Location = New System.Drawing.Point(332, 72)
        Me.cmdStopCapture.Name = "cmdStopCapture"
        Me.cmdStopCapture.Size = New System.Drawing.Size(80, 23)
        Me.cmdStopCapture.TabIndex = 15
        Me.cmdStopCapture.Text = "Stop Capture"
        Me.cmdStopCapture.UseVisualStyleBackColor = True
        '
        'cmdFilename
        '
        Me.cmdFilename.Location = New System.Drawing.Point(420, 43)
        Me.cmdFilename.Name = "cmdFilename"
        Me.cmdFilename.Size = New System.Drawing.Size(75, 23)
        Me.cmdFilename.TabIndex = 14
        Me.cmdFilename.Text = "Browse"
        Me.cmdFilename.UseVisualStyleBackColor = True
        '
        'cmdShowPropertyPage
        '
        Me.cmdShowPropertyPage.Location = New System.Drawing.Point(420, 12)
        Me.cmdShowPropertyPage.Name = "cmdShowPropertyPage"
        Me.cmdShowPropertyPage.Size = New System.Drawing.Size(75, 23)
        Me.cmdShowPropertyPage.TabIndex = 13
        Me.cmdShowPropertyPage.Text = "Properties"
        Me.cmdShowPropertyPage.UseVisualStyleBackColor = True
        '
        'cboVideoCodec
        '
        Me.cboVideoCodec.FormattingEnabled = True
        Me.cboVideoCodec.Location = New System.Drawing.Point(89, 14)
        Me.cboVideoCodec.Name = "cboVideoCodec"
        Me.cboVideoCodec.Size = New System.Drawing.Size(323, 21)
        Me.cboVideoCodec.TabIndex = 12
        '
        'txtFilename
        '
        Me.txtFilename.Location = New System.Drawing.Point(89, 46)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(323, 20)
        Me.txtFilename.TabIndex = 11
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(9, 49)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(74, 13)
        Me.label2.TabIndex = 10
        Me.label2.Text = "AVI File Name"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(9, 17)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(68, 13)
        Me.label1.TabIndex = 9
        Me.label1.Text = "Video Codec"
        '
        'writeavi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 107)
        Me.Controls.Add(Me.chkPause)
        Me.Controls.Add(Me.cmdStartCapture)
        Me.Controls.Add(Me.cmdStopCapture)
        Me.Controls.Add(Me.cmdFilename)
        Me.Controls.Add(Me.cmdShowPropertyPage)
        Me.Controls.Add(Me.cboVideoCodec)
        Me.Controls.Add(Me.txtFilename)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "writeavi"
        Me.Text = "Write AVI"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents chkPause As System.Windows.Forms.CheckBox
    Private WithEvents cmdStartCapture As System.Windows.Forms.Button
    Private WithEvents cmdStopCapture As System.Windows.Forms.Button
    Private WithEvents cmdFilename As System.Windows.Forms.Button
    Private WithEvents cmdShowPropertyPage As System.Windows.Forms.Button
    Private WithEvents cboVideoCodec As System.Windows.Forms.ComboBox
    Private WithEvents txtFilename As System.Windows.Forms.MaskedTextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
