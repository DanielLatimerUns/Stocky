using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Data;
using System.IO;

namespace Stocky.Service.Contracts
{
    [MessageContract]
    public class RemoteFileInfo :IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public string TypeDirectory { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public string ObjectID { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public long Length { get; set; }

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; set; }

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }


    }
}
