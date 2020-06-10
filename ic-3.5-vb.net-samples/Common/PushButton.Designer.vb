<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PushButton
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
        Me.Button = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button
        '
        Me.Button.Location = New System.Drawing.Point(3, 3)
        Me.Button.Name = "Button"
        Me.Button.Size = New System.Drawing.Size(75, 23)
        Me.Button.TabIndex = 1
        Me.Button.Text = "button1"
        Me.Button.UseVisualStyleBackColor = True
        '
        'PushButton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button)
        Me.Name = "PushButton"
        Me.Size = New System.Drawing.Size(83, 30)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Button As System.Windows.Forms.Button

End Class
