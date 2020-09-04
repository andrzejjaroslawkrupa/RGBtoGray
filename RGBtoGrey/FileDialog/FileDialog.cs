using RGBtoGrey.ViewModel.FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGBtoGrey.FileDialog
{
	internal class FileDialog : IFileDialog
	{
		private readonly Microsoft.Win32.FileDialog _fileDialog;

		public FileDialog(Microsoft.Win32.FileDialog fileDialog)
		{
			_fileDialog = fileDialog;
			InitializeFileDialog();
		}

		public string FilePath { get; private set; }

		private static Dictionary<ImageFileFormats, string> FilterDictionary =>
			((ImageFileFormats[])Enum.GetValues(typeof(ImageFileFormats))).ToDictionary(
				imageFileFormat => imageFileFormat, FormatFilter);

		private static string DefaultExtension => "." + FilterDictionary.Keys.First();

		private void InitializeFileDialog()
		{
			_fileDialog.DefaultExt = DefaultExtension;
			_fileDialog.Filter = GenerateFilter();
			_fileDialog.Title = "Please select an image file to convert.";
			_fileDialog.RestoreDirectory = true;
		}

		private static string GenerateFilter()
		{
			var filter = FilterDictionary.Aggregate(string.Empty, (current, fileFormat) => current + fileFormat.Value);

			return filter.TrimEnd('|');
		}

		private static string FormatFilter(ImageFileFormats format)
		{
			var formatString = format.ToString();

			return formatString.ToUpper() + " Files (*." + formatString + ")|*." + formatString + "|";
		}

		public bool? ShowDialog()
		{
			var didFileDialogOpen = _fileDialog.ShowDialog();

			FilePath = didFileDialogOpen == true ? _fileDialog.FileName : "";

			return didFileDialogOpen;
		}
	}
}