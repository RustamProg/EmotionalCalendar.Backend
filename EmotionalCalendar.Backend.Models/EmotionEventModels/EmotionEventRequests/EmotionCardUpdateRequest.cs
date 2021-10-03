using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests
{
    public class EmotionCardUpdateRequest
    {
        public long Id { get; set; }
        public string EventNoteTitle { get; set; }
        public string EventNoteContent { get; set; }
        public IEnumerable<EmotionRateRequest> Emotions { get; set; }
    }
}
