using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ImageLibrary.Converters
{
    // <summary>
    /// Similar class to built-in BoolToVisibility converter, however it inverts the result.
    /// </summary>
    public class InverseVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Visible: return false;
                case Visibility.Collapsed: return true;
                case Visibility.Hidden: return true;
            }
            return false;
        }
    }
}
