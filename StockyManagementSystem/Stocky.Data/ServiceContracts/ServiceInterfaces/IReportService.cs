
using System.ServiceModel;
using Stocky.Utility;
using Stocky.Data;
using Stocky.Service.Contracts;

namespace Stocky.Service.ServiceInterfaces
{
    [ServiceContract]
    public interface IReportService 
    {
        [OperationContract]
        ReportContract GetReports(string ReportBaseURI);
    }
}
