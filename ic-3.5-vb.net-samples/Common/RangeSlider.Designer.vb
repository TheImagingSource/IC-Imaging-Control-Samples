<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RangeSlider
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
        Me.ValueText = New System.Windows.Forms.TextBox
        Me.Slider = New System.Windows.Forms.TrackBar
        CType(Me.Slider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ValueText
        '
        Me.ValueText.Enabled = False
        Me.ValueText.Location = New System.Drawing.Point(318, 9)
        Me.ValueText.Name = "ValueText"
        Me.ValueText.ReadOnly = True
        Me.ValueText.Size = New System.Drawing.Size(87, 20)
        Me.ValueText.TabIndex = 5
        '
        'Slider
        '
        Me.Slider.Location = New System.Drawing.Point(6, 5)
        Me.Slider.Name = "Slider"
        Me.Slider.Size = New System.Drawing.Size(306, 45)
        Me.Slider.TabIndex = 4
        '
        'RangeSlider
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ValueText)
        Me.Controls.Add(Me.Slider)
        Me.Name = "RangeSlider"
        Me.Size = New System.Drawing.Size(410, 45)
        CType(Me.Slider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ValueText As System.Windows.Forms.TextBox
    Private WithEvents Slider As System.Windows.Forms.TrackBar

End Class
