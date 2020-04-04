using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel.FileManagement;
using RGBtoGrey.ViewModel.Interfaces;
using Unity;

namespace RGBtoGrey.ViewModel
{
	public class ConvertedImageViewModel : BindableBase
	{
		private readonly IBitmapImageFileExporting _bitmapImageFileExporting;
		private readonly IFileDialog _fileDialog;
		private readonly IImageProcessingAdapter _imageProcessingAdapter;
		private string _conversionTime;

		private BitmapSource _convertedImage;
		private bool _isImageNotConverting = true;

		public ConvertedImageViewModel(
			[Dependency("ConvertedImageFileDialog")]
			IFileDialog fileDialog,
			IBitmapImageFileExporting bitmapImageFileExporting,
			IImageProcessingAdapter imageProcessingAdapter,
			IFileLocation fileLocation
		)
		{
			_fileDialog = fileDialog;
			_bitmapImageFileExporting = bitmapImageFileExporting;
			_imageProcessingAdapter = imageProcessingAdapter;

			fileLocation.GetFileLocationsObservable.Subscribe(OnFileLocationChanged);
		}

		public string FileLocation { get; private set; }

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

		public ICommand ConvertCommand => new DelegateCommand(async () => await ConvertImage());
		public ICommand SaveAsCommand => new DelegateCommand(ShowSaveFileDialog);

		private void OnFileLocationChanged(string fileLocation)
		{
			FileLocation = fileLocation;
		}

		private async Task ConvertImage()
		{
			if (string.IsNullOrEmpty(FileLocation))
				return;

			IsImageNotConverting = false;

			var watch = Stopwatch.StartNew();
			var outputImage = await _imageProcessingAdapter.ConvertImage(FileLocation);
			ConvertedImage = outputImage;
			watch.Stop();

			var elapsedMs = watch.ElapsedMilliseconds;
			ConversionTime = Convert.ToString(elapsedMs) + "ms";

			IsImageNotConverting = true;
		}

		private void ShowSaveFileDialog()
		{
			_fileDialog.ShowDialog();
			if (string.IsNullOrEmpty(_fileDialog.FilePath))
				return;
			var extension = ExtractExtensionFromPath(_fileDialog.FilePath);

			if (Enum.TryParse(extension, out ImageFileFormats imageFormat))
				_bitmapImageFileExporting.ExportImageAsFile(ConvertedImage, imageFormat, _fileDialog.FilePath);
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