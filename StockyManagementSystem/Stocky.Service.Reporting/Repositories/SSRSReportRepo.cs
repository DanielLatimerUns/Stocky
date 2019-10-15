using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.Reporting.Database;
using Stocky.Data;

namespace Stocky.Service.Reporting
{
    public class SSRSReportRepo
    {
        SSRSDBDataContext ReportDB;

        public SSRSReportRepo()
        {
            DBConnection.Set("LocalHost", "ReportServer");

            ReportDB = new SSRSDBDataContext();    
        }

        public List<ReportLink> GetReports(string ReportBaseURI)
        {
            var query = from R in ReportDB.Catalogs
                        where R.Type == 2
                        select new ReportLink
                        {
                            DisplayName = R.Name,
                            ReportID = R.ItemID,
                            ReportParentID = R.ParentID,
                            ReportLocationString = (ReportBaseURI + "/Pages/Report.aspx?ItemPath=" + R.Path.Replace(@"/", @"%2f").Replace(@" ", @"+"))
                        };

            return query.ToList();
        }

        public List<ReportFolder> GetReportFolders()
        {
            var query = from R in ReportDB.Catalogs
                        where R.Type == 1
                        select new ReportFolder
                        {
                            DisplayName = R.Name,
                            FolderId = R.ItemID
                        };

            return query.ToList();

        }
    }
}
