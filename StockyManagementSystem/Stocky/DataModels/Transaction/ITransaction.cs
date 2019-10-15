using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.DataModels.Transaction
{
    public interface ITransaction
    {
         int ID { get; set; }
         Data.TransactionType TransactionType { get; set; }
         DateTime? TransactionTime { get; set; }

         decimal? Amount { get; set; }

         string Title { get; set; }

         string Description { get; set; }

         string SearchString { get; }

         void OpenTransaction();
         
         

    }
}
