using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests
{
    public class EmotionRateRequest
    {
        public long EmotionId { get; set; }
        public int EmotionRate { get; set; }
    }
}
