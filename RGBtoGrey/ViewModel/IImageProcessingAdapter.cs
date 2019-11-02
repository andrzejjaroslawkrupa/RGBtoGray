using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel
{
	public interface IImageProcessingAdapter
	{
		BitmapImage ConvertImage(string path);
	}
}