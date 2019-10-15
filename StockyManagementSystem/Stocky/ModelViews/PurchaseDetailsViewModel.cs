using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using System.Windows;
using MVVM_Framework;
using System.ComponentModel;

namespace Stocky.ModelViews
{
    public class PurchaseDetailsViewModel : Observable
    {
        private skPurchase _PurchaseObj;
        private skvendors _VendorObj;
        private DataFunctions Data;
        private skAddresses _Addressobj;

        public PurchaseDetailsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            Data = new DataFunctions();
            _PurchaseObj = Data.GetPurchaseDetails(StaticDataReposityory.SelectedSaleID);
            if (_PurchaseObj.VendorId != null)
            {
                _VendorObj = Data.GetVendorDetails(Convert.ToInt32(_PurchaseObj.VendorId));
                _Addressobj = Data.GetAddressObject(Convert.ToInt32(_VendorObj.AddresseID));
                
            }
        }

        public skPurchase PurchaseObj
        {
            get { return _PurchaseObj; }
            set
            {
                _PurchaseObj = value;
                NotifyPropertyChanged("Putchase");
            }
        }
        
        public skvendors VendorObj
        {
            get { return _VendorObj; }
            set
            {
                _VendorObj = value;
                NotifyPropertyChanged("VendorObj");
            }
        }

          public skAddresses Addressobj
        {
            get { return _Addressobj; }
            set
            {
                _Addressobj = value;
                NotifyPropertyChanged("Addressobj");
            }
        }

    }

    

}
