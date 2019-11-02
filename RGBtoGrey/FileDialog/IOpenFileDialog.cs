namespace RGBtoGrey.FileDialog
{
	public interface IOpenFileDialog
	{
		string FilePath { get; }
		bool? ShowDialog();
	}
}
