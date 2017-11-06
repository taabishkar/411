using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities.Notifications
{
    public class NotificationType
    {
        public int NotificationTypeId { get; set; }
        public string NotificationTypeDescription { get; set; }
        public ICollection<Notification> Notification { get; set; }
    }
}
