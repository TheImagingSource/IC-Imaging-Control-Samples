Public Class SaveVideoForm

    Private m_ImagingControl As TIS.Imaging.ICImagingControl
    Private m_OldSink As TIS.Imaging.BaseSink
    Private m_OldLiveMode As Boolean
    Private m_Sink As TIS.Imaging.MediaStreamSink

    Public Sub New(ByVal ic As TIS.Imaging.ICImagingControl)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_ImagingControl = ic
    End Sub

        Private Sub SaveVideoForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboMediaStreamContainer.DataSource = TIS.Imaging.MediaStreamContainer.MediaStreamContainers
        cboVideoCodec.DataSource = TIS.Imaging.AviCompressor.AviCompressors

        txtFileName.Text = System.IO.Path.ChangeExtension("video.avi", CurrentMediaStreamContainer.PreferredFileExtension)

        btnStopCapture.Enabled = False
    End Sub
    
    Private ReadOnly Property CurrentMediaStreamContainer() As TIS.Imaging.MediaStreamContainer
        Get
            CurrentMediaStreamContainer = cboMediaStreamContainer.SelectedItem
        End Get
    End Property

    Private ReadOnly Property CurrentVideoCodec()
        Get
            If CurrentMediaStreamContainer.IsCustomCodecSupported Then
                CurrentVideoCodec = cboVideoCodec.SelectedItem
            Else
                CurrentVideoCodec = Nothing
            End If
        End Get
    End Property

    Private Sub cboMediaStreamContainer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMediaStreamContainer.SelectedIndexChanged
        If CurrentMediaStreamContainer.IsCustomCodecSupported Then
            cboVideoCodec.Enabled = True
            if not CurrentVideoCodec = nothing then 
                btnProperties.Enabled = CurrentVideoCodec.PropertyPageAvailable
            Else
                btnProperties.Enabled = False
            End If
        Else
            cboVideoCodec.Enabled = False
            btnProperties.Enabled = False
        End If

        txtFileName.Text = System.IO.Path.ChangeExtension(txtFileName.Text, CurrentMediaStreamContainer.PreferredFileExtension)
    End Sub

    Private Sub cboVideoCodec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVideoCodec.SelectedIndexChanged
        btnProperties.Enabled = CurrentVideoCodec.PropertyPageAvailable
    End Sub


    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click
        CurrentVideoCodec.ShowPropertyPage()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim dlg As New SaveFileDialog
        dlg.AddExtension = True

        Dim ext As String = CurrentMediaStreamContainer.PreferredFileExtension
        dlg.DefaultExt = ext
        dlg.Filter = CurrentMediaStreamContainer.Name + " Video Files (*." + ext + ")|*." + ext + "||"

        If dlg.ShowDialog() = DialogResult.OK Then
            txtFileName.Text = dlg.FileName
        End If
    End Sub

        Private Sub btnStartCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartCapture.Click
        m_Sink = New TIS.Imaging.MediaStreamSink
        m_Sink.StreamContainer = CurrentMediaStreamContainer
        m_Sink.Codec = CurrentVideoCodec
        m_Sink.Filename = txtFileName.Text
        m_Sink.SinkModeRunning = Not chkPause.Checked

        m_OldLiveMode = m_ImagingControl.LiveVideoRunning
        m_OldSink = m_ImagingControl.Sink

        m_ImagingControl.LiveStop()

        m_ImagingControl.Sink = m_Sink

        m_ImagingControl.LiveStart()

        btnStartCapture.Enabled = False
        btnStopCapture.Enabled = True
        btnClose.Enabled = False
    End Sub
    
        Private Sub chkPause_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPause.CheckedChanged
        If Not m_Sink Is Nothing Then
            m_Sink.SinkModeRunning = Not chkPause.Checked
        End If
    End Sub
    
    Private Sub btnStopCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopCapture.Click
        m_ImagingControl.LiveStop()

        chkPause.Checked = False
        btnStartCapture.Enabled = True
        btnStopCapture.Enabled = False
        btnClose.Enabled = True

        m_ImagingControl.Sink = m_OldSink

        If m_OldLiveMode Then m_ImagingControl.LiveStart()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class