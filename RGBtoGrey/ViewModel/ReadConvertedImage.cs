using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RGBtoGrey.FileDialog;

namespace RGBtoGrey.ViewModel
{
	public class ReadConvertedImage : ObservableObject
	{
		private BitmapImage _convertedImage;
		private string _conversionTime;
		private bool _isImageConverted = false;

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

		public bool IsImageConverted
		{
			get => _isImageConverted;
			private set
			{
				_isImageConverted = value;
				RaisePropertyChangedEvent("IsImageConverted");
			}
		}

		public IImageProcessingAdapter ImageProcessingAdapter { get; set; } = new ImageProcessingAdapter();

		public IFileDialog FileDialog { get; set; } = new FileDialog.FileDialog(new Microsoft.Win32.SaveFileDialog());

		public ICommand ConvertCommand => new DelegateCommand(ConvertImage);
		public ICommand SaveAsCommand => new DelegateCommand(ShowSaveFileDialog);

		private void ConvertImage()
		{
			IsImageConverted = false;

			var watch = System.Diagnostics.Stopwatch.StartNew();
			ConvertedImage = ImageProcessingAdapter.ConvertImage(Presenter.FilePath);
			watch.Stop();

			var elapsedMs = watch.ElapsedMilliseconds;
			ConversionTime = Convert.ToString(elapsedMs) + "ms";

			IsImageConverted = true;
		}

		private void ShowSaveFileDialog()
		{
			FileDialog.ShowDialog();
			var ext = Path.GetExtension(FileDialog.FilePath);

			Enum.TryParse(ext, out ImageFileFormats imageFormat);
			ExportImageAsFile(imageFormat, FileDialog.FilePath);
		}

		private void ExportImageAsFile(ImageFileFormats imageFormat, string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				return;

			BitmapEncoder encoder;
			switch (imageFormat)
			{
				case ImageFileFormats.png:
					encoder = new PngBitmapEncoder();
					break;
				case ImageFileFormats.bmp:
					encoder = new BmpBitmapEncoder();
					break;
				case ImageFileFormats.jpeg:
				case ImageFileFormats.jpg:
					encoder = new JpegBitmapEncoder();
					break;
				default:
					return;
			};

			encoder.Frames.Add(BitmapFrame.Create(ConvertedImage));

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				encoder.Save(fileStream);
			}
		}
	}
	public enum ImageFileFormats { jpg, jpeg, png, bmp }
}