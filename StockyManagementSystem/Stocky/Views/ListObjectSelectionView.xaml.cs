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

namespace Stocky.Views
{
    /// <summary>
    /// Interaction logic for ListObjectSelectionView.xaml
    /// </summary>
    public partial class ListObjectSelectionView : UserControl
    {
        public ListObjectSelectionView()
        {
            InitializeComponent();
           
        }

        public static readonly DependencyProperty DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(String), typeof(ListObjectSelectionView), null);

        public string DisplayMember
        {
            get { return (String)GetValue(DisplayMemberProperty); }
            set { SetValue(DisplayMemberProperty, value); }
        }

        public static readonly DependencyProperty SourceListProperty = DependencyProperty.Register("ObjectSourceList", typeof(List<skPurchase>), typeof(ListObjectSelectionView), null);

        public List<skAddresses> ObjectSourceList
        {
            get { return (List<skAddresses>)GetValue(SourceListProperty); }
            set { SetValue(SourceListProperty, value); }
        }

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(ListObjectSelectionView), null);

        public object SelectedObject
        {
            get { return (object)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

      
    }
}
