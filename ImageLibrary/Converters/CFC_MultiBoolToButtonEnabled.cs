using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageLibrary.Converters;

internal class CFC_MultiBoolToButtonEnabled : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0) return false; 
        return values.Cast<bool>().All(x => x == true);

    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        object[] a = new object[] {true, true};
        object[] b = new object[] {false, false};
        return ((bool)value) ? a : b;
    }
}
