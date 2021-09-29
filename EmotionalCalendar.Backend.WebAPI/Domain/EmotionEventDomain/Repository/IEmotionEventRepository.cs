using EmotionalCalendar.Backend.Models.EmotionEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository
{
    public interface IEmotionEventRepository
    {
        Task<IEnumerable<Emotion>> GetAllEmotions();
        Task<IEnumerable<DailyEmotion>> GetAllDailyEmotions();
        Task<IEnumerable<DailyEmotion>> GetDailyEmotionsByUserId(Guid userId);
        Task<IEnumerable<EventNote>> GetAllEventNotes();
        Task AddEmotion(Emotion emotion);
        Task AddEventNote(EventNote eventNote);
        Task AddDailyEmotion(DailyEmotion dailyEmotion);
    }
}
