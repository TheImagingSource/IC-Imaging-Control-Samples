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
        Me.lblZoom = New System.Windows.Forms.Label
        Me.lblZoomPercent = New System.Windows.Forms.Label
        Me.chkScrollbarsEnable = New System.Windows.Forms.CheckBox
        Me.chkDisplayDefault = New System.Windows.Forms.CheckBox
        Me.cmdImageSettings = New System.Windows.Forms.Button
        Me.cmdStop = New System.Windows.Forms.Button
        Me.cmdDevice = New System.Windows.Forms.Button
        Me.cmdStart = New System.Windows.Forms.Button
        Me.sldZoom = New System.Windows.Forms.TrackBar
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        Me.lblScrollPosition = New System.Windows.Forms.Label
        CType(Me.sldZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblZoom
        '
        Me.lblZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblZoom.AutoSize = True
        Me.lblZoom.Location = New System.Drawing.Point(333, 11)
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(34, 13)
        Me.lblZoom.TabIndex = 18
        Me.lblZoom.Text = "Zoom"
        '
        'lblZoomPercent
        '
        Me.lblZoomPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblZoomPercent.AutoSize = True
        Me.lblZoomPercent.Location = New System.Drawing.Point(329, 268)
        Me.lblZoomPercent.Name = "lblZoomPercent"
        Me.lblZoomPercent.Size = New System.Drawing.Size(35, 13)
        Me.lblZoomPercent.TabIndex = 17
        Me.lblZoomPercent.Text = "label1"
        '
        'chkScrollbarsEnable
        '
        Me.chkScrollbarsEnable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkScrollbarsEnable.AutoSize = True
        Me.chkScrollbarsEnable.Location = New System.Drawing.Point(181, 267)
        Me.chkScrollbarsEnable.Name = "chkScrollbarsEnable"
        Me.chkScrollbarsEnable.Size = New System.Drawing.Size(72, 17)
        Me.chkScrollbarsEnable.TabIndex = 16
        Me.chkScrollbarsEnable.Text = "Scrollbars"
        Me.chkScrollbarsEnable.UseVisualStyleBackColor = True
        '
        'chkDisplayDefault
        '
        Me.chkDisplayDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkDisplayDefault.AutoSize = True
        Me.chkDisplayDefault.Location = New System.Drawing.Point(181, 244)
        Me.chkDisplayDefault.Name = "chkDisplayDefault"
        Me.chkDisplayDefault.Size = New System.Drawing.Size(125, 17)
        Me.chkDisplayDefault.TabIndex = 15
        Me.chkDisplayDefault.Text = "Default Window Size"
        Me.chkDisplayDefault.UseVisualStyleBackColor = True
        '
        'cmdImageSettings
        '
        Me.cmdImageSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdImageSettings.Location = New System.Drawing.Point(87, 273)
        Me.cmdImageSettings.Name = "cmdImageSettings"
        Me.cmdImageSettings.Size = New System.Drawing.Size(75, 23)
        Me.cmdImageSettings.TabIndex = 14
        Me.cmdImageSettings.Text = "Settings"
        Me.cmdImageSettings.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdStop.Location = New System.Drawing.Point(87, 244)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(75, 23)
        Me.cmdStop.TabIndex = 13
        Me.cmdStop.Text = "Stop"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'cmdDevice
        '
        Me.cmdDevice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdDevice.Location = New System.Drawing.Point(6, 273)
        Me.cmdDevice.Name = "cmdDevice"
        Me.cmdDevice.Size = New System.Drawing.Size(75, 23)
        Me.cmdDevice.TabIndex = 12
        Me.cmdDevice.Text = "Device"
        Me.cmdDevice.UseVisualStyleBackColor = True
        '
        'cmdStart
        '
        Me.cmdStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdStart.Location = New System.Drawing.Point(6, 244)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(75, 23)
        Me.cmdStart.TabIndex = 11
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'sldZoom
        '
        Me.sldZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sldZoom.Location = New System.Drawing.Point(332, 27)
        Me.sldZoom.Maximum = 30
        Me.sldZoom.Name = "sldZoom"
        Me.sldZoom.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.sldZoom.Size = New System.Drawing.Size(45, 211)
        Me.sldZoom.TabIndex = 10
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.ImageAvailableExecutionMode = TIS.Imaging.EventExecutionMode.MultiThreaded
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(8, 10)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(316, 227)
        Me.IcImagingControl1.TabIndex = 19
        '
        'lblScrollPosition
        '
        Me.lblScrollPosition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblScrollPosition.AutoSize = True
        Me.lblScrollPosition.Location = New System.Drawing.Point(199, 287)
        Me.lblScrollPosition.Name = "lblScrollPosition"
        Me.lblScrollPosition.Size = New System.Drawing.Size(24, 13)
        Me.lblScrollPosition.TabIndex = 20
        Me.lblScrollPosition.Text = "0/0"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 307)
        Me.Controls.Add(Me.lblScrollPosition)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.lblZoom)
        Me.Controls.Add(Me.lblZoomPercent)
        Me.Controls.Add(Me.chkScrollbarsEnable)
        Me.Controls.Add(Me.chkDisplayDefault)
        Me.Controls.Add(Me.cmdImageSettings)
        Me.Controls.Add(Me.cmdStop)
        Me.Controls.Add(Me.cmdDevice)
        Me.Controls.Add(Me.cmdStart)
        Me.Controls.Add(Me.sldZoom)
        Me.Name = "Form1"
        Me.Text = "Scroll And Zoom"
        CType(Me.sldZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lblZoom As System.Windows.Forms.Label
    Private WithEvents lblZoomPercent As System.Windows.Forms.Label
    Private WithEvents chkScrollbarsEnable As System.Windows.Forms.CheckBox
    Private WithEvents chkDisplayDefault As System.Windows.Forms.CheckBox
    Private WithEvents cmdImageSettings As System.Windows.Forms.Button
    Private WithEvents cmdStop As System.Windows.Forms.Button
    Private WithEvents cmdDevice As System.Windows.Forms.Button
    Private WithEvents cmdStart As System.Windows.Forms.Button
    Private WithEvents sldZoom As System.Windows.Forms.TrackBar
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl
    Friend WithEvents lblScrollPosition As System.Windows.Forms.Label

End Class
