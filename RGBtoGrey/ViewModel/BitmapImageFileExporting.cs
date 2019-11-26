using System;
using System.IO;
using System.Windows.Media.Imaging;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel
{
	public class BitmapImageFileExporting : IBitmapImageFileExporting
	{
		public void ExportImageAsFile(BitmapSource bitmapSource, ImageFileFormats imageFormat, string outputPath)
		{
			if (bitmapSource is null || outputPath is null)
				throw new ArgumentNullException();

			if (!IsPathValid(outputPath))
				return;

			var encoder = AssignProperBitmapEncoderFromFileFormat(imageFormat);
			encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

			using (var fileStream = new FileStream(outputPath, FileMode.Create))
			{
				encoder.Save(fileStream);
			}
		}

		private BitmapEncoder AssignProperBitmapEncoderFromFileFormat(ImageFileFormats imageFileFormat)
		{
			switch (imageFileFormat)
			{
				case ImageFileFormats.png:
					return new PngBitmapEncoder();
				case ImageFileFormats.bmp:
					return new BmpBitmapEncoder();
				case ImageFileFormats.jpeg:
				case ImageFileFormats.jpg:
					return new JpegBitmapEncoder();
				default:
					return null;
			};
		}

		private bool IsPathValid(string path)
		{
			try
			{
				path = Path.GetDirectoryName(path);
			}
			catch (Exception)
			{
				throw new ArgumentException("Invalid directory");
			}

			if (string.IsNullOrEmpty(path))
				throw new ArgumentException("Path is empty");

			return Directory.Exists(path);
		}
	}
}
