using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RGBtoGray.ViewModel
{
    class ReadOriginalImage : ObservableObject
    {
        private string _filename;
        private string _selectedPath;
        private BitmapImage _originalImage;

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                RaisePropertyChangedEvent("Filename");
            }
        }

        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                RaisePropertyChangedEvent("SelectedPath");
            }
        }

        public BitmapImage OriginalImage
        {
            get { return _originalImage; }
            set
            {
                _originalImage = value;
                RaisePropertyChangedEvent("OriginalImage");
            }
        }
        
        public ICommand OpenFileDialogCommand
        {
            get { return new DelegateCommand(OpenFileDialog); }
        }

        private void OpenFileDialog()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.DefaultExt = ".jpg";
            FileDialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
            FileDialog.InitialDirectory = @"C:\";
            FileDialog.Title = "Please select an image file to convert.";
            Nullable<bool> result = FileDialog.ShowDialog();

            if (result == true)
            {
                SelectedPath = FileDialog.FileName;
                ChangeFilenameFromPath(SelectedPath);
                ChangeImageFromPath(SelectedPath);
            }
            else SelectedPath = null;
            
        }

        private void ChangeFilenameFromPath(string Path)
        {
            if (Path != null)
            {
                try
                {
                    string[] directorySplit = Path.Split('\\');
                    Filename = directorySplit[directorySplit.Length - 1];
                }
                catch (Exception e)
                {
                    throw new ApplicationException("ChangeFilename: ", e);
                }
            }
        }

        private void ChangeImageFromPath(string Path)
        {
            try
            {
                OriginalImage = new BitmapImage(new Uri(Path));
            }
            catch(Exception e)
            {
                throw new ApplicationException("ChangeImage: ", e);
            }
        }

    }
}
