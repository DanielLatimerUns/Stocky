using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Stocky.UI.Converters
{
    public class BoolToColour : IValueConverter
    {
        public object Convert(object value, Type targettype, object paramater, System.Globalization.CultureInfo culture)
        {
            bool convertedvalue = System.Convert.ToBoolean(value);

            if (convertedvalue == true)
            {
                return "LightGreen";
            }
            else
            {
                return "Tomato";
            }
        }
        public object ConvertBack(object value, Type targettype, object paramater, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if ((string)value == "Green")
                    return true;
                else
                    return false;

            }
            return false;

        }
    }
}
