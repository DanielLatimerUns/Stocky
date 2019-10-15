using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;


namespace Stocky.ToolBar
{
    
    public class ToolBarButton
    {
        public string Content { get; set; }
        public Command Command { get; set; }
        public object Paramater { get; set; }
        
    }
}
