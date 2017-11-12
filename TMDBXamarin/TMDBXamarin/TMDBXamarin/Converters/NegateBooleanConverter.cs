using System;
using System.Globalization;
using Xamarin.Forms;

namespace TMDBXamarin.Converters
{
    //credit where credit is due https://forums.xamarin.com/discussion/36714/how-to-in-a-binding-in-xaml
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
