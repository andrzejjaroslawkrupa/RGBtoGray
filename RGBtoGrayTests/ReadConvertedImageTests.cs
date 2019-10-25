using NUnit.Framework;
using RGBtoGray.ViewModel;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadConvertedImageTests
	{
		private ReadConvertedImage _ReadOriginalImage;
		private string _TestFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void NewImageConverted_ImageChangedCorrectly()
		{
			
		}
	}
}