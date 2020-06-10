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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolbarPlayButton = New System.Windows.Forms.ToolStripButton
        Me.ToolBarStopButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolBarSnapButton = New System.Windows.Forms.ToolStripButton
        Me.ToolBarAVIButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolBarDeviceButton = New System.Windows.Forms.ToolStripButton
        Me.ToolBarPropertiesButton = New System.Windows.Forms.ToolStripButton
        Me.ToolBarInputChannel = New System.Windows.Forms.ToolStripDropDownButton
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolBarTriggerButton = New System.Windows.Forms.ToolStripButton
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip
        Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.saveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.writeAviToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.previewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.playToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.stopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.deviceSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.deviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.inputChannelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.externalTriggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        Me.toolStrip1.SuspendLayout()
        Me.MainMenu1.SuspendLayout()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolbarPlayButton, Me.ToolBarStopButton, Me.toolStripSeparator2, Me.ToolBarSnapButton, Me.ToolBarAVIButton, Me.toolStripSeparator3, Me.ToolBarDeviceButton, Me.ToolBarPropertiesButton, Me.ToolBarInputChannel, Me.toolStripSeparator4, Me.ToolBarTriggerButton})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(360, 25)
        Me.toolStrip1.TabIndex = 3
        Me.toolStrip1.Text = "toolStrip1"
        '
        'ToolbarPlayButton
        '
        Me.ToolbarPlayButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolbarPlayButton.Image = CType(resources.GetObject("ToolbarPlayButton.Image"), System.Drawing.Image)
        Me.ToolbarPlayButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolbarPlayButton.Name = "ToolbarPlayButton"
        Me.ToolbarPlayButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolbarPlayButton.Text = "toolStripButton1"
        '
        'ToolBarStopButton
        '
        Me.ToolBarStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarStopButton.Image = CType(resources.GetObject("ToolBarStopButton.Image"), System.Drawing.Image)
        Me.ToolBarStopButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarStopButton.Name = "ToolBarStopButton"
        Me.ToolBarStopButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarStopButton.Text = "toolStripButton2"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBarSnapButton
        '
        Me.ToolBarSnapButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarSnapButton.Image = CType(resources.GetObject("ToolBarSnapButton.Image"), System.Drawing.Image)
        Me.ToolBarSnapButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarSnapButton.Name = "ToolBarSnapButton"
        Me.ToolBarSnapButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarSnapButton.Text = "toolStripButton3"
        '
        'ToolBarAVIButton
        '
        Me.ToolBarAVIButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarAVIButton.Image = CType(resources.GetObject("ToolBarAVIButton.Image"), System.Drawing.Image)
        Me.ToolBarAVIButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarAVIButton.Name = "ToolBarAVIButton"
        Me.ToolBarAVIButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarAVIButton.Text = "toolStripButton4"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBarDeviceButton
        '
        Me.ToolBarDeviceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarDeviceButton.Image = CType(resources.GetObject("ToolBarDeviceButton.Image"), System.Drawing.Image)
        Me.ToolBarDeviceButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarDeviceButton.Name = "ToolBarDeviceButton"
        Me.ToolBarDeviceButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarDeviceButton.Text = "toolStripButton5"
        '
        'ToolBarPropertiesButton
        '
        Me.ToolBarPropertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarPropertiesButton.Image = CType(resources.GetObject("ToolBarPropertiesButton.Image"), System.Drawing.Image)
        Me.ToolBarPropertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarPropertiesButton.Name = "ToolBarPropertiesButton"
        Me.ToolBarPropertiesButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarPropertiesButton.Text = "toolStripButton6"
        '
        'ToolBarInputChannel
        '
        Me.ToolBarInputChannel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarInputChannel.Image = CType(resources.GetObject("ToolBarInputChannel.Image"), System.Drawing.Image)
        Me.ToolBarInputChannel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarInputChannel.Name = "ToolBarInputChannel"
        Me.ToolBarInputChannel.Size = New System.Drawing.Size(29, 22)
        Me.ToolBarInputChannel.Text = "toolStripButton7"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolBarTriggerButton
        '
        Me.ToolBarTriggerButton.CheckOnClick = True
        Me.ToolBarTriggerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolBarTriggerButton.Image = CType(resources.GetObject("ToolBarTriggerButton.Image"), System.Drawing.Image)
        Me.ToolBarTriggerButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolBarTriggerButton.Name = "ToolBarTriggerButton"
        Me.ToolBarTriggerButton.Size = New System.Drawing.Size(23, 22)
        Me.ToolBarTriggerButton.Text = "toolStripButton8"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.previewToolStripMenuItem, Me.deviceSettingsToolStripMenuItem})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(360, 24)
        Me.MainMenu1.TabIndex = 2
        Me.MainMenu1.Text = "menuStrip1"
        '
        'fileToolStripMenuItem
        '
        Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.saveImageToolStripMenuItem, Me.writeAviToolStripMenuItem, Me.toolStripSeparator1, Me.exitToolStripMenuItem})
        Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
        Me.fileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.fileToolStripMenuItem.Text = "File"
        '
        'saveImageToolStripMenuItem
        '
        Me.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem"
        Me.saveImageToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.saveImageToolStripMenuItem.Text = "Save Image"
        '
        'writeAviToolStripMenuItem
        '
        Me.writeAviToolStripMenuItem.Name = "writeAviToolStripMenuItem"
        Me.writeAviToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.writeAviToolStripMenuItem.Text = "Write Avi..."
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(128, 6)
        '
        'exitToolStripMenuItem
        '
        Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
        Me.exitToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.exitToolStripMenuItem.Text = "Exit"
        '
        'previewToolStripMenuItem
        '
        Me.previewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.playToolStripMenuItem, Me.stopToolStripMenuItem})
        Me.previewToolStripMenuItem.Name = "previewToolStripMenuItem"
        Me.previewToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.previewToolStripMenuItem.Text = "Preview"
        '
        'playToolStripMenuItem
        '
        Me.playToolStripMenuItem.Name = "playToolStripMenuItem"
        Me.playToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.playToolStripMenuItem.Text = "Play"
        '
        'stopToolStripMenuItem
        '
        Me.stopToolStripMenuItem.Name = "stopToolStripMenuItem"
        Me.stopToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.stopToolStripMenuItem.Text = "Stop"
        '
        'deviceSettingsToolStripMenuItem
        '
        Me.deviceSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.deviceToolStripMenuItem, Me.imageToolStripMenuItem, Me.inputChannelsToolStripMenuItem, Me.externalTriggerToolStripMenuItem})
        Me.deviceSettingsToolStripMenuItem.Name = "deviceSettingsToolStripMenuItem"
        Me.deviceSettingsToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.deviceSettingsToolStripMenuItem.Text = "Device Settings"
        '
        'deviceToolStripMenuItem
        '
        Me.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem"
        Me.deviceToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.deviceToolStripMenuItem.Text = "Device..."
        '
        'imageToolStripMenuItem
        '
        Me.imageToolStripMenuItem.Name = "imageToolStripMenuItem"
        Me.imageToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.imageToolStripMenuItem.Text = "Image..."
        '
        'inputChannelsToolStripMenuItem
        '
        Me.inputChannelsToolStripMenuItem.Name = "inputChannelsToolStripMenuItem"
        Me.inputChannelsToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.inputChannelsToolStripMenuItem.Text = "Input Channels"
        '
        'externalTriggerToolStripMenuItem
        '
        Me.externalTriggerToolStripMenuItem.Name = "externalTriggerToolStripMenuItem"
        Me.externalTriggerToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.externalTriggerToolStripMenuItem.Text = "External Trigger"
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(0, 52)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(360, 282)
        Me.IcImagingControl1.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 334)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.toolStrip1)
        Me.Controls.Add(Me.MainMenu1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "Demo Application VB"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents ToolbarPlayButton As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolBarStopButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolBarSnapButton As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolBarAVIButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolBarDeviceButton As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolBarPropertiesButton As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolBarInputChannel As System.Windows.Forms.ToolStripDropDownButton
    Private WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ToolBarTriggerButton As System.Windows.Forms.ToolStripButton
    Private WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Private WithEvents fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents saveImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents writeAviToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents previewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents playToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents stopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents deviceSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents deviceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents imageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents inputChannelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents externalTriggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
