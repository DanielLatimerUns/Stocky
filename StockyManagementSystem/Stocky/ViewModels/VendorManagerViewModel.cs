using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Stocky.Data;
using System.ComponentModel;
using System.Windows;
using MVVM_Framework;
using Stocky.ToolBar;

namespace Stocky.ViewModels
{
   public class VendorManagerViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        ActionBar ToolBar;
        #endregion

        #region Properties
        private skvendors _VendorOBJ;

        public skvendors VendorOBJ
        {
            get { return _VendorOBJ; }
            set
            {
                _VendorOBJ = value;
                NotifyPropertyChanged("VendorOBJ");
            }
        }

        private skvendors _SelectedVendorOBJ;

        public skvendors SelectedVendorOBJ
        {
            get { return _SelectedVendorOBJ; }
            set
            {
                _SelectedVendorOBJ = value;
                NotifyPropertyChanged("SelectedVendorOBJ");
                if (SelectedVendorOBJ != null)
                    Edit.CanExecute = true;
            }
        }

        public ObservableCollection<skvendors> vendorList { get; set; }

        public ObservableCollection<ToolBarButton> Buttons { get; set; }
        #endregion

        #region Constructors
        public VendorManagerViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
                _VendorOBJ = new skvendors();
                _UpdateVendoryList = new Command(GetVendorList);
                _NewVendor = new Command(OpenNewVendor);
                _Edit = new Command(EditVendor,false);
                ToolBar = new ActionBar();
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "Refresh", Command = _UpdateVendoryList });
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "Add New", Command = _NewVendor });
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "Edit", Command = Edit });
                vendorList = new ObservableCollection<skvendors>(skvendors.VendorList());
                Buttons = new ObservableCollection<ToolBarButton>(ToolBar.ButtonList);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Commands
        private Command _UpdateVendoryList;
        private Command _NewVendor;
        private Command _Edit;

        public Command UpdateVendoryList { get { return _UpdateVendoryList; } }
        public Command Edit { get { return _Edit; } }
        public Command NewVendor { get { return _NewVendor; } }
        #endregion

        #region Command Methods
        public void GetVendorList()
        {
            try
            {
                vendorList.Clear();

                foreach (skvendors SV in skvendors.VendorList())
                {
                    vendorList.Add(SV);
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void OpenNewVendor()
        {
            try
            {
                if(SelectedVendorOBJ != null)
                UI.Enviroment.LoadNewTab("NewVendorView");
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void EditVendor()
        {
            try
            {
                ObjectMessenger.Send("VENDOROBJ", SelectedVendorOBJ);
                UI.Enviroment.LoadNewTab("VendorDetailsView| "+SelectedVendorOBJ.VendorsName);
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
