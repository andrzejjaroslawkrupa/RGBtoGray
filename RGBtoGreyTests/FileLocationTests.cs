using Moq;
using NUnit.Framework;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel;
using RGBtoGrey.ViewModel.FileManagement;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGreyTests
{
	[TestFixture]
	public class FileLocationTests
	{
		private IFileLocation _fileLocation;
		private Mock<IFileDialog> _fileDialogMock;
		private Mock<IBitmapImageFileExporting> _bitmapImageFileExporting;
		private Mock<IImageProcessingAdapter> _imageProcessingAdapter;
		private ConvertedImageViewModel _convertedImageViewModel;

		[SetUp]
		public void SetUp()
		{
			_fileLocation = new FileLocation();
			_fileDialogMock = new Mock<IFileDialog>();
			_bitmapImageFileExporting = new Mock<IBitmapImageFileExporting>();
			_imageProcessingAdapter = new Mock<IImageProcessingAdapter>();

			_convertedImageViewModel = new ConvertedImageViewModel(_fileDialogMock.Object,
				_bitmapImageFileExporting.Object, _imageProcessingAdapter.Object, _fileLocation);
		}

		[Test]
		public void SetNewLocation_LocationIsChangedImmediately_ObservableSendsNewLocationToSubscribers()
		{
			_fileLocation.SetNewLocation("path");
			_fileLocation.SetNewLocation("path2");

			Assert.That(_convertedImageViewModel.FileLocation, Is.EqualTo("path2"));
		}

		[Test]
		public void SetNewLocation_NewLocation_ObservableSendsLocationToSubscribers()
		{
			_fileLocation.SetNewLocation("path");

			Assert.That(_convertedImageViewModel.FileLocation, Is.EqualTo("path"));
		}

		[Test]
		public void SetNewLocation_NoLocationSet_ObservableDoesNotSendLocation()
		{
			Assert.That(_convertedImageViewModel.FileLocation, Is.Null);
		}
	}
}