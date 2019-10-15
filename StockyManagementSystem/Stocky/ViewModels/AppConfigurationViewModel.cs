using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Session;
using MVVM_Framework;
using System.ComponentModel;
using Stocky.Application;

namespace Stocky.ViewModels
{
    public class AppConfigurationViewModel : ViewModelBase,Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties
        private ApplicationSettings _APP;

        public ApplicationSettings APP
        {
            get { return _APP; }
            set
            {
                _APP = value;
                NotifyPropertyChanged("APP");
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
        #endregion

        #region Constructors
        public AppConfigurationViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _APP = new ApplicationSettings();
                APP.LoadSettings();
              
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
