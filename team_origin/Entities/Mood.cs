using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Notifications;

namespace team_origin.Entities
{
    public class Mood
    {
        public int MoodId { get; set; }
        public string MoodDescription { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
    }
}
