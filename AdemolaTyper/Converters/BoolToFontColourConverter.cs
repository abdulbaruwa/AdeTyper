using System;
using System.Globalization;
using System.Windows.Data;

namespace AdemolaTyper.Converters
{
    public class BoolToFontColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (Boolean) value;
            switch (state)
            {
                case true:
                    return System.Windows.Media.Brushes.AliceBlue;
                default:
                    return System.Windows.Media.Brushes.OrangeRed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}