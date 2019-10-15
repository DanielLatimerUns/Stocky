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

namespace Stocky.Views
{
    /// <summary>
    /// Interaction logic for PurchaseDetailsView.xaml
    /// </summary>
    public partial class PurchaseDetailsView : UserControl
    {
        public PurchaseDetailsView()
        {
            InitializeComponent();
        }

        private void ex_Collapsed(object sender, RoutedEventArgs e)
        {
            MainGrid.RowDefinitions[3].Height = new GridLength(50);
        }

        private void ex_Expanded(object sender, RoutedEventArgs e)
        {
            MainGrid.RowDefinitions[3].Height = new GridLength(150);
        }
    }
}
