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
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnDialog = New System.Windows.Forms.Button()
        Me.lblSelectedFilter = New System.Windows.Forms.Label()
        Me.lstFrameFilters = New System.Windows.Forms.ListBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.lstFrameFilterModules = New System.Windows.Forms.ListBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnStopLive = New System.Windows.Forms.Button()
        Me.btnProperties = New System.Windows.Forms.Button()
        Me.btnStartLive = New System.Windows.Forms.Button()
        Me.btnDevice = New System.Windows.Forms.Button()
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl()
        Me.groupBox1.SuspendLayout()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.btnRemove)
        Me.groupBox1.Controls.Add(Me.btnDialog)
        Me.groupBox1.Controls.Add(Me.lblSelectedFilter)
        Me.groupBox1.Location = New System.Drawing.Point(16, 324)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(151, 131)
        Me.groupBox1.TabIndex = 18
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Loaded Filter"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(6, 88)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnDialog
        '
        Me.btnDialog.Location = New System.Drawing.Point(6, 59)
        Me.btnDialog.Name = "btnDialog"
        Me.btnDialog.Size = New System.Drawing.Size(75, 23)
        Me.btnDialog.TabIndex = 2
        Me.btnDialog.Text = "Dialog"
        Me.btnDialog.UseVisualStyleBackColor = True
        '
        'lblSelectedFilter
        '
        Me.lblSelectedFilter.AutoSize = True
        Me.lblSelectedFilter.Location = New System.Drawing.Point(6, 26)
        Me.lblSelectedFilter.Name = "lblSelectedFilter"
        Me.lblSelectedFilter.Size = New System.Drawing.Size(35, 13)
        Me.lblSelectedFilter.TabIndex = 0
        Me.lblSelectedFilter.Text = "label3"
        '
        'lstFrameFilters
        '
        Me.lstFrameFilters.FormattingEnabled = True
        Me.lstFrameFilters.Location = New System.Drawing.Point(12, 219)
        Me.lstFrameFilters.Name = "lstFrameFilters"
        Me.lstFrameFilters.Size = New System.Drawing.Size(156, 95)
        Me.lstFrameFilters.TabIndex = 17
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(16, 203)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(66, 13)
        Me.label2.TabIndex = 16
        Me.label2.Text = "Frame Filters"
        '
        'lstFrameFilterModules
        '
        Me.lstFrameFilterModules.FormattingEnabled = True
        Me.lstFrameFilterModules.Location = New System.Drawing.Point(12, 94)
        Me.lstFrameFilterModules.Name = "lstFrameFilterModules"
        Me.lstFrameFilterModules.Size = New System.Drawing.Size(156, 95)
        Me.lstFrameFilterModules.TabIndex = 15
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(16, 78)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(104, 13)
        Me.label1.TabIndex = 14
        Me.label1.Text = "Frame Filter Modules"
        '
        'btnStopLive
        '
        Me.btnStopLive.Location = New System.Drawing.Point(93, 41)
        Me.btnStopLive.Name = "btnStopLive"
        Me.btnStopLive.Size = New System.Drawing.Size(75, 23)
        Me.btnStopLive.TabIndex = 13
        Me.btnStopLive.Text = "Stop Live"
        Me.btnStopLive.UseVisualStyleBackColor = True
        '
        'btnProperties
        '
        Me.btnProperties.Location = New System.Drawing.Point(93, 12)
        Me.btnProperties.Name = "btnProperties"
        Me.btnProperties.Size = New System.Drawing.Size(75, 23)
        Me.btnProperties.TabIndex = 12
        Me.btnProperties.Text = "Properties"
        Me.btnProperties.UseVisualStyleBackColor = True
        '
        'btnStartLive
        '
        Me.btnStartLive.Location = New System.Drawing.Point(12, 41)
        Me.btnStartLive.Name = "btnStartLive"
        Me.btnStartLive.Size = New System.Drawing.Size(75, 23)
        Me.btnStartLive.TabIndex = 11
        Me.btnStartLive.Text = "Start Live"
        Me.btnStartLive.UseVisualStyleBackColor = True
        '
        'btnDevice
        '
        Me.btnDevice.Location = New System.Drawing.Point(12, 12)
        Me.btnDevice.Name = "btnDevice"
        Me.btnDevice.Size = New System.Drawing.Size(75, 23)
        Me.btnDevice.TabIndex = 10
        Me.btnDevice.Text = "Device"
        Me.btnDevice.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.DeviceListChangedExecutionMode = TIS.Imaging.EventExecutionMode.Invoke
        Me.IcImagingControl1.DeviceLostExecutionMode = TIS.Imaging.EventExecutionMode.AsyncInvoke
        Me.IcImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(176, 16)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(510, 438)
        Me.IcImagingControl1.TabIndex = 19
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 469)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.lstFrameFilters)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.lstFrameFilterModules)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnStopLive)
        Me.Controls.Add(Me.btnProperties)
        Me.Controls.Add(Me.btnStartLive)
        Me.Controls.Add(Me.btnDevice)
        Me.Name = "Form1"
        Me.Text = "Filter Inspector"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents btnRemove As System.Windows.Forms.Button
    Private WithEvents btnDialog As System.Windows.Forms.Button
    Private WithEvents lblSelectedFilter As System.Windows.Forms.Label
    Private WithEvents lstFrameFilters As System.Windows.Forms.ListBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents lstFrameFilterModules As System.Windows.Forms.ListBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnStopLive As System.Windows.Forms.Button
    Private WithEvents btnProperties As System.Windows.Forms.Button
    Private WithEvents btnStartLive As System.Windows.Forms.Button
    Private WithEvents btnDevice As System.Windows.Forms.Button
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
