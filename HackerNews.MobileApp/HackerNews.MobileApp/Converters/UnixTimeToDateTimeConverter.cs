using System;
using System.Globalization;
using Xamarin.Forms;

namespace HackerNews.MobileApp.Converters
{
    public class UnixTimeToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            var time = (long)value;
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time);
            return GetTimeSince(dateTimeOffset);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetTimeSince(DateTimeOffset objDateTime)
        {
            TimeSpan timeSpan = DateTimeOffset.Now.ToUniversalTime().Subtract(objDateTime);
            int days = timeSpan.Days;
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            if (days > 0)
                return $"{days} days ago";
            if (hours > 0)
                return $"{hours} hours ago";
            if (minutes > 0)
                return $"{minutes} minutes ago";
            if (seconds > 0)
                return $"{seconds} seconds ago";

            return string.Empty;
        }
    }
}
