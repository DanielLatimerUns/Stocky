using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Stocky;
using Stocky.UI;
using Stocky.Session;
using System.Windows;


namespace MVVM_Framework
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Properties
        public ObjectMessenger ObjectMessenger { get; set; }

        public Stocky.Commands.UIControlCommands UiCommands { get; set; }

        private Visibility _Busy = Visibility.Hidden;

        public Visibility Busy
        {
            get { return _Busy; }
            set
            {
                _Busy = value;
                NotifyPropertyChanged("Busy");
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

        #region Constructor
        public ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                ObjectMessenger = new ObjectMessenger();
                UiCommands = new Stocky.Commands.UIControlCommands();
            }
            catch (Exception E)
            {
                Stocky.ExepionLogger.Logger.LogException(E);
                Stocky.ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods

        internal void SetBusy(bool IsBusy)
        {
            if (IsBusy)
                Busy = Visibility.Visible;
            else
                Busy = Visibility.Hidden;
        }

        #endregion
    }
}
