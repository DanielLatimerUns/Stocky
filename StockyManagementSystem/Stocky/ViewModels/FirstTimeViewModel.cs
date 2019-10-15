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
    public  class FirstTimeViewModel: ViewModelBase
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
        #endregion

        #region Constructors
        public FirstTimeViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _APP = new ApplicationSettings();
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
