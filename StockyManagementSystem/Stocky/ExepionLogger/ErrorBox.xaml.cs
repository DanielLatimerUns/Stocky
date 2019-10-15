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
using System.Windows.Shapes;

namespace Stocky.ExepionLogger
{
    /// <summary>
    /// Interaction logic for ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : Window
    {
        public ErrorBox()
        {
            InitializeComponent();
            this.Owner = App.Current.MainWindow;
        }

        public void Show(Exception exobj)
        {
            MainText.Text = "Error Message: " + exobj.Message.ToString();
            ExtraText.Text = exobj.StackTrace.ToString();
            this.Show();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
