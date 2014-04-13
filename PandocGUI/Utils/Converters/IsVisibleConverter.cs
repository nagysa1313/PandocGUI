using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PandocGUI.Utils.Converters
{
    /// <summary>
    /// Two state IsVisibleConverter, converts bool to Visibility and vica versa.
    /// Pairing: true <-> Visible, false <-> Collapsed. Hidden state is omitted cause of rare usage.
    /// </summary>
    public class IsVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                var isTrue = (value as bool?) ?? false;
                if (parameter is string && (parameter as string).ToLower().Contains("inverse"))
                    isTrue = !isTrue;
                if (isTrue)
                    return Visibility.Visible;
                else return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility)
            {
                var isVis = ((value as Visibility?) ?? Visibility.Collapsed) == Visibility.Visible;
                if (parameter is string && (parameter as string).ToLower().Contains("inverse"))
                    isVis = !isVis;
                return isVis;
            }

            return false;
        }
    }
}