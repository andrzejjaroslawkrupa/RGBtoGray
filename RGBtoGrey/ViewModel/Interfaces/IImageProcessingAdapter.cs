using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IImageProcessingAdapter
	{
		BitmapSource ConvertImage(string path);
	}
}