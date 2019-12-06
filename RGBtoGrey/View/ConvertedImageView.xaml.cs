using RGBtoGrey.ViewModel;

namespace RGBtoGrey.View
{
	public partial class ConvertedImageView
	{
		public ConvertedImageView()
		{
			InitializeComponent();
			DataContext = new ConvertedImageViewModel();
		}
	}
}