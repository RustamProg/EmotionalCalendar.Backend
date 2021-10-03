using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests
{
    public class EmotionCardRequest
    {
        public string EventNoteTitle { get; set; }
        public string EventNoteContent { get; set; }
        public IEnumerable<DailyEmotionEmotionModel> Emotions { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
