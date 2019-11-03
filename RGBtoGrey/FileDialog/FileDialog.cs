namespace RGBtoGrey.FileDialog
{
	internal class FileDialog : IFileDialog
	{
		private readonly Microsoft.Win32.FileDialog _fileDialog;
		public FileDialog(Microsoft.Win32.FileDialog fileDialog)
		{
			_fileDialog = fileDialog;
		}

		public string FilePath { get; private set; }

		public bool? ShowDialog()
		{
			_fileDialog.DefaultExt = ".jpg";
			_fileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
			_fileDialog.Title = "Please select an image file to convert.";
			_fileDialog.RestoreDirectory = true;

			var didFileDialogOpen = _fileDialog.ShowDialog();

			FilePath = didFileDialogOpen == true ? _fileDialog.FileName : "";

			return didFileDialogOpen;
		}
	}
}
