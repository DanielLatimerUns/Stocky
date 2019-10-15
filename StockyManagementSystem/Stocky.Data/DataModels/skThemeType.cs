using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public class skThemeType
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ThemeXML { get; set; }

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is skThemeType))
                return false;
            return ((skThemeType)obj).ID == 0 ? ((skThemeType)obj) == this : ((skThemeType)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {

            return this.ID == 0 ? base.GetHashCode() : this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return ThemeXML;
        }

        #endregion
    }

}
