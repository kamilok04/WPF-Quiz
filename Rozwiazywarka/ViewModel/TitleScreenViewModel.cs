using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rozwiazywarka.ViewModel
{
   public class TitleScreenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _productId;
        private FileModel _currentQuiz;
        private ICommand _setQuizFile;
        private ICommand _startQuizCommand;
        string IPageViewModel.Name => "TitleScreen";

        #endregion

        #region Public Properties/Commands

        public FileModel CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                if (value != _currentQuiz)
                {
                    _currentQuiz = value;
                    OnPropertyChanged("CurrentQuiz");
                }
            }
        }

        public ICommand SetQuizFileCommand
        {
            get
            {
                if (_setQuizFile == null)
                {
                    _setQuizFile = new RelayCommand(
                        param => SetQuizFile(),
                        param => (CurrentQuiz != null)
                    );
                }
                return _setQuizFile;
            }
        }

        public ICommand StartQuizCommand
        {
            get
            {
                if (_startQuizCommand == null)
                {
                    _startQuizCommand = new RelayCommand(
                        param => StartQuiz(),
                        param => (CurrentQuiz != null)
                    );
                }
                return _startQuizCommand;
            }
        }


        #endregion

        #region Private Helpers

        private void SetQuizFile()
        {
            // Spytaj o plik 
            FileModel fileSelectionModel = new FileModel();
            fileSelectionModel.FilePathString = CurrentQuiz.FilePathString;
            fileSelectionModel.FileFilter = CurrentQuiz.FileFilter;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                CurrentQuiz.FilePathString = dialog.FileName;
            }
        }

        private void StartQuiz()
        {
            // Spróbuj wczytać plik quizu

        }

        #endregion
    }
}
