using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.DataAccesse;
using Stocky.Utility;


namespace Stocky.Data
{
    public class skvendors : Validation, IDataErrorInfo,INotification
    {
        #region Fields
        private DataFunctions DATA;
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
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            DATA = new DataFunctions();
            NewNotification = new Notification();
            this.ValiationObject = this;

            VendorCreated += NewNotification.OnNotificationReceived;

            onlineVendor = false;
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

        public List<skvendors> VendorList()
        {
            return DATA.GetVendorList().ToList();
        }

        public List<skvendors> VendorList(bool filter)
        {
            return DATA.GetVendorList(this).ToList();
        }

        public List<skAddresses> GetAddressses()
        {
            return DATA.GetAdressesList().ToList();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skvendors))
                return false;
            return ((skvendors)obj).VendorID == this.VendorID;
        }

        public override int GetHashCode()
        {
            return this.VendorID.GetHashCode();
        }

        public bool Add()
        {         
            if(IsObjectValid)
            {
                DATA.AddNewVendor(this);                
                OnVendorCreated();
                return true;
            }
            else
            {
                return false;
            }         
        }

        public bool Add(skAddresses AddressObject)
        {          
            if(IsObjectValid)
            {
                DATA.AddNewVendor(this,AddressObject);
                OnVendorCreated();
                return true;           
            }
            else
            {
                return false;
            }           
        }

        public bool Update()
        {
            if(IsObjectValid)
            {
                DATA.UpdateVendor(this);
                OnVendorUpdated();
                return true;
            }
            else
            {
                return false;
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
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Vendor created";
                    NewNotification.Description = string.Format("A New vendor has been created: {0}", VendorsName);
                    NewNotification.IsNew = true;
                    NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("A New vendor has been created. {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Vendor updated";
                    NewNotification.Description = string.Format("Vendor has been updated: {0}", VendorsName);
                    NewNotification.IsNew = true;
                    NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("Vendor update: {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Vendor deleted";
                    NewNotification.Description = string.Format("Vendor has been deleted: {0}", VendorsName);
                    NewNotification.IsNew = true;
                    NewNotification.RaisedBy = StaticDataReposityory.UserID;
                    NewNotification.EmailBody = string.Format("Vendor deletion: {0} Name: {1} {2} Description: {3}", Environment.NewLine, VendorsName, Environment.NewLine, VendorsDescption);

                    break;
            }

        }

        public void OpenNotification()
        {
            //Do Something
        }
        #endregion
    }


}
