using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataAcesse.DataBase;
using Stocky.Exceptions;

namespace Stocky.DataAcesse.Reposetories
{
    public class StockRepo : RepoBase
    {
      
        #region SelectMethods

        public skStock GetStockObject(int StockID)
        {
            var query = (from SL in DB.dtStocks
                         join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                         join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                         from TR in DB.dtPurcheses
                          .Where(o => SL.PurchaseID == o.pID)
                          .DefaultIfEmpty()
                         from SD in DB.dtStockDetails
                        .Where(o => o.StockID == SL.sID)
                        .DefaultIfEmpty()
                         where SL.sID == StockID
                         select new skStock
                         {
                             Stockid = SL.sID,
                             Name = SL.ItemTitle,
                             Description = SL.ItemDesc,
                             purchaseddate = TR.Purchesed_Date,
                             purchasedvalue = SD.PurchaseValue,
                             Sold = SL.Sold,
                             Created = SL.Created,
                             Updated = SL.Updated,
                             CategoryObject = new skCategory
                             {
                                 Description = ST.Description,
                                 Name = ST.Title,
                                 StockTypeID = ST.CatID
                             },
                             ValueBandObject = new skValueBands
                             {
                                 Description = VB.Description,
                                 HighValue = VB.HighValue,
                                 ID = VB.ivID,
                                 LowValue = VB.LowValue,

                             }
                         }).FirstOrDefault();
            if(query == null)
            {
                throw new NoRecordException(typeof(skStock));
            }
            else
            {
                return query;
            }
        }
        public IEnumerable<skStock> GetStockList()
        {
            var query = from SL in DB.dtStocks
                        join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                        join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                        from TR in DB.dtPurcheses
                         .Where(o => SL.PurchaseID == o.pID)
                             .DefaultIfEmpty()
                        from SD in DB.dtStockDetails
                   .Where(o => o.StockID == SL.sID)
                   .DefaultIfEmpty()
                        select new skStock
                        {
                            Stockid = SL.sID,
                            Name = SL.ItemTitle,
                            Description = SL.ItemDesc,
                            purchaseddate = TR.Purchesed_Date,
                            purchasedvalue = SD.PurchaseValue,
                            Sold = SL.Sold,
                            Created = SL.Created,
                            Updated = SL.Updated,
                            CategoryObject = new skCategory
                            {
                                Description = ST.Description,
                                Name = ST.Title,
                                StockTypeID = ST.CatID
                            },
                            ValueBandObject = new skValueBands
                            {
                                Description = VB.Description,
                                HighValue = VB.HighValue,
                                ID = VB.ivID,
                                LowValue = VB.LowValue,
                            }
                        };
           

            return query;
        }

