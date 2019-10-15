using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.DataAccesse;
using System.ComponentModel;
using System.Reflection;

namespace Stocky.Data
{
    public class skValueBands : Validation, IDataErrorInfo
    {

        #region Fields
        public DataFunctions DATA;
        #endregion

        #region Properties
        public int ID { get; set; }

        public decimal? LowValue { get; set; }

        public decimal? HighValue { get; set; }

        public string Description { get; set; }
        #endregion

        #region Constructors
        public skValueBands()
        {
            DATA = new DataFunctions();
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
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skValueBands))
                return false;
            return ((skValueBands)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public List<skValueBands> ValueBandList(bool filter)
        {
            if (filter != true)
            {
                return DATA.GetValueBandList().ToList();
            }
            else
            {
                return DATA.GetValueBandList(this).ToList();
            }
        }
        #endregion

    }
}
