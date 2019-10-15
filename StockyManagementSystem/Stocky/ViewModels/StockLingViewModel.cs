using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MVVM_Framework;
using Stocky.Data;
using System.ComponentModel;
using Stocky.UI.Dialogs;
using Stocky.Data.Interfaces;
using Stocky.Session;

namespace Stocky.ViewModels
{
    public class StockLingViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        private ITransaction TransactionOBJ;
        #endregion

        #region Properties
        private skStock _StockObj;

        public skStock StockObj
        {
            get { return _StockObj; }
            set
            {
                _StockObj = value;
                NotifyPropertyChanged("StockObj");
            }
        }

        public ObservableCollection<skStock> StockList { get; set; }
        #endregion

        #region Constructors
        public StockLingViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _StockObj = new skStock();
                
                TransactionOBJ = ObjectMessenger.FindObject("TRANLEDGEROBJ") as ITransaction;       
                StockList = new ObservableCollection<skStock>(_StockObj.GetOrphanedStockList(TransactionOBJ.TransactionType));
      
                _Submit = new Command(CreateLink);
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
        private Command _Submit;

        //Properties
        public Command Submit { get { return _Submit; } }
        #endregion

        #region Command Methods
        public void CreateLink ()
        {
            try
            {
                UI.Enviroment.CloseWindow("StockLingView");

                if (TransactionOBJ.TransactionType == TransactionType.Purchase)
                {
                    decimal result;
                    bool hasvalue = decimal.TryParse(QeustionBox.Show("Purchased Value", "If you would like to change the purchased value please enter it now."), out result);
                    if (hasvalue)
                        _StockObj.purchasedvalue = result;
                }
                if(TransactionOBJ.TransactionType == TransactionType.Sale)
                {
                    decimal result;
                    bool hasvalue = decimal.TryParse(QeustionBox.Show("Sale Value", "If you would like to change the purchased value please enter it now."), out result);
                    if (hasvalue)
                        _StockObj.SaleValue = result;
                }
                _StockObj.LinktoTransaction(TransactionOBJ, CurrentSession.CurrentUserObject.UserID);              
              
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
