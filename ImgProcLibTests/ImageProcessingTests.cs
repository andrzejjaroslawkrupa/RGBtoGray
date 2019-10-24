using ImgProcLib;
using NUnit.Framework;
using System;
using System.Windows.Media.Imaging;

namespace ImgProcLibTests
{
	[TestFixture]
	public class ImageProcessingTests
	{
		private string _CurrentDirectory = TestContext.CurrentContext.TestDirectory;
		private ImageProcessing _ImageProcessing;

		[SetUp]
		public void Setup()
		{
			_ImageProcessing = new ImageProcessing();
		}

		[Test]
		public void ConvertToGrayscale_BlackImage_ImageUnchanged()
		{
			var originalImage = new BitmapImage(new Uri(_CurrentDirectory + @"\\TestFiles\\testImage.jpg"));

			var convertedImage = _ImageProcessing.ConvertToGrayscale(originalImage);

			CollectionAssert.AreEqual(_ImageProcessing.GetBitmapPixels(convertedImage), _ImageProcessing.GetBitmapPixels(originalImage));
		}

		[Test]
		public void GetBitmapPixels_BlackImage_ValidByteArray()
		{
			var originalImage = new BitmapImage(new Uri(_CurrentDirectory + @"\\TestFiles\\testImage.jpg"));
			var expected = new byte[] { 0, 0, 0, 255 };

			var pixels = _ImageProcessing.GetBitmapPixels(originalImage);

			CollectionAssert.AreEqual(expected, pixels);
		}
	}
}