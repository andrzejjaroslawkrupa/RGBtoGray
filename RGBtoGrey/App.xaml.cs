using System.Windows;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using RGBtoGrey.View;
using RGBtoGrey.ViewModel;

namespace RGBtoGrey
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			ViewModelLocationProvider.Register<OriginalImageView, OriginalImageViewModel>();
			ViewModelLocationProvider.Register<ConvertedImageView, ConvertedImageViewModel>();
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			
		}
	}
}
