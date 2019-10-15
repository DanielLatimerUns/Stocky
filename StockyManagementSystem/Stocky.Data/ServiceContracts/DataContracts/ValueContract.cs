using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Data;

namespace Stocky.Service.Contracts
{   [DataContract]
    public class ValueContract
    {
        [DataMember]
        public skValueBands ValueBandObject { get; set; }
        [DataMember]
        public List<skValueBands> ValueBandList { get; set; }

    }
}
