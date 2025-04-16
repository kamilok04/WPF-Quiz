using Rozwiazywarka.View;
using Rozwiazywarka.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Quiz
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindowView app = new();
            MainWindowViewModel context = new();
            app.DataContext = context;
            app.Show();
        }
    }

}
