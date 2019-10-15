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
    [MessageContract]
    public class DownloadRequest
    {
        
        [MessageBodyMember]
        public string FileName;
        [MessageBodyMember]
        public string ObjectID;
        [MessageBodyMember]
        public string TypeID;
    }
}
