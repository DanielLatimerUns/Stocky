using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MVVM_Framework;
using Stocky.Utility;
using System.Reflection;
using Stocky.Data.Interfaces;

namespace Stocky.Data
{
     public class skStockHistory : DataModelBase, IDataErrorInfo,IDataModel
    {
        #region Fields

        #endregion

        #region Properties
        public int ID { get; set; }
        public DateTime? Submited { get; set; }

        public string Status { get; set; }

        public string SubmitedBy { get; set; }

        public decimal? Amount { get; set; }

        public string SearchString { get { return Status + Amount; } }
        #endregion

        #region Constructors
        public skStockHistory()
        {
            Initialise();
        }
        public skStockHistory(skUser CurrentUserOBJ)
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
            this.ValiationObject = this;
        }
        public List<skStockHistory> GetStockHistory(long StockID)
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                var Returnlist = DataClient.GetStockHistory(StockID).StockHistory;

                return Returnlist;
            }
            finally
            {
                DataClient.Close();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is skStockHistory)
                return false;
            return ((skStockHistory)obj).ID == 0 ? ((skStockHistory)obj) == this : ((skStockHistory)obj).ID == this.ID; 
        }

        public override int GetHashCode()
        {
            return this.ID == 0 ? base.GetHashCode() : this.ID.GetHashCode();
        }

        #endregion
    }
}
