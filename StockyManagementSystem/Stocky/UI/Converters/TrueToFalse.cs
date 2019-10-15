using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Stocky.UI.Converters
{
    public class TrueToFalse : IValueConverter
    {

      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if((bool)value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
