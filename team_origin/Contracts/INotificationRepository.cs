using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.Contracts
{
    public interface INotificationRepository
    {
        bool UpdateNotification(Notification Notification);
        ICollection<Notification> GetNotificationsByUserId(string UserId);
        
    }
}
