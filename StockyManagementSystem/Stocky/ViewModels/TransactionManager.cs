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
using Stocky.Views;
using Stocky;
using Stocky.ToolBar;
using Stocky.Adapters;
using Stocky.Utility;
using Stocky.Data.Interfaces;


namespace Stocky.ViewModels
{
   public class TransactionManagerViewModel :ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields

        private List<Stocky.Data.Interfaces.IDataModel> SourceList;
        #endregion

        #region Properties
        private skTransaction _Transaction;

        public skTransaction Transaction
        {
            get { return _Transaction; }
            set
            {
                _Transaction = value;
                NotifyPropertyChanged("Transaction");
            }
        }

        private QuickSearch _Qsearch;

        public QuickSearch QuickSearch
        {
            get { return _Qsearch; }
            set
            {
                _Qsearch = value;
                NotifyPropertyChanged("QuickSearch");
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

        #region Constructors
        public TransactionManagerViewModel()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

                Transaction = new skTransaction();
                _NewPurchase = new Command(OpenNewPurchase);
                _UpdateList = new AsynchronousCommand(SearchMethod);
                _TranDetails = new Command(OpenTranDetails);
                _Export = new Command(ExportList);
                actionbar = new ActionBar();
                _Loaded = new AsynchronousCommand(SearchMethod);

                actionbar.Add(new ToolBarButton { Content = "Add New Purchase", Command = _NewPurchase });
                actionbar.Add(new ToolBarButton { Content = "Transaction Details", Command = _TranDetails });
                actionbar.Add(new ToolBarButton { Content = "Refresh List", Command = _UpdateList });
                actionbar.Add(new ToolBarButton { Content = "Export Current", Command = _Export });
            
               // SourceList = new List<dynamic>(Transaction.GetTransactionList());
               // QuickSearch = new QuickSearch(SourceList);
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
        private Command _Export;
        private Command _TranDetails;
        private AsynchronousCommand _UpdateList;
        private Command _NewPurchase;
        private AsynchronousCommand _Loaded;

        //Properties
        public AsynchronousCommand UpdateList { get { return _UpdateList; } }
        public Command TranDetails { get { return _TranDetails; } }
        public Command NewPurchase { get { return _NewPurchase; } }
        public AsynchronousCommand Loaded { get { return _Loaded; } }
        #endregion

        #region Command Methods

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

            if (QuickSearch == null)
            {
                
                var task = Task.Run(new Action(() =>
                {
                    try
                    {

                        SetBusy(true);

                        SourceList = new List<Stocky.Data.Interfaces.IDataModel>(Transaction.GetTransactionList());
                        QuickSearch = new QuickSearch(SourceList);

                        SetBusy(false);
                    }
                    catch(Exception e)
                    {
                        App.Current.Dispatcher.BeginInvoke((Action)delegate
                        {
                            ExepionLogger.Logger.LogException(e);
                            ExepionLogger.Logger.Show(e);

                        });
                    }           
                }
                ));
                await task;
            }
            else
            {
                var task = Task.Run(new Action(() =>
                {
                    try
                    {
                        SetBusy(true);

                        SourceList.Clear();
                        foreach (var Item in Transaction.GetTransactionList())
                        {
                            SourceList.Add(Item);
                        }

                        SetBusy(false);
                    }
                    catch(Exception e)
                    {
                        App.Current.Dispatcher.BeginInvoke((Action)delegate
                        {
                            ExepionLogger.Logger.LogException(e);
                            ExepionLogger.Logger.Show(e);

                        });
                    }
                }
                ));
                await task;
                await App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    QuickSearch.updatelist(SourceList);
                });       
            }       
        }


        private void OpenNewPurchase()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                NewPurchaseView P = new NewPurchaseView();
                UI.Enviroment.LoadNewTab(P,"New Purchase");
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void OpenTranDetails()
        {
            try
            {
                BuissnessHelpers.LoadTransaction(Transaction.SelectedTransaction);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        private void ExportList()
        {
            try
            {
                CSVExporter exporter = new CSVExporter();
                exporter.List = QuickSearch.DisplayList.ToList<dynamic>();
                exporter.Export();
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
