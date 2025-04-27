using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Model;

namespace Rozwiazywarka.Model
{
    public class QuestionSelection : ObservableObject
    {
        private readonly Question _question;
        private readonly int _index;
        private bool _isAnswered = false;

        public QuestionSelection() { _question = new(); }
        public QuestionSelection(Question question, int index)
        {
            _question = question;
            Index = index;
        }

        public Question Question
        {
            get { return _question; }
            init { _question = value; }

        }

        public bool IsAnswered
        {
            get => _isAnswered;
            set
            {
                _isAnswered = value;
                OnPropertyChanged(nameof(IsAnswered));
            }

        }

        public int Index
        {
            get => _index;
            init => _index = value;
        }

    }
}