using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Service.Contracts;
using Stocky.Data;

namespace Stocky.Service.ServiceInterfaces
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        string HashPassword(string Password);
        [OperationContract]
        UserContract LoginUser(string UserName, string PassWord);
        

    }
}
