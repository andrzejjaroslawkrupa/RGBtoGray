using ImgProcLib;
using NUnit.Framework;
using System;
using System.Windows.Media.Imaging;

namespace ImgProcLibTests
{
	[TestFixture]
	public class ImageProcessingTests
	{
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles";
		private readonly string _blackTestImage = @"\\blackTestImage.jpg";
		private readonly string _redTestImage = @"\\redTestImage.jpg";

		[Test]
		public void ConvertToGreyscale_BlackImage_ImageUnchanged()
		{
			var originalImage = new BitmapImage(new Uri(_testFilesDirectory + _blackTestImage));

			var convertedImage = ImageProcessing.ConvertBitmapImageToGreyscale(originalImage);

			CollectionAssert.AreEqual(ImageProcessing.GetBitmapPixels(convertedImage), ImageProcessing.GetBitmapPixels(originalImage));
		}

		[Test]
		public void ConvertToGreyscale_RedImage_ImageConvertedCorrectly()
		{
			var originalImage = new BitmapImage(new Uri(_testFilesDirectory + _redTestImage));
			var expected = new byte[] { 44, 44, 44, 255 };

			var convertedImage = ImageProcessing.ConvertBitmapImageToGreyscale(originalImage);

			CollectionAssert.AreEqual(expected, ImageProcessing.GetBitmapPixels(convertedImage));
		}

		[Test]
		public void GetBitmapPixels_BlackImage_ValidByteArray()
		{
			var originalImage = new BitmapImage(new Uri(_testFilesDirectory + _blackTestImage));
			var expected = new byte[] { 0, 0, 0, 255 };

			var pixels = ImageProcessing.GetBitmapPixels(originalImage);

			CollectionAssert.AreEqual(expected, pixels);
		}

		[Test]
		public void GetBitmapPixels_RedImage_ValidByteArray()
		{
			var originalImage = new BitmapImage(new Uri(_testFilesDirectory + _redTestImage));
			var expected = new byte[] { 36, 27, 237, 255 };

			var pixels = ImageProcessing.GetBitmapPixels(originalImage);

			CollectionAssert.AreEqual(expected, pixels);
		}

		[Test]
		public void GetBitmapPixels_Null_ExceptionIsThrown()
		{
			Assert.That(() => ImageProcessing.GetBitmapPixels(null), Throws.ArgumentNullException);
		}
	}
}