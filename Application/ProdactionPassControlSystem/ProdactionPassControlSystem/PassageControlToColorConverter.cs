using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ProdactionPassControlSystem
{
    public class PassageControlToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "True":
                    return new SolidColorBrush(Colors.GreenYellow);
                case "False":
                    return new SolidColorBrush(Colors.Red);
                default:
                    break;
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
    }
}
