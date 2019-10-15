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
    public class skNumericTextBox : TextBox
    {
        public skNumericTextBox()
        {
            PreviewTextInput += NumericTextBox_PreviewTextInput;

            KeyDown += SkNumericTextBox_KeyDown;     
            
                   
        }



        public bool IsCurrency
        {
            get { return (bool)GetValue(IsCurrencyProperty); }
            set { SetValue(IsCurrencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCurrency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurrencyProperty =
            DependencyProperty.Register("IsIsCurrency", typeof(bool), typeof(skNumericTextBox),new PropertyMetadata(false));

       

        private void SkNumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsCurrency)
            {
                if (e.Key == Key.Decimal)
                {
                    var split = Text.Split('.');

                    this.SelectionStart = split[0].Length + 1;
                    e.Handled = true;
                }
            }

        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {           
            foreach (char c in e.Text)
            {
                if (!e.Text.Contains("."))
                {
                    if (c < '0' || c > '9')
                    {
                        e.Handled = true;
                        return;
                    }
                }             
                e.Handled = false;

            }
        }

        private void Initialse()
        {

        }
    }
}

