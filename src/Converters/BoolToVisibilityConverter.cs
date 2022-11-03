using System;
using System.Globalization;
using System.Windows;

namespace GenshinCharacterBrowser.Converters;

public class BoolToVisibilityConverter : BaseValueConverter
{
    public bool IsReverse { get; set; }

    public bool UseHidden { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var @bool = System.Convert.ToBoolean(value);

        if (IsReverse) @bool = !@bool;

        if (@bool) return Visibility.Visible;
        else return UseHidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var vis = (Visibility)value;

        bool res = vis != Visibility.Visible;
        if (IsReverse) res = !res;

        return res;
    }
}
