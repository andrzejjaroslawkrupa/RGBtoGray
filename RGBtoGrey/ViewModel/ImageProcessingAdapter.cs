﻿using System;
using System.Windows.Media.Imaging;
using ImgProcLib;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel
{
	public class ImageProcessingAdapter : IImageProcessingAdapter
	{
		public BitmapSource ConvertImage(string path)
		{
			var uri = new Uri(path);
			var originalImage = new BitmapImage(uri);

			return ImageProcessing.ConvertBitmapImageToGreyscale(originalImage);
		}
	}
}