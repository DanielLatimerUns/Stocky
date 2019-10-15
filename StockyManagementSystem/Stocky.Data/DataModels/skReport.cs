using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Stocky.Proxies;

namespace Stocky.Data
{
    public class skReport :DataModelBase
    {
        public skReport()
        {
            Reports = new ObservableCollection<ReportLink>();
        }

        public string FolderName { get; set; }

        public ObservableCollection<ReportLink> Reports { get; set; }

        public static List<skReport> GetReports(string ReportBaseURI)
        {
            var retunrlist = new List<skReport>();
            var ReportClient = new ReportClient();
            try
            {
                ReportClient.Open();

                var ReportContract = ReportClient.GetReports(ReportBaseURI);

                foreach (ReportFolder RF in ReportContract.ReporFolders.Where(x => x.DisplayName != "" && x.DisplayName != "Data Sources"))
                {
                    var folder = new skReport() { FolderName = RF.DisplayName };

                    foreach (ReportLink RL in ReportContract.ReportLinks.Where(x => x.ReportParentID == RF.FolderId))
                    {
                        folder.Reports.Add(RL);
                    }

                    retunrlist.Add(folder);
                }
                return retunrlist;
            }
            finally
            {
                ReportClient.Close();
            }
           
        }
    }

    public class ReportFolder
    {
        public string DisplayName { get; set; }

        public Guid FolderId { get; set; }
    }

    public class ReportLink
    {
        public string DisplayName { get; set; }

        public Uri ReportLocationURI { get { return new Uri(ReportLocationString); } }

        public Guid? ReportParentID { get; set; }

        public string ReportLocationString { get; set; }

        public Guid ReportID { get; set; }
    }
}
