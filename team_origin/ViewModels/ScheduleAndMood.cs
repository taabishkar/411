using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities;
using team_origin.Entities.Schedule;

namespace team_origin.ViewModels
{
    public class ScheduleAndMood
    {
        public Schedule Schedule { get; set; }
        public Mood Mood { get; set; }
    }
}
