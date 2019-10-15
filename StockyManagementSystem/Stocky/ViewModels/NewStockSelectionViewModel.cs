using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTools.ViewModels;
using System.ComponentModel;

namespace Stocky.ViewModels
{
    public class NewStockSelectionViewModel :ViewModelBase
    {
        #region Properties
        private bool _IsSingle = false;

        public bool IsSingle
        {
            get { return _IsSingle; }
            set
            {
                _IsSingle = value;
                RaisePropertyChanged("IsSingle");
            }
        }

        private bool _IsSingleWP = false;

        public bool IsSingleWP
        {
            get { return _IsSingleWP; }
            set
            {
                _IsSingleWP = value;
                RaisePropertyChanged("IsSingleWP");
            }
        }

        private bool _IsMultiWP = false;

        public bool IsMultiWP
        {
            get { return _IsMultiWP; }
            set
            {
                _IsMultiWP = value;
                RaisePropertyChanged("IsMultiWP");
            }
        }
        #endregion

        #region Constructors
        public NewStockSelectionViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _Continue = new Command(SelectModule);
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
        private Command _Continue;

        //Properties
        public Command Continue { get { return _Continue; } }
        #endregion

        #region Command Methods
        private void SelectModule()
        {
            try
            {
                if (IsSingle) { UI.Enviroment.LoadNewTab("NewStockView| New Stock"); }
                if (IsSingleWP) { UI.Enviroment.LoadNewTab("NewMultiStockView|New Stock"); }
                if (IsMultiWP) { UI.Enviroment.LoadNewTab("NewMultiStockView|New Stock"); }

                UI.Enviroment.CloseWindow("NewStockSelectionView");
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
