using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Proxies
{
    public class EndPointDefinitions
    {
        public static bool IsInDebugMode { get; set; }

        public static string AuthenticationEndPoint
        {
            get
            {
                return IsInDebugMode ? "DebugAuthentication" : "Authentication";
            }
        }
        public static string DataEndPoint
        {
            get
            {
                return IsInDebugMode ? "DebugData" : "Data";
            }
        }
        public static string AppEndPoint
        {
            get
            {
                return IsInDebugMode ? "DebugApp" : "App";
            }
        }
        public static string MailEndPoint
        {
            get
            {
                return IsInDebugMode ? "DebugMail" : "Mail";
            }
        }
        public static string ReportEndPoint
        {
            get
            {
                return IsInDebugMode ? "DebugReport" : "Report";
            }
        }
    }
}
