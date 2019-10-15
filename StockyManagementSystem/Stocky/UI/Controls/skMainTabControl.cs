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

namespace Stocky.UI.Controls
{
 
    public class skMainTabControl : TabControl
    {
        //public ObservableCollection<TabItem> TabCache { get; private set; }

        public skMainTabControl()
        {
            //   this.SelectionChanged += SkMainTabControl_SelectionChanged;

            var closebinding = new CommandBinding();
            closebinding.Command = _closeTab;
            closebinding.Executed += CloseTabMethod;
            closebinding.CanExecute += CloseTabPickerCanExecute;
            CommandBindings.Add(closebinding);

            //var navigatebinding = new CommandBinding();
            //navigatebinding.Command = _Navigate;
            //navigatebinding.Executed += NavigateMethod;
            //navigatebinding.CanExecute += NavigateCanExecute;
            //CommandBindings.Add(navigatebinding);

            //TabCache = new ObservableCollection<TabItem>();

        }

        static skMainTabControl()
        {
            _closeTab = new RoutedCommand("CloseTab", typeof(skMainTabControl));
            //   _Navigate = new RoutedCommand("Navigate", typeof(skMainTabControl));
        }

        // private void SkMainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        //     TabCache.Add(SelectedItem as TabItem);
        //  }

        #region AddTab

        public void Add(UserControl Item, string TabHeader = "")
        {
            if (Item != null)
            {
                TabItem Tab = new TabItem();
                Tab.Content = Item;

                if (TabHeader != "")
                {
                    Tab.Header = TabHeader;
                }
                else
                {
                    Tab.Header = Item.GetType().Name;
                }

                Tab.Tag = Guid.NewGuid();

                this.Items.Add(Tab);
                this.SelectedItem = Tab;
                // TabCache.Add(Tab);
            }
        }

        #endregion 

        #region CloseTab 
        public static readonly DependencyProperty ShowColumnPickerCommandProperty =
        DependencyProperty.Register("CloseTab", typeof(RoutedCommand), typeof(skMainTabControl));

        private static RoutedCommand _closeTab;

        public static RoutedCommand CloseTab
        {
            get
            {
                return _closeTab;
            }
        }


        private void CloseTabMethod(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var source = e.Source as TabItem;
                var tag = source.Tag.ToString();
                var tabtodelte = this.Items.Cast<TabItem>().Where(x => x.Tag.ToString().Equals(tag)).SingleOrDefault();

                if (tabtodelte != null)
                {
                    Items.Remove(tabtodelte);

                    //TabCache.Remove(tabtodelte);
                }
            }
            catch(Exception ex)
            {
                Stocky.ExepionLogger.Logger.LogException(ex);
                Stocky.ExepionLogger.Logger.Show(ex);
            }
        }

        private void CloseTabPickerCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.Items.Count > 1)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        #endregion

        #region Navigation
        // public static readonly DependencyProperty Navigation =
        //DependencyProperty.Register("Navigate", typeof(RoutedCommand), typeof(skMainTabControl));

        // private static RoutedCommand _Navigate;

        // public static RoutedCommand Navigate
        // {
        //     get
        //     {
        //         return _Navigate;
        //     }
        // }


        // private void NavigateMethod(object sender, ExecutedRoutedEventArgs e)
        // {       
        //     switch(e.Parameter.ToString())
        //     {
        //         case "BACK":
        //             var lastitem = TabCache.;
        //             SelectedItem = lastitem;
        //             break;
        //     }
        // }

        // private void NavigateCanExecute(object sender, CanExecuteRoutedEventArgs e)
        // {
        //     if (this.Items.Count > 1)
        //     {
        //         e.CanExecute = true;
        //     }
        //     else
        //     {
        //         e.CanExecute = false;
        //     }
        // }
        #endregion

    }
}

