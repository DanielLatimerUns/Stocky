using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using Stocky.Utility;
using Stocky.Data.Interfaces;
using MVVM_Framework;

namespace Stocky.Data
{
    public class skRefund : skTransactionBase , IDataErrorInfo,INotification,ITransaction, IDataModel
    {
        #region Fields
        #endregion

        #region Properties
        [RequiredData]
        public decimal? SHippingCosts { get; set; }
        [RequiredData]
        public bool ReturnStock { get; set; }

        public skStock StockItem { get; set; }

        public Notification NewNotification { get; set; }

        public skUser User { get; set; }

        public DateTime Updated { get; set; }

        public DateTime Created { get; set; }

        public string PayPalTransactionID { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> RefundCreated;

        public virtual void OnRefundCreated()
        {
                CreateNotification(EventActionType.Created);
                RefundCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> RefundUpdated;

        public virtual void OnRefundUpdated()
        {
            CreateNotification(EventActionType.Updated);
            RefundUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> RefundDeleted;

        public virtual void OnRefundDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            RefundDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skRefund()
        {
            Initialise();
        }
        public skRefund(skUser CurrentUserOBJ)
            :base(CurrentUserOBJ)
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

            RefundCreated += NewNotification.OnNotificationReceived;

            TransactionType = TransactionType.Refund;
        }
        public void AddnewRefund(int UserID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewRefund(this, UserID);
                   
                    OnRefundCreated();

                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public bool Update()
        {
            if (IsObjectValid)
            {
                //DataClient.Open();
                //DataClient.u
                OnRefundUpdated();
                return true;
            }
            return false;
        }

        public bool Delete()
        {
            if (IsObjectValid)
            {
                //DATA.DeleteRefund(this, true);
                OnRefundDeleted();
                return true;
            }
            return false;
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Refund Created.";
                    NewNotification.Description = string.Format("{0} Has been refunded succsesfully.", StockItem.Name);

                    StringBuilder CreatedEmail = new StringBuilder();

                    CreatedEmail.AppendLine("A new refund has been created with the below details");
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine("Stock ID:" + StockItem.Stockid);
                    CreatedEmail.AppendLine("Stock Name:" + StockItem.Name);
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine("Refund Reason: " + Description);
                    CreatedEmail.AppendLine("Refund Amount: " + Amount);

                    NewNotification.EmailBody = CreatedEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Refund Updated.";
                    NewNotification.Description = string.Format("{0} refund has been updated.", StockItem.Name);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine("Refund with the below details has been updated.");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Stock ID:" + StockItem.Stockid);
                    UpdatedEmail.AppendLine("Stock Name:" + StockItem.Name);
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Refund Reason: " + Description);
                    UpdatedEmail.AppendLine("Refund Amount: " + Amount);

                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Refund Deleted.";
                    NewNotification.Description = string.Format("{0} refund has been deleted.", StockItem.Name);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("Refund with the below details has been deleted.");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Stock ID:" + StockItem.Stockid);
                    DeletedEmail.AppendLine("Stock Name:" + StockItem.Name);
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Refund Reason: " + Description);
                    DeletedEmail.AppendLine("Refund Amount: " + Amount);

                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public static skRefund GetRefundObject(int ID)
        {
            Stocky.Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                skRefund RefundOBJ = DataClient.GetRefundObject(ID).RefundObject;

                if (RefundOBJ != null)
                {
                    return RefundOBJ;
                }
                else
                {
                    throw new Exception("No Refund Found");
                }
            }
            finally
            {
                DataClient.Close();
            }
        
       }

        public void UpdateCurrentObject()
        {
            if(ID > 0)
            {
                if(IsDirtyObject())
                {
                    var newobject = skRefund.GetRefundObject(this.ID);

                    this.Amount = newobject.Amount;
                    this.Created = newobject.Created;
                    this.Description = newobject.Description;
                    this.PayPalTransactionID = newobject.PayPalTransactionID;
                    this.ReturnStock = newobject.ReturnStock;
                    this.SHippingCosts = newobject.SHippingCosts;
                    this.StockItem = newobject.StockItem;
                    this.Title = newobject.Title;
                    this.TransactionTime = newobject.TransactionTime;
                    this.TransactionType = newobject.TransactionType;
                    this.Updated = newobject.Updated;
//                    this.User = newobject.User;

                }
            }
        }

        //public static void OpenRefundTab(skRefund RefundOBJ)
        //{
        //    ObjectMessenger om = new ObjectMessenger();


        //    if (RefundOBJ != null)
        //    {
        //        om.Send("TRANOBJ", RefundOBJ);
        //        UI.Enviroment.LoadNewTab("SalesDetailsView");
        //    }
        //    else
        //    {
        //        throw new Exception("No Sale with this ID can be found.");
        //    }
        //}

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skRefund))
                return false;
            return ((skRefund)obj).ID == 0 ? ((skRefund)obj) == this : ((skRefund)obj).ID == this.ID;
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
            //Do something
        }

        public void OpenTransaction()
        {
            UpdateCurrentObject();
        }
        #endregion
    }
}
