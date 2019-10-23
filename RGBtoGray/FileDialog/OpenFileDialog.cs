using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.FileDialog
{
	class OpenFileDialog : IOpenFileDialog
	{
		public string FilePath { get; private set; }

		public bool? ShowDialog()
		{
			var fileDialog = new Microsoft.Win32.OpenFileDialog
			{
				DefaultExt = ".jpg",
				Filter =
					"JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp",
				Title = "Please select an image file to convert.",
				RestoreDirectory = true
			};
			var didFileDialogOpen = fileDialog.ShowDialog();

			if (didFileDialogOpen == true)
			{
				FilePath = fileDialog.FileName;
			}
			else
				FilePath = "";

			return didFileDialogOpen;
		}
	}
}
