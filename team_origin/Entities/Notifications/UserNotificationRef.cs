using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities.Notifications
{
    public class UserNotificationRef
    {
        public int UserNotificationRefId { get; set; }
        public string RecipientUserId { get; set; }
        public int NotificationId { get; set; }
        public User User { get; set; }
        public Notification Notification { get; set; }
    }
}
