using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using MVVM_Framework;
using Stocky.Data;
using Stocky.UI;
using System.ComponentModel;
using Stocky.Session;

namespace Stocky.ViewModels
{
    public class NewPurchaseViewModel : ViewModelBase ,ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties
        private skPurchase _PurchaseDetails;

        public skPurchase PurchaseDetails
        {
            get { _PurchaseDetails.Validate(); return _PurchaseDetails; }
            set
            {
                _PurchaseDetails = value;
                NotifyPropertyChanged("PurchaseDetails");
            }
        }

        public ObservableCollection<skvendors> VendorList { get; set; }
        #endregion

        #region Constructors
        public NewPurchaseViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                PurchaseDetails = new skPurchase();
                _SubmitPurchase = new Command(submitpurchase);
                VendorList = new ObservableCollection<skvendors>(skvendors.VendorList());
                
            
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
        private Command _SubmitPurchase;

        //Properties
        public Command SubmitPurchase { get { return _SubmitPurchase; } }      
        #endregion

        #region Command Methods
        private void submitpurchase()
        {           
            try
            {
                PurchaseDetails.Add();
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
