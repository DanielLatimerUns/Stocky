
using System.ServiceModel;
using Stocky.Utility;
using Stocky.Data;


namespace Stocky.Service.ServiceInterfaces
{
    [ServiceKnownType(typeof(Notification))]
    [ServiceKnownType(typeof(skPurchase))]
    [ServiceKnownType(typeof(skSales))]
    [ServiceKnownType(typeof(skRefund))]
    [ServiceKnownType(typeof(skStock))]
    [ServiceKnownType(typeof(skCategory))]
    [ServiceKnownType(typeof(skPurchase))]

    [ServiceContract]
    public interface IMailService 
    {
        [OperationContract]
        void SendEmail(string Body, string Subject,string From,string Too);
        [OperationContract]
        void SendNotification(INotification NotiificationObject);
    }
}
