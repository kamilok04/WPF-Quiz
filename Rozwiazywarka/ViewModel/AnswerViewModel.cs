using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Threading;
using Quiz.Model;
using Rozwiazywarka.Model;

namespace Rozwiazywarka.ViewModel
{
    public class AnswerViewModel:  ObservableObject, IPageViewModel
    {
        #region Fields
        private QuizStatus _quizStatus;
        private bool _quizInProgress = false;
        
        private ICommand? _selectAnswer;
        private ICommand? _selectQuestion;
        private ICommand? _stopQuiz;
        private ICommand? _confirmAnswer;
        private ICommand? _startQuiz;
        private int _currentQuestionIndex;
        private QuestionSelection _currentQuestion;
        private ObservableCollection<AnswerSelection> _currentAnswers;

        private readonly Timer timer = new();
        private int _timeElapsed = 0;
        private string _timeString = "00:00:00";



        string IPageViewModel.Name => "AnswerViewModel";

       
        #endregion
        #region Constructors
        public AnswerViewModel() { }
        public AnswerViewModel(Quiz.Model.Quiz quiz)
        {
            QuizStatus = new(quiz);
            PropertyChanged += AnswerViewModel_PropertyChanged;
            timer.PropertyChanged += Timer_PropertyChanged;
            CurrentQuestionIndex = 0;
        }

        
        #endregion
        #region Properties / Commands
        public QuizStatus QuizStatus
        {
            get { return _quizStatus; }
            init { _quizStatus = value; }
        }
        public int CurrentQuestionIndex
        {
            get { return _currentQuestionIndex; }
            set
            {
                _currentQuestionIndex = value;

                OnPropertyChanged(nameof(CurrentQuestionIndex));
                OnPropertyChanged(nameof(CurrentQuestionIndexFormatted));

            }
        }
        public int CurrentQuestionIndexFormatted
        {
            get { return _currentQuestionIndex + 1; }
        }

        public QuestionSelection CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }
        public ObservableCollection<AnswerSelection> CurrentAnswers
        {
            get { return _currentAnswers; }
            set { _currentAnswers = value; }
        }
        public bool QuizInProgress
        {
            get { return _quizInProgress; }
            set {  _quizInProgress = value;
            OnPropertyChanged(nameof(QuizInProgress));}

        }
        public ICommand StartQuiz
        {
            get
            {
                if (_startQuiz == null)
                {
                    _startQuiz = new RelayCommand(
                        param => InitializeQuiz(),
                        param => !QuizInProgress
                        );
                }
                return _startQuiz;
            }
        }
        public ICommand SelectQuestion
        {
            get
            {
                if (_selectQuestion == null)
                {
                    _selectQuestion = new RelayCommand(
                        param => OnSelectQuestion(param),
                        param => QuizInProgress
                        );
                }
                return _selectQuestion;
            }
        }


        public ICommand ConfirmAnswer
        {
            get
            {
                if (_confirmAnswer == null)
                {
                    _confirmAnswer = new RelayCommand(
                        param => OnConfirmAnswer(param),
                        param => QuizInProgress
                        );
                }
                return _confirmAnswer;
            }
        }

        public ICommand SelectAnswer
        {
            get
            {
                if (_selectAnswer == null)
                {
                    _selectAnswer = new RelayCommand(
                        param => OnSelectAnswer(param),
                        param => QuizInProgress

                        );
                }
                return _selectAnswer;
            }
        }

        public ICommand StopQuiz
        {
            get
            {
                if (_stopQuiz == null)
                {
                    _stopQuiz = new RelayCommand(
                        param => OnStopQuiz(),
                        param => QuizInProgress
                        );
                }
                return _stopQuiz ;
            }
        }

 
        public string TimeString
        {
            get { return _timeString; }
            set
            {
                _timeString = value;
                OnPropertyChanged(nameof(TimeString));
            }

        }



        #endregion

        #region Private Helpers

        private void InitializeQuiz()
        {
            
            QuizInProgress = true;
            timer.Start();
         
        }

