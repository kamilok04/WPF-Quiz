using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    public class Question
    {
        public string Text { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }

        public Question()
        {
            Text = "Lorem ipsum?";
            Answers = new ObservableCollection<Answer>();
        }

        public Question(string text, ObservableCollection<Answer> answers)
        {
            Text = text;
            Answers = answers;
        }

        public static explicit operator string(Question q) => q.Text;
    }
}
