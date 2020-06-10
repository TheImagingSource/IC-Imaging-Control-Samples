Public Class Form1

    Dim modulePathCollection As System.Collections.Specialized.StringCollection

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
                If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()
            If Not IcImagingControl1.DeviceValid Then
                Close()
                Return
            End If
        End If

        ' Disable all overlays, so that they do not influence the
        ' color format of the image stream.
        IcImagingControl1.OverlayBitmapPosition = TIS.Imaging.PathPositions.None

        ' Start live mode.
        IcImagingControl1.LiveStart()
        
                ' Use a collection to save the full paths to the filter modules.
        modulePathCollection = New System.Collections.Specialized.StringCollection

        ' For each filter info:
        ' - Check if the filter's path is already in the module paths collection
        ' - If not, add the module name to the filter module list box.
        For Each ffi As TIS.Imaging.FrameFilterInfo In IcImagingControl1.FrameFilterInfos
            If modulePathCollection.IndexOf(ffi.ModulePath) < 0 Then
                lstFrameFilterModules.Items.Add(ffi.ModuleName)
                modulePathCollection.Add(ffi.ModulePath)
            End If
        Next
            End Sub

    Private Sub lstFrameFilterModules_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFrameFilterModules.SelectedIndexChanged
        ' Get the full path to the selected module from the ModulePaths collection.
        If lstFrameFilterModules.SelectedIndex >= 0 Then
            Dim selectedModulePath As String = modulePathCollection(lstFrameFilterModules.SelectedIndex)

            lstFrameFilters.Items.Clear()
            For Each ffi As TIS.Imaging.FrameFilterInfo In IcImagingControl1.FrameFilterInfos
                If ffi.ModulePath = selectedModulePath Then
                    lstFrameFilters.Items.Add(ffi)
                End If
            Next
        End If
    End Sub
    
    Private Sub btnDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDevice.Click
        ' If live mode is active, stop.
        Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
        If wasLive Then
            IcImagingControl1.LiveStop()
        End If

        ' Show device selection dialog.
        IcImagingControl1.ShowDeviceSettingsDialog()

        ' If live mode was active, restart.
        If wasLive Then
            IcImagingControl1.LiveStart()
        End If
    End Sub

    Private Sub btnProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProperties.Click
        IcImagingControl1.ShowPropertyDialog()
    End Sub

    Private Sub btnDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDialog.Click
        IcImagingControl1.DeviceFrameFilters(0).ShowDialog()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        ' If live mode is active, stop.
        Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
        If wasLive Then
            IcImagingControl1.LiveStop()
        End If

        ' Show device selection dialog.
        IcImagingControl1.DeviceFrameFilters.Clear()

        ' If live mode was active, restart.
        If wasLive Then
            IcImagingControl1.LiveStart()
        End If

        lblSelectedFilter.Text = "<no filter>"
        btnDialog.Enabled = False
        btnRemove.Enabled = False
        lstFrameFilters.SelectedItem = Nothing
    End Sub

    Private Sub btnStartLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartLive.Click
        IcImagingControl1.LiveStart()
    End Sub

    Private Sub btnStopLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStopLive.Click
        IcImagingControl1.LiveStop()
    End Sub


    Private Sub lstFrameFilters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFrameFilters.SelectedIndexChanged
                ' Get the selected FrameFilterInfo object 
        Dim ffi As TIS.Imaging.FrameFilterInfo = CType(lstFrameFilters.SelectedItem, TIS.Imaging.FrameFilterInfo)

        If Not ffi Is Nothing Then
            ' Create the new FrameFilter instance.
            Dim newFrameFilter As TIS.Imaging.FrameFilter = IcImagingControl1.FrameFilterCreate(ffi)
            
                        ' If live mode is active, stop.
            Dim wasLive As Boolean = IcImagingControl1.LiveVideoRunning
            If wasLive Then
                IcImagingControl1.LiveStop()
            End If

            ' Set the new frame filter.
            IcImagingControl1.DeviceFrameFilters.Clear()
            IcImagingControl1.DeviceFrameFilters.Add(newFrameFilter)

            ' If live mode was active, restart.
            If wasLive Then
                IcImagingControl1.LiveStart()
            End If
                        lblSelectedFilter.Text = newFrameFilter.Name
            btnDialog.Enabled = newFrameFilter.HasDialog
            btnRemove.Enabled = True
        End If
    End Sub
End Class
