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
    public class DepartmentNameToColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Service M":
                    return new SolidColorBrush(Colors.OrangeRed);
                case "Service H":
                    return new SolidColorBrush(Colors.GreenYellow);
                case "Traffic service":
                    return new SolidColorBrush(Colors.Aqua);
                case "Electro-mechanical service":
                    return new SolidColorBrush(Colors.Azure);
                case "Security service":
                    return new SolidColorBrush(Colors.LightGreen);
                case "Economic department":
                    return new SolidColorBrush(Colors.Tan);
                case "Computer department":
                    return new SolidColorBrush(Colors.Gold);
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