        public IEnumerable<skStock> GetStockList(skStock stockOBJ)
        {
            long tempval;
            long? sIDv = long.TryParse(stockOBJ.Stockid.ToString(), out tempval) ? tempval : (long?)null;

            var query = (from SL in DB.dtStocks
                        join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                        join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                        from SD in DB.dtStockDetails.AsEnumerable()
                        .Where(o => o.StockID == SL.sID)
                        .DefaultIfEmpty()
                        from TR in DB.dtPurcheses.AsEnumerable()
                             .Where(o => SL.PurchaseID == o.pID)
                             .DefaultIfEmpty()
                        where SL.ItemTitle.IndexOf(stockOBJ.SearchFilter.ObjectName == null ? SL.ItemTitle : stockOBJ.SearchFilter.ObjectName) >= 0
                                && SL.ItemDesc.IndexOf(stockOBJ.SearchFilter.ObjectDescription == null ? SL.ItemDesc : stockOBJ.SearchFilter.ObjectDescription) >= 0
                                && SL.sID.Equals(sIDv == 0 ? SL.sID : sIDv)
                                && SL.Sold.Equals(stockOBJ.Sold == false ? SL.Sold : stockOBJ.Sold)
                                && SL.ItemTitle.IndexOf(stockOBJ.SearchFilter.ObjectName == null ? SL.ItemTitle : stockOBJ.SearchFilter.ObjectName) >= 0
                                && (SL.Created >= stockOBJ.SearchFilter.ObjectCreatedFrom && SL.Created <= stockOBJ.SearchFilter.ObjectCreatedTo)
                                orderby SL.Created descending
                         select new skStock
                        {
                            Stockid = SL.sID,
                            Name = SL.ItemTitle,
                            Description = SL.ItemDesc,
                            purchaseddate = TR.Purchesed_Date,
                            purchasedvalue = SD.PurchaseValue,
                            Sold = SL.Sold,
                            Created = SL.Created,
                            Updated = SL.Updated,
                            CategoryObject = new skCategory
                            {
                                Description = ST.Description,
                                Name = ST.Title,
                                StockTypeID = ST.CatID
                            },
                            ValueBandObject = new skValueBands
                            {
                                Description = VB.Description,
                                HighValue = VB.HighValue,
                                ID = VB.ivID,
                                LowValue = VB.LowValue,

                            }

                        }).Take(stockOBJ.SearchFilter.RecordsToReturn == 0 ? 100000 : stockOBJ.SearchFilter.RecordsToReturn);
            return query;
        }
        public IEnumerable<skStock> GetStockListBySaleID(int SaleID)
        {

            var query = from SL in DB.dtStocks
                        join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                        join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                        from SD in DB.dtStockDetails
                        .Where(o => o.StockID == SL.sID)
                        .DefaultIfEmpty()
                        where SL.SaleID == SaleID

                        select new skStock
                        {
                            Stockid = SL.sID,
                            Name = SL.ItemTitle,
                            Description = SL.ItemDesc,
                            purchasedvalue = SD.PurchaseValue,
                            Sold = SL.Sold,
                            Created = SL.Created,
                            Updated = SL.Updated,
                            CategoryObject = new skCategory
                            {
                                Description = ST.Description,
                                Name = ST.Title,
                                StockTypeID = ST.CatID
                            },
                            ValueBandObject = new skValueBands
                            {
                                Description = VB.Description,
                                HighValue = VB.HighValue,
                                ID = VB.ivID,
                                LowValue = VB.LowValue,

                            }
                        };
            return query;
        }
        public IEnumerable<skStock> GetStockListByPrucahseID(int PurchaseID)
        {

            var query = from SL in DB.dtStocks
                        join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                        join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                        from SD in DB.dtStockDetails
                        .Where(o => o.StockID == SL.sID)
                        .DefaultIfEmpty()
                        where SL.PurchaseID == PurchaseID

                        select new skStock
                        {
                            Stockid = SL.sID,
                            Name = SL.ItemTitle,
                            Description = SL.ItemDesc,
                            purchasedvalue = SD.PurchaseValue,
                            Sold = SL.Sold,
                            Created = SL.Created,
                            Updated = SL.Updated,
                            CategoryObject = new skCategory
                            {
                                Description = ST.Description,
                                Name = ST.Title,
                                StockTypeID = ST.CatID
                            },
                            ValueBandObject = new skValueBands
                            {
                                Description = VB.Description,
                                HighValue = VB.HighValue,
                                ID = VB.ivID,
                                LowValue = VB.LowValue,

                            }


                        };
            return query;
        }

        public IEnumerable<skStock> GetOrphanedStockList(TransactionType TransactionType )
        {
            IQueryable<skStock> ReturnData = null;

            switch (TransactionType)
            {
                case TransactionType.Purchase:
                    var query = from SL in DB.dtStocks
                                join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                                join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                                from TR in DB.dtPurcheses
                                 .Where(o => SL.PurchaseID == o.pID)
                                     .DefaultIfEmpty()
                                from SD in DB.dtStockDetails
                                      .Where(o => o.StockID == SL.sID)
                                      .DefaultIfEmpty()
                                where SL.PurchaseID == null
                                select new skStock
                                {
                                    Stockid = SL.sID,
                                    Name = SL.ItemTitle,
                                    Description = SL.ItemDesc,
                                    purchaseddate = TR.Purchesed_Date,
                                    purchasedvalue = SD.PurchaseValue,
                                    Sold = SL.Sold,
                                    Created = SL.Created,
                                    Updated = SL.Updated,
                                    CategoryObject = new skCategory
                                    {
                                        Description = ST.Description,
                                        Name = ST.Title,
                                        StockTypeID = ST.CatID
                                    },
                                    ValueBandObject = new skValueBands
                                    {
                                        Description = VB.Description,
                                        HighValue = VB.HighValue,
                                        ID = VB.ivID,
                                        LowValue = VB.LowValue,

                                    }
                                };
                    ReturnData = query;
                    break;

                case TransactionType.Sale:
                    var query2 = from SL in DB.dtStocks
                                 join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                                 join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                                 from TR in DB.dtPurcheses
                                  .Where(o => SL.PurchaseID == o.pID)
                                      .DefaultIfEmpty()
                                 from SD in DB.dtStockDetails
                                 .Where(o => o.StockID == SL.sID)
                                 .DefaultIfEmpty()
                                 where SL.SaleID == null
                                 select new skStock
                                 {
                                     Stockid = SL.sID,
                                     Name = SL.ItemTitle,
                                     Description = SL.ItemDesc,
                                     purchaseddate = TR.Purchesed_Date,
                                     purchasedvalue = SD.PurchaseValue,
                                     Sold = SL.Sold,
                                     Created = SL.Created,
                                     Updated = SL.Updated,
                                     CategoryObject = new skCategory
                                     {
                                         Description = ST.Description,
                                         Name = ST.Title,
                                         StockTypeID = ST.CatID
                                     },
                                     ValueBandObject = new skValueBands
                                     {
                                         Description = VB.Description,
                                         HighValue = VB.HighValue,
                                         ID = VB.ivID,
                                         LowValue = VB.LowValue,

                                     }
                                 };
                    ReturnData = query2;
                    break;
            }

            return ReturnData;
        }

