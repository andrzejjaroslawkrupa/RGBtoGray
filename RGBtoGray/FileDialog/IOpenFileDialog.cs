namespace RGBtoGray.FileDialog
{
	public interface IOpenFileDialog
	{
		string FilePath { get; }
		bool? ShowDialog();
	}
}
