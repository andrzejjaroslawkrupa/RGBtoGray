using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IImageProcessingAdapter
	{
		BitmapImage ConvertImage(string path);
	}
}