        private void Timer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender == null) return;
            switch (e.PropertyName)
            {
                case "TimeElapsed":
                    _timeElapsed += 1;
                    TimeString = TimeSpan.FromSeconds(_timeElapsed).ToString(@"hh\:mm\:ss");
                    break;
            }
        }

        private void OnConfirmAnswer(object param)
        {
            if (param is not string) return;
            if (!int.TryParse((string)param, out int offset)) return;
            CurrentQuestion.IsAnswered = true;
            QuizStatus.QuestionsAnswered += 1;
            ProcessAnswers(CurrentQuestionIndex);
            if (CurrentQuestionIndex + offset >= QuizStatus.TotalQuestions) OnStopQuiz();
            else CurrentQuestionIndex += offset;
        }

        private void OnStopQuiz() {
            MessageBoxResult result;
            if (QuizStatus.QuestionsAnswered == QuizStatus.TotalQuestions)
            {
                string text = "To zakończy quiz. Na pewno kontynuować?";
                string title = "Koniec quizu";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Question;
                result = MessageBox.Show(text, title, button, image, MessageBoxResult.No);
            }
            else
            {
                string text = "To zakończy quiz, a nie udzielono odpowiedzi na wszystkie pytania. Na pewno kontynuować?";
                string title = "Koniec quizu";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage image = MessageBoxImage.Warning;
                result = MessageBox.Show(text, title, button, image, MessageBoxResult.No);
            }

            if (result == MessageBoxResult.Yes)
                QuizInProgress = false;
                
        }

        private void OnSelectAnswer(object param)
        {
            if (param is not AnswerSelection) return;
            var answer = (AnswerSelection)param;
            answer.IsSelected = !answer.IsSelected;
            CurrentQuestion.IsAnswered = true;
            QuizStatus.QuestionsAnswered += 1;
            
        }
        private void OnSelectQuestion(object param)
        {
            // if (param is not QuestionSelection question) return;
            if (param is not int newIndex) return;
            ProcessAnswers(CurrentQuestionIndex);
            CurrentQuestionIndex = newIndex;
        }

        private void ProcessAnswers(int index)
        {
            
 
            List<bool> selectedAnswers = new();
            foreach (AnswerSelection answer in CurrentAnswers)
            {
                selectedAnswers.Add(answer.IsSelected);
            }
            
            QuizStatus.ConfirmedAnswers[index] = selectedAnswers;
        }

        private void AnswerViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is null) return;
            switch (e.PropertyName)
            {
                case "CurrentQuestionIndex":
                    LoadQuestion(CurrentQuestionIndex);
                    break;
            }
        }
        private void LoadQuestion(int index)
        {
            CurrentQuestion = QuizStatus.Questions[CurrentQuestionIndex];
            if (CurrentAnswers != null)
                CurrentAnswers.Clear();
            else CurrentAnswers = new();
            for (int i = 0; i < CurrentQuestion.Question.Answers.Count; i++)
            {
                List<bool>? answered = QuizStatus.ConfirmedAnswers[CurrentQuestionIndex];
                CurrentAnswers.Add(new(CurrentQuestion.Question.Answers[i],
                    answered != null && answered[i]));

            }
        }

        #endregion
    }

    public class AnswerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (values == null || values.Length < 2 || values[1] is not ObservableCollection<AnswerSelection> answers || answers.Count < 1)
                return "";

            return (char)( answers.IndexOf((AnswerSelection) values[0]) + 'A');
        }

        public object[] ConvertBack(object value, Type[] targetTypes,  object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
        

    }

    public class AnswerStyleConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => (bool)value == true ? "SelectedAnswer" : "UnselectedAnswer";
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
    public class GetIndexMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || values[1] is not ObservableCollection<QuestionSelection> questions || questions.Count < 1)
                return "";
            var itemIndex = questions.IndexOf((QuestionSelection)values[0]);

            return itemIndex;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException("GetIndexMultiConverter_ConvertBack");
        
    }
}
