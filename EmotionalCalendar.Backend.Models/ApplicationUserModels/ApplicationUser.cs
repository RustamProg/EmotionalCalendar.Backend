using System;

namespace EmotionalCalendar.Backend.Models.ApplicationUserModels
{
    public class ApplicationUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleEnum Role { get; set; }
    }
}
