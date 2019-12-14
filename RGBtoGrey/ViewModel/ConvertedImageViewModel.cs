using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel.Interfaces;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace RGBtoGrey.ViewModel
{
	public class ConvertedImageViewModel : BindableBase
	{
		private BitmapSource _convertedImage;
		private string _conversionTime;
		private bool _isImageNotConverting = true;
		private string _fileLocation;

		public ConvertedImageViewModel(IFileLocation fileLocation)
		{
			fileLocation.GetFileLocationsObservable.Subscribe(f => OnFileLocationChanged(f));
		}

		private void OnFileLocationChanged(string fileLocation)
		{
			_fileLocation = fileLocation;
		}

		public BitmapSource ConvertedImage
		{
			get => _convertedImage;
			private set
			{
				_convertedImage = value;
				RaisePropertyChanged(nameof(ConvertedImage));
			}
		}

		public string ConversionTime
		{
			get => _conversionTime;
			private set
			{
				_conversionTime = value;
				RaisePropertyChanged(nameof(ConversionTime));
			}
		}

		public bool IsImageNotConverting
		{
			get => _isImageNotConverting;
			private set
			{
				_isImageNotConverting = value;
				RaisePropertyChanged(nameof(IsImageNotConverting));
				RaisePropertyChanged(nameof(IsImageConverted));
			}
		}

		public bool IsImageConverted => ConvertedImage != null && IsImageNotConverting;

		public IImageProcessingAdapter ImageProcessingAdapter { get; set; } = new ImageProcessingAdapter();

		public IFileDialog FileDialog { get; set; } = new FileDialog.FileDialog(new Microsoft.Win32.SaveFileDialog());
		public IBitmapImageFileExporting BitmapImageFileExporting { get; set; } = new BitmapImageFileExporting();

		public ICommand ConvertCommand => new DelegateCommand(async ()=> await ConvertImage());
		public ICommand SaveAsCommand => new DelegateCommand(ShowSaveFileDialog);

		private async Task ConvertImage()
		{
			IsImageNotConverting = false;

			var watch = System.Diagnostics.Stopwatch.StartNew();
			var outputImage = await ImageProcessingAdapter.ConvertImage(_fileLocation);
			ConvertedImage = outputImage;
			watch.Stop();

			var elapsedMs = watch.ElapsedMilliseconds;
			ConversionTime = Convert.ToString(elapsedMs) + "ms";

			IsImageNotConverting = true;
		}

		private void ShowSaveFileDialog()
		{
			FileDialog.ShowDialog();
			if (string.IsNullOrEmpty(FileDialog.FilePath))
				return;
			string extension = ExtractExtensionFromPath(FileDialog.FilePath);

			if (Enum.TryParse(extension, out ImageFileFormats imageFormat))
				BitmapImageFileExporting.ExportImageAsFile(ConvertedImage, imageFormat, FileDialog.FilePath);
		}

		private string ExtractExtensionFromPath(string path)
		{
			var ext = Path.GetExtension(path);
			try
			{
				ext = ext.Substring(1);
			}
			catch (Exception)
			{
				throw new FileFormatException();
			}

			return ext;
		}
	}
}