using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class DailyEmotion
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public EventNote EventNote { get; set; }
        public Emotion Emotion { get; set; }
        public int EmotionRate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
