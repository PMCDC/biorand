using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace biorand.app.Resources.Converters
{
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

            var parameters = parameter.ToString().Split('|');
            foreach (var enumValueName in parameters)
            {
                if (Enum.IsDefined(enumType, enumValueName))
                {
                    if (value.Equals(Enum.Parse(enumType, enumValueName)))
                    {
                        return Visibility.Visible;
                    }
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}


