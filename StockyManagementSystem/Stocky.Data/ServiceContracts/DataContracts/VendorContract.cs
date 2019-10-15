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
    public class VendorContract
    {
        [DataMember]
        public skvendors VendorObject { get; set; }
        [DataMember]
        public List<skvendors> VendorList { get; set; }
    }
}
