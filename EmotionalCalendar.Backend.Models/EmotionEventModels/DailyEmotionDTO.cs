using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class DailyEmotionDTO
    {
        public EventNoteDTO EventNote { get; set; }
        public long EmotionId { get; set; }
        public int EmotionRate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
