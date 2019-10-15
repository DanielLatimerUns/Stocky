using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
namespace Stocky.Views
{
    public class StockToolBar : ViewModelBase
    {
        private int Height;
        private Command SetProperty;

        public StockToolBar()
        {
            SetProperty = new Command(Setint);
            Height = 45;

            int test;
        }

        public Command _setPropery
        {
            get { return SetProperty; }
        }

        private void Setint()
        {
            if (Height == 45)
            {
                _Height = 6;
            }
            else
            {
                _Height = 45;
            }
           
        }
        public int _Height
        {
            get { return Height; }
            set
            {
                Height = value;
                NotifyPropertyChanged("_Height");
            }
        }
    }

}
