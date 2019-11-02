using NUnit.Framework;
using RGBtoGray.ViewModel;
using Moq;
using System;
using System.Windows.Media.Imaging;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadConvertedImageTests
	{
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";
		private BitmapImage _bitmapImage;
		private Mock<IImageProcessingAdapter> _imageProcessingMock;

		[SetUp]
		public void Setup()
		{
			_imageProcessingMock = new Mock<IImageProcessingAdapter>();
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConvertImageUsedOnce()
		{
			Presenter.FilePath = _testFilesDirectory;
			var readConvertedImage = new ReadConvertedImage
			{
				ImageProcessingAdapter = _imageProcessingMock.Object
			};

			readConvertedImage.ConvertCommand.Execute(null);

			_imageProcessingMock.Verify(m => m.ConvertImage(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConvertedImageSet()
		{
			_bitmapImage = new BitmapImage((new Uri(_testFilesDirectory)));
			_imageProcessingMock.Setup(m => m.ConvertImage(It.IsAny<string>()))
				.Returns(_bitmapImage);
			Presenter.FilePath = _testFilesDirectory;
			var readConvertedImage = new ReadConvertedImage
			{
				ImageProcessingAdapter = _imageProcessingMock.Object
			};
			var imageProcessing = new ImgProcLib.ImageProcessing();
			var expected = imageProcessing.GetBitmapPixels(_bitmapImage);

			readConvertedImage.ConvertCommand.Execute(null);
			var actual = imageProcessing.GetBitmapPixels(readConvertedImage.ConvertedImage);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}