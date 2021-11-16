using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.CalendarModels.CalendarResponses
{
    public class CalendarMonth
    {
        public string FullDate { get; set; }
        public int MonthNumInYear { get; set; }
        public ICollection<CalendarDay> Days { get; set; } = new List<CalendarDay>();
    }
}
