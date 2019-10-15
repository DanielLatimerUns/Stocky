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
using ReportViewer.ViewModels;
using Stocky.Data;

namespace ReportViewer.Views
{
    /// <summary>
    /// Interaction logic for ReportViewerView.xaml
    /// </summary>
    public partial class ReportViewerView : UserControl
    {
        public ReportViewerView()
        {
            InitializeComponent();
        }

        private void ReportTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = this.DataContext as ReportsViewerViewModel;
            context.Navi.SelectedReport = ReportTreeView.SelectedItem as ReportLink;

        }
    }
}
