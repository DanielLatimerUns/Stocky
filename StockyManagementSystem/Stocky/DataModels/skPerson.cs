using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using Stocky.DataAccesse;
using Stocky.Utility;

namespace Stocky.Data
{
    public class skPerson : Validation,IDataErrorInfo, INotification
    {
        #region Fields
        private DataFunctions DATA;
        #endregion

        #region Properties
        public int pID { get; set; }
        [RequiredData]
        public string FirstName { get; set; }
        [RequiredData]
        public string Surname { get; set; }
        [RequiredData]
        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Mobile { get; set; }
        [RequiredData]
        public string EbayName { get; set; }

        public int AddressID { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }

            set { FullName = value; }
        }

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
            ValiationObject = this;
            DATA = new DataFunctions();
            NewNotification = new Notification();
            PersonCreated += NewNotification.OnNotificationReceived;
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
        public bool AddNew()
        {
            if (FirstName != null)
            {
                DATA.AddNewPerson(this);
                OnPersonCreated();
                return true;
            }
            return false;
        }

        public bool Update()
        {
            if (IsObjectValid)
            {
                //DATA.UpdatePerson(this);
                OnPersonUpdated();
                return true;
            }
            return false;
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

        public void OpenNotification()
        {
            //Do something here
        }
        #endregion
    }
}