        public int GetTop1MostrecentSid()
        {
            var query = (from s in DB.dtStocks
                         orderby s.sID descending
                         select new
                         {
                             sID = s.sID
                         }).Take(1);
            string i = query.FirstOrDefault().sID.ToString();
            return Int32.Parse(i);
        }

        public IEnumerable<skStockHistory> GetStockHistory(long StockID)
        {
            var query = from SH in DB.dtStockHistories
                        join U in DB.dtUsers on SH.UserID equals U.uID
                        where SH.StoockID == StockID
                        select new skStockHistory
                        {
                            Amount = SH.Value,
                            Status = SH.Status,
                            Submited = SH.Created,
                            SubmitedBy = U.UserName

                        };

            return query;
        }


        #endregion

        #region InsertMethods

        public void AddNewStockItem(skStock StockOb,int UserID)
        {
                dtStock newstock = new dtStock
                {
                    CategoryID = StockOb.CategoryObject.StockTypeID,
                    ItemDesc = StockOb.Description,
                    ItemTitle = StockOb.Name,
                    ValueBandID = StockOb.ValueBandObject.ID,
                    Batch = 0,
                    Sold = false,
                    CreatedBy = UserID,
                    Created = DateTime.Now

                };

                dtStockDetail newdetails = new dtStockDetail
                {
                    PurchaseValue = StockOb.purchasedvalue
                };

                dtStockHistory newstatus = new dtStockHistory
                {

                    Status = "Stock Created",
                    Value = 0,
                    UserID = UserID,
                    Created = DateTime.Now,
                    StatusID = 0
                };

                newstock.dtStockHistories.Add(newstatus);
                newstock.dtStockDetails.Add(newdetails);

                DB.dtStocks.InsertOnSubmit(newstock);
                DB.SubmitChanges();
        }

