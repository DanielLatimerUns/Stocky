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
    public class NewCategoryVeiwModel : ViewModelBase, Stocky.ViewModels.Interfaces.IViewModelTabItem
    {
        #region Properties
        private skCategory _Catobj;

        public skCategory Catobj
        {
            get { _Catobj.Validate(); return _Catobj; }
            set
            {
                _Catobj = value;
                NotifyPropertyChanged("Catobj");
            }
        }
        #endregion

        #region Constructors
        public NewCategoryVeiwModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _Catobj = new skCategory();
                _SubmitNewCategory = new Command(Submit);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Commands
        //Fields
        Command _SubmitNewCategory;

        //Properties
        public Command SubmitNewCategory { get { return _SubmitNewCategory; } }
        #endregion

        #region Command Methods
        public void Submit()
        {
            try
            {
                Catobj.AddNew();

                this.Close();
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion
    }
}
