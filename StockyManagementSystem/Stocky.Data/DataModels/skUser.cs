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
    public class skUser : DataModelBase, IDataErrorInfo, INotification,IDataModel
    {
        #region Fields
        
        #endregion

        #region Properties
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get { return FistName + "" + LastName; } }

        public string FistName { get; set; }

        public string LastName { get; set; }

        public string Initials { get; set; }

        public DateTime? DOB { get; set; }

        public string Email { get; set; }

        public string WorkPhone { get; set; }

        public string HomePhone { get; set; }

        public string Mobile { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }

        public int ClientID { get; set; }

        public Notification NewNotification { get; set; }

        public string SearchString
        { get
            { return 
                    UserID
                    +UserName
                    +FistName
                    +LastName
                    +Initials
                    +Email
                    +WorkPhone
                    +HomePhone;
            }
        }
        #endregion

        #region Events
        public event EventHandler<NewNotificationEventArgs> UserCreated;

        protected virtual void OnUserCreated()
        {
            CreateNotification(EventActionType.Created);
            UserCreated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> UserUpdated;

        protected virtual void OnUserUpdated()
        {
            CreateNotification(EventActionType.Updated);
            UserUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> UserDeleted;

        protected virtual void OnUserDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            UserDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }
        #endregion

        #region Constructors
        public skUser()
        {
            Initialise();
        }
        public skUser(skUser CurrentUserOBJ)
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
                            MSG = "Field is Required for new users!";
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
        }

        public void adduser()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                if (Password == ConfirmPassword)
                {                
                    DataClient.Open();
                    DataClient.AddNewUser(this);                  
                    OnUserCreated();
                }
                else
                {
                    throw new Exception("Password's Do not match!");
                }
            }
            finally
            {
                DataClient.Close();
            }
        }
        
        public void UpdateUser(bool IsPasswordReset)
        {

            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();
                DataClient.UpdateUser(this, IsPasswordReset);
                OnUserUpdated();
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
                //DATA.DeleteUser(this);
                OnUserDeleted();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static skUser LoadUser(int UserID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                var Object = DataClient.GetUserObject(UserID).UserObject;
                if (Object != null)
                {
                    return Object;
                }
                else { throw new Exception("No user exists with this ID"); }
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

                    NewNotification.Name = "New User Created.";
                    NewNotification.Description = string.Format("{0} - has been created as a user.", FullName);

                    StringBuilder CreateEmail = new StringBuilder();

                    CreateEmail.AppendLine("A New user has been created with the below details");
                    CreateEmail.AppendLine(Environment.NewLine);
                    CreateEmail.AppendLine("Name:" + FullName);
                    CreateEmail.AppendLine("Username: " + UserName);
                    CreateEmail.AppendLine("Email Address: " + Email);
                    CreateEmail.AppendLine("Contact Numbers:");
                    CreateEmail.AppendLine("                " + "Home: " + HomePhone);
                    CreateEmail.AppendLine("                " + "Work: " + WorkPhone);
                    CreateEmail.AppendLine("                " + "Mobile: " + Mobile);

                    NewNotification.EmailBody = CreateEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "User Updated.";
                    NewNotification.Description = string.Format("{0} - user has been updated.", FullName);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine("User with the below details has been updated");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Name:" + FullName);
                    UpdatedEmail.AppendLine("Username: " + UserName);
                    UpdatedEmail.AppendLine("Email Address: " + Email);
                    UpdatedEmail.AppendLine("Contact Numbers:");
                    UpdatedEmail.AppendLine("                " + "Home: " + HomePhone);
                    UpdatedEmail.AppendLine("                " + "Work: " + WorkPhone);
                    UpdatedEmail.AppendLine("                " + "Mobile: " + Mobile);

                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "User Deleted.";
                    NewNotification.Description = string.Format("{0} - user has been deleted.", FullName);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("User with the below details has been deleted");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Name:" + FullName);
                    DeletedEmail.AppendLine("Username: " + UserName);
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
            if (obj == null || !(obj is skUser))
                return false;
            return ((skUser)obj).UserID == 0 ? ((skUser)obj) == this : ((skUser)obj).UserID == this.UserID;
        }

        public override int GetHashCode()
        {
            return this.UserID == 0 ? base.GetHashCode() : this.UserID.GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }

        public void OpenNotification()
        {
            //Do something here
        }


        #endregion
    }
}
