using NUnit.Framework;
using RGBtoGray.ViewModel;
using Moq;
using System;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadConvertedImageTests
	{
		private string _TestFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConvertImageUsedOnce()
		{
			var imageProcessing = new Mock<IImageProcessingAdapter>();
			Presenter.FilePath = _TestFilesDirectory;
			var readConvertedImage = new ReadConvertedImage();
			readConvertedImage.ImageProcessingAdapter = imageProcessing.Object;

			readConvertedImage.ConvertCommand.Execute(null);

			imageProcessing.Verify(m => m.ConvertImage(It.IsAny<Uri>()), Times.Once);
		}
	}
}