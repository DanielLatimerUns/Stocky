using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Stocky.Data;
using Stocky.ViewModels;
using Stocky.Views;
using Stocky.UI;
using MVVM_Framework;
using Stocky.Utility;
using System.ComponentModel;
using Stocky.Session;

namespace Stocky.ViewModels
{
    public class RefundViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        private Notification Notification;
        #endregion

        #region Properties
        private skRefund _RefundObject;

        public skRefund RefundObject
        {
            get { _RefundObject.Validate(); return _RefundObject; }
            set
            {
                _RefundObject = value;
                NotifyPropertyChanged("RefundObject");
            }
        }


        #endregion

        #region Constructors
        public RefundViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _RefundObject = new skRefund();
                _RefundObject.StockItem = ObjectMessenger.FindObject("STOCKITEM");
           
                _SubmitRefund = new Command(submitRefund);
                Notification = new Notification();
                RefundObject.RefundCreated += Notification.OnNotificationReceived;
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
        private Command _SubmitRefund;

        //Properties
        public Command SubmitRefund { get { return _SubmitRefund; } }
        #endregion

        #region Command Methods
        public void submitRefund()
        {
           
            try
            {
                RefundObject.AddnewRefund(CurrentSession.CurrentUserObject.UserID);
              
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
            finally
            {
                this.Close();
            }
        }
        #endregion
    }
}
