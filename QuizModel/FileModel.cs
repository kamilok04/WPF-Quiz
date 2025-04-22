using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quiz.Model
{
    public class FileModel : ObservableObject
    {
        #region Fields

        private string _filePathString = "piwo";
        private string _fileFilter = "All files (*.*)|*.*";

        #endregion // Fields
        #region Constructors
        public FileModel() { }
        public FileModel(string filePathString)
        {
            _filePathString = filePathString;
        }
        public FileModel(string filePathString, string fileFilter)
        {
            _filePathString = filePathString;
            _fileFilter = fileFilter;
        }
        #endregion
        #region Properties

        public string FilePathString
        {
            get { return _filePathString; }
            set
            {

                if (value != _filePathString)
                {
                    _filePathString = value;
                    OnPropertyChanged(nameof(FilePathString));
                }
            }
        }

        public string FileFilter
        {
            get { return _fileFilter; }
            set
            {
                if (value != _fileFilter)
                {
                    _fileFilter = value;
                   // OnPropertyChanged("FileFilter");
                }
            }
        }

        #endregion // Properties


    }
}
