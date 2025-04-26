using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Rozwiazywarka.ViewModel
{
    public class Gap
    {
        // Ustawia margines dla wszystkich dzieci kontrolki, jak gap w CSS
        public static Thickness GetMargin(DependencyObject obj) => (Thickness)obj.GetValue(MarginProperty);
        public static void SetMargin(DependencyObject obj, Thickness value) => obj.SetValue(MarginProperty, value);

        public static readonly DependencyProperty MarginProperty =
    DependencyProperty.RegisterAttached(nameof(FrameworkElement.Margin), typeof(Thickness),
        typeof(Gap), new UIPropertyMetadata(new Thickness(), MarginChangedCallback));
        public static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        { 
            var panel = sender as Panel;
            if (panel == null) return;
            panel.Loaded += Panel_Loaded;
        }

        private static void Panel_Loaded(object sender, EventArgs e)
        {
            var panel = sender as Panel;
            // Ustaw margines dla każdego dziecka
            foreach (FrameworkElement fe in panel.Children.OfType<FrameworkElement>())
                fe.Margin = GetMargin(panel);
        }
    }
}
