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
using Stocky.Service.Utility.IO;
using System.Web;
using System.IO;
using Stocky.Utility;

namespace Stocky.Service
{
    [KnownType(typeof(Notification))]
    public class StockyAppService : IAppService
    {
        SettingsRepo SettingsRepo;
        UserRepo UserRepo;
        NotificationRepo NotificationRepo;
        public StockyAppService()
        {
            if (Helpers.DBIdentification)
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set("LocalHost", "Stocky");
            }
            else
            {
                Stocky.DataAcesse.DataBase.DBConnection.Set(@"7TECH-SVR1", "Stocky_LIVE", "applogin", "728652Hotdog");
            }
           
            SettingsRepo = new SettingsRepo();
            UserRepo = new UserRepo();
            NotificationRepo = new NotificationRepo();
        }

        public void AddThemeType(skThemeType ThemeTypeObject)
        {
            UserRepo.AddThemeType(ThemeTypeObject);
        }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
           return FileDownloader.DownLoadFile(request);
        }

        public List<string> GetObjectFileNames(string ObjectID, string TypeID)
        {
            return FileDownloader.GetObjectFileNames(ObjectID, TypeID);
        }

        public skTheme GetThemeObject(int ThemeID)
        {
            return UserRepo.GetThemeObject(ThemeID);
        }

        public List<skThemeType> GetThemeTypeList()
        {
            return UserRepo.GetThemeTypeList().ToList();
        }

        public UserContract GetUserPreferances(int userID)
        {
            UserContract retunrobject = new UserContract();

            retunrobject.UserPreferancesList = UserRepo.GetPreferanceList(userID).ToList();

            return retunrobject;
        }

        public string LoadSettings()
        {
            return SettingsRepo.LoadSettingsData();
        }

        public void SaveSettings(string SettingsXML)
        {
            SettingsRepo.SaveSettingsData(SettingsXML);
        }

        public void UpdateSinlgeUserPreferances(int UserID, string Code, object value)
        {
            UserRepo.UpDatePreferances(UserID, Code, value);
        }

        public void UpdateThemeType(skThemeType ThemeTypeObject)
        {
            UserRepo.UpdateThemeType(ThemeTypeObject);
        }

        public void UpdateUserPreferances(List<skPreference> Preferances, int UserID)
        {
            UserRepo.UpDatePreferances(Preferances, UserID);
        }

        public void UploadFiles(RemoteFileInfo request)
        {
            FileUploader.Upload(request);        
        }

        public void AddNotification(Notification NotificationObj)
        {
            NotificationRepo.AddNotification(NotificationObj);
        }

        public NotificationContract GetNotifications(int UserID)
        {
            NotificationContract returnobject = new NotificationContract();

            returnobject.NotificationList = NotificationRepo.GetNotifications(UserID).ToList();

            return returnobject;
        }

        public void SetNotificationStatus(Guid NotificationGUID,bool IsNew)
        {
            NotificationRepo.SetNotificationStatus(NotificationGUID, IsNew);
        }
    }
}
