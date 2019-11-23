using System.IO;
using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel
{
	public class BitmapImageFileExporting : IBitmapImageFileExporting
	{
		public void ExportImageAsFile(BitmapImage bitmapImage, ImageFileFormats imageFormat, string outputPath)
		{
			if (string.IsNullOrEmpty(outputPath))
				return;

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
	}
}
