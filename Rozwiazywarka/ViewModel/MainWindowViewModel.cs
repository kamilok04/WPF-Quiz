﻿using Quiz.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
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


        


        #endregion
        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                _changePageCommand ??= new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);

                return _changePageCommand;
            }
        }


        public List<IPageViewModel> PageViewModels
        {
            get => _pageViewModels ??= [];
   
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
                    OnPropertyChanged(nameof(CurrentPageViewModel));
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
                        AnswerViewModel answerViewModel = new(titleScreenViewModel.LoadedQuiz);
                        answerViewModel.PropertyChanged += AnswerViewModel_PropertyChanged;
                        ChangeViewModel(answerViewModel);
                    }
                    break;


            }
        }

        private void AnswerViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender == null) return;

            switch (e.PropertyName)
            {
                case nameof(AnswerViewModel.QuizInProgress):
                    var answerViewModel = (AnswerViewModel)sender;
                    if (answerViewModel.QuizInProgress == false)
                    {
                        // Koniec quizu, zmień na widok podsumowania
                        QuizSummaryViewModel summary = new(answerViewModel.QuizStatus);
                        summary.PropertyChanged += QuizSummaryViewModel_PropertyChanged;
                        ChangeViewModel(summary);
                    }
                    break;


            }
        }

        private void QuizSummaryViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender == null) return;
            switch (e.PropertyName) {
                case (nameof(QuizSummaryViewModel.ReadyToReturn)):
                    var model = (QuizSummaryViewModel)sender;
                    if (model.ReadyToReturn == true) ChangeViewModel(PageViewModels[0]);
                    break;
            }
        }


        #endregion
    }
}
