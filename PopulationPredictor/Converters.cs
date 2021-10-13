// Group2: Jingfei Yao, Grace Pauly, Xiaotong Han.
using System;
using System.Globalization;
using System.Windows.Data;

namespace PopulationPredictor
{
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.GetType() == typeof(int) ? value.ToString() : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(string))
            {
                bool success = int.TryParse((string)value, out int days);
                return success ? days : 0;
            }
            else
            {
                return 0;
            }
        }
    }

    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
