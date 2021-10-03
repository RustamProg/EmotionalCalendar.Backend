using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class EventNote
    {
        [Key]
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public ICollection<Emotion> Emotions { get; set; } = new List<Emotion>();
        public ICollection<EmotionEventRate> EmotionEventRates { get; set; } = new List<EmotionEventRate>();
    }
}
