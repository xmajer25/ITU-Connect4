using System;
using System.Globalization;
using System.Windows.Data;

namespace Connect4.Converters
{
    public class ModeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isPopMode && isPopMode)
            {
                return "POP";
            }
            else
            {
                return "PUT";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}