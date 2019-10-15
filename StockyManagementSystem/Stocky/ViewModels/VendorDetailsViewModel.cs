using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using MVVM_Framework;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Stocky.ViewModels
{
    public class VendorDetailsViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields


        #endregion

        #region Properties

        private skvendors _VendorObj;

        public skvendors VendorObj
        {
            get { _VendorObj.Validate(); return _VendorObj; }
            set
            {
                _VendorObj = value;
                _VendorObj.Validate();
                NotifyPropertyChanged("VendorObj");
            }
        }

        public ObservableCollection<skAddresses> AddressList { get; set; }

        #endregion

        #region Constuctors
        public VendorDetailsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                VendorObj = ObjectMessenger.FindObject("VENDOROBJ");

                _SaveChanges = new Command(Submit);
                
                AddressList = new ObservableCollection<skAddresses>(_VendorObj.GetAddressses());
                
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        #endregion

        #region Commands

        private Command _SaveChanges;

        public Command SaveChanges { get { return _SaveChanges; } }
                    
        #endregion

        #region Command Methods
        
        private void Submit()
        {
            _VendorObj.Update();
        }

        #endregion

    }
}
