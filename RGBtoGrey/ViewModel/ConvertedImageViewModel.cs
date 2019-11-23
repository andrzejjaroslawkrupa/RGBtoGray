using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel
{
	public class ConvertedImageViewModel : ObservableObject
	{
		private BitmapSource _convertedImage;
		private string _conversionTime;
		private bool _isImageConverted = false;

		public BitmapSource ConvertedImage
		{
			get => _convertedImage;
			private set
			{
				_convertedImage = value;
				OnPropertyChanged(() => ConvertedImage);
			}
		}

		public string ConversionTime
		{
			get => _conversionTime;
			private set
			{
				_conversionTime = value;
				OnPropertyChanged(() => ConversionTime);
			}
		}

		public bool IsImageConverted
		{
			get => _isImageConverted;
			private set
			{
				_isImageConverted = value;
				OnPropertyChanged(() => IsImageConverted);
			}
		}

		public IImageProcessingAdapter ImageProcessingAdapter { get; set; } = new ImageProcessingAdapter();

		public IFileDialog FileDialog { get; set; } = new FileDialog.FileDialog(new Microsoft.Win32.SaveFileDialog());
		public IBitmapImageFileExporting BitmapImageFileExporting { get; set; } = new BitmapImageFileExporting();

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
			if (string.IsNullOrEmpty(FileDialog.FilePath))
				return;
			var ext = Path.GetExtension(FileDialog.FilePath);
			try
			{
				ext = ext.Substring(1);
			}
			catch (Exception)
			{
				throw new FileFormatException();
			}
			
			if (Enum.TryParse(ext, out ImageFileFormats imageFormat))
				BitmapImageFileExporting.ExportImageAsFile(ConvertedImage, imageFormat, FileDialog.FilePath);
		}
	}
	public enum ImageFileFormats { jpg, jpeg, png, bmp }
}