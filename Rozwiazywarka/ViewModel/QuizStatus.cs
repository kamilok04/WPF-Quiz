using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Rozwiazywarka.ViewModel
{
    public class QuizStatus : ObservableObject
    {
        private readonly Quiz.Model.Quiz quiz;
        private readonly string _name;
        private readonly int _totalQuestions;
        private int _currentQuestionIndex;
        private Question _currentQuestion;
        private ObservableCollection<AnswerSelection> _currentAnswers;
        private IndexableProperty<char> _answersGiven;


        public QuizStatus(Quiz.Model.Quiz quiz)
        {
            _name = quiz.Name;
            _totalQuestions = quiz.Questions.Count;
            _answersGiven = new(TotalQuestions);
            _currentQuestionIndex = 0;
            this.quiz = quiz;
            PropertyChanged += QuizStatus_PropertyChanged;
           
        }

        private void QuizStatus_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is null) return;
            switch (e.PropertyName)
            {
                case "CurrentQuestionIndex":
                    // TODO : LoadQuestion
                    CurrentQuestion = quiz.Questions[CurrentQuestionIndex];
                    CurrentAnswers = new(ObservableCollection<AnswerSelection>());
                    break;
            }
        }

        public string Name { get { return _name; } }
        public int TotalQuestions { get { return _totalQuestions; } }
        public int CurrentQuestionIndex
        {
            get { return _currentQuestionIndex; }
            set { _currentQuestionIndex = value;
                OnPropertyChanged("CurrentQuestionIndex");
            }
        }
        public ObservableCollection<Question> Questions
        {
            get { return quiz.Questions; }
        }
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set { _currentQuestion = value; }
        }
        public ObservableCollection<AnswerSelection> CurrentAnswers
        {
            get { return _currentAnswers; }
            set { _currentAnswers = value; }
        }
        public IndexableProperty<char> answersGiven { get { return _answersGiven; } }
    }

}
