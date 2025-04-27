using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.Json;
using Microsoft.Win32;
using Quiz.Model;

namespace GeneratorQuiz.ViewModel
{
    class MainWindowViewModel : ObservableObject
    {
        public Quiz.Model.Quiz newQuiz { get; set; } = new Quiz.Model.Quiz();


        private bool _isVisible = false; 
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        private string? _question;

        public string? Question
        {
            get => _question;
            set
            {
                if(_question != value)
                {
                    _question = value;
                    OnPropertyChanged(nameof(Question));
                }
            }
        }

        private string? _answer1;

        public string? Answer1
        {
            get => _answer1;
            set
            {
                if (_answer1 != value)
                {
                    _answer1 = value;
                    OnPropertyChanged(nameof(Answer1));
                }
            }
        }

        private bool _a1IsTrue;
        public bool A1IsTrue
        {
            get => _a1IsTrue;
            set
            {
                if(_a1IsTrue != value)
                {
                    _a1IsTrue = value;
                    OnPropertyChanged(nameof(A1IsTrue));
                }
            }
        }

        private string? _answer2;

        public string? Answer2
        {
            get => _answer2;
            set
            {
                if (_answer2 != value)
                {
                    _answer2 = value;
                    OnPropertyChanged(nameof(Answer2));
                }
            }
        }

        private bool _a2IsTrue;
        public bool A2IsTrue
        {
            get => _a2IsTrue;
            set
            {
                if (_a2IsTrue != value)
                {
                    _a2IsTrue = value;
                    OnPropertyChanged(nameof(A2IsTrue));
                }
            }
        }

        private string? _answer3;

        public string? Answer3
        {
            get => _answer3;
            set
            {
                if (_answer3 != value)
                {
                    _answer3 = value;
                    OnPropertyChanged(nameof(Answer3));
                }
            }
        }

        private bool _a3IsTrue;
        public bool A3IsTrue
        {
            get => _a3IsTrue;
            set
            {
                if (_a3IsTrue != value)
                {
                    _a3IsTrue = value;
                    OnPropertyChanged(nameof(A3IsTrue));
                }
            }
        }

        private string? _answer4;

        public string? Answer4
        {
            get => _answer4;
            set
            {
                if (_answer4 != value)
                {
                    _answer4 = value;
                    OnPropertyChanged(nameof(Answer4));
                }
            }
        }

