using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using MVVM_Framework;
using System.ComponentModel;

namespace Stocky.ViewModels
{
    public class CategoryDetailsViewModel : ViewModelBase ,Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Fields
        #endregion

        #region Properties

        private skCategory _CategoryOBJ;

        public skCategory CategoryOBJ
        {
            get { _CategoryOBJ.Validate();  return _CategoryOBJ; }
            set
            {
                _CategoryOBJ = value;
                _CategoryOBJ.Validate();
                NotifyPropertyChanged("CATEGORYOBJ");
            }
        }

        #endregion

        #region Constructors

        public CategoryDetailsViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

             CategoryOBJ = ObjectMessenger.FindObject("CATEGORYOBJ");

            _SaveChanges = new Command(Submit);

        }

        #endregion

        #region Commands

        private Command _SaveChanges;

        public Command SaveChanges { get { return _SaveChanges; } }

        #endregion

        #region Command Methods

        private void Submit()
        {
            try
            {
                _CategoryOBJ.Update();
            }
            catch(Exception e)
            {
                ExepionLogger.Logger.LogException(e);
                ExepionLogger.Logger.Show(e);
            }
        }

        #endregion

    }
}
