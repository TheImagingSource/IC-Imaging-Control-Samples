<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AviDialog
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
        Me.chkCapturePause = New System.Windows.Forms.CheckBox
        Me.cmdStopButton = New System.Windows.Forms.Button
        Me.cmdStartButton = New System.Windows.Forms.Button
        Me.cmdCloseButton = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.edtAVIFile = New System.Windows.Forms.TextBox
        Me.cmdAVIFileChooser = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.edtConfigFile = New System.Windows.Forms.TextBox
        Me.cmdCodecDataFileChooser = New System.Windows.Forms.Button
        Me.cmdSaveCodecData = New System.Windows.Forms.Button
        Me.cmdLoadCodecData = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdShowPropPage = New System.Windows.Forms.Button
        Me.CodecBox = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkCapturePause
        '
        Me.chkCapturePause.Location = New System.Drawing.Point(331, 180)
        Me.chkCapturePause.Name = "chkCapturePause"
        Me.chkCapturePause.Size = New System.Drawing.Size(64, 24)
        Me.chkCapturePause.TabIndex = 23
        Me.chkCapturePause.Text = "Pause"
        '
        'cmdStopButton
        '
        Me.cmdStopButton.Location = New System.Drawing.Point(187, 180)
        Me.cmdStopButton.Name = "cmdStopButton"
        Me.cmdStopButton.Size = New System.Drawing.Size(128, 24)
        Me.cmdStopButton.TabIndex = 22
        Me.cmdStopButton.Text = "Stop"
        '
        'cmdStartButton
        '
        Me.cmdStartButton.ImageIndex = 0
        Me.cmdStartButton.Location = New System.Drawing.Point(51, 180)
        Me.cmdStartButton.Name = "cmdStartButton"
        Me.cmdStartButton.Size = New System.Drawing.Size(128, 24)
        Me.cmdStartButton.TabIndex = 21
        Me.cmdStartButton.Text = "Start"
        '
        'cmdCloseButton
        '
        Me.cmdCloseButton.Location = New System.Drawing.Point(371, 220)
        Me.cmdCloseButton.Name = "cmdCloseButton"
        Me.cmdCloseButton.Size = New System.Drawing.Size(88, 24)
        Me.cmdCloseButton.TabIndex = 20
        Me.cmdCloseButton.Text = "Close"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.edtAVIFile)
        Me.GroupBox1.Controls.Add(Me.cmdAVIFileChooser)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 132)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 80)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Avi Capture Control"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "File name:"
        '
        'edtAVIFile
        '
        Me.edtAVIFile.Location = New System.Drawing.Point(104, 16)
        Me.edtAVIFile.Name = "edtAVIFile"
        Me.edtAVIFile.Size = New System.Drawing.Size(248, 20)
        Me.edtAVIFile.TabIndex = 1
        Me.edtAVIFile.Text = "test.avi"
        '
        'cmdAVIFileChooser
        '
        Me.cmdAVIFileChooser.Location = New System.Drawing.Point(360, 16)
        Me.cmdAVIFileChooser.Name = "cmdAVIFileChooser"
        Me.cmdAVIFileChooser.Size = New System.Drawing.Size(64, 24)
        Me.cmdAVIFileChooser.TabIndex = 2
        Me.cmdAVIFileChooser.Text = "Browse"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.edtConfigFile)
        Me.GroupBox2.Controls.Add(Me.cmdCodecDataFileChooser)
        Me.GroupBox2.Controls.Add(Me.cmdSaveCodecData)
        Me.GroupBox2.Controls.Add(Me.cmdLoadCodecData)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmdShowPropPage)
        Me.GroupBox2.Controls.Add(Me.CodecBox)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(456, 112)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Codec Data"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Codec:"
        '
        'edtConfigFile
        '
        Me.edtConfigFile.Location = New System.Drawing.Point(104, 48)
        Me.edtConfigFile.Name = "edtConfigFile"
        Me.edtConfigFile.Size = New System.Drawing.Size(248, 20)
        Me.edtConfigFile.TabIndex = 10
        Me.edtConfigFile.Text = "test.cod"
        '
        'cmdCodecDataFileChooser
        '
        Me.cmdCodecDataFileChooser.Location = New System.Drawing.Point(360, 48)
        Me.cmdCodecDataFileChooser.Name = "cmdCodecDataFileChooser"
        Me.cmdCodecDataFileChooser.Size = New System.Drawing.Size(64, 24)
        Me.cmdCodecDataFileChooser.TabIndex = 11
        Me.cmdCodecDataFileChooser.Text = "Browse"
        '
        'cmdSaveCodecData
        '
        Me.cmdSaveCodecData.Location = New System.Drawing.Point(64, 80)
        Me.cmdSaveCodecData.Name = "cmdSaveCodecData"
        Me.cmdSaveCodecData.Size = New System.Drawing.Size(128, 24)
        Me.cmdSaveCodecData.TabIndex = 3
        Me.cmdSaveCodecData.Text = "Save Codec Data"
        '
        'cmdLoadCodecData
        '
        Me.cmdLoadCodecData.Location = New System.Drawing.Point(232, 80)
        Me.cmdLoadCodecData.Name = "cmdLoadCodecData"
        Me.cmdLoadCodecData.Size = New System.Drawing.Size(128, 24)
        Me.cmdLoadCodecData.TabIndex = 4
        Me.cmdLoadCodecData.Text = "Load Codec Data"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 26)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Codec Configuration file:"
        '
        'cmdShowPropPage
        '
        Me.cmdShowPropPage.Location = New System.Drawing.Point(360, 16)
        Me.cmdShowPropPage.Name = "cmdShowPropPage"
        Me.cmdShowPropPage.Size = New System.Drawing.Size(64, 24)
        Me.cmdShowPropPage.TabIndex = 5
        Me.cmdShowPropPage.Text = "Properties"
        '
        'CodecBox
        '
        Me.CodecBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CodecBox.Location = New System.Drawing.Point(104, 16)
        Me.CodecBox.Name = "CodecBox"
        Me.CodecBox.Size = New System.Drawing.Size(248, 21)
        Me.CodecBox.TabIndex = 0
        '
        'AviDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 254)
        Me.Controls.Add(Me.chkCapturePause)
        Me.Controls.Add(Me.cmdStopButton)
        Me.Controls.Add(Me.cmdStartButton)
        Me.Controls.Add(Me.cmdCloseButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "AviDialog"
        Me.Text = "Capture Avi File"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkCapturePause As System.Windows.Forms.CheckBox
    Friend WithEvents cmdStopButton As System.Windows.Forms.Button
    Friend WithEvents cmdStartButton As System.Windows.Forms.Button
    Friend WithEvents cmdCloseButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents edtAVIFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdAVIFileChooser As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents edtConfigFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdCodecDataFileChooser As System.Windows.Forms.Button
    Friend WithEvents cmdSaveCodecData As System.Windows.Forms.Button
    Friend WithEvents cmdLoadCodecData As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdShowPropPage As System.Windows.Forms.Button
    Friend WithEvents CodecBox As System.Windows.Forms.ComboBox
End Class
