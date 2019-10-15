using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.ToolBar;
using System.Collections.ObjectModel;
using Stocky.Login;
using Stocky.Views;
using Stocky.Users;
using System.Windows;
using System.ComponentModel;
using Debugger;
using Stocky.Session;
using Stocky.Application;


namespace Stocky.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        #endregion

        #region Properties
        
        #endregion

        #region Constructors      
        public MainWindowViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {                           
                _Debuggerview = new Command(OpenDebugger);

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
        private Command _Debuggerview;
        //Properties
        public Command Debuggerview { get { return _Debuggerview; } }
        #endregion
          
        #region Command Methods
    
        #endregion

        #region Methods
        private void OpenDebugger()
        {
            try
            {
                Debugger.ScreenDuggerView SCV = new ScreenDuggerView();

                SCV.Show();
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