        private bool _a4IsTrue;
        public bool A4IsTrue
        {
            get => _a4IsTrue;
            set
            {
                if (_a4IsTrue != value)
                {
                    _a4IsTrue = value;
                    OnPropertyChanged(nameof(A4IsTrue));
                }
            }
        }

        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }

            }

        }
        private Question? _selectedQuestion;

        public Question? SelectedQuestion 
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != value && value is not null)
                {   
                    _selectedQuestion = value;
                    Question = value.Text;
                    Answer1 = value.Answers[0].Text;
                    Answer2 = value.Answers[1].Text;
                    Answer3 = value.Answers[2].Text;
                    Answer4 = value.Answers[3].Text;
                    A1IsTrue = value.Answers[0].IsCorrect;
                    A2IsTrue = value.Answers[1].IsCorrect;
                    A3IsTrue = value.Answers[2].IsCorrect;
                    A4IsTrue = value.Answers[3].IsCorrect;
                    OnPropertyChanged(nameof(SelectedQuestion));
                    OnPropertyChanged(nameof(Answer1));
                    OnPropertyChanged(nameof(Answer2));
                    OnPropertyChanged(nameof(Answer3));
                    OnPropertyChanged(nameof(Answer4));
                    OnPropertyChanged(nameof(A1IsTrue));
                    OnPropertyChanged(nameof(A2IsTrue));
                    OnPropertyChanged(nameof(A3IsTrue));
                    OnPropertyChanged(nameof(A4IsTrue));
                    OnPropertyChanged(nameof(Question));
                }
            }
        }

        private ICommand? _createNewQuiz;
        public ICommand CreateNewQuiz
        {
            get
            {
                if(_createNewQuiz == null)
                {
                    _createNewQuiz = new RelayCommand(
                        p =>
                        {
                            IsVisible = true;
                            Question = Answer1 = Answer2 = Answer3 = Answer4 = Name = String.Empty;
                            A1IsTrue = A2IsTrue = A3IsTrue = A4IsTrue = false;
                            newQuiz = new Quiz.Model.Quiz();
                            OnPropertyChanged(nameof(newQuiz));
                        },
                        p => true

                    );

                }

                return _createNewQuiz;
            }
        }

        private ICommand? _addQuestion;

        public ICommand AddQuestion
        {
            get
            {
                if(_addQuestion == null)
                {
                    _addQuestion = new RelayCommand(
                        p =>
                        {
                            ObservableCollection<Answer> answers = new ObservableCollection<Answer>();
                            answers.Add(new Answer(Answer1, A1IsTrue));
                            answers.Add(new Answer(Answer2, A2IsTrue));
                            answers.Add(new Answer(Answer3, A3IsTrue));
                            answers.Add(new Answer(Answer4, A4IsTrue));
                            Question newQuestion = new Question(Question, answers);
                            newQuiz.Questions.Add(newQuestion);
                            Question = Answer1 = Answer2 = Answer3 = Answer4 = String.Empty;
                            A1IsTrue = A2IsTrue = A3IsTrue = A4IsTrue = false;

                        },
                        p => (!String.IsNullOrEmpty(Question) && !String.IsNullOrEmpty(Answer1) && !String.IsNullOrEmpty(Answer2) && 
                        !String.IsNullOrEmpty(Answer3) && !String.IsNullOrEmpty(Answer4) && (A1IsTrue || A2IsTrue || A3IsTrue || A4IsTrue))
                        );
                }

                return _addQuestion;
            }
        }

        private ICommand? _delQuestion;

        public ICommand DelQuestion
        {
            get
            {
                if (_delQuestion == null)
                {
                    _delQuestion = new RelayCommand(
                        p =>
                        {
                            newQuiz.Questions.Remove(SelectedQuestion);

                        },
                        p => (!String.IsNullOrEmpty(Question) && !String.IsNullOrEmpty(Answer1) && !String.IsNullOrEmpty(Answer2) &&
                        !String.IsNullOrEmpty(Answer3) && !String.IsNullOrEmpty(Answer4) && (A1IsTrue || A2IsTrue || A3IsTrue || A4IsTrue) && (SelectedQuestion is not null)
                        && newQuiz.Questions.IndexOf(SelectedQuestion) >= 0)
                        );
                }

                return _delQuestion;
            }
        }

        public ICommand? _editQuestion;

        public ICommand EditQuestion
        {
            get
            {
                if (_editQuestion == null)
                {
                    _editQuestion = new RelayCommand(
                        p =>
                        {
                            ObservableCollection<Answer> answers = new ObservableCollection<Answer>();
                            answers.Add(new Answer(Answer1, A1IsTrue));
                            answers.Add(new Answer(Answer2, A2IsTrue));
                            answers.Add(new Answer(Answer3, A3IsTrue));
                            answers.Add(new Answer(Answer4, A4IsTrue));
                            Question newQuestion = new Question(Question, answers);
                            newQuiz.Questions[newQuiz.Questions.IndexOf(SelectedQuestion)] = newQuestion;
                            Question = Answer1 = Answer2 = Answer3 = Answer4 = String.Empty;
                            A1IsTrue = A2IsTrue = A3IsTrue = A4IsTrue = false;

                        },
                        p => (!String.IsNullOrEmpty(Question) && !String.IsNullOrEmpty(Answer1) && !String.IsNullOrEmpty(Answer2) &&
                        !String.IsNullOrEmpty(Answer3) && !String.IsNullOrEmpty(Answer4) && (A1IsTrue || A2IsTrue || A3IsTrue || A4IsTrue) && (SelectedQuestion is not null)
                        && newQuiz.Questions.IndexOf(SelectedQuestion) >= 0)
                        );
                }

                return _editQuestion;
            }
        }

        private ICommand? _saveQuiz;

        public ICommand SaveQuiz
        {
            get
            {
                if (_saveQuiz == null)
                {
                    _saveQuiz = new RelayCommand(
                        p =>
                        {
                            newQuiz.Name = Name;
                            SaveFileDialog saveFileDialog = new SaveFileDialog
                            {
                                Filter = "All files (*.*)|*.*",
                                Title = "Zapisz quiz"
                            };

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                string jsonString = JsonSerializer.Serialize(newQuiz, new JsonSerializerOptions { WriteIndented = true });
                                string key = "bpzqCj9mQ2L6kDWh"; 
                                string encryptedJson = AESHelper.Encrypt(jsonString, key);
                                File.WriteAllText(saveFileDialog.FileName, encryptedJson);
                                MessageBox.Show("Quiz zapisany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                        },
                        p => (!String.IsNullOrEmpty(Name) && (newQuiz.Questions.Count >= 1))
                        );
                }

                return _saveQuiz;
            }

        }

        private ICommand? _loadQuiz;

        public ICommand LoadQuiz
        {
            get
            {
                if (_loadQuiz == null)
                {
                    _loadQuiz = new RelayCommand(
                        p =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog
                            {
                                Filter = "All files (*.*)|*.*",
                                Title = "Wczyatj quiz"
                            };

                            if (openFileDialog.ShowDialog() == true)
                            {
                                string encryptedJson = File.ReadAllText(openFileDialog.FileName);
                                string key = "bpzqCj9mQ2L6kDWh"; 
                                string jsonString = AESHelper.Decrypt(encryptedJson, key);
                                newQuiz = JsonSerializer.Deserialize<Quiz.Model.Quiz>(jsonString);
                                Name = newQuiz.Name;
                                OnPropertyChanged(nameof(newQuiz));
                                IsVisible = true;
                            }

                        },
                        p => true
                        );
                }

                return _loadQuiz;
            }

        }

    }
}
