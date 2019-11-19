using System.IO;
using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel
{
	class ImageFileExporting
	{
		private BitmapImage _bitmapImage;

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
			BitmapEncoder encoder;

			switch (imageFileFormat)
			{
				case ImageFileFormats.png:
					encoder = new PngBitmapEncoder();
					break;
				case ImageFileFormats.bmp:
					encoder = new BmpBitmapEncoder();
					break;
				case ImageFileFormats.jpeg:
				case ImageFileFormats.jpg:
					encoder = new JpegBitmapEncoder();
					break;
				default:
					return null;
			};

			return encoder;
		}
	}
}
