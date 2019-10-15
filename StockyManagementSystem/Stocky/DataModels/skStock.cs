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
using Stocky.ExepionLogger;
using Stocky.DataAccesse;
using Stocky.Attributes;
using Stocky.Utility;
using Stocky.DataModels.Transaction;


namespace Stocky.Data
{
    public class skStock : Validation, IDataErrorInfo,INotification
    {
        #region Fields
        private DataFunctions DATA;
        #endregion

        #region Properties
        public long Stockid { get; set; }
        [RequiredData]
        public string Name { get; set; }
        [RequiredData]
        public string Description { get; set; }
        
        public DateTime? purchaseddate { get; set; }
       
        public string category { get; set; }
        [RequiredData]
        public int categoryID { get; set; }
        [RequiredData]
        public decimal? purchasedvalue { get; set; }
        [RequiredData]
        public decimal? SaleValue { get; set; }

        public int? Sold { get; set; }
        [RequiredData]
        public int ValueBandID { get; set; }
       
        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public string SearchString { get { return Name + Description; } }

        public List<skStock> StockList { get; set; }

        public skStockHistory StockHistory {get;set;}

        public skPurchase PurchaseObject { get; set; }

        public skSales SaleObject { get; set; }

        public Notification NewNotification { get; set; }
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
            StockHistory = new skStockHistory();
            StockList = new List<skStock>();

            DATA = new DataFunctions();
            NewNotification = new Notification();
            this.ValiationObject = this;

            StockCreated += NewNotification.OnNotificationReceived;
            StockUpdated += NewNotification.OnNotificationReceived;
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
        public bool Add()
        {
            bool ReturnValue = false;

            if (IsObjectValid)
            {
                try
                {
                    if (Name != "" & Name != null)
                    {
                        DATA.AddNewStockItem(this);
                        ReturnValue = true;
                    }
                    else
                    {
                        ReturnValue = false;
                    }
                    OnStockCreated();
                    return ReturnValue;
                }
                catch (Exception E)
                {
                    Logger.LogException(E);
                    Logger.Show(E);

                    ReturnValue = false;                
                }
                ReturnValue = false;
            }
            return ReturnValue;
        }

        public bool AddWithPurchase(skPurchase Purchase)
        {
            PurchaseObject = Purchase;
            bool ReturnValue = false;
            try
            {
                if (Name != "" & Name != null)
                {
                    DATA.AddNewStockItem(this,PurchaseObject);
                    OnStockCreated();
                    ReturnValue = true;
                }
                else
                {
                    ReturnValue = false;
                }
                return ReturnValue;
            }
            catch (Exception E)
            {
                Logger.LogException(E);
                Logger.Show(E);

                ReturnValue = false;
                return ReturnValue;
            }
        }

        public bool AddMultiWithPurchase(skPurchase Purchase)
        {
            PurchaseObject = Purchase;
            bool ReturnValue = false;
            if (this.IsObjectValid)
            {
                try
                {
                    if (PurchaseObject.Title != "" & PurchaseObject != null)
                    {
                        DATA.AddNewStockItem(StockList, PurchaseObject);
                        OnStockCreated();
                        ReturnValue = true;
                    }
                    else
                    {
                        ReturnValue = false;
                    }
                    return ReturnValue;
                }
                catch (Exception E)
                {
                    Logger.LogException(E);
                    Logger.Show(E);

                    ReturnValue = false;
                    return ReturnValue;
                }
            }
            ReturnValue = false;
            return ReturnValue;

        }

        public void GoToStockTransaction()
        {

        }

        public bool Update()
        {
            bool ReturnValue = false;

            try
            {
                DATA.UpdateStock(this);
                ReturnValue = true;
                OnStockUpdated();
                return ReturnValue;
            }
            catch (Exception E)
            {
                
                ReturnValue = false;
                return ReturnValue;

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
                UI.Enviroment.LoadNewTab("StockManagerView");
            }
        }

        public void PopulatePurchaseObj()
        {
            try
            {
                PurchaseObject = DATA.GetPurchaseDetailsByStockID(Convert.ToInt32(Stockid));
            }
            catch(Exception e)
            {
                if (e.HResult == -2147467261)
                {
                    throw new Exception("No Purchase exists for this stock item.");
                }
            }
        }
        

        public void PopulateSaleObj()
        {
            try
            {
                SaleObject = DATA.GetSalesDetailsByStockID(Convert.ToInt32(Stockid));
            }
            catch (Exception e)
            {
                if (e.HResult == -2147467261)
                {
                    throw new Exception("No Sale exists for this stock item.");
                }
            }
        }
        
        public void LinktoTransaction(ITransaction TransactionObject)
        {
            DATA.LinkTranToStock(TransactionObject, this);

        } 

       public List<skStock> GetStockList(skStock SearchFilterObj)
       {
            return DATA.GetStockList(SearchFilterObj).ToList<skStock>();
       }

        public List<skStock> GetStockList()
        {
            return DATA.GetStockList().ToList<skStock>();
        }

        public List<skStock> GetOrphanedStockList(int type)
        {
            return DATA.GetOrphanedStockList(type).ToList<skStock>();
        }

       public override bool Equals(object obj)
       {
           if (obj == null || !(obj is skStock))
               return false;
           return ((skStock)obj).Stockid == this.Stockid;
       }

       public override int GetHashCode()
       {
           return this.Stockid.GetHashCode();
       } 

        public void RemoveFromPurchase()
        {
            DATA.RemoveStockFromPurchase(Convert.ToInt32(this.Stockid)); 
        }

        public void RemoveFromSale()
        {
            DATA.RemoveStockFromSale(Convert.ToInt32(this.Stockid));
        }


        public static skStock GetStockObject(int stockID)
        {

            DataFunctions DB = new DataFunctions();

            skStock PurchaseOBJ = DB.GetStockDetails(stockID);

            if(PurchaseOBJ != null)
            {
                return PurchaseOBJ;
            }
            else
            {
                throw new Exception("No stock found !");
            }

        }

        public void UpdateCurrentObject()
        {
            if(Stockid >= 0)
            {
                if(IsDirtyObject())
                {
                    var NewObject = GetStockObject(Convert.ToInt32(this.Stockid));

                    this.category = NewObject.category;
                    this.categoryID = NewObject.categoryID;
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
                    this.ValueBandID = NewObject.ValueBandID;

                }
            }
        }
        #endregion

    }
}
