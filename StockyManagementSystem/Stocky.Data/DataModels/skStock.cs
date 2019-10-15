using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MVVM_Framework;
using System.Reflection;
using Stocky.Attributes;
using Stocky.Utility;
using Stocky.Data.Interfaces;
using Stocky.Exceptions;


namespace Stocky.Data
{
    public class skStock : DataModelBase, IDataErrorInfo, INotification,IDataModel
    {
        #region Fields
        #endregion

        #region Properties
        public long Stockid { get; set; }
        [RequiredData]
        public string Name { get; set; }
        [RequiredData]
        public string Description { get; set; }
        public DateTime? purchaseddate { get; set; }
        [RequiredData (AllowZeros =true)]
        public decimal? purchasedvalue { get; set; }
        [RequiredData]
        public decimal? SaleValue { get; set; }
        public bool Sold { get; set; }
        [RequiredData]
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string SearchString { get { return Name + Description + Stockid + CategoryObject.Name; } }
        public List<skStock> StockList { get; set; }
        public skStockHistory StockHistory { get; set; }
        public skPurchase PurchaseObject { get; set; }
        public skSales SaleObject { get; set; }
        public Notification NewNotification { get; set; }
        [RequiredData]
        public skCategory CategoryObject { get; set; }
        [RequiredData]
        public skValueBands ValueBandObject { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> StockCreated;

        protected virtual void OnStockCreated()
        {
            CreateNotification(EventActionType.Created);
            StockCreated?.Invoke(this, new NewNotificationEventArgs(this));
            
        }

        public event EventHandler<NewNotificationEventArgs> StockUpdated;

        protected virtual void OnStockUpdated()
        {
            CreateNotification(EventActionType.Updated);
            StockUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> StockDeleted;

        protected virtual void OnStockDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            StockDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }


        #endregion

        #region Constructors
        public skStock()
        {
            Initialise();
        }

        public skStock(skUser CurrentUserObj) 
            :base(CurrentUserObj)
        {
            Initialise();
        }

        #endregion

        #region IDataError
        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string MSG = null;
                PropertyInfo[] prop = this.GetType().GetProperties();

                foreach (PropertyInfo P in prop)
                {
                    if (P.Name == columnName)
                    {
                        bool isempty = IsFieldEmpty(P.GetValue(this, null));

                        if (isempty == true)
                            MSG = "Field is Empty!";
                    }
                }

                return MSG;
            }
        }
        #endregion

        #region Methods

        private void Initialise()
        {
            StockHistory = new skStockHistory();
            StockList = new List<skStock>();
             NewNotification = new Notification();
            this.ValiationObject = this;

            StockCreated += NewNotification.OnNotificationReceived;
            StockUpdated += NewNotification.OnNotificationReceived;
        }

