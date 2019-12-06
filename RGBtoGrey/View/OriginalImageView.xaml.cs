using RGBtoGrey.ViewModel;

namespace RGBtoGrey.View
{
	public partial class OriginalImageView
	{
		public OriginalImageView()
		{
			InitializeComponent();
			DataContext = new OriginalImageViewModel();
		}
	}
}