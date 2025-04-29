using Quiz.Model;
using Rozwiazywarka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Rozwiazywarka.ViewModel
{
    public class QuizSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        string IPageViewModel.Name => "QuizSummaryViewModel";
        private readonly QuizStatus _status;
        private readonly string _timeElapsedFormatted;
        private readonly int _score;
        private bool _readyToReturn = false;
        private readonly AnswerViewModel _answerViewModel;
        private ICommand _returnToTitleCommand;

        #endregion

        #region Constructors
        public QuizSummaryViewModel() { }
        public QuizSummaryViewModel(QuizStatus status)
        {
            QuizStatus = status;
            Score = CountPoints();
            TimeElapsedFormatted = TimeSpan.FromSeconds(QuizStatus.TotalTimeElapsed).ToString(@"hh\:mm\:ss");
            QuizStatus retrospective = CreateRetrospective();
            AnswerViewModel = new(retrospective);
        }
        #endregion

        #region Public Properties / Commands

        public QuizStatus QuizStatus
        {
            get => _status;
            init => _status = value;

     
        }
        public bool ReadyToReturn
        {
            get => _readyToReturn;
            set => _readyToReturn = value;
        }
        public int Score
        {
            get => _score;
            init => _score = value;
        }
        public string TimeElapsedFormatted
        {
            get => _timeElapsedFormatted;
            init => _timeElapsedFormatted = value;
        }

        public AnswerViewModel AnswerViewModel
        {
            get => _answerViewModel;
            init => _answerViewModel = value;

        }

        public ICommand ReturnToTitleCommand
        {
            get
            {
                if (_returnToTitleCommand == null)
                {
                    _returnToTitleCommand = new RelayCommand(
                        param => { ReadyToReturn = true; OnPropertyChanged(nameof(ReadyToReturn)); }
                        ); 
                }
                return _returnToTitleCommand;
            }
        }
        #endregion

        #region Private Helpers
        private int CountPoints()
        {
            int score = 0;
            for(int i = 0; i < QuizStatus.TotalQuestions; i++)
            {
                List<bool> correctAnswers = [];
                foreach (Answer answer in QuizStatus.Quiz.Questions[i].Answers)
                {
                    correctAnswers.Add(answer.IsCorrect);
                }

                List<bool> selectedAnswers = QuizStatus.ConfirmedAnswers[i];
                if (selectedAnswers is null) continue;
                if (correctAnswers.SequenceEqual(selectedAnswers)) score++;
            }
            return score;
        }

        private QuizStatus CreateRetrospective()
        {
            QuizStatus retrospective = new(QuizStatus.Quiz);
            for (int i = 0; i < QuizStatus.TotalQuestions; i++)
            {
                List<bool> correctAnswers = [];
                foreach (Answer answer in QuizStatus.Quiz.Questions[i].Answers)
                {
                    correctAnswers.Add(answer.IsCorrect);
                }

                List<bool> selectedAnswers = QuizStatus.ConfirmedAnswers[i];
                selectedAnswers ??= [.. Enumerable.Repeat(false, correctAnswers.Count)];

                bool questionCorrect = true;
                retrospective.ConfirmedAnswers[i] = [..Enumerable.Repeat(false, correctAnswers.Count)];
                for (int j = 0; j < selectedAnswers.Count; j++)
                {
                    if (selectedAnswers[j] == correctAnswers[j])
                    {
                        retrospective.ConfirmedAnswers[i][j] = true;
                      
                    }
                    questionCorrect = correctAnswers.SequenceEqual(selectedAnswers);

                }
                retrospective.Questions[i].IsAnswered = questionCorrect;
                
            }
            return retrospective;
        }

 

        #endregion

    }
}
