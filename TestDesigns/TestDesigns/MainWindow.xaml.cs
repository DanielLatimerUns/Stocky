using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TestDesigns.AdminSettings;
using TestDesigns.CategoryTab;
using TestDesigns.PurchaseTab;
using TestDesigns.Reports;
using TestDesigns.StockTab;
using TestDesigns.VendorTab;

namespace TestDesigns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            userName.Focus();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximise_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }

        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            StockSubMenu.PlacementTarget = (Button)sender;
            StockSubMenu.IsOpen = true;
        }

        private void btnTransaction_Click(object sender, RoutedEventArgs e)
        {
            TransactionSubMenu.PlacementTarget = (Button)sender;
            TransactionSubMenu.IsOpen = true;
        }

        private void btnVendor_Click(object sender, RoutedEventArgs e)
        {
            VendorSubMenu.PlacementTarget = (Button)sender;
            VendorSubMenu.IsOpen = true;
        }

        private void btnAdminSettings_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsSubMenu.PlacementTarget = (Button)sender;
            AdminSettingsSubMenu.IsOpen = true;
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsSubMenu.PlacementTarget = (Button)sender;
            ReportsSubMenu.IsOpen = true;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LogOutSubMenu.PlacementTarget = (Button)sender;
            LogOutSubMenu.IsOpen = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ExitSubMenu.PlacementTarget = (Button)sender;
            ExitSubMenu.IsOpen = true;
        }

        private void btnSingleStock_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            SingleStock tabcontent = new SingleStock();
            tab.Header = string.Format("{0}: Single Stock", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            StockSubMenu.IsOpen = false;
        }

        private void btnSingleStockWithPurchase_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            SingleStockPurchaseTabs tabcontent = new SingleStockPurchaseTabs();
            tab.Header = string.Format("{0}: Single Stock with Purchase", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;

            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            StockSubMenu.IsOpen = false;
        }

        private void btnStockMan_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            StockManager tabcontent = new StockManager();
            tab.Header = string.Format("{0}: Stock Manager", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;

            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            StockSubMenu.IsOpen = false;
        }

        private void btnNewPurchase_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            NewPurchase tabcontent = new NewPurchase();
            tab.Header = string.Format("{0}: New Purchase", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            TransactionSubMenu.IsOpen = false;
        }

        private void btnNewVendor_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            VendorTabs tabcontent = new VendorTabs();
            tab.Header = string.Format("{0}: New Vendor", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;

            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            VendorSubMenu.IsOpen = false;
        }

        private void btnNewCategory_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            NewCategory tabcontent = new NewCategory();
            tab.Header = string.Format("{0}: New Category", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            StockSubMenu.IsOpen = false;
        }

        private void btnCategoryMan_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            CategoryManager tabcontent = new CategoryManager();
            tab.Header = string.Format("{0}: Category Manager", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            StockSubMenu.IsOpen = false;
        }

        private void btnTransactionMan_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            TransactionManager tabcontent = new TransactionManager();
            tab.Header = string.Format("{0}: Transaction Manager", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            TransactionSubMenu.IsOpen = false;
        }

        private void btnVendorMan_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            VendorManager tabcontent = new VendorManager();
            tab.Header = string.Format("{0}: Vendor Manager", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            VendorSubMenu.IsOpen = false;
        }

        private void btnReportViewer_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            ReportViewer tabcontent = new ReportViewer();
            tab.Header = string.Format("{0}: Report Viewer", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            ReportsSubMenu.IsOpen = false;
        }

        private void btnPreferences_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            MyPreferences tabcontent = new MyPreferences();
            tab.Header = string.Format("{0}: My Preferences", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            AdminSettingsSubMenu.IsOpen = false;
        }

        private void btnAppSettings_Click(object sender, RoutedEventArgs e)
        {
            CloseableTab tab = new CloseableTab();
            ApplicationSettings tabcontent = new ApplicationSettings();
            tab.Header = string.Format("{0}: Application Settings", MainTabWindow.Items.Count + 1);
            tab.Content = tabcontent;
            tab.HorizontalContentAlignment = HorizontalAlignment.Left;
            tab.VerticalContentAlignment = VerticalAlignment.Top;
            tab.IsSelected = true;
            MainTabWindow.Items.Add(tab);
            AdminSettingsSubMenu.IsOpen = false;
        }

        private void btnLogOutYes_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)FindResource("animateFadeIn")).Begin(windowOverlay);
            //windowOverlay.Visibility = Visibility.Visible;
            LogOutSubMenu.IsOpen = false;
            
            userPassword.Focus();
        }

        private void btnLogOutNo_Click(object sender, RoutedEventArgs e)
        {
            LogOutSubMenu.IsOpen = false;
        }

        private void btnExitYes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExitNo_Click(object sender, RoutedEventArgs e)
        {
            ExitSubMenu.IsOpen = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnMenuExpander_Click(object sender, RoutedEventArgs e)
        {
            if (MenuExpander.IsExpanded == true)
            {
                MenuExpander.IsExpanded = false;
            }
            else
            {
                MenuExpander.IsExpanded = true;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((userName.Text.ToUpper() == "MAT") && (userPassword.Password == "password"))
            {
                ((Storyboard)FindResource("animateFadeOut")).Begin(windowOverlay);
                //windowOverlay.Visibility = Visibility.Collapsed;
                loginFailure.Content = "";
            }
            else
            {
                loginFailure.Content = "Incorrect Username or Password*";
            }
            userPassword.Clear();
        }

    }
}