using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Threading;
using Quiz.Model;

namespace Rozwiazywarka.ViewModel
{
    public class AnswerViewModel:  ObservableObject, IPageViewModel
    {
        #region Fields
        private QuizStatus _quizStatus;
        IndexableProperty<bool> _selectedAnswers = new([false, false, false, false]);

        private ICommand? _selectAnswer;
        private ICommand? _stopQuiz;
        private ICommand? _confirmAnswer;
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
            // Zrób nawigację
           
            InitializeQuiz();
            timer.PropertyChanged += Timer_PropertyChanged;
        }

        
        #endregion
        #region Properties / Commands
        public QuizStatus QuizStatus
        {
            get { return _quizStatus; }
            init { _quizStatus = value; }
        }

        
       public bool[] Answers
        {
            get { return _selectedAnswers; }
         
        }


        public ICommand ConfirmAnswer
        {
            get
            {
                if (_confirmAnswer == null)
                {
                    _confirmAnswer = new RelayCommand(
                        param => OnConfirmAnswer()
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
                        param => OnSelectAnswer(param)
                        
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
                        param => OnStopQuiz()
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

            QuizStatus.CurrentQuestionIndex = 0;
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

        private void OnConfirmAnswer()
        {

        }

        private void OnStopQuiz() { 
            
        }

        private void OnSelectAnswer(object param)
        {
            var c = (char)param;
            if (c <= 'A' || c >= 'Z') return;
            int index = c - 'A';
            Answers[index] = !Answers[index];
        }
        
        #endregion
    }

    public class AnswerConverter : IMultiValueConverter
    {
        private char index='A';
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
            
            if (values == null || values.Length < 2 || values[1] is not ObservableCollection<Answer> answers || answers.Count < 1)
                return "";

            if(index <= 'A' || index >= 'Z') index = 'A';
            return index++;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,  object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();

    }
}
