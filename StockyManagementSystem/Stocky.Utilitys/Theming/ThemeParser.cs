using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using MVVM_Framework;
using Stocky.Data;


namespace Stocky.Utility.Theming
{
    public class ThemeParser
    {
        public static skTheme ParseThemeXML(string ThemeXML)
        {
            skTheme ThemeObject = new skTheme();

            if (ThemeXML != "<>")
            {
                XDocument SettingsXML = XDocument.Parse(ThemeXML);
                Type T = ThemeObject.GetType();

                var settings = from rs in SettingsXML.Descendants()
                               select new 
                               {
                                   objectvalue = rs.Value,
                                   code = rs.Name
                               };

                foreach (var item in settings)
                {                   
                    PropertyInfo PropToAssign = T.GetProperty(item.code.ToString());

                    if (PropToAssign != null)
                    {
                        PropToAssign.SetValue(ThemeObject, item.objectvalue);
                    }
                }
            }
            else
            {
                throw new Exception("No Default Theme Set !");
            }

            return ThemeObject;
        }

        public static string CreateThemeXML(skTheme ThemeObject)
        {
            XDocument SettingXML = new XDocument();
            Type T = ThemeObject.GetType();
            PropertyInfo[] Props = T.GetProperties();

            XElement root = new XElement("Theme_Properties");

            foreach (PropertyInfo P in Props)
            {
                if (P.PropertyType == typeof(string) && Attribute.IsDefined(P, typeof(ThemeProp)))
                {
                    root.Add(new XElement(P.Name, P.GetValue(ThemeObject)));
                }
            }

            SettingXML.Add(root);

            return SettingXML.ToString();
        }
    }

}
