using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.CalendarModels.CalendarResponses
{
    public class CalendarDay
    {
        public DateTime CalendarDate { get; set; }
        public long? MaxEmotionId { get; set; }
        public bool IsCurrentMonth { get; set; }
    }
}
