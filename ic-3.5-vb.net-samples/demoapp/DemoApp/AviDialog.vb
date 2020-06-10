Imports TIS.Imaging

Public Class AviDialog
    ' AviDialog
    '
    ' This dialog implements all controls for codec select and AVI capture
    '
    Dim ImagingControl As TIS.Imaging.ICImagingControl
    Private _oldSink As TIS.Imaging.BaseSink
    Private _restartVideo As Boolean

    Public Sub New(ByVal ic As TIS.Imaging.ICImagingControl)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ImagingControl = ic
        _restartVideo = ImagingControl.LiveVideoRunning


    End Sub

    '
    ' AviDialog_Load
    '
    ' Setup the controls. From the registry the last used codec, codec confifuration
    ' file name and AVI file name is retrieved. The related controls are filledwith
    ' these items.
    ' In the file DeviceSettings.vb are the registry functions implemented.
    '
    Private Sub AviDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim LastUsedCodec As String
        Dim i As Integer
        cmdSaveCodecData.Enabled = False
        cmdLoadCodecData.Enabled = False
        cmdShowPropPage.Enabled = False
        cmdStopButton.Enabled = False
        cmdStartButton.Enabled = True

        'Insert all installed codecs into the CodecBox combobox.
        CodecBox.DataSource = TIS.Imaging.AviCompressor.AviCompressors

        LastUsedCodec = GetCodecFromRegisty() ' Retrieve the name of the last used codec.
        ' Select the last used codec in the codec combobox and select it.
        CodecBox.SelectedIndex = 0
        For i = 0 To CodecBox.Items.Count - 1
            If CType(CodecBox.Items.Item(i), AviCompressor).Name = LastUsedCodec Then
                CodecBox.SelectedIndex = i
                i = CodecBox.Items.Count + 1 ' Terminate the loop
            End If
        Next

        ' Get the last used file names from the registry
        edtConfigFile.Text = GetCodecFileFromRegisty()
        edtAVIFile.Text = GetAVIFileFromRegisty()
    End Sub

    '
    ' CodecBox_Changed
    '
    ' Handler sub for the selection change event of the codec combo box.
    ' It is checked, whether the new selected codec has a configuration dialog
    ' and is able to export and import configuration data. Depending on these
    ' capabilities the appropriate controls of the AVI Dialog will be enabled or
    ' disabled.
    '
    Private Sub CodecBox_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CodecBox.SelectedValueChanged
        Dim Codec As AviCompressor
        Codec = CType(CodecBox.SelectedItem(), AviCompressor)

        ' Check for the configuration dialog
        If Codec.PropertyPageAvailable Then
            cmdShowPropPage.Enabled = True
        Else
            cmdShowPropPage.Enabled = False
        End If

        ' Check, whether the codec exports and imports configuration data
        If Codec.CompressorDataSize > 0 Then
            cmdSaveCodecData.Enabled = True
            cmdLoadCodecData.Enabled = True
            edtConfigFile.Enabled = True
            cmdCodecDataFileChooser.Enabled = True
        Else
            cmdSaveCodecData.Enabled = False
            cmdLoadCodecData.Enabled = False
            edtConfigFile.Enabled = False
            cmdCodecDataFileChooser.Enabled = False
        End If
    End Sub

    '
    ' cmdAVIFileChooser_Click
    '
    ' Click event handler for the cmdAVIFileChooser. Opens a file selection box.
    '
    Private Sub cmdAVIFileChooser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAVIFileChooser.Click
        Dim saveFileDialog1 As New SaveFileDialog

        saveFileDialog1.Filter = "avi files (*.avi)|*.avi|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            edtAVIFile.Text = saveFileDialog1.FileName
        End If
    End Sub

    '
    ' cmdStartButton_Click
    '
    ' Start the AVI capturing.
    '
        Private Sub cmdStartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartButton.Click
        If ImagingControl.DeviceValid Then
            ImagingControl.LiveStop()

            _oldSink = ImagingControl.Sink

            ImagingControl.Sink = New TIS.Imaging.MediaStreamSink(DirectCast(CodecBox.SelectedItem, TIS.Imaging.AviCompressor), edtAVIFile.Text)
            ImagingControl.LiveStart()
            cmdStartButton.Enabled = False
            cmdStopButton.Enabled = True
        End If
    End Sub
    
    '
    ' cmdStopButton_Click
    '
    ' Stop the AVI capturing.
    '
    Private Sub cmdStopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStopButton.Click
        If ImagingControl.DeviceValid Then
            ImagingControl.LiveStop()
            ImagingControl.Sink = _oldSink
            cmdStartButton.Enabled = True
            cmdStopButton.Enabled = False
            If _restartVideo = True Then
                ImagingControl.LiveStart()
            End If

        End If
    End Sub

    '
    ' chkPause_Click
    '
    ' Pause or restart the avi capture according to the value in chkPause.
    '
    Private Sub chkCapturePause_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCapturePause.CheckedChanged
        ImagingControl.Sink.SinkModeRunning = Not chkCapturePause.CheckState
    End Sub
    
    '
    ' cmdShowPropPage_Click
    '
    ' Event handler sub of the codec property button. If the current
    ' seleceted codec has a property dialog, the property button is
    ' enabled. A click on this button opens the codec's property dialog.
    '
    Private Sub cmdShowPropPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowPropPage.Click
        Dim Codec As AviCompressor
        Codec = CType(CodecBox.SelectedItem(), AviCompressor)
        Codec.ShowPropertyPage()
    End Sub

    '
    ' cmdCloseButton_Click
    '
    ' Event handler sub of the close button.
    '
    Private Sub cmdCloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCloseButton.Click
        Close()
    End Sub

    '
    ' cmdCodecDataFileChooser_Click
    '
    ' Click event handler for the cmdCodecDataFileChooser button. Opens a file selection box.
    '
    Private Sub cmdCodecDataFileChooser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodecDataFileChooser.Click
        Dim saveFileDialog1 As New SaveFileDialog

        saveFileDialog1.Filter = "codec data files (*.cod)|*.cod|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            edtConfigFile.Text = saveFileDialog1.FileName
        End If
    End Sub

    '
    ' cmdSaveCodecData_Click
    '
    ' Save the configuration properties of the current selected codec to a
    ' binary file.
    '
        Private Sub cmdSaveCodecData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveCodecData.Click
        If edtConfigFile.Text <> "" Then
            Try
                Dim Filestream As New System.IO.FileStream(edtConfigFile.Text, System.IO.FileMode.Create, System.IO.FileAccess.Write)

                ' Create the writer for data.
                Dim BinWriter As New System.IO.BinaryWriter(Filestream)

                ' Write data to Test.data.
                Dim aviComp As AviCompressor
                aviComp = CType(CodecBox.SelectedItem(), AviCompressor)

                BinWriter.Write(aviComp.ToString())
                BinWriter.Write(aviComp.CompressorDataSize)
                BinWriter.Write(aviComp.CompressorData)

                BinWriter.Close()
                Filestream.Close()

            Catch Ex As System.Exception
                MsgBox(Ex.Message, MsgBoxStyle.Critical, "Write error")
            End Try
        End If
    End Sub
    
    '
    ' cmdLoadCodecData_Click
    '
    ' The previously saved codec configuration is loaded from the
    ' file specified in edtConfigFile.Text. The codec configuration
    ' contains the name of the codec. This name is searched in the
    ' codec combobox. If it was found, the codec is selected and the
    ' configuration data is assigned to this codec.
    '
        Private Sub cmdLoadCodecData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadCodecData.Click
        If edtConfigFile.Text <> "" Then
            Try
                ' Create the reader for data.
                Dim Filestrem As New System.IO.FileStream(edtConfigFile.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                Dim BinReader As New System.IO.BinaryReader(Filestrem)

                Dim CodecName As String
                Dim Codec As AviCompressor
                Dim CodecFound As Boolean

                ' Retrieve the name of the codec from the codec configuration file
                CodecName = BinReader.ReadString()

                ' Search this codec in the codec combo box
                CodecFound = False
                For Each item As AviCompressor In CodecBox.Items
                    If item.ToString() = CodecName Then
                        CodecBox.SelectedItem = item
                        Codec = item
                        CodecFound = True
                    End If
                Next

                If CodecFound = True Then
                    Dim codecDataLen As Integer = BinReader.ReadInt32()
                    ' Assign the configuration data to the codec.
                    Codec.CompressorData = BinReader.ReadBytes(codecDataLen)
                Else
                    ' If the codec was not found, display an error message.
                    MsgBox("The codec " + CodecName + " was not found!", MsgBoxStyle.Information, "Load codec configuration")
                End If
                BinReader.Close()
                Filestrem.Close()

            Catch Ex As System.Exception
                MsgBox(Ex.Message, MsgBoxStyle.Critical, "Read error")
            End Try
        End If
    End Sub
    
    '
    ' AviDialog_Closing
    '
    ' When the dialog is closed, the contents of the controls is save to the
    ' registry. This contents will be loaded from the registry, when the 
    ' AVI dialog is opened again.
    '
    Private Sub AviDialog_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        SaveAVIFileInRegisty(edtAVIFile.Text)
        SaveCodecInRegisty(CType(CodecBox.SelectedItem, AviCompressor).Name)
        SaveCodecFileInRegisty(edtConfigFile.Text)
    End Sub
End Class