using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using MVVM_Framework;

namespace Stocky.Application
{
    public class ApplicationSettings
    {
        private Command _Save;
        private Command _Load;

        #region Setting Properties
        /// <summary>
        /// Simply add a static string property with the "IsSetting" Attribute and it will save/load its values via reflection.
        /// </summary>

        [IsSetting(IsSettingProperty = true)]
        public static string SSRSServer { get; set; }

        [IsSetting(IsSettingProperty = true)]
        public static string LogLocation { get; set; }

        [IsSetting(IsSettingProperty = true)]
        public static string ImageDirectory { get; set; }

        [IsSetting(IsSettingProperty = true)]
        public static string ExportDirectory { get; set; }

        [IsSetting(IsSettingProperty = true)]
        public static string CacheLocation { get; set; }

        public List<SettingsType> SettingsList { get; set; }

        public Command Save { get { return _Save; } }
        public Command Load { get { return _Load; } }
        #endregion


        public ApplicationSettings()
        {
            SettingsList = new List<SettingsType>();
            _Save = new Command(SaveSettings);
            _Load = new Command(LoadSettings);
        }

        public void SaveSettings()
        {
         
            try
            {
                XDocument SettingXML = new XDocument();
                Type T = this.GetType();
                PropertyInfo[] Props = T.GetProperties();

                XElement root = new XElement("General_Settings");

                foreach (PropertyInfo P in Props)
                {
                    if (P.PropertyType == typeof(string) && Attribute.IsDefined(P, typeof(IsSetting)))
                    {
                        root.Add(new XElement(P.Name, P.GetValue(this)));
                    }
                }

                SettingXML.Add(root);
                Proxies.AppClient AppClient = new Proxies.AppClient();

                AppClient.SaveSettings(SettingXML.ToString());
            }
            catch (Exception)
            {
                throw;

            }

        }

        public void LoadSettings()
        {
            try
            {
                Proxies.AppClient AppClient = new Proxies.AppClient();

                var query = AppClient.LoadSettings();

                    SettingsType SettingXML = new SettingsType();
                    SettingXML.Code = "APP";
                    SettingXML.Value = query;
                    string UnparsedXML = (string)SettingXML.Value;

                    if (UnparsedXML != "<>")
                    {
                        XDocument SettingsXML = XDocument.Parse(UnparsedXML);
                        Type T = this.GetType();

                        var settings = from rs in SettingsXML.Descendants()
                                       select new
                                       {
                                           objectvalue = rs.Value,
                                           code = rs.Name
                                       };

                        foreach (var item in settings)
                        {
                            SettingsList.Add(new SettingsType { Code = item.code.ToString(), Value = item.objectvalue });

                            PropertyInfo PropToAssign = T.GetProperty(item.code.ToString());

                            if (PropToAssign != null)
                            {
                                PropToAssign.SetValue(this, item.objectvalue);
                            }
                        }
                    }
                    else
                    {
                        var firsttime = new Stocky.Views.FirstTimeView();
                        firsttime.ShowDialog();
                        
                    }
                }
                          
            
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class IsSetting : Attribute
    {
        public bool IsSettingProperty { get; set; }

        public string Info { get; }

        public IsSetting()
        {
            Info = "This is a Setting Property!";
        }
    }
    public class SettingsType
    {
        public string Code { get; set; }
        public object Value { get; set; }
    }
}
