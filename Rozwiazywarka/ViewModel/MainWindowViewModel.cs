using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rozwiazywarka.ViewModel
{
    public class MainWindowViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        string IPageViewModel.Name => "MainWindow";



        #endregion
        #region Constructors

        public MainWindowViewModel()
        {
     
            TitleScreenViewModel titleScreenViewModel = new();
            titleScreenViewModel.PropertyChanged += TitleScreenViewModel_PropertyChanged;
            ChangeViewModel(titleScreenViewModel);
        }

        private void TitleScreenViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender == null) return;

            switch (e.PropertyName)
            {
                case nameof(TitleScreenViewModel.LoadedQuiz):
                    var titleScreenViewModel = (TitleScreenViewModel)sender;
                    if (titleScreenViewModel.LoadedQuiz != null)
                    {
                        // Switch to AnswerViewModel when LoadedQuiz is set
                        ChangeViewModel(new AnswerViewModel(titleScreenViewModel.LoadedQuiz));
                    }
                    break;
            }
        }


        #endregion
        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }


        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }



        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }


        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }



        #endregion
    }
}
