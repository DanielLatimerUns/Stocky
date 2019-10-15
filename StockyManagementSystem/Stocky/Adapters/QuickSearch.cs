using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Stocky.ViewModels;
using Stocky.Data;
using System.Reflection;
using MVVM_Framework;
using Stocky.Data.Interfaces;
namespace Stocky.Adapters
{
    public class QuickSearch : ViewModelBase
    {
        public ObservableCollection<IDataModel> DisplayList { get; private set; }
        private List<IDataModel> Qsearchlist { get; set; }
        private Command _SearchCMD;

        public QuickSearch(List<IDataModel> searchList)
        {

            DisplayList = new ObservableCollection<IDataModel>(searchList);

            Qsearchlist = new List<IDataModel>(DisplayList);

            _SearchCMD = new Command(search);
        }

   

        public void updatelist(List<IDataModel> searchlist)
        {
            DisplayList.Clear();

            foreach(dynamic Item in searchlist)
            {
                DisplayList.Add(Item);
            }
            Qsearchlist = new List<IDataModel>(DisplayList);
        }


        public void search(object param)
        {
            try
            {
                DisplayList.Clear();

                if (!string.IsNullOrWhiteSpace(param.ToString()))
                {
                    foreach (var u in Qsearchlist.Where(x => x.SearchString.ToUpper().Contains(param.ToString().ToUpper())))
                    {
                        DisplayList.Add(u);
                    }
                }
                else
                {
                    foreach (var u in Qsearchlist)
                    {
                        DisplayList.Add(u);
                    }
                }
            }
            catch(Exception E)
            {
                ExepionLogger.Logger.LogException(E);
                ExepionLogger.Logger.Show(E);
            }
        }
        public Command SearchCMD
        {
            get { return _SearchCMD; }
        }
    }


}
