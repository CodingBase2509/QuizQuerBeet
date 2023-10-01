using System;
using System.Globalization;

namespace QuizQuerBeet.Services;

public class CountToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int count = (int)value;
        switch (count)
        {
            case >= 4:
                return false;
            default:
                return true;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
