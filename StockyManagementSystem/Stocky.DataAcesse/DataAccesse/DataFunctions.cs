using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataModels.Transaction;

namespace Stocky.DataAcesse.DataAccesse
{
    public class DataFunctions
    {
        //StockyDataDataContext DB = new StockyDataDataContext();

        //public int GetTop1MostrecentSid()
        //{
        //    var query = (from s in DB.dtStocks
        //                 orderby s.sID descending
        //                 select new
        //                 {
        //                     sID = s.sID
        //                 }).Take(1);
        //    string i = query.FirstOrDefault().sID.ToString();
        //    return Int32.Parse(i);
        //}

        //public IEnumerable<skStock> GetStockList(skStock stockOBJ)
        //{
        //    long tempval;
        //    long? sIDv = long.TryParse(stockOBJ.Stockid.ToString(), out tempval) ? tempval : (long?)null;
        //    int? sold = null;
        //    if (stockOBJ.Sold == 1)
        //        sold = stockOBJ.Sold;

        //    var query = from SL in DB.dtStocks
        //                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                from SD in DB.dtStockDetails
        //                .Where(o => o.StockID == SL.sID)
        //                .DefaultIfEmpty()
        //                from TR in DB.dtPurcheses
        //                     .Where(o => SL.PurchaseID == o.pID)
        //                     .DefaultIfEmpty()
        //                where SL.ItemTitle.IndexOf(stockOBJ.Name == null ? SL.ItemTitle : stockOBJ.Name) >= 0
        //                        && SL.sID.Equals(sIDv == 0 ? SL.sID : sIDv)
        //                        && SL.Sold.Equals(sold == null ? SL.Sold : sold)

        //                select new skStock
        //                {
        //                    Stockid = SL.sID,
        //                    Name = SL.ItemTitle,
        //                    Description = SL.ItemDesc,
        //                    purchaseddate = TR.Purchesed_Date,
        //                    category = ST.TypeTitle,
        //                    purchasedvalue = SD.PurchaseValue,
        //                    Sold = SL.Sold,
        //                    categoryID = ST.itID,
        //                    Created = SL.Created,
        //                    ValueBandID = (int)SL.ValueBandID,
        //                    Updated = SL.Updated


        //                };
        //    return query;
        //}

        //public IEnumerable<skStock> GetStockListBySaleID(int SaleID)
        //{

        //    var query = from SL in DB.dtStocks
        //                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                from SD in DB.dtStockDetails
        //                .Where(o => o.StockID == SL.sID)
        //                .DefaultIfEmpty()
        //                where SL.SaleID == SaleID

        //                select new skStock
        //                {
        //                    Stockid = SL.sID,
        //                    Name = SL.ItemTitle,
        //                    Description = SL.ItemDesc,
        //                    category = ST.TypeTitle,
        //                    purchasedvalue = SD.PurchaseValue,
        //                    Sold = SL.Sold,
        //                    categoryID = ST.itID,
        //                    Created = SL.Created,
        //                    ValueBandID = (int)SL.ValueBandID,
        //                    Updated = SL.Updated


        //                };
        //    return query;
        //}
        //public IEnumerable<skStock> GetStockListByPrucahseID(int PurchaseID)
        //{

        //    var query = from SL in DB.dtStocks
        //                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                from SD in DB.dtStockDetails
        //                .Where(o => o.StockID == SL.sID)
        //                .DefaultIfEmpty()
        //                where SL.PurchaseID == PurchaseID

        //                select new skStock
        //                {
        //                    Stockid = SL.sID,
        //                    Name = SL.ItemTitle,
        //                    Description = SL.ItemDesc,
        //                    category = ST.TypeTitle,
        //                    purchasedvalue = SD.PurchaseValue,
        //                    Sold = SL.Sold,
        //                    categoryID = ST.itID,
        //                    Created = SL.Created,
        //                    ValueBandID = (int)SL.ValueBandID,
        //                    Updated = SL.Updated


        //                };
        //    return query;
        //}

        //public IEnumerable<skStock> GetStockList()
        //{

        //    var query = from SL in DB.dtStocks
        //                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                from TR in DB.dtPurcheses
        //                 .Where(o => SL.PurchaseID == o.pID)
        //                     .DefaultIfEmpty()
        //                from SD in DB.dtStockDetails
        //           .Where(o => o.StockID == SL.sID)
        //           .DefaultIfEmpty()
        //                select new skStock
        //                {
        //                    Stockid = SL.sID,
        //                    Name = SL.ItemTitle,
        //                    Description = SL.ItemDesc,
        //                    purchaseddate = TR.Purchesed_Date,
        //                    category = ST.TypeTitle,
        //                    purchasedvalue = SD.PurchaseValue,
        //                    Sold = SL.Sold,
        //                    categoryID = ST.itID,
        //                    Created = SL.Created,
        //                    ValueBandID = (int)SL.ValueBandID,
        //                    Updated = SL.Updated

        //                };
        //    return query;
        //}
        ///// <summary>
        ///// Gets a List of Orphaned stock objects,2 = Sale,1 = Purchase
        ///// </summary>
        ///// <param name="Type"> 2 = Sale,1 = Purchase</param>
        ///// <returns></returns>
        //public IEnumerable<skStock> GetOrphanedStockList(int Type)
        //{
        //    IQueryable<skStock> ReturnData = null;

        //    switch (Type)
        //    {
        //        case 1:
        //            var query = from SL in DB.dtStocks
        //                        join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                        from TR in DB.dtPurcheses
        //                         .Where(o => SL.PurchaseID == o.pID)
        //                             .DefaultIfEmpty()
        //                        from SD in DB.dtStockDetails
        //                              .Where(o => o.StockID == SL.sID)
        //                              .DefaultIfEmpty()
        //                        where SL.PurchaseID == null
        //                        select new skStock
        //                        {
        //                            Stockid = SL.sID,
        //                            Name = SL.ItemTitle,
        //                            Description = SL.ItemDesc,
        //                            purchaseddate = TR.Purchesed_Date,
        //                            category = ST.TypeTitle,
        //                            purchasedvalue = SD.PurchaseValue,
        //                            Sold = SL.Sold,
        //                            categoryID = ST.itID,
        //                            Created = SL.Created,
        //                            ValueBandID = (int)SL.ValueBandID,
        //                            Updated = SL.Updated
        //                        };
        //            ReturnData = query;
        //            break;

        //        case 2:
        //            var query2 = from SL in DB.dtStocks
        //                         join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                         from TR in DB.dtPurcheses
        //                          .Where(o => SL.PurchaseID == o.pID)
        //                              .DefaultIfEmpty()
        //                         from SD in DB.dtStockDetails
        //                         .Where(o => o.StockID == SL.sID)
        //                         .DefaultIfEmpty()
        //                         where SL.SaleID == null
        //                         select new skStock
        //                         {
        //                             Stockid = SL.sID,
        //                             Name = SL.ItemTitle,
        //                             Description = SL.ItemDesc,
        //                             purchaseddate = TR.Purchesed_Date,
        //                             category = ST.TypeTitle,
        //                             purchasedvalue = SD.PurchaseValue,
        //                             Sold = SL.Sold,
        //                             categoryID = ST.itID,
        //                             Created = SL.Created,
        //                             ValueBandID = (int)SL.ValueBandID,
        //                             Updated = SL.Updated
        //                         };
        //            ReturnData = query2;
        //            break;

        //    }

        //    return ReturnData;


        //}

        //public IEnumerable<skStock> GetStockList(int ID)
        //{
        //    var query = from SL in DB.dtStocks
        //                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                where SL.sID == ID
        //                select new skStock
        //                {
        //                    Stockid = SL.sID,
        //                    Name = SL.ItemTitle,
        //                    Description = SL.ItemDesc,
        //                    category = ST.TypeTitle,
        //                    Sold = SL.Sold,
        //                    categoryID = ST.itID,
        //                    Created = SL.Created,
        //                    ValueBandID = (int)SL.ValueBandID,
        //                    Updated = SL.Updated
        //                };
        //    return query;
        //}

        //public void AddNewStockItem(skStock StockOb)
        //{
        //    try
        //    {
        //        dtStock newstock = new dtStock
        //        {
        //            ItemTypeID = StockOb.categoryID,
        //            ItemDesc = StockOb.Description,
        //            ItemTitle = StockOb.Name,
        //            ValueBandID = StockOb.ValueBandID,
        //            Batch = 0,
        //            Sold = 0,
        //            CreatedBy = StaticDataReposityory.UserID,
        //            Created = DateTime.Now

        //        };

        //        dtStockDetail newdetails = new dtStockDetail
        //        {
        //            PurchaseValue = StockOb.purchasedvalue
        //        };

        //        dtStockStatus newstatus = new dtStockStatus
        //        {

        //            Status = "Stock Created",
        //            Value = 0,
        //            UserID = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            StatusID = 0
        //        };

        //        newstock.dtStockStatus.Add(newstatus);
        //        newstock.dtStockDetails.Add(newdetails);

