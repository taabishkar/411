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

        public FriendsController(
            IRepository<User> userRepo,
            IFriendshipRepository friendshipRepo
           )
        {

            _userRepo = userRepo;
            _friendshipRepo = friendshipRepo;
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

        //Search your friend by phone number
        [HttpPost("addfriend")]
        public IActionResult AddFriend([FromBody] AddFriendViewModel addFriendsViewModel)
        {
            try
            {
                var checkSuccess = _friendshipRepo.AddFriend(addFriendsViewModel.FromUserId, addFriendsViewModel.ToUserId);
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
    }
}
