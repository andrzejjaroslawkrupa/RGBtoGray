using System;

namespace RGBtoGrey.ViewModel.Interfaces
{
	public interface IFileLocation
	{
		IObservable<string> GetFileLocationsObservable { get; }

		void SetNewLocation(string path);
	}
}