        //        DB.dtStocks.InsertOnSubmit(newstock);
        //        DB.SubmitChanges();
        //    }
        //    catch (Exception E)
        //    {
        //        ExepionLogger.Logger.LogException(E);
        //        ExepionLogger.Logger.Show(E);
        //    }
        //}

        //public void AddNewStockItem(List<skStock> StockList, skPurchase PurchaseObject)
        //{

        //    //try
        //    //{
        //    dtPurchese newpurchase = new dtPurchese
        //    {
        //        ItemTitle = PurchaseObject.Title,
        //        ItemDescription = PurchaseObject.Description,
        //        PurchesedValue = PurchaseObject.Amount,
        //        ShippingCosts = PurchaseObject.Postage,
        //        InvoiceID = PurchaseObject.Invoice,
        //        Purchesed_Date = PurchaseObject.PurchaseDate,
        //        AddedBy = StaticDataReposityory.UserID,
        //        VendorID = PurchaseObject.VendorId,
        //        IsExpense = false,
        //        Created = DateTime.Now,
        //        Updated = DateTime.Now
        //    };

        //    DB.dtPurcheses.InsertOnSubmit(newpurchase);
        //    DB.SubmitChanges();

        //    int ID = (from s in DB.dtPurcheses
        //              orderby s.pID descending
        //              select new { ID = s.pID }).Take(1).FirstOrDefault().ID;

        //    foreach (skStock s in StockList)
        //    {
        //        dtStock newstock = new dtStock
        //        {
        //            ItemTypeID = s.categoryID,
        //            ItemDesc = s.Description,
        //            ItemTitle = s.Name,
        //            ValueBandID = s.ValueBandID,
        //            Batch = 0,
        //            Sold = 0,
        //            CreatedBy = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            PurchaseID = ID

        //        };

        //        dtStockDetail newdetails = new dtStockDetail
        //        {
        //            PurchaseValue = s.purchasedvalue
        //        };

        //        dtStockStatus newstatus = new dtStockStatus
        //        {

        //            Status = "Stock Created",
        //            Value = s.ValueBandID,
        //            UserID = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            StatusID = 0
        //        };

        //        newstock.dtStockStatus.Add(newstatus);
        //        newstock.dtStockDetails.Add(newdetails);


        //        DB.dtStocks.InsertOnSubmit(newstock);
        //    }

        //    DB.SubmitChanges();

        //}

        //public void AddNewStockItem(skStock StockOb, skPurchase PurchaseOb)
        //{
        //    try
        //    {
        //        dtPurchese newpurchase = new dtPurchese
        //        {
        //            ItemTitle = StockOb.Name,
        //            ItemDescription = StockOb.Description,
        //            PurchesedValue = PurchaseOb.Amount,
        //            ShippingCosts = PurchaseOb.Postage,
        //            InvoiceID = PurchaseOb.Invoice,
        //            Purchesed_Date = PurchaseOb.PurchaseDate,
        //            AddedBy = StaticDataReposityory.UserID,
        //            VendorID = PurchaseOb.VendorId,
        //            IsExpense = false,
        //            Created = DateTime.Now,
        //            Updated = DateTime.Now

        //        };

        //        dtStock newstock = new dtStock
        //        {
        //            ItemTypeID = StockOb.categoryID,
        //            ItemDesc = StockOb.Description,
        //            ItemTitle = StockOb.Name,
        //            ValueBandID = StockOb.ValueBandID,
        //            Batch = 0,
        //            Sold = 0,
        //            CreatedBy = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            dtPurchese = newpurchase
        //        };

        //        dtStockDetail newdetails = new dtStockDetail
        //        {
        //            PurchaseValue = StockOb.purchasedvalue
        //        };

        //        newstock.dtStockDetails.Add(newdetails);
        //        DB.dtStocks.InsertOnSubmit(newstock);
        //        DB.SubmitChanges();
        //    }
        //    catch (Exception E)
        //    {
        //        ExepionLogger.Logger.LogException(E, "Insert Stock_Purchase failed");
        //        ExepionLogger.Logger.Show(E);
        //    }
        //    try
        //    {
        //        decimal pVALUE = System.Math.Abs(Convert.ToDecimal(PurchaseOb.Amount)) * (-1);
        //        decimal pPOSTAGE = System.Math.Abs(PurchaseOb.Postage) * (-1);
        //        int pID = TopPurchase();
        //        int? stockID = GetTop1MostrecentSid();

        //        dtStockStatus newstatus = new dtStockStatus
        //        {
        //            StoockID = stockID,
        //            Status = "Stock Created",
        //            Value = StockOb.ValueBandID,
        //            UserID = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            StatusID = 0
        //        };

        //        dtTransactionLedger newpurchaseledger = new dtTransactionLedger
        //        {
        //            ID = pID,
        //            TransactionType = "Purchase",
        //            TotelAmount = pVALUE,
        //            SaleID = null,
        //            TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
        //        };

        //        dtTransactionLedger newshippingledger = new dtTransactionLedger
        //        {
        //            ID = pID,
        //            TransactionType = "Shipping",
        //            TotelAmount = pPOSTAGE,
        //            SaleID = null,
        //            TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
        //        };
        //        DB.dtStockStatus.InsertOnSubmit(newstatus);
        //        DB.dtTransactionLedgers.InsertOnSubmit(newpurchaseledger);
        //        DB.dtTransactionLedgers.InsertOnSubmit(newshippingledger);
        //        DB.SubmitChanges();
        //    }
        //    catch (Exception E)
        //    {
        //        ExepionLogger.Logger.LogException(E);
        //        ExepionLogger.Logger.Show(E);
        //    }
        //}

        //public void UpdateStock(skStock StockObj)
        //{
        //    try
        //    {
        //        StockyDataDataContext DB = new StockyDataDataContext();
        //        dtStock stock = DB.dtStocks.Single(s => s.sID == StockObj.Stockid);

        //        stock.ItemTitle = StockObj.Name;
        //        stock.ItemDesc = StockObj.Description;
        //        stock.ItemTypeID = StockObj.categoryID;
        //        stock.ValueBandID = StockObj.ValueBandID;
        //        stock.Updated = DateTime.Now;

        //        DB.SubmitChanges();

        //        dtStockStatus ST = new dtStockStatus
        //        {
        //            StoockID = Convert.ToInt32(StockObj.Stockid),
        //            Status = "Stock Details Updated",
        //            Value = null,
        //            UserID = StaticDataReposityory.UserID,
        //            Created = DateTime.Now,
        //            StatusID = 3

        //        };
        //        DB.dtStockStatus.InsertOnSubmit(ST);
        //        DB.SubmitChanges();
        //    }
        //    catch (Exception E)
        //    {
        //        ExepionLogger.Logger.LogException(E);
        //        ExepionLogger.Logger.Show(E);
        //    }
        //}

        //public IEnumerable<skStockHistory> GetStockHistory(long StockID)
        //{
        //    var query = from SH in DB.dtStockStatus
        //                join U in DB.dtUsers on SH.UserID equals U.uID
        //                where SH.StoockID == StockID
        //                select new skStockHistory
        //                {
        //                    Amount = SH.Value,
        //                    Status = SH.Status,
        //                    Submited = SH.Created,
        //                    SubmitedBy = U.UserName

        //                };

        //    return query;
        //}

