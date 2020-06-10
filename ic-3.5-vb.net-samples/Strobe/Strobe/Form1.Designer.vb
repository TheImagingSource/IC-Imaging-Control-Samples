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
        Me.chkStrobe = New System.Windows.Forms.CheckBox
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkStrobe
        '
        Me.chkStrobe.AutoSize = True
        Me.chkStrobe.Location = New System.Drawing.Point(12, 385)
        Me.chkStrobe.Name = "chkStrobe"
        Me.chkStrobe.Size = New System.Drawing.Size(57, 17)
        Me.chkStrobe.TabIndex = 2
        Me.chkStrobe.Text = "Strobe"
        Me.chkStrobe.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(9, 10)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(534, 366)
        Me.IcImagingControl1.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 414)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.chkStrobe)
        Me.Name = "Form1"
        Me.Text = "Strobe"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents chkStrobe As System.Windows.Forms.CheckBox
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
