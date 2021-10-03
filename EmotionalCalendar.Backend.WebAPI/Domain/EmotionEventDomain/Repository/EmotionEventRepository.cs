using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository
{
    public class EmotionEventRepository : IEmotionEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EmotionEventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmotionAsync(Emotion emotion)
        {
            await _context.Emotions.AddAsync(emotion);
        }

        public async Task AddEventNoteWithEmotionAsync(EventNote eventNote)
        {
            await _context.EventNotes.AddAsync(eventNote);
        }

        public async Task DeleteEmotionAsync(long emotionId)
        {
            await Task.Run(() =>_context.Emotions.Remove(new Emotion { Id = emotionId}));
        }

        public async Task DeleteEventNoteAsync(long eventNoteId)
        {
            await Task.Run(() => _context.EventNotes.Remove(new EventNote { Id = eventNoteId }));
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotionsAsync()
        {
            return await _context.Emotions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<EventNote>> GetAllEventNotesAsync()
        {
            return await _context.EventNotes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Emotion> GetEmotionByIdAsync(long emotionId)
        {
            return await _context.Emotions
                .FirstOrDefaultAsync(x => x.Id == emotionId);
        }

        public async Task<Emotion> GetEmotionByNameAsync(string emotionName)
        {
            return await _context.Emotions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == emotionName);
        }

        public async Task<IEnumerable<EventNote>> GetEmotionEventRatesByUser(Guid userId)
        {
            return await _context.EventNotes
                .Include(p => p.EmotionEventRates)
                .ThenInclude(p => p.Emotion)
                .ToListAsync();
        }

        public async Task<EventNote> GetEventNoteByIdAsync(long eventNoteId)
        {
            return await _context.EventNotes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == eventNoteId);
        }

        public async Task<IEnumerable<EventNote>> GetEventNotesWithEmotions()
        {
            return await _context.EventNotes
                .AsNoTracking()
                .Include(x => x.Emotions)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventNote>> GetEventNotesWithEmotionsByUserId(Guid userId)
        {
            return await _context.EventNotes
                .AsNoTracking()
                .Include(x => x.Emotions)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public Task<IEnumerable<T>> GetWhereAsync<T>(Func<T, bool> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task SaveDataAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmotionAsync(Emotion emotion)
        {
            await Task.Run(() => _context.Emotions.Update(emotion));
        }

        public async Task UpdateEventNoteWithEmotionAsync(EventNote eventNote, long idToDelete)
        {
            await Task.Run(() => _context.EventNotes.Remove(new EventNote { Id = idToDelete }));
            await _context.EventNotes.AddAsync(eventNote);
        }
    }
}
