using NUnit.Framework;
using RGBtoGray.ViewModel;
using Moq;
using RGBtoGray.FileDialog;
using System;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadOriginalImageTests
	{

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			var readOriginalImage = new ReadOriginalImage();
			var fileDialogMock = new Mock<IOpenFileDialog>();
			fileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
			fileDialogMock.Setup(m => m.FilePath).Returns("C:\\testPath\\testFile.jpg");
			readOriginalImage.FileDialog = fileDialogMock.Object;

			Assert.That(() => readOriginalImage.OpenFileDialogCommand.Execute(null), Throws.Exception.TypeOf<ApplicationException>());
			Assert.That(readOriginalImage.Filename, Is.EqualTo("testFile.jpg"));
		}
	}
}