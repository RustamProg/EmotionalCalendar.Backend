using System;
using System.Linq;
using AutoMapper;
using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Models.ApplicationUserModels;
using Microsoft.AspNetCore.Mvc;

namespace EmotionalCalendar.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public UserController(IUserService userService, ApplicationDbContext dbContext, IMapper mapper)
        {
            _userService = userService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _dbContext.ApplicationUsers
                .ToDictionary(p => p.Id);

            return Ok(result);
        }

        /// <summary>
        /// Получить информацию о текущем пользователе
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            return Ok(_userService.User);
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="userDto">Данные пользователя</param>
        /// <returns>Созданного пользователя</returns>
        [HttpPost]
        public IActionResult CreateUser(ApplicationUserDTO userDto)
        {
            var existingUsers = _dbContext.ApplicationUsers
                .ToDictionary(p => p.Username);

            if (existingUsers.ContainsKey(userDto.Username))
            {
                throw new Exception($"User '{userDto.Username}' already exists");
            }

            var newUser = _mapper.Map<ApplicationUser>(userDto);
            _dbContext.ApplicationUsers.Add(newUser);
            _dbContext.SaveChanges();

            return Ok(newUser);
        }
    }
}