using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MVVM_Framework;

namespace Stocky.ToolBar
{
   public class ActionBar : ToolBarButton
    {
       public ObservableCollection<ToolBarButton> ButtonList { get; set; }

       public ActionBar( string content = null,Command command = null,object paramater = null)
       {
           Content = content;
           Command = command;
           Paramater = paramater;

           ButtonList = new ObservableCollection<ToolBarButton>();
       }

       public virtual void Add(ToolBarButton Button)
       {
            if(Button != null)
            {
                ButtonList.Add(Button);
            }
       }

        public void Refresh()
        {
            var templist = new List<ToolBarButton>(ButtonList);

            ButtonList.Clear();

            foreach(var i in templist)
            {
                ButtonList.Add(i);
            }
        }
    }
}
