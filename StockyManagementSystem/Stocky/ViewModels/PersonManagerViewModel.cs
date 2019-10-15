using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using Stocky;
using Stocky.UI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Stocky.Utility;
using Stocky.Adapters;
using Stocky.ToolBar;

namespace Stocky.ViewModels
{
    class PersonManagerViewModel : ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties

        private skPerson _SelectedPerson;
        
        public skPerson SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
                NotifyPropertyChanged("SelectedPerson");
            }
        }

        private skPerson _PersonObject;

        public skPerson PersonObject
        {
            get { return _PersonObject; }
            set
            {
                _PersonObject = value;
                NotifyPropertyChanged("PersonObject");
            }
        }

        private QuickSearch _qSearch;

        public QuickSearch QSearch
        {
            get { return _qSearch; }
            set
            {
                _qSearch = value;
                NotifyPropertyChanged("QSearch");
            }
        }

        private ActionBar _actionbar;

        public ActionBar actionbar
        {
            get { return _actionbar; }
            set
            {
                _actionbar = value;
                NotifyPropertyChanged("actionbar");
            }
        }
        #endregion

        #region Commands
        private Command LoadSelectedPerson;
        private Command LoadNewPerson;
        private AsynchronousCommand _GetRecordsCommand;

        public AsynchronousCommand GetRecordsCommand { get { return _GetRecordsCommand; } }
        #endregion

        #region Fields

        private List<Stocky.Data.Interfaces.IDataModel> SourceList;

        #endregion
        public PersonManagerViewModel()
        {
            LoadSelectedPerson = new Command(LoadSelectedPersonMethod);
            LoadNewPerson = new Command(LoadNewPersonMethod);
            try
            {        
                _GetRecordsCommand = new AsynchronousCommand(SearchMethod);
                actionbar = new ActionBar();
                actionbar.Add(new ToolBarButton { Content = "Person Details", Command = LoadSelectedPerson });
                actionbar.Add(new ToolBarButton { Content = "New Person", Command = LoadNewPerson });
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }
        private async void SearchMethod()
        {
            try
            {
                await GetRecords();
            }
            catch (Exception E)
            {
                await App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    ExepionLogger.Logger.LogException(E);
                    ExepionLogger.Logger.Show(E);

                });
            }
        }

        private async Task GetRecords()
        {

            if (QSearch == null)
            {
                var task = Task.Run(new Action(() =>
                {
                    SetBusy(true);

                    SourceList = new List<Stocky.Data.Interfaces.IDataModel>(skPerson.GetPersonList());
                    QSearch = new QuickSearch(SourceList);

                    SetBusy(false);
                }
                ));
                await task;
            }
            else
            {
                var task = Task.Run(new Action(() =>
                {
                    SetBusy(true);

                    SourceList.Clear();
                    foreach (var Item in skPerson.GetPersonList())
                    {
                        SourceList.Add(Item);
                    }

                    SetBusy(false);
                }
                ));
                await task;
                await App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    QSearch.updatelist(SourceList);
                });
            }
        }

        private void LoadSelectedPersonMethod()
        {
            try
            {
                if (SelectedPerson != null)
                    SelectedPerson.Load();
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }
        private void LoadNewPersonMethod()
        {
            try
            {
                 
                Enviroment.LoadTab(new NewPersonViewModel());
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }
    }
}
