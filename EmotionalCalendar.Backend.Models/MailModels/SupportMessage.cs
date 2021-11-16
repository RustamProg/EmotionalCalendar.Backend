using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.MailModels
{
    public class SupportMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SenderEmail { get; set; }
    }
}
