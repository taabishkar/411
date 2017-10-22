using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using team_origin.Contracts;
using team_origin.Entities;
using team_origin.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace team_origin.Controllers
{
    [Route("api/[controller]")]
    public class FriendsController : Controller
    {
        private readonly IRepository<User> _userRepo;

        public FriendsController(
            IRepository<User> userRepo
           )
        {

            _userRepo = userRepo;
        }

        //Search your friend by phone number
        [HttpPost("search")]
        public IActionResult SearchUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var user = _userRepo.Find(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
                if (user != null)
                {
                    var searchResult = new UserResult
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName
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
    }
}
