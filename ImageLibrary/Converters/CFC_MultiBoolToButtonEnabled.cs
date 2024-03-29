﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageLibrary.Converters;

/// <summary>
/// Aggregates a list of bools.
/// </summary>
public class CFC_MultiBoolToButtonEnabled : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length == 0) return false;
        try
        {
            return values.Cast<bool>().All(x => x == true);

        }
        catch (InvalidCastException)
        {
            return false;
        }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        object[] a = new object[] {true, true};
        object[] b = new object[] {false, false};
        return ((bool)value) ? a : b;
    }
}
