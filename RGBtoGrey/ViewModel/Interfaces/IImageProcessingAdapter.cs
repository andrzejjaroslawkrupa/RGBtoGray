using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IImageProcessingAdapter
	{
		Task<BitmapSource> ConvertImage(string path);
	}
}