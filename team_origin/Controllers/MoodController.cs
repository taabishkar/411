using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using team_origin.Entities;
using team_origin.Contracts;
using team_origin.ViewModels;

namespace team_origin.Controllers
{
    [Route("api/mood")]
    public class MoodController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Mood> _moodRepository;
        public MoodController(IRepository<User> userRepository, IRepository<Mood> moodRepository)
        {
            _userRepository = userRepository;
            _moodRepository = moodRepository;
        }

        [HttpPost("get")]
        public IActionResult GetMoodByUser([FromBody]  GetMoodByUserViewModel getMoodByUserViewModel)
        {
            var mood = _moodRepository.Find(m => m.UserId == getMoodByUserViewModel.UserId).SingleOrDefault();
            if(mood != null)
            {
                return Ok(mood);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public IActionResult CreateOrUpdateMood([FromBody] Mood mood)
        {
            try
            {
                if (mood.MoodId != 0)
                {
                    var _mood = _moodRepository.Find(m => m.MoodId == mood.MoodId).SingleOrDefault();
                    _mood.MoodDescription = mood.MoodDescription;
                    _mood.CreatedDate = DateTime.Now;
                    _moodRepository.Update(_mood);
                }
                else
                {
                    mood.CreatedDate = DateTime.Now;
                    _moodRepository.Add(mood);
                }
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteMood(int moodId)
        {
            try
            {                
                    var mood = _moodRepository.Find(m => m.MoodId == moodId).SingleOrDefault();
                    if(mood != null)
                    {
                        _moodRepository.Remove(mood);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}