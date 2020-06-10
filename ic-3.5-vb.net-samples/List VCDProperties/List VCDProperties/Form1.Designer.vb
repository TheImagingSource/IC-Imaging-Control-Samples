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
        Me.CtrlFrame = New System.Windows.Forms.GroupBox
        Me.btnSelectDevice = New System.Windows.Forms.Button
        Me.btnShowPage = New System.Windows.Forms.Button
        Me.Tree = New System.Windows.Forms.TreeView
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CtrlFrame
        '
        Me.CtrlFrame.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CtrlFrame.Location = New System.Drawing.Point(267, 454)
        Me.CtrlFrame.Name = "CtrlFrame"
        Me.CtrlFrame.Size = New System.Drawing.Size(533, 69)
        Me.CtrlFrame.TabIndex = 9
        Me.CtrlFrame.TabStop = False
        Me.CtrlFrame.Text = "Range"
        '
        'btnSelectDevice
        '
        Me.btnSelectDevice.Location = New System.Drawing.Point(10, 12)
        Me.btnSelectDevice.Name = "btnSelectDevice"
        Me.btnSelectDevice.Size = New System.Drawing.Size(125, 23)
        Me.btnSelectDevice.TabIndex = 8
        Me.btnSelectDevice.Text = "Select Device"
        Me.btnSelectDevice.UseVisualStyleBackColor = True
        '
        'btnShowPage
        '
        Me.btnShowPage.Location = New System.Drawing.Point(141, 12)
        Me.btnShowPage.Name = "btnShowPage"
        Me.btnShowPage.Size = New System.Drawing.Size(117, 23)
        Me.btnShowPage.TabIndex = 7
        Me.btnShowPage.Text = "Show Property Page"
        Me.btnShowPage.UseVisualStyleBackColor = True
        '
        'Tree
        '
        Me.Tree.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Tree.Location = New System.Drawing.Point(10, 41)
        Me.Tree.Name = "Tree"
        Me.Tree.Size = New System.Drawing.Size(248, 482)
        Me.Tree.TabIndex = 6
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(267, 16)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(532, 429)
        Me.IcImagingControl1.TabIndex = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 535)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.CtrlFrame)
        Me.Controls.Add(Me.btnSelectDevice)
        Me.Controls.Add(Me.btnShowPage)
        Me.Controls.Add(Me.Tree)
        Me.Name = "Form1"
        Me.Text = "List VCD Properties"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents CtrlFrame As System.Windows.Forms.GroupBox
    Private WithEvents btnSelectDevice As System.Windows.Forms.Button
    Private WithEvents btnShowPage As System.Windows.Forms.Button
    Private WithEvents Tree As System.Windows.Forms.TreeView
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
