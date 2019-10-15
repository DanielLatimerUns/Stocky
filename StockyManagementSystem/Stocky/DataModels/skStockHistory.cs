using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MVVM_Framework;
using Stocky.DataAccesse;
using Stocky.Utility;
using System.Reflection;

namespace Stocky.Data
{
     public class skStockHistory : Validation, IDataErrorInfo
    {
        #region Fields
        private DataFunctions DATA;
        #endregion

        #region Properties
        public DateTime? Submited { get; set; }

        public string Status { get; set; }

        public string SubmitedBy { get; set; }

        public decimal? Amount { get; set; }
        #endregion

        #region Constructors
        public skStockHistory()
        {
            DATA = new DataFunctions();
            this.ValiationObject = this;

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
        public List<skStockHistory> GetStockHistory(long StockID)
        {
            return DATA.GetStockHistory(StockID).ToList();
        }
        #endregion
    }
}
