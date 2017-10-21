using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using team_origin.Contracts;
using team_origin.Entities;
using team_origin.Services;
using team_origin.ViewModels;

namespace team_origin.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IUserRepository _userRepo;

        private readonly IVerificationCodeSenderService _verificationCodeSenderService;

        private readonly IRepository<VerificationCode> _verificationCodeRepo;
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserRepository userRepo,
            IVerificationCodeSenderService verificationCodeSenderService,
            IRepository<VerificationCode> verificationCodeRepo
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _verificationCodeSenderService = verificationCodeSenderService;
            _verificationCodeRepo = verificationCodeRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {

            var user = new User
            {
                UserName = registerViewModel.UserName,
                PhoneNumber = registerViewModel.Mobile,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var verificationcode = await _verificationCodeSenderService.SendSmsAsync(user.PhoneNumber);

            var savedUser = await _userManager.FindByNameAsync(user.UserName);

            var verificationCodeToBeSaved = new VerificationCode
            {
                Code = verificationcode,
                UserId = savedUser.Id,
                ExpirationDate = DateTime.UtcNow.AddMinutes(30)
            };

            _verificationCodeRepo.Add(verificationCodeToBeSaved);

            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName,
                loginViewModel.Password,
                false,
                false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Qas2ty9qqRuwekfg$ytty7j874&32iILOpqu@ayghqpyrbslid52abwtys%"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("https://google.com",
              "https://google.com",
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("validateAccessCode")]
        public IActionResult ValidateAccessCode([FromBody] ValidateAccessCodeViewModel validateVerificationCodeViewModel)
        {
            var userFromDatabase = _userRepo.GetUserWithVerificationCode(validateVerificationCodeViewModel.UserName);

            if (userFromDatabase.VerificationCode.Code != validateVerificationCodeViewModel.AccessCode) {
                return BadRequest();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Qas2ty9qqRuwekfg$ytty7j874&32iILOpqu@ayghqpyrbslid52abwtys%"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("https://google.com",
              "https://google.com",
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}