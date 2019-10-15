using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.DataAcesse.DataBase;
using Stocky.Data;
using Stocky.Data.Interfaces;
using Stocky.Utility;
using Stocky.Exceptions;

namespace Stocky.DataAcesse.Reposetories
{
    public class TransactionRepo : RepoBase
    {

        #region SelectMethods
        public List<ITransaction> GetTransactionList(SearchFilterObject SearchFilter)
        {
            List<ITransaction> ReturnList = new List<ITransaction>();

            StockRepo StockRepo = new StockRepo();
            AddressRepo AddressRepo = new AddressRepo();

            TransactionType TranType = (TransactionType)SearchFilter.ExtraSearchObject1;

            if (TranType == TransactionType.All || TranType == TransactionType.Sale)
            {
                var SalesQuery = (from S in DB.dtSales
                                  join U in DB.dtUsers on S.SoldBy equals U.uID
                                  join UD in DB.dtUserDetails on U.uID equals UD.UserID
                                  join P in DB.dtPersons on S.PersonID equals P.pID

                                  where S.tID.ToString().Equals(SearchFilter.ObjectID == null ? S.tID.ToString() : SearchFilter.ObjectID)
                                  && S.Title.IndexOf(SearchFilter.ObjectName == null ? S.Title : SearchFilter.ObjectName) >= 0
                                  && S.Description.IndexOf(SearchFilter.ObjectDescription == null ? S.Description : SearchFilter.ObjectDescription) >= 0
                                   && (S.Created >= SearchFilter.ObjectCreatedFrom && S.Created <= SearchFilter.ObjectCreatedTo)
                                  select new skSales
                                  {
                                      ID = S.tID,
                                      Title = S.Title,
                                      Description = S.Description,
                                      Postage = S.PandP,
                                      Amount = S.SoldValue,
                                      SaleDate = S.SoldDate,
                                      PayPalFees = S.PaypayFees,
                                      PayPalTransactionID = S.PayPalTransactionID,
                                      Created = S.Created,
                                      Updated = S.Updated,
                                      TransactionTime = S.SoldDate,
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
                                      PersonObj = new skPerson
                                      {
                                          pID = P.pID,
                                          Created = P.Created,
                                          EbayName = P.EbayName,
                                          Email = P.Email,
                                          FirstName = P.FirstName,
                                          HomePhone = P.HomePhone,
                                          LinkedAddresses = AddressRepo.GetPersonAddressList(P.pID).ToList(),
                                          Mobile = P.WorkPhone,
                                          WorkPhone = P.WorkPhone,
                                          Surname = P.SureName,
                                          Updated = P.Updated
                                      }
                                     ,
                                      StockList = new List<skStock>(StockRepo.GetStockListBySaleID(S.tID))
                                  }).Take(SearchFilter.RecordsToReturn == 0 ? 10000 : SearchFilter.RecordsToReturn);

                foreach (var item in SalesQuery)
                {
                    ReturnList.Add(item);
                }
            }
            if (TranType == TransactionType.All || TranType == TransactionType.Purchase)
            {
                var VendorRepo = new VendorRepo();

                var PurchaseQuery = (from P in DB.dtPurcheses
                                    from U in DB.dtUsers
                                    .Where(x => P.AddedBy == x.uID)
                                    .DefaultIfEmpty()
                                    where P.pID.ToString().Equals(SearchFilter.ObjectID == null ? P.pID.ToString() : SearchFilter.ObjectID)
                                     && P.ItemTitle.IndexOf(SearchFilter.ObjectName == null ? P.ItemTitle : SearchFilter.ObjectName) >= 0
                                     && P.ItemDescription.IndexOf(SearchFilter.ObjectDescription == null ? P.ItemDescription : SearchFilter.ObjectDescription) >= 0
                                          && (P.Created >= SearchFilter.ObjectCreatedFrom && P.Created <= SearchFilter.ObjectCreatedTo)
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
                                        VendorObject = VendorRepo.GetVendorDetails(Convert.ToInt32(P.VendorID)),
                                        PurchaseDate = P.Purchesed_Date,
                                        TransactionTime = P.Purchesed_Date,
                                        Updated = P.Updated,
                                        LinkedStock = new List<skStock>(StockRepo.GetStockListByPrucahseID(P.pID))
                                    }).Take(SearchFilter.RecordsToReturn == 0 ? 10000 : SearchFilter.RecordsToReturn);

                foreach (var item in PurchaseQuery)
                {
                    ReturnList.Add(item);
                }
            }

