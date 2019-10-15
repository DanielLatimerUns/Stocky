using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;

namespace Stocky.Session
{
    public sealed class CurrentSession
    {
        public static skUser CurrentUserObject { get; set; }

        public static List<skPreference> CurrentPreferences { get; set; }

        public static skTheme CurrentTheme { get; set; }

        public static bool IsloggedIn
        {
            get
            {
                if (CurrentUserObject != null && CurrentUserObject.UserID != 0)
                    return true;
                else
                return false;
            }
        }

        public static bool IsInDebugMode { get; set; }

    }
}
