using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Utility;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class NotificationRepo :RepoBase
    {
        public IEnumerable<Notification> GetNotifications(int UserID)
        {
            var query = from N in DB.dtNotifications
                        where N.RaisedBy == UserID
                        select new Notification
                        {
                            Description = N.Description,
                            Name = N.Name,
                            EmailBody = N.EmailMsg,
                            IsNew = N.IsNew,
                            RaisedBy = N.RaisedBy,
                            
                        };
            return query;
        }

        public void AddNotification(Notification NotificationObject)
        {

            dtNotification NewNotification = new dtNotification
            {
                Created = DateTime.Now,
                Description = NotificationObject.Description,
                EmailMsg = NotificationObject.EmailBody,
                IsNew = NotificationObject.IsNew,
                Name = NotificationObject.Name,
                RaisedBy = NotificationObject.RaisedBy,
                ObjectType = NotificationObject.ObjectType,
                NotificationGUID = NotificationObject.ID
                    
                };

            DB.dtNotifications.InsertOnSubmit(NewNotification);
            DB.SubmitChanges();
        }

        public void SetNotificationStatus(Guid NotificationGUID,bool IsNew)
        {
            var Notification = DB.dtNotifications.Where(x => x.NotificationGUID == NotificationGUID).FirstOrDefault();

            Notification.IsNew = IsNew;

            DB.SubmitChanges();
        }



    }
}
