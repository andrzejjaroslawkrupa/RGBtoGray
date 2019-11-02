using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
	public class ReadConvertedImage : ObservableObject
	{
		private BitmapImage _convertedImage;
		private string _conversionTime;

		public BitmapImage ConvertedImage
		{
			get => _convertedImage;
			private set
			{
				_convertedImage = value;
				RaisePropertyChangedEvent("ConvertedImage");
			}
		}

		public string ConversionTime
		{
			get => _conversionTime;
			private set
			{
				_conversionTime = value;
				RaisePropertyChangedEvent("ConversionTime");
			}
		}

		public IImageProcessingAdapter ImageProcessingAdapter { get; set; } = new ImageProcessingAdapter();

		public ICommand ConvertCommand => new DelegateCommand(ConvertImage);

		private void ConvertImage()
		{
			var uri = new Uri(Presenter.FilePath);

			var watch = System.Diagnostics.Stopwatch.StartNew();
			ConvertedImage = ImageProcessingAdapter.ConvertImage(uri);
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			ConversionTime = Convert.ToString(elapsedMs) + "ms";
		}
	}
}