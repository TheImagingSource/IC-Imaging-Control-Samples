
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Imports TIS.Imaging


Public Class Form1
    Private _imageSink As FrameSnapSink

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _imageSink = New FrameSnapSink(TIS.Imaging.MediaSubtypes.RGB32)

        IcImagingControl1.Sink = _imageSink
        LoadLastUsedDevice()
        UpdateControls()
    End Sub

    ''' <summary>
    ''' exitToolStripMenuItem_Click
    '''
    ''' Exit the demo application
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub exitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitToolStripMenuItem.Click
        Close()
    End Sub

    ''' <summary>
    ''' deviceToolStripMenuItem_Click
    '''
    ''' Show the device selection dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub deviceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deviceToolStripMenuItem.Click
        SelectDevice()
    End Sub


    ''' <summary>
    ''' playToolStripMenuItem_Click
    '''
    ''' Handler for the play menu item. StartLiveVideo() sub is called.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub playToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles playToolStripMenuItem.Click
        StartLiveVideo()
    End Sub

    ''' <summary>
    ''' stopToolStripMenuItem_Click
    '''
    ''' Stop the live video display by a call to StopLiveVideo() sub.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub stopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stopToolStripMenuItem.Click
        StopLiveVideo()
    End Sub


    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        IcImagingControl1.Size = ClientSize
    End Sub

    ''' <summary>
    ''' imageToolStripMenuItem_Click
    '''
    ''' Show the video and camera properties dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imageToolStripMenuItem.Click
        ShowProperties()
    End Sub

    ''' <summary>
    ''' writeAviToolStripMenuItem_Click
    '''
    ''' Show the AVI capture dialog.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub writeAviToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles writeAviToolStripMenuItem.Click
        ShowAVIDialog()
    End Sub

    ''' <summary>
    ''' saveImageToolStripMenuItem_Click
    '''
    ''' Handler for the SaveImage menu. SaveImage() Sub is called.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub saveImageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveImageToolStripMenuItem.Click
        SaveImage()
    End Sub

    Private Sub externalTriggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles externalTriggerToolStripMenuItem.Click
        ToggleTrigger()
    End Sub

    Private Sub UpdateControls()

        Dim IsDeviceValid As Boolean = False

        IsDeviceValid = IcImagingControl1.DeviceValid
        imageToolStripMenuItem.Enabled = IsDeviceValid
        writeAviToolStripMenuItem.Enabled = IsDeviceValid
        saveImageToolStripMenuItem.Enabled = IsDeviceValid
        playToolStripMenuItem.Enabled = IsDeviceValid
        stopToolStripMenuItem.Enabled = IsDeviceValid
        ToolbarPlayButton.Enabled = IsDeviceValid
        ToolBarStopButton.Enabled = IsDeviceValid

        externalTriggerToolStripMenuItem.Enabled = False
        ToolBarTriggerButton.Enabled = False


        If IsDeviceValid = True Then

            playToolStripMenuItem.Enabled = Not IcImagingControl1.LiveVideoRunning
            stopToolStripMenuItem.Enabled = Not playToolStripMenuItem.Enabled
            ToolbarPlayButton.Enabled = Not IcImagingControl1.LiveVideoRunning()
            ToolBarStopButton.Enabled = Not ToolbarPlayButton.Enabled

            If (IcImagingControl1.DeviceTriggerAvailable) Then

                externalTriggerToolStripMenuItem.Enabled = True
                ToolBarTriggerButton.Enabled = True
                externalTriggerToolStripMenuItem.Checked = IcImagingControl1.DeviceTrigger
                ToolBarTriggerButton.Checked = IcImagingControl1.DeviceTrigger
            End If

            If (IcImagingControl1.InputChannelAvailable) Then

                ''Create the sub menus that allow to select the input channels.
                ToolBarInputChannel.DropDownItems.Clear()
                inputChannelsToolStripMenuItem.DropDown.Items.Clear()
                inputChannelsToolStripMenuItem.Enabled = True
                ToolBarInputChannel.Enabled = True

                Dim s As InputChannel
                For Each s In IcImagingControl1.InputChannels

                    Dim mitem As System.Windows.Forms.ToolStripItem = Nothing

                    ' Add the input channel as menu item to the main menu
                    mitem = inputChannelsToolStripMenuItem.DropDownItems.Add(s.ToString())

                    If IcImagingControl1.InputChannel = s.ToString() Then

                        Dim i As ToolStripMenuItem = DirectCast(mitem, ToolStripMenuItem)
                        i.Checked = True

                    End If

                    AddHandler mitem.Click, AddressOf mnuInputChannelChild_Click

                    ' Add the input channel as menu item to the toolbar button's context menu
                    mitem = ToolBarInputChannel.DropDown.Items.Add(s.ToString())

                    If (IcImagingControl1.InputChannel = s.ToString()) Then


                        Dim i As ToolStripMenuItem = DirectCast(mitem, ToolStripMenuItem)
                        i.Checked = True

                    End If

                    AddHandler mitem.Click, AddressOf mnuInputChannelChild_Click

                Next
            Else

                ' Remove the input channels from the submenus.
                ToolBarInputChannel.DropDownItems.Clear()
                inputChannelsToolStripMenuItem.DropDown.Items.Clear()
                inputChannelsToolStripMenuItem.Enabled = False
                ToolBarInputChannel.Enabled = False

            End If

            ToolBarSnapButton.Enabled = IsDeviceValid
            ToolBarAVIButton.Enabled = IsDeviceValid
            ToolBarPropertiesButton.Enabled = IsDeviceValid
        End If

    End Sub

    Private Sub toolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolStrip1.ItemClicked
        ' Evaluate the Button property to determine which button was clicked.
        Select Case (toolStrip1.Items.IndexOf(e.ClickedItem))

            Case 0
                StartLiveVideo()
            Case 1
                StopLiveVideo()
            Case 3
                SaveImage()
            Case 4
                ShowAVIDialog()
            Case 6
                SelectDevice()
            Case 7
                ShowProperties()
            Case 10
                ToggleTrigger()
        End Select
    End Sub

    ''' <summary>
    ''' StartLiveVideo
    '''
    ''' Start the live video display and change the button states of the
    ''' play and stop button.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartLiveVideo()

        If IcImagingControl1.DeviceValid Then

            IcImagingControl1.LiveStart()
            playToolStripMenuItem.Enabled = False
            stopToolStripMenuItem.Enabled = True
            ToolbarPlayButton.Enabled = False
            ToolBarStopButton.Enabled = True
            ToolBarSnapButton.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' StopLiveVideo
    '''
    ''' Stop the live video display and change the button states of the
    ''' play and stop button.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StopLiveVideo()

        If IcImagingControl1.DeviceValid Then

            IcImagingControl1.LiveStop()
            playToolStripMenuItem.Enabled = True
            stopToolStripMenuItem.Enabled = False
            ToolbarPlayButton.Enabled = True
            ToolBarStopButton.Enabled = False
            ToolBarSnapButton.Enabled = False

        End If

    End Sub

    ''' <summary>
    ''' SelectDevice
    '''
    ''' Show the device selection dialog.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectDevice()

        If IcImagingControl1.LiveVideoRunning Then

            IcImagingControl1.LiveStop()
            IcImagingControl1.ShowDeviceSettingsDialog()

            If (IcImagingControl1.DeviceValid) Then
                IcImagingControl1.LiveStart()
            End If


        Else
            IcImagingControl1.ShowDeviceSettingsDialog()

            UpdateControls()
            SetWindowSizeToImagesize()

            DeviceSettings.SaveSelectedDevice(IcImagingControl1)

        End If

    End Sub

    ''' <summary>
    ''' ShowProperties
    '''
    ''' Show the property dialog of the current video capture device.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowProperties()

        If IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowPropertyDialog()
            UpdateControls()
        End If

    End Sub

    ''' <summary>
    ''' SaveImage
    '''
    ''' Snap (capture) an image from the video stream and save it to harddisk.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveImage()

        If IcImagingControl1.DeviceValid Then

            Try
                Dim image As IFrameQueueBuffer
                ' Snap (capture) an image to the memory
                image = _imageSink.SnapSingle(TimeSpan.FromSeconds(1))

                ' Call the save file dialog to enter the file name of the image
                Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()

                saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*"
                saveFileDialog1.FilterIndex = 1
                saveFileDialog1.RestoreDirectory = True

                If saveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    ' Save the image.
                    image.SaveAsBitmap(saveFileDialog1.FileName)
                End If
            Catch
                MessageBox.Show("Snap Image failed, timeout occurred.")
            End Try


        End If

    End Sub

    ''' <summary>
    ''' ShowAVIDialog
    '''
    ''' Show the dialog for AVI capture
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowAVIDialog()

        If (icImagingControl1.DeviceValid) Then

            Dim AVIDlg As AviDialog = New AviDialog(IcImagingControl1)
            AVIDlg.ShowDialog()
            AVIDlg.Dispose()
            AVIDlg = Nothing

        End If

    End Sub


    ''' <summary>
    ''' ToggleTrigger
    '''
    ''' Enable or disable the external trigger.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleTrigger()

        If IcImagingControl1.DeviceValid Then

            If (IcImagingControl1.DeviceTriggerAvailable) Then

                Dim trigger As Boolean = Not IcImagingControl1.DeviceTrigger

                If (IcImagingControl1.LiveVideoRunning) Then

                    IcImagingControl1.LiveStop()
                    IcImagingControl1.DeviceTrigger = trigger
                    IcImagingControl1.LiveStart()

                Else

                    IcImagingControl1.DeviceTrigger = trigger
                End If

                externalTriggerToolStripMenuItem.Checked = trigger
                ToolBarTriggerButton.Checked = trigger

            End If
        End If
    End Sub

    ''' <summary>
    ''' SetWindowSizeToImagesize
    '''
    ''' Resize the application to the size of the video format.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWindowSizeToImagesize()
        If IcImagingControl1.DeviceValid Then
            ' Change the witdth to a minimum size of 230 pixel. If the width is
            ' less than 230 pixel, the toolbar can not be seen completely.
            Width = Math.Max(IcImagingControl1.LiveDisplayOutputSize.Width + Width - ClientSize.Width, 230)
            Height = IcImagingControl1.LiveDisplayOutputSize.Height + Height - ClientSize.Height + toolStrip1.Size.Height
        End If
    End Sub


    Private Sub mnuInputChannelChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim mitem As System.Windows.Forms.ToolStripMenuItem = Nothing
        mitem = DirectCast(sender, System.Windows.Forms.ToolStripMenuItem)

        Dim Item As System.Windows.Forms.ToolStripMenuItem

        For Each Item In inputChannelsToolStripMenuItem.DropDownItems
            If Item.Text = mitem.Text Then
                Item.Checked = True
            Else
                Item.Checked = False
            End If
        Next

        For Each Item In ToolBarInputChannel.DropDown.Items
            If Item.Text = mitem.Text Then
                Item.Checked = True
            Else
                Item.Checked = False
            End If
        Next

        IcImagingControl1.InputChannel = mitem.Text
        ' Save the new input channel to the registry.
        DeviceSettings.SaveSelectedDevice(IcImagingControl1)

    End Sub

    Private Sub LoadLastUsedDevice()

        DeviceSettings.OpenSelectedDevice(IcImagingControl1)
        SetWindowSizeToImagesize()

    End Sub

End Class
