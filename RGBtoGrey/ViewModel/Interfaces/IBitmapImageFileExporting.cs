using System.Windows.Media.Imaging;
using RGBtoGrey.ViewModel.FileManagement;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IBitmapImageFileExporting
	{
		void ExportImageAsFile(BitmapSource bitmapSource, ImageFileFormats imageFormat, string outputPath);
	}
}
