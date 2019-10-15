using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using Stocky.Login;
using System.ComponentModel;
using Stocky.Session;

namespace Stocky.ViewModels
{
    public class AccountPreferanceViewModel : ViewModelBase, ViewModels.Interfaces.IViewModelTabItem
    {
        #region Fields
        private skUser _userobj;
        private bool _IsPasswordreset;
        #endregion

        #region Properties
        public bool IsPasswordreset
        {
            get { return _IsPasswordreset; }
            set
            {
                _IsPasswordreset = value;
                NotifyPropertyChanged("IsPasswordreset");
            }
        }

        private Guid _CurrentInstanceGUID = Guid.NewGuid();
        public Guid CurrentInstanceGUID
        {
            get
            {
                if (_CurrentInstanceGUID == null)
                {
                    _CurrentInstanceGUID = Guid.NewGuid();
                    return _CurrentInstanceGUID;
                }
                else
                    return _CurrentInstanceGUID;
            }
        }
        public skUser userObj
        {
            get { return _userobj; }
            set
            {
                _userobj = value;
                NotifyPropertyChanged("userOBJ");
            }
        }
        #endregion

        #region Constructors
        public AccountPreferanceViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            { 
                _userobj = skUser.LoadUser(CurrentSession.CurrentUserObject.UserID);
                _userobj.Password = "";
                _Update = new Command(UpdatePreferances);
         
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Commands
        //Fields
        private Command _Update;

        //Properties
        public Command Update { get {  return _Update; } }

       
        #endregion

        #region Command Methods
        private void UpdatePreferances()
        {
            try
            {
                _userobj.UpdateUser(IsPasswordreset);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion
    }
}