        public void AddNewStockItem(List<skStock> StockList, skPurchase PurchaseObject,int UserID)
        {
            dtPurchese newpurchase = new dtPurchese
            {
                ItemTitle = PurchaseObject.Title,
                ItemDescription = PurchaseObject.Description,
                PurchesedValue = PurchaseObject.Amount,
                ShippingCosts = PurchaseObject.Postage,
                InvoiceID = PurchaseObject.Invoice,
                Purchesed_Date = PurchaseObject.PurchaseDate,
                AddedBy = UserID,
                VendorID = PurchaseObject.VendorObject.VendorID,
                IsExpense = false,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            DB.dtPurcheses.InsertOnSubmit(newpurchase);
            DB.SubmitChanges();

            int ID = (from s in DB.dtPurcheses
                      orderby s.pID descending
                      select new { ID = s.pID }).Take(1).FirstOrDefault().ID;

            foreach (skStock s in StockList)
            {
                dtStock newstock = new dtStock
                {
                    CategoryID = s.CategoryObject.StockTypeID,
                    ItemDesc = s.Description,
                    ItemTitle = s.Name,
                    ValueBandID = s.ValueBandObject.ID,
                    Batch = 0,
                    Sold = false,
                    CreatedBy = UserID,
                    Created = DateTime.Now,
                    PurchaseID = ID

                };

                dtStockDetail newdetails = new dtStockDetail
                {
                    PurchaseValue = s.purchasedvalue
                };

                dtStockHistory newstatus = new dtStockHistory
                {

                    Status = "Stock Created",
                    Value = s.purchasedvalue,
                    UserID = UserID,
                    Created = DateTime.Now,
                    StatusID = 0
                };

                newstock.dtStockHistories.Add(newstatus);
                newstock.dtStockDetails.Add(newdetails);


                DB.dtStocks.InsertOnSubmit(newstock);
            }

            DB.SubmitChanges();
        }

        public void AddNewStockItem(skStock StockOb, skPurchase PurchaseOb,int UserID)
        {
            
                dtPurchese newpurchase = new dtPurchese
                {
                    ItemTitle = StockOb.Name,
                    ItemDescription = StockOb.Description,
                    PurchesedValue = PurchaseOb.Amount,
                    ShippingCosts = PurchaseOb.Postage,
                    InvoiceID = PurchaseOb.Invoice,
                    Purchesed_Date = PurchaseOb.PurchaseDate,
                    AddedBy = UserID,
                    VendorID = PurchaseOb.VendorObject.VendorID,
                    IsExpense = false,
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };

                dtStock newstock = new dtStock
                {
                    CategoryID = StockOb.CategoryObject.StockTypeID,
                    ItemDesc = StockOb.Description,
                    ItemTitle = StockOb.Name,
                    ValueBandID = StockOb.ValueBandObject.ID,
                    Batch = 0,
                    Sold = false,
                    CreatedBy = UserID,
                    Created = DateTime.Now,
                    dtPurchese = newpurchase
                };

                dtStockDetail newdetails = new dtStockDetail
                {
                    PurchaseValue = StockOb.purchasedvalue
                };

                newstock.dtStockDetails.Add(newdetails);
                DB.dtStocks.InsertOnSubmit(newstock);
                DB.SubmitChanges();
                   
                decimal pVALUE = System.Math.Abs(Convert.ToDecimal(PurchaseOb.Amount)) * (-1);
                decimal pPOSTAGE = System.Math.Abs(PurchaseOb.Postage) * (-1);
                int pID = TopPurchase();
                int? stockID = GetTop1MostrecentSid();

            dtStockHistory newstatus = new dtStockHistory
            {
                    StoockID = stockID,
                    Status = "Stock Created",
                    Value = StockOb.purchasedvalue,
                    UserID = UserID,
                    Created = DateTime.Now,
                    StatusID = 0
                };

                dtTransactionLedger newpurchaseledger = new dtTransactionLedger
                {
                    PurchaseID = pID,
                    TransactionType = "Purchase",
                    TotelAmount = pVALUE,
                    SaleID = null,
                    TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
                };

                dtTransactionLedger newshippingledger = new dtTransactionLedger
                {
                    PurchaseID = pID,
                    TransactionType = "Shipping",
                    TotelAmount = pPOSTAGE,
                    SaleID = null,
                    TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
                };
                DB.dtStockHistories.InsertOnSubmit(newstatus);
                DB.dtTransactionLedgers.InsertOnSubmit(newpurchaseledger);
                DB.dtTransactionLedgers.InsertOnSubmit(newshippingledger);
                DB.SubmitChanges();         
        }

        public void AddToExistingPurchase(skStock StockOb, int PurchaseID,int UserID)
        {

            dtStockDetail newdetails = new dtStockDetail
            {
                PurchaseValue = StockOb.purchasedvalue
        
            };

            dtStock newstock = new dtStock
            {
                CategoryID = StockOb.CategoryObject.StockTypeID,
                ItemDesc = StockOb.Description,
                ItemTitle = StockOb.Name,
                ValueBandID = StockOb.ValueBandObject.ID,
                Batch = 0,
                Sold = false,
                CreatedBy = UserID,
                Created = DateTime.Now,
                PurchaseID = PurchaseID,
                
            };

            newstock.dtStockDetails.Add(newdetails);

            DB.dtStocks.InsertOnSubmit(newstock);
            DB.SubmitChanges();

            dtPurchese purchase = DB.dtPurcheses.Single(x => x.pID == PurchaseID);
            purchase.PurchesedValue += StockOb.purchasedvalue;

            DB.SubmitChanges();

        }
        #endregion

        #region UpdateMethods

        public void UpdateStock(skStock StockObj,int UserID)
        {
                 
                dtStock stock = DB.dtStocks.Single(s => s.sID == StockObj.Stockid);

                stock.ItemTitle = StockObj.Name;
                stock.ItemDesc = StockObj.Description;
                stock.CategoryID = StockObj.CategoryObject.StockTypeID;
                stock.ValueBandID = StockObj.ValueBandObject.ID;
                stock.Updated = DateTime.Now;

             dtStockHistory ST = new dtStockHistory
            {
                    StoockID = Convert.ToInt32(StockObj.Stockid),
                    Status = "Stock Details Updated",
                    Value = null,
                    UserID = UserID,
                    Created = DateTime.Now,
                    StatusID = 3

                };

                DB.dtStockHistories.InsertOnSubmit(ST);
                DB.SubmitChanges();       
        }

        #endregion
    }
}
