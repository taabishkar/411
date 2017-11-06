using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_origin.Contracts;
using team_origin.Entities.Notifications;

namespace team_origin.Controllers
{
    [Route("api/notification")]
    public class NotificationController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Notification> _notificationRepository;
        public NotificationController(IRepository<User> userRepository, IRepository<Notification> notificationRepository)
        {
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;

        }

        //[HttpPost("get")]
        //public IActionResult GetNotificationsByUser([FromBody] string UserId)
        //{
        //    try
        //    {
        //        var notifications = Notification
        //            .Join(UserNotificationRef, n => n.NotificationId, nr => nr.NotificationId, (n, nr) => new { n, nr })
        //            .Join(User, u => u.UserId, nr => nr.RecipientId, (nr, u) => new { nr, u })
        //            .Select(u => new { UserId = n.nr.u.UserId });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}