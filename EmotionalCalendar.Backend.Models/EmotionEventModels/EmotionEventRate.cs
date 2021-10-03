using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class EmotionEventRate
    {
        public int EmotionRate { get; set; }

        [JsonIgnore]
        public long EventNoteId { get; set; }
        [JsonIgnore]
        public EventNote EventNote { get; set; }

        [JsonIgnore]
        public long EmotionId { get; set; }
        public Emotion Emotion { get; set; }
    }
}
