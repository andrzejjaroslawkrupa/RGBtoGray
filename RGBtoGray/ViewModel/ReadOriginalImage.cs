using RGBtoGray.FileDialog;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
	public class ReadOriginalImage : ObservableObject
	{
		private string _filename;
		private string _selectedPath;
		private BitmapImage _originalImage;
		private IOpenFileDialog _openFileDialog = new OpenFileDialog();

		public string Filename
		{
			get => _filename;
			private set
			{
				_filename = value;
				RaisePropertyChangedEvent("Filename");
			}
		}

		public string SelectedPath
		{
			get => _selectedPath;
			private set
			{
				_selectedPath = value;
				RaisePropertyChangedEvent("SelectedPath");
			}
		}

		public BitmapImage OriginalImage
		{
			get => _originalImage;
			private set
			{
				_originalImage = value;
				RaisePropertyChangedEvent("OriginalImage");
			}
		}

		public IOpenFileDialog FileDialog { get { return _openFileDialog; } set { _openFileDialog = value; } }


		public ICommand OpenFileDialogCommand => new DelegateCommand(OpenFileDialog);

		private void OpenFileDialog()
		{
			if (FileDialog.ShowDialog() == true)
			{
				ChangeFilenameFromPath(FileDialog.FilePath);
				ChangeImageFromPath(FileDialog.FilePath);
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
			if (path == null) return;
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