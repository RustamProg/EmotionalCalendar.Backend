namespace EmotionalCalendar.Backend.Models.ApplicationUserModels
{
    public class ApplicationUserDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleEnum Role { get; set; }
    }
}