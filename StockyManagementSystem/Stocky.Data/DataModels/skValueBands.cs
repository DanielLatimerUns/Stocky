using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using System.ComponentModel;
using System.Reflection;
using Stocky.Data.Interfaces;

namespace Stocky.Data
{
    public class skValueBands : DataModelBase, IDataErrorInfo,IDataModel
    {

        #region Fields
        
        #endregion

        #region Properties
        public int ID { get; set; }

        public decimal? LowValue { get; set; }

        public decimal? HighValue { get; set; }

        public string Description { get; set; }

        public string SearchString { get { return LowValue + HighValue + ID + Description; } }
        #endregion

        #region Constructors
        public skValueBands()
        {
  
        }
        public skValueBands(skUser CurrentUserOBJ)
            :base(CurrentUserOBJ)
        {
            
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
            return ((skValueBands)obj).ID == 0 ? ((skValueBands)obj) == this : ((skValueBands)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return this.ID == 0 ? base.GetHashCode() : this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return Description + "High Value: " + HighValue + " Low Value: " + LowValue;
        }

        public static List<skValueBands> ValueBandList()
        {
            Proxies.AppDataClient DataClient = new Proxies.AppDataClient();
            try
            {
                DataClient.Open();

                List<skValueBands> retunlist = DataClient.GetValueBandList().ValueBandList;

                if(retunlist.Count != 0)
                {
                    return retunlist;
                }
                else
                {
                    throw new Exception("No Records To Return");
                }   
            }
            finally
            {
                DataClient.Close();
            }
        }
        #endregion

    }
}
