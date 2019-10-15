using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stocky.Data
{
    public class skTheme : DataModelBase
    {
        #region Properties
        private string _MainBackground;
        [ThemeProp(IsThemeProperty = true)]
        public string MainBackground
        {
            get { return _MainBackground; }
            set
            {
                _MainBackground = value;
                RaisePropertyChanged("MainBackground");
            }
        }

        private string _backingColour;
        [ThemeProp(IsThemeProperty = true)]
        public string BackingColour
        {
            get { return _backingColour; }
            set
            {
                _backingColour = value;
                RaisePropertyChanged("BackingColour");
            }
        }

        private string _secondaryColour;
        [ThemeProp(IsThemeProperty = true)]
        public string SecondaryColour
        {
            get { return _secondaryColour; }
            set
            {
                _secondaryColour = value;
                RaisePropertyChanged("SecondaryColour");
            }
        }

        private string _borderColour;
        [ThemeProp(IsThemeProperty = true)]
        public string BorderColour
        {
            get { return _borderColour; }
            set
            {
                _borderColour = value;
                RaisePropertyChanged("BorderColour");
            }
        }

        private string _fontColour;
        [ThemeProp(IsThemeProperty = true)]
        public string FontColour
        {
            get { return _fontColour; }
            set
            {
                _fontColour = value;
                RaisePropertyChanged("FontColour");
            }
        }

        private string _menuFontColour;
        [ThemeProp(IsThemeProperty = true)]
        public string MenuFontColour
        {
            get { return _menuFontColour; }
            set
            {
                _menuFontColour = value;
                RaisePropertyChanged("MenuFontColour");
            }
        }

        private string _highlightFontColour;
        [ThemeProp(IsThemeProperty = true)]
        public string HighlightFontColour
        {
            get { return _highlightFontColour; }
            set
            {
                _highlightFontColour = value;
                RaisePropertyChanged("HighlightFontColour");
            }
        }

        private string _highlightColour;
        [ThemeProp(IsThemeProperty = true)]
        public string HighlightColour
        {
            get { return _highlightColour; }
            set
            {
                _highlightColour = value;
                RaisePropertyChanged("HighlightColour");
            }
        }
        #endregion

    }

    public class ThemeProp : Attribute
    {
        public bool IsThemeProperty { get; set; }
    }
}
