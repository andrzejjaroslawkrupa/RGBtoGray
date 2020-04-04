using System;
using System.IO;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using RGBtoGrey.ViewModel.FileManagement;

namespace RGBtoGreyTests
{
	[TestFixture]
	public class BitmapImageFileExportingTests
	{
		private readonly string _testFileDirectory =
			TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		private readonly string _outputFileWithoutExt = TestContext.CurrentContext.TestDirectory + @"\\outputFile";
		private readonly DirectoryInfo _testDirInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);
		private BitmapImageFileExporting _bitmapImageFileExporting;
		private BitmapImage _testBitmapImage;

		[SetUp]
		public void SetUp()
		{
			_bitmapImageFileExporting = new BitmapImageFileExporting();
			_testBitmapImage = new BitmapImage(new Uri(_testFileDirectory));
		}

		[TearDown]
		public void TearDown()
		{
			foreach (var file in _testDirInfo.GetFiles())
				if (file.Name.Contains("outputFile"))
					file.Delete();
		}

		[Test]
		public void ExportImageAsFile_EmptyOutputPath_ExceptionThrown()
		{
			Assert.That(
				() => _bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.jpg, ""),
				Throws.Exception.TypeOf<ArgumentException>());
		}

		[Test]
		public void ExportImageAsFile_InvalidOutputPath_ExceptionThrown()
		{
			Assert.That(
				() => _bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.jpg, "xx"),
				Throws.Exception.TypeOf<ArgumentException>());
		}

		[Test]
		public void ExportImageAsFile_NullImage_ExceptionThrown()
		{
			Assert.That(
				() => _bitmapImageFileExporting.ExportImageAsFile(null, ImageFileFormats.jpg,
					_outputFileWithoutExt + ".jpg"),
				Throws.ArgumentNullException);
		}

		[Test]
		public void ExportImageAsFile_NullOutputPath_ExceptionThrown()
		{
			Assert.That(
				() => _bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.jpg, null),
				Throws.ArgumentNullException);
		}

		[Test]
		public void ExportImageAsFile_TestImage_ImageSavedAsBMP()
		{
			_bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.bmp,
				_outputFileWithoutExt + ".bmp");

			Assert.That(File.Exists(_outputFileWithoutExt + ".bmp"));
		}

		[Test]
		public void ExportImageAsFile_TestImage_ImageSavedAsJPG()
		{
			_bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.jpg,
				_outputFileWithoutExt + ".jpg");

			Assert.That(File.Exists(_outputFileWithoutExt + ".jpg"));
		}

		[Test]
		public void ExportImageAsFile_TestImage_ImageSavedAsPNG()
		{
			_bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.png,
				_outputFileWithoutExt + ".png");

			Assert.That(File.Exists(_outputFileWithoutExt + ".png"));
		}

		[Test]
		public void ExportImageAsFile_TestImageWithJPEG_ImageSavedAsJPG()
		{
			_bitmapImageFileExporting.ExportImageAsFile(_testBitmapImage, ImageFileFormats.jpeg,
				_outputFileWithoutExt + ".jpg");

			Assert.That(File.Exists(_outputFileWithoutExt + ".jpg"));
		}
	}
}