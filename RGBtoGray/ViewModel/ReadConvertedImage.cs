using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGray.ViewModel
{
	public class ReadConvertedImage : ObservableObject
	{
		private BitmapImage _convertedImage;

		public BitmapImage ConvertedImage
		{
			get => _convertedImage;
			private set
			{
				_convertedImage = value;
				RaisePropertyChangedEvent("ConvertedImage");
			}
		}

		public ICommand ConvertCommand => new DelegateCommand(ConvertImage);

		private void ConvertImage()
		{
			var imageProcessing = new ImageProcessing();

			ConvertedImage = imageProcessing.ConvertBitmapImageToGrayscale(new BitmapImage(new Uri(Presenter.FilePath)));
		}
	}
}