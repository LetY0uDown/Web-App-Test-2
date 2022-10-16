using System;
using System.Globalization;
using System.Windows.Data;

namespace Client.Converters;

internal sealed class PriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return $"Цена: {value:0.00} ₽";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}