using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using Stocky.AppService.Contracts;

namespace Stocky.AppService.ServiceInterfaces
{
    [ServiceContract]
    public interface IAppDataService
    {
        [OperationContract]
        StockContract GetStockList();
        [OperationContract]
        StockContract GetStockObject();

    }
}