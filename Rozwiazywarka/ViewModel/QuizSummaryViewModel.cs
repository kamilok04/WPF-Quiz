using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rozwiazywarka.ViewModel
{
    public class QuizSummaryViewModel : ObservableObject, IPageViewModel
    {
        string IPageViewModel.Name => "QuizSummaryViewModel";
    }
}
