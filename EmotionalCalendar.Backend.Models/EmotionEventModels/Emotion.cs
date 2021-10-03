using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class Emotion
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int RedColor { get; set; }
        public int GreenColor { get; set; }
        public int BlueColor { get; set; }

        [JsonIgnore]
        public ICollection<EventNote> EventNotes { get; set; } = new List<EventNote>();
        [JsonIgnore]
        public ICollection<EmotionEventRate> EmotionEventRates { get; set; } = new List<EmotionEventRate>();
    }
}
