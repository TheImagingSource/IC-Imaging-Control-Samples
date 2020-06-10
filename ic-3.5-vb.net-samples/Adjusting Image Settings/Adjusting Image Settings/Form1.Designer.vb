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
        Me.label1 = New System.Windows.Forms.Label
        Me.trackBar1 = New System.Windows.Forms.TrackBar
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(8, 271)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(53, 13)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Bightness"
        '
        'trackBar1
        '
        Me.trackBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trackBar1.Location = New System.Drawing.Point(78, 265)
        Me.trackBar1.Name = "trackBar1"
        Me.trackBar1.Size = New System.Drawing.Size(310, 45)
        Me.trackBar1.TabIndex = 3
        Me.trackBar1.TickFrequency = 4
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(9, 13)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(382, 236)
        Me.IcImagingControl1.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 313)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.trackBar1)
        Me.Name = "Form1"
        Me.Text = "Adjusting Image Settings"
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
