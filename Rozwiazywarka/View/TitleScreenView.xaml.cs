using Rozwiazywarka.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rozwiazywarka.View
{
    /// <summary>
    /// Interaction logic for TitleScreen.xaml
    /// </summary>
    public partial class TitleScreenView : UserControl
    {
        public TitleScreenView()
        {
            
            InitializeComponent();
        }
        public TitleScreenView(TitleScreenViewModel titleScreenViewModel)
        {
            DataContext = titleScreenViewModel;
            InitializeComponent();
        }

        // Widok nadal nie wie niczego o modelu
        // MVVM jest całe :)
        public SecureString password { set; get; }
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox pb) return;
            if (DataContext != null)
            {
                ((dynamic)DataContext).EncryptionKey = pb.Password;
            }
        }
    }

    
}
