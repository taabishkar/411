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
            throw new NotImplementedException();
        }

        public bool UpdateNotification(Notification Notification)
        {
            throw new NotImplementedException();
        }
    }
}
