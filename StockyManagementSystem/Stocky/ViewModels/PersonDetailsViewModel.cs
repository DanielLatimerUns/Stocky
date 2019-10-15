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
    public class PersonDetailsViewModel :ViewModelBase, ViewModels.Interfaces.IViewModelTabItem
    {
   
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

        private skAddresses _NewAddress;
        public skAddresses NewAddress
        {
            get { return _NewAddress; }
            set
            {
                _NewAddress = value;
                NotifyPropertyChanged("NewAddress");
            }
        }
        private skAddresses _SelectedAddress;
        public skAddresses SelectedObject
        {
            get { return _SelectedAddress; }
            set
            {
                _SelectedAddress = value;
                NotifyPropertyChanged("SelectedObject");
            }
        }

        private Visibility _LinkVisibility = Visibility.Hidden;
        public Visibility LinkVisibility
        {
            get { return _LinkVisibility; }
            set
            {
                _LinkVisibility = value;
                NotifyPropertyChanged("LinkVisibility");
            }
        }

        public ObservableCollection<skAddresses> ObjectSourceList { get; set; }
        public ObservableCollection<skAddresses> LinkedAddresses { get; set; }


        public PersonDetailsViewModel()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

                PersonObject = ObjectMessenger.FindObject("PersonObject");

                LinkedAddresses = new ObservableCollection<skAddresses>(PersonObject.LinkedAddresses);
                
                _SubmitChangesCommand = new Command(PersonObject.Update);
                _RemoveAddressCommand = new Command(()=> { PersonObject.RemoveLinkedAddress(SelectedObject.AddressID); });
                _LinkAddressCommand = new Command(LinkAddressMethod);
                _SubmitAddressLinkCommand = new Command(SubmitAddressLinkMethod);
                _AddNewAddressCommand = new Command(AddNewAddressMethod);
                ObjectSourceList = new ObservableCollection<skAddresses>(skAddresses.GetAddressList());
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        private Command _SubmitChangesCommand;
        private Command _AddNewAddressCommand;
        private Command _RemoveAddressCommand;
        private Command _LinkAddressCommand;
        private Command _SubmitNewAddressCommand;
        private Command _SubmitAddressLinkCommand;

        public Command LinkAddressCommand { get { return _LinkAddressCommand; } }
        public Command SubmitChangesCommand { get { return _SubmitChangesCommand; } }
        public Command AddNewAddressCommand { get { return _AddNewAddressCommand; } }
        public Command RemoveAddressCommand { get { return _RemoveAddressCommand; } }
        public Command SubmitNewAddressCommand { get { return _SubmitNewAddressCommand; } }
        public Command ListSubmitCommand { get { return _SubmitAddressLinkCommand; } }

        private void LinkAddressMethod()
        {
            try
            {
                LinkVisibility = Visibility.Visible;
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        private void AddNewAddressMethod()
        {
            try
            {
                ObjectMessenger.Send("ADDRESSLINKOBJECT",PersonObject);
                Enviroment.LoadTab(new NewAddressViewModel());
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }

        }

        private void SubmitAddressLinkMethod()
        {
            try
            {
                PersonObject.LinkAddress(SelectedObject.AddressID);
                LinkVisibility = Visibility.Hidden;

                PersonObject.UpdateLinkedAddresses();

                LinkedAddresses.Clear();

                foreach(var item in PersonObject.LinkedAddresses)
                {
                    LinkedAddresses.Add(item);
                }              
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        
    }
}
