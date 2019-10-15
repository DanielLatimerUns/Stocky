using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestDesigns
{
    public class Themes
    {
        #region Properties
        private string _theme;

        public string Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                SetThemeColours(value);
            }
        }


        private Brush _backingColour;

        public Brush BackingColour
        {
            get { return _backingColour; }
            set
            {
                _backingColour = value;
            }
        }

        private Brush _secondaryColour;

        public Brush SecondaryColour
        {
            get { return _secondaryColour; }
            set
            {
                _secondaryColour = value;
            }
        }

        private Brush _borderColour;

        public Brush BorderColour
        {
            get { return _borderColour; }
            set
            {
                _borderColour = value;
            }
        }

        private Brush _fontColour;

        public Brush FontColour
        {
            get { return _fontColour; }
            set
            {
                _fontColour = value;
            }
        }

        private Brush _menuFontColour;

        public Brush MenuFontColour
        {
            get { return _menuFontColour; }
            set
            {
                _menuFontColour = value;
            }
        }

        private Brush _highlightFontColour;

        public Brush HighlightFontColour
        {
            get { return _highlightFontColour; }
            set
            {
                _highlightFontColour = value;
            }
        }

        private Brush _highlightColour;

        public Brush HighlightColour
        {
            get { return _highlightColour; }
            set
            {
                _highlightColour = value;
            }
        }
        #endregion

        public Themes()
        {
            SetThemeColours("Blue1");
        }

        private void SetThemeColours(string themeName)
        {
            var bc = new BrushConverter();

            switch (themeName)
            {
                case "Blue":
                    BackingColour = (Brush)bc.ConvertFrom("#FFD6DBE9");
                    SecondaryColour = (Brush)bc.ConvertFrom("#FF293955");
                    BorderColour = (Brush)bc.ConvertFrom("#FF2D2D30");
                    FontColour = (Brush)bc.ConvertFrom("#FF2D2D30");
                    MenuFontColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    HighlightFontColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    HighlightColour = (Brush)bc.ConvertFrom("#FF007ACC");
                    break;

                default:
                    BackingColour = (Brush)bc.ConvertFrom("#FF3F3F46");
                    SecondaryColour = (Brush)bc.ConvertFrom("#FF2D2D30");
                    BorderColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    FontColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    MenuFontColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    HighlightFontColour = (Brush)bc.ConvertFrom("#FFFFFFFF");
                    HighlightColour = (Brush)bc.ConvertFrom("#FF007ACC");
                    break;
            }
        }
    }
}
