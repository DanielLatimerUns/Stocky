using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace Debugger
{
    public class DebugType :DebugInfo
    {
        public List<DebugInfo> InfoList { get; set; }
        public PropertyInfo[] ViewModelProperties {get;set;}
        public MethodInfo[] ViewModelMethods { get; set; }
        
        public DebugType()
        {
            InfoList = new List<DebugInfo>();
        }
    }
}
