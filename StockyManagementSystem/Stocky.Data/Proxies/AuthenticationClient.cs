using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Service.ServiceInterfaces;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Data.Interfaces;

namespace Stocky.Proxies
{
    public class AuthenticationClient : ClientBase<IAuthenticationService>, IAuthenticationService
    {
        public AuthenticationClient()
            :base(EndPointDefinitions.AuthenticationEndPoint)
        {
            
        }

        public string HashPassword(string Password)
        {
           return base.Channel.HashPassword(Password);
        }

        public UserContract LoginUser(string UserName, string PassWord)
        {
                         return base.Channel.LoginUser(UserName, PassWord);
           
        }
    }
}
