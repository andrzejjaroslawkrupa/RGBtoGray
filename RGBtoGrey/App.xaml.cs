using System.Windows;
using Microsoft.Win32;
using Prism.Ioc;
using Prism.Mvvm;
using RGBtoGrey.FileDialog;
using RGBtoGrey.View;
using RGBtoGrey.ViewModel;
using RGBtoGrey.ViewModel.FileManagement;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			ViewModelLocationProvider.Register<OriginalImageView, OriginalImageViewModel>();
			ViewModelLocationProvider.Register<ConvertedImageView, ConvertedImageViewModel>();

			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IFileLocation, FileLocation>();
			containerRegistry.Register<IImageProcessingAdapter, ImageProcessingAdapter>();
			containerRegistry.Register<IBitmapImageFileExporting, BitmapImageFileExporting>();

			containerRegistry.RegisterInstance(typeof(IFileDialog),
				new FileDialog.FileDialog(new OpenFileDialog()), "OriginalImageFileDialog");
			containerRegistry.RegisterInstance(typeof(IFileDialog),
				new FileDialog.FileDialog(new SaveFileDialog()), "ConvertedImageFileDialog");
		}
	}
}