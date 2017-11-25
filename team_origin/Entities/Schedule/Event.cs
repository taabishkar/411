using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities.Schedule
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventDescription { get; set; }
        public int DayId { get; set; }
        public int From { get; set; }   //Minutes from Midnight-start time for event
        public int To { get; set; }     //Minutes from midnight - end time for midnight
        public UserEventRef UserEventRef { get; set; }

    }
}
