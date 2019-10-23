﻿using NUnit.Framework;
using RGBtoGray.ViewModel;
using Moq;
using RGBtoGray.FileDialog;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace RGBtoGrayTests
{
	[TestFixture]
	public class ReadOriginalImageTests
	{
		private ReadOriginalImage _ReadOriginalImage;
		private Mock<IOpenFileDialog> _FileDialogMock;
		private string _CurrentDirectory = TestContext.CurrentContext.TestDirectory;

		[SetUp]
		public void Setup()
		{
			_ReadOriginalImage = new ReadOriginalImage();
			_FileDialogMock = new Mock<IOpenFileDialog>();
			_FileDialogMock.Setup(m => m.ShowDialog()).Returns(true);
		}

		[Test]
		public void NewImageLoaded_FilenameChangedCorrectly()
		{
			_FileDialogMock.Setup(m => m.FilePath).Returns(_CurrentDirectory + @"\\TestFiles\\testImage.jpg");
			_ReadOriginalImage.FileDialog = _FileDialogMock.Object;

			_ReadOriginalImage.OpenFileDialogCommand.Execute(null);

			Assert.That(_ReadOriginalImage.Filename, Is.EqualTo("testImage.jpg"));
		}

		[Test]
		public void NewImageLoaded_ImageSetCorrectly()
		{
			var testImagePath = _CurrentDirectory + @"\\TestFiles\\testImage.jpg";
			_FileDialogMock.Setup(m => m.FilePath).Returns(testImagePath);
			_ReadOriginalImage.FileDialog = _FileDialogMock.Object;
			var expected = GetBitmapPixels(new BitmapImage(new Uri(testImagePath)));

			_ReadOriginalImage.OpenFileDialogCommand.Execute(null);
			var actual = GetBitmapPixels(_ReadOriginalImage.OriginalImage);

			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void PathChosenWithoutExistingFile_ExceptionThrown()
		{
			_FileDialogMock.Setup(m => m.FilePath).Returns("C:\\testPath\\testFile.jpg");
			_ReadOriginalImage.FileDialog = _FileDialogMock.Object;

			Assert.That(() => _ReadOriginalImage.OpenFileDialogCommand.Execute(null), Throws.Exception.TypeOf<ApplicationException>());
		}

		private byte[] GetBitmapPixels(BitmapImage bitmapImage)
		{
			int stride = bitmapImage.PixelWidth * (bitmapImage.Format.BitsPerPixel / 8);
			byte[] pixels = new byte[bitmapImage.PixelHeight * stride];

			bitmapImage.CopyPixels(pixels, stride, 0);

			return pixels;
		}
	}
}