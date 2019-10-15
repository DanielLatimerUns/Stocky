using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.Data.Interfaces;
using Stocky.ViewModels.Interfaces;
using Stocky.Views;
using MVVM_Framework;

namespace Stocky.ViewModels
{
    public static class UIExtensions
    {
        public static void LoadTransaction(this ITransaction Object)
        {
            Stocky.ViewModels.BuissnessHelpers.LoadTransaction(Object);
        }

        public static void Close( this IViewModelTabItem Tab)
        {
            Stocky.UI.Enviroment.CloseTab(Tab);
        }

        public static void Load(this skPerson Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("PersonObject", Object);
            UI.Enviroment.LoadNewTab("PersonDetailsView", Object.FullName);
        }
        public static void Load(this skStock Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("SelectedStockItem", Object);
            UI.Enviroment.LoadNewTab("StockDetailsView",Object.Name);
        }
        public static void Load(this skCategory Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("CATEGORYOBJ", Object);
            UI.Enviroment.LoadNewTab("CategoryDetailsView",Object.Name);
        }
        public static void Load(this skSales Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("TRANOBJ", Object);
            UI.Enviroment.LoadNewTab("SalesDetailsView",Object.Title);
        }
        public static void Load(this skPurchase Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("TRANOBJ", Object);
            UI.Enviroment.LoadNewTab("PurchaseDetailsView",Object.Title);
        }
        public static void CreateRefund(this skStock Object)
        {
            ObjectMessenger OM = new ObjectMessenger();

            OM.Send("STOCKITEM", Object);
            Stocky.UI.Enviroment.LoadNewTab("RefundView|");
        }
    }
}
