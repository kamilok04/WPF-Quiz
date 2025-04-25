using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rozwiazywarka.ViewModel
{
    public static class StyleProperties
    {
        public static string GetBindingString(DependencyObject obj)
            => (string)obj.GetValue(BindingString);
        public static void SetBindingString(DependencyObject obj, string value)
            => obj.SetValue(BindingString, value);
        public static readonly DependencyProperty BindingString =
            DependencyProperty.RegisterAttached(
                "BindingString",
                typeof(string),
                typeof(StyleProperties),
                new PropertyMetadata(null)
                );

    }
}
