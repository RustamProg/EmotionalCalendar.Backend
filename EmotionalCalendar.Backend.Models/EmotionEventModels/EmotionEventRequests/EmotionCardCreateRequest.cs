using System;
using System.Collections.Generic;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests
{
    public class EmotionCardCreateRequest
    {
        public string EventNoteTitle { get; set; }
        public string EventNoteContent { get; set; }
        public IEnumerable<EmotionRateRequest> Emotions { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
