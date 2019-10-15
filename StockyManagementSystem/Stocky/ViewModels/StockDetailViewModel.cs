using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Stocky.Data;
using System.ComponentModel;
using MVVM_Framework;
using Stocky.Commands;
using Stocky.Session;
using Stocky.Application;
using System.Windows;

namespace Stocky.ViewModels
{
    public class StockDetailsViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        private ImageImporter ImgImp;
        #endregion

        #region Properties
        private skStock _StockObj;

        public skStock StockObj
        {
            get { _StockObj.Validate(); return _StockObj; }

            set
            {
                _StockObj = value;
                _StockObj.Validate();
                NotifyPropertyChanged("StockObj");
            }
        }

        private skPurchase _Selected;

        public skPurchase SelectedObject
        {
            get { return _Selected; }
            set
            {
                _Selected = value;
                NotifyPropertyChanged("SelectedPurchase");
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

        public ObservableCollection<skCategory> StockTypes { get; set; }
        public ObservableCollection<skValueBands> ValueBands { get; set; }
        public ObservableCollection<skStockHistory> StockHistory { get; set; }
        public ObservableCollection<string> ImageLIst { get; set; }
        public ObservableCollection<Stocky.Data.Interfaces.ITransaction> ObjectSourceList { get; set; }

        #endregion

        #region Constructor
        public StockDetailsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                ImgImp = new ImageImporter();
                if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

                StockObj = ObjectMessenger.FindObject("SelectedStockItem");

                StockObj.UpdateCurrentObject();
                StockHistory = new ObservableCollection<skStockHistory>(StockObj.StockHistory.GetStockHistory(StockObj.Stockid));
                ImageLIst = new ObservableCollection<string>();
                _UpdateStock = new Command(BasicStockUpdate);
                _UploadPhotos = new Command(PhotoUpload);
                _OpenTransactionDetails = new Command(OpenTransactionObject);
                _Loaded = new AsynchronousCommand(Load);
                _SubmitPurchaseLinkCommand = new Command(SubmitPurchaseLinkMethod);
                ObjectSourceList = new ObservableCollection<Data.Interfaces.ITransaction>(skPurchase.GetPurchases());
                _LinkPurchaseCommand = new Command(LinkPurchaseMethod);
                FillComboBoxes();
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
        private Command _UpdateStock;
        private Command _UploadPhotos;
        private AsynchronousCommand _Loaded;
        private Command _OpenTransactionDetails;
        private Command _SubmitPurchaseLinkCommand;
        private Command _LinkPurchaseCommand;

        //Properties
        public Command UpdateStock { get { return _UpdateStock; } }
        public Command UploadPhotso { get { return _UploadPhotos; } }
        public AsynchronousCommand Loaded { get { return _Loaded; } }
        public Command OpenTransactionDetails { get { return _OpenTransactionDetails; } }
        public Command ListSubmitCommand { get { return _SubmitPurchaseLinkCommand; } }

        public Command LinkPurchaseCommand { get { return _LinkPurchaseCommand; } }
        #endregion

        #region Command Methods
        public void BasicStockUpdate()
        {
            try
            {
                
                _StockObj.Update(CurrentSession.CurrentUserObject.UserID);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        public async void Load()
        {
            try
            {
                if (ImageLIst.Count > 0)
                    return;
                await GetPhotos();
            }
            catch(Exception E)
            {
               
                await App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    ExepionLogger.Logger.LogException(E);
                    ExepionLogger.Logger.Show(E);

                });
            }
        }

        public async void PhotoUpload()
        {
            try
            {
                await ImgImp.SaveImagesAsync("Stock", StockObj.Stockid.ToString());

                await GetPhotos();
            }
            catch(Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }

                 
        }

        private void OpenTransactionObject(object paramater)
        {           
            try
            {            
                switch(paramater.ToString())
                {
                    case "Purchase":
                        StockObj.PopulatePurchaseObj();
                        BuissnessHelpers.LoadTransaction(StockObj.PurchaseObject);
                        break;
                    case "Sale":
                        StockObj.PopulateSaleObj();
                        BuissnessHelpers.LoadTransaction(StockObj.SaleObject);
                        break;
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
        private void FillComboBoxes()
        {
            try
            {
                StockTypes = new ObservableCollection<skCategory>(skCategory.CategoryList());
                ValueBands = new ObservableCollection<skValueBands>(skValueBands.ValueBandList());
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }

        public async Task GetPhotos()
        {
            try
            {
                System.Threading.Thread.Sleep(200);
                var images = ImgImp.GetImagesAsync(StockObj.Stockid.ToString(), "Stock");
                await images;

                await App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    ImageLIst.Clear();
                });

                await App.Current.Dispatcher.BeginInvoke((Action)(() => { try { foreach (string s in images.Result) { ImageLIst.Add(s); }; } catch (Exception e) { ExepionLogger.Logger.Show(e); ExepionLogger.Logger.Show(e); } }));
            }
            catch (Exception E)
            {
                if (E.Message != "This object does not have any files.")
                    await App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                   
                        ExepionLogger.Logger.LogException(E); ExepionLogger.Logger.Show(E);
                }));
            }
        }

        private void LinkPurchaseMethod()
        {
            try
            {
                LinkVisibility = Visibility.Visible;
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        private void SubmitPurchaseLinkMethod()
        {
            try
            {
                StockObj.LinktoTransaction((skPurchase)SelectedObject, CurrentSession.CurrentUserObject.UserID);
                LinkVisibility = Visibility.Hidden;

                StockObj.UpdateCurrentObject();
            }
            catch (Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }
        #endregion
    }
}



