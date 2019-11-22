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
	public class OriginalImageTests
	{
		private OriginalImageViewModel _originalImageVM;
		private Mock<IFileDialog> _fileDialogMock;
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			_originalImageVM = new OriginalImageViewModel();
			_fileDialogMock = new Mock<IFileDialog>();
			_fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
		}

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(_testFilesDirectory);
			_originalImageVM.FileDialog = _fileDialogMock.Object;

			_originalImageVM.OpenFileDialogCommand.Execute(null);

			Assert.That(_originalImageVM.Filename, Is.EqualTo("testImage.jpg"));
		}

		[Test]
		public void NewImageLoaded_ImageSetCorrectly()
		{
			var testImagePath = _testFilesDirectory;
			_fileDialogMock.Setup(m => m.FilePath).Returns(testImagePath);
			_originalImageVM.FileDialog = _fileDialogMock.Object;
			var expected = ImageProcessing.GetBitmapPixels(new BitmapImage(new Uri(testImagePath)));

			_originalImageVM.OpenFileDialogCommand.Execute(null);
			var actual = ImageProcessing.GetBitmapPixels(_originalImageVM.OriginalImage);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void PathChosenWithoutExistingFile_ExceptionThrown()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(@"C:\\testPath\\testFile.jpg");
			_originalImageVM.FileDialog = _fileDialogMock.Object;

			Assert.That(() => _originalImageVM.OpenFileDialogCommand.Execute(null), Throws.Exception.TypeOf<ApplicationException>());
		}
	}
}