using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.Data
{
    public static class Helpers
    {
        public static void SetDataContextUser(skUser UserOBJ)
        {
            if(UserOBJ != null)    
               DataModelBase.CurrentUser = UserOBJ;
        }
    }
}
