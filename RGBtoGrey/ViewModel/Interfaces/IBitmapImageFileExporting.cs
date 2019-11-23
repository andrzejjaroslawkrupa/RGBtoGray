using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IBitmapImageFileExporting
	{
		void ExportImageAsFile(BitmapImage bitmapImage, ImageFileFormats imageFormat, string outputPath);
	}
}
