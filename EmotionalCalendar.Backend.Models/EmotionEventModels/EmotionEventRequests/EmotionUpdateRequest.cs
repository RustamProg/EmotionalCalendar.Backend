using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.Models.EmotionEventModels.EmotionEventRequests
{
    public class EmotionUpdateRequest
    {
        public long EmotionId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int RedColor { get; set; }
        public int GreenColor { get; set; }
        public int BlueColor { get; set; }
    }
}
