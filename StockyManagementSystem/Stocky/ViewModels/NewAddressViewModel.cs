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

namespace Stocky.ViewModels
{
    public class NewAddressViewModel : ViewModelBase, ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties

        private skAddresses _AddressObject;
        public skAddresses AddressObject
        {
            get { _AddressObject.Validate(); return _AddressObject; }
            set
            {
                _AddressObject = value;
                NotifyPropertyChanged("AddressObject");
            }
        }


        #endregion

        #region Fields

        private skPerson PersonObject;
        private skvendors VendorObject;

        #endregion

        #region Constructors

        public NewAddressViewModel()
        {
            _SubmitNewAddressCommand = new Command(SubmitNewAddressMethod);
            _AddressObject = new skAddresses();

            object LinkObject = ObjectMessenger.FindObject("ADDRESSLINKOBJECT");
            
            if(LinkObject != null)
            {
                Type ObjectType = LinkObject.GetType();

                switch(ObjectType.Name)
                {
                    case "skPerson":
                        PersonObject = LinkObject as skPerson;
                        break;
                    case "skvendors":
                        VendorObject = LinkObject as skvendors;
                        break;
                }
            }          
        }

        #endregion
        #region Commands

        private Command _SubmitNewAddressCommand;
        public Command SubmitNewAddressCommand { get { return _SubmitNewAddressCommand; } }

        #endregion

        #region Command Methods

        public void SubmitNewAddressMethod()
        {
            try
            {
                if (PersonObject != null)
                {
                    AddressObject.Add(PersonObject.pID,typeof(skPerson));
                    this.Close();
                }
                else if (VendorObject != null)
                {
                    AddressObject.Add(PersonObject.pID, typeof(skvendors));
                    this.Close();
                }
                else
                {
                    AddressObject.Add();
                    this.Close();
                }
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
