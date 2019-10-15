using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Stocky.Service.Contracts;
using Stocky.Data;
using Stocky.Utility;

namespace Stocky.Service.ServiceInterfaces
{
    [ServiceContract]
    [ServiceKnownType(typeof(System.IO.FileStream))]
    public interface IAppService
    {
        [OperationContract]
        void SaveSettings(string SettingsXML);
        [OperationContract]
        string LoadSettings();
        [OperationContract]
        UserContract GetUserPreferances(int userID);
        [OperationContract]
        void UpdateSinlgeUserPreferances(int UserID, string Code, object value);
        [OperationContract]
        void UpdateUserPreferances(List<skPreference> Preferances, int UserID);
        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);
        [OperationContract]
        void UploadFiles(RemoteFileInfo request);
        [OperationContract]
        List<string> GetObjectFileNames(string ObjectID, string TypeID);
        [OperationContract]
        List<skThemeType> GetThemeTypeList();
        [OperationContract]
        void AddThemeType(skThemeType ThemeTypeObject);
        [OperationContract]
        void UpdateThemeType(skThemeType ThemeTypeObject);
        [OperationContract]
        skTheme GetThemeObject(int ThemeID);
        [OperationContract]
        void AddNotification(Notification NotificationObj);
        [OperationContract]
        NotificationContract GetNotifications(int UserID);

        [OperationContract]
        void SetNotificationStatus(Guid NotificationGUID,bool IsNew);
    }
}
