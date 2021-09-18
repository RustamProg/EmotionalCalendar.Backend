using System;
using System.Linq;
using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Models.ApplicationUserModels;
using Microsoft.AspNetCore.Mvc;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        
        public UserController(IUserService userService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _dbContext.ApplicationUsers
                .ToDictionary(p => p.Id);

            return Ok(result);
        }

        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            return Ok(_userService.User);
        }

        [HttpPost]
        public IActionResult CreateUser(ApplicationUserDTO userDto)
        {
            var existingUsers = _dbContext.ApplicationUsers
                .ToDictionary(p => p.Username);

            if (existingUsers.ContainsKey(userDto.Username))
            {
                throw new Exception($"User '{userDto.Username}' already exists");
            }
            
            var newUser = new ApplicationUser
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Role = userDto.Role
            };

            _dbContext.ApplicationUsers.Add(newUser);
            _dbContext.SaveChanges();
            return Ok(newUser);
        }
    }
}