        public void Add(int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();

                    DataClient.AddNewStock(this, UserID);

                    OnStockCreated();
                }
                else
                {
                    throw new InvalidObjectException(base.PropertyList);
                }
            }
            finally
            {
                DataClient.Close();

            }
        }

        public void AddWithPurchase(skPurchase Purchase,int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (Validate())
                {
                    if (Purchase.Validate())
                    {
                        DataClient.Open();

                        DataClient.AddNewStock(this, Purchase, UserID);

                        OnStockCreated();
                    }
                    else
                    {
                        throw new InvalidObjectException(Purchase.PropertyList);
                    }
                }
                else
                {
                    throw new InvalidObjectException(base.PropertyList);
                }
            }
            finally
            {
                DataClient.Close();
            }
       }
               
        public void AddMultiWithPurchase(skPurchase Purchase, int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                foreach (skStock s in StockList)
                {
                    if (!s.IsObjectValid)
                        throw new InvalidObjectException(s.PropertyList);
                }
                if (Purchase.IsObjectValid)
                {
                    DataClient.Open();

                    DataClient.AddNewStock(StockList, Purchase, UserID);                    

                    OnStockCreated();
                }
                else
                {
                    throw new InvalidObjectException(Purchase.PropertyList);
                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void AddToExistingPurchase(int PurchaseID, int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
               
                if (IsObjectValid)
                {
                    DataClient.Open();

                    DataClient.AddToExistingPurchase(this, PurchaseID, UserID);

                    OnStockCreated();
                }
                else
                {
                    throw new InvalidObjectException(PropertyList);
                }
            }
            finally
            {
                DataClient.Close();
            }
        }
        public void Update(int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();

                    DataClient.UpdateStock(this, UserID);

                    OnStockUpdated();
                }
                else
                {
                    throw new InvalidObjectException(PropertyList);
                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public bool Delete()
        {
            if (IsObjectValid)
            {
                //DATA.DeleteStock(this);
                OnStockDeleted();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            NewNotification.RaisedBy = CurrentUser.UserID;
            switch (eventActionType)
            {
                case EventActionType.Created:

                    if (PurchaseObject != null)
                    {
                        NewNotification.Description = PurchaseObject.Title + ": Has been created with " + StockList.Count + " Stock Items";
                        NewNotification.Name = "New Purchase created";

                        StringBuilder EmailItems = new StringBuilder();
                        EmailItems.AppendLine("New Purchase Created:");
                        EmailItems.AppendLine();
                        EmailItems.AppendLine("Purchase Name: " + PurchaseObject.Title);
                        EmailItems.AppendLine("Purchase Description: " + PurchaseObject.Description);
                        EmailItems.AppendLine("Stock Items attached to Purchase:");

                        foreach (var s in StockList)
                        {
                            EmailItems.AppendLine(s.Name);
                        }

                        NewNotification.EmailBody = EmailItems.ToString();
                    }
                    else
                    {
                        NewNotification.Description = Name + " Has been created.";
                        NewNotification.Name = "New stock item created";
                        NewNotification.EmailBody = Name + Environment.NewLine + Description;
                    }

                    break;
                case EventActionType.Updated:

                    NewNotification.Description = "Stock has been updated.";
                    NewNotification.Name = "Stock item updated";
                    NewNotification.EmailBody = Name + Environment.NewLine + Description;

                    break;
                case EventActionType.Deleted:

                    NewNotification.Description = "Stock has been deleted.";
                    NewNotification.Name = "Stock item deleted";
                    NewNotification.EmailBody = Name + Environment.NewLine + Description;

                    break;
            }         
        }    

        public void OpenNotification()
        {
            if (PurchaseObject != null)
            {
                skTransaction tranobj = new skTransaction();
                
            }
            else
            {
      //          UI.Enviroment.LoadNewTab("StockManagerView");
            }
        }

        public void PopulatePurchaseObj()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                PurchaseObject = DataClient.GetPurchaseObjectByStockID(Convert.ToInt32(Stockid)).PurchaseObject;

            }
            finally
            {
                DataClient.Close();
            }         
        }
        

        public void PopulateSaleObj()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                SaleObject = DataClient.GetSalesObjectByStockID(Convert.ToInt32(Stockid)).SalesObject;

            }
            finally
            {
                DataClient.Close();
            }
        }
        
        public void LinktoTransaction(ITransaction TransactionObject,int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                DataClient.LinkTranToStock(TransactionObject, this, UserID);
            }
            finally
            {
                DataClient.Close();
            }
        } 

       public static List<skStock> GetStockList(skStock SearchFilterObj)
       {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skStock> ReturnList = DataClient.GetStockList(SearchFilterObj).StockList;

                if (ReturnList.Count != 0)
                {
                    return ReturnList;
                }
                else { throw new Exception("Result has no records!"); }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public static List<skStock> GetStockList()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skStock> ReturnList = DataClient.GetStockList(new skStock()).StockList;

                if (ReturnList.Count != 0)
                {
                    return ReturnList;
                }
                else { throw new Exception("Result has no records!"); }

            }
            finally
            {
                DataClient.Close();
            }            
        }

        public List<skStock> GetOrphanedStockList(TransactionType TransactionType)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skStock> ReturnList = DataClient.GetOrphanedStockList(TransactionType).StockList;

                if (ReturnList.Count != 0)
                {
                    return ReturnList;
                }
                else { throw new Exception("Result has no records!"); }
            }
            finally
            {
                DataClient.Close();
            }
        }

       public override bool Equals(object obj)
       {
           if (obj == null || !(obj is skStock))
               return false;
            return ((skStock)obj).Stockid == 0 ? ((skStock)obj) == this : ((skStock)obj).Stockid == this.Stockid;
       }

       public override int GetHashCode()
       {

            return this.Stockid == 0 ? base.GetHashCode() : this.Stockid.GetHashCode();
       }

        public override string ToString()
        {
            return Name + " " + Description;
        }

        public void RemoveFromPurchase(int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                DataClient.RemoveStockFromPurchase(Convert.ToInt32(this.Stockid), UserID);
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void RemoveFromSale(int UserID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                DataClient.RemoveStockFromSale(Convert.ToInt32(this.Stockid), UserID);
            }
            finally
            {
                DataClient.Close();
            }
        }

        public static skStock GetStockObject(int stockID)
        {
            Stocky.Proxies.AppDataClient StockProxie = new Proxies.AppDataClient();
            try
            {
                StockProxie.Open();

                skStock PurchaseOBJ = StockProxie.GetStockObject(stockID).StockObj;

                if (PurchaseOBJ != null)
                {
                    return PurchaseOBJ;
                }
                else
                {
                    throw new Exception("No stock found !");
                }
            }
            finally
            {
                StockProxie.Close();
            }            
        }

        public void UpdateCurrentObject()
        {
            if(Stockid >= 0)
            {
                if(IsDirtyObject())
                {
                    var NewObject = GetStockObject(Convert.ToInt32(this.Stockid));

                    this.CategoryObject = NewObject.CategoryObject;
                    this.Created = NewObject.Created;
                    this.Description = NewObject.Description;
                    this.Name = NewObject.Name;
                    this.purchaseddate = NewObject.purchaseddate;
                    this.purchasedvalue = NewObject.purchasedvalue;
                    this.PurchaseObject = NewObject.PurchaseObject;
                    this.SaleObject = NewObject.SaleObject;
                    this.SaleValue = NewObject.SaleValue;
                    this.Sold = NewObject.Sold;
                    this.StockHistory = NewObject.StockHistory;
                    this.Updated = NewObject.Updated;
                    this.ValueBandObject = NewObject.ValueBandObject;

                }
            }
        }
        #endregion
    }
}
