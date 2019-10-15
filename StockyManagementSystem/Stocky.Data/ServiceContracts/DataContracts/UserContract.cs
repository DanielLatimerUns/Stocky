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
    public class UserContract
    {
       [DataMember]
       public skUser UserObject { get; set; }
       [DataMember]
       public List<skUser> UserList { get; set; }
       [DataMember]
       public bool IsValidUser { get; set; }
       [DataMember]
       public skPreference UserPreferanceObject { get; set; }
       [DataMember]
       public List<skPreference> UserPreferancesList { get; set; }
    }
}
