using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Stocky.Service.ServiceInterfaces;
using Stocky.Service.Contracts;
using Stocky.DataAcesse.Reposetories;
using Stocky.Data;
using Stocky.Data.Interfaces;
using System;

namespace Stocky.Service
{
    [KnownType(typeof(skPurchase))]
    [KnownType(typeof(skSales))]
    [KnownType(typeof(skRefund))]
    [KnownType(typeof(skCategory))]
    [KnownType(typeof(TransactionType))]
    public class StockyDataService : IAppDataService
    {
        StockRepo StockRepo;
        TransactionRepo TransactionRepo;
        AddressRepo AddressRepo;
        CategoryRepo CatergoryRepo;
        PersonRepo PersonRepo;
        UserRepo UserRepo;
        ValueBandRepo ValueRepo;
        VendorRepo VendorRepo;
        ValidationRepo ValidationRepo;

        public StockyDataService()
        {
            if (Helpers.DBIdentification)
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set("LocalHost", "Stocky");
            }
            else
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set(@"7TECH-SVR1", "Stocky_LIVE", "applogin", "728652Hotdog");
            }


            StockRepo = new StockRepo();
            TransactionRepo = new TransactionRepo();
            AddressRepo = new AddressRepo();
            CatergoryRepo = new CategoryRepo();
            PersonRepo = new PersonRepo();
            UserRepo = new UserRepo();
            ValueRepo = new ValueBandRepo();
            VendorRepo = new VendorRepo();
            ValidationRepo = new ValidationRepo();
        }

        public void AddNewPurchase(skPurchase PurchaseObject, int UserID)
        {
            TransactionRepo.AddNewPurchase(PurchaseObject, UserID);
        }

        public void AddNewRefund(skRefund RefundObject, int UserID)
        {
            TransactionRepo.AddNewRefund(RefundObject, UserID);
        }

        public void AddNewRefund(skRefund RefundObject, bool ReturnStock, int UserID)
        {
            TransactionRepo.AddNewRefund(RefundObject, ReturnStock, UserID);
        }

        public void AddNewSale(skSales TransactionObject, int UserID )
        {
            TransactionRepo.AddNewSale(TransactionObject, UserID);
        }

        public void AddNewSale(skAddresses AddressObject, skPerson PersonObject, skSales TransactionObject, int UserID)
        {
            TransactionRepo.AddNewSale(TransactionObject, PersonObject, AddressObject, UserID);
        }

        public void AddNewStock(skStock StockOBJ, int UserID)
        {
            StockRepo.AddNewStockItem(StockOBJ, UserID);
        }

        public void AddNewStock(skStock StockOb, skPurchase PurchaseOb, int UserID)
        {
            StockRepo.AddNewStockItem(StockOb, PurchaseOb, UserID);
        }

        public void AddNewStock(List<skStock> StockList, skPurchase PurchaseObject, int UserID)
        {
            StockRepo.AddNewStockItem(StockList, PurchaseObject, UserID);
        }

        public StockContract GetOrphanedStockList(TransactionType TransactionType)
        {
            StockContract retunrobject = new StockContract();

            retunrobject.StockList = StockRepo.GetOrphanedStockList(TransactionType).ToList();

            return retunrobject;
        }

        public TransactionContract GetPurchaseObject(int PurchaseID)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.PurchaseObject = TransactionRepo.GetPurchaseDetails(PurchaseID);

            return retunrobject;
        }

        public TransactionContract GetPurchaseObjectByStockID(int StockID)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.PurchaseObject = TransactionRepo.GetPurchaseDetailsByStockID(StockID);

            return retunrobject;
        }

        public TransactionContract GetRefundObject(int RefundID)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.RefundObject = TransactionRepo.GetRefundDetails(RefundID);

            return retunrobject;
        }

        public TransactionContract GetSaleObject(int SaleID)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.SalesObject = TransactionRepo.GetSalesDetails(SaleID);

            return retunrobject;
        }

        public TransactionContract GetSalesObjectByStockID(int StockID)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.SalesObject = TransactionRepo.GetSalesDetailsByStockID(StockID);

            return retunrobject;
        }

        public StockContract GetStockHistory(long StockID)
        {
            StockContract retunrobject = new StockContract();

            retunrobject.StockHistory = StockRepo.GetStockHistory(StockID).ToList();

            return retunrobject;
        }   

        public StockContract GetStockList(skStock StockOBj)
        {
            StockContract retunobject = new StockContract();

            retunobject.StockList = StockRepo.GetStockList(StockOBj).ToList();

            return retunobject;
        }

        public StockContract GetStockListByTransaction(ITransaction Transaction)
        {
            StockContract retunobject = new StockContract();

            switch(Transaction.TransactionType)
            {
                case TransactionType.Purchase:
                    retunobject.StockList = StockRepo.GetStockListByPrucahseID(Transaction.ID).ToList();
                    break;
                case TransactionType.Sale:
                    retunobject.StockList = StockRepo.GetStockListBySaleID(Transaction.ID).ToList();
                    break;                    
            }
            return retunobject;
        }

        public StockContract GetStockObject(int StockID)
        {
            StockContract retunobject = new StockContract();

            retunobject.StockObj = StockRepo.GetStockObject(StockID);

            return retunobject;
        }

        public TransactionContract GetTransactionList(SearchFilterObject SearchFilter)
        {
            TransactionContract retunrobject = new TransactionContract();

            retunrobject.TransactionList = TransactionRepo.GetTransactionList(SearchFilter);

            return retunrobject;
        }

        public void LinkTranToStock(ITransaction TransactionObject, skStock StockOBJ, int UserID)
        {
            TransactionRepo.LinkTranToStock(TransactionObject, StockOBJ, UserID);
        }

        public void RemoveStockFromPurchase(int StockID, int UserID)
        {
            TransactionRepo.RemoveStockFromPurchase(StockID, UserID);
        }

        public void RemoveStockFromSale(int StockID, int UserID)
        {
            TransactionRepo.RemoveStockFromSale(StockID, UserID);
        }

        public void UpdatePurchase(skPurchase PurchaseObject)
        {
            TransactionRepo.UpdatePurchase(PurchaseObject);
        }

        public void UpdateSale(skSales SaleObject)
        {
            TransactionRepo.UpdateSale(SaleObject);
        }

        public void UpdateStock(skStock StockObj, int UserID)
        {
            StockRepo.UpdateStock(StockObj, UserID);
        }

        public RecipientContract GetAddressesList()
        {
            RecipientContract RetunObject = new RecipientContract();

            RetunObject.AddressList = AddressRepo.GetAddressList().ToList();

            return RetunObject;
        }

        public RecipientContract GetAddressObject(int AddressID)
        {
            RecipientContract RetunObject = new RecipientContract();

            RetunObject.AddressObject = AddressRepo.GetAddressObject(AddressID);

            return RetunObject;
        }

        public void AddNewAddress(skAddresses AddressObject)
        {
            AddressRepo.AddNewAddress(AddressObject);
        }

        public void AddNewCategory(skCategory CategoryObject)
        {
            CatergoryRepo.AddNewCategory(CategoryObject);
        }

        public void UpdateCategory(skCategory CategoryObject)
        {
            CatergoryRepo.UpdateCategory(CategoryObject);
        }

        public StockContract GetCategoryList(skCategory CategoryFilterObject)
        {
            StockContract retunobject = new StockContract();

            retunobject.CategoryList = CatergoryRepo.GetCategoryList(CategoryFilterObject).ToList();

            return retunobject;
        }

        public RecipientContract GetPersonList()
        {
            RecipientContract returnobject = new RecipientContract();

            returnobject.PersonList = PersonRepo.GetPersonList().ToList();

            return returnobject;
        }

        public RecipientContract GetPersonObject(int PersonID)
        {
            RecipientContract returnobject = new RecipientContract();

            returnobject.PersonObject = PersonRepo.GetPersonObject(PersonID);

            return returnobject;
        }

        public void AddNewPerson(skPerson PersonObject)
        {
            PersonRepo.AddNewPerson(PersonObject);
        }

        public UserContract GetUserObject(int UserID)
        {
            UserContract returncontract = new UserContract();

            returncontract.UserObject = UserRepo.GetUserObject(UserID);

            return returncontract;
        }

        public UserContract GetUserObject(string UserName)
        {
            UserContract returncontract = new UserContract();

            returncontract.UserObject = UserRepo.GetUserObject(UserName);

            return returncontract;
        }

        public void AddNewUser(skUser UserObject)
        {
            string HashedPassword = Authentication.UserAuthentication.HashPassword(UserObject.Password);

            UserObject.Password = HashedPassword;

            UserRepo.AddNewUser(UserObject);
        }

        public void UpdateUser(skUser UserObject,bool ResetPassword)
        {
            string HashedPassword = Authentication.UserAuthentication.HashPassword(UserObject.Password);

            UserObject.Password = HashedPassword;

            UserRepo.UpdateUser(UserObject,ResetPassword);
        }

        public ValueContract GetValueBandObject(int ValueBandID)
        {
            ValueContract returncontract = new ValueContract();

            returncontract.ValueBandObject = ValueRepo.GetValueBand(ValueBandID);

            return returncontract;
        }

        public ValueContract GetValueBandList(skValueBands ValueBandList)
        {
            ValueContract returncontract = new ValueContract();

            returncontract.ValueBandList = ValueRepo.GetValueBandList(ValueBandList).ToList();

            return returncontract;
        }

        public VendorContract GetVendorObject(int vendorID)
        {
            VendorContract returncontract = new VendorContract();

            returncontract.VendorObject = VendorRepo.GetVendorDetails(vendorID);

            return returncontract;
        }

        public VendorContract GetVendorList(skvendors VendorObject)
        {
            VendorContract returncontract = new VendorContract();

            returncontract.VendorList = VendorRepo.GetVendorList(VendorObject).ToList(); ;

            return returncontract;
        }

        public void AddNewVendor(skvendors VendorObject)
        {
            VendorRepo.AddNewVendor(VendorObject);
        }

        public void AddNewVendor(skvendors VendorObject, skAddresses AddressObject)
        {
            VendorRepo.AddNewVendor(VendorObject,AddressObject);
        }

        public void UpdateVendor(skvendors VendorObject)
        {
            VendorRepo.UpdateVendor(VendorObject);
        }
        
        public bool IsRecordDirty(object ValidationObject)
        {
            return ValidationRepo.IsRecordDirty(ValidationObject);
        }

        public VendorContract GetVendorList()
        {
            VendorContract returncontract = new VendorContract();

            returncontract.VendorList = VendorRepo.GetVendorList().ToList();

            return returncontract;
        }

        public StockContract GetCategoryList()
        {
            StockContract retunobject = new StockContract();

            retunobject.CategoryList = CatergoryRepo.GetCategoryList().ToList();

            return retunobject;
        }

        public ValueContract GetValueBandList()
        {
            ValueContract returncontract = new ValueContract();

            returncontract.ValueBandList = ValueRepo.GetValueBandList().ToList();

            return returncontract;
        }

        public void AddNewPerson(skPerson PersonObject, skAddresses AddressObject)
        {
            PersonRepo.AddNewPerson(PersonObject, AddressObject);
        }

        public void AddNewPerson(skPerson PersonObject, int AddressID)
        {
            PersonRepo.AddNewPerson(PersonObject, AddressID);
        }

        public void UpdatePerson(skPerson PersonObject)
        {
            PersonRepo.UpdatePerson(PersonObject);
        }

        public void RemovePersonAddressLink(int PersonID, int AddressID)
        {
            AddressRepo.RemoveLinktoPerson(AddressID, PersonID);
        }

        public void LinkAddressToPerson(int PersonId, int AddressID)
        {
            AddressRepo.LinkAddressToPerson(AddressID, PersonId);
        }

        public RecipientContract GetAddressesListByPerson(int PersonID)
        {
            RecipientContract returncontract = new RecipientContract();

            returncontract.AddressList = AddressRepo.GetPersonAddressList(PersonID).ToList();

            return returncontract;

        }

        public void AddNewAddress(skAddresses AddressObject, int ObjectID, Type ObjectType)
        {
            AddressRepo.AddNewAddress(AddressObject, ObjectID, ObjectType);
        }

        public void AddToExistingPurchase(skStock StockOb, int PurchaseID, int UserID)
        {
            StockRepo.AddToExistingPurchase(StockOb, PurchaseID, UserID);
        }
    }
}
