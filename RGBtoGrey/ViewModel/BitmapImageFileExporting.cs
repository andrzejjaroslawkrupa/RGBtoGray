using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel
{
	public class BitmapImageFileExporting : IBitmapImageFileExporting
	{
		public void ExportImageAsFile(BitmapImage bitmapImage, ImageFileFormats imageFormat, string outputPath)
		{
			if (bitmapImage is null || outputPath is null)
				throw new ArgumentNullException();

			if (string.IsNullOrEmpty(outputPath) || IsPathValid(outputPath))
				throw new ArgumentException("Invalid output path");

			var encoder = AssignProperBitmapEncoderFromFileFormat(imageFormat);
			encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

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
			return !Directory.Exists(path);
		}
	}
}
