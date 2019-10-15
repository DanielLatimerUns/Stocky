using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.DataAccesse;
using Stocky.Utility;
using Stocky.DataModels.Transaction;

namespace Stocky.Data
{
    public class skSales : skTransactionBase, IDataErrorInfo, INotification,ITransaction
    {
        #region Fields
        private decimal? _PayPalFees;
        private decimal? _Postage;
        private DateTime? _SaleDate;
        private string _SaleMethod;

     
        #endregion

        #region Properties
 
        [RequiredData]
        public decimal? PayPalFees
        {
            get { return _PayPalFees; }
            set
            {
                _PayPalFees = value;
                RaisePropertyChanged("PayPalFees");
            }
        }
        [RequiredData]
        public decimal? Postage
        {
            get { return _Postage; }
            set
            {
                _Postage = value;
                RaisePropertyChanged("Postage");
            }
        }

        public string SoldBY { get; set; }
        [RequiredData]
        public DateTime? SaleDate
        {
            get { return _SaleDate; }
            set
            {
                _SaleDate = value;
                RaisePropertyChanged("SaleDate");
            }
        }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }
        [RequiredData]
        public int? AddressId { get; set; }

        public string PayPalTransactionID { get; set; }
        [RequiredData]
        public string SaleMethod
        {
            get { return _SaleMethod; }
            set
            {
                _SaleMethod = value;
                RaisePropertyChanged("SaleMethod");
            }
        }

        public List<skStock> StockList { get; set; }

        public skUser UserObj { get; set; }

        public decimal? TotalFee { get { return Amount + PayPalFees + Postage; } }

        public Notification NewNotification { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> SaleCreated;

        protected virtual void OnSaleCreated()
        {
            CreateNotification(EventActionType.Created);
            SaleCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> SaleUpdated;

        protected virtual void OnSaleUpdated()
        {
            CreateNotification(EventActionType.Updated);
            SaleUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> SaleDeleted;

        protected virtual void OnSaleDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            SaleDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skSales()
        {
            StockList = new List<skStock>();
            NewNotification = new Notification();
            this.ValiationObject = this;
            SaleCreated += NewNotification.OnNotificationReceived;

            TransactionType = TransactionType.Sale;
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
        public bool AddNewSale()
        {           
            try
            {
                if (IsObjectValid)
                {
                    DATA.AddNewSale(this);
                    OnSaleCreated();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool AddNewSale(skAddresses AddressObject)
        {
            try
            {
                if (IsObjectValid)
                {
                    DATA.AddNewSale(AddressObject, this);
                    OnSaleCreated();
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update()
        {
            Validate();
                if (IsObjectValid)
                {
                    DATA.UpdateSale(this);
                    OnSaleUpdated();              
                }
                else
                {
                   
                }

        }

        public bool Delete()
        {
                if (IsObjectValid)
                {
                    //DATA.DeleteSale(this);
                    OnSaleDeleted();
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public static void OpenSaleTab(skSales SaleObj)
        {
          
            ObjectMessenger om = new ObjectMessenger();


            if (SaleObj != null)
            {
                om.Send("TRANOBJ", SaleObj);
                UI.Enviroment.LoadNewTab("SalesDetailsView");
            }
            else
            {
                throw new Exception("No Sale with this ID can be found.");
            }
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Sale Created.";
                    NewNotification.Description = string.Format("{0} - has been created as a sale.", Title);

                    StringBuilder CreateEmail = new StringBuilder();

                    CreateEmail.AppendLine("A New sale has been created with the below details");
                    CreateEmail.AppendLine(Environment.NewLine);
                    CreateEmail.AppendLine("Name: " + Title);
                    CreateEmail.AppendLine("Description: " + Description);
                    CreateEmail.AppendLine("Method: " + SaleMethod);
                    CreateEmail.AppendLine("Sale Amount: £" + Amount);
                    CreateEmail.AppendLine("Postage Amount: £" + Postage);
                    CreateEmail.AppendLine("PayPal Fees: £" + PayPalFees);
                    CreateEmail.AppendLine("Total Fee: " + TotalFee);

                    NewNotification.EmailBody = CreateEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Sale Updated.";
                    NewNotification.Description = string.Format("{0} - sale has been updated.", Title);

                    StringBuilder UpdateEmail = new StringBuilder();

                    UpdateEmail.AppendLine("Sale with the below details has been updated.");
                    UpdateEmail.AppendLine(Environment.NewLine);
                    UpdateEmail.AppendLine("Name: " + Title);
                    UpdateEmail.AppendLine("Description: " + Description);
                    UpdateEmail.AppendLine("Method: " + SaleMethod);
                    UpdateEmail.AppendLine("Sale Amount: £" + Amount);
                    UpdateEmail.AppendLine("Postage Amount: £" + Postage);
                    UpdateEmail.AppendLine("PayPal Fees: £" + PayPalFees);
                    UpdateEmail.AppendLine("Total Fee: " + TotalFee);

                    NewNotification.EmailBody = UpdateEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Sale Deleted.";
                    NewNotification.Description = string.Format("{0} - sale has been deleted.", Title);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("Sale with the below details has been deleted.");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Name: " + Title);
                    DeletedEmail.AppendLine("Description: " + Description);
                    DeletedEmail.AppendLine("Method: " + SaleMethod);
                    DeletedEmail.AppendLine("Sale Amount: £" + Amount);
                    DeletedEmail.AppendLine("Postage Amount: £" + Postage);
                    DeletedEmail.AppendLine("PayPal Fees: £" + PayPalFees);
                    DeletedEmail.AppendLine("Total Fee: " + TotalFee);

                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public void OpenNotification()
        {
            OpenTransaction();
        }

        public static skSales GetSalesObject(int SaleID)
        {
            DataFunctions DB = new DataFunctions();

            skSales SalesOBJ = DB.GetSalesDetails(SaleID);

            if(SalesOBJ != null)
            {
                return SalesOBJ;
            }
            else
            {
                throw new Exception("No sale found !");
            }
        }

        public void UpdateCurrentObject()
        {
            if(ID >= 0)
            {
                if (IsDirtyObject())
                {
                    var newobject = skSales.GetSalesObject(this.ID);

                    this.AddressId = newobject.AddressId;
                    this.Created = newobject.Created;
                    this.Description = newobject.Description;
                    this.Title = newobject.Title;
                    this.PayPalFees = newobject.PayPalFees;
                    this.PayPalTransactionID = newobject.PayPalTransactionID;
                    this.Postage = newobject.Postage;
                    this.Amount = newobject.Amount;
                    this.SaleDate = newobject.SaleDate;
                    this.SaleMethod = newobject.SaleMethod;
                    this.SoldBY = newobject.SoldBY;
                    this.StockList = newobject.StockList;
                    this.Updated = newobject.Updated;
                }

            }
        }

        public void OpenTransaction()
        {
            UpdateCurrentObject();
            skSales.OpenSaleTab(this);
        }

        #endregion
    }

}
