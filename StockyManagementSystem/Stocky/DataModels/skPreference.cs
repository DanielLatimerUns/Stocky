using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.DataAccesse;

namespace Stocky.Data
{
    public class skPreference
    {
        #region Properties
        public int ID { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public object Value { get; set; }
        #endregion
    }
}
