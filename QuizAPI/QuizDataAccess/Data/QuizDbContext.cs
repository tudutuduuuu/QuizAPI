using Microsoft.EntityFrameworkCore;
using QuizAPI.QuizModels.Participant;
using QuizAPI.QuizModels.Question;

namespace QuizAPI.QuizDataAccess.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
