using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Data.Interfaces;

namespace Stocky.Service.ServiceInterfaces
{

    [ServiceContract]

    [ServiceKnownType(typeof(skPurchase))]
    [ServiceKnownType(typeof(skSales))]
    [ServiceKnownType(typeof(skRefund))]
    [ServiceKnownType(typeof(skStock))]
    [ServiceKnownType(typeof(skPerson))]
    [ServiceKnownType(typeof(TransactionType))]
    [ServiceKnownType(typeof(SearchFilterObject))]
    public interface IAppDataService
    {            
        [OperationContract]
        StockContract GetStockObject(int StockID);
        [OperationContract]
        StockContract GetStockList(skStock StockOBj);
        [OperationContract]
        StockContract GetStockListByTransaction(ITransaction Transaction);
        [OperationContract]
        StockContract GetOrphanedStockList(TransactionType TransactionType);
        [OperationContract]
        StockContract GetStockHistory(long StockID);
        [OperationContract(Name = "AddNewStock")]
        void AddNewStock(skStock StockOBJ, int UserID);
        [OperationContract(Name = "AddNewStocklistWithPurchase")]
        void AddNewStock(List<skStock> StockList, skPurchase PurchaseObject, int UserID);
        [OperationContract(Name = "AddNewStockWithPurchase")]
        void AddNewStock(skStock StockOb, skPurchase PurchaseOb, int UserID);
        [OperationContract]
        void UpdateStock(skStock StockObj, int UserID);
        [OperationContract]
        TransactionContract GetTransactionList(SearchFilterObject SearchFilter);
        [OperationContract]
        TransactionContract GetPurchaseObject(int PurchaseID);
        [OperationContract]
        TransactionContract GetSaleObject(int SaleID);
        [OperationContract]
        TransactionContract GetPurchaseObjectByStockID(int StockID);
        [OperationContract]
        TransactionContract GetSalesObjectByStockID(int StockID);
        [OperationContract]
        TransactionContract GetRefundObject(int RefundID);
        [OperationContract]
        void AddNewPurchase(skPurchase PurchaseObject, int UserID);
        [OperationContract(Name = "AddNewSaleWithPerson")]
        void AddNewSale(skAddresses AddressObject,skPerson PersonObject, skSales TransactionObject, int UserID);
        [OperationContract(Name = "AddNewSale")]
        void AddNewSale(skSales TransactionObject, int UserID);
        [OperationContract(Name = "AddNewRefund")]
        void AddNewRefund(skRefund RefundObject, int UserID);
        [OperationContract(Name = "AddNewPurchaseWithReturn")]
        void AddNewRefund(skRefund RefundObject, bool ReturnStock, int UserID);
        [OperationContract]
        void UpdatePurchase(skPurchase PurchaseObject);
        [OperationContract]
        void UpdateSale(skSales SaleObject);
        [OperationContract]
        void LinkTranToStock(ITransaction TransactionObject,skStock StockOBJ,int UserID);
        [OperationContract]
        void RemoveStockFromPurchase(int StockID, int UserID);
        [OperationContract]
        void RemoveStockFromSale(int StockID, int UserID);
        [OperationContract]
        RecipientContract GetAddressesList();
        [OperationContract]
        RecipientContract GetAddressesListByPerson(int PersonID);
        [OperationContract]
        RecipientContract GetAddressObject(int AddressID);
        [OperationContract(Name = "AddNewAddressObjectLink")]
        void AddNewAddress(skAddresses AddressObject, int ObjectID, Type ObjectType);
        [OperationContract]
        void AddNewAddress(skAddresses AddressObject);
        [OperationContract]
        void AddNewCategory(skCategory CategoryObject);
        [OperationContract]
        void UpdateCategory(skCategory CategoryObject);
        [OperationContract(Name = "GetCategoryListWithFilter")]
        StockContract GetCategoryList(skCategory CategoryFilterObject);
        [OperationContract(Name = "GetCategoryList")]
        StockContract GetCategoryList();
        [OperationContract]
        RecipientContract GetPersonList();
        [OperationContract]
        RecipientContract GetPersonObject(int PersonID);
        [OperationContract(Name = "AddNewPerson")]
        void AddNewPerson(skPerson PersonObject);
        [OperationContract(Name = "AddNewPersonWithAddressID")]
        void AddNewPerson(skPerson PersonObject,int AddressID);
        [OperationContract(Name = "AddNewPersonWithAddressOBject")]
        void AddNewPerson(skPerson PersonObject, skAddresses AddressObject);
        [OperationContract]
        void UpdatePerson(skPerson PersonObject);
        [OperationContract]
        void RemovePersonAddressLink(int PersonID, int AddressID);
        [OperationContract]
        void LinkAddressToPerson(int PersonId, int AddressID);

        [OperationContract(Name = "GetUserObjectUserID")]
        UserContract GetUserObject(int UserID);
        [OperationContract(Name = "GetUserObjectUserName")]
        UserContract GetUserObject(string UserName);
        [OperationContract]
        void AddNewUser(skUser UserObject);
        [OperationContract]
        void UpdateUser(skUser UserObject,bool ResetPassword);
        [OperationContract]
        ValueContract GetValueBandObject(int ValueBandID);
        [OperationContract(Name ="GetValueBandListWithFilter")]
        ValueContract GetValueBandList(skValueBands ValueBandList);
        [OperationContract(Name = "GetValueBandList")]
        ValueContract GetValueBandList();
        [OperationContract]
        VendorContract GetVendorObject(int vendorID);
        [OperationContract(Name = "GetVendorList")]
        VendorContract GetVendorList();
        [OperationContract(Name = "GetVendorListWithFilter")]
        VendorContract GetVendorList(skvendors VendorObject);
        [OperationContract(Name = "AddNewVendor")]
        void AddNewVendor(skvendors VendorObject);
        [OperationContract(Name = "AddNewVendorWithAddress")]
        void AddNewVendor(skvendors VendorObject, skAddresses AddressObject);
        [OperationContract]
        void UpdateVendor(skvendors VendorObject);
        [OperationContract]     
        bool IsRecordDirty(object ValidationObject);
        [OperationContract]
        void AddToExistingPurchase(skStock StockOb, int PurchaseID, int UserID);
    }
}