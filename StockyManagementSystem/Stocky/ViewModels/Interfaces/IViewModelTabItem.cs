using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocky.ViewModels.Interfaces
{
    public interface IViewModelTabItem
    {
        Guid CurrentInstanceGUID { get; }
    }
}
