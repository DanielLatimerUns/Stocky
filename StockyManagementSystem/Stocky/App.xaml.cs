using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Framework;
using System.ComponentModel;
using Stocky.Users;
using System.Windows.Markup;
using System.Globalization;

namespace Stocky
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {}
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Stocky.Application.Caching.DeleteObjectCache();
                base.OnExit(e);
            }
            catch(Exception ex)
            {
                Stocky.ExepionLogger.Logger.LogException(ex);
            }
           
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);
        }

    }
}
