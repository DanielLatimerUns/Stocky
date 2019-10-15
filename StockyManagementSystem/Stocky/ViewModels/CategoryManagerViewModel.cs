using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Stocky.Data;
using Stocky.Views;
using System.ComponentModel;
using MVVM_Framework;
using System.Windows;
using Stocky.ToolBar;
using Stocky.Adapters;


namespace Stocky.ViewModels
{
   public class CategoryManagerViewModel : ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Fields

        private skCategory _SelectedCategoryOBJ;
        #endregion

        #region Properties
        private List<Stocky.Data.Interfaces.IDataModel> SourceList { get; set; }       


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


        public bool IsCategorySelected
        {
            get
            {
                if (SelectedCategoryOBJ != null)
                    return true;
                return false;
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

        public skCategory SelectedCategoryOBJ
        {
            get { return _SelectedCategoryOBJ; }
            set
            {
                _SelectedCategoryOBJ = value;
                NotifyPropertyChanged("CategoryOBJ");
                LoadToolBar();
            }
        }

        public skCategory SearchFileter { get; set; }
        #endregion

        #region Constructors
        public CategoryManagerViewModel()
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

                SearchFileter = new skCategory();
                _GetRecordsCommand = new AsynchronousCommand(SearchMethod);
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
        //Fields
        private AsynchronousCommand _GetRecordsCommand;     
        //Properties
        public AsynchronousCommand GetRecordsCommand { get { return _GetRecordsCommand; } }
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
                    SetBusy(true);

                    SourceList = new List<Stocky.Data.Interfaces.IDataModel>(skCategory.CategoryList());
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
                    foreach (var Item in skCategory.CategoryList(SearchFileter))
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
        #endregion


        #region Methods

        public void LoadToolBar()
        {
            try
            {
                if (actionbar != null)
                    actionbar.ButtonList.Clear();
                else
                    actionbar = new ActionBar();

                actionbar.ButtonList.Add(new ToolBarButton { Content = "Add New", Command = UiCommands.LoadNewModule,Paramater = "NewCategoryView| New Category"});
                actionbar.ButtonList.Add(new ToolBarButton { Content = "Details", Command = new Command(SelectedCategoryOBJ.Load,IsCategorySelected)});
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

