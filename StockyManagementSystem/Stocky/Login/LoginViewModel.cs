using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using Stocky.Views;
using Stocky.Session;
using Stocky.Application;
using Stocky.Users;

namespace Stocky.Login
{
    public class LoginViewModel : ViewModelBase
    {
      
        private AsynchronousCommand _Enter;      
        private string _ErrorMessage;
        private skUser _userobj;
        
        public LoginViewModel()
        {          
            _Enter = new AsynchronousCommand(Login);

            skStock s = new skStock();

            _userobj = new skUser();        
        }

        private async void Login()
        {
            var task = Task.Run(new Action(() =>
            {
                SetBusy(true);

                LoginMethod();

                SetBusy(false);

            }));

            await task;
        }

        private void LoginMethod()     
        {
            Stocky.Proxies.EndPointDefinitions.IsInDebugMode = CurrentSession.IsInDebugMode;
            Proxies.AuthenticationClient AuthenticationClient = new Proxies.AuthenticationClient();
            try
            {             
                ApplicationSettings _App = new ApplicationSettings();
                if (userobj != null)
                {                 
                    var result = AuthenticationClient.LoginUser(userobj.UserName, userobj.Password);

                    if (result.IsValidUser)
                    {
                        userobj = result.UserObject;
                 
                        CurrentSession.CurrentUserObject = userobj;
                        Stocky.Data.Helpers.SetDataContextUser(userobj);
                        UserPreference UserPref = new UserPreference();
                        Stocky.Application.ApplicationSettings AppSettings = new Application.ApplicationSettings();

                        UserPref.Load();                 
                        AppSettings.LoadSettings();

                        App.Current.Dispatcher.BeginInvoke((Action)delegate
                        {
                            MainWindowView MW = new MainWindowView();
                            App.Current.MainWindow = MW;
                            MW.Show();
                            Stocky.UI.Enviroment.CloseWindow("LoginView");
                        });
                    }
                    else
                    {
                        ErrorMEssage = "Incorrect Password!";
                    }
                }
            }
            catch(Exception E)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    ExepionLogger.Logger.LogException(E);
                    ExepionLogger.Logger.Show(E);
                });
             
            }
            finally
            {
                AuthenticationClient.Close();
            }                               
        }


        public string ErrorMEssage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        public skUser userobj
        {
            get { return _userobj; }
            set
            {
                _userobj = value;
                NotifyPropertyChanged("userobj");
            }
        }
        public AsynchronousCommand Enter
        {
            get { return _Enter; }
        }

    }
}
