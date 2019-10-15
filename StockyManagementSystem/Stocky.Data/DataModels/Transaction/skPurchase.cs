using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.Exceptions;
using Stocky.Utility;
using Stocky.Data.Interfaces;

namespace Stocky.Data
{
   public class skPurchase : skTransactionBase, IDataErrorInfo, INotification,ITransaction, IDataModel
    {
        #region Fields
        #endregion

        #region Properties

        private decimal _Postage;
        [RequiredData(AllowZeros = true)]
        public decimal Postage
        {
            get { return _Postage; }
            set
            {
                _Postage = value;
                TotalValue += value;
                RaisePropertyChanged("Postage");
            }
        }

        private string _CreatedBy;

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set
            {
                _CreatedBy = value;
                RaisePropertyChanged("CreatedBy");
            }
        }

        private DateTime? _Created;

        public DateTime? Created
        {
            get { return _Created; }
            set
            {
                _Created = value;
                RaisePropertyChanged("Created");
            }
        }

        private DateTime? _PurchaseDate;

        public DateTime? PurchaseDate
        {
            get { return _PurchaseDate; }
            set
            {
                _PurchaseDate = value;
                RaisePropertyChanged("PurchaseDate");
            }
        }

        private DateTime? _Updated;

        public DateTime? Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                RaisePropertyChanged("Updated");
            }
        }

        private string _Invoice;

        public string Invoice
        {
            get { return _Invoice; }
            set
            {
                _Invoice = value;
                RaisePropertyChanged("Invoice");
            }
        }

        private skvendors _VendorObject;
        [RequiredData]
        public skvendors VendorObject
        {
            get { return _VendorObject; }
            set
            {
                _VendorObject = value;
                RaisePropertyChanged("VendorObject");
            }
        }

        private string _PapPalTransactionID;

        public string PapPalTransactionID
        {
            get { return _PapPalTransactionID; }
            set
            {
                _PapPalTransactionID = value;
                RaisePropertyChanged("PapPalTransactionID");
            }
        }

        private bool? _IsExpense;

        public bool? IsExpense
        {
            get { return _IsExpense; }
            set
            {
                _IsExpense = value;
                RaisePropertyChanged("IsExpense");
            }
        }

        private decimal _TotalValue;
        /// <summary>
        /// /To get Proerpty changed event to work you must call the setter TotalValue = 0
        /// </summary>
        public decimal TotalValue 
        {
            get { return _TotalValue = (Postage + Convert.ToDecimal(Amount)); }
            set
            {
                _TotalValue = value;
                RaisePropertyChanged("TotalValue");
            }
        }

        public List<skStock> LinkedStock { get; set; }

        public Notification NewNotification { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> PurchaseCreated;

        protected virtual void OnPurchaseCreated()
        {
            CreateNotification(EventActionType.Created);
            PurchaseCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> PurchaseUpdated;

        protected virtual void OnPurchaseUpdated()
        {
            CreateNotification(EventActionType.Updated);
            PurchaseUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> PurchaseDeleted;

        protected virtual void OnPurchaseDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            PurchaseDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skPurchase()
        {
            Initialise();
        }
        public skPurchase(skUser CurrentUserObj) 
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
            NewNotification = new Notification();
         
            this.ValiationObject = this;

            PurchaseCreated += NewNotification.OnNotificationReceived;
            PurchaseCreated += NewNotification.OnNotificationReceived;

            TransactionType = TransactionType.Purchase;
        }

        public void Add()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {             
                if (IsObjectValid)
                {
                    DataClient.Open();
                                          
                    DataClient.AddNewPurchase(this, CurrentUser.UserID);
                    
                    OnPurchaseCreated();
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

        public List<skStock> LinkedStockList()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skStock> returnList = DataClient.GetStockListByTransaction(this).StockList;

                return returnList;
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void Update()
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                Validate();
                if (IsObjectValid)
                {
                    OnPurchaseUpdated();
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

        public void Delete()
        {
            if (IsObjectValid)
            {
                //DATA.DeletePurchase(this);
                OnPurchaseDeleted();

            }
            else
            {
                throw new InvalidObjectException(PropertyList);
            }
        }

        //public static void OpenPurchaseTab(skPurchase PurchaseObj)
        //{
           
        //    ObjectMessenger om = new ObjectMessenger();
                     
        //    if (PurchaseObj != null)
        //    {
        //        om.Send("TRANOBJ", PurchaseObj);
        // //       UI.Enviroment.LoadNewTab("PurchaseDetailsView");
        //    }
        //    else
        //    {
        //        throw new Exception("No Purchase with this ID can be found.");
        //    }          
        //}

        public static skPurchase GetPurchaseObject(int ID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                skPurchase PurchaseOBJ = DataClient.GetPurchaseObject(ID).PurchaseObject;

                if (PurchaseOBJ != null)
                {
                    return PurchaseOBJ;
                }
                else
                {
                    throw new Exception("No purchase found !");
                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public static List<Stocky.Data.Interfaces.ITransaction> GetPurchases()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();

            List<Stocky.Data.Interfaces.ITransaction> templist = new List<Stocky.Data.Interfaces.ITransaction>();

            try
            {
                DataClient.Open();

                foreach (var item in DataClient.GetTransactionList(new SearchFilterObject { ExtraSearchObject1 = Stocky.Data.TransactionType.Purchase }).TransactionList)
                {
                    templist.Add(item);
                }           
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                DataClient.Close();
            }

            return templist;
        }

        public void UpdateCurrentObject()
        {
            if(ID >= 0)
            {
                if (IsDirtyObject())
                {
                    var newobject = skPurchase.GetPurchaseObject(this.ID);

                    this.Created = newobject.Created;
                    this.CreatedBy = newobject.CreatedBy;
                    this.Invoice = newobject.Invoice;
                    this.IsExpense = newobject.IsExpense;
                    this.Description = newobject.Description;
                    this.Title = newobject.Title;
                    this.LinkedStock = newobject.LinkedStock;
                    this.PapPalTransactionID = newobject.PapPalTransactionID;
                    this.Postage = newobject.Postage;
                    this.PurchaseDate = newobject.PurchaseDate;
                    this.Amount = newobject.Amount;
                    this.Updated = newobject.Updated;             
                    this.VendorObject = newobject.VendorObject;
                    this.TotalValue = newobject.TotalValue;
                }                               
            }
            else
            {
                throw new Exception("No Purchase ID for this object!");
            }
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            NewNotification.RaisedBy = CurrentUser.UserID;
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Purchase Created.";
                    NewNotification.Description = string.Format("{0} - has been created as a purchase.", Title);

                    StringBuilder CreatedEmail = new StringBuilder();

                    CreatedEmail.AppendLine("A New purchase has been created with the below details");
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine("Name: " + Title);
                    CreatedEmail.AppendLine("Description: " + Description);
                    CreatedEmail.AppendLine("Purchase Price: £" + Amount);
                    CreatedEmail.AppendLine("Postage Price: £" + Postage);
                    CreatedEmail.AppendLine("Total Price: £" + TotalValue);

                    NewNotification.EmailBody = CreatedEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Purchase Updated.";
                    NewNotification.Description = string.Format("{0} - purchase has been updated.", Title);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine("Purchase has been updated with the below details");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Name: " + Title);
                    UpdatedEmail.AppendLine("Description: " + Description);
                    UpdatedEmail.AppendLine("Purchase Price: £" + Amount);
                    UpdatedEmail.AppendLine("Postage Price: £" + Postage);
                    UpdatedEmail.AppendLine("Total Price: £" + TotalValue);

                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Purchase Deleted.";
                    NewNotification.Description = string.Format("{0} - purchase has been deleted.", Title);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("Purchase with the below details has been deleted.");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Name: " + Title);
                    DeletedEmail.AppendLine("Description: " + Description);
                    DeletedEmail.AppendLine("Purchase Price: £" + Amount);
                    DeletedEmail.AppendLine("Postage Price: £" + Postage);
                    DeletedEmail.AppendLine("Total Price: £" + TotalValue);

                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skPurchase))
                return false;
            return ((skPurchase)obj).ID == 0 ? ((skPurchase)obj) == this : ((skPurchase)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID == 0 ? base.GetHashCode() : this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return Title + Environment.NewLine + Description;
        }

        public void OpenNotification()
        {
            OpenTransaction();
        }

        public void OpenTransaction()
        {
            UpdateCurrentObject();
         
        }
        #endregion
    }
}
