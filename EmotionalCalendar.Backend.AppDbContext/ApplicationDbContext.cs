using EmotionalCalendar.Backend.Models.ApplicationUserModels;
using EmotionalCalendar.Backend.Models.EmotionEventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionalCalendar.Backend.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        public DbSet<EventNote> EventNotes { get; set; }
        public DbSet<DailyEmotion> DailyEmotions { get; set; }
    }
}
