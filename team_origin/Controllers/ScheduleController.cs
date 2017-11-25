using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_origin.Entities.Schedule;
using team_origin.Contracts;
using team_origin.Entities.Notifications;

namespace team_origin.Controllers
{
    [Route("api/schedule")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserEventRef> _userEventRefRepository;
        public ScheduleController(IScheduleRepository scheduleRepository, IRepository<User> userRepository, IRepository<UserEventRef> userEventRefRepository)
        {
            _scheduleRepository = scheduleRepository;
            _userRepository = userRepository;
            _userEventRefRepository = userEventRefRepository;
        }

        [HttpPost("save")]
        public IActionResult SaveSchedule([FromBody] Schedule schedule)
        {
            try
            {
                bool saveSchedule = _scheduleRepository.SaveSchedule(schedule);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}