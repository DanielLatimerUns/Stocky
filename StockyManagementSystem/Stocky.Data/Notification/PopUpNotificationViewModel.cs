using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MVVM_Framework;
using Stocky.Utility;
using System.ComponentModel;
using Stocky.Utility;

namespace Stocky.Utility
{
    public class PopUpnotificationViewModel 
    {
        #region Properties
        private INotification _NotificationOBJ;

        public INotification NotificationOBJ
        {
            get { return _NotificationOBJ; }
            set
            {
                _NotificationOBJ = value;
                NotifyPropertyChanged("NotificationOBJ");
            }
        }
        #endregion

        #region Constructors
        public PopUpnotificationViewModel()
        {
            ObjectMessenger om = new ObjectMessenger();
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _NotificationOBJ = om.FindObject("NEWNOTIFICATION") as INotification;
                _OpenObject = new Command(Open);
            }
            catch (Exception E)
            {
                throw;
            }
        }
        #endregion

        #region Commands
        //Fields
        private Command _OpenObject;

        //Properties
        public Command OpenObject { get { return _OpenObject; } }


        #endregion

        #region Command Methods
        private async void Open()
        {
            try
            {
                var task = _NotificationOBJ.NewNotification.SetStatus(false);
                await task;

                _NotificationOBJ.OpenNotification();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
