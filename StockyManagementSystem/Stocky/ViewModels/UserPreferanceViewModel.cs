using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Users;
using Stocky.Data;
using Stocky.UI;
using Stocky.ToolBar;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Stocky.Views;
using System.ComponentModel;

namespace Stocky.ViewModels
{
    public class UserPreferanceViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        ActionBar ToolBar;
        #endregion

        #region Properties
        private UserControl _Con;

        public UserControl Con
        {
            get { return _Con; }
            set
            {
                _Con = value;
                NotifyPropertyChanged("Con");
            }
        }

        public ObservableCollection<ToolBarButton> Buttons { get; set; }
        #endregion

        #region Constructors
        public UserPreferanceViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _ButtonCommand = new Command(RunCommand);
                ToolBar = new ActionBar();
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "Visual Preferances", Command = _ButtonCommand, Paramater = "VP" });
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "Account Details", Command = _ButtonCommand, Paramater = "AD" });
                ToolBar.ButtonList.Add(new ToolBarButton { Content = "New Account", Command = _ButtonCommand, Paramater = "NA" });
                Buttons = new ObservableCollection<ToolBarButton>(ToolBar.ButtonList);
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
        private Command _ButtonCommand;

        //Properties
        public Command ButtonCommand { get { return _ButtonCommand; } }
        #endregion

        #region Command Methods
        public void RunCommand(object paramater)
        {
            try
            {
                switch (paramater.ToString())
                {
                    case "VP":
                        PreferanceStyleView PSV = new PreferanceStyleView();
                        Con = PSV;
                        break;

                    case "AD":
                        AccountPreferanceView APV = new AccountPreferanceView();
                        Con = APV;
                        break;

                    case "NA":
                        NewUserVeiw NUV = new NewUserVeiw();
                        NUV.Show();
                        break;
                }
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
