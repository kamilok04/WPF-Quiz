using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using Quiz.Model;

namespace Rozwiazywarka.ViewModel
{
    public class AnswerViewModel:  ObservableObject, IPageViewModel
    {
        #region Fields
        private readonly Quiz.Model.Quiz _quiz; // quiz może być dokładnie jeden podczas gry

        bool[] _answers = [false, false, false, false];

        private ICommand? _selectAnswer;
        private ICommand? _stopQuiz;
        private ICommand? _confirmAnswer;


        string IPageViewModel.Name => "AnswerViewModel";

        #endregion
        #region Constructors
        private AnswerViewModel() { }
        public AnswerViewModel(Quiz.Model.Quiz quiz)
        {
            Quiz = quiz;
        }
        #endregion
        #region Properties / Commands
        public Quiz.Model.Quiz Quiz
        {
            get { return _quiz; }
            init
            {
                if (value != _quiz)
                {
                    _quiz = value;
                    OnPropertyChanged("Quiz");
                }
            }
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
                        param => OnSelectAnswer(param),
                        param => param is int index && index >= 0 && index < _answers.Length
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

        

      

        #endregion

        #region Private Helpers

        private void OnConfirmAnswer()
        {

        }

        private void OnStopQuiz() { 
        
        }

        private void OnSelectAnswer(int index)
        {

        }
        #endregion
    }
}
