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
        Me.chkDigitalOutputState = New System.Windows.Forms.CheckBox
        Me.chkDigitalInputState = New System.Windows.Forms.CheckBox
        Me.cmdWriteDigitalOutput = New System.Windows.Forms.Button
        Me.cmdReadDigitalInput = New System.Windows.Forms.Button
        Me.IcImagingControl1 = New TIS.Imaging.ICImagingControl
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkDigitalOutputState
        '
        Me.chkDigitalOutputState.AutoSize = True
        Me.chkDigitalOutputState.Location = New System.Drawing.Point(93, 427)
        Me.chkDigitalOutputState.Name = "chkDigitalOutputState"
        Me.chkDigitalOutputState.Size = New System.Drawing.Size(86, 17)
        Me.chkDigitalOutputState.TabIndex = 8
        Me.chkDigitalOutputState.Text = "Output State"
        Me.chkDigitalOutputState.UseVisualStyleBackColor = True
        '
        'chkDigitalInputState
        '
        Me.chkDigitalInputState.AutoSize = True
        Me.chkDigitalInputState.Location = New System.Drawing.Point(93, 398)
        Me.chkDigitalInputState.Name = "chkDigitalInputState"
        Me.chkDigitalInputState.Size = New System.Drawing.Size(78, 17)
        Me.chkDigitalInputState.TabIndex = 7
        Me.chkDigitalInputState.Text = "Input State"
        Me.chkDigitalInputState.UseVisualStyleBackColor = True
        '
        'cmdWriteDigitalOutput
        '
        Me.cmdWriteDigitalOutput.Location = New System.Drawing.Point(12, 423)
        Me.cmdWriteDigitalOutput.Name = "cmdWriteDigitalOutput"
        Me.cmdWriteDigitalOutput.Size = New System.Drawing.Size(75, 23)
        Me.cmdWriteDigitalOutput.TabIndex = 6
        Me.cmdWriteDigitalOutput.Text = "Write Input"
        Me.cmdWriteDigitalOutput.UseVisualStyleBackColor = True
        '
        'cmdReadDigitalInput
        '
        Me.cmdReadDigitalInput.Location = New System.Drawing.Point(12, 394)
        Me.cmdReadDigitalInput.Name = "cmdReadDigitalInput"
        Me.cmdReadDigitalInput.Size = New System.Drawing.Size(75, 23)
        Me.cmdReadDigitalInput.TabIndex = 5
        Me.cmdReadDigitalInput.Text = "Read Input"
        Me.cmdReadDigitalInput.UseVisualStyleBackColor = True
        '
        'IcImagingControl1
        '
        Me.IcImagingControl1.BackColor = System.Drawing.Color.White
        Me.IcImagingControl1.LiveDisplayPosition = New System.Drawing.Point(0, 0)
        Me.IcImagingControl1.Location = New System.Drawing.Point(12, 10)
        Me.IcImagingControl1.Name = "IcImagingControl1"
        Me.IcImagingControl1.Size = New System.Drawing.Size(527, 374)
        Me.IcImagingControl1.TabIndex = 9
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 454)
        Me.Controls.Add(Me.IcImagingControl1)
        Me.Controls.Add(Me.chkDigitalOutputState)
        Me.Controls.Add(Me.chkDigitalInputState)
        Me.Controls.Add(Me.cmdWriteDigitalOutput)
        Me.Controls.Add(Me.cmdReadDigitalInput)
        Me.Name = "Form1"
        Me.Text = "DigitalIO"
        CType(Me.IcImagingControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents chkDigitalOutputState As System.Windows.Forms.CheckBox
    Private WithEvents chkDigitalInputState As System.Windows.Forms.CheckBox
    Private WithEvents cmdWriteDigitalOutput As System.Windows.Forms.Button
    Private WithEvents cmdReadDigitalInput As System.Windows.Forms.Button
    Friend WithEvents IcImagingControl1 As TIS.Imaging.ICImagingControl

End Class
