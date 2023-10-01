using System.Globalization;

namespace QuizQuerBeet.Services;

public class ImageArrayToBoolConverter : IValueConverter
{
    public Grid EmptyArrayTemplate { get; set; }
    public Grid FilledArrayTemplate { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var array = value as byte[];

        var returningValue = array.Length switch
        {
            0 => EmptyArrayTemplate,
            _ => FilledArrayTemplate
        };

        return returningValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value as byte[]).Length == 0;
    }
}

