# Import IC Capture Configuration File (ICCF)

This code snippet imports IC Capture Configuration files (ICCF) to IC Imaging Control. The ICCF files differ extremely from the XML format "LoadDeviceStatefromFile()" expects. Therefore, an importer is necessary.

The file "ICCFImport.cs" contains the import function itself. Returned is a string, that contains XML data, which is read by "LoadDeviceState()" (not from file)

It is used as follows:

``` C#
icImagingControl1.LiveStop();
try
{
    icImagingControl1.LoadDeviceState(ICCFImport.ICCFImport.Import("cameras.iccf"), true);
}
catch (ICException IEx)
{
    MessageBox.Show("Import of ICCF file failed:\n" + IEx.Message, "Import IC Capture File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
```
In case, there is more than one camera specified in the ICCF file, a camera name plus serial number can passed:

``` C#
icImagingControl1.LiveStop();
try
{
    icImagingControl1.LoadDeviceState(ICCFImport.ICCFImport.Import("cameras.iccf", "DFK 33UX264 23040123"), true);
}
catch (ICException IEx)
{
    MessageBox.Show("Import of ICCF file failed:\n" + IEx.Message, "Import IC Capture File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
```

The "SelectDeviceDlg*.cs" files show a dialog for camera selection from the ICCF file. It is used as:

``` C#
ICCFImport.SelectDeviceDlg DevDlg = new ICCFImport.SelectDeviceDlg("cameras.iccf");
if (DevDlg.ShowDialog(this) == DialogResult.OK)
{
    try
    {
        icImagingControl1.LoadDeviceState(ICCFImport.ICCFImport.Import("cameras.iccf", DevDlg.Selected), true);
    }
    catch (ICException IEx)
    {
        MessageBox.Show("Import of ICCF file failed:\n" + IEx.Message, "Import IC Capture File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
``` 

## Limitations
- ROIs are only considered, if the sensor of the camera supports this in hardware. Most of our cameras do that.
- Rotation and flip are ignored, because the importer does not load the "rotateflip" filter.
