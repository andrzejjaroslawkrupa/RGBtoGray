using NUnit.Framework;
using RGBtoGrey.ViewModel;
using Moq;
using RGBtoGrey.FileDialog;
using System;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGreyTests
{
	[TestFixture]
	public class ReadOriginalImageTests
	{
		private ReadOriginalImage _readOriginalImage;
		private Mock<IFileDialog> _fileDialogMock;
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			_readOriginalImage = new ReadOriginalImage();
			_fileDialogMock = new Mock<IFileDialog>();
			_fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
		}

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(_testFilesDirectory);
			_readOriginalImage.FileDialog = _fileDialogMock.Object;

			_readOriginalImage.OpenFileDialogCommand.Execute(null);

			Assert.That(_readOriginalImage.Filename, Is.EqualTo("testImage.jpg"));
		}

		[Test]
		public void NewImageLoaded_ImageSetCorrectly()
		{
			var testImagePath = _testFilesDirectory;
			_fileDialogMock.Setup(m => m.FilePath).Returns(testImagePath);
			_readOriginalImage.FileDialog = _fileDialogMock.Object;
			var expected = ImageProcessing.GetBitmapPixels(new BitmapImage(new Uri(testImagePath)));

			_readOriginalImage.OpenFileDialogCommand.Execute(null);
			var actual = ImageProcessing.GetBitmapPixels(_readOriginalImage.OriginalImage);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void PathChosenWithoutExistingFile_ExceptionThrown()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(@"C:\\testPath\\testFile.jpg");
			_readOriginalImage.FileDialog = _fileDialogMock.Object;

			Assert.That(() => _readOriginalImage.OpenFileDialogCommand.Execute(null), Throws.Exception.TypeOf<ApplicationException>());
		}
	}
}