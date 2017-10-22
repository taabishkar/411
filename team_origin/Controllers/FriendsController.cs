using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using team_origin.Contracts;
using team_origin.Entities;
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

        public FriendsController(
            IRepository<User> userRepo,
            IFriendshipRepository friendshipRepo,
            IRepository<Friendship> friendshipRepository
           )
        {

            _userRepo = userRepo;
            _friendshipRepo = friendshipRepo;
            _friendshipRepository = friendshipRepository;
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

        [HttpPost("acceptFriendRequest")]
        public IActionResult AcceptFriendRequeust([FromBody] FriendRequestViewModel friendRequestVieModel)
        {
            var friendship = _friendshipRepo.AcceptRequest(friendRequestVieModel.FromUserId, friendRequestVieModel.ToUserId);

            if (friendship != null)
            {
                friendship.FriendshipStatusId = 1;
                _friendshipRepository.Update(friendship);
                return Ok(true);
            }
            else {
                return BadRequest();
            }
        }
    }
}
