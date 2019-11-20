using System.IO;
using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel
{
	class ImageFileExporting
	{
		private readonly BitmapImage _bitmapImage;

		public ImageFileExporting(BitmapImage bitmapImage)
		{
			_bitmapImage = bitmapImage;
		}
		public void ExportImageAsFile(ImageFileFormats imageFormat, string outputPath)
		{
			if (string.IsNullOrEmpty(outputPath))
				return;

			var encoder = AssignProperBitmapEncoderFromFileFormat(imageFormat);
			encoder.Frames.Add(BitmapFrame.Create(_bitmapImage));

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
	}
}
