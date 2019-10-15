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
    public class ReportContract
    {
        [DataMember]
        public List<ReportLink> ReportLinks { get; set; }
        [DataMember]
        public List<ReportFolder> ReporFolders { get; set; }
    }
}
