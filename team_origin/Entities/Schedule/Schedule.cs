using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace team_origin.Entities.Schedule
{
    public class Schedule
    {
        public List<Event> Events { get; set; }
        public string UserId { get; set; }
    }
}
