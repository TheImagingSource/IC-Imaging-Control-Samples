Imports TIS.Imaging

Public Class Form1

    Public WithEvents AbsValCtrl As AbsValSlider
    Public WithEvents MapStringsCtrl As StringCombo
    Public WithEvents SwitchCtrl As Switch
    Public WithEvents RangeCtrl As RangeSlider
    Public WithEvents ButtonCtrl As PushButton

    ''' <summary>
    ''' btnSelectDevice_Click
    '''
    ''' Show the built in device selection dialog of IC Imaging Control. If
    ''' a valid video capture device was selected, query all properties of the
    ''' device. The properties are listed in a tree.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub btnSelectDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDevice.Click
        ' The device settings dialog needs the live mode to be stopped.
        If IcImagingControl1.LiveVideoRunning Then
            IcImagingControl1.LiveStop()
        End If

        ' Show the device settings dialog
        IcImagingControl1.ShowDeviceSettingsDialog()

        ' If no device was selected, exit the program.
        If Not IcImagingControl1.DeviceValid Then
            MessageBox.Show("No device was selected.")
            Close()
            Exit Sub
        End If

        ListAllPropertyItems()

        ' Start live mode
        IcImagingControl1.LiveStart()

        ' Query all properties of the video capture device and list
        ' them in a tree control.
        QueryVCDProperties()
    End Sub
    
    ''' <summary>
    ''' btnShowPage_Click
    '''
    ''' Show the built-in property dialog of IC Imaging Control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub btnShowPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowPage.Click
        If IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowPropertyDialog()
        End If
    End Sub
    
    ''' <summary>
    ''' Form_Load
    '''
    ''' In the Form_Load event it is checked whether a video capture device was
    ''' already specified in the properties of IC Imaging Control. If no device was
    ''' specified, the device selection dialog of IC Imaging Control is shown.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
        Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If IcImagingControl1.DeviceValid = False Then
            ' Show the device settings dialog.
            btnSelectDevice_Click(Me, Nothing)
        End If

        If IcImagingControl1.DeviceValid = True Then

            ' Start live mode.
            IcImagingControl1.LiveStart()

            ' Query all properties of the video capture device and list
            ' them in a tree control.
            QueryVCDProperties()
        End If
    End Sub
    
    ''' <summary>
    ''' QueryVCDProperties
    '''
    ''' QueryVCDProperties clears the tree control and calls QueryVCDPropertyItems
    ''' to retrieve all VCDPropertyItems and insert them into the tree control.
    ''' </summary>
    ''' <remarks></remarks>
        Private Sub QueryVCDProperties()
        ' Erase the complete tree.
        Tree.Nodes.Clear()

        ' Create the root node.
        Dim root As New TreeNode("VCDPropertyItems")
        root.Tag = Nothing
        Tree.Nodes.Add(root)

        ' Query the VCDPropertyItems and fill the tree.
        QueryVCDPropertyItems(root, IcImagingControl1.VCDPropertyItems)

        ' Make sure the tree is expanded
        root.ExpandAll()
    End Sub
    
    ''' <summary>
    ''' QueryVCDPropertyItems
    '''
    ''' Retrieve all availalbe VCDPropertyItems that are contained in the
    ''' VCDPropertyItems collection. For each VCDPropertyItem call the sub
    ''' QueryVCDPropertyElements to retrieve all VCDPropertyElements of the
    ''' current property item.
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="PropertyItems"></param>
    ''' <remarks></remarks>
        Private Sub QueryVCDPropertyItems(ByVal ParentNode As TreeNode, ByVal PropertyItems As TIS.Imaging.VCDPropertyItems)
        ' Iterate through all VCDPropertyItems and insert them into the tree control.
        For Each item As TIS.Imaging.VCDPropertyItem In PropertyItems
            ' Create a new tree node for the property item.
            ' The item identifier string is stored in the tag
            ' property of the tree node.
            Dim newNode As New TreeNode(item.Name, 0, 0)
            newNode.Tag = Nothing

            ParentNode.Nodes.Add(newNode)

            ' Now query the elements of the property item.
            QueryVCDPropertyElements(newNode, item)

        Next
    End Sub
    
    ''' <summary>
    ''' QueryVCDPropertyElements
    '''
    ''' Query all VCDPropertyElements of the passed VCDPropertyItem and insert them
    ''' into the tree control. Then retrieve all interfaces of the VCDPropertyElements
    ''' by a call to QueryVCDPropertyInterface.
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="PropertyItem"></param>
    ''' <remarks></remarks>
        Private Sub QueryVCDPropertyElements(ByVal ParentNode As TreeNode, ByVal PropertyItem As TIS.Imaging.VCDPropertyItem)
        ' generate a display name
        Dim name As String
        Dim Image As Integer
        Dim Element As TIS.Imaging.VCDPropertyElement

        For Each Element In PropertyItem.Elements
            ' Create a name for the property element.
            Select Case Element.ElementGUID
                Case VCDGUIDs.VCDElement_Value
                    name = "VCDElement_Value"
                    Image = 1
                Case VCDGUIDs.VCDElement_Auto
                    name = "VCDElement_Auto"
                    Image = 2
                Case VCDGUIDs.VCDElement_OnePush
                    name = "VCDElement_OnePush"
                    Image = 3
                Case VCDGUIDs.VCDElement_WhiteBalanceRed
                    name = "VCDElement_WhiteBalanceRed"
                    Image = 4
                Case VCDGUIDs.VCDElement_WhiteBalanceBlue
                    name = "VCDElement_WhiteBalanceBlue"
                    Image = 4
                Case Else
                    name = "Other Element ID"
                    Image = 4
            End Select

            ' Create a tree node for the element and insert it into the tree control.
            ' The item and element identifier strings are stored in the tag property
            ' of the tree node.
            Dim newNode As New TreeNode(name + ": " + Element.Name, Image, Image)
            newNode.Tag = Nothing

            ParentNode.Nodes.Add(newNode)

            ' Now query the interfaces of the property element.
            QueryVCDPropertyInterface(newNode, Element)

        Next

    End Sub
    
    ''' <summary>
    ''' QueryVCDPropertyInterface
    '''
    ''' Query all interfaces of the passed VCDPropertyElement. The found VCDPropertyInterfaces
    ''' are inserted into the tree control.
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="PropertyElement"></param>
    ''' <remarks></remarks>
        Private Sub QueryVCDPropertyInterface(ByVal ParentNode As TreeNode, ByVal PropertyElement As TIS.Imaging.VCDPropertyElement)
        Dim Name As String
        Dim image As Integer
        Dim Itf As TIS.Imaging.VCDPropertyInterface

        ' Iterate through all VCDPropertyInterfaces of the passed VCDPropertyElement
        ' and insert them into the tree control.
        For Each Itf In PropertyElement
            ' Create an appropriate interface name.
            Select Case Itf.InterfaceGUID
                Case VCDGUIDs.VCDInterface_AbsoluteValue
                    Name = "AbsoluteValue"
                    image = 4
                Case VCDGUIDs.VCDInterface_MapStrings
                    Name = "MapStrings"
                    image = 6
                Case VCDGUIDs.VCDInterface_Range
                    Name = "Range"
                    image = 4
                Case VCDGUIDs.VCDInterface_Switch
                    Name = "Switch"
                    image = 5
                Case VCDGUIDs.VCDInterface_Button
                    Name = "Button"
                    image = 3
            End Select

            ' Insert the node.
            ' The item, element and interface identifier strings are stored in the tag property
            ' of the tree node.
            Dim newNode As New TreeNode(Name, image, image)
            newNode.Tag = Itf.Parent.Parent.ItemGUID.ToString() + ":" + Itf.Parent.ElementGUID.ToString() + ":" + Itf.InterfaceGUID.ToString()

            ParentNode.Nodes.Add(newNode)
        Next
    End Sub
    
        Private Sub Tree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Tree.AfterSelect

        ' If the Tag property is empty, no leaf node was selected
        If Tree.SelectedNode.Tag Is Nothing Then Exit Sub

        If Not RangeCtrl Is Nothing Then RangeCtrl.Dispose()
        If Not SwitchCtrl Is Nothing Then SwitchCtrl.Dispose()
        If Not MapStringsCtrl Is Nothing Then MapStringsCtrl.Dispose()
        If Not ButtonCtrl Is Nothing Then ButtonCtrl.Dispose()
        If Not AbsValCtrl Is Nothing Then AbsValCtrl.Dispose()

        Dim itfPath As String = Tree.SelectedNode.Tag
        Dim itf As TIS.Imaging.VCDPropertyInterface = IcImagingControl1.VCDPropertyItems.FindInterface(itfPath)

        If Not itf Is Nothing Then

            itf.Update()

            ' Show the control group matching the type of the selected interface
            ' and initialize it
            Select Case itf.InterfaceGUID
                Case VCDGUIDs.VCDInterface_AbsoluteValue
                    ShowAbsoluteValueControl(itf)

                Case VCDGUIDs.VCDInterface_MapStrings
                    ShowComboBoxControl(itf)

                Case VCDGUIDs.VCDInterface_Range
                    ShowRangeControl(itf)

                Case VCDGUIDs.VCDInterface_Switch
                    ShowSwitchControl(itf)

                Case VCDGUIDs.VCDInterface_Button
                    ShowButtonControl(itf)

            End Select

        End If

    End Sub
    
    ''' <summary>
    ''' Show...Control
    '''
    ''' The following subs show the matching user control to an passed interface. The subs
    ''' are called from the case statement in the Tree_NodeClick sub.
    ''' The user controls are saved in AbsValSlider.vb, PushButton.vb, RangeSlider.vb,
    ''' StringCombo.vb and Switch.vb.
    ''' Each user control is surrounded by a frame. The frame's caption will be set to
    ''' the property item's name. The property item's name is stored in the
    ''' parent.parent.name property of the interface. parent.parent.name returns e.g.
    ''' "Brightness" or "Exposure".
    ''' </summary>
    ''' <param name="itf"></param>
    ''' <remarks></remarks>
        Private Sub ShowAbsoluteValueControl(ByVal itf As TIS.Imaging.VCDPropertyInterface)
        AbsValCtrl = New AbsValSlider()
        CtrlFrame.Controls.Add(AbsValCtrl)
        AbsValCtrl.SetBounds(20, 20, 500, 27)
        AbsValCtrl.AssignItf(itf)
        CtrlFrame.Text = itf.Parent.Parent.Name + ": " + itf.Parent.Name ' Property item name
    End Sub
    
    Private Sub ShowRangeControl(ByVal itf As TIS.Imaging.VCDPropertyInterface)
        RangeCtrl = New RangeSlider()
        CtrlFrame.Controls.Add(RangeCtrl)
        RangeCtrl.SetBounds(20, 20, 500, 27)
        RangeCtrl.AssignItf(itf)
        CtrlFrame.Text = itf.Parent.Parent.Name + ": " + itf.Parent.Name ' Property item name
    End Sub

    Private Sub ShowComboBoxControl(ByVal itf As TIS.Imaging.VCDPropertyInterface)
        MapStringsCtrl = New StringCombo()
        CtrlFrame.Controls.Add(MapStringsCtrl)
        MapStringsCtrl.SetBounds(20, 20, 500, 27)
        MapStringsCtrl.AssignItf(itf)
        CtrlFrame.Text = itf.Parent.Parent.Name + ": " + itf.Parent.Name ' Property item name
    End Sub

    Private Sub ShowSwitchControl(ByVal itf As TIS.Imaging.VCDPropertyInterface)
        SwitchCtrl = New Switch()
        CtrlFrame.Controls.Add(SwitchCtrl)
        SwitchCtrl.SetBounds(20, 20, 500, 27)
        SwitchCtrl.AssignItf(itf)
        CtrlFrame.Text = itf.Parent.Parent.Name ' Property item name
    End Sub

    Private Sub ShowButtonControl(ByVal itf As TIS.Imaging.VCDPropertyInterface)
        ButtonCtrl = New PushButton()
        CtrlFrame.Controls.Add(ButtonCtrl)
        ButtonCtrl.SetBounds(20, 20, 500, 27)
        ButtonCtrl.AssignItf(itf)
        CtrlFrame.Text = itf.Parent.Parent.Name ' Property item name
    End Sub

    ''' <summary>
    ''' ListAllPropertyItems
    '''
    ''' This sub builds an item - element - lists all names and values of the properties in the
    ''' debug window. It shows, how to enumerate all properties, elements and interfaces. The
    ''' interfaces have to be "casted" to the apropriate interface types like range, absolute
    ''' value etc. to get a correct access to the current interface's properties.
    ''' </summary>
    ''' <remarks></remarks>
        Private Sub ListAllPropertyItems()
        Dim PropertyItem As TIS.Imaging.VCDPropertyItem
        Dim PropertyElement As TIS.Imaging.VCDPropertyElement
        Dim PropertyInterFace As TIS.Imaging.VCDPropertyInterface ' Default interface type.

        ' Interface types for the different property interfaces.
        Dim Range As TIS.Imaging.VCDRangeProperty
        Dim Switch As TIS.Imaging.VCDSwitchProperty
        Dim AbsoluteValue As TIS.Imaging.VCDAbsoluteValueProperty
        Dim MapString As TIS.Imaging.VCDMapStringsProperty
        Dim Button As TIS.Imaging.VCDButtonProperty

        ' Get all property items
        For Each PropertyItem In IcImagingControl1.VCDPropertyItems
            System.Diagnostics.Debug.WriteLine(PropertyItem.Name)

            ' Get all property elements of the current property item.
            For Each PropertyElement In PropertyItem.Elements
                System.Diagnostics.Debug.WriteLine("    Element : " + PropertyElement.Name)

                ' Get all interfaces of the current property element.
                For Each PropertyInterFace In PropertyElement
                    System.Diagnostics.Debug.Write("        Interface ")

                    Try

                        ' Cast the current interface into the apropriate type to access
                        ' the special interface properties.
                        Select Case PropertyInterFace.InterfaceGUID
                            Case VCDGUIDs.VCDInterface_AbsoluteValue
                                AbsoluteValue = PropertyInterFace
                                System.Diagnostics.Debug.WriteLine("Absolut Value : " + AbsoluteValue.Value.ToString)

                            Case VCDGUIDs.VCDInterface_MapStrings
                                MapString = PropertyInterFace
                                System.Diagnostics.Debug.WriteLine("Mapstring : " + MapString.String)

                            Case VCDGUIDs.VCDInterface_Switch
                                Switch = PropertyInterFace
                                System.Diagnostics.Debug.WriteLine("Switch : " + Switch.Switch.ToString)

                            Case VCDGUIDs.VCDInterface_Button
                                Button = PropertyInterFace
                                System.Diagnostics.Debug.WriteLine("Button")

                            Case VCDGUIDs.VCDInterface_Range
                                Range = PropertyInterFace
                                System.Diagnostics.Debug.WriteLine("Range : " + Range.Value.ToString)
                        End Select
                    Catch ex As System.Exception
                        System.Diagnostics.Debug.WriteLine("<error>")
                    End Try
                Next
            Next
        Next
    End Sub
    
End Class
