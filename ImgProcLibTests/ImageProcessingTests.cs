using ImgProcLib;
using NUnit.Framework;
using System;
using System.Windows.Media.Imaging;

namespace ImgProcLibTests
{
	[TestFixture]
	public class ImageProcessingTests
	{
		private string _TestFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles";
		private ImageProcessing _ImageProcessing;
		private readonly string _blackTestImage = @"\\blackTestImage.jpg";
		private readonly string _redTestImage = @"\\redTestImage.jpg";

		[SetUp]
		public void Setup()
		{
			_ImageProcessing = new ImageProcessing();
		}

		[Test]
		public void ConvertToGrayscale_BlackImage_ImageUnchanged()
		{
			var originalImage = new BitmapImage(new Uri(_TestFilesDirectory + _blackTestImage));

			var convertedImage = _ImageProcessing.ConvertBitmapImageToGrayscale(originalImage);

			CollectionAssert.AreEqual(_ImageProcessing.GetBitmapPixels(convertedImage), _ImageProcessing.GetBitmapPixels(originalImage));
		}

		[Test]
		public void ConvertToGrayscale_RedImage_ImageConvertedCorectly()
		{
			var originalImage = new BitmapImage(new Uri(_TestFilesDirectory + _redTestImage));
			var expected = new byte[] { 44, 44, 44, 255 };

			var convertedImage = _ImageProcessing.ConvertBitmapImageToGrayscale(originalImage);

			CollectionAssert.AreEqual(expected, _ImageProcessing.GetBitmapPixels(convertedImage));
		}

		[Test]
		public void GetBitmapPixels_BlackImage_ValidByteArray()
		{
			var originalImage = new BitmapImage(new Uri(_TestFilesDirectory + _blackTestImage));
			var expected = new byte[] { 0, 0, 0, 255 };

			var pixels = _ImageProcessing.GetBitmapPixels(originalImage);

			CollectionAssert.AreEqual(expected, pixels);
		}

		[Test]
		public void GetBitmapPixels_RedImage_ValidByteArray()
		{
			var originalImage = new BitmapImage(new Uri(_TestFilesDirectory + _redTestImage));
			var expected = new byte[] { 36, 27, 237, 255 };

			var pixels = _ImageProcessing.GetBitmapPixels(originalImage);

			CollectionAssert.AreEqual(expected, pixels);
		}

		[Test]
		public void GetBitmapPixels_Null_ExceptionIsThrown()
		{
			BitmapImage originalImage = null;

			Assert.That(() => _ImageProcessing.GetBitmapPixels(originalImage), Throws.ArgumentNullException);
		}
	}
}