using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.DB
{
    public class PollSystemContext : DbContext
    {
        public PollSystemContext()
        {
        }

        public PollSystemContext(DbContextOptions<PollSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PollOption> PollOptions { get; set; }
        public virtual DbSet<PollQuestion> PollQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PollQuestion>().ToTable("PollQuestion");
            modelBuilder.Entity<PollOption>().ToTable("PollOption");
        }
    }
}
