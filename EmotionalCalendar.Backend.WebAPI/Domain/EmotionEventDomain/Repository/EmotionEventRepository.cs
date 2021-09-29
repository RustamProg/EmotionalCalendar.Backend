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

        public async Task AddDailyEmotion(DailyEmotion dailyEmotion)
        {
            await _dbContext.DailyEmotions.AddAsync(dailyEmotion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEmotion(Emotion emotion)
        {
            var emotionNames = await _dbContext.Emotions
                .ToDictionaryAsync(p => p.Name);

            if (emotionNames.ContainsKey(emotion.Name))
            {
                throw new Exception(
                    $"Эмоция \"{emotion.Name}\" с названием \"{emotion.DisplayName}\" и RGB = ({emotion.RedColor}, {emotion.GreenColor}, {emotion.BlueColor}) уже существует");
            }

            await _dbContext.Emotions.AddAsync(emotion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEventNote(EventNote eventNote)
        {
            await _dbContext.EventNotes.AddAsync(eventNote);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailyEmotion>> GetAllDailyEmotions()
        {
            return await _dbContext.DailyEmotions.ToListAsync();
        }

        public async Task<IEnumerable<Emotion>> GetAllEmotions()
        {
            return await _dbContext.Emotions.ToListAsync();
        }

        public async Task<IEnumerable<EventNote>> GetAllEventNotes()
        {
            return await _dbContext.EventNotes.ToListAsync();
        }

        public async Task<IEnumerable<DailyEmotion>> GetDailyEmotionsByUserId(Guid userId)
        {
            var result = await _dbContext.DailyEmotions
                .Where(x => x.UserId == userId).ToListAsync();

            return result;
        }
    }
}
