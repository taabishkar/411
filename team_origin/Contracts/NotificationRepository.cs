using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.Contracts
{
    public class NotificationRepository:Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(TeamOriginContext context) : base(context)
        {

        }
        public ICollection<Notification> GetNotificationsByUserId(string UserId)
        {
            ICollection<Notification> notifications = null;
            try
            {
                notifications = (from u in _dbContext.Users
                                 join unr in _dbContext.UserNotificationRef on u.Id equals unr.RecipientUserId
                                 join n in _dbContext.Notification on unr.NotificationId equals n.NotificationId
                                 where u.Id == UserId && n.NotificationAcknowledged == false
                                 select n
                                 ).ToList();               
            }
            catch (Exception e)
            {
                throw e;
            }
            return notifications;
        }

        public bool UpdateNotification(Notification Notification)
        {
            throw new NotImplementedException();
        }
    }
}
