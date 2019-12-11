using NUnit.Framework;
using RGBtoGrey.ViewModel;
using Moq;
using RGBtoGrey.FileDialog;
using System;
using System.Windows.Media.Imaging;
using ImgProcLib;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGreyTests
{
	[TestFixture]
	public class OriginalImageTests
	{
		private OriginalImageViewModel _originalImageViewModel;
		private Mock<IFileDialog> _fileDialogMock;
		private Mock<IFileLocation> _fileLocationMock;
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			_fileLocationMock = new Mock<IFileLocation>();
			_originalImageViewModel = new OriginalImageViewModel(_fileLocationMock.Object);
			_fileDialogMock = new Mock<IFileDialog>();
			_fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
		}

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(_testFilesDirectory);
			_originalImageViewModel.FileDialog = _fileDialogMock.Object;

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);

			Assert.That(_originalImageViewModel.Filename, Is.EqualTo("testImage.jpg"));
		}

		[Test]
		public void NewImageLoaded_ImageSetCorrectly()
		{
			var testImagePath = _testFilesDirectory;
			_fileDialogMock.Setup(m => m.FilePath).Returns(testImagePath);
			_originalImageViewModel.FileDialog = _fileDialogMock.Object;
			var expected = ImageProcessing.GetBitmapPixels(new BitmapImage(new Uri(testImagePath)));

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);
			var actual = ImageProcessing.GetBitmapPixels(_originalImageViewModel.OriginalImage);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void PathChosenWithoutExistingFile_ExceptionThrown()
		{
			_fileDialogMock.Setup(m => m.FilePath).Returns(@"C:\\testPath\\testFile.jpg");
			_originalImageViewModel.FileDialog = _fileDialogMock.Object;

			Assert.That(() => _originalImageViewModel.OpenFileDialogCommand.Execute(null), Throws.Exception.TypeOf<ApplicationException>());
		}

		[Test]
		public void NoPathChosen_MethodReturns()
		{
			_fileDialogMock.Setup(m => m.ShowDialog()).Returns(false);
			_originalImageViewModel.FileDialog = _fileDialogMock.Object;

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);

			_fileLocationMock.Verify(m => m.SetNewLocation(It.IsAny<string>()), Times.Never);
		}
	}
}