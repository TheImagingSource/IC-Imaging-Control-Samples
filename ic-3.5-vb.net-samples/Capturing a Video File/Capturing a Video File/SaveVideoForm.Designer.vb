<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SaveVideoForm
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
        Me.btnStartCapture = New System.Windows.Forms.Button
        Me.btnStopCapture = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.btnProperties = New System.Windows.Forms.Button
        Me.txtFileName = New System.Windows.Forms.MaskedTextBox
        Me.cboVideoCodec = New System.Windows.Forms.ComboBox
        Me.cboMediaStreamContainer = New System.Windows.Forms.ComboBox
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'chkPause
        '
        Me.chkPause.AutoSize = True
        Me.chkPause.Location = New System.Drawing.Point(114, 127)
        Me.chkPause.Name = "chkPause"
        Me.chkPause.Size = New System.Drawing.Size(56, 17)
        Me.chkPause.TabIndex = 23
        Me.chkPause.Text = "Pause"
        Me.chkPause.UseVisualStyleBackColor = True
        '
        'btnStartCapture
        '
        Me.btnStartCapture.Location = New System.Drawing.Point(13, 123)
        Me.btnStartCapture.Name = "btnStartCapture"
        Me.btnStartCapture.Size = New System.Drawing.Size(81, 23)
        Me.btnStartCapture.TabIndex = 22
        Me.btnStartCapture.Text = "Start Capture"
        Me.btnStartCapture.UseVisualStyleBackColor = True
        '
        'btnStopCapture
        '
        Me.btnStopCapture.Location = New System.Drawing.Point(211, 123)
        Me.btnStopCapture.Name = "btnStopCapture"
        Me.btnStopCapture.Size = New System.Drawing.Size(89, 23)
        Me.btnStopCapture.TabIndex = 21
        Me.btnStopCapture.Text = "Stop Capture"
        Me.btnStopCapture.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(308, 123)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(308, 76)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 19
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnProperties
        '
        Me.btnProperties.Location = New System.Drawing.Point(308, 43)
        Me.btnProperties.Name = "btnProperties"
        Me.btnProperties.Size = New System.Drawing.Size(75, 23)
        Me.btnProperties.TabIndex = 18
        Me.btnProperties.Text = "Properties..."
        Me.btnProperties.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(100, 79)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(200, 20)
        Me.txtFileName.TabIndex = 17
        '
        'cboVideoCodec
        '
        Me.cboVideoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVideoCodec.FormattingEnabled = True
        Me.cboVideoCodec.Location = New System.Drawing.Point(100, 45)
        Me.cboVideoCodec.Name = "cboVideoCodec"
        Me.cboVideoCodec.Size = New System.Drawing.Size(200, 21)
        Me.cboVideoCodec.TabIndex = 16
        '
        'cboMediaStreamContainer
        '
        Me.cboMediaStreamContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMediaStreamContainer.FormattingEnabled = True
        Me.cboMediaStreamContainer.Location = New System.Drawing.Point(100, 12)
        Me.cboMediaStreamContainer.Name = "cboMediaStreamContainer"
        Me.cboMediaStreamContainer.Size = New System.Drawing.Size(200, 21)
        Me.cboMediaStreamContainer.TabIndex = 15
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(10, 86)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(84, 13)
        Me.label3.TabIndex = 14
        Me.label3.Text = "Video File Name"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(10, 48)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(68, 13)
        Me.label2.TabIndex = 13
        Me.label2.Text = "Video Codec"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(10, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(80, 13)
        Me.label1.TabIndex = 12
        Me.label1.Text = "Video File Type"
        '
        'SaveVideoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 158)
        Me.Controls.Add(Me.chkPause)
        Me.Controls.Add(Me.btnStartCapture)
        Me.Controls.Add(Me.btnStopCapture)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.btnProperties)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.cboVideoCodec)
        Me.Controls.Add(Me.cboMediaStreamContainer)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Name = "SaveVideoForm"
        Me.Text = "SaveVideoForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents chkPause As System.Windows.Forms.CheckBox
    Private WithEvents btnStartCapture As System.Windows.Forms.Button
    Private WithEvents btnStopCapture As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents btnProperties As System.Windows.Forms.Button
    Private WithEvents txtFileName As System.Windows.Forms.MaskedTextBox
    Private WithEvents cboVideoCodec As System.Windows.Forms.ComboBox
    Private WithEvents cboMediaStreamContainer As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
End Class
