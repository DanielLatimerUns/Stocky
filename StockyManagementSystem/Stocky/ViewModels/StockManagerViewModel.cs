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
using Stocky.Login;
using Stocky.Adapters;
using Stocky.ToolBar;
using Stocky.Views;
using Stocky.UI.Dialogs;
using Stocky.Utility;
using Microsoft.Win32;

namespace Stocky.ViewModels
{
  
    public class StockManagerViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        
        private List<Stocky.Data.Interfaces.IDataModel> StockList;
        #endregion

        #region Properties

        public skStock SearchFilter { get; set; }


        private skStock _SelectedStockItem;
        public skStock SelectedStockItem
        {
            get { return _SelectedStockItem; }
            set
            {
                _SelectedStockItem = value;
                NotifyPropertyChanged("SelectedStockItem");
                LoadToolBar();
            }
        }

        public bool IsStockSelected
        {
            get
            {
                if (SelectedStockItem != null)
                    return true;
                return false;
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

        #region Constructors
        public StockManagerViewModel()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

                SearchFilter = new skStock();
                _GetRecordsCommand = new AsynchronousCommand(SearchMethod);
                _Export = new Command(ExportList);
                LoadToolBar();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Commands

        public AsynchronousCommand GetRecordsCommand { get { return _GetRecordsCommand; } }
        //Fields
        private AsynchronousCommand _GetRecordsCommand;
        private Command _Export;

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
                if (QSearch == null)
                {             
                    var task = Task.Run(new Action(() =>
                    {
                        try
                        {
                            SetBusy(true);

                            StockList = new List<Stocky.Data.Interfaces.IDataModel>(skStock.GetStockList());
                            QSearch = new QuickSearch(StockList);

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
                    var task = Task.Run((() =>
                    {
                        try
                        {
                            SetBusy(true);

                            StockList.Clear();
                            foreach (var Item in skStock.GetStockList(SearchFilter))
                            {
                                StockList.Add(Item);
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
                        QSearch.updatelist(StockList);
                    });
                }
                    
        }

       

        private void ExportList()
        {
            try
            {
                CSVExporter exporter = new CSVExporter();

                SaveFileDialog Dialog = new SaveFileDialog();
                Dialog.FileName = "StockListExport";
                Dialog.AddExtension = false;

                if (Dialog.ShowDialog() == true)
                {
                    exporter.FileName = Dialog.FileName;
                    exporter.List = QSearch.DisplayList.ToList<dynamic>();
                    exporter.Export();
                }
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }     
        #endregion

        #region Methods
        private void LoadToolBar()
        {
            try
            {   if (actionbar != null)
                    actionbar.ButtonList.Clear();
                else
                    actionbar = new ActionBar();

                actionbar.Add(new ToolBarButton { Content = "Add New", Command =  UiCommands.LoadNewModule, Paramater = "NewStockSelectionView" });
                actionbar.Add(new ToolBarButton { Content = "Stock Details", Command = new Command(SelectedStockItem.Load, IsStockSelected)});
                actionbar.Add(new ToolBarButton { Content = "Refund/Return", Command = new Command(SelectedStockItem.CreateRefund, IsStockSelected == false ? false : SelectedStockItem.Sold) });
                actionbar.Add(new ToolBarButton { Content = "Export Current", Command = _Export });
                actionbar.Add(new ToolBarButton { Content = "Create Sale", Command = UiCommands.LoadSaleTab, Paramater = SelectedStockItem });

                UiCommands.LoadSaleTab.CanExecute = IsStockSelected == false ? false : !SelectedStockItem.Sold;
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
