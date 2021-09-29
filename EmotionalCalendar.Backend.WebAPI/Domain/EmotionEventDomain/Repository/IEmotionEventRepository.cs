using EmotionalCalendar.Backend.Models.EmotionEventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository
{
    public interface IEmotionEventRepository
    {
        Task<IEnumerable<Emotion>> GetAllEmotionsAsync();
        Task<Emotion> GetEmotionByIdAsync(long emotionId);
        Task<Emotion> GetEmotionByNameAsync(string emotionName);
        Task<IEnumerable<DailyEmotion>> GetAllDailyEmotionsAsync();
        Task<IEnumerable<DailyEmotion>> GetDailyEmotionsByUserIdAsync(Guid userId);
        Task<DailyEmotion> GetDailyEmotionByIdAsync(long dailyEmotionId);
        Task<IEnumerable<EventNote>> GetAllEventNotesAsync();
        Task<EventNote> GetEventNoteByIdAsync(long eventNoteId);
        Task AddEmotionAsync(Emotion emotion);
        Task AddEventNoteAsync(EventNote eventNote);
        Task AddDailyEmotionAsync(DailyEmotion dailyEmotion);
        Task UpdateEmotionAsync(Emotion emotion);
        Task UpdateEventNoteAsync(EventNote eventNote);
        Task UpdateDailyEmotionAsync(DailyEmotion dailyEmotion);
        Task DeleteEmotionAsync(long emotionId);
        Task DeleteEventNoteAsync(long eventNoteId);
        Task DeleteDailyEmotionAsync(long dailyEmotionId);
        Task<IEnumerable<T>> GetWhereAsync<T>(Func<T, bool> predicate) where T : class;
    }
}
