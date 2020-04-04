using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using RGBtoGrey.ViewModel.Interfaces;

namespace RGBtoGrey.ViewModel.FileManagement
{
	public class FileLocation : IFileLocation
	{
		private readonly Subject<string> _pathSubject;

		public FileLocation()
		{
			_pathSubject = new Subject<string>();
		}

		public IObservable<string> GetFileLocationsObservable => _pathSubject.AsObservable();

		public void SetNewLocation(string path)
		{
			_pathSubject.OnNext(path ?? "");
		}
	}
}