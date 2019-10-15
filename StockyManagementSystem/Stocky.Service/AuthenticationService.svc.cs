using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Stocky.Service.ServiceInterfaces;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.DataAcesse.Reposetories;
using Stocky.Authentication;


namespace Stocky.Service
{
    
    public class AuthenticationService : IAuthenticationService
    {
        UserRepo Repo;

        public AuthenticationService()
        {
            if (Helpers.DBIdentification)
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set("LocalHost", "Stocky");
            }
            else
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set(@"7TECH-SVR1", "Stocky_LIVE", "applogin", "728652Hotdog");
            }

            Repo = new UserRepo();
        }
        public string HashPassword(string Password)
        {
            return UserAuthentication.HashPassword(Password);
        }

        public UserContract LoginUser(string UserName, string PassWord)
        {            
            UserContract ReturnObject = new UserContract();

            if (UserAuthentication.LoginUser(UserName, PassWord))
            {               
                ReturnObject.UserObject =  Repo.GetUserObject(UserName);
                ReturnObject.IsValidUser = true;
            }
            else { ReturnObject.IsValidUser = false; }
            return ReturnObject;
        }

      
    }
}
