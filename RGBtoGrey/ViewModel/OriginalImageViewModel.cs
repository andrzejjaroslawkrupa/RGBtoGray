using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Mvvm;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel.Interfaces;
using Unity;

namespace RGBtoGrey.ViewModel
{
	public class OriginalImageViewModel : BindableBase
	{
		private readonly IFileDialog _fileDialog;
		private readonly IFileLocation _fileLocation;
		private string _filename;
		private BitmapImage _originalImage;


		public OriginalImageViewModel([Dependency("OriginalImageFileDialog")]
			IFileDialog fileDialog, IFileLocation fileLocation)
		{
			_fileDialog = fileDialog;
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

		public ICommand OpenFileDialogCommand => new DelegateCommand(ShowOpenFileDialog);

		private void ShowOpenFileDialog()
		{
			if (_fileDialog.ShowDialog() != true || ChangeImageFromPath(_fileDialog.FilePath) != true) return;
			ChangeFilenameFromPath(_fileDialog.FilePath);
			_fileLocation.SetNewLocation(_fileDialog.FilePath);
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