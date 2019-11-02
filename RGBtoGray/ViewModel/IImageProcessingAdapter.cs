using System;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
	public interface IImageProcessingAdapter
	{
		BitmapImage ConvertImage(string path);
	}
}