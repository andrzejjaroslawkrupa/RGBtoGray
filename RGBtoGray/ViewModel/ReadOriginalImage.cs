using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
	class ReadOriginalImage : ObservableObject
	{
		private string _filename;
		private string _selectedPath;
		private BitmapImage _originalImage;

		public string Filename
		{
			get => _filename;
			set
			{
				_filename = value;
				RaisePropertyChangedEvent("Filename");
			}
		}

		public string SelectedPath
		{
			get => _selectedPath;
			set
			{
				_selectedPath = value;
				RaisePropertyChangedEvent("SelectedPath");
			}
		}

		public BitmapImage OriginalImage
		{
			get => _originalImage;
			set
			{
				_originalImage = value;
				RaisePropertyChangedEvent("OriginalImage");
			}
		}

		public ICommand OpenFileDialogCommand => new DelegateCommand(OpenFileDialog);

		private void OpenFileDialog()
		{
			var fileDialog = new OpenFileDialog
			{
				DefaultExt = ".jpg",
				Filter =
					"JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp",
				Title = "Please select an image file to convert.",
				RestoreDirectory = true
			};
			var result = fileDialog.ShowDialog();

			if (result == true)
			{
				SelectedPath = fileDialog.FileName;
				ChangeFilenameFromPath(SelectedPath);
				ChangeImageFromPath(SelectedPath);
			}
			else
				SelectedPath = null;
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

		private void ChangeImageFromPath(string path)
		{
			try
			{
				OriginalImage = new BitmapImage(new Uri(path));
			}
			catch (Exception e)
			{
				throw new ApplicationException("ChangeImage: ", e);
			}
		}
	}
}