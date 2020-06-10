<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StringCombo
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Combo = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Combo
        '
        Me.Combo.FormattingEnabled = True
        Me.Combo.Location = New System.Drawing.Point(3, 3)
        Me.Combo.Name = "Combo"
        Me.Combo.Size = New System.Drawing.Size(245, 21)
        Me.Combo.TabIndex = 1
        '
        'StringCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Combo)
        Me.Name = "StringCombo"
        Me.Size = New System.Drawing.Size(251, 26)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Combo As System.Windows.Forms.ComboBox

End Class
