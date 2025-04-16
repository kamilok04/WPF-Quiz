using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Quiz.Model
{
    public class FileModel : ObservableObject
    {
        #region Fields

        private string _filePathString = ".";
        private string _fileFilter = "All files (*.*)|*.*";

        #endregion // Fields

        #region Properties

        public string FilePathString
        {
            get { return _filePathString; }
            set
            {

                if (value != _filePathString)
                {
                    _filePathString = value;
                    OnPropertyChanged("FilePath");
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
