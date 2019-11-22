using NUnit.Framework;
using RGBtoGrey.ViewModel;
using Moq;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using ImgProcLib;
using RGBtoGrey.FileDialog;

namespace RGBtoGreyTests
{
	public class ConvertedImageTests
	{
		private readonly string _testFilesDirectory = TestContext.CurrentContext.TestDirectory + @"\\TestFiles\\testImage.jpg";
		private Mock<IImageProcessingAdapter> _imageProcessingMock;

		[TestFixture]
		public class ConvertImageTests : ConvertedImageTests
		{
			[SetUp]
			public void Setup()
			{
				_imageProcessingMock = new Mock<IImageProcessingAdapter>();
				Presenter.FilePath = _testFilesDirectory;
			}

			private ConvertedImageViewModel GetSut()
			{
				return new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object
				};
			}

			[Test]
			public void ConvertImage_ConvertCommandExecuted_ConvertImageUsedOnce()
			{
				ConvertedImageViewModel convertedImageVM = GetSut();

				convertedImageVM.ConvertCommand.Execute(null);

				_imageProcessingMock.Verify(m => m.ConvertImage(It.IsAny<string>()), Times.Once);
			}


			[Test]
			public void ConvertImage_ConvertCommandExecuted_ConvertedImageSet()
			{
				var bitmapImage = new BitmapImage(new Uri(_testFilesDirectory));
				_imageProcessingMock.Setup(m => m.ConvertImage(It.IsAny<string>()))
					.Returns(bitmapImage);
				var readConvertedImage = GetSut();
				var expected = ImageProcessing.GetBitmapPixels(bitmapImage);

				readConvertedImage.ConvertCommand.Execute(null);
				var actual = ImageProcessing.GetBitmapPixels(readConvertedImage.ConvertedImage);

				Assert.That(actual, Is.EqualTo(expected));
			}

			[Test]
			public void ConvertImage_ConvertCommandExecuted_ConversionTimeSet()
			{
				var readConvertedImage = GetSut();

				readConvertedImage.ConvertCommand.Execute(null);

				Assert.That(readConvertedImage.ConversionTime, Is.Not.EqualTo(null));
			}

			[Test]
			public void ConvertImage_ConvertCommandIsNotExecuted_IsImageConvertedSetToFalse()
			{
				var readConvertedImage = GetSut();

				Assert.That(readConvertedImage.IsImageConverted, Is.False);
			}

			[Test]
			public void ConvertImage_ConvertCommandExecuted_IsImageConvertedSetToTrue()
			{
				var readConvertedImage = GetSut();

				readConvertedImage.ConvertCommand.Execute(null);

				Assert.That(readConvertedImage.IsImageConverted, Is.True);
			}
		}

		[TestFixture]
		public class SaveAsTests : ConvertedImageTests
		{
			private readonly string _outputFileWithoutExt = TestContext.CurrentContext.TestDirectory + @"\\outputFile";
			private Mock<IFileDialog> _fileDialogMock;
			private Mock<IBitmapImageFileExporting> _bitmapFileExportingMock;

			[SetUp]
			public void Setup()
			{
				_imageProcessingMock = new Mock<IImageProcessingAdapter>();
				_fileDialogMock = new Mock<IFileDialog>();
				_bitmapFileExportingMock = new Mock<IBitmapImageFileExporting>();
			}

			private ConvertedImageViewModel GetSutWithExtension(string extension)
			{
				_fileDialogMock.Setup(m => m.FilePath).Returns(_outputFileWithoutExt + extension);
				Presenter.FilePath = _outputFileWithoutExt + extension;
				var readConvertedImage = new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object,
					FileDialog = _fileDialogMock.Object,
					BitmapImageFileExporting = _bitmapFileExportingMock.Object
				};
				return readConvertedImage;
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsJPG()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".jpg");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				_bitmapFileExportingMock.Verify(
				m => m.ExportImageAsFile(It.IsAny<BitmapImage>(), ImageFileFormats.jpg, It.IsAny<string>()), Times.Once);
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsPNG()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".png");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				_bitmapFileExportingMock.Verify(
				m => m.ExportImageAsFile(It.IsAny<BitmapImage>(), ImageFileFormats.png, It.IsAny<string>()), Times.Once);
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsBMP()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".bmp");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				_bitmapFileExportingMock.Verify(
				m => m.ExportImageAsFile(It.IsAny<BitmapImage>(), ImageFileFormats.bmp, It.IsAny<string>()), Times.Once);
			}

			[Test]
			public void SaveAs_SaveAsCommandExecutedWithoutOutputPathSet_DoNothing()
			{
				_fileDialogMock.Setup(m => m.FilePath).Returns<string>(null);
				var readConvertedImage = new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object,
					FileDialog = _fileDialogMock.Object,
					BitmapImageFileExporting = _bitmapFileExportingMock.Object
				};

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				_bitmapFileExportingMock.Verify(
				m => m.ExportImageAsFile(It.IsAny<BitmapImage>(), It.IsAny<ImageFileFormats>(), It.IsAny<string>()), Times.Never);
			}

			[Test]
			public void SaveAs_SaveAsCommandExecutedWithInvalidOutputPathSet_ExceptionThrown()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension("");

				readConvertedImage.ConvertCommand.Execute(null);

				Assert.That(() => readConvertedImage.SaveAsCommand.Execute(null), Throws.Exception.TypeOf<FileFormatException>());
			}
		}
	}
}