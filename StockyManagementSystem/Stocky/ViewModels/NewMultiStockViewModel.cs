using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using Stocky;
using Stocky.UI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Stocky.Utility;
using Stocky.Session;

namespace Stocky.ViewModels
{
    public class NewMultiStockViewModel: ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Fields 
        private Notification Notification;
        #endregion

        #region Properties
        private skStock _StockDetails;

        public skStock StockDetails
        {
            get { AddStock.CanExecute = _StockDetails.Validate(); return _StockDetails; }
            set
            {
                _StockDetails = value;
                NotifyPropertyChanged("StockDetails");
            }
        }

        private skPurchase _PurchaseDetails;

        public skPurchase PurchaseDetails
        {
            get { submitNewStockItem.CanExecute = _PurchaseDetails.Validate(); return _PurchaseDetails; }
            set
            {
                _PurchaseDetails = value;
                NotifyPropertyChanged("PurchaseDetails");
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

                if (StockToAdd.Contains(SelectedStock))
                {
                    _RemoveStock.CanExecute = true;
                }
                else
                {
                    _RemoveStock.CanExecute = false;
                }
            }
        }

        public ObservableCollection<skvendors> VendorList { get; set; }

        public ObservableCollection<skCategory> CategoryList { get; set; }

        public ObservableCollection<skValueBands> ValueBandList { get; set; }

        public ObservableCollection<skStock> StockToAdd { get; set; }
        #endregion

        #region Constructors
        public NewMultiStockViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                StockDetails = new skStock();
                PurchaseDetails = new skPurchase();    
              
                _AddStock = new Command(Add);
                _RemoveStock = new Command(Remove,false);
                _ClearStock = new Command(Clear);
                _SubmitNewStockItem = new Command(SubmitData);

                VendorList = new ObservableCollection<skvendors>(skvendors.VendorList());
                CategoryList = new ObservableCollection<skCategory>(skCategory.CategoryList());
                ValueBandList = new ObservableCollection<skValueBands>(skValueBands.ValueBandList());
                StockToAdd = new ObservableCollection<skStock>();

                Notification = new Notification();
                PurchaseDetails.Amount = 0;
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
        private Command _AddStock;
        private Command _RemoveStock;
        private Command _ClearStock;
        private Command _SubmitNewStockItem;

        //Properties
        public Command AddStock { get { return _AddStock; } }
        public Command RemoveStock { get { return _RemoveStock; } }
        public Command ClearStock { get { return _ClearStock; } }
        public Command submitNewStockItem { get { return _SubmitNewStockItem; } }
        #endregion

        #region Command Methods
        private void Add()
        {
            try
            { 
                if (StockDetails.IsObjectValid)
                {
                        StockToAdd.Add(StockDetails);
                        PurchaseDetails.Amount += Convert.ToDecimal(_StockDetails.purchasedvalue);
                        PurchaseDetails.TotalValue += Convert.ToDecimal(_StockDetails.purchasedvalue);
                        StockDetails = new skStock(CurrentSession.CurrentUserObject);
                        PurchaseDetails.Validate(); ;        
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void Remove()
        {
            try
            {
                if (SelectedStock != null && StockToAdd.Contains(SelectedStock))
                {
                    PurchaseDetails.Amount -= Convert.ToDecimal(SelectedStock.purchasedvalue);
                    PurchaseDetails.TotalValue = 0;
                    var stocktoremove = SelectedStock;
                    StockToAdd.Remove(SelectedStock);
                   
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void Clear()
        {
            try
            {
                if (StockToAdd.Count() > 0)
                {
                    PurchaseDetails.Amount = 0;
                    PurchaseDetails.TotalValue = 0;
                    StockToAdd.Clear();
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void SubmitData()
        {
            try
            {

                StockDetails.StockCreated += Notification.OnNotificationReceived;

                foreach (skStock item in StockToAdd)
                {
                    _StockDetails.StockList.Add(item);
                }

                StockDetails.AddMultiWithPurchase(PurchaseDetails, CurrentSession.CurrentUserObject.UserID);

                this.Close();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion
    }
}
