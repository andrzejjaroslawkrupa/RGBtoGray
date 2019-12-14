using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel
{
	public class OriginalImageViewModel : BindableBase
	{
		private string _filename;
		private BitmapImage _originalImage;
		private readonly IFileLocation _fileLocation;

		public OriginalImageViewModel(IFileLocation fileLocation)
		{
			_fileLocation = fileLocation;
		}

		public string Filename
		{
			get => _filename;
			private set
			{
				_filename = value;
				RaisePropertyChanged(nameof(Filename));
			}
		}

		public BitmapImage OriginalImage
		{
			get => _originalImage;
			private set
			{
				_originalImage = value;
				RaisePropertyChanged(nameof(OriginalImage));
			}
		}

		public IFileDialog FileDialog { get; set; } = new FileDialog.FileDialog(new Microsoft.Win32.OpenFileDialog());

		public ICommand OpenFileDialogCommand => new DelegateCommand(ShowOpenFileDialog);

		private void ShowOpenFileDialog()
		{
			if (FileDialog.ShowDialog() != true || ChangeImageFromPath(FileDialog.FilePath) != true) return;
			ChangeFilenameFromPath(FileDialog.FilePath);
			_fileLocation.SetNewLocation(FileDialog.FilePath);
		}

		private void ChangeFilenameFromPath(string path)
		{
			if (path == null) return;
			try
			{
				var directorySplit = path.Split('\\');
				Filename = directorySplit[directorySplit.Length - 1];
			}
			catch (Exception e)
			{
				throw new ApplicationException("ChangeFilename: ", e);
			}
		}

		private bool? ChangeImageFromPath(string path)
		{
			if (path == null) return false;
			try
			{
				OriginalImage = new BitmapImage(new Uri(path));
				return true;
			}
			catch (Exception e)
			{
				throw new ApplicationException("ChangeImage: ", e);
			}
		}
	}
}