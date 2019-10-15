using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.Data.Interfaces;
using Stocky.ViewModels;
using MVVM_Framework;

namespace Stocky.ViewModels
{
    public static class BuissnessHelpers
    {
        public static void LoadTransaction(ITransaction TransactionObject)
        {
            ObjectMessenger om = new ObjectMessenger();

            switch (TransactionObject.TransactionType)
            {
                case TransactionType.Purchase:
                    if (!string.IsNullOrEmpty(TransactionObject.Title))
                    {
                        om.Send("TRANOBJ", (skPurchase)TransactionObject);
                               UI.Enviroment.LoadNewTab("PurchaseDetailsView|Purchase Details");
                    }
                    else
                    {
                        throw new Exception("No Purchase with this ID can be found.");
                    }
                    break;
                case TransactionType.Sale:
                    if (!string.IsNullOrEmpty(TransactionObject.Title))
                    {
                        om.Send("TRANOBJ", (skSales)TransactionObject);
                                 UI.Enviroment.LoadNewTab("SalesDetailsView|Sale Details");
                    }
                    else
                    {
                        throw new Exception("No Sale with this ID can be found.");
                    }
                    break;
            }
        }
    }
}
