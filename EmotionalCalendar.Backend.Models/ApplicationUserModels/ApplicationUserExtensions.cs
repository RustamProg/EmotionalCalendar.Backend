using System.Collections.Generic;

namespace EmotionalCalendar.Backend.Models.ApplicationUserModels
{
    public static class ApplicationUserExtensions
    {
        public static string GetDescription(this RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Admin => "Администратор",
                RoleEnum.User => "Пользователь",
                _ => throw new KeyNotFoundException("Такой роли не существует")
            };
        }
    }

    public enum RoleEnum
    {
        Admin,
        User
    }
}
