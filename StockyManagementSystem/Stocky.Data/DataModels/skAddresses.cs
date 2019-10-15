using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.Utility;
using Stocky.Data.Interfaces;

namespace Stocky.Data
{
    public class skAddresses : DataModelBase,IDataErrorInfo,INotification, IDataModel
    {
        #region Fields
        
        #endregion

        #region Properties
        public int AddressID { get; set; }
        [RequiredData]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }
        [RequiredData]
        public string PostCode { get; set; }

        public bool? Active { get; set; }

        public string Town { get; set; }
        [RequiredData]
        public string Country { get; set; }

        public string County { get; set; }

        public string FullAddress
        {
            get
            {                
                StringBuilder Address = new StringBuilder();

                Address.AppendLine(AddressLine1);
                if (AddressLine2 != "")
                {
                    Address.AppendLine(AddressLine2);
                }
                if (AddressLine3 != "")
                {
                    Address.AppendLine(AddressLine3);
                }
                if (Town != "")
                {
                    Address.AppendLine(Town);
                }
                if (County != "")
                {
                    Address.AppendLine(County);
                }
                if (Country != "")
                {
                    Address.AppendLine(Country);
                }
                
                return Address.ToString();
            }
        }

        public string SearchString
        {
            get { return AddressID + AddressLine1 + AddressLine2 + AddressLine3 + PostCode + Country + County; }
        }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }


        public Notification NewNotification { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> AddressCreated;

        protected virtual void OnAddressCreated()
        {
            CreateNotification(EventActionType.Created);
            AddressCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> AddressUpdated;

        protected virtual void OnAddressUpdated()
        {
            CreateNotification(EventActionType.Updated);
            AddressUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> AddressDeleted;

        protected virtual void OnAddressDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            AddressDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skAddresses()
        {
            Initialise();
        }

        public skAddresses(skUser CUrrentUserOBJ):
            base(CUrrentUserOBJ)    
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

            AddressCreated += NewNotification.OnNotificationReceived;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skAddresses))
                return false;
            return ((skAddresses)obj).AddressID == 0 ? ((skAddresses)obj) == this : ((skAddresses)obj).AddressID == this.AddressID;
        }

        public override int GetHashCode()
        {
            return this.AddressID == 0 ? base.GetHashCode() : this.AddressID.GetHashCode();
        }
        public override string ToString()
        {
            return FullAddress;
        }

        public static List<skAddresses> GetAddressList()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skAddresses> ReturnList = DataClient.GetAddressesList().AddressList;

                return ReturnList;
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void Add()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewAddress(this);

                    OnAddressCreated();
                }
                else
                {
                    throw new Stocky.Exceptions.InvalidObjectException(this.PropertyList);
                }
               
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void Add(int ObjectID, Type ObjectType)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewAddress(this, ObjectID, ObjectType);
                    OnAddressCreated();
                }
                else
                {
                    throw new Stocky.Exceptions.InvalidObjectException(this.PropertyList);
                }

            }
            finally
            {
                DataClient.Close();
            }
        }

        public void Update()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    //DataClient.update(this);
                }
                else
                {
                    throw new Stocky.Exceptions.InvalidObjectException(this.PropertyList);
                }         
            }
            finally
            {
                DataClient.Close();
            }
          
        }

        public void Delete()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    //DataClient.update(this);                
                }
                else
                {
                    throw new Stocky.Exceptions.InvalidObjectException(this.PropertyList);
                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            NewNotification.RaisedBy = CurrentUser.UserID;
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Address created";
                    NewNotification.Description = string.Format("A New address has been created: {0}", AddressID);

                    StringBuilder CreatedEmail = new StringBuilder();

                    CreatedEmail.AppendLine("A new address has been created with the below details");
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine(AddressLine1);
                    if (AddressLine2 != "")
                    {
                        CreatedEmail.AppendLine(AddressLine2);
                    }
                    if (AddressLine3 != "")
                    {
                        CreatedEmail.AppendLine(AddressLine3);
                    }
                    if (Town != "")
                    {
                        CreatedEmail.AppendLine(Town);
                    }
                    if (County != "")
                    {
                        CreatedEmail.AppendLine(County);
                    }
                    CreatedEmail.AppendLine(PostCode);
                    CreatedEmail.AppendLine(Country);


                    NewNotification.EmailBody = CreatedEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Address Update";
                    NewNotification.Description = string.Format("An address has been Updated: {0}", AddressID);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine(AddressID + ": address has been updated with the below details");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine(AddressLine1);
                    if (AddressLine2 != "")
                    {
                        UpdatedEmail.AppendLine(AddressLine2);
                    }
                    if (AddressLine3 != "")
                    {
                        UpdatedEmail.AppendLine(AddressLine3);
                    }
                    if (Town != "")
                    {
                        UpdatedEmail.AppendLine(Town);
                    }
                    if (County != "")
                    {
                        UpdatedEmail.AppendLine(County);
                    }
                    UpdatedEmail.AppendLine(PostCode);
                    UpdatedEmail.AppendLine(Country);


                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Address Updated";
                    NewNotification.Description = string.Format("An address has been deleted: {0}", AddressID);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine(AddressID + ": address has been deleted");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine(AddressLine1);
                    if (AddressLine2 != "")
                    {
                        DeletedEmail.AppendLine(AddressLine2);
                    }
                    if (AddressLine3 != "")
                    {
                        DeletedEmail.AppendLine(AddressLine3);
                    }
                    if (Town != "")
                    {
                        DeletedEmail.AppendLine(Town);
                    }
                    if (County != "")
                    {
                        DeletedEmail.AppendLine(County);
                    }
                    DeletedEmail.AppendLine(PostCode);
                    DeletedEmail.AppendLine(Country);


                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public void OpenNotification()
        {
            //Do Something Here
        }
        #endregion
    }
}
