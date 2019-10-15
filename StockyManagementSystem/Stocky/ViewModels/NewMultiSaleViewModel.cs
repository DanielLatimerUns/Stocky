using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using System.Windows;
using System.Collections.ObjectModel;
using Stocky.UI;
using System.ComponentModel;
using Stocky.Session;

namespace Stocky.ViewModels
{
     public class NewMultiSaleViewModel : ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties
        private skSales _TransactionDetails;
        public skSales TransactionDetails
        {
            get { _TransactionDetails.Validate(); return _TransactionDetails; }
            set
            {
                _TransactionDetails = value;
                NotifyPropertyChanged("TransactionDetails");
            }
        }

        private skPerson _SelectedPersonOBJ;
        public skPerson SelectedPersonOBJ
        {
            get { return _SelectedPersonOBJ; }
            set
            {
                _SelectedPersonOBJ = value;
                NotifyPropertyChanged("SelectedPersonOBJ");
            }
        }

        private decimal _StockItemSaleValue;

        public decimal StockItemSaleValue
        {
            get { return _StockItemSaleValue; }
            set
            {
                _StockItemSaleValue = value;
                NotifyPropertyChanged("StockItemSaleValue");
                _AddStocktoSale.CanExecute = _StockItemSaleValue > 0 ? true : false;
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

                if (StockSaleList.Contains(SelectedStock))
                {
                    _RemoveStockFromSale.CanExecute = true;
                }
                else
                {
                    _RemoveStockFromSale.CanExecute = false;
                }

                if (StockItemSaleValue > 0 && StockList.Contains(SelectedStock))
                {
                    _AddStocktoSale.CanExecute = true;
                }
                else
                {
                    _AddStocktoSale.CanExecute = false;
                }            
            }
        }

        public ObservableCollection<skPerson> PersonList { get; set; }

        public ObservableCollection<skStock> StockList { get; set; }

        public ObservableCollection<skStock> StockSaleList { get; set; }
        #endregion

        #region Constructors
        public NewMultiSaleViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _SelectedStock = new skStock();
                _SubmitSale = new Command(SubmitNewSaleMethod);
                _AddStocktoSale = new Command(AddStockToSaleMethod, false);
                _RemoveStockFromSale = new Command(RemoveStockFromSaleMethod, false);
                _ClearStockFromSale = new Command(ClearStockFromSaleMethod);
                _AddNewPersonCommand = new Command(AddNewPersonMethod);
                TransactionDetails = new skSales();
                TransactionDetails.SaleDate = DateTime.Now;
                TransactionDetails.Amount = 0;
                PersonList = new ObservableCollection<skPerson>(skPerson.GetPersonList());
                StockList = new ObservableCollection<skStock>(_SelectedStock.GetOrphanedStockList(TransactionType.Sale));
                StockSaleList = new ObservableCollection<skStock>();

                var InitialStockOBJ = ObjectMessenger.FindObject("STOCKOBJ") as skStock;
                TransactionDetails.Amount += InitialStockOBJ.SaleValue;
                StockSaleList.Add(InitialStockOBJ);
                StockList.Remove(InitialStockOBJ);

                TransactionDetails.Title = "Sale of " + InitialStockOBJ.Name;
                TransactionDetails.Description = InitialStockOBJ.Description;
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }  
        #endregion

        #region Commands
        //Fields
        private Command _SubmitSale;
        private Command _AddStocktoSale;
        private Command _RemoveStockFromSale;
        private Command _ClearStockFromSale;
        private Command _AddNewPersonCommand;
        //Properties
        public Command Submitsale { get { return _SubmitSale; } }
        public Command AddStockToSale { get { return _AddStocktoSale; } }
        public Command RemoveStockFromSale { get { return _RemoveStockFromSale; } }
        public Command ClearStockFromSale { get { return _ClearStockFromSale; } }
        public Command AddNewPersonCommand { get { return _AddNewPersonCommand; } }
        #endregion

        #region Command Methods
        private void SubmitNewSaleMethod()
        {
            try
            {
                foreach(skStock s in StockSaleList)
                {
                    TransactionDetails.StockList.Add(s);
                }
                    TransactionDetails.PersonObj = SelectedPersonOBJ;        
                    TransactionDetails.AddNewSale(CurrentSession.CurrentUserObject.UserID);
                    this.Close();                      
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void AddStockToSaleMethod()
        {
            try
            {
                if (SelectedStock != null)
                {
                    SelectedStock.SaleValue = StockItemSaleValue;
                    TransactionDetails.Amount += SelectedStock.SaleValue;
                    StockSaleList.Add(_SelectedStock);                                
                    StockList.Remove(StockList.FirstOrDefault(x => x.Stockid == SelectedStock.Stockid));
                    SelectedStock = new skStock();
                    StockItemSaleValue = 0;
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void RemoveStockFromSaleMethod()
        {
            try
            {
                if (SelectedStock != null && StockSaleList.Contains(SelectedStock))
                {
                    TransactionDetails.Amount -= SelectedStock.SaleValue;
                    StockList.Add(SelectedStock);
                    StockSaleList.Remove(SelectedStock);
                    SelectedStock = new skStock();
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void ClearStockFromSaleMethod()
        {
            try
            {
                if (StockSaleList.Count() > 0)
                {
                    foreach (var item in StockSaleList)
                    {
                        StockList.Add(item);  
                    }
                    StockSaleList.Clear();
                    TransactionDetails.Amount = 0;
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }

        }

        private void AddNewPersonMethod()
        {
            NewPersonViewModel _NewPersonViewModel = new NewPersonViewModel();
            _NewPersonViewModel.TabClosed += _NewPersonViewModel_TabClosed;
            Enviroment.LoadTab(_NewPersonViewModel);
        }

        private void _NewPersonViewModel_TabClosed(object sender, EventArgs e)
        {
            RefreshPersonListMethod();
        }

        private void RefreshPersonListMethod()
        {
            try
            {
                PersonList.Clear();

                foreach(skPerson P in skPerson.GetPersonList())
                {
                    PersonList.Add(P);
                }
            }
            catch(Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion
    }
}
