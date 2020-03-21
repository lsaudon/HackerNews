using System;
using System.Globalization;
using Xamarin.Forms;

namespace HackerNews.MobileApp.Converters
{
    public class StringToUppercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var text = (string)value;
            return text.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
