using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    public class Quiz
    {
        public string Name { get; set; }

        private ObservableCollection<Question> _questions { get; set; }

        public Quiz()
        {
            Name = "Placeholder";
            Questions = new ObservableCollection<Question>();
        }

        public Quiz(string name, ObservableCollection<Question> questions)
        {
            Name = name;
            Questions = questions;
        }

        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }
        

    }
}
