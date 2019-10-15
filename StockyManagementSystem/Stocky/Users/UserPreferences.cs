using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Framework;
using Stocky.Data;
using System.Windows;
using Stocky.Session;
using Stocky.Utility.Theming;

namespace Stocky.Users
{
     public class UserPreference : ViewModelBase
    {

         public void Load()
         {
            Proxies.AppClient AppClient = new Proxies.AppClient();
            try
            {
                AppClient.Open();

                CurrentSession.CurrentPreferences = AppClient.GetUserPreferances(CurrentSession.CurrentUserObject.UserID).UserPreferancesList;
                CurrentSession.CurrentTheme = AppClient.GetThemeObject(CurrentSession.CurrentUserObject.UserID);

                AppClient.Close();
            }
            catch(Exception)
            {
                throw;
            }
            finally { AppClient.Close(); }


         }

         public void UpdateSinglePreference(int UserID,string Code,object value)
         {

            Proxies.AppClient AppClient = new Proxies.AppClient();
            AppClient.Open();

            AppClient.UpdateSinlgeUserPreferances(UserID, Code, value); 

            Load();
         }

         public void UpdatePreferenceList(List<skPreference> PreferenceOBJ)
         {
             if(PreferenceOBJ.Count != 0)
             {
                Proxies.AppClient AppClient = new Proxies.AppClient();
                AppClient.Open();

                AppClient.UpdateUserPreferances(PreferenceOBJ, CurrentSession.CurrentUserObject.UserID);

                AppClient.Close();
                Load();

             }
         }
   
    }
}
