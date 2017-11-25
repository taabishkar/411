using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_origin.Contracts;
using team_origin.Entities.Notifications;
using team_origin.ViewModels;
using System.Collections;

namespace team_origin.Controllers
{
    [Route("api/notification")]
    public class NotificationController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<UserNotificationRef> _notificationRefRepository;
        public NotificationController(IRepository<User> userRepository, IRepository<Notification> notificationRepository, IRepository<UserNotificationRef> notificationRefRepository)
        {
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
            _notificationRefRepository = notificationRefRepository;
        }

        [HttpPost("get")]
        public IActionResult GetNotificationsByUser([FromBody] GetMoodByUserViewModel user)
        {
            List<Notification>notifications = new List<Notification>();
            try
            {
                var notificationRefList = _notificationRefRepository.Find(nr => nr.RecipientUserId == user.UserId).ToList();

                foreach(var notificationRef in notificationRefList)
                {
                    var notification = _notificationRepository.Find(n => n.NotificationId == notificationRef.NotificationId).SingleOrDefault();
                    if(notification.NotificationAcknowledged == false)
                    {
                        notifications.Add(notification);
                    }                    
                }

                return Ok(notifications);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPost("acknowledge")]
        public IActionResult AcknowledgeNotification([FromBody] Notification notification)
        {           
            try
            {
                notification.NotificationAcknowledged = true;
                _notificationRepository.Update(notification);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}