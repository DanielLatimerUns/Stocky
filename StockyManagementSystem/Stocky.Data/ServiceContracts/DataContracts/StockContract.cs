using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Data;

namespace Stocky.Service.Contracts
{
    [DataContract]
    public class StockContract
    {
        [DataMember]
        public skStock StockObj { get; set; }
        [DataMember]
        public List<skStock> StockList { get; set; }
        [DataMember]
        public List<skStockHistory> StockHistory { get; set; }
        [DataMember]
        public List<skCategory> CategoryList { get; set; }
        [DataMember]
        public skCategory CategoryObj { get; set; }
    }
}