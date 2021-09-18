using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService, ApplicationDbContext dbContext)
        {
            var username = context.Request.Headers["TempUsername"].FirstOrDefault() ?? 
                throw new Exception("Не задано имя пользователя (костыльная реализация авторизации)");

            var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Username == username);

            if (user is not null)
            {
                userService.User = user;
            }
            else
            {
                throw new Exception("Пользователь не зарегистрирован (костыльная реализация авторизации)");
            }

            await _next(context);
        }
    }
}
