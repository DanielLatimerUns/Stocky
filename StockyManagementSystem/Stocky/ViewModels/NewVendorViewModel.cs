using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MVVM_Framework;
using Stocky.Data;
using System.ComponentModel;


namespace Stocky.ViewModels
{
    public class NewVendorViewModel : ViewModelBase,Interfaces.IViewModelTabItem
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
                NotifyPropertyChanged("VendorObj");
            }
        }

        private skAddresses _AddressObj;

        public skAddresses AddressObj
        {
            get { return _AddressObj; }
            set
            {
                _AddressObj = value;
                NotifyPropertyChanged("AddressObj");
            }
        }

        private bool _IsExistingAdress;

        public bool IsExistingAdress
        {
            get { return _IsExistingAdress; }
            set
            {
                _IsExistingAdress = value;
                NotifyPropertyChanged("IsExistingAdress");
            }
        }

        public ObservableCollection<skAddresses> AddressesList { get; set; }
        #endregion

        #region Constructors
        public NewVendorViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
                _VendorObj = new skvendors();
                _AddressObj = new skAddresses();

                AddressesList = new ObservableCollection<skAddresses>(skAddresses.GetAddressList());
                InitialiseCommands();
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
        private Command _SubmitNewVendor;

        //Properties
        public Command SubmitNewVendor { get { return _SubmitNewVendor; } }
        #endregion

        #region Command Methods
        private void InitialiseCommands()
        {
            try
            {
                _SubmitNewVendor = new Command(SubmitNewVendorMethod);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void SubmitNewVendorMethod()
        {
            try
            {                         
               if (_IsExistingAdress == false)
               {                        
                    VendorObj.Add(AddressObj);                                      
                    this.Close();                     
               }
               else
               {
                   VendorObj.AddresseID = AddressObj.AddressID;
                   VendorObj.Add();
                    this.Close();
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
