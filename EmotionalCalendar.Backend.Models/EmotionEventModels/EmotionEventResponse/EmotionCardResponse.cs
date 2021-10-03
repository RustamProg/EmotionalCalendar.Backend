using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventResponse
{
    public class EmotionCardResponse
    {
        public long Id { get; set; }
        public string EmotionEventTitle { get; set; }
        public string EmotionEventContent { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<EmotionRateResponse> Emotions { get; set; }
    }
}
