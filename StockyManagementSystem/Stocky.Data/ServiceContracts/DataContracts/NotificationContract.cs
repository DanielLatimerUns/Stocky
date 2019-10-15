using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Utility;

namespace Stocky.Service.Contracts
{
    [DataContract]
    public class NotificationContract
    {
        [DataMember]
        public Notification NotificationObject { get; set; }
        [DataMember]
        public List<Notification> NotificationList { get; set; }
    }
}
