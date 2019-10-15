using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace Stocky.Service
{
    public static class Helpers
    {
        public static bool DBIdentification
        {
            get
            {
                if(OperationContext.Current.EndpointDispatcher.EndpointAddress.ToString().ToUpper().Contains("LOCALHOST"))
                {
                    return true;
                }
                return false;
            }
        }
    }
}