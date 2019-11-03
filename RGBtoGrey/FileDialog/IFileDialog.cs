namespace RGBtoGrey.FileDialog
{
	public interface IFileDialog
	{
		string FilePath { get; }
		bool? ShowDialog();
	}
}
