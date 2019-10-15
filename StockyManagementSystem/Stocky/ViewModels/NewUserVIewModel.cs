using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using MVVM_Framework;
using Stocky.Login;
using System.ComponentModel;


namespace Stocky.ViewModels
{
    public class NewUserViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Properties
        private skUser _UsrObj;

        public skUser UsrObj
        {
            get { return _UsrObj; }
            set
            {
                _UsrObj = value;
                NotifyPropertyChanged("UsrObj");
            }
        }

        private string _ConfirmPassword;

        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set
            {
                _ConfirmPassword = value;
                NotifyPropertyChanged("ConfirmPassword");
            }
        }
        #endregion

        #region Constructors
        public NewUserViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _UsrObj = new skUser();
                _AddUser = new Command(_UsrObj.adduser);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Command
        //Fields
        private Command _AddUser;

        //Properties
        public Command AddUser { get { return _AddUser; } }
        #endregion
    }
}
