using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.ServiceInterfaces;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Data.Interfaces;


namespace Stocky.Proxies
{
    public class AppDataClient : ClientBase<IAppDataService>,IAppDataService
    {
        public AppDataClient()
            :base(EndPointDefinitions.DataEndPoint)
        {

        }
        public void AddNewStock(List<skStock> StockList, skPurchase PurchaseObject, int UserID)
        {
            base.Channel.AddNewStock(StockList, PurchaseObject, UserID);         
        }

        public void AddNewPurchase(skPurchase PurchaseObject, int UserID)
        {
            base.Channel.AddNewPurchase(PurchaseObject, UserID);
        }

        public void AddNewRefund(skRefund RefundObject, int UserID)
        {
            base.Channel.AddNewRefund(RefundObject, UserID);
        }

        public void AddNewRefund(skRefund RefundObject, bool ReturnStock, int UserID)
        {
            base.Channel.AddNewRefund(RefundObject, ReturnStock, UserID);
        }

        public void AddNewSale(skSales TransactionObject, int UserID)
        {
            base.Channel.AddNewSale(TransactionObject, UserID);
        }

        public void AddNewSale( skAddresses AddressObject, skPerson PersonObject, skSales TransactionObject, int UserID)
        {
            base.Channel.AddNewSale(AddressObject,PersonObject, TransactionObject, UserID);
        }

        public void AddNewStock(skStock StockOBJ, int UserID)
        {
            base.Channel.AddNewStock(StockOBJ, UserID);
        }

        public void AddNewStock(skStock StockOb, skPurchase PurchaseOb, int UserID)
        {
            base.Channel.AddNewStock(StockOb, PurchaseOb, UserID);
        }

        public StockContract GetOrphanedStockList(TransactionType TransactionType)
        {
            return base.Channel.GetOrphanedStockList(TransactionType);
        }

        public TransactionContract GetPurchaseObject(int PurchaseID)
        {
            return base.Channel.GetPurchaseObject(PurchaseID);
        }

        public TransactionContract GetPurchaseObjectByStockID(int StockID)
        {
            return base.Channel.GetPurchaseObjectByStockID(StockID);
        }

        public TransactionContract GetRefundObject(int RefundID)
        {
            return base.Channel.GetRefundObject(RefundID);
        }

        public TransactionContract GetSaleObject(int SaleID)
        {
            return base.Channel.GetSaleObject(SaleID);
        }

        public TransactionContract GetSalesObjectByStockID(int StockID)
        {
            return base.Channel.GetSalesObjectByStockID(StockID);
        }

        public StockContract GetStockHistory(long StockID)
        {
            return base.Channel.GetStockHistory(StockID);
        }

        public StockContract GetStockList(skStock StockOBj)
        {
            return base.Channel.GetStockList(StockOBj);
        }

        public StockContract GetStockListByTransaction(ITransaction Transaction)
        {
            return base.Channel.GetStockListByTransaction(Transaction);
        }

        public StockContract GetStockObject(int StockID)
        {
            return base.Channel.GetStockObject(StockID);
        }

        public TransactionContract GetTransactionList(SearchFilterObject SearchFilter)
        {
            return base.Channel.GetTransactionList(SearchFilter);
        }

        public void LinkTranToStock(ITransaction TransactionObject, skStock StockOBJ, int UserID)
        {
            base.Channel.LinkTranToStock(TransactionObject, StockOBJ, UserID);
        }

        public void RemoveStockFromPurchase(int StockID, int UserID)
        {
            base.Channel.RemoveStockFromPurchase(StockID, UserID);
        }

        public void RemoveStockFromSale(int StockID, int UserID)
        {
            base.Channel.RemoveStockFromSale(StockID, UserID);
        }

        public void UpdatePurchase(skPurchase PurchaseObject)
        {
            base.Channel.UpdatePurchase(PurchaseObject);
        }

        public void UpdateSale(skSales SaleObject)
        {
            base.Channel.UpdateSale(SaleObject);
        }

        public void UpdateStock(skStock StockObj, int UserID)
        {
            base.Channel.UpdateStock(StockObj, UserID);
        }

        public RecipientContract GetAddressesList()
        {
          return base.Channel.GetAddressesList();
        }

