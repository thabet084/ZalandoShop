using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ZalandoShop.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && parameter is string && ((string)parameter).ToLower() == "negate")
            {
                return (value is bool && !(bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && parameter is string && ((string)parameter).ToLower() == "negate")
            {
                return value is Visibility && (Visibility)value != Visibility.Visible;
            }
            else
            {
                return value is Visibility && (Visibility)value == Visibility.Visible;
            }
        }
     
    }
}
