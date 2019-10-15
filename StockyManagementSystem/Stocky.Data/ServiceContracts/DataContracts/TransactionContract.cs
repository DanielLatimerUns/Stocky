using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Data;
using Stocky.Data.Interfaces;

namespace Stocky.Service.Contracts
{   
    [DataContract]
    [KnownType(typeof(skPurchase))]
    [KnownType(typeof(skSales))]
    [KnownType(typeof(skRefund))]
    public class TransactionContract
    {
        [DataMember]
        public skPurchase PurchaseObject { get; set; }
        [DataMember]
        public skSales SalesObject { get; set; }
        [DataMember]
        public skRefund RefundObject { get; set; }
        [DataMember]
        public List<ITransaction> TransactionList { get; set; }
    }
}
