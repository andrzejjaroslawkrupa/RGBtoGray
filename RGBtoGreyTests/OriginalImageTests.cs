using System;
using System.Windows.Media.Imaging;
using ImgProcLib;
using Moq;
using NUnit.Framework;
using RGBtoGrey.FileDialog;
using RGBtoGrey.ViewModel;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGreyTests
{
	[TestFixture]
	public class OriginalImageTests
	{
		private OriginalImageViewModel _originalImageViewModel;
		private Mock<IFileLocation> _fileLocationMock;

		private readonly string _testFilesDirectory =
			TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";

		[SetUp]
		public void Setup()
		{
			_fileLocationMock = new Mock<IFileLocation>();
		}

		private void SetSut(IMock<IFileDialog> fileDialogMock)
		{
			_originalImageViewModel = new OriginalImageViewModel(fileDialogMock.Object, _fileLocationMock.Object);
		}

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			var fileDialogMock = new Mock<IFileDialog>();
			fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
			fileDialogMock.Setup(m => m.FilePath).Returns(_testFilesDirectory);
			SetSut(fileDialogMock);

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);

			Assert.That(_originalImageViewModel.Filename, Is.EqualTo("testImage.jpg"));
		}

		[Test]
		public void NewImageLoaded_ImageSetCorrectly()
		{
			var testImagePath = _testFilesDirectory;
			var fileDialogMock = new Mock<IFileDialog>();
			fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
			fileDialogMock.Setup(m => m.FilePath).Returns(testImagePath);
			SetSut(fileDialogMock);
			var expected = ImageProcessing.GetBitmapPixels(new BitmapImage(new Uri(testImagePath)));

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);
			var actual = ImageProcessing.GetBitmapPixels(_originalImageViewModel.OriginalImage);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void NoPathChosen_MethodReturns()
		{
			var fileDialogMock = new Mock<IFileDialog>();
			fileDialogMock.Setup(m => m.ShowDialog()).Returns(false);
			SetSut(fileDialogMock);

			_originalImageViewModel.OpenFileDialogCommand.Execute(null);

			_fileLocationMock.Verify(m => m.SetNewLocation(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void PathChosenWithoutExistingFile_ExceptionThrown()
		{
			var fileDialogMock = new Mock<IFileDialog>();
			fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
			fileDialogMock.Setup(m => m.FilePath).Returns(@"C:\\testPath\\testFile.jpg");
			SetSut(fileDialogMock);

			Assert.That(() => _originalImageViewModel.OpenFileDialogCommand.Execute(null),
				Throws.Exception.TypeOf<ApplicationException>());
		}
	}
}