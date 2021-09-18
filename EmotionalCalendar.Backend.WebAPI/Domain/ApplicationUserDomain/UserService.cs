using System;
using System.Text;
using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Constracts.ApplicationUserContracts;
using EmotionalCalendar.Backend.Models.ApplicationUserModels;

namespace EmotionalCalendar.Backend.WebAPI.Domain.ApplicationUserDomain
{
    public class UserService : IUserService
    {
        public ApplicationUser User { get; set; }
        private readonly ApplicationDbContext _dbContext;
        
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void InitalizeDefaultUser()
        {
            
        }
        
        public string GetFullName()
        {
            var result = new StringBuilder();
            result.Append(User.FirstName);
            result.Append(' ');
            result.Append(User.LastName);

            return result.ToString();
        }

        public Guid GetId()
        {
            return User.Id;
        }

        public string GetUsername()
        {
            return User.Username;
        }

        public string GetRoleName()
        {
            return User.Role.GetDescription();
        }
    }
}