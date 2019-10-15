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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Stocky.Data;
using Stocky.ViewModels;
using System.Diagnostics;
using Stocky.Views;
using Stocky.UI;



namespace Stocky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {   
        public MainWindowView()
        {
            InitializeComponent();
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

        private void btnLogOutNo_Click(object sender, RoutedEventArgs e)
        {
            LogOutSubMenu.IsOpen = false;
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
    
    }
}
