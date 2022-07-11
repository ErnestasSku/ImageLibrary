using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ImageLibrary.Converters
{
    /// <summary>
    /// Class which converts bool to specified image.
    /// </summary>
    public class BoolToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new BitmapImage();
            if ((bool)value)
            {
                Uri resourseUri = new Uri(Resources.CheckIcon, UriKind.Relative);
                return new BitmapImage(resourseUri);
            }
            else
            {
                Uri resourseUri = new Uri(Resources.RemoveIcon, UriKind.Relative);
                return new BitmapImage(resourseUri);
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = (BitmapImage)value;
            if (img != null)
            {
                if (img.UriSource == new Uri(Resources.CheckIcon, UriKind.Relative))
                    return true;
                else if (img.UriSource == new Uri(Resources.RemoveIcon, UriKind.Relative))
                    return false;
                else
                    return false;
            }

            return null;
        }
    }
}
