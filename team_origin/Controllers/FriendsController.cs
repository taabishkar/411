using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using team_origin.Contracts;
using team_origin.Entities;
using team_origin.Entities.Notifications;
using team_origin.Enums;
using team_origin.Results;
using team_origin.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace team_origin.Controllers
{
    [Route("api/[controller]")]
    public class FriendsController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IFriendshipRepository _friendshipRepo;
        private readonly IRepository<Friendship> _friendshipRepository;
        private readonly IRepository<Notification> _notificationRespository;
        private readonly IRepository<UserNotificationRef> _userNotificationRef;

        public FriendsController(
            IRepository<User> userRepo,
            IFriendshipRepository friendshipRepo,
            IRepository<Friendship> friendshipRepository,
            IRepository<Notification> notificationRespository,
            IRepository<UserNotificationRef> usernotificationRefRepository
           )
        {

            _userRepo = userRepo;
            _friendshipRepo = friendshipRepo;
            _friendshipRepository = friendshipRepository;
            _notificationRespository = notificationRespository;
            _userNotificationRef = usernotificationRefRepository;
        }

        //Search your friend by phone number
        [HttpPost("search")]
        public IActionResult SearchUserByPhoneNumber([FromBody] SearchUserViewModel searchUserViewModel)
        {
            try
            {
                var searchedFriend = _userRepo.Find(u => u.PhoneNumber == searchUserViewModel.PhoneNumber).FirstOrDefault();
                if (searchedFriend != null)
                {
                   string friendshipStatus = _friendshipRepo.CheckFriendship(searchUserViewModel.UserId, searchedFriend.Id);

                    var searchResult = new UserResult
                    {
                        FirstName = searchedFriend.FirstName,
                        LastName = searchedFriend.LastName,
                        PhoneNumber = searchedFriend.PhoneNumber,
                        UserName = searchedFriend.UserName,
                        FriendhipStatus = friendshipStatus,
                        Id = searchedFriend.Id
                    };
                    return Ok(searchResult);
                }
                else
                {
                    return NotFound();
                }

            } catch(Exception e)
            {
                return BadRequest(); 
            }
        }

        
        [HttpPost("addfriend")]
        public IActionResult AddFriend([FromBody] FriendRequestViewModel friendRequestVieModel)
        {
            try
            {
                var checkSuccess = _friendshipRepo.AddFriend(friendRequestVieModel.FromUserId, friendRequestVieModel.ToUserId);
                if (checkSuccess)
                {
                    //get the UserName of the request sender and create a notification
                    var RequestSender = _userRepo.Find(u => u.Id == friendRequestVieModel.FromUserId).SingleOrDefault();
                    var notification = new Notification
                    {
                        NotificationTypeId = (int)NotificationTypeConstants.FriendRequestSent,
                        NotificationDetails = $"{RequestSender.FirstName + ' ' + RequestSender.LastName} sent you a friend request",
                        NotificationAcknowledged = false,
                        CreatedBy = friendRequestVieModel.FromUserId,
                        CreatedDateTime = DateTime.UtcNow
                    };
                    _notificationRespository.Add(notification);
                    _notificationRespository.SaveChanges();

                    //get the notification id and save it in the UserNotificationRefTable
                    int NotificationId = notification.NotificationId;
                    var userNotificationRef = new UserNotificationRef
                    {
                        NotificationId = NotificationId,
                        RecipientUserId = friendRequestVieModel.ToUserId
                    };
                    _userNotificationRef.Add(userNotificationRef);
                    _userNotificationRef.SaveChanges();

                    return Ok(true);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("accept")]
        public IActionResult AcceptFriendRequeust([FromBody] RequestResponseViewModel requestResponseViewModel)
        {
            try
            {
                //get the friendship object from the friendship table
                var friendship = _friendshipRepo.AcceptRequest(requestResponseViewModel.Notification.CreatedBy, requestResponseViewModel.LoggedInUserId);

                if (friendship != null)
                {
                    //If friendship object exists, change the status to 2: Accepted!
                    friendship.FriendshipStatusId = 2;
                    _friendshipRepository.Update(friendship);

                    //Update the acknowledgement status of the original notification
                    requestResponseViewModel.Notification.NotificationAcknowledged = true;
                    _notificationRespository.Update(requestResponseViewModel.Notification);

                    //Create a new notification for friend requested accepted event
                    //get the UserName of the request accepter and create a notification
                    var requestAcceptor = _userRepo.Find(u => u.Id == requestResponseViewModel.LoggedInUserId).SingleOrDefault();
                    var notification = new Notification
                    {
                        NotificationTypeId = (int)NotificationTypeConstants.FriendRequestAccepted,
                        NotificationDetails = $"{requestAcceptor.FirstName + ' ' + requestAcceptor.LastName} accepted your request",
                        NotificationAcknowledged = false,
                        CreatedBy = requestResponseViewModel.LoggedInUserId,
                        CreatedDateTime = DateTime.UtcNow
                    };
                    _notificationRespository.Add(notification);
                    _notificationRespository.SaveChanges();

                    //get the notification id and save it in the UserNotificationRefTable
                    int NotificationId = notification.NotificationId;
                    var userNotificationRef = new UserNotificationRef
                    {
                        NotificationId = NotificationId,
                        RecipientUserId = requestResponseViewModel.Notification.CreatedBy
                    };
                    _userNotificationRef.Add(userNotificationRef);
                    _userNotificationRef.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("reject")]
        public IActionResult RejectFriendRequeust([FromBody] RequestResponseViewModel requestResponseViewModel)
        {
            try
            {
                var friendship = _friendshipRepo.AcceptRequest(requestResponseViewModel.Notification.CreatedBy, requestResponseViewModel.LoggedInUserId);

                if (friendship != null)
                {
                    //Update the friendship status to 3: Rejected!
                    friendship.FriendshipStatusId = 3;
                    _friendshipRepository.Update(friendship);

                    //Update the acknowledgement status of the notification
                    requestResponseViewModel.Notification.NotificationAcknowledged = true;
                    _notificationRespository.Update(requestResponseViewModel.Notification);
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }

        [HttpPost("get")]
        public IActionResult GetAllFriends([FromBody] GetMoodByUserViewModel user)
        {
            List<User> friends = new List<User>();
            List<FriendListViewModel> friendList = new List<FriendListViewModel>();
            try
            {
                friends = _friendshipRepo.GetAllFriendsByUserId(user.UserId);
                if(friends != null)
                {
                    foreach(var friend in friends)
                    {
                        FriendListViewModel friendListViewModel = new FriendListViewModel
                        {
                            FirstName = friend.FirstName,
                            LastName = friend.LastName,
                            UserId = friend.Id,
                            Phone = friend.PhoneNumber
                        };
                        friendList.Add(friendListViewModel);
                    }
                }
                return Ok(friendList);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

    }
}
