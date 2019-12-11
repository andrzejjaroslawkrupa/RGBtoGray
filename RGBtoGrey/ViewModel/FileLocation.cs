using RGBtoGrey.ViewModel.Interfaces;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RGBtoGrey.ViewModel
{
	class FileLocation : IFileLocation
	{
		private readonly Subject<string> _pathSubject;

		public FileLocation()
		{
			_pathSubject = new Subject<string>();
		}

		public IObservable<string> GetFileLocationsObservable => _pathSubject.AsObservable();

		public void SetNewLocation(string path)
		{
			if (path == null)
				_pathSubject.OnNext("");
			else
				_pathSubject.OnNext(path);
		}
	}
}