            if (TranType == TransactionType.All || TranType == TransactionType.Refund)
            {
                var RefundQuery = (from R in DB.dtRefunds
                                  from U in DB.dtUsers
                                  .Where(x => R.RefundedBy == x.uID)
                                  .DefaultIfEmpty()
                                  join UD in DB.dtUserDetails on U.uID equals UD.UserID

                                  join S in DB.dtStocks on R.StockID equals S.sID
                                  join ST in DB.dtCategories on S.CategoryID equals ST.CatID
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
                                          Created = S.Created,
                                          Description = S.ItemDesc,
                                          Name = S.ItemTitle,
                                          purchasedvalue = SD.PurchaseValue,
                                          SaleValue = SD.SaleValue,
                                          Sold = S.Sold,
                                          Updated = S.Updated,
                                          CategoryObject = new skCategory
                                          {
                                              Description = ST.Description,
                                              Name = ST.Title,
                                              StockTypeID = ST.CatID
                                          }

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
                                  }).Take(SearchFilter.RecordsToReturn == 0 ? 10000 : SearchFilter.RecordsToReturn);

                foreach (var item in RefundQuery)
                {
                    ReturnList.Add(item);
                }

            }

            ReturnList = ReturnList.OrderBy(x => x.TransactionTime).Take(SearchFilter.RecordsToReturn == 0? 10000 : SearchFilter.RecordsToReturn).ToList();
            ReturnList = ReturnList.OrderBy(x => x.TransactionTime).Take(SearchFilter.RecordsToReturn == 0 ? 10000 : SearchFilter.RecordsToReturn).ToList();
            return ReturnList;

        }

        public skSales GetSalesDetails(int saleid)
        {

            AddressRepo AddressRepo = new AddressRepo();

            if (saleid > 0)
            {
                var query = (from S in DB.dtSales
                             join U in DB.dtUsers on S.SoldBy equals U.uID
                             join UD in DB.dtUserDetails on U.uID equals UD.UserID
                             join P in DB.dtPersons on S.PersonID equals P.pID
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

                                 },
                                 PersonObj = new skPerson
                                 {
                                     pID = P.pID,
                                     Created = P.Created,
                                     EbayName = P.EbayName,
                                     Email = P.Email,
                                     FirstName = P.FirstName,
                                     HomePhone = P.HomePhone,
                                     LinkedAddresses = AddressRepo.GetPersonAddressList(P.pID).ToList(),
                                     Mobile = P.WorkPhone,
                                     WorkPhone = P.WorkPhone,
                                     Surname = P.SureName,
                                     Updated = P.Updated
                                 }

                             }).FirstOrDefault();

                var stocklist = from SL in DB.dtStocks
                                join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                                join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
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
                                    purchasedvalue = SD.PurchaseValue,
                                    SaleValue = SD.SaleValue,
                                    Sold = SL.Sold,
                                    Created = SL.Created,
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
                if (query == null)
                {
                    throw new NoRecordException(typeof(skSales));
                }
                else
                {
                    query.StockList = stocklist.ToList<skStock>();

                    return query;
                }
            }
            throw new Exception("Missing Sale ID");
        }

        public skRefund GetRefundDetails(int RefundID)
        {
            var RefundQuery = (from R in DB.dtRefunds
                               from U in DB.dtUsers
                               .Where(x => R.RefundedBy == x.uID)
                               .DefaultIfEmpty()
                               join UD in DB.dtUserDetails on U.uID equals UD.UserID

                               join S in DB.dtStocks on R.StockID equals S.sID
                               join ST in DB.dtCategories on S.CategoryID equals ST.CatID
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
                                       Created = S.Created,
                                       Description = S.ItemDesc,
                                       Name = S.ItemTitle,
                                       purchasedvalue = SD.PurchaseValue,
                                       SaleValue = SD.SaleValue,
                                       Sold = S.Sold,
                                       Updated = S.Updated,
                                       CategoryObject = new skCategory
                                       {
                                           Description = ST.Description,
                                           Name = ST.Title,
                                           StockTypeID = ST.CatID
                                       }

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

            if(RefundQuery == null)
            {
                throw new NoRecordException(typeof(skRefund));
            }

            return RefundQuery;
        }

        public skSales GetSalesDetailsByStockID(int stockid)
        {

            AddressRepo AddressRepo = new AddressRepo();

            var query = (from S in DB.dtSales
                        join ST in DB.dtStocks on S.tID equals ST.SaleID
                        join U in DB.dtUsers on S.SoldBy equals U.uID
                        join UD in DB.dtUserDetails on U.uID equals UD.UserID
                        join P in DB.dtPersons on S.PersonID equals P.pID
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
                            },
                            PersonObj = new skPerson
                            {
                                pID = P.pID,
                                Created = P.Created,
                                EbayName = P.EbayName,
                                Email = P.Email,
                                FirstName = P.FirstName,
                                HomePhone = P.HomePhone,
                                LinkedAddresses = AddressRepo.GetPersonAddressList(P.pID).ToList(),
                                Mobile = P.WorkPhone,
                                WorkPhone = P.WorkPhone,
                                Surname = P.SureName,
                                Updated = P.Updated
                            }

                        }).FirstOrDefault();

            var stocklist = from SL in DB.dtStocks
                            join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                            join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
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
                                purchasedvalue = SD.PurchaseValue,
                                Sold = SL.Sold,
                                Created = SL.Created,
                                SaleValue = SD.SaleValue,
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
            if (query == null)
            {
                throw new NoRecordException(typeof(skSales));
            }
            else
            {
                query.StockList = stocklist.ToList<skStock>();

                return query;
            }
        }

        public skPurchase GetPurchaseDetails(int ID)
        {
            var VendorRepo = new VendorRepo();

            var query = (from P in DB.dtPurcheses
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
                             VendorObject = VendorRepo.GetVendorDetails(Convert.ToInt32(P.VendorID)),
                            PurchaseDate = P.Purchesed_Date,
                            Updated = P.Updated

                        }).FirstOrDefault();

            var StockQuery = from SL in DB.dtStocks
                             join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                             join SD in DB.dtStockDetails on SL.sID equals SD.StockID
                             join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                             where SL.PurchaseID == query.ID
                             select new skStock
                             {
                                 Stockid = SL.sID,
                                 Name = SL.ItemTitle,
                                 Description = SL.ItemDesc,
                                 Sold = SL.Sold,
                                 Created = SL.Created,
                                 purchasedvalue = SD.PurchaseValue,
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
            if (query == null)
            {
                throw new NoRecordException(typeof(skPurchase));
            }
            else
            {
                query.LinkedStock = StockQuery.ToList<skStock>();

                return query;
            }
        }

        public skPurchase GetPurchaseDetailsByStockID(int stockid)
        {
            var VendorRepo = new VendorRepo();

            var query = (from P in DB.dtPurcheses
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
                            VendorObject = VendorRepo.GetVendorDetails(Convert.ToInt32(P.VendorID)),
                            PurchaseDate = P.Purchesed_Date,
                            Updated = P.Updated

                        }).FirstOrDefault();

            var StockQuery = from SL in DB.dtStocks
                             join SD in DB.dtStockDetails on SL.sID equals SD.StockID
                             join ST in DB.dtCategories on SL.CategoryID equals ST.CatID
                             join VB in DB.dtValueBands on SL.ValueBandID equals VB.ivID
                             where SL.PurchaseID == query.ID
                             select new skStock
                             {
                                 Stockid = SL.sID,
                                 Name = SL.ItemTitle,
                                 Description = SL.ItemDesc,
                                 Sold = SL.Sold,
                                 Created = SL.Created,
                                 SaleValue = SD.SaleValue,
                                 purchasedvalue = SD.PurchaseValue,
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
            if (query == null)
            {
                throw new NoRecordException(typeof(skPurchase));
            }
            else
            {
                query.LinkedStock = StockQuery.ToList<skStock>();

                return query;
            }
        }

        #endregion

        #region InsertMethods

        public void AddNewPurchase(skPurchase PurchaseOb,int UserID)
        {
           
                dtPurchese newpurchase = new dtPurchese
                {
                    ItemTitle = PurchaseOb.Title,
                    ItemDescription = PurchaseOb.Description,
                    PurchesedValue = PurchaseOb.Amount,
                    ShippingCosts = PurchaseOb.Postage,
                    InvoiceID = PurchaseOb.Invoice,
                    Purchesed_Date = PurchaseOb.PurchaseDate,
                    AddedBy = UserID,
                    VendorID = PurchaseOb.VendorObject.VendorID,
                    IsExpense = PurchaseOb.IsExpense,
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

                DB.dtTransactionLedgers.InsertOnSubmit(newpurchaseledger);
                DB.dtTransactionLedgers.InsertOnSubmit(newshippingledger);
                DB.SubmitChanges();          
        }



        public void AddNewSale(skSales TransactionObject,skPerson PersonObject,skAddresses AddressObject, int UserID)
        {

            PersonRepo PersonRepo = new PersonRepo();

                PersonRepo.AddNewPerson(PersonObject, AddressObject);
                var personid = DB.dtPersons.OrderBy(x => x.Created).FirstOrDefault().pID;

            dtSale newtransaction = new dtSale
            {
                SoldValue = TransactionObject.Amount,
                PaypayFees = TransactionObject.PayPalFees,
                PandP = TransactionObject.Postage,
                SaleMethod = TransactionObject.SaleMethod,
                SoldDate = TransactionObject.SaleDate,
                SoldBy = UserID,
                PayPalTransactionID = TransactionObject.PayPalTransactionID,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Title = TransactionObject.Title,
                Description = TransactionObject.Description,
                PersonID = personid
             };

                DB.dtSales.InsertOnSubmit(newtransaction);
                DB.SubmitChanges();
           
                int tid = MostRecentsalesID();
                decimal pvalue = Math.Abs(Convert.ToDecimal(TransactionObject.PayPalFees)) * (-1);
                decimal ppvalue = Math.Abs(Convert.ToDecimal(TransactionObject.Postage)) * (-1);

                dtTransactionLedger newsaleledger = new dtTransactionLedger
                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = Convert.ToDecimal(TransactionObject.Amount),
                    TransactionType = "Item Sale",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newspaypal = new dtTransactionLedger
                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = pvalue,
                    TransactionType = "PayPal Fees",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newepostage = new dtTransactionLedger
                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = ppvalue,
                    TransactionType = "Sale Postage",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                foreach (skStock s in TransactionObject.StockList)
                {
                    dtStockHistory newstatus = new dtStockHistory
                    {
                        StoockID = Convert.ToInt32(s.Stockid),
                        Status = "Stock Sold",
                        Value = 0,
                        UserID = UserID,
                        Created = DateTime.Now,
                        StatusID = 1
                    };
                    DB.dtStockHistories.InsertOnSubmit(newstatus);

                    dtStockDetail SD = DB.dtStockDetails.Single(x => x.StockID == s.Stockid);
                    SD.SaleValue = s.SaleValue;
                }

            List<long> StockIds = new List<long>();

            foreach (skStock s in TransactionObject.StockList)
            {
                StockIds.Add(s.Stockid);
            }

            var setsold = from ST in DB.dtStocks
                          where StockIds.Contains(ST.sID)
                          select ST;

            foreach (dtStock s in setsold)
            {
                s.Sold = true;
                s.SaleID = tid;
            }

                DB.dtTransactionLedgers.InsertOnSubmit(newspaypal);
                DB.dtTransactionLedgers.InsertOnSubmit(newepostage);
                DB.dtTransactionLedgers.InsertOnSubmit(newsaleledger);

                DB.SubmitChanges();        
        }

        public void AddNewSale(skSales TransactionObject,int UserID)
        {                        
                dtSale newtransaction = new dtSale
                {
                    SoldValue = TransactionObject.Amount,
                    PaypayFees = TransactionObject.PayPalFees,
                    PandP = TransactionObject.Postage,
                    SaleMethod = TransactionObject.SaleMethod,
                    SoldDate = TransactionObject.SaleDate,
                    SoldBy = UserID,
                    PayPalTransactionID = TransactionObject.PayPalTransactionID,
                    Updated = DateTime.Now,
                    Created = DateTime.Now,
                    Title = TransactionObject.Title,
                    Description = TransactionObject.Description,
                    PersonID = TransactionObject.PersonObj.pID
                    

                };

                DB.dtSales.InsertOnSubmit(newtransaction);
                DB.SubmitChanges();           
           
                int tid = MostRecentsalesID();
                decimal pvalue = Math.Abs(Convert.ToDecimal(TransactionObject.PayPalFees)) * (-1);
                decimal ppvalue = Math.Abs(Convert.ToDecimal(TransactionObject.Postage)) * (-1);

                dtTransactionLedger newsaleledger = new dtTransactionLedger
                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = Convert.ToDecimal(TransactionObject.Amount),
                    TransactionType = "Item Sale",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newspaypal = new dtTransactionLedger

                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = pvalue,
                    TransactionType = "PayPal Fees",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                dtTransactionLedger newepostage = new dtTransactionLedger
                {
                    SaleID = tid,
                    PurchaseID = null,
                    TotelAmount = ppvalue,
                    TransactionType = "Sale Postage",
                    TransactionDateTime = Convert.ToDateTime(TransactionObject.SaleDate)
                };

                foreach (skStock s in TransactionObject.StockList)
                {
                     dtStockHistory newstatus = new dtStockHistory
                     {
                        StoockID = Convert.ToInt32(s.Stockid),
                        Status = "Stock Sold",
                        Value = 0,
                        UserID = UserID,
                        Created = DateTime.Now,
                        StatusID = 1
                    };
                    DB.dtStockHistories.InsertOnSubmit(newstatus);

                dtStockDetail SD = DB.dtStockDetails.Single(x => x.StockID == s.Stockid);
                SD.SaleValue = s.SaleValue;
              }

            List<long> StockIds = new List<long>();

            foreach (skStock s in TransactionObject.StockList)
            {
                StockIds.Add(s.Stockid);
            }

            var setsold = from ST in DB.dtStocks
                          where StockIds.Contains(ST.sID)
                          select ST;

            foreach (dtStock s in setsold)
            {
                s.Sold = true;
                s.SaleID = tid;
            }

                DB.dtTransactionLedgers.InsertOnSubmit(newepostage);
                DB.dtTransactionLedgers.InsertOnSubmit(newspaypal);
                DB.dtTransactionLedgers.InsertOnSubmit(newsaleledger);

                DB.SubmitChanges();
             }

        public void AddNewRefund(skRefund RefundObject,int UserID)
        {
            dtRefund NewRefund = new dtRefund
            {
                Reason = RefundObject.Description,
                Amount = Convert.ToDecimal(RefundObject.Amount),
                Postage = RefundObject.SHippingCosts,
                Refunded = DateTime.Now,
                RefundedBy = UserID,
                StockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Created = DateTime.Now,
                Updated = DateTime.Now
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

            dtStockHistory NewStatus = new dtStockHistory
            {
                StoockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Status = "Return",
                Value = RefundObject.Amount,
                UserID = UserID,
                Created = DateTime.Now,
                StatusID = 0
            };
            DB.dtTransactionLedgers.InsertOnSubmit(NewRefundLedger);
            DB.dtStockHistories.InsertOnSubmit(NewStatus);
            DB.SubmitChanges();


            dtStock stock = DB.dtStocks.Single(s => s.sID == RefundObject.StockItem.Stockid);
            stock.Sold = false;
            DB.SubmitChanges();
        }
           public void AddNewRefund(skRefund RefundObject, bool ReturnStock,int UserID)
           {

            dtRefund NewRefund = new dtRefund
            {
                Reason = RefundObject.Description,
                Amount = Convert.ToDecimal(RefundObject.Amount),
                Postage = RefundObject.SHippingCosts,
                Refunded = DateTime.Now,
                RefundedBy = UserID,
                StockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Created = DateTime.Now,
                Updated = DateTime.Now,


            };

            dtStockHistory NewStatus = new dtStockHistory
            {
                StoockID = Convert.ToInt32(RefundObject.StockItem.Stockid),
                Status = "Return",
                Value = RefundObject.Amount,
                UserID = UserID,
                Created = DateTime.Now,
                StatusID = 0
            };


            if (ReturnStock == true)
            {

                dtStockDetail details = DB.dtStockDetails.Single(s => s.StockID == RefundObject.StockItem.Stockid);
                dtStock stock = DB.dtStocks.Single(s => s.sID == RefundObject.StockItem.Stockid);

                stock.Sold = false;
                stock.SaleID = null;
                details.SaleValue = null;
            }

            DB.dtRefunds.InsertOnSubmit(NewRefund);
            DB.dtStockHistories.InsertOnSubmit(NewStatus);

            DB.SubmitChanges();

        }


        #endregion

        #region UpdateMethods

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
            dtSale SaleToUpdate = DB.dtSales.Single(x => x.tID == SaleObject.ID);

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

        public void LinkTranToStock(ITransaction TransactionObject, skStock StockOBJ,int UserID)
        {

            dtStock StockObj = DB.dtStocks.Single(x => x.sID == StockOBJ.Stockid);
            dtStockDetail StockDetais = DB.dtStockDetails.Single(x => x.StockID == StockOBJ.Stockid);

            switch (TransactionObject.TransactionType)
            {
                case TransactionType.Sale:
                    StockDetais.SaleValue = StockOBJ.SaleValue;
                    dtSale SaleObj = DB.dtSales.Single(x => x.tID == TransactionObject.ID);
                    SaleObj.SoldValue += StockDetais.SaleValue;
                    StockObj.SaleID = TransactionObject.ID;
                    CreateNewStockStatus(StockOBJ.Stockid, "Linked To sale " + SaleObj.tID,UserID,Convert.ToDecimal(StockOBJ.SaleValue) );
                    break;

                case TransactionType.Purchase:
                    StockDetais.PurchaseValue = StockOBJ.purchasedvalue;
                    dtPurchese PurchaseObj = DB.dtPurcheses.Single(x => x.pID == TransactionObject.ID);
                    PurchaseObj.PurchesedValue += StockDetais.PurchaseValue;
                    StockObj.PurchaseID = TransactionObject.ID;
                    CreateNewStockStatus(StockOBJ.Stockid, "Linked To Purchase " + PurchaseObj.pID,UserID,Convert.ToDecimal(StockOBJ.purchasedvalue));
                    break;
            }

            DB.SubmitChanges();

        }

        public void RemoveStockFromPurchase(int StockID,int UserID)
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

                CreateNewStockStatus(StockID, "Removed from Purchase",UserID);

            }
        }

        public void RemoveStockFromSale(int StockID,int UserID)
        {
            dtStock StockObj = DB.dtStocks.Single(x => x.sID == StockID);
            dtStockDetail StockDetailObj = DB.dtStockDetails.Single(x => x.StockID == StockObj.sID);
            dtSale SaleOBJ;

            if (StockObj.SaleID != null)
            {
                SaleOBJ = DB.dtSales.Single(x => x.tID == StockObj.SaleID);

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

                CreateNewStockStatus(StockID, "Removed from sale",UserID);
            }
        }
        #endregion
    }
}
