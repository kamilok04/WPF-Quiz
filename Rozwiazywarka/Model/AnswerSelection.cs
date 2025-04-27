using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Rozwiazywarka.Model
{
    public class AnswerSelection : ObservableObject
    {
         private readonly Answer _answer;
        private bool _isSelected = false;

        public AnswerSelection(Answer answer, bool selected = false)
        {
            _answer = answer;
            _isSelected = selected;
        }

    
        

        public AnswerSelection() { _answer = new(); _isSelected = false; }


        public Answer Answer
        {
            get { return _answer; }
        }

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
