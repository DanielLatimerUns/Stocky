using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public abstract class DataModelBase : Validation
    {

        public SearchFilterObject SearchFilter { get; set; }

        static internal skUser CurrentUser { get; set; }
     
        public DataModelBase()
        {
            SearchFilter = new SearchFilterObject();
        }
        public DataModelBase(skUser CurrentUserObj)
        {
            if(CurrentUserObj != null)
            {
                CurrentUser = CurrentUserObj;
            }          
            SearchFilter = new SearchFilterObject();
        }
    }
}