        public void AddNewPurchase(skPurchase PurchaseOb)
        {
            try
            {
                dtPurchese newpurchase = new dtPurchese
                {
                    ItemTitle = PurchaseOb.Title,
                    ItemDescription = PurchaseOb.Description,
                    PurchesedValue = PurchaseOb.Amount,
                    ShippingCosts = PurchaseOb.Postage,
                    InvoiceID = PurchaseOb.Invoice,
                    Purchesed_Date = PurchaseOb.PurchaseDate,
                    AddedBy = StaticDataReposityory.UserID,
                    VendorID = PurchaseOb.VendorId,
                    IsExpense = false,
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                DB.dtPurcheses.InsertOnSubmit(newpurchase);
                DB.SubmitChanges();

                decimal pVALUE = System.Math.Abs(Convert.ToDecimal(PurchaseOb.Amount)) * (-1);
                decimal pPOSTAGE = System.Math.Abs(PurchaseOb.Postage) * (-1);
                int pID = TopPurchase();

                dtTransactionLedger newpurchaseledger = new dtTransactionLedger
                {
                    ID = pID,
                    TransactionType = "Purchase",
                    TotelAmount = pVALUE,
                    SaleID = null,
                    TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
                };

                dtTransactionLedger newshippingledger = new dtTransactionLedger
                {
                    ID = pID,
                    TransactionType = "Shipping",
                    TotelAmount = pPOSTAGE,
                    SaleID = null,
                    TransactionDateTime = Convert.ToDateTime(PurchaseOb.PurchaseDate)
                };

                DB.dtTransactionLedgers.InsertOnSubmit(newpurchaseledger);
                DB.dtTransactionLedgers.InsertOnSubmit(newshippingledger);
                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        public void UpdatePurchase(skPurchase PurchaseObject)
        {
            dtPurchese PurchaseToUpdate = DB.dtPurcheses.Single(x => x.pID == PurchaseObject.ID);

            if (PurchaseToUpdate != null)
            {
                PurchaseToUpdate.ItemTitle = PurchaseObject.Title;
                PurchaseToUpdate.ItemDescription = PurchaseObject.Description;
                PurchaseToUpdate.ShippingCosts = PurchaseObject.Postage;
                PurchaseToUpdate.Purchesed_Date = PurchaseObject.PurchaseDate;

                PurchaseToUpdate.Updated = DateTime.Now;

                DB.SubmitChanges();
            }
            else
            {
                throw new Exception("Purchase record doesnt exist within the Database.");
            }
        }

        public void UpdateSale(skSales SaleObject)
        {
            dtSaleTransaction SaleToUpdate = DB.dtSaleTransactions.Single(x => x.tID == SaleObject.ID);

            if (SaleToUpdate != null)
            {
                SaleToUpdate.Description = SaleObject.Description;
                SaleToUpdate.Title = SaleObject.Title;
                SaleToUpdate.SoldDate = SaleObject.TransactionTime;

                SaleToUpdate.Updated = DateTime.Now;

                DB.SubmitChanges();
            }
            else
            {
                throw new Exception("Sale record doesnt exist within the Database.");
            }
        }

        public void AddNewSale(skAddresses AddressObject, skSales TransactionObject, skPerson PersonObject = null)
        {

            List<long> StockIds = new List<long>();
            foreach (skStock s in TransactionObject.StockList)
            {
                StockIds.Add(s.Stockid);
            }
            try
            {
                dtAddress newaddress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    isActive = true,

                };

                dtSaleTransaction newtransaction = new dtSaleTransaction
                {
                    SoldValue = TransactionObject.Amount,
                    PaypayFees = TransactionObject.PayPalFees,
                    PandP = TransactionObject.Postage,
                    SaleMethod = TransactionObject.SaleMethod,
                    SoldDate = TransactionObject.SaleDate,
                    SoldBy = StaticDataReposityory.UserID,
                    PayPalTransactionID = TransactionObject.PayPalTransactionID,
                    dtAddress = newaddress,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Title = TransactionObject.Title,
                    Description = TransactionObject.Description
                };

                DB.dtSaleTransactions.InsertOnSubmit(newtransaction);
                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
            try
            {
                int tid = MostRecentsalesID();
                decimal pvalue = Math.Abs(Convert.ToDecimal(TransactionObject.PayPalFees)) * (-1);
                decimal ppvalue = Math.Abs(Convert.ToDecimal(TransactionObject.Postage)) * (-1);

                var setsold = from ST in DB.dtStocks
                              where StockIds.Contains(ST.sID)
                              select ST;

                foreach (dtStock s in setsold)
                {
                    s.Sold = 1;
                    s.SaleID = tid;
                }

                dtTransactionLedger newsaleledger = new dtTransactionLedger
                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = Convert.ToDecimal(TransactionObject.Amount),
                    TransactionType = "Item Sale",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };


                dtTransactionLedger newspaypal = new dtTransactionLedger

                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = pvalue,
                    TransactionType = "PayPal Fees",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };



                dtTransactionLedger newepostage = new dtTransactionLedger
                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = ppvalue,
                    TransactionType = "Sale Postage",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };



                foreach (skStock s in TransactionObject.StockList)
                {
                    dtStockStatus newstatus = new dtStockStatus
                    {
                        StoockID = Convert.ToInt32(s.Stockid),
                        Status = "Stock Sold",
                        Value = 0,
                        UserID = StaticDataReposityory.UserID,
                        Created = DateTime.Now,
                        StatusID = 1
                    };
                    DB.dtStockStatus.InsertOnSubmit(newstatus);

                    dtStockDetail SD = DB.dtStockDetails.Single(x => x.SdID == s.Stockid);

                    SD.SaleValue = s.SaleValue;
                }

                DB.dtTransactionLedgers.InsertOnSubmit(newspaypal);
                DB.dtTransactionLedgers.InsertOnSubmit(newepostage);
                DB.dtTransactionLedgers.InsertOnSubmit(newsaleledger);

                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        public void AddNewSale(skSales TransactionObject, skPerson PersonObject = null)
        {
            List<long> StockIds = new List<long>();
            foreach (skStock s in TransactionObject.StockList)
            {
                StockIds.Add(s.Stockid);
            }
            try
            {

                if (PersonObject != null)
                {
                    dtPerson newPerson = new dtPerson
                    {
                        AddressID = Convert.ToInt32(GetLatestAddress()),
                        EbayName = PersonObject.EbayName,
                        Email = PersonObject.Email,
                        FirstName = PersonObject.FirstName,
                        SureName = PersonObject.Surname,
                        HomePhone = PersonObject.HomePhone,
                        WorkPhone = PersonObject.WorkPhone
                    };

                    DB.dtPersons.InsertOnSubmit(newPerson);
                    DB.SubmitChanges();
                }

                dtSaleTransaction newtransaction = new dtSaleTransaction
                {
                    SoldValue = TransactionObject.Amount,
                    PaypayFees = TransactionObject.PayPalFees,
                    PandP = TransactionObject.Postage,
                    SaleMethod = TransactionObject.SaleMethod,
                    SoldDate = TransactionObject.SaleDate,
                    SoldBy = StaticDataReposityory.UserID,
                    AddressID = TransactionObject.AddressId,
                    PayPalTransactionID = TransactionObject.PayPalTransactionID,
                    Updated = DateTime.Now,
                    Created = DateTime.Now,
                    Title = TransactionObject.Title,
                    Description = TransactionObject.Description

                };

                DB.dtSaleTransactions.InsertOnSubmit(newtransaction);
                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }

            try
            {

                int tid = MostRecentsalesID();
                decimal pvalue = Math.Abs(Convert.ToDecimal(TransactionObject.PayPalFees)) * (-1);
                decimal ppvalue = Math.Abs(Convert.ToDecimal(TransactionObject.Postage)) * (-1);

                var setsold = from ST in DB.dtStocks
                              where StockIds.Contains(ST.sID)
                              select ST;

                foreach (dtStock s in setsold)
                {
                    s.Sold = 1;
                    s.SaleID = tid;


                }

                dtTransactionLedger newsaleledger = new dtTransactionLedger
                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = Convert.ToDecimal(TransactionObject.Amount),
                    TransactionType = "Item Sale",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newspaypal = new dtTransactionLedger

                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = pvalue,
                    TransactionType = "PayPal Fees",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newepostage = new dtTransactionLedger
                {
                    SaleID = tid,
                    ID = null,
                    TotelAmount = ppvalue,
                    TransactionType = "Sale Postage",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                foreach (skStock s in TransactionObject.StockList)
                {
                    dtStockStatus newstatus = new dtStockStatus
                    {
                        StoockID = Convert.ToInt32(s.Stockid),
                        Status = "Stock Sold",
                        Value = 0,
                        UserID = StaticDataReposityory.UserID,
                        Created = DateTime.Now,
                        StatusID = 1
                    };
                    DB.dtStockStatus.InsertOnSubmit(newstatus);

                    dtStockDetail SD = DB.dtStockDetails.Single(x => x.StockID == s.Stockid);

                    SD.SaleValue = s.SaleValue;
                }

                DB.dtTransactionLedgers.InsertOnSubmit(newepostage);
                DB.dtTransactionLedgers.InsertOnSubmit(newspaypal);
                DB.dtTransactionLedgers.InsertOnSubmit(newsaleledger);

                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        //public int MostRecentsalesID()
        //{
        //    var Query = (from
        //                      ST in DB.dtSaleTransactions
        //                 orderby ST.tID descending
        //                 select new
        //                 {

        //                     TranID = ST.tID
        //                 }).Take(1);
        //    return Query.FirstOrDefault().TranID;

        //}
        //public int TopPurchase()
        //{
        //    var pIDString = (from P in DB.dtPurcheses
        //                     orderby P.pID descending
        //                     select new
        //                     {
        //                         ID = P.pID
        //                     }).Take(1);

        //    return pIDString.FirstOrDefault().ID;
        //}
        public IEnumerable<skvendors> GetVendorList()
        {
            var query = from V in DB.dtVendors
                        join A in DB.dtAddresses on V.AddresseID equals A.aID
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            onlineVendor = V.OnlineVender,
                            AddresseID = V.AddresseID,
                            CurrentAddress = new skAddresses
                            {
                                Active = A.isActive,
                                AddressID = A.aID,
                                AddressLine1 = A.AddressLine1,
                                AddressLine2 = A.AddressLine2,
                                AddressLine3 = A.AddressLine3,
                                Country = A.Country,
                                County = A.County,
                                PersonID = A.PersonID,
                                PostCode = A.PostCode,
                                Town = A.Town
                            }
                        };
            return query;
        }

        public IEnumerable<skvendors> GetVendorList(skvendors vendorOBJ)
        {
            long tempval;
            long? sIDv = long.TryParse(vendorOBJ.VendorID.ToString(), out tempval) ? tempval : (long?)null;
            var query = from V in DB.dtVendors
                        join A in DB.dtAddresses on V.AddresseID equals A.aID
                        where V.vID.Equals(sIDv
                        == 0 ? V.vID : sIDv)
                        && V.VendorsName.IndexOf(vendorOBJ.VendorsName == null ? V.VendorsName : vendorOBJ.VendorsName) >= 0
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            CurrentAddress = new skAddresses
                            {
                                Active = A.isActive,
                                AddressID = A.aID,
                                AddressLine1 = A.AddressLine1,
                                AddressLine2 = A.AddressLine2,
                                AddressLine3 = A.AddressLine3,
                                Country = A.Country,
                                County = A.County,
                                PersonID = A.PersonID,
                                PostCode = A.PostCode,
                                Town = A.Town
                            }
                        };
            return query;
        }

        public skvendors GetVendorDetails(int VendorId)
        {
            var query = from V in DB.dtVendors
                        where V.vID == VendorId
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            onlineVendor = V.OnlineVender,
                            AddresseID = V.AddresseID
                        };

            var query2 = from A in DB.dtAddresses
                         where A.aID == query.FirstOrDefault().AddresseID
                         select new skAddresses
                         {
                             Active = A.isActive,
                             AddressID = A.aID,
                             AddressLine1 = A.AddressLine1,
                             AddressLine2 = A.AddressLine2,
                             AddressLine3 = A.AddressLine3,
                             Country = A.Country,
                             County = A.County,
                             PersonID = A.PersonID,
                             PostCode = A.PostCode,
                             Town = A.Town
                         };

            skvendors Vobj = new skvendors();
            Vobj.CurrentAddress = query2.FirstOrDefault();
            Vobj.onlineVendor = query.FirstOrDefault().onlineVendor;
            Vobj.VendorID = query.FirstOrDefault().VendorID;
            Vobj.VendorsDescption = query.FirstOrDefault().VendorsDescption;
            Vobj.VendorsName = query.FirstOrDefault().VendorsName;

            return Vobj;
        }

        public void UpdateVendor(skvendors VendorObj)
        {
            dtVendor Vendor = DB.dtVendors.Single(x => x.vID == VendorObj.VendorID);

            Vendor.VendorsName = VendorObj.VendorsName;
            Vendor.VendorsDescption = VendorObj.VendorsDescption;
            Vendor.OnlineVender = VendorObj.onlineVendor;
            Vendor.AddresseID = VendorObj.CurrentAddress.AddressID;

            DB.SubmitChanges();
        }

        public IEnumerable<skCategory> GetCategoryList()
        {
            var query = from STL in DB.dtSTockTypes

                        select new skCategory
                        {
                            StockTypeID = STL.itID,
                            Name = STL.TypeTitle,
                            Description = STL.TypeDescription
                        };
            return query;
        }

        public IEnumerable<skCategory> GetCategoryList(skCategory CategoryOBJ)
        {
            int tempval;
            int? sIDv = int.TryParse(CategoryOBJ.StockTypeID.ToString(), out tempval) ? tempval : (int?)null;


            var query = from STL in DB.dtSTockTypes
                        where STL.TypeTitle.IndexOf(CategoryOBJ.Name == null ? STL.TypeTitle : CategoryOBJ.Name) >= 0
                                  && STL.TypeDescription.IndexOf(CategoryOBJ.Description == null ? STL.TypeDescription : CategoryOBJ.Description) >= 0
                                  && STL.itID.Equals(sIDv == 0 ? STL.itID : sIDv)

                        select new skCategory
                        {
                            StockTypeID = STL.itID,
                            Name = STL.TypeTitle,
                            Description = STL.TypeDescription
                        };
            return query;
        }

        public void UpdateCategory(skCategory CategoryObj)
        {

            dtSTockType category = DB.dtSTockTypes.Single(x => x.itID == CategoryObj.StockTypeID);

            category.TypeDescription = CategoryObj.Description;
            category.TypeTitle = CategoryObj.Name;

            DB.SubmitChanges();

        }

        public IEnumerable<skValueBands> GetValueBandList()
        {
            var query = from VB in DB.dtValueBands
                        select new skValueBands
                        {
                            Description = VB.Description,
                            HighValue = VB.HighValue,
                            ID = VB.ivID,
                            LowValue = VB.LowValue
                        };
            return query;
        }

        public IEnumerable<skValueBands> GetValueBandList(skValueBands ValueBandObj)
        {
            int tempval;
            int? ConvertedID = int.TryParse(ValueBandObj.ID.ToString(), out tempval) ? tempval : (int?)null;

            var query = from VB in DB.dtValueBands
                        where VB.ivID.Equals(ConvertedID == 0 ? VB.ivID : ValueBandObj.ID)
                        select new skValueBands
                        {
                            Description = VB.Description,
                            HighValue = VB.HighValue,
                            ID = VB.ivID,
                            LowValue = VB.LowValue
                        };
            return query;
        }

        public void AddNewRefund(skRefund RefundObject)
        {
            dtRefund NewRefund = new dtRefund
            {
                Reason = RefundObject.Description,
                Amount = Convert.ToDecimal(RefundObject.Amount),
                Postage = RefundObject.SHippingCosts,
                Refunded = DateTime.Now,
                RefundedBy = StaticDataReposityory.UserID,
                StockID = Convert.ToInt32(StaticDataReposityory.SelectedStockID)

            };

            DB.dtRefunds.InsertOnSubmit(NewRefund);
            DB.SubmitChanges();

            dtTransactionLedger NewRefundLedger = new dtTransactionLedger
            {
                RefundID = getlastrefund(),
                TransactionType = "Refund",
                TotelAmount = Maths.GetNegativeValue(Convert.ToDecimal(RefundObject.Amount)),
                TransactionDateTime = DateTime.Now
            };
            DB.dtTransactionLedgers.InsertOnSubmit(NewRefundLedger);
            DB.SubmitChanges();

            dtStockStatus NewStatus = new dtStockStatus
            {
                StoockID = Convert.ToInt32(StaticDataReposityory.SelectedStockID),
                Status = "Return",
                Value = RefundObject.Amount,
                UserID = StaticDataReposityory.UserID,
                Created = DateTime.Now,
                StatusID = 0
            };
            DB.dtStockStatus.InsertOnSubmit(NewStatus);
            DB.SubmitChanges();



            dtStock stock = DB.dtStocks.Single(s => s.sID == StaticDataReposityory.SelectedStockID);
            stock.Sold = 0;
            DB.SubmitChanges();


        }
        public void AddNewRefund(skRefund RefundObject, bool ReturnStock)
        {
            dtRefund NewRefund = new dtRefund
            {
                Reason = RefundObject.Description,
                Amount = Convert.ToDecimal(RefundObject.Amount),
                Postage = RefundObject.SHippingCosts,
                Refunded = DateTime.Now,
                RefundedBy = StaticDataReposityory.UserID,
                StockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Created = DateTime.Now,
                Updated = DateTime.Now,


            };

            //dtTransactionLedger NewRefundLedger = new dtTransactionLedger
            //{
            //    RefundID = getlastrefund(),
            //    TransactionType = "Refund",
            //    TotelAmount = Maths.GetNegativeValue(RefundObject.RefundAMount),
            //    TransactionDateTime = DateTime.Now
            //};

            dtStockStatus NewStatus = new dtStockStatus
            {
                StoockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Status = "Return",
                Value = RefundObject.Amount,
                UserID = StaticDataReposityory.UserID,
                Created = DateTime.Now,
                StatusID = 0
            };


            if (ReturnStock == true)
            {

                dtStockDetail details = DB.dtStockDetails.Single(s => s.StockID == RefundObject.StockItem.Stockid);
                dtStock stock = DB.dtStocks.Single(s => s.sID == RefundObject.StockItem.Stockid);

                stock.Sold = 0;
                stock.SaleID = null;
                details.SaleValue = null;
            }

            DB.dtRefunds.InsertOnSubmit(NewRefund);
            DB.dtStockStatus.InsertOnSubmit(NewStatus);

            DB.SubmitChanges();

        }

        private int getlastrefund()
        {
            var query = (from R in DB.dtRefunds
                         orderby R.rID descending
                         select new { RefundID = R.rID }).Take(1);

            return query.FirstOrDefault().RefundID;
        }

        public List<ITransaction> GetTransactionList(TransactionType TransactionType, int ID, string Title, string Description)
        {
            List<ITransaction> ReturnList = new List<ITransaction>();

            if (TransactionType == TransactionType.All || TransactionType == TransactionType.Sale)
            {
                var SalesQuery = from S in DB.dtSaleTransactions
                                 join U in DB.dtUsers on S.SoldBy equals U.uID
                                 join UD in DB.dtUserDetails on U.uID equals UD.UserID
                                 from A in DB.dtAddresses
                                .Where(x => S.AddressID == x.aID)
                                .DefaultIfEmpty()

                                 where S.tID.Equals(ID == 0 ? S.tID : ID)
                                 && S.Title.IndexOf(Title == null ? S.Title : Title) >= 0
                                 && S.Description.IndexOf(Description == null ? S.Description : Description) >= 0

                                 select new skSales
                                 {
                                     ID = S.tID,
                                     Title = S.Title,
                                     Description = S.Description,
                                     Postage = S.PandP,
                                     Amount = S.SoldValue,
                                     SaleDate = S.SoldDate,
                                     PayPalFees = S.PaypayFees,
                                     AddressId = S.AddressID,
                                     PayPalTransactionID = S.PayPalTransactionID,
                                     Created = S.Created,
                                     Updated = S.Updated,
                                     TransactionTime = S.Created,
                                     SaleMethod = S.SaleMethod,
                                     UserObj = new skUser
                                     {
                                         UserID = U.uID,
                                         DOB = UD.DateOfBirth,
                                         Email = UD.Email,
                                         FistName = UD.FirstName,
                                         LastName = UD.LastName,
                                         HomePhone = UD.HomePhone,
                                         Initials = UD.Initials,
                                         UserName = U.UserName,
                                         WorkPhone = UD.WorkPhone

                                     },

                                     StockList = new List<skStock>(GetStockListBySaleID(S.tID))
                                 };

                foreach (var item in SalesQuery)
                {
                    ReturnList.Add(item);
                }
            }
            if (TransactionType == TransactionType.All || TransactionType == TransactionType.Purchase)
            {
                var PurchaseQuery = from P in DB.dtPurcheses
                                    from U in DB.dtUsers
                                    .Where(x => P.AddedBy == x.uID)
                                    .DefaultIfEmpty()

                                    where P.pID.Equals(ID == 0 ? P.pID : ID)
                                     && P.ItemTitle.IndexOf(Title == null ? P.ItemTitle : Title) >= 0
                                     && P.ItemDescription.IndexOf(Description == null ? P.ItemDescription : Description) >= 0

                                    select new skPurchase
                                    {
                                        ID = P.pID,
                                        Created = P.Created,
                                        CreatedBy = U.UserName,
                                        PapPalTransactionID = P.PayPalTransactionID,
                                        Description = P.ItemDescription,
                                        Title = P.ItemTitle,
                                        Invoice = P.InvoiceID,
                                        Amount = Convert.ToDecimal(P.PurchesedValue),
                                        Postage = Convert.ToDecimal(P.ShippingCosts),
                                        VendorId = Convert.ToInt32(P.VendorID),
                                        PurchaseDate = P.Purchesed_Date,
                                        TransactionTime = P.Created,
                                        Updated = P.Updated,
                                        LinkedStock = new List<skStock>(GetStockListByPrucahseID(P.pID))
                                    };

                foreach (var item in PurchaseQuery)
                {
                    ReturnList.Add(item);
                }
            }

            if (TransactionType == TransactionType.All || TransactionType == TransactionType.Refund)
            {
                var RefundQuery = from R in DB.dtRefunds
                                  from U in DB.dtUsers
                                  .Where(x => R.RefundedBy == x.uID)
                                  .DefaultIfEmpty()
                                  join UD in DB.dtUserDetails on U.uID equals UD.UserID

                                  join S in DB.dtStocks on R.StockID equals S.sID
                                  join ST in DB.dtSTockTypes on S.ItemTypeID equals ST.itID
                                  from SD in DB.dtStockDetails
                                  .Where(o => o.StockID == S.sID)
                                  .DefaultIfEmpty()

                                  select new skRefund
                                  {
                                      Amount = R.Amount,
                                      Created = R.Created,
                                      Description = R.Reason,
                                      Title = S.ItemTitle,
                                      ID = R.rID,
                                      PayPalTransactionID = R.PayPalTransactionID,
                                      SHippingCosts = R.Postage,
                                      StockItem = new skStock
                                      {
                                          Stockid = S.sID,
                                          category = ST.TypeTitle,
                                          categoryID = ST.itID,
                                          Created = S.Created,
                                          Description = S.ItemDesc,
                                          Name = S.ItemTitle,
                                          purchasedvalue = SD.PurchaseValue,
                                          SaleValue = SD.SaleValue,
                                          Sold = S.Sold,
                                          Updated = S.Updated,

                                      },
                                      TransactionTime = R.Refunded,
                                      TransactionType = TransactionType.Refund,
                                      Updated = R.Updated,
                                      User = new skUser
                                      {
                                          UserID = U.uID,
                                          DOB = UD.DateOfBirth,
                                          Email = UD.Email,
                                          FistName = UD.FirstName,
                                          LastName = UD.LastName,
                                          HomePhone = UD.HomePhone,
                                          Initials = UD.Initials,
                                          UserName = U.UserName,
                                          WorkPhone = UD.WorkPhone
                                      }
                                  };

                foreach (var item in RefundQuery)
                {
                    ReturnList.Add(item);
                }

            }

            ReturnList = ReturnList.OrderBy(x => x.TransactionTime).ToList();
            return ReturnList;

        }
        [Obsolete]
        public IEnumerable<skTransactionLedger> GetTransactionList(skTransactionTypes SelectedTranType, string TranTitle)
        {
            if (SelectedTranType.ID == 2)
            {

                var query = from P in DB.dtPurcheses
                            where P.ItemTitle.IndexOf(TranTitle == null ? P.ItemTitle : TranTitle) >= 0
                            select new skTransactionLedger
                            {
                                TransactionID = P.pID,
                                TransactionType = TransactionType.Purchase,
                                Amount = P.PurchesedValue,
                                TransactionTime = P.Purchesed_Date,
                                Title = P.ItemTitle,
                                Description = P.ItemDescription
                            };

                return query;
            }
            if (SelectedTranType.ID == 1)
            {

                var query = from S in DB.dtSaleTransactions
                            where S.Title.IndexOf(TranTitle == null ? S.Title : TranTitle) >= 0
                            select new skTransactionLedger
                            {
                                TransactionID = S.tID,
                                TransactionType = TransactionType.Sale,
                                Amount = S.SoldValue,
                                TransactionTime = S.SoldDate,
                                Title = S.Title,
                                Description = S.Title
                            };

                return query;
            }
            else
            {

                var query = ((from P in DB.dtPurcheses
                              where P.ItemTitle.IndexOf(TranTitle == null ? P.ItemTitle : TranTitle) >= 0
                              select new skTransactionLedger
                              {
                                  TransactionID = P.pID,
                                  TransactionType = TransactionType.Purchase,
                                  Amount = P.PurchesedValue,
                                  TransactionTime = P.Purchesed_Date,
                                  Title = P.ItemTitle,
                                  Description = P.ItemDescription
                              })
                             .Concat
                                  (from S in DB.dtSaleTransactions
                                   where S.Title.IndexOf(TranTitle == null ? S.Title : TranTitle) >= 0
                                   select new skTransactionLedger
                                   {
                                       TransactionID = S.tID,
                                       TransactionType = TransactionType.Sale,
                                       Amount = S.SoldValue,
                                       TransactionTime = S.SoldDate,
                                       Title = S.Title,
                                       Description = S.Description
                                   }))

                                    .OrderByDescending(x => x.TransactionTime);
                return query;

            }
        }

        public IEnumerable<skTransactionTypes> GetTransactionTypeList()
        {
            var query = from TL in DB.dtTransactionTypes
                        select new skTransactionTypes
                        {
                            ID = TL.ID,
                            TypeName = TL.TypeName,
                            TypeDescription = TL.TypeDescription
                        };
            return query;
        }

        public skSales GetSalesDetails(int saleid)
        {
            skSales robject = new skSales();
            if (saleid > 0)
            {
                var query = (from S in DB.dtSaleTransactions
                             join U in DB.dtUsers on S.SoldBy equals U.uID
                             join UD in DB.dtUserDetails on U.uID equals UD.UserID
                             from A in DB.dtAddresses
                            .Where(x => S.AddressID == x.aID)
                            .DefaultIfEmpty()
                             where S.tID == saleid
                             select new skSales
                             {
                                 ID = S.tID,
                                 Title = S.Title,
                                 Description = S.Description,
                                 Postage = S.PandP,
                                 Amount = S.SoldValue,
                                 SaleDate = S.SoldDate,
                                 PayPalFees = S.PaypayFees,
                                 AddressId = S.AddressID,
                                 PayPalTransactionID = S.PayPalTransactionID,
                                 Created = S.Created,
                                 Updated = S.Updated,
                                 SaleMethod = S.SaleMethod,
                                 UserObj = new skUser
                                 {
                                     UserID = U.uID,
                                     DOB = UD.DateOfBirth,
                                     Email = UD.Email,
                                     FistName = UD.FirstName,
                                     LastName = UD.LastName,
                                     HomePhone = UD.HomePhone,
                                     Initials = UD.Initials,
                                     UserName = U.UserName,
                                     WorkPhone = UD.WorkPhone

                                 }

                             }).FirstOrDefault();

                var stocklist = from SL in DB.dtStocks
                                join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
                                from TR in DB.dtPurcheses
                                .Where(o => SL.PurchaseID == o.pID)
                                .DefaultIfEmpty()
                                from SD in DB.dtStockDetails
                                .Where(o => o.StockID == SL.sID)
                                .DefaultIfEmpty()
                                where SL.SaleID == query.ID

                                select new skStock
                                {
                                    Stockid = SL.sID,
                                    Name = SL.ItemTitle,
                                    Description = SL.ItemDesc,
                                    purchaseddate = TR.Purchesed_Date,
                                    category = ST.TypeTitle,
                                    purchasedvalue = SD.PurchaseValue,
                                    SaleValue = SD.SaleValue,
                                    Sold = SL.Sold,
                                    categoryID = ST.itID,
                                    Created = SL.Created,
                                    ValueBandID = (int)SL.ValueBandID,

                                };

                //robject.Title = query.FirstOrDefault().Title;
                //robject.Description = query.FirstOrDefault().Description;
                //robject.ID = query.FirstOrDefault().ID;
                //robject.Amount = query.FirstOrDefault().Amount;
                //robject.SaleDate = query.FirstOrDefault().SaleDate;
                //robject.Postage = query.FirstOrDefault().Postage;
                //robject.PayPalFees = query.FirstOrDefault().PayPalFees;
                //robject.AddressId = query.FirstOrDefault().AddressId;
                //robject.PayPalTransactionID = query.FirstOrDefault().PayPalTransactionID;
                //robject.Created = query.FirstOrDefault().Created;
                //robject.Updated = query.FirstOrDefault().Updated;
                //robject.SaleMethod = query.FirstOrDefault().SaleMethod;
                //robject.UserObj = query.FirstOrDefault().UserObj;
                query.StockList = stocklist.ToList<skStock>();

                return query;
            }
            throw new Exception("Missing Sale ID");
        }

        public skSales GetSalesDetailsByStockID(int stockid)
        {
            skSales robject = new skSales();

            var query = from S in DB.dtSaleTransactions
                        join ST in DB.dtStocks on S.tID equals ST.SaleID
                        join U in DB.dtUsers on S.SoldBy equals U.uID
                        join UD in DB.dtUserDetails on U.uID equals UD.UserID
                        from A in DB.dtAddresses
                        .Where(x => S.AddressID == x.aID)
                        .DefaultIfEmpty()
                        where ST.sID == stockid
                        select new skSales
                        {
                            ID = S.tID,
                            Title = S.Title,
                            Description = S.Description,
                            Postage = S.PandP,
                            Amount = S.SoldValue,
                            SaleDate = S.SoldDate,
                            PayPalFees = S.PaypayFees,
                            AddressId = S.AddressID,
                            PayPalTransactionID = S.PayPalTransactionID,
                            Created = S.Created,
                            Updated = S.Updated,
                            SaleMethod = S.SaleMethod,
                            UserObj = new skUser
                            {
                                UserID = U.uID,
                                DOB = UD.DateOfBirth,
                                Email = UD.Email,
                                FistName = UD.FirstName,
                                LastName = UD.LastName,
                                HomePhone = UD.HomePhone,
                                Initials = UD.Initials,
                                UserName = U.UserName,
                                WorkPhone = UD.WorkPhone

                            }
                        };

            var stocklist = from SL in DB.dtStocks
                            join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
                            from TR in DB.dtPurcheses
                            .Where(o => SL.PurchaseID == o.pID)
                            .DefaultIfEmpty()
                            from SD in DB.dtStockDetails
                            .Where(o => o.StockID == SL.sID)
                            .DefaultIfEmpty()
                            where SL.SaleID == query.FirstOrDefault().ID

                            select new skStock
                            {
                                Stockid = SL.sID,
                                Name = SL.ItemTitle,
                                Description = SL.ItemDesc,
                                purchaseddate = TR.Purchesed_Date,
                                category = ST.TypeTitle,
                                purchasedvalue = SD.PurchaseValue,
                                Sold = SL.Sold,
                                categoryID = ST.itID,
                                Created = SL.Created,
                                ValueBandID = (int)SL.ValueBandID,
                                SaleValue = SD.SaleValue

                            };

            robject.Title = query.FirstOrDefault().Title;
            robject.Description = query.FirstOrDefault().Description;
            robject.ID = query.FirstOrDefault().ID;
            robject.Amount = query.FirstOrDefault().Amount;
            robject.SaleDate = query.FirstOrDefault().SaleDate;
            robject.Postage = query.FirstOrDefault().Postage;
            robject.PayPalFees = query.FirstOrDefault().PayPalFees;
            robject.AddressId = query.FirstOrDefault().AddressId;
            robject.PayPalTransactionID = query.FirstOrDefault().PayPalTransactionID;
            robject.Created = query.FirstOrDefault().Created;
            robject.Updated = query.FirstOrDefault().Updated;
            robject.SaleMethod = query.FirstOrDefault().SaleMethod;
            robject.UserObj = query.FirstOrDefault().UserObj;
            robject.StockList = stocklist.ToList<skStock>();

            return robject;
        }

        public skPurchase GetPurchaseDetails(int ID)
        {
            var query = from P in DB.dtPurcheses
                        from U in DB.dtUsers
                        .Where(x => P.AddedBy == x.uID)
                        .DefaultIfEmpty()
                        where P.pID == ID
                        select new skPurchase
                        {
                            ID = P.pID,
                            Created = P.Created,
                            CreatedBy = U.UserName,
                            PapPalTransactionID = P.PayPalTransactionID,
                            Description = P.ItemDescription,
                            Title = P.ItemTitle,
                            Invoice = P.InvoiceID,
                            Amount = Convert.ToDecimal(P.PurchesedValue),
                            Postage = Convert.ToDecimal(P.ShippingCosts),
                            VendorId = Convert.ToInt32(P.VendorID),
                            PurchaseDate = P.Purchesed_Date,
                            Updated = P.Updated

                        };

            var StockQuery = from SL in DB.dtStocks
                             join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
                             join SD in DB.dtStockDetails on SL.sID equals SD.StockID
                             where SL.PurchaseID == query.FirstOrDefault().ID
                             select new skStock
                             {
                                 Stockid = SL.sID,
                                 Name = SL.ItemTitle,
                                 Description = SL.ItemDesc,
                                 category = ST.TypeTitle,
                                 Sold = SL.Sold,
                                 categoryID = ST.itID,
                                 Created = SL.Created,
                                 ValueBandID = (int)SL.ValueBandID,
                                 purchasedvalue = SD.PurchaseValue
                             };

            skPurchase robject = new skPurchase();
            robject.Title = query.FirstOrDefault().Title;
            robject.Description = query.FirstOrDefault().Description;
            robject.Invoice = query.FirstOrDefault().Invoice;
            robject.Created = query.FirstOrDefault().Created;
            robject.CreatedBy = query.FirstOrDefault().CreatedBy;
            robject.PapPalTransactionID = query.FirstOrDefault().PapPalTransactionID;
            robject.Postage = query.FirstOrDefault().Postage;
            robject.Amount = query.FirstOrDefault().Amount;
            robject.ID = query.FirstOrDefault().ID;
            robject.VendorId = query.FirstOrDefault().VendorId;
            robject.Created = query.FirstOrDefault().Created;
            robject.Updated = query.FirstOrDefault().Updated;
            robject.PurchaseDate = query.FirstOrDefault().PurchaseDate;
            robject.LinkedStock = StockQuery.ToList<skStock>();

            return robject;
        }



        public skPurchase GetPurchaseDetailsByStockID(int stockid)
        {
            var query = from P in DB.dtPurcheses
                        from U in DB.dtUsers
                        .Where(x => P.AddedBy == x.uID)
                        .DefaultIfEmpty()
                        join S in DB.dtStocks on P.pID equals S.PurchaseID
                        where S.sID == stockid
                        select new skPurchase
                        {
                            ID = P.pID,
                            Created = P.Created,
                            CreatedBy = U.UserName,
                            PapPalTransactionID = P.PayPalTransactionID,
                            Description = P.ItemDescription,
                            Title = P.ItemTitle,
                            Invoice = P.InvoiceID,
                            Amount = Convert.ToDecimal(P.PurchesedValue),
                            Postage = Convert.ToDecimal(P.ShippingCosts),
                            VendorId = Convert.ToInt32(P.VendorID),
                            PurchaseDate = P.Purchesed_Date,
                            Updated = P.Updated

                        };

            var StockQuery = from SL in DB.dtStocks
                             join SD in DB.dtStockDetails on SL.sID equals SD.StockID
                             join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
                             where SL.PurchaseID == query.FirstOrDefault().ID
                             select new skStock
                             {
                                 Stockid = SL.sID,
                                 Name = SL.ItemTitle,
                                 Description = SL.ItemDesc,
                                 category = ST.TypeTitle,
                                 Sold = SL.Sold,
                                 categoryID = ST.itID,
                                 Created = SL.Created,
                                 ValueBandID = (int)SL.ValueBandID,
                                 SaleValue = SD.SaleValue,
                                 purchasedvalue = SD.PurchaseValue
                             };

            skPurchase robject = new skPurchase();
            robject.Title = query.FirstOrDefault().Title;
            robject.Description = query.FirstOrDefault().Description;
            robject.Invoice = query.FirstOrDefault().Invoice;
            robject.Created = query.FirstOrDefault().Created;
            robject.CreatedBy = query.FirstOrDefault().CreatedBy;
            robject.PapPalTransactionID = query.FirstOrDefault().PapPalTransactionID;
            robject.Postage = query.FirstOrDefault().Postage;
            robject.Amount = query.FirstOrDefault().Amount;
            robject.ID = query.FirstOrDefault().ID;
            robject.VendorId = query.FirstOrDefault().VendorId;
            robject.Created = query.FirstOrDefault().Created;
            robject.Updated = query.FirstOrDefault().Updated;
            robject.PurchaseDate = query.FirstOrDefault().PurchaseDate;
            robject.LinkedStock = StockQuery.ToList<skStock>();

            return robject;
        }

        //public skSales GetSalesDetailsByStockID(int stockid)
        //{

        //}

        public bool AddNewVendor(skvendors VendorObj)
        {
            if (VendorObj != null)
            {
                dtVendor NewVendor = new dtVendor
                {
                    VendorsName = VendorObj.VendorsName,
                    VendorsDescption = VendorObj.VendorsDescption,
                    OnlineVender = VendorObj.onlineVendor,
                    AddresseID = Convert.ToInt32(VendorObj.AddresseID)
                };

                DB.dtVendors.InsertOnSubmit(NewVendor);
                DB.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewVendor(skvendors VendorObj, skAddresses AddressObject)
        {
            if (VendorObj != null)
            {
                dtVendor NewVendor = new dtVendor
                {
                    VendorsName = VendorObj.VendorsName,
                    VendorsDescption = VendorObj.VendorsDescption,
                    OnlineVender = VendorObj.onlineVendor,

                };

                dtAddress NewAdress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    isActive = true
                };

                NewAdress.dtVendors.Add(NewVendor);


                DB.dtAddresses.InsertOnSubmit(NewAdress);
                DB.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewAddress(skAddresses AddressObject, skPerson PersonObject = null)
        {
            if (AddressObject != null)
            {
                dtAddress NewAdress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    isActive = true
                };

                DB.dtAddresses.InsertOnSubmit(NewAdress);
                DB.SubmitChanges();

                if (PersonObject != null)
                {
                    dtPerson newPerson = new dtPerson
                    {
                        AddressID = Convert.ToInt32(GetLatestAddress()),
                        EbayName = PersonObject.EbayName,
                        Email = PersonObject.Email,
                        FirstName = PersonObject.FirstName,
                        SureName = PersonObject.Surname,
                        HomePhone = PersonObject.HomePhone,
                        WorkPhone = PersonObject.WorkPhone
                    };

                    DB.dtPersons.InsertOnSubmit(newPerson);
                    DB.SubmitChanges();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<skAddresses> GetAdressesList()
        {
            var query = from A in DB.dtAddresses
                        select new skAddresses
                        {
                            AddressLine1 = A.AddressLine1,
                            AddressLine2 = A.AddressLine2,
                            AddressLine3 = A.AddressLine3,
                            Country = A.Country,
                            County = A.County,
                            PostCode = A.PostCode,
                            Town = A.Town,
                            Active = A.isActive,
                            AddressID = A.aID
                        };

            return query;
        }

        public skAddresses GetAddressObject(int AddressID)
        {
            var query = from A in DB.dtAddresses
                        where A.aID == AddressID
                        select new skAddresses
                        {
                            AddressLine1 = A.AddressLine1,
                            AddressLine2 = A.AddressLine2,
                            AddressLine3 = A.AddressLine3,
                            Country = A.Country,
                            County = A.County,
                            PostCode = A.PostCode,
                            Town = A.Town,
                            Active = A.isActive,
                            AddressID = A.aID
                        };

            skAddresses Aobj = new skAddresses();
            if (query.FirstOrDefault().AddressLine1 != null)
            {
                Aobj.Active = query.FirstOrDefault().Active;
                Aobj.AddressLine1 = query.FirstOrDefault().AddressLine1;
                Aobj.AddressLine2 = query.FirstOrDefault().AddressLine2;
                Aobj.AddressLine3 = query.FirstOrDefault().AddressLine3;
                Aobj.AddressID = query.FirstOrDefault().AddressID;
                Aobj.Country = query.FirstOrDefault().Country;
                Aobj.County = query.FirstOrDefault().County;
                Aobj.PersonID = query.FirstOrDefault().PersonID;
                Aobj.PostCode = query.FirstOrDefault().PostCode;
                Aobj.Town = query.FirstOrDefault().Town;
            }

            return Aobj;
        }

        public bool AddNewPerson(skPerson PersonObject)
        {
            if (PersonObject != null)
            {
                //Do Stuff

                return true;
            }
            else
            {
                return false;
            }
        }

        public skPerson GetPersonObject(int personID)
        {
            var query = from A in DB.dtPersons
                        where A.pID == personID
                        select new skPerson
                        {
                            AddressID = Convert.ToInt32(A.AddressID),
                            EbayName = A.EbayName,
                            Email = A.Email,
                            FirstName = A.FirstName,
                            Surname = A.SureName,
                            HomePhone = A.HomePhone,
                            WorkPhone = A.HomePhone

                        };

            skPerson Pobj = new skPerson();
            Pobj.AddressID = query.FirstOrDefault().AddressID;
            Pobj.EbayName = query.FirstOrDefault().EbayName;
            Pobj.Email = query.FirstOrDefault().Email;
            Pobj.FirstName = query.FirstOrDefault().FirstName;
            Pobj.Surname = query.FirstOrDefault().Surname;
            Pobj.pID = query.FirstOrDefault().pID;
            Pobj.HomePhone = query.FirstOrDefault().HomePhone;
            Pobj.WorkPhone = query.FirstOrDefault().WorkPhone;

            return Pobj;
        }

        public IEnumerable<skPerson> GetPersonList()
        {
            var query = from A in DB.dtPersons
                        select new skPerson
                        {
                            AddressID = Convert.ToInt32(A.AddressID),
                            EbayName = A.EbayName,
                            Email = A.Email,
                            FirstName = A.FirstName,
                            Surname = A.SureName,
                            HomePhone = A.HomePhone,
                            WorkPhone = A.HomePhone

                        };

            return query;
        }

        public IEnumerable<skUser> GetUserList()
        {
            var query = from U in DB.dtUsers
                        from UD in DB.dtUserDetails
                        .Where(x => x.UserID == U.uID)
                        .DefaultIfEmpty()
                        select new skUser
                        {
                            Password = U.PassWord,
                            UserID = U.uID,
                            UserName = U.UserName,
                            DOB = UD.DateOfBirth,
                            FistName = UD.FirstName,
                            LastName = UD.LastName,
                            Email = UD.Email,
                            HomePhone = UD.HomePhone,
                            Initials = UD.Initials,
                            WorkPhone = UD.WorkPhone

                        };

            return query;
        }

        public int? GetLatestAddress()
        {
            int? query = (from A in DB.dtAddresses
                          orderby A.aID
                          select new { AdressID = A.aID }).FirstOrDefault().AdressID;

            return query;

        }

        public void AddNewCategory(skCategory categoryobj)
        {
            dtSTockType newcat = new dtSTockType
            {
                TypeDescription = categoryobj.Description,
                TypeTitle = categoryobj.Name

            };

            DB.dtSTockTypes.InsertOnSubmit(newcat);
            DB.SubmitChanges();
        }

        public skUser GetUserObj(int userid)
        {
            skUser User = new skUser();
            var query = from U in DB.dtUsers
                        from UD in DB.dtUserDetails
                        .Where(x => x.UserID == U.uID)
                        .DefaultIfEmpty()
                        where U.uID == userid
                        select new skUser
                        {
                            Password = U.PassWord,
                            UserID = U.uID,
                            UserName = U.UserName,
                            DOB = UD.DateOfBirth,
                            FistName = UD.FirstName,
                            LastName = UD.LastName,
                            Email = UD.Email,
                            HomePhone = UD.HomePhone,
                            Initials = UD.Initials,
                            WorkPhone = UD.WorkPhone
                        };
            if (query != null)
            {
                User.UserName = query.FirstOrDefault().UserName;
                User.UserID = query.FirstOrDefault().UserID;
                User.Password = query.FirstOrDefault().Password;
                User.FistName = query.FirstOrDefault().FistName;
                User.LastName = query.FirstOrDefault().LastName;
                User.DOB = query.FirstOrDefault().DOB;
                User.Email = query.FirstOrDefault().Email;
                User.HomePhone = query.FirstOrDefault().HomePhone;
                User.WorkPhone = query.FirstOrDefault().HomePhone;
                User.Initials = query.FirstOrDefault().Initials;

                return User;
            }

            return User;

        }

        public void UpdateUser(skUser UserObj, bool ResetPassword)
        {
            dtUser user = DB.dtUsers.Single(x => x.uID == UserObj.UserID);
            if (ResetPassword)
            {
                user.PassWord = UserObj.Password;
            }
            user.UserName = UserObj.UserName;

            dtUserDetail userdetail = DB.dtUserDetails.Single(x => x.UserID == UserObj.UserID);

            userdetail.DateOfBirth = UserObj.DOB;
            userdetail.Email = UserObj.Email;
            userdetail.FirstName = UserObj.FistName;
            userdetail.LastName = UserObj.LastName;
            userdetail.HomePhone = UserObj.HomePhone;
            userdetail.WorkPhone = UserObj.WorkPhone;
            userdetail.Initials = UserObj.Initials;

            DB.SubmitChanges();
        }

        public void AddNewUser(skUser UserObj)
        {
            try
            {
                dtUser newuser = new dtUser
                {
                    UserName = UserObj.UserName,
                    PassWord = UserObj.Password,
                    Created = DateTime.Now,
                    IsAdmin = UserObj.IsAdmin
                };

                dtUserDetail newuserdetails = new dtUserDetail
                {
                    FirstName = UserObj.FistName,
                    LastName = UserObj.LastName,
                    DateOfBirth = UserObj.DOB,
                    Initials = UserObj.Initials,
                    Email = UserObj.Email,
                    HomePhone = UserObj.HomePhone,
                    WorkPhone = UserObj.WorkPhone,
                    dtUser = newuser
                };

                dtUserPreferance newuserpref = new dtUserPreferance
                {
                    Code = "MAINTOOLCO",
                    Description = "Main tool bar background colour",
                    Name = "Main ToolBar",
                    UserID = GetLatestUserID() + 1,
                    PreferenceTypeID = 1,
                    Value = "Black"
                };

                DB.dtUserDetails.InsertOnSubmit(newuserdetails);
                DB.dtUserPreferances.InsertOnSubmit(newuserpref);

                DB.SubmitChanges();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E, "User Creation Failed");
                ExepionLogger.Logger.Show(E);
            }

        }

        public int GetLatestUserID()
        {
            int id = (from U in DB.dtUsers
                      orderby U.uID descending
                      select new { UserID = U.uID }).Take(1).FirstOrDefault().UserID;
            return id;

        }

        public void LinkTranToStock(ITransaction TransactionObject, skStock StockOBJ)
        {

            dtStock StockObj = DB.dtStocks.Single(x => x.sID == StockOBJ.Stockid);
            dtStockDetail StockDetais = DB.dtStockDetails.Single(x => x.StockID == StockOBJ.Stockid);

            StockDetais.PurchaseValue = StockOBJ.purchasedvalue;
            StockDetais.SaleValue = StockOBJ.SaleValue;


            switch (TransactionObject.TransactionType)
            {
                case TransactionType.Sale:
                    dtSaleTransaction SaleObj = DB.dtSaleTransactions.Single(x => x.tID == TransactionObject.ID);
                    SaleObj.SoldValue += StockDetais.SaleValue;
                    StockObj.SaleID = TransactionObject.ID;
                    CreateNewStockStatus(StockOBJ.Stockid, "Linked To sale " + SaleObj.tID, Convert.ToDecimal(StockOBJ.SaleValue));
                    break;

                case TransactionType.Purchase:
                    dtPurchese PurchaseObj = DB.dtPurcheses.Single(x => x.pID == TransactionObject.ID);
                    PurchaseObj.PurchesedValue += StockDetais.PurchaseValue;
                    StockObj.PurchaseID = TransactionObject.ID;
                    CreateNewStockStatus(StockOBJ.Stockid, "Linked To Purchase " + PurchaseObj.pID, Convert.ToDecimal(StockOBJ.purchasedvalue));
                    break;
            }

            DB.SubmitChanges();

        }

        public void RemoveStockFromPurchase(int StockID)
        {
            dtStock StockObj = DB.dtStocks.Single(x => x.sID == StockID);
            dtStockDetail StockDetailObj = DB.dtStockDetails.Single(x => x.StockID == StockObj.sID);
            dtPurchese PurchaseOBJ;

            if (StockObj.PurchaseID != null)
            {
                PurchaseOBJ = DB.dtPurcheses.Single(x => x.pID == StockObj.PurchaseID);

                if (StockDetailObj.PurchaseValue > PurchaseOBJ.PurchesedValue)
                {
                    PurchaseOBJ.PurchesedValue = 0;
                }
                else
                {
                    PurchaseOBJ.PurchesedValue -= StockDetailObj.PurchaseValue;
                }

                StockDetailObj.PurchaseValue = null;
                StockObj.PurchaseID = null;

                DB.SubmitChanges();

                CreateNewStockStatus(StockID, "Removed from Purchase");

            }
        }

        public void RemoveStockFromSale(int StockID)
        {
            dtStock StockObj = DB.dtStocks.Single(x => x.sID == StockID);
            dtStockDetail StockDetailObj = DB.dtStockDetails.Single(x => x.StockID == StockObj.sID);
            dtSaleTransaction SaleOBJ;

            if (StockObj.SaleID != null)
            {
                SaleOBJ = DB.dtSaleTransactions.Single(x => x.tID == StockObj.SaleID);

                if (StockDetailObj.SaleValue > SaleOBJ.SoldValue)
                {
                    SaleOBJ.SoldValue = 0;
                }
                else
                {
                    SaleOBJ.SoldValue -= StockDetailObj.SaleValue;
                }

                StockDetailObj.SaleValue = null;
                StockObj.SaleID = null;

                DB.SubmitChanges();

                CreateNewStockStatus(StockID, "Removed from sale");
            }
        }

        //public void CreateNewStockStatus(long stockid, string Status, decimal Value = 0)
        //{
        //    dtStockStatus StatusOBJ = new dtStockStatus
        //    {
        //        Created = DateTime.Now,
        //        Status = Status,
        //        StoockID = Convert.ToInt32(stockid),
        //        UserID = StaticDataReposityory.UserID,
        //        StatusID = 0,
        //        Value = Value
        //    };

        //    DB.dtStockStatus.InsertOnSubmit(StatusOBJ);
        //    DB.SubmitChanges();

        //}

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

                    var newsaledate = (from S in DB.dtSaleTransactions
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

        //public skStock GetStockDetails(int StockID)
        //{
        //    var query = (from SL in DB.dtStocks
        //                 join ST in DB.dtSTockTypes on SL.ItemTypeID equals ST.itID
        //                 from TR in DB.dtPurcheses
        //                  .Where(o => SL.PurchaseID == o.pID)
        //                      .DefaultIfEmpty()
        //                 from SD in DB.dtStockDetails
        //            .Where(o => o.StockID == SL.sID)
        //            .DefaultIfEmpty()
        //                 where SL.sID == StockID
        //                 select new skStock
        //                 {
        //                     Stockid = SL.sID,
        //                     Name = SL.ItemTitle,
        //                     Description = SL.ItemDesc,
        //                     purchaseddate = TR.Purchesed_Date,
        //                     category = ST.TypeTitle,
        //                     purchasedvalue = SD.PurchaseValue,
        //                     Sold = SL.Sold,
        //                     categoryID = ST.itID,
        //                     Created = SL.Created,
        //                     ValueBandID = (int)SL.ValueBandID,
        //                     Updated = SL.Updated

        //                 }).FirstOrDefault(); ;

        //    return query;
        //}

        public skRefund GetRefundDetails(int RefundID)
        {
            var RefundQuery = (from R in DB.dtRefunds
                               from U in DB.dtUsers
                               .Where(x => R.RefundedBy == x.uID)
                               .DefaultIfEmpty()
                               join UD in DB.dtUserDetails on U.uID equals UD.UserID

                               join S in DB.dtStocks on R.StockID equals S.sID
                               join ST in DB.dtSTockTypes on S.ItemTypeID equals ST.itID
                               from SD in DB.dtStockDetails
                               .Where(o => o.StockID == S.sID)
                               .DefaultIfEmpty()

                               select new skRefund
                               {
                                   Amount = R.Amount,
                                   Created = R.Created,
                                   Description = R.Reason,
                                   Title = S.ItemTitle,
                                   ID = R.rID,
                                   PayPalTransactionID = R.PayPalTransactionID,
                                   SHippingCosts = R.Postage,
                                   StockItem = new skStock
                                   {
                                       Stockid = S.sID,
                                       category = ST.TypeTitle,
                                       categoryID = ST.itID,
                                       Created = S.Created,
                                       Description = S.ItemDesc,
                                       Name = S.ItemTitle,
                                       purchasedvalue = SD.PurchaseValue,
                                       SaleValue = SD.SaleValue,
                                       Sold = S.Sold,
                                       Updated = S.Updated,

                                   },
                                   TransactionTime = R.Refunded,
                                   TransactionType = TransactionType.Refund,
                                   Updated = R.Updated,
                                   User = new skUser
                                   {
                                       UserID = U.uID,
                                       DOB = UD.DateOfBirth,
                                       Email = UD.Email,
                                       FistName = UD.FirstName,
                                       LastName = UD.LastName,
                                       HomePhone = UD.HomePhone,
                                       Initials = UD.Initials,
                                       UserName = U.UserName,
                                       WorkPhone = UD.WorkPhone
                                   }
                               }).FirstOrDefault();

            return RefundQuery;
        }

    }
}
