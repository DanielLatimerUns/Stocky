using System.Threading.Tasks;
using Stocky.Service.ServiceInterfaces;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Data.Interfaces;
using System;

namespace Stocky.Proxies
{
    public class ReportClient : ClientBase<IReportService>, IReportService
    {
        public ReportClient()
            :base(EndPointDefinitions.ReportEndPoint)
        {

        }
        public ReportContract GetReports(string GetReportURI)
        {
          return base.Channel.GetReports(GetReportURI);
        }
    }
}
