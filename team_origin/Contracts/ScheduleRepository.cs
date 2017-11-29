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
                    _dbContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

       public Schedule GetScheduleByUserId(string UserId)
        {
            Schedule schedule = new Schedule();
            List<Event> Events = new List<Event>();
            try
            {
                Events = (from e in _dbContext.Event
                          join uer in _dbContext.UserEventRef on e.EventId equals uer.EventId
                          join u in _dbContext.Users on uer.UserId equals u.Id
                          where u.Id == UserId
                          select e
                         ).ToList();
                schedule.Events = Events.OrderBy(o => o.DayId).ThenBy(o => o.From).ToList();
                schedule.UserId = UserId;
            }
            catch (Exception e)
            {
                throw e;
            }
            return schedule;
        }

        public Schedule UpdateSchedule(Schedule schedule)
        {
            Schedule updatedSchedule = new Schedule();
            List<Event> Events = new List<Event>();
            try
            {
                //get the ref objs to be deleted
                var deleteUER = (from e in _dbContext.Event
                                      join uer in _dbContext.UserEventRef on e.EventId equals uer.EventId
                                      join u in _dbContext.Users on uer.UserId equals u.Id
                                      where u.Id == schedule.UserId
                                      select uer
                                     ).ToList();

                //get the events to be deleted
                var deleteEvents = (from e in _dbContext.Event
                                 join uer in _dbContext.UserEventRef on e.EventId equals uer.EventId
                                 join u in _dbContext.Users on uer.UserId equals u.Id
                                 where u.Id == schedule.UserId
                                 select e
                                     ).ToList();

                //delete ref objs
                foreach (var UER in deleteUER)
                {
                    _dbContext.UserEventRef.Remove(UER);
                }
                _dbContext.SaveChanges();

                //delete events
                foreach (var e in deleteEvents)
                {
                    _dbContext.Event.Remove(e);
                }
                _dbContext.SaveChanges();

                //save schedule and return the new schedule
                if (SaveSchedule(schedule))
                {
                    updatedSchedule = GetScheduleByUserId(schedule.UserId);
                    return updatedSchedule;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return updatedSchedule;
        }
    }
}
