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
    public class PreferenceStyleViewModel : ViewModelBase,Interfaces.IViewModelTabItem
    {
        #region Fields
        UserPreference UP;
        #endregion

        #region Properties
        private skTheme _ThemeOBJ;

        public skTheme ThemeOBJ
        {
            get { return _ThemeOBJ; }
            set
            {
                _ThemeOBJ = value;
                NotifyPropertyChanged("ToolBarColour");
            }
        }

        private string _ToolBarColour;

        public string ToolBarColour
        {
            get { return _ToolBarColour; }
            set
            {
                _ToolBarColour = value;
                NotifyPropertyChanged("ToolBarColour");
            }
        }

        public ObservableCollection<string> ColourLIst { get; set; }
        #endregion

        #region Constructors
        public PreferenceStyleViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;
            try
            {
                _ThemeOBJ = new skTheme();
                UP = new UserPreference();
                ColourLIst = new ObservableCollection<string>();
                _Update = new Command(UpdatePref);
                LoadColours();
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
        private Command _Update;
        
        //Properties
        public Command Update { get { return _Update; } }
        #endregion

        #region Command Methods
        public void UpdatePref()
        {
            try
            {
                List<skPreference> PreferanceList = new List<skPreference>();
                PreferanceList.Add(new skPreference { Code = "MAINTOOLCO", Value = ToolBarColour, Name = "Main ToolBar" });

                UP.UpdatePreferenceList(PreferanceList);
            }
            catch (Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        #endregion

        #region Methods
        private void LoadColours()
        {
            try
            {
                ColourLIst.Add("Black");
                ColourLIst.Add("LightGrey");
                ColourLIst.Add("White");
                ColourLIst.Add("LightBlue");
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
