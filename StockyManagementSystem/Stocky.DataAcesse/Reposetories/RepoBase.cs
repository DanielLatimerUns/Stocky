using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class RepoBase
    {
        internal  StockyDataDataContext DB;
        public RepoBase()
        {
            DB = new StockyDataDataContext();
        }

        #region CommonDataMethods
        internal int MostRecentsalesID()
        {
            var Query = (from
                              ST in DB.dtSales
                         orderby ST.tID descending
                         select new
                         {

                             TranID = ST.tID
                         }).Take(1);
            return Query.FirstOrDefault().TranID;

        }
        internal int TopPurchase()
        {
            var pIDString = (from P in DB.dtPurcheses
                             orderby P.pID descending
                             select new
                             {
                                 ID = P.pID
                             }).Take(1);

            return pIDString.FirstOrDefault().ID;
        }
        internal int? GetLatestAddress()
        {
            int? query = (from A in DB.dtAddresses
                          orderby A.aID
                          select new { AdressID = A.aID }).FirstOrDefault().AdressID;

            return query;

        }

        internal int getlastrefund()
        {
            var query = (from R in DB.dtRefunds
                         orderby R.rID descending
                         select new { RefundID = R.rID }).Take(1);

            return query.FirstOrDefault().RefundID;
        }

        public int GetLatestUserID()
        {
            int id = (from U in DB.dtUsers
                      orderby U.uID descending
                      select new { UserID = U.uID }).Take(1).FirstOrDefault().UserID;
            return id;

        }

        public void CreateNewStockStatus(long stockid,string Status,int UserID,decimal Value = 0)
        {
            dtStockHistory StatusOBJ = new dtStockHistory
            {
                Created = DateTime.Now,
                Status = Status,
                StoockID = Convert.ToInt32(stockid),
                UserID = UserID,
                StatusID = 0,
                Value = Value
            };

            DB.dtStockHistories.InsertOnSubmit(StatusOBJ);
            DB.SubmitChanges();

        }

        #endregion
    }
}
