using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ICWPF_VideoWindow
{
    public enum ImageAlignment
	{
		Default,
		Center,
		Stretch,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
	}

	/// <summary>
	/// Interaction logic for VideoWindow.xaml
	/// </summary>
	public partial class VideoWindow : UserControl
	{
		private WriteableBitmap _source = null;
		private bool IsSourceBottomUp { get; set; }

		private Adorner _overlayAdorner = null;

		public VideoWindow()
		{
			InitializeComponent();
		}

		#region Events
		public class DrawOverlayEventArgs : EventArgs
		{
			public DrawingContext DrawingContext { get; private set; }
			public Rect WindowRect { get; private set; }

			internal DrawOverlayEventArgs(DrawingContext dc, Rect rc)
			{
				DrawingContext = dc;
				WindowRect = rc;
			}
		}
		public event EventHandler<DrawOverlayEventArgs> DrawOverlay;
		private void OnDrawOverlay(DrawingContext dc)
		{
			if (DrawOverlay != null)
			{
				DrawOverlay(this, new DrawOverlayEventArgs(dc, new Rect(this.DesiredSize)));
			}
		}
		#endregion

		#region Operations
		public void UpdateImage(TIS.Imaging.IFrame buffer)
		{
			if (display.Dispatcher.CheckAccess())
			{
				DoUpdateImage(buffer);
			}
			else
			{
				if ((_updateImageOperation != null) && (_updateImageOperation.Status != System.Windows.Threading.DispatcherOperationStatus.Completed))
				{
					return;
				}

				_updateImageOperation = display.Dispatcher.BeginInvoke(new Action(() => DoUpdateImage(buffer)));
			}
		}

		private System.Windows.Threading.DispatcherOperation _updateImageOperation = null;

		private void DoUpdateImage(TIS.Imaging.IFrame buffer)
		{
			int bufferWidth = buffer.FrameType.Width;
			int bufferHeight = buffer.FrameType.Height;
			PixelFormat bufferFormat = PixelFormatFromFrameType(buffer.FrameType);

			if ((_source == null)
				|| (_source.PixelWidth != bufferWidth)
				|| (_source.PixelHeight != bufferHeight)
				|| (_source.Format != bufferFormat))
			{
				_source = CreateBackBuffer(bufferWidth, bufferHeight, bufferFormat);
				CopyImageBufferToWritableBitmap(buffer, _source);
				display.Source = _source;
			}
			else
			{
				CopyImageBufferToWritableBitmap(buffer, _source);
			}

			if (_overlayAdorner != null)
			{
				_overlayAdorner.InvalidateVisual();
			}

			IsSourceBottomUp = buffer.FrameType.IsBottomUp;
			UpdateFlip();
		}
		#endregion

		#region Dependency Properties
		public static readonly DependencyProperty FlipVProperty =
			DependencyProperty.Register("FlipV", typeof(Boolean), typeof(VideoWindow),
			new FrameworkPropertyMetadata(false,
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(FlipChanged)
				));
		public static readonly DependencyProperty FlipHProperty =
			DependencyProperty.Register("FlipH", typeof(Boolean), typeof(VideoWindow),
			new FrameworkPropertyMetadata(false,
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(FlipChanged)
				));
		public static readonly DependencyProperty ImageAlignmentProperty =
			DependencyProperty.Register("ImageAlignment", typeof(ImageAlignment), typeof(VideoWindow),
			new FrameworkPropertyMetadata(ImageAlignment.Default,
				FrameworkPropertyMetadataOptions.AffectsRender,
				new PropertyChangedCallback(StretchCenterChanged)
				));
		public static readonly DependencyProperty ZoomFactorProperty =
			DependencyProperty.Register("ZoomFactor", typeof(double), typeof(VideoWindow),
			new FrameworkPropertyMetadata(1.0,
				FrameworkPropertyMetadataOptions.AffectsMeasure,
				new PropertyChangedCallback(ZoomFactorChanged)
				));

		public bool FlipV
		{
			get
			{
				return (bool)GetValue(FlipVProperty);
			}
			set
			{
				SetValue(FlipVProperty, value);
			}
		}
		public bool FlipH
		{
			get
			{
				return (bool)GetValue(FlipHProperty);
			}
			set
			{
				SetValue(FlipHProperty, value);
			}
		}
		public double ZoomFactor
		{
			get
			{
				return (double)GetValue(ZoomFactorProperty);
			}
			set
			{
				SetValue(ZoomFactorProperty, value);
			}
		}
		public ImageAlignment ImageAlignment
		{
			get
			{
				return (ImageAlignment)GetValue(ImageAlignmentProperty);
			}
			set
			{
				SetValue(ImageAlignmentProperty, value);
			}
		}

		static void FlipChanged(DependencyObject dep, DependencyPropertyChangedEventArgs e)
		{
			var vw = dep as VideoWindow;
			vw.UpdateFlip();
		}
		static void StretchCenterChanged(DependencyObject dep, DependencyPropertyChangedEventArgs e)
		{
			var vw = dep as VideoWindow;
			vw.UpdateStretchCenter();
		}
		static void ZoomFactorChanged(DependencyObject dep, DependencyPropertyChangedEventArgs e)
		{
			var vw = dep as VideoWindow;
			vw.UpdateZoom();
		}
		#endregion

		private void UpdateStretchCenter()
		{
			ImageAlignment align = this.ImageAlignment;

			switch (align)
			{
				case ImageAlignment.Center:
					display.HorizontalAlignment = HorizontalAlignment.Center;
					display.VerticalAlignment = VerticalAlignment.Center;
					break;
				case ImageAlignment.Stretch:
					display.HorizontalAlignment = HorizontalAlignment.Stretch;
					display.VerticalAlignment = VerticalAlignment.Stretch;
					break;
				case ImageAlignment.TopRight:
					display.HorizontalAlignment = HorizontalAlignment.Right;
					display.VerticalAlignment = VerticalAlignment.Top;
					break;
				case ImageAlignment.BottomRight:
					display.HorizontalAlignment = HorizontalAlignment.Right;
					display.VerticalAlignment = VerticalAlignment.Bottom;
					break;
				case ImageAlignment.BottomLeft:
					display.HorizontalAlignment = HorizontalAlignment.Left;
					display.VerticalAlignment = VerticalAlignment.Bottom;
					break;
				case ImageAlignment.TopLeft:
				case ImageAlignment.Default:
				default:
					display.HorizontalAlignment = HorizontalAlignment.Left;
					display.VerticalAlignment = VerticalAlignment.Top;
					break;
			}

			UpdateFlip();

			bool stretch = this.ImageAlignment == ImageAlignment.Stretch;
			scrollView.HorizontalScrollBarVisibility = stretch ? ScrollBarVisibility.Disabled : ScrollBarVisibility.Auto;
			scrollView.VerticalScrollBarVisibility = stretch ? ScrollBarVisibility.Disabled : ScrollBarVisibility.Auto;
		}

		private void UpdateFlip()
		{
			bool doFlipV = this.FlipV ^ IsSourceBottomUp;
			bool doflipH = this.FlipH;

			int flipFactorX = doflipH ? -1 : 1;
			int flipFactorY = doFlipV ? -1 : 1;

			var transform = new System.Windows.Media.ScaleTransform(flipFactorX, flipFactorY);
			display.RenderTransformOrigin = new Point(0.5, 0.5);
			display.RenderTransform = transform;
		}

		private void UpdateZoom()
		{
			double zoom = this.ZoomFactor;

			displayContainer.LayoutTransform = new ScaleTransform(zoom, zoom);
		}

		private WriteableBitmap CreateBackBuffer(int width, int height, PixelFormat format)
		{
			double dpiX = 96;
			double dpiY = 96;

			try
			{
				Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
				dpiX = m.M11 * 96;
				dpiY = m.M22 * 96;
			}
			catch
			{
			}

			return new WriteableBitmap(width, height, dpiX, dpiY, format, null);
		}

		private static PixelFormat PixelFormatFromFrameType(TIS.Imaging.FrameType ftype)
		{
			switch (ftype.PixelFormat)
			{
				case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
					return PixelFormats.Bgra32;
				case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
					return PixelFormats.Bgr32;
				case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
					return PixelFormats.Bgra32;

				case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
					return PixelFormats.Bgr24;

				case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
					return PixelFormats.Gray8;

				default:
					throw new Exception("Unknown pixel Format");
			}
		}

		private static void CopyImageBufferToWritableBitmap(TIS.Imaging.IFrame buffer, WriteableBitmap wb)
		{
			var rect = new Int32Rect(0, 0, buffer.FrameType.Width, buffer.FrameType.Height);
			wb.WritePixels(rect, buffer.GetIntPtr(), buffer.FrameType.BufferSize, buffer.FrameType.BytesPerLine);
		}

		private class UpdateActionAdorner : Adorner
		{
			private VideoWindow _window;
			private Action<DrawingContext> _updateAction;

			public UpdateActionAdorner(UIElement adornedElement, VideoWindow window, Action<DrawingContext> updateAction)
				: base(adornedElement)
			{
				_window = window;
				_updateAction = updateAction;
			}

			protected override void OnRender(DrawingContext drawingContext)
			{
				bool doFlipV = (bool)_window.FlipV ^ _window.IsSourceBottomUp;
				bool doFlipH = (bool)_window.FlipH;

				double flipFactorX = doFlipH ? -1 : 1;
				double flipFactorY = doFlipV ? -1 : 1;

				double centerX = AdornedElement.DesiredSize.Width / 2;
				double centerY = AdornedElement.DesiredSize.Height / 2;

				if (_window.ImageAlignment == ImageAlignment.Stretch)
				{
					flipFactorX /= _window.ZoomFactor;
					flipFactorY /= _window.ZoomFactor;
					centerX = 0;
					centerY = 0;

					double translateX = doFlipH ? AdornedElement.DesiredSize.Width : 0;
					double translateY = doFlipV ? AdornedElement.DesiredSize.Height : 0;

					drawingContext.PushTransform(new TranslateTransform(translateX, translateY));
				}
				else
				{
					drawingContext.PushTransform(new TranslateTransform(0, 0));
				}

				drawingContext.PushTransform(new ScaleTransform(flipFactorX, flipFactorY, centerX, centerY));

				_updateAction(drawingContext);

				drawingContext.Pop();
				drawingContext.Pop();
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var adornerLayer = AdornerLayer.GetAdornerLayer(display);
			_overlayAdorner = new UpdateActionAdorner(display, this, this.OnDrawOverlay);
			adornerLayer.Add(_overlayAdorner);
		}
	}
}
