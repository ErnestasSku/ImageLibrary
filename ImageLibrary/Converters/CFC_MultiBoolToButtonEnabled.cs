using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageLibrary.Converters
{
    internal class CFC_MultiBoolToButtonEnabled : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0) return false; 
            List<bool?> resultList = values.Cast<bool?>().ToList();
            if (resultList == null) return false;
            int nulls, falses, trues;
            nulls = falses = trues = 0;
            resultList.ForEach(x =>
            {
                switch (x)
                {
                    case true:
                        trues++;
                        break;
                    case false:
                        falses++;
                        break;
                    case null:
                        nulls++;
                        break;
                }
            });

            if (nulls > 0) return false;
            if (falses > 0) return false;
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] a = new object[] {true, true};
            object[] b = new object[] {false, false};
            return ((bool)value) ? a : b;
        }
    }
}
