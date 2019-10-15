using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.Data.Interfaces;
using Stocky.Utility;


namespace Stocky.Data
{
    public class skvendors : DataModelBase, IDataErrorInfo,INotification,IDataModel
    {
        #region Fields
   
        #endregion

        #region Properties
        public int VendorID { get; set; }   
        [RequiredData]
        public string VendorsName { get; set; }
        [RequiredData]
        public string VendorsDescption { get; set; }
        [RequiredData]
        public bool? onlineVendor { get; set; }        
        public int? AddresseID { get; set; }
        public skAddresses _CurrentAddress { get; set; }
        public skAddresses CurrentAddress
        {
            get { return _CurrentAddress; }
            set
            {
                _CurrentAddress = value;
                RaisePropertyChanged("CurrentAddress");
            }
        }

        public Notification NewNotification { get; set; }

        public string SearchString { get { return VendorID + VendorsName + VendorsDescption; } }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> VendorCreated;

        public void OnVendorCreated()
        {
                CreateNotification(EventActionType.Created);
                VendorCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> VendorUpdated;

        public void OnVendorUpdated()
        {
            CreateNotification(EventActionType.Updated);
            VendorUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> VendorDeleted;

        public void OnVendorDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            VendorDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors

        public skvendors()
        {
            Initialise();
        }
        public skvendors(skUser CurrentUserOBJ)
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

            VendorCreated += NewNotification.OnNotificationReceived;

            onlineVendor = false;
        }
        public static List<skvendors> VendorList()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();
                List<skvendors> returnlist = DataClient.GetVendorList().VendorList;

                if(returnlist.Count != 0)
                {
                    return returnlist;
                }
                else
                {
                    throw new Exception("No Records Found");
                }
            }
            finally
            {
                DataClient.Close();
            }
        }

        public List<skAddresses> GetAddressses()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skAddresses> returnlist = DataClient.GetAddressesList().AddressList;
                if(returnlist.Count != 0)
                {
                    return returnlist;
                }
                else
                {
                    throw new Exception("No Records Found");
                }
            }
            finally
            {
                DataClient.Close();
            }
        }



        public bool Add()
        {         
            if(IsObjectValid)
            {
                Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
                DataClient.Open();
                DataClient.AddNewVendor(this);
                DataClient.Close();
                           
                OnVendorCreated();
                return true;
            }
            else
            {
                return false;
            }         
        }

        public void Add(skAddresses AddressObject)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.AddNewVendor(this, AddressObject);
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

        public bool Update()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (IsObjectValid)
                {
                    DataClient.Open();
                    DataClient.UpdateVendor(this);
                    return true;
                }
                else
                {
                    return false;
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
                //DATA.DeleteVendor(this);
                OnVendorDeleted();
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

                    NewNotification.Name = "New Vendor created";
                    NewNotification.Description = string.Format("A New vendor has been created: {0}", VendorsName);
                    NewNotification.IsNew = true;
                //    NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("A New vendor has been created. {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Vendor updated";
                    NewNotification.Description = string.Format("Vendor has been updated: {0}", VendorsName);
                    NewNotification.IsNew = true;
                 //   NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("Vendor update: {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Vendor deleted";
                    NewNotification.Description = string.Format("Vendor has been deleted: {0}", VendorsName);
                    NewNotification.IsNew = true;
                  //  NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("Vendor deletion: {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
            }

        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skvendors))
                return false;
            return ((skvendors)obj).VendorID == 0 ? ((skvendors)obj) == this : ((skvendors)obj).VendorID == this.VendorID;
        }

        public override int GetHashCode()
        {
            return this.VendorID == 0 ? base.GetHashCode() : this.VendorID.GetHashCode();
        }

        public override string ToString()
        {
            return VendorsName + Environment.NewLine + VendorsDescption;
        }

        public void OpenNotification()
        {
            //Do Something
        }
        #endregion
    }


}
