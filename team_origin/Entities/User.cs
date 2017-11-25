using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using team_origin.Entities.Schedule;

namespace team_origin.Entities.Notifications
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VerificationCode VerificationCode { get; set; }
        public ICollection<Friendship> ToUserFriendship { get; set; }
        public ICollection<Friendship> FromUserFriendship { get; set; }
        public Mood Mood { get; set; }
        public ICollection<Notification> Notification {get; set;}
        public ICollection<UserNotificationRef> UserNotificationRef { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<UserEventRef> UserEventRef { get; set; }

    }
}
