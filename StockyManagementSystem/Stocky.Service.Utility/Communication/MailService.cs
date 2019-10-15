using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Stocky.Utility;

namespace Stocky.Service.Utility.Communication
{
    public class MailService
    {
        #region Fields
        private SmtpClient Client;
        private MailMessage MSG;
        #endregion

        #region Properties
        public string Service { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public List<string> Attachments { get; set; }
        #endregion

        public MailService()
        {
            Attachments = new List<string>();
            Service = @"smtp.gmail.com";
            UserName = @"stockynotifications@gmail.com";
            PassWord = @"Hotdog7286521992@";
            Port = 587;
        }

        #region Methods
        public void Send(string Body, string Subject)
        {
            try
            {
                BuildClient();
                BuildMSG(Body, Subject);

                Client.Send(MSG);
                MSG.Dispose();
            }
            catch(SmtpException e)
            { throw; }
    
        }

        private void BuildClient()
        {
            Client = new SmtpClient(Service, Port);
            Client.Credentials = new NetworkCredential(UserName, PassWord);
            Client.EnableSsl = true;
        }

        private void BuildMSG(string Body, string Subject)
        {
            MSG = new MailMessage(FromEmail, ToEmail);
            MSG.Body = Body;
            MSG.Subject = Subject;

            if (Attachments.Count < 0)
            {
                foreach (var o in Attachments)
                {
                    Attachment attachment = new Attachment(o);
                    MSG.Attachments.Add(attachment);
                }
            }
        }
        #endregion
        #region Static Methods
        public static void SendNotificationEmail(INotification NotificationObj)
        {
            Stocky.DataAcesse.Reposetories.UserRepo Repo = new DataAcesse.Reposetories.UserRepo();

            var userobject = Repo.GetUserObject(NotificationObj.NewNotification.RaisedBy);
       
                MailService Mail = new MailService();
                Mail.FromEmail = @"stockynotifications@gmail.com";
                Mail.ToEmail = userobject.Email;
                Mail.Attachments = NotificationObj.NewNotification.NotificationAttachments;
                
                Mail.Send(NotificationObj.NewNotification.EmailBody, "New Notification: " + NotificationObj.NewNotification.Name);            
        }

        #endregion

    }
}
