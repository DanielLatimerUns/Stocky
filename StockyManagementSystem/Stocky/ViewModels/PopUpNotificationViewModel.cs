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

namespace Stocky.ViewModels
{
    public class PopUpnotificationViewModel : ViewModelBase
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
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _NotificationOBJ = ObjectMessenger.FindObject("NEWNOTIFICATION") as INotification;
                _OpenObject = new Command(Open);
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
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }
        #endregion
    }
}
