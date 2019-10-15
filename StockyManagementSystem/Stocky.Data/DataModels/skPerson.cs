using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using Stocky.Utility;
using Stocky.Data.Interfaces;

namespace Stocky.Data
{
    public class skPerson : DataModelBase,IDataErrorInfo, INotification,IDataModel
    {
        #region Fields

        #endregion

        #region Properties
        public int pID { get; set; }
        private string _Firstname;
        [RequiredData]
        public string FirstName
        {
            get { return _Firstname; }
            set
            {
                _Firstname = value;
                RaisePropertyChanged("FirstName");
                RaisePropertyChanged("FullName");
            }
        }
        private string _Surname;
        [RequiredData]
        public string Surname
        {
            get { return _Surname; }
            set
            {
                _Surname = value;
                RaisePropertyChanged("Surname");
               RaisePropertyChanged("FullName");
            }
        }
        [RequiredData]
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Mobile { get; set; }
        public string EbayName { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public List<skAddresses> LinkedAddresses { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }

        }

        public string SearchString { get { return pID + FirstName + Surname + Email + HomePhone + WorkPhone + EbayName; } }

        public Notification NewNotification { get; set; }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> PersonCreated;

        public virtual void OnPersonCreated()
        {
            CreateNotification(EventActionType.Created);
            PersonCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> PersonUpdated;

        public virtual void OnPersonUpdated()
        {
            CreateNotification(EventActionType.Updated);
            PersonUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> PersonDeleted;

        public virtual void OnPersonDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            PersonDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skPerson()
        {
            Initialise();
        }
        public skPerson(skUser CurrentUserObj)
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
            ValiationObject = this;

            NewNotification = new Notification();
            PersonCreated += NewNotification.OnNotificationReceived;
            PersonUpdated += NewNotification.OnNotificationReceived;
        }

        public void AddNew()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewPerson(this);

                    OnPersonCreated();
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

        public void AddNew(skAddresses AddressObject)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    if (AddressObject.IsObjectValid)
                    {
                        DataClient.Open();
                        DataClient.AddNewPerson(this, AddressObject);

                        OnPersonCreated();
                    }
                    else { throw new Stocky.Exceptions.InvalidObjectException(AddressObject.PropertyList); }
                }
                else
                { throw new Stocky.Exceptions.InvalidObjectException(this.PropertyList); }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public void AddNew(int AddressID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewPerson(this, AddressID);

                    OnPersonCreated();
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

        public static List<skPerson> GetPersonList()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                var returnlist = DataClient.GetPersonList().PersonList;   
                            
                    return returnlist;

            }
            finally
            {
                DataClient.Close();
            }
        }

        public static skPerson GetPersonObject(int PersonID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                return DataClient.GetPersonObject(PersonID).PersonObject;
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
                DataClient.Open();

                if (IsObjectValid)
                {
                    DataClient.UpdatePerson(this);
                    OnPersonUpdated();
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

        public void RemoveLinkedAddress(int AddressID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                if (this.pID != 0)
                {
                    DataClient.RemovePersonAddressLink(this.pID,AddressID);
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

        public void LinkAddress(int AddressID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                if (this.pID != 0)
                {
                    DataClient.LinkAddressToPerson(this.pID, AddressID);
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

        public void UpdateLinkedAddresses()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                if (LinkedAddresses.Count != 0)
                {
                    LinkedAddresses.Clear();

                    foreach (var item in DataClient.GetAddressesListByPerson(this.pID).AddressList)
                    {
                        LinkedAddresses.Add(item);
                    }
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
                //DATA.DeletePerson(this);
                OnPersonDeleted();
                return true;
            }
            return false;
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            NewNotification.RaisedBy = CurrentUser.UserID;
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Person Created.";
                    NewNotification.Description = string.Format("{0} - has been created as a person.", FullName);

                    StringBuilder CreatedEmail = new StringBuilder();

                    CreatedEmail.AppendLine("A New person has been created with the below details");
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine("Name:" + FullName);
                    CreatedEmail.AppendLine("Ebay Name: " + EbayName);
                    CreatedEmail.AppendLine("Email Address: " + Email);
                    CreatedEmail.AppendLine("Contact Numbers:");
                    CreatedEmail.AppendLine("                " + "Home: " + HomePhone);
                    CreatedEmail.AppendLine("                " + "Work: " + WorkPhone);
                    CreatedEmail.AppendLine("                " + "Mobile: " + Mobile);

                    NewNotification.EmailBody = CreatedEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Person Updated.";
                    NewNotification.Description = string.Format("{0} - has been updated.", FullName);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine("A person has been updated with the below details");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Name:" + FullName);
                    UpdatedEmail.AppendLine("Ebay Name: " + EbayName);
                    UpdatedEmail.AppendLine("Email Address: " + Email);
                    UpdatedEmail.AppendLine("Contact Numbers:");
                    UpdatedEmail.AppendLine("                " + "Home: " + HomePhone);
                    UpdatedEmail.AppendLine("                " + "Work: " + WorkPhone);
                    UpdatedEmail.AppendLine("                " + "Mobile: " + Mobile);

                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Person Deleted.";
                    NewNotification.Description = string.Format("{0} - has been deleted.", FullName);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("A person with the below details has been deleted");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Name:" + FullName);
                    DeletedEmail.AppendLine("Ebay Name: " + EbayName);
                    DeletedEmail.AppendLine("Email Address: " + Email);
                    DeletedEmail.AppendLine("Contact Numbers:");
                    DeletedEmail.AppendLine("                " + "Home: " + HomePhone);
                    DeletedEmail.AppendLine("                " + "Work: " + WorkPhone);
                    DeletedEmail.AppendLine("                " + "Mobile: " + Mobile);

                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skPerson))
                return false;
            return ((skPerson)obj).pID == 0 ? ((skPerson)obj) == this : ((skPerson)obj).pID == this.pID; 
        }

        public override int GetHashCode()
        {
            return this.pID == 0 ? base.GetHashCode() : this.pID.GetHashCode();
        }

        public override string ToString()
        {
            return FirstName + " " + Surname;
        }

        public void OpenNotification()
        {
            //Do something here
        }
        #endregion
    }
}
