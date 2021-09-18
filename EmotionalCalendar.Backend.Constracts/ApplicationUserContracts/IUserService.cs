using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalCalendar.Backend.Models.ApplicationUserModels;

namespace EmotionalCalendar.Backend.Constracts.ApplicationUserContracts
{
    public interface IUserService
    {
        ApplicationUser User { get; set; }
        string GetFullName();
        Guid GetId();
        string GetUsername();
        string GetRoleName();
    }
}
