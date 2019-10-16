using System.Windows;
using System.Windows.Controls;

namespace ImgProcLib
{
	public class ImageProcessing : Control
	{
		static ImageProcessing()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageProcessing), new FrameworkPropertyMetadata(typeof(ImageProcessing)));
		}
	}
}
