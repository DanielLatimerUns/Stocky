using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using System.Collections.ObjectModel;
using Stocky;
using Stocky.UI;
using System.ComponentModel;
using Stocky.Utility;
using Stocky.Adapters;
using Stocky.ToolBar;

namespace Stocky.ViewModels
{
    public class NewPersonViewModel : ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {

        #region Events
        public event EventHandler<EventArgs> TabClosed;

        protected virtual void OnTabClosed()
        {
            TabClosed?.Invoke(this, new EventArgs());
            this.Close();
        }

        #endregion

        public ObservableCollection<skAddresses> AddressList { get; set; }



        private skPerson _PersonObject;
        public skPerson PersonObject
        {
            get { _PersonObject.Validate(); return _PersonObject; }
            set
            {
                _PersonObject = value;
                NotifyPropertyChanged("PersonObject");
            }
        }

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

        private bool _NewAddress;
        public bool NewAddress
        {
            get { return _NewAddress; }
            set
            {
                _NewAddress = value;
                NotifyPropertyChanged("NewAddress");

                if(_NewAddress)
                {
                    AddressObject = new skAddresses();
                }
            }
        }

        public NewPersonViewModel()
        {
            try
            {
                AddressObject = new skAddresses();
                PersonObject = new skPerson();
                _AddNewPerson = new Command(Submit);
                AddressList = new ObservableCollection<skAddresses>(skAddresses.GetAddressList());
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        private Command _AddNewPerson;
        public Command AddNewPerson { get { return _AddNewPerson; } }
        

        public void Submit()
        {
            try
            {
                if (NewAddress)
                    PersonObject.AddNew(AddressObject);
                else
                    PersonObject.AddNew(AddressObject.AddressID);

                OnTabClosed();
            }
            catch(Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
    }
}
