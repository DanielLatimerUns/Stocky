using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using Stocky.Utility;
using MVVM_Framework;
using Stocky.Data;
using Stocky.Proxies;


namespace Stocky.Utility
{
    public class Notification
    {
        #region Fields
        private INotification NewNotification;
        private ObjectMessenger OM;

        #endregion

        #region Properties

        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailBody { get; set; }
        public int RaisedBy { get; set; }
        public bool IsNew { get; set; } = true;
        public List<string> NotificationAttachments { get; set; }
        public DateTime Created { get; set; }
        public string ObjectType { get; set; }

        #endregion

        #region Constructors


        public Notification()
        {
            NotificationAttachments = new List<string>();
            OM = new ObjectMessenger();
        }
        #endregion

        #region Methods
        public async void OnNotificationReceived(object sender, NewNotificationEventArgs e)
        {
            try
            {
                NewNotification = e.notification;
                ObjectType = NewNotification.GetType().Name;

                var Add = AddtoDBAsync();
                var Send = SendNotification();

                await Add;
                await Send;
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
      
        #endregion

        #region Tasks

        private async Task SendNotification()
        {
                var task = Task.Run(() =>
                {

                    Stocky.Proxies.MailClient MailClient = new Proxies.MailClient();
                    try
                    {
                        OM.Send("NEWNOTIFICATION", NewNotification);
                        Application.Current.Dispatcher.Invoke((Action)delegate { Stocky.Utility.PopUpNotificationView p = new PopUpNotificationView(); p.Show(); }); 
                        
                                             
                        MailClient.SendNotification(NewNotification);
                       
                    }               
                    finally
                    {
                        MailClient.Close();
                    }
                });

                await task;
        }
  
        private async Task AddtoDBAsync()
        {
            AppClient client = new AppClient();
            try
            {
                await Task.Run(() => client.AddNotification(NewNotification.NewNotification));
            }

            finally
            {
                client.Close();
            }
        }
        /// Needs Implementing
        public async Task<bool> SetStatus(bool Status)
        {
            AppClient client = new AppClient();
            try
            {
                var task = Task.Run(() => client.SetNotificationStatus(NewNotification.NewNotification.ID,Status));
                await task;
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    #endregion

    public class NewNotificationEventArgs : EventArgs
    {
        public INotification notification { get; set; }
      

        public NewNotificationEventArgs(INotification ReceivedNotificaiton)
        {
            notification = ReceivedNotificaiton;
        }
    }

    public interface INotification
    {
        /// <summary>
        /// Notification object.
        /// </summary>
        Notification NewNotification { get; set; }

        
        /// <summary>
        /// Create and populate the notification object.
        /// </summary>
        void CreateNotification(EventActionType eventActionType);
        /// <summary>
        /// Method to go to the object that raised the notifiation.
        /// </summary>
        void OpenNotification();
    }

    public enum EventActionType
    {
        Created = 1,
        Updated = 2,
        Deleted = 3
    }


}
