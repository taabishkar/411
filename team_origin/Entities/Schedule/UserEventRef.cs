using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.Entities.Schedule
{
    public class UserEventRef
    {
        public int UserEventRefId { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
