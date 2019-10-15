using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.ServiceInterfaces;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Utility;

namespace Stocky.Proxies
{
    public class MailClient : ClientBase<IMailService>, IMailService
    {
        public MailClient():
            base(EndPointDefinitions.MailEndPoint)
        {

        }
        public void SendEmail(string Body, string Subject, string From, string Too)
        {
            base.Channel.SendEmail(Body, Subject, From, Too);
        }

        public void SendNotification(INotification NotiificationObject)
        {
            base.Channel.SendNotification(NotiificationObject);
        }
    }
}
