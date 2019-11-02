using NUnit.Framework;
using RGBtoGrey.ViewModel;
using Moq;
using System;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGreyTests
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
			Presenter.FilePath = _testFilesDirectory;
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConvertImageUsedOnce()
		{
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
			_bitmapImage = new BitmapImage(new Uri(_testFilesDirectory));
			_imageProcessingMock.Setup(m => m.ConvertImage(It.IsAny<string>()))
				.Returns(_bitmapImage);
			var readConvertedImage = new ReadConvertedImage
			{
				ImageProcessingAdapter = _imageProcessingMock.Object
			};
			var expected = ImageProcessing.GetBitmapPixels(_bitmapImage);

			readConvertedImage.ConvertCommand.Execute(null);
			var actual = ImageProcessing.GetBitmapPixels(readConvertedImage.ConvertedImage);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConversionTimeSet()
		{
			_imageProcessingMock.Setup(m => m.ConvertImage(It.IsAny<string>()));
			var readConvertedImage = new ReadConvertedImage
			{
				ImageProcessingAdapter = _imageProcessingMock.Object
			};

			readConvertedImage.ConvertCommand.Execute(null);

			Assert.That(readConvertedImage.ConversionTime, Is.Not.EqualTo(null));
		}
	}
}