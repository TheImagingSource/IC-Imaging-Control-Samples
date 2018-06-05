using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICCFImport
{
    public partial class SelectDeviceDlg : Form
    {
        public string Selected
        {
            get
            {
                return lbDevices.Items[lbDevices.SelectedIndex].ToString();
            }
        }
        public SelectDeviceDlg(string ICCFFileName)
        {
            InitializeComponent();
            List<string> Devices = ICCFImport.GetDeviceList(ICCFFileName);

            foreach (string dev in Devices)
            {
                lbDevices.Items.Add(dev);
            }
            lbDevices.SelectedIndex = 0;
        }

        private void SelectDeviceDlg_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
