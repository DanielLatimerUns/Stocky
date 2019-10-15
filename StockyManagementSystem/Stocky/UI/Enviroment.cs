using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Stocky.Views;
using System.Reflection;
using Stocky.Application;
using Stocky.UI;
using Stocky.ViewModels.Interfaces;

namespace Stocky.UI
{
    public class Enviroment
    {
        /// <summary>
        /// Closes any open window from any class within scope. Window must have a title to work.
        /// </summary>
        /// <param name="WindowTitle"></param>
        public static void CloseWindow(string WindowTitle)
        {
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType().Name == WindowTitle)
                {
                    (window as Window).Close();

                }
            }
        }

        public static void LoadNewTab(UserControl usercontrol, string tabname)
        {

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindowView))
                {
                    (window as MainWindowView).TabControl1.Add(usercontrol, tabname);
                    (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                }
            }
        }

        public static void LoadNewTab(string Paramater)
        {
            
            string ModuleClass;
            string Header;

            if (Paramater.Contains('|'))
            {
                string[] Split;

                Split = Paramater.Split('|');
                ModuleClass = Split[0];
                Header = Split[1];
            }
            else
            {
                ModuleClass = Paramater;
                Header = Paramater;
            }
                
            string modulename = string.Format("Stocky.Views.{0}", ModuleClass);
            if (ModuleClass != "")
            {
                Type ModuleType = Type.GetType(modulename);

                if (ModuleType != null)
                {
                    var usercontrol = (UserControl)Activator.CreateInstance(ModuleType);          

                    foreach (Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindowView))
                        {
                            if ((window as MainWindowView).TabControl1.Items.Contains(usercontrol))
                            {
                                (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                            }
                            else
                            {
                                (window as MainWindowView).TabControl1.Add(usercontrol, Header);
                                (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                            }
                        }
                    }
                }
            }
        }

        public static void LoadNewTab(string ModuleClass,string TabName)
        {
            string modulename = string.Format("Stocky.Views.{0}", ModuleClass);
            if (ModuleClass != "")
            {
                Type ModuleType = Type.GetType(modulename);

                if (ModuleType != null)
                {
                    var usercontrol = (UserControl)Activator.CreateInstance(ModuleType);

                    foreach (Window window in System.Windows.Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindowView))
                        {
                            if ((window as MainWindowView).TabControl1.Items.Contains(usercontrol))
                            {
                                (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                            }
                            else
                            {
                                (window as MainWindowView).TabControl1.Add(usercontrol, TabName);
                                (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                            }


                        }
                    }
                }
            }
        }

        public static void LoadNewTab(string ModuleClass,bool FullyQaulifiedClassname)
        {
            try
            {
                string modulename = string.Format(ModuleClass);
                if (ModuleClass != "")
                {
                    Type ModuleType = Type.GetType(modulename);
                    if (!SetLoadConditions(ModuleType)) { return; }
                    if (ModuleType != null)
                    {
                        var usercontrol = (UserControl)Activator.CreateInstance(ModuleType);

                        foreach (Window window in System.Windows.Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindowView))
                            {
                                (window as MainWindowView).TabControl1.Add(usercontrol, ModuleClass);
                                (window as MainWindowView).TabControl1.SelectedItem = usercontrol;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Stocky.ExepionLogger.Logger.LogException(e);
                Stocky.ExepionLogger.Logger.Show(e);
            }
        }

    

        public static void LoadWindow(string WindowClass)
        {
            string WindowName = string.Format("Stocky.Views.{0}", WindowClass);
            if (WindowClass != "")
            {
                Type WindowType = Type.GetType(WindowName);
                if (WindowType != null)
                {

                    var Window = (System.Windows.Window)Activator.CreateInstance(WindowType);

                  //  Window.Owner = Application.Current.MainWindow;
                    Window.Show();

                }
            }
        }

        private static bool SetLoadConditions(Type Module)
        {
            try
            {
                switch (Module.Name)
                {
                    case "ReportViewerView":
                        if (!string.IsNullOrWhiteSpace(ApplicationSettings.SSRSServer))
                        {
                            ReportViewer.SessionVariables.ReportServerHome = new Uri(ApplicationSettings.SSRSServer);
                            return true;
                        }
                        else
                        {
                            throw new Exception("Please Set a SSRS server First");
                        }                      
                }
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static void LoadWindow(string WindowClass,object ObjectToSend,string ObjectKey)
        {
            MVVM_Framework.ObjectMessenger Om = new MVVM_Framework.ObjectMessenger();

            if(ObjectToSend != null)
            {
                Om.Send(ObjectKey, ObjectToSend);
            }

            string WindowName = string.Format("Stocky.Views.{0}", WindowClass);
            if (WindowClass != "")
            {
                Type WindowType = Type.GetType(WindowName);
                if (WindowType != null)
                {

                    var Window = (System.Windows.Window)Activator.CreateInstance(WindowType);

                    Window.Owner = System.Windows.Application.Current.MainWindow;
                    Window.Show();

                }
            }
        }

        public static void LoadDialog(string WindowClass, object ObjectToSend, string ObjectKey)
        {
            MVVM_Framework.ObjectMessenger Om = new MVVM_Framework.ObjectMessenger();

            if (ObjectToSend != null)
            {
                Om.Send(ObjectKey, ObjectToSend);
            }

            string WindowName = string.Format("Stocky.Views.{0}", WindowClass);
            if (WindowClass != "")
            {
                Type WindowType = Type.GetType(WindowName);
                if (WindowType != null)
                {

                    var Window = (System.Windows.Window)Activator.CreateInstance(WindowType);

                    Window.Owner = System.Windows.Application.Current.MainWindow;
                    Window.ShowDialog();

                }
            }
        }

        public static void ChnageDB()
        {
            Login.LoginView LoginView = new Login.LoginView();
            System.Windows.Application.Current.MainWindow = LoginView;

            CloseWindow("MainWindowView");
            LoginView.Show();
        }

        public static void CloseCurrentTab()
        {
            
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindowView))
                {
                  var CurrentTab =  (window as MainWindowView).TabControl1.SelectedItem;
                    (window as MainWindowView).TabControl1.Items.Remove(CurrentTab);

                }
            }
        }

        public static void CloseTab(IViewModelTabItem Tab)
        {
            
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindowView))
                {
                    var TabItem = (window as MainWindowView).TabControl1.Items.OfType<TabItem>().ToList();

                    foreach(var t in TabItem)
                    {
                        var content = (UserControl)t.Content;
                        var VM = (IViewModelTabItem)content.DataContext;

                        if (VM.CurrentInstanceGUID == Tab.CurrentInstanceGUID)
                        {
                            (window as MainWindowView).TabControl1.Items.Remove(t);
                        }
                    }

                }
            }
        }

        public static void LoadTab(IViewModelTabItem Tab)
        {
            
            var vmtype = Tab.GetType();

            var ViewName = "Stocky.Views."+vmtype.Name.Replace("Model", "");

            var viewtype = Type.GetType(ViewName);

                var view = Activator.CreateInstance(viewtype) as UserControl;
                view.DataContext = Tab;

                foreach (Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindowView))
                    {
                        (window as MainWindowView).TabControl1.Add(view);
                    }
                }              
        }

        public static void ExitApplication(bool ClosePromt)
        {
            if (ClosePromt == true)
            {

             MessageBoxResult Results =  MessageBox.Show("Are You sure you want to Exit?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(Results == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
                
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
