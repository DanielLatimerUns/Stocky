using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Stocky.Service.ServiceInterfaces;
using Stocky.Utility;
using Stocky.Data;
using Stocky.Service.Utility.Communication;


namespace Stocky.Service
{
    [KnownType(typeof(skCategory))]
    [KnownType(typeof(skUser))]
    [KnownType(typeof(skPerson))]
    public class StockyMailService :IMailService
    {
        public StockyMailService()
        {

        }

        public void SendEmail(string Body, string Subject, string From, string Too)
        {
            MailService Mail = new MailService();

            Mail.ToEmail = Too;
            Mail.FromEmail = From;

            Mail.Send(Body, Subject);

        }

        public void SendNotification(INotification NotiificationObject)
        {

                MailService.SendNotificationEmail(NotiificationObject);
           
        }
    }
}
