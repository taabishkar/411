using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Schedule;

namespace team_origin.Contracts
{
    public interface IScheduleRepository
    {
        bool SaveSchedule(Schedule schedule);
        Schedule GetScheduleByUserId(string UserId);
    }
}
