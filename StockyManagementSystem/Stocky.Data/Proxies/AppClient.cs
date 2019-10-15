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
using Stocky.Utility;

namespace Stocky.Proxies
{
    public class AppClient : ClientBase<IAppService>, IAppService
    {
        public AppClient():
            base(EndPointDefinitions.AppEndPoint)
        {

        }

        public void AddNotification(Notification NotificationObj)
        {
            base.Channel.AddNotification(NotificationObj);
        }

        public void AddThemeType(skThemeType ThemeTypeObject)
        {
            base.Channel.AddThemeType(ThemeTypeObject);
        }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            return base.Channel.DownloadFile(request);
        }

        public NotificationContract GetNotifications(int UserID)
        {
          return base.Channel.GetNotifications(UserID);
        }

        public List<string> GetObjectFileNames(string ObjectID, string TypeID)
        {
            return base.Channel.GetObjectFileNames(ObjectID, TypeID);
        }

        public skTheme GetThemeObject(int ThemeID)
        {
            return base.Channel.GetThemeObject(ThemeID);
        }

        public List<skThemeType> GetThemeTypeList()
        {
            return base.Channel.GetThemeTypeList();
        }

        public UserContract GetUserPreferances(int userID)
        {
            return base.Channel.GetUserPreferances(userID);
        }

        public string LoadSettings()
        {
          return base.Channel.LoadSettings();
        }

        public void SaveSettings(string SettingsXML)
        {
            base.Channel.SaveSettings(SettingsXML);
        }

        public void SetNotificationStatus(Guid NotificationGUID, bool IsNew)
        {
            base.Channel.SetNotificationStatus(NotificationGUID, IsNew);
        }

        public void UpdateSinlgeUserPreferances(int UserID, string Code, object value)
        {
            base.Channel.UpdateSinlgeUserPreferances(UserID, Code, value);

        }

        public void UpdateThemeType(skThemeType ThemeTypeObject)
        {
            base.Channel.UpdateThemeType(ThemeTypeObject);
        }

        public void UpdateUserPreferances(List<skPreference> Preferances, int UserID)
        {
            base.Channel.UpdateUserPreferances(Preferances, UserID);
        }

        public void UploadFiles(RemoteFileInfo request)
        {
            base.Channel.UploadFiles(request);
        }
    }
}