        public RecipientContract GetAddressObject(int AddressID)
        {
            return base.Channel.GetAddressObject(AddressID);
        }

        public void AddNewAddress(skAddresses AddressObject)
        {
            base.Channel.AddNewAddress(AddressObject);
        }

        public void AddNewCategory(skCategory CategoryObject)
        {
            base.Channel.AddNewCategory(CategoryObject);
        }

        public void UpdateCategory(skCategory CategoryObject)
        {
            base.Channel.UpdateCategory(CategoryObject);
        }

        public StockContract GetCategoryList(skCategory CategoryFilterObject)
        {
           return base.Channel.GetCategoryList(CategoryFilterObject);
        }

        public RecipientContract GetPersonList()
        {
            return base.Channel.GetPersonList();
        }

        public RecipientContract GetPersonObject(int PersonID)
        {
            return base.Channel.GetPersonObject(PersonID);
        }

        public void AddNewPerson(skPerson PersonObject)
        {
            base.Channel.AddNewPerson(PersonObject);
        }

        public UserContract GetUserObject(int UserID)
        {
           return base.Channel.GetUserObject(UserID);
        }

        public UserContract GetUserObject(string UserName)
        {
            return base.Channel.GetUserObject(UserName);
        }

        public void AddNewUser(skUser UserObject)
        {
            base.Channel.AddNewUser(UserObject);
        }

        public void UpdateUser(skUser UserObject, bool ResetPassword)
        {
            base.Channel.UpdateUser(UserObject, ResetPassword);
        }

        public ValueContract GetValueBandObject(int ValueBandID)
        {
            return base.Channel.GetValueBandObject(ValueBandID);
        }

        public ValueContract GetValueBandList(skValueBands ValueBandList)
        {
            return base.Channel.GetValueBandList(ValueBandList);
        }

        public VendorContract GetVendorObject(int vendorID)
        {
            return base.Channel.GetVendorObject(vendorID);
        }

        public VendorContract GetVendorList(skvendors VendorObject)
        {
            return base.Channel.GetVendorList(VendorObject);
        }

        public void AddNewVendor(skvendors VendorObject)
        {
            base.Channel.AddNewVendor(VendorObject);
        }

        public void AddNewVendor(skvendors VendorObject, skAddresses AddressObject)
        {
            base.Channel.AddNewVendor(VendorObject, AddressObject);
        }

        public void UpdateVendor(skvendors VendorObject)
        {
            base.Channel.UpdateVendor(VendorObject);
        }

        public bool IsRecordDirty(object ValidationObject)
        {
           return base.Channel.IsRecordDirty(ValidationObject);
        }

        public VendorContract GetVendorList()
        {
            return base.Channel.GetVendorList();
        }

        public StockContract GetCategoryList()
        {
            return base.Channel.GetCategoryList();
        }

        public ValueContract GetValueBandList()
        {
            return base.Channel.GetValueBandList();
        }

        public void AddNewPerson(skPerson PersonObject, skAddresses AddressObject)
        {
            base.Channel.AddNewPerson(PersonObject, AddressObject);
        }

        public void AddNewPerson(skPerson PersonObject, int AddressID)
        {
            base.Channel.AddNewPerson(PersonObject, AddressID);
        }

        public void UpdatePerson(skPerson PersonObject)
        {
            base.Channel.UpdatePerson(PersonObject);
        }

        public void RemovePersonAddressLink(int PersonID, int AddressID)
        {
            base.Channel.RemovePersonAddressLink(PersonID, AddressID);
        }

        public void LinkAddressToPerson(int PersonId, int AddressID)
        {
            base.Channel.LinkAddressToPerson(PersonId, AddressID);
        }

        public RecipientContract GetAddressesListByPerson(int PersonID)
        {
            return base.Channel.GetAddressesListByPerson(PersonID);
        }

        public void AddNewAddress(skAddresses AddressObject, int ObjectID, Type ObjectType)
        {
            base.Channel.AddNewAddress(AddressObject, ObjectID, ObjectType);
        }

        public void AddToExistingPurchase(skStock StockOb, int PurchaseID, int UserID)
        {
            base.Channel.AddToExistingPurchase(StockOb, PurchaseID, UserID);
        }
    }
}
