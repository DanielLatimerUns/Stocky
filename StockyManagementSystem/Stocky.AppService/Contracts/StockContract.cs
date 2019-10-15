using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using Stocky.Data;

namespace Stocky.AppService.Contracts
{
    [DataContract]
    public class StockContract
    {
        [DataMember]
        public skStock StockObj { get; set; }
        [DataMember]
        public List<skStock> StockList { get; set; }

    }
}