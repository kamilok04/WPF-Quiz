using Quiz.Model;
using Rozwiazywarka.ViewModel;
using System.Collections.ObjectModel;

namespace Rozwiazywarka.Model
{
    public class QuizStatus
    {
        private readonly Quiz.Model.Quiz quiz;
        private readonly string _name;
        private readonly int _totalQuestions;


        private ObservableCollection<QuestionSelection> _questions;
        private int _questionsAnswered = 0;
        private IndexableProperty<List<bool>> _confirmedAnswers;


        public QuizStatus()
        {

        }
        public QuizStatus(Quiz.Model.Quiz quiz)
        {
            Name = quiz.Name;
            Questions = [.. quiz.Questions.Select<Question, QuestionSelection>(a => new(a, quiz.Questions.IndexOf(a)))];
            TotalQuestions = Questions.Count;
            ConfirmedAnswers = new(TotalQuestions);
           
            this.quiz = quiz;


        }



        public string Name { get { return _name; } init { _name = value; } }
        public int TotalQuestions { get { return _totalQuestions; } init { _totalQuestions = value; } }


        public ObservableCollection<QuestionSelection> Questions
        {
            get { return _questions; }
            init { _questions =  value; }
        }


        public int QuestionsAnswered
        {
            get { return _questionsAnswered; }
            set {_questionsAnswered = _questionsAnswered > TotalQuestions ? TotalQuestions :  value; }
        }
 
        public IndexableProperty<List<bool>> ConfirmedAnswers
        {
            get { return _confirmedAnswers; }
            init
            {
                _confirmedAnswers = value;
            }

        }

    }

}
