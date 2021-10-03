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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emotion>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            modelBuilder.Entity<EventNote>()
                .HasMany(em => em.Emotions)
                .WithMany(nt => nt.EventNotes)
                .UsingEntity<EmotionEventRate>(
                    j => j
                        .HasOne(em => em.Emotion)
                        .WithMany(rt => rt.EmotionEventRates)
                        .HasForeignKey(em => em.EmotionId),
                    j => j
                        .HasOne(nt => nt.EventNote)
                        .WithMany(rt => rt.EmotionEventRates)
                        .HasForeignKey(nt => nt.EventNoteId));
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        public DbSet<EventNote> EventNotes { get; set; }
    }
}
