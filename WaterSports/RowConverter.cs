using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WaterSports
{
    class RowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Sports)
            {

                return Brushes.White;

            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
