using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using System.Windows;
using MVVM_Framework;

namespace Stocky.ModelViews
{
    public class SaleDetailViewModel : Observable
    {
        private skSales _Salesobj;
        private skPurchase _Purchaseobj;
        private skAddresses _Addressobj;
        private DataFunctions Data;
        private skPerson _Personobj;

        public SaleDetailViewModel()
        {  
            _Salesobj = new skSales();
            _Purchaseobj = new skPurchase();
            Data = new DataFunctions();

            Salesobj = Data.GetSalesDetails(StaticDataReposityory.SelectedSaleID);
            if (Salesobj.AddressId != null)
            {
                Addressobj = Data.GetAddressObject(Convert.ToInt32(Salesobj.AddressId));
               // Personobj = Data.GetPersonObject(Convert.ToInt32(Addressobj.PersonID));
            }
            
        }

        public skPerson Personobj
        {
            get { return _Personobj; }
            set
            {
                _Personobj = value;
                NotifyPropertyChanged("Personobj");
            }
        }

        public skSales Salesobj
        {
            get { return _Salesobj; }
            set
            {
                _Salesobj = value;
                NotifyPropertyChanged("Salesobj");
            }
        }
        public skPurchase Purchaseobj
        {
            get { return _Purchaseobj; }
            set
            {
                _Purchaseobj = value;
                NotifyPropertyChanged("Purchaseobj");
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
