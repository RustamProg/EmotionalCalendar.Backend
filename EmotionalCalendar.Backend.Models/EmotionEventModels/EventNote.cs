﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels
{
    public class EventNote
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public ICollection<DailyEmotion> DailyEmotions { get; set; }
    }
}
