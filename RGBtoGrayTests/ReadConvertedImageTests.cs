using NUnit.Framework;
using RGBtoGray.ViewModel;
using Moq;
using System;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadConvertedImageTests
	{
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void ConvertImage_ConvertCommandExecuted_ConvertImageUsedOnce()
		{
			var imageProcessing = new Mock<IImageProcessingAdapter>();
			Presenter.FilePath = _testFilesDirectory;
			var readConvertedImage = new ReadConvertedImage
			{
				ImageProcessingAdapter = imageProcessing.Object
			};

			readConvertedImage.ConvertCommand.Execute(null);

			imageProcessing.Verify(m => m.ConvertImage(It.IsAny<string>()), Times.Once);
		}
	}
}