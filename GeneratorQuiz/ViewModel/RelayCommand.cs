using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneratorQuiz.ViewModel
{
    class RelayCommand : ICommand
    {
        // Powiadamia interfejs użytkownika, gdy stan możliwości wykonania
        // komendy (CanExecute) się zmienia.
        // W WPF CommandManager.RequerySuggested to statyczne zdarzenie,
        // które informuje, że system powinien ponownie sprawdzić, czy dana
        // komenda może zostać wykonana (CanExecute).
        // To jest istotne np. po zmianie danych, które wpływają na dostępność przycisków/poleceń.
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        // execute przechowuje metodę, którą komenda ma wykonać.
        // canExecute określa, czy komenda może zostać wykonana
        // (np. dezaktywacja przycisku, gdy akcja jest niemożliwa).
        private Action<object> execute;
        private Predicate<object> canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Sprawdza, czy komenda może zostać wykonana.
        // Jeśli canExecute nie jest ustawione, domyślnie zwraca true
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
