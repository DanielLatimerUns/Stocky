using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Stocky.Data.Interfaces;
using Stocky.Session;


namespace Stocky.ViewModels
{
    public class TransactionDetailsViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Properties

        private NewStockViewModel _NewStockViewModel;

        private ITransaction _TransactionObject;

        public ITransaction TransactionObject
        {
            get { return _TransactionObject; }
            set
            {
                _TransactionObject = value;               
                NotifyPropertyChanged("TransactionObject");
            }
        }


        private skPurchase _PurchaseOBJ;

        public skPurchase PurchaseOBJ
        {
            get { return _PurchaseOBJ; }
            set
            {
                _PurchaseOBJ = value;
                NotifyPropertyChanged("PurchaseOBJ");
            }
        }

        public skSales _SaleOBJ;

        public skSales SaleOBJ
        {
            get { return _SaleOBJ; }
            set
            {
                _SaleOBJ = value;
                NotifyPropertyChanged("SaleOBJ");
            }
        }

        private skStock _SelectedStock;

        public skStock SelectedStock
        {
            get { return _SelectedStock; }
            set
            {
                _SelectedStock = value;
                NotifyPropertyChanged("SelectedStock");
            }
        }

    
        public ObservableCollection<skStock> StockList { get; set; }

        public ObservableCollection<skvendors> VendorList { get; set; }
        #endregion

        #region Constructor
        public TransactionDetailsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                
                _TransactionObject = ObjectMessenger.FindObject("TRANOBJ");
                PopulateBasedOnType();
                _Link = new Command(LinkExtraStock);
                _Refresh = new Command(PopulateBasedOnType);          
                _RemoveStockFromTransaction = new Command(RemoveFromTransaction);
                _AddExtraStockCommand = new Command(AddExtraStockMethod);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void _NewStockViewModel_TabClosed(object sender, EventArgs e)
        {
            StockList.Clear();
            switch (TransactionObject.TransactionType)
            {
                case TransactionType.Purchase:
                    PurchaseOBJ.UpdateCurrentObject();
                    foreach (var item in PurchaseOBJ.LinkedStock)
                    {
                        StockList.Add(item);
                    }

                    break;

                case TransactionType.Sale:
                    PurchaseOBJ.UpdateCurrentObject();
                    foreach (var item in SaleOBJ.StockList)
                    {
                        StockList.Add(item);
                    }
                    break;

                    //  case TransactionType.Refund:
            }

        }
        #endregion

        #region Commands
        //Fields
        private Command _Link;
        private Command _Refresh;
        private Command _Update;
        private Command _RemoveStockFromTransaction;
        private Command _AddExtraStockCommand;

        //Properties
        public Command Link { get { return _Link; } }
        public Command Refresh { get { return _Refresh; } }
        public Command Update { get { return _Update; } }
        public Command RemoveStockFromTransaction { get { return _RemoveStockFromTransaction; } }
        public Command AddExtraStockCommand { get { return _AddExtraStockCommand; } }
        #endregion

        #region Command Methods
        private void PopulateBasedOnType()
        {
            try
            {
                switch (TransactionObject.TransactionType)
                {
                    case TransactionType.Purchase:
                         PurchaseOBJ = TransactionObject as skPurchase;
                        _Update = new Command(PurchaseOBJ.Update);
                        StockList = new ObservableCollection<skStock>(PurchaseOBJ.LinkedStock);
                        VendorList = new ObservableCollection<skvendors>(skvendors.VendorList());
                        break;

                    case TransactionType.Sale:
                        SaleOBJ = TransactionObject as skSales;
                        _Update = new Command(SaleOBJ.Update);
                        StockList = new ObservableCollection<skStock>(SaleOBJ.StockList);
                        break;

                  //  case TransactionType.Refund:
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void LinkExtraStock()
        {
            switch(TransactionObject.TransactionType)
            {
                case TransactionType.Purchase:                  
                    try
                    {
                        Stocky.UI.Enviroment.LoadDialog("StockLingView", TransactionObject, "TRANLEDGEROBJ");
                        PurchaseOBJ.UpdateCurrentObject();
                        StockList.Clear();

                        foreach (var item in PurchaseOBJ.LinkedStock)
                        {
                            StockList.Add(item);
                        }
                    }
                    catch (Exception E)
                    {
                        ExepionLogger.Logger.LogException(E);
                        ExepionLogger.Logger.Show(E);
                    }
                    break;
                case TransactionType.Sale:
                    try
                    {
                        Stocky.UI.Enviroment.LoadDialog("StockLingView", TransactionObject, "TRANLEDGEROBJ");
                        SaleOBJ.UpdateCurrentObject();
                        StockList.Clear();

                        foreach (var item in SaleOBJ.StockList)
                        {
                            StockList.Add(item);
                        }
                    }
                    catch (Exception E)
                    {
                        ExepionLogger.Logger.LogException(E);
                        ExepionLogger.Logger.Show(E);
                    }
                    break;
            }
           
        }

        private void RemoveFromTransaction()
        {
            try
            {
                if (TransactionObject.TransactionType == TransactionType.Purchase)
                {
                    
                    SelectedStock.RemoveFromPurchase(CurrentSession.CurrentUserObject.UserID);
                    StockList.Remove(SelectedStock);
                    PurchaseOBJ.UpdateCurrentObject();

                }
                else
                {
                    SelectedStock.RemoveFromSale(CurrentSession.CurrentUserObject.UserID);
                    StockList.Remove(SelectedStock);
                    SaleOBJ.UpdateCurrentObject();

                }
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        private void AddExtraStockMethod()
        {
            try
            {
                _NewStockViewModel = new NewStockViewModel();
                _NewStockViewModel.Transaction = PurchaseOBJ;
                _NewStockViewModel.TabClosed += _NewStockViewModel_TabClosed;

                UI.Enviroment.LoadTab(_NewStockViewModel);
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }


        #endregion
    }
}
