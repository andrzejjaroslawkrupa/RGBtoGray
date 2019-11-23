using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IBitmapImageFileExporting
	{
		void ExportImageAsFile(BitmapSource bitmapSource, ImageFileFormats imageFormat, string outputPath);
	}
}
