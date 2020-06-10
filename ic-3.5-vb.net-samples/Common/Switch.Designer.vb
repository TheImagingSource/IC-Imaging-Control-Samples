<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Switch
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
        Me.Check = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Check
        '
        Me.Check.AutoSize = True
        Me.Check.Location = New System.Drawing.Point(4, 5)
        Me.Check.Name = "Check"
        Me.Check.Size = New System.Drawing.Size(80, 17)
        Me.Check.TabIndex = 1
        Me.Check.Text = "checkBox1"
        Me.Check.UseVisualStyleBackColor = True
        '
        'Switch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Check)
        Me.Name = "Switch"
        Me.Size = New System.Drawing.Size(88, 27)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Check As System.Windows.Forms.CheckBox

End Class
