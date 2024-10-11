using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace biorand.desktop.Resources.Converters;

public class EnumToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return Visibility.Collapsed;
        }

        // Parse the parameter to the enum type
        var enumType = value.GetType();
        if (Enum.IsDefined(enumType, parameter) && Enum.TryParse(enumType, parameter.ToString(), true, out var enumValue))
        {
            return value.Equals(enumValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
