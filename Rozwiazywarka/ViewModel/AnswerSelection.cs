using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rozwiazywarka.ViewModel
{
    class AnswerSelection : ObservableObject
    {
         private readonly Answer _answer;
        private bool _isSelected = false;

        public AnswerSelection(Answer answer)
        {
            _answer = answer;
        }

        public AnswerSelection() { }



        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }



    }
}
