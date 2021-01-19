using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    public class NullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("NullConverter can only be used one way.");
        }
    }
}
