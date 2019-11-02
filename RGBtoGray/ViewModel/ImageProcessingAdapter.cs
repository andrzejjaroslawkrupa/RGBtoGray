﻿using System;
using System.Windows.Media.Imaging;
using ImgProcLib;

namespace RGBtoGray.ViewModel
{
	public class ImageProcessingAdapter : IImageProcessingAdapter
	{
		public BitmapImage ConvertImage(string path)
		{
			var imageProcessing = new ImageProcessing();
			var uri = new Uri(path);
			var originalImage = new BitmapImage(uri);

			return imageProcessing.ConvertBitmapImageToGrayscale(originalImage);
		}
	}
}