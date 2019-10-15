using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Stocky.AppService.ServiceInterfaces;
using Stocky.AppService.Contracts;
using Stocky.DataAcesse.DataAccesse;

namespace Stocky.AppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AppService : IAppDataService
    {
        DataFunctions Functions = new DataFunctions();

        public StockContract GetStockList()
        {
            StockContract retunobject = new StockContract();

             retunobject.StockList = Functions.GetStockList().ToList();

            return retunobject;
        }

        public StockContract GetStockObject()
        {
            throw new NotImplementedException();
        }
    }
}
