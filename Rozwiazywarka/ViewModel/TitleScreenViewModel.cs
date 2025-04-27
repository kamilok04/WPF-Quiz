using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Rozwiazywarka.ViewModel
{
   public class TitleScreenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private FileModel _currentFile = new("", "Wszystkie pliki (*.*)|*.*"); // new("","Pliki (*.json)|*.json");
        private string _encryptionKey;
        private ICommand? _setQuizFile;
        private ICommand? _loadQuizCommand;
        private string _errorString;
        private ICommand? _pasteEncryptionKeyCommand;
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

        public ICommand PasteEncryptionKeyCommand
        {
            get
            {
                _pasteEncryptionKeyCommand ??= new RelayCommand(
                    param => PasteEncryptionKey()
                    );
                return _pasteEncryptionKeyCommand;
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

        public string EncryptionKey
        {
            set
            {
                _encryptionKey = value;
            }
            get => _encryptionKey;
        }


        public string ErrorString
        {
            set
            {
                _errorString = value;
                OnPropertyChanged(nameof(ErrorString));
            }
            get => _errorString;
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
            
            
            string encryptedJsonString = File.ReadAllText(CurrentFile.FilePathString);
            string decryptedJsonString = "";
            try { decryptedJsonString = AESHelper.Decrypt(encryptedJsonString, EncryptionKey); }
            catch(Exception e) { QuizLoadingError(e.Message);  return; }
            

            Quiz.Model.Quiz? quiz = JsonSerializer.Deserialize<Quiz.Model.Quiz>(decryptedJsonString);
            if (quiz != null)
            {
                LoadedQuiz = quiz;

            }
            else QuizLoadingError();
        }

        private void QuizLoadingError(string extraInfo = "")
        {
            ErrorString = "Błąd ładowania quizu! Dodatkowe dane: " + extraInfo;
        }

        private void PasteEncryptionKey() => EncryptionKey = Clipboard.GetText();
        

        #endregion
    }
}
