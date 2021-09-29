using EmotionalCalendar.Backend.AppDbContext;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.WebAPI.Domain.EmotionEventDomain.Repository
{
    public class EmotionEventRepository : IEmotionEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmotionEventRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddDailyEmotionAsync(DailyEmotion dailyEmotion)
        {
            if (dailyEmotion.EmotionRate > 100 || dailyEmotion.EmotionRate < 1)
            {
                throw new Exception($"Значение эмоции может быть только от 1 до 100 включительно");
            }

            await _dbContext.DailyEmotions.AddAsync(dailyEmotion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEmotionAsync(Emotion emotion)
        {
            await CheckEmotionExistanceAsync(emotion);
            await _dbContext.Emotions.AddAsync(emotion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEventNoteAsync(EventNote eventNote)
        {
            await _dbContext.EventNotes.AddAsync(eventNote);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDailyEmotionAsync(long dailyEmotionId)
        {
            await Task.Run(() => _dbContext.DailyEmotions.Remove(new DailyEmotion { Id = dailyEmotionId }));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmotionAsync(long emotionId)
        {
            await Task.Run(() => _dbContext.Emotions.Remove(new Emotion { Id = emotionId }));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventNoteAsync(long eventNoteId)
        {
            await Task.Run(() => _dbContext.EventNotes.Remove(new EventNote { Id = eventNoteId }));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailyEmotion>> GetAllDailyEmotionsAsync()
        {
            return await _dbContext.DailyEmotions
                .Include(x => x.Emotion)
                .Include(x => x.EventNote)
                .ToListAsync();
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotionsAsync()
        {
            return await _dbContext.Emotions.ToListAsync();
        }

        public async Task<IEnumerable<EventNote>> GetAllEventNotesAsync()
        {
            return await _dbContext.EventNotes.ToListAsync();
        }

        public async Task<DailyEmotion> GetDailyEmotionByIdAsync(long dailyEmotionId)
        {
            return await _dbContext.DailyEmotions
                .Include(x => x.Emotion)
                .Include(x => x.EventNote)
                .FirstAsync(x => x.Id == dailyEmotionId);
        }

        public async Task<IEnumerable<DailyEmotion>> GetDailyEmotionsByUserIdAsync(Guid userId)
        {
            var result = await _dbContext.DailyEmotions
                .Include(x => x.Emotion)
                .Include(x => x.EventNote)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetWhereAsync<T>(Func<T, bool> predicate) where T : class
        {
            return await Task.Run(() => _dbContext.Set<T>().Where(predicate).ToList());
        }

        public async Task<Emotion> GetEmotionByIdAsync(long emotionId)
        {
            return await _dbContext.Emotions.FirstAsync(x => x.Id == emotionId);
        }

        public async Task<Emotion> GetEmotionByNameAsync(string emotionName)
        {
            return await _dbContext.Emotions.FirstAsync(x => x.Name == emotionName);
        }

        public async Task<EventNote> GetEventNoteByIdAsync(long eventNoteId)
        {
            return await _dbContext.EventNotes.FirstAsync(x => x.Id == eventNoteId);
        }

        public Task UpdateDailyEmotionAsync(DailyEmotion dailyEmotion)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEmotionAsync(Emotion emotion)
        {
            await Task.Run(() => _dbContext.Emotions.Update(emotion));
        }

        public Task UpdateEventNoteAsync(EventNote eventNote)
        {
            throw new NotImplementedException();
        }

        private async Task CheckEmotionExistanceAsync(Emotion emotion)
        {
            var emotionNames = await _dbContext.Emotions
                .ToDictionaryAsync(p => p.Name);

            if (emotionNames.ContainsKey(emotion.Name))
            {
                throw new Exception(
                    $"Эмоция \"{emotion.Name}\" с названием \"{emotion.DisplayName}\" и RGB = ({emotion.RedColor}, {emotion.GreenColor}, {emotion.BlueColor}) уже существует");
            }
        }
    }
}
