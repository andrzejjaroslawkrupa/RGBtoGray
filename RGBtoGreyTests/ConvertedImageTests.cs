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
			private BitmapImage _bitmapImage;
			private Mock<IFileDialog> _fileDialogMock;
			private readonly DirectoryInfo _testDirInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

			[SetUp]
			public void Setup()
			{
				_imageProcessingMock = new Mock<IImageProcessingAdapter>();
				Presenter.FilePath = _testFilesDirectory;
				_bitmapImage = new BitmapImage(new Uri(_testFilesDirectory));
				_fileDialogMock = new Mock<IFileDialog>();
				_imageProcessingMock.Setup(m => m.ConvertImage(It.IsAny<string>())).Returns(_bitmapImage);
			}

			private ConvertedImageViewModel GetSutWithExtension(string extension)
			{
				_fileDialogMock.Setup(m => m.FilePath).Returns(_outputFileWithoutExt + extension);
				var readConvertedImage = new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object,
					FileDialog = _fileDialogMock.Object
				};
				return readConvertedImage;
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsJPG()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".jpg");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				Assert.That(File.Exists(_outputFileWithoutExt + ".jpg"));
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsPNG()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".png");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				Assert.That(File.Exists(_outputFileWithoutExt + ".png"));
			}

			[Test]
			public void SaveAs_SaveAsCommandExecuted_ImageSavedAsBMP()
			{
				ConvertedImageViewModel readConvertedImage = GetSutWithExtension(".bmp");

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				Assert.That(File.Exists(_outputFileWithoutExt + ".bmp"));
			}

			[Test]
			public void SaveAs_SaveAsCommandExecutedWithoutOutputPathSet_DoNothing()
			{
				_fileDialogMock.Setup(m => m.FilePath).Returns<string>(null);
				var readConvertedImage = new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object,
					FileDialog = _fileDialogMock.Object
				};

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				Assert.That(!File.Exists(_outputFileWithoutExt));
			}

			[Test]
			public void SaveAs_SaveAsCommandExecutedWithInvalidOutputPathSet_DoNothing()
			{
				_fileDialogMock.Setup(m => m.FilePath).Returns("");
				var readConvertedImage = new ConvertedImageViewModel
				{
					ImageProcessingAdapter = _imageProcessingMock.Object,
					FileDialog = _fileDialogMock.Object
				};

				readConvertedImage.ConvertCommand.Execute(null);
				readConvertedImage.SaveAsCommand.Execute(null);

				Assert.That(!File.Exists(_outputFileWithoutExt));
			}

			[TearDown]
			public void TearDown()
			{
				foreach (FileInfo file in _testDirInfo.GetFiles())
				{
					if (file.Name.Contains("outputFile"))
						file.Delete();
				}
			}
		}
	}
}