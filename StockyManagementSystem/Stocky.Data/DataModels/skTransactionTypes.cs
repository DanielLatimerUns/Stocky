using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.DataAccesse;

namespace Stocky.Data
{
    public class skTransactionTypes : skTransactionBase
    {
        #region Properties
        public int? ID { get; set; }

        public string TypeName { get; set; }

        public string TypeDescription { get; set; }
        #endregion
    }
}
