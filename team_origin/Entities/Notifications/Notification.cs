using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities.Notifications
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        public string NotificationDetails { get; set; }
        public bool NotificationAcknowledged  { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public User User { get; set; }
        public NotificationType NotificationType { get; set; }
        public UserNotificationRef UserNotificationRef { get; set; }
    }
}
