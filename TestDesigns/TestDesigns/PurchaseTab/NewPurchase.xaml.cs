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

namespace TestDesigns.PurchaseTab
{
    /// <summary>
    /// Interaction logic for NewPurchase.xaml
    /// </summary>
    public partial class NewPurchase : UserControl
    {
        public NewPurchase()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    var CurrentTab = (window as MainWindow).MainTabWindow.SelectedItem;
                    (window as MainWindow).MainTabWindow.Items.Remove(CurrentTab);

                }
            }
        }
    }
}
