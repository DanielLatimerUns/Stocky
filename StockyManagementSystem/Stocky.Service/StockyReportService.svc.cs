using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Stocky.Service.Contracts;
using Stocky.Service.ServiceInterfaces;
using Stocky.Service.Reporting;

namespace Stocky.Service
{
    public class StockyReportService : IReportService
    {
        SSRSReportRepo Repo;

        public StockyReportService()
        {
            Repo = new SSRSReportRepo();
        }
        public ReportContract GetReports(string ReportBaseURI)
        {
            ReportContract returncontract = new ReportContract();

            returncontract.ReporFolders = Repo.GetReportFolders();
            returncontract.ReportLinks = Repo.GetReports(ReportBaseURI);

            return returncontract;
        }
    }
}
