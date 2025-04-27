using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Quiz.Model;

namespace GeneratorQuiz.ViewModel
{
    class MainWindowViewModel : ObservableObject
    {
        Quiz.Model.Quiz newQuiz = new Quiz.Model.Quiz();

        private bool _isVisible = true; // ma być false
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

                        },
                        p => (!String.IsNullOrEmpty(Question) && !String.IsNullOrEmpty(Answer1) && !String.IsNullOrEmpty(Answer2) && 
                        !String.IsNullOrEmpty(Answer3) && !String.IsNullOrEmpty(Answer4) && (A1IsTrue || A2IsTrue || A3IsTrue || A4IsTrue))
                        );
                }
            }
        }
    }
}
