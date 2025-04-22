using Quiz.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rozwiazywarka.ViewModel
{
   public class TitleScreenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private FileModel _currentFile = new("","Pliki (*.json)|*.json");
        private ICommand? _setQuizFile;
        private ICommand? _loadQuizCommand;
        private Quiz.Model.Quiz? _loadedQuiz;
     

        string IPageViewModel.Name => "TitleScreen";

        #endregion



        #region Public Properties/Commands
       
 

        public FileModel CurrentFile
        {
            get { return _currentFile; }
            set
            {
                if (value != _currentFile)
                {
                    _currentFile = value;
                    OnPropertyChanged(nameof(CurrentFile));
                    
                }
            }
        }

       
        public Quiz.Model.Quiz LoadedQuiz
        {
            get { return _loadedQuiz; }
            set
            {
                if (value != _loadedQuiz)
                {
                    _loadedQuiz  = value;

                    OnPropertyChanged(nameof(LoadedQuiz));
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
                        param => SetQuizFile()
                        );
                }
                return _setQuizFile;
            }
        }

        public ICommand LoadQuizCommand
        {
            get
            {
                if (_loadQuizCommand == null)
                {
                    _loadQuizCommand = new RelayCommand(
                        param => LoadQuiz(),
                        param => (CurrentFile != null && CurrentFile.FilePathString != "")
                    );
                }
                return _loadQuizCommand;
            }
        }



        #endregion

        #region Private Helpers

        private void SetQuizFile()
        {
            // Spytaj o plik 
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = CurrentFile.FileFilter;
            dialog.FileName = CurrentFile.FilePathString;
            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                CurrentFile.FilePathString = dialog.FileName;
                
            }
        }

        private void LoadQuiz()
        {
            // Spróbuj wczytać plik quizu
            // TODO: szyfrowanie
            
            string jsonString = File.ReadAllText(CurrentFile.FilePathString);

            Quiz.Model.Quiz? quiz = JsonSerializer.Deserialize<Quiz.Model.Quiz>(jsonString);
            if (quiz != null)
            {
                LoadedQuiz = quiz;
                
            }
        }


        #endregion
    }
}
