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
    public class RecipientContract
    {
        [DataMember]
        public skAddresses AddressObject { get; set; }
        [DataMember]
        public skPerson PersonObject { get; set; }
        [DataMember]
        public List<skAddresses> AddressList { get; set; }
        [DataMember]
        public List<skPerson> PersonList { get; set; }
    }
}
