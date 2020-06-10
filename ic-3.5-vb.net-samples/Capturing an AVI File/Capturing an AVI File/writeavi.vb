Public Class writeavi

    Private ICControl As TIS.Imaging.ICImagingControl
    Private OldSink As TIS.Imaging.BaseSink

    Public Sub New(ByVal _ic As TIS.Imaging.ICImagingControl)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        ICControl = _ic
    End Sub

    ''' <summary>
    ''' writeavi_Load
    '''
    ''' Display all available video codecs in a combo box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub writeavi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Insert all installed codecs into the cboVideoCodec combobox.
        For Each Codec As TIS.Imaging.AviCompressor In TIS.Imaging.AviCompressor.AviCompressors
            cboVideoCodec.Items.Add(Codec)
        Next
        ' Show the first codec in the combobox.
        cboVideoCodec.SelectedIndex = 0
    End Sub
    
    ''' <summary>
    ''' cboVideoCodec_SelectedValueChanged
    '''
    ''' Handle the change of the current selection in the cvbVideoCodec combo box. If
    ''' the selection has changed, it is checked whether the codec as a properties
    ''' dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboVideoCodec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVideoCodec.SelectedIndexChanged
        Dim Codec As TIS.Imaging.AviCompressor
        ' Retrieve the codec from the cboVideoCodec combobox.
        Codec = CType(cboVideoCodec.SelectedItem(), TIS.Imaging.AviCompressor)

        ' Check for the configuration dialog.
        If Codec.PropertyPageAvailable Then
            cmdShowPropertyPage.Enabled = True
        Else
            cmdShowPropertyPage.Enabled = False
        End If
    End Sub
    
    ''' <summary>
    ''' cmdShowPropertyPage_Click
    '''
    ''' Show the property dialog of the currently selected codec.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdShowPropertyPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowPropertyPage.Click
        Dim Codec As TIS.Imaging.AviCompressor
        ' Retrieve the codec from the cboVideoCodec combobox.
        Codec = CType(cboVideoCodec.SelectedItem(), TIS.Imaging.AviCompressor)
        Codec.ShowPropertyPage()
    End Sub
    
    ''' <summary>
    ''' cmdFilename_Click
    '''
    ''' Select a filename for the AVI file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdFilename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilename.Click
        Dim saveFileDialog1 As New SaveFileDialog

        saveFileDialog1.Filter = "avi files (*.avi)|*.avi|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            txtFilename.Text = saveFileDialog1.FileName
        End If
    End Sub

    ''' <summary>
    ''' cmdStartCapture
    '''
    ''' Start avi capture with the selected filename and codec.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdStartCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartCapture.Click
        If txtFilename.Text = "" Then
            MsgBox("Please select an AVI filename first.", vbExclamation + vbOKOnly)
        Else
            OldSink = ICControl.Sink

            ICControl.Sink = New TIS.Imaging.MediaStreamSink(DirectCast(cboVideoCodec.SelectedItem, TIS.Imaging.AviCompressor), txtFilename.Text)

            ICControl.LiveStart()
            cmdStopCapture.Enabled = True
            cmdStartCapture.Enabled = False
        End If
    End Sub
    
    ''' <summary>
    ''' cmdStopCapture_Click
    '''
    ''' Stop video capture.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdStopCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopCapture.Click
        ICControl.LiveStop()
        ICControl.Sink = OldSink
        cmdStopCapture.Enabled = False
        cmdStartCapture.Enabled = True
    End Sub


    ''' <summary>
    ''' chkPause_Click
    '''
    ''' Pause or restart the avi capture according to the value in chkPause.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPause_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPause.CheckedChanged
        ICControl.Sink.SinkModeRunning = Not chkPause.CheckState
    End Sub
End Class