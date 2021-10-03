using EmotionalCalendar.Backend.Models.EmotionEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain
{
    public static class Validator
    {
        public static void ValidateEmotion(Emotion emotion)
        {
            if (emotion.RedColor > 255 || emotion.RedColor < 0
                || emotion.GreenColor > 255 || emotion.GreenColor < 0
                || emotion.BlueColor > 255 || emotion.BlueColor < 0)
            {
                throw new Exception("Цвет должен иметь значение от 0 до 255");
            }

            foreach (var rate in emotion.EmotionEventRates)
            {
                if (rate.EmotionRate < 1 || rate.EmotionRate > 100)
                {
                    throw new Exception("Интенсивность эмоции можеть быть только от 1 до 100");
                }
            }
        }

        public static void ValidateEventNote(EventNote eventNote)
        {
            foreach (var rate in eventNote.EmotionEventRates)
            {
                if (rate.EmotionRate < 1 || rate.EmotionRate > 100)
                {
                    throw new Exception("Интенсивность эмоции можеть быть только от 1 до 100");
                }
            }
        }
    }
}
