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
using Stocky.Session;


namespace Stocky.ViewModels
{
    public class NewStockViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Events
        public event EventHandler<EventArgs> TabClosed;
        #endregion
        protected virtual void OnTabClosed()
        {
            TabClosed?.Invoke(this,new EventArgs());
            this.Close();
        }

        #region Fields
        #endregion

        #region Properties
        private skStock _StockDetails;

        public skStock StockDetails
        {
            get { _StockDetails.Validate(); return _StockDetails; }
            set
            {

                _StockDetails = value;
                _StockDetails.Validate();
                NotifyPropertyChanged("StockDetails");
            }
        }

        public Data.Interfaces.ITransaction Transaction { get; set; }

        public ObservableCollection<skvendors> VendorList { get; set; }

        public ObservableCollection<skCategory> CategoryList { get; set; }

        public ObservableCollection<skValueBands> ValueBandList { get; set; }
        #endregion

        #region Constructors
        public NewStockViewModel()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

                StockDetails = new skStock();
                VendorList = new ObservableCollection<skvendors>(skvendors.VendorList());
                CategoryList = new ObservableCollection<skCategory>(skCategory.CategoryList());
                ValueBandList = new ObservableCollection<skValueBands>(skValueBands.ValueBandList());
                _SubmitStock = new Command(AddNewStockItem);

               
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
        private Command _SubmitStock;
        //Properties
        public Command SubmitStock { get { return _SubmitStock; } }
        #endregion

        #region Command Methods
        public void AddNewStockItem()
        {
            try
            {   if (Transaction == null)
                    StockDetails.Add(CurrentSession.CurrentUserObject.UserID);             
                else
                    StockDetails.AddToExistingPurchase(Transaction.ID, CurrentSession.CurrentUserObject.UserID);
                    
                OnTabClosed();
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
