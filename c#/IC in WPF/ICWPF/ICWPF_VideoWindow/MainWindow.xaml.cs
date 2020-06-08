using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TIS.Imaging;
namespace ICWPF_VideoWindow
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private TIS.Imaging.ICImagingControl _ic = new TIS.Imaging.ICImagingControl();

		public MainWindow()
		{
			InitializeComponent();
            _ic.Sink = new FrameQueueSink(ShowBuffer, MediaSubtypes.RGB32, 5);
		}

        private FrameQueuedResult ShowBuffer(IFrameQueueBuffer buffer)
        {
            videoWindow1.UpdateImage(buffer);
            return FrameQueuedResult.ReQueue;
        }

		private void chkFlipH_Checked(object sender, RoutedEventArgs e)
		{
			videoWindow1.FlipH = chkFlipH.IsChecked.Value;
		}

		private void chkFlipV_Checked(object sender, RoutedEventArgs e)
		{
			videoWindow1.FlipV = chkFlipV.IsChecked.Value;
		}

		private void chkFlipH_Unchecked(object sender, RoutedEventArgs e)
		{
			videoWindow1.FlipH = chkFlipH.IsChecked.Value;
		}

		private void chkFlipV_Unchecked(object sender, RoutedEventArgs e)
		{
			videoWindow1.FlipV = chkFlipV.IsChecked.Value;
		}

		private void btnSelectDevice_Click(object sender, RoutedEventArgs e)
		{
			_ic.LiveStop();

			_ic.ShowDeviceSettingsDialog();

			if( _ic.DeviceValid )
			{
				_ic.LiveStart();
			}
		}
	}
}
