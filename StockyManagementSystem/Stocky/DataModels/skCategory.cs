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
using Stocky.DataAccesse;
using Stocky.Utility;


namespace Stocky.Data
{
    public class skCategory :Validation,IDataErrorInfo,INotification
    {
        #region Fields
        private DataFunctions DATA;
        #endregion

        #region Properties
        public int StockTypeID { get; set; }
        [RequiredData]
        public string Name { get; set; }
        [RequiredData]
        public  string Description { get; set; }

        public Notification NewNotification { get; set; }
        #endregion

        #region Events

        public event EventHandler<NewNotificationEventArgs> CategoryCreated;

        public virtual void OnCategoryCreated()
        {
                CreateNotification(EventActionType.Created);
                CategoryCreated?.Invoke(this, new NewNotificationEventArgs(this));         
        }

        public event EventHandler<NewNotificationEventArgs> CategoryUpdated;

        public virtual void OnCategoryUpdated()
        {
            CreateNotification(EventActionType.Updated);
            CategoryUpdated?.Invoke(this, new NewNotificationEventArgs(this));
        }

        public event EventHandler<NewNotificationEventArgs> CategoryDeleted;

        public virtual void OnCategoryDeleted()
        {
            CreateNotification(EventActionType.Deleted);
            CategoryDeleted?.Invoke(this, new NewNotificationEventArgs(this));
        }

        #endregion

        #region Constructors
        public skCategory()
        {

            ValiationObject = this;
            DATA = new DataFunctions();
            NewNotification = new Notification();
            CategoryCreated += NewNotification.OnNotificationReceived;
            CategoryUpdated += NewNotification.OnNotificationReceived;
            
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
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skCategory))
                return false;
            return ((skCategory)obj).StockTypeID == this.StockTypeID;
        }

        public override int GetHashCode()
        {
            return this.StockTypeID.GetHashCode();
        }

        public bool AddNew()
        {    
            if(Name != null)
            {
                DATA.AddNewCategory(this);
                OnCategoryCreated();
                return true;
            }
            return false;
        }

        public bool Update()
        {
            if(IsObjectValid)
            {
                DATA.UpdateCategory(this);
                OnCategoryUpdated();
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
                //DATA.DeleteCategory(this);
                OnCategoryDeleted();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<skCategory> CategoryList()
        {
            return DATA.GetCategoryList().ToList();
        }

        public List<skCategory> CategoryList(bool UseFilter)
        {
            return DATA.GetCategoryList(this).ToList();
        }

        public void CreateNotification(EventActionType eventActionType)
        {
            switch (eventActionType)
            {
                case EventActionType.Created:

                    NewNotification.Name = "New Category Created";
                    NewNotification.Description = string.Format("{0} - has been created as a category.", Description);

                    StringBuilder CreatedEmail = new StringBuilder();

                    CreatedEmail.AppendLine("A New category has been created with the below details");
                    CreatedEmail.AppendLine(Environment.NewLine);
                    CreatedEmail.AppendLine("Name:" + Name);
                    CreatedEmail.AppendLine("Description: " + Description);

                    NewNotification.EmailBody = CreatedEmail.ToString();

                    break;
                case EventActionType.Updated:

                    NewNotification.Name = "Category Updated";
                    NewNotification.Description = string.Format("Category has been updated: {0}", Description);

                    StringBuilder UpdatedEmail = new StringBuilder();

                    UpdatedEmail.AppendLine("Category has been updated with the below details");
                    UpdatedEmail.AppendLine(Environment.NewLine);
                    UpdatedEmail.AppendLine("Name:" + Name);
                    UpdatedEmail.AppendLine("Description: " + Description);

                    NewNotification.EmailBody = UpdatedEmail.ToString();

                    break;
                case EventActionType.Deleted:

                    NewNotification.Name = "Category Deleted";
                    NewNotification.Description = string.Format("Category has been deleted: {0}", Description);

                    StringBuilder DeletedEmail = new StringBuilder();

                    DeletedEmail.AppendLine("Category with the below details has been deleted.");
                    DeletedEmail.AppendLine(Environment.NewLine);
                    DeletedEmail.AppendLine("Name:" + Name);
                    DeletedEmail.AppendLine("Description: " + Description);

                    NewNotification.EmailBody = DeletedEmail.ToString();

                    break;
            }
        }

        public void OpenNotification()
        {
            UI.Enviroment.LoadNewTab("CategoryManager");
        }

        #endregion
    }
}
