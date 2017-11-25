using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using team_origin.Entities.Schedule;

namespace team_origin.Contracts
{
    public class ScheduleRepository: Repository<Event>, IScheduleRepository
    {        
        public ScheduleRepository(TeamOriginContext context) : base(context)
        {

        }

        public ICollection<Event> GetScheduleByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public bool SaveSchedule(Schedule schedule)
        {
            var _uerSet = _dbContext.Set<UserEventRef>();
            try
            {
                foreach(var Event in schedule.Events){
                    _dbSet.Add(Event);
                    _dbContext.SaveChanges();
                    var userEventRef = new UserEventRef
                    {
                        EventId = Event.EventId,
                        UserId = schedule.UserId
                    };
                    _uerSet.Add(userEventRef);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
