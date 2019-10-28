using RGBtoGray.FileDialog;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
	public class ReadOriginalImage : ObservableObject
	{
		private string _filename;
		private BitmapImage _originalImage;

		public string Filename
		{
			get => _filename;
			private set
			{
				_filename = value;
				RaisePropertyChangedEvent("Filename");
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

		public IOpenFileDialog FileDialog { get; set; } = new OpenFileDialog();


		public ICommand OpenFileDialogCommand => new DelegateCommand(OpenFileDialog);

		private void OpenFileDialog()
		{
			if (FileDialog.ShowDialog() == true && ChangeImageFromPath(FileDialog.FilePath) == true)
			{
				ChangeFilenameFromPath(FileDialog.FilePath);
				Presenter.FilePath = FileDialog.FilePath;
			}
			else
				Presenter.FilePath = null;
		}

		private bool? ChangeFilenameFromPath(string path)
		{
			if (path == null) return false;
			try
			{
				var directorySplit = path.Split('\\');
				Filename = directorySplit[directorySplit.Length - 1];
				return true;
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