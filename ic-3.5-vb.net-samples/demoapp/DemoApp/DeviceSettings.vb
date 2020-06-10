Module DeviceSettings

    ' Constants for Settings dialog box
    Const REG_APP_NAME = "TIS VB .NET DemoApp"
    Const REG_SECTION = "Settings"
    Const REG_KEY_VIDEO_SOURCE = "VideoSource"
    Const REG_KEY_VIDEO_NORM = "VideoNorm"
    Const REG_KEY_INPUT_CHANNEL = "InputChannel"
    Const REG_KEY_VIDEO_FORMAT = "VideoFormat"
    Const REG_KEY_WINDOW_SIZE = "Adjust Window Size"
    Const REG_KEY_FRAME_RATE = "Frame Rate"
    Const REG_KEY_CODEC = "Codec"
    Const REG_KEY_CODECDATA = "Codec Data"
    Const REG_KEY_AVIFILE = "AVI File"

    '
    ' CurrentProcessName
    '
    ' Return the name of the current process. This fuction is used in the
    ' GetSetting and SaveSetting function as section parameter
    ' This makes this module useable for other applications too.
    '
    Function CurrentProcessName()
        CurrentProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName
    End Function
    '
    ' OpenSelectedDevice
    '
    ' Open the device that has be stored in the registry last time, the
    ' device selection dialog has been used. Call this sub in the beginning
    ' of an application to restore the last used video capture device.
    '
    Function OpenSelectedDevice(ByVal IC15Control As TIS.Imaging.ICImagingControl) As Boolean
        On Error GoTo err_OpenSelectedDevice
        Dim VideoSource As String

        With IC15Control
            .Device = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_SOURCE, "")

            ' Restore settings
            If .VideoNormAvailable Then
                .VideoNorm = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_NORM, "")
            End If
            If .InputChannelAvailable Then
                .InputChannel = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_INPUT_CHANNEL, "")
            End If
            .VideoFormat = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_FORMAT, "")

            If .DeviceFrameRateAvailable Then
                .DeviceFrameRate = CSng(GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_FRAME_RATE, ""))
            End If
            ' No error occured, and device is present. Indicate success
            OpenSelectedDevice = True
        End With

        Exit Function

err_OpenSelectedDevice:
        Exit Function
    End Function

    '
    ' SaveSelectedDevice
    '
    ' Saves the device settings from IC15Control to the registry
    '
    Sub SaveSelectedDevice(ByVal IC15Control As TIS.Imaging.ICImagingControl)
        On Error GoTo err_SaveSelectedDevice
        With IC15Control
            If .DeviceValid Then
                SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_SOURCE, .Device)
                If .InputChannelAvailable Then
                    SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_INPUT_CHANNEL, .InputChannel)
                End If
                If .VideoNormAvailable Then
                    SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_NORM, .VideoNorm)
                End If

                SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_VIDEO_FORMAT, .VideoFormat)

                If .DeviceFrameRateAvailable Then
                    SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_FRAME_RATE, CStr(.DeviceFrameRate))
                End If

            End If
        End With
err_SaveSelectedDevice:
    End Sub

    '
    ' GetCodecFromRegisty
    '
    ' Retrieve the last used codec from the registry.
    '
    Function GetCodecFromRegisty() As String
        GetCodecFromRegisty = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_CODEC, "")
    End Function

    '
    ' SaveCodecInRegisty
    '
    ' Save the last used codec  file in the registry.
    '
    Sub SaveCodecInRegisty(ByVal Codec As String)
        SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_CODEC, Codec)
    End Sub

    '
    ' GetCodecFileFromRegisty
    '
    ' Retrieve the last used codec configuration file from the registry.
    '
    Function GetCodecFileFromRegisty() As String
        GetCodecFileFromRegisty = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_CODECDATA, "new.cod")
    End Function

    '
    ' SaveCodecFileInRegisty
    '
    ' Save the last used codec configuration file to the registry.
    '
    Sub SaveCodecFileInRegisty(ByVal FileName As String)
        SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_CODECDATA, FileName)
    End Sub

    '
    ' GetAVIFileFromRegisty
    '
    ' Retrieve the last used AVI file from the registry.
    '
    Function GetAVIFileFromRegisty() As String
        GetAVIFileFromRegisty = GetSetting(CurrentProcessName(), REG_SECTION, REG_KEY_AVIFILE, "new.avi")
    End Function

    '
    ' SaveAVIFileInRegisty
    '
    ' Retrieve the last used AVI file from the registry
    '
    Sub SaveAVIFileInRegisty(ByVal FileName As String)
        SaveSetting(CurrentProcessName(), REG_SECTION, REG_KEY_AVIFILE, FileName)
    End Sub

End Module
