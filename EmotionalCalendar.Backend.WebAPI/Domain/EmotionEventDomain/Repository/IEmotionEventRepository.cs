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
        Task<IEnumerable<EventNote>> GetAllEventNotesAsync();
        Task<EventNote> GetEventNoteByIdAsync(long eventNoteId);
        Task<IEnumerable<EventNote>> GetEventNotesWithEmotions();
        Task<IEnumerable<EventNote>> GetEventNotesWithEmotionsByUserId(Guid userId);
        Task AddEmotionAsync(Emotion emotion);
        Task AddEventNoteWithEmotionAsync(EventNote eventNote);
        Task UpdateEmotionAsync(Emotion emotion);
        Task UpdateEventNoteWithEmotionAsync(EventNote eventNote);
        Task DeleteEmotionAsync(long emotionId);
        Task DeleteEventNoteAsync(long eventNoteId);
        Task<IEnumerable<T>> GetWhereAsync<T>(Func<T, bool> predicate) where T : class;
    }
}
