using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;

namespace Stocky.DataAcesse.Reposetories
{
    public class ValidationRepo : RepoBase
    {
        public bool IsRecordDirty(object RecordToCheck)
        {
            Type typetocheck = RecordToCheck.GetType();

            switch (typetocheck.Name)
            {
                case "skStock":

                    skStock StockOBJ = (skStock)RecordToCheck;

                    var newstockdate = (from S in DB.dtStocks
                                        where S.sID == StockOBJ.Stockid
                                        select new skStock
                                        {
                                            Updated = S.Updated
                                        }).SingleOrDefault().Updated;

                    return newstockdate > StockOBJ.Updated ? true : false;

                case "skSales":

                    skSales SalesOBJ = (skSales)RecordToCheck;

                    var newsaledate = (from S in DB.dtSales
                                       where S.tID == SalesOBJ.ID
                                       select new skSales
                                       {
                                           Updated = S.Updated
                                       }).SingleOrDefault().Updated;

                    return newsaledate > SalesOBJ.Updated ? true : false;

                case "skPurchase":

                    skPurchase PurchaseOBJ = (skPurchase)RecordToCheck;

                    var newpurchasedate = (from P in DB.dtPurcheses
                                           where P.pID == PurchaseOBJ.ID
                                           select new skPurchase
                                           {
                                               Updated = P.Updated
                                           }).SingleOrDefault().Updated;

                    return newpurchasedate > PurchaseOBJ.Updated ? true : false;

                default:
                    throw new Exception("Object is not a valid record");
                case "skRefund":

                    skRefund RefundOBJ = (skRefund)RecordToCheck;

                    var newrefunddate = (from R in DB.dtRefunds
                                         where R.rID == RefundOBJ.ID
                                         select new skRefund
                                         {
                                             Updated = R.Updated
                                         }).SingleOrDefault().Updated;

                    return newrefunddate > RefundOBJ.Updated ? true : false;

            }

        }
    }
}
