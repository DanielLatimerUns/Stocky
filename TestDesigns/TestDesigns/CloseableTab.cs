using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TestDesigns
{
    public class CloseableTab : TabItem
    {

        private static RoutedCommand _closeTab;

        public static RoutedCommand CloseTab
        {
            get { return _closeTab; }

        }

        public CloseableTab()
        {
            var closebinding = new CommandBinding();
            closebinding.Command = _closeTab;
            closebinding.Executed += Close_Tab;
            CommandBindings.Add(closebinding);
        }

        static CloseableTab()
        {
            _closeTab = new RoutedCommand("CloseTab", typeof(CloseableTab));
        }

        void Close_Tab(object sender, RoutedEventArgs e)
        {
            ((TabControl)this.Parent).Items.Remove(this);    
        }

    }
}
