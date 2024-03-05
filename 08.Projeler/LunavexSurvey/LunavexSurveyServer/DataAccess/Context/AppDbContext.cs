using LunavexSurveyServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LunavexSurveyServer.DataAccess.Context;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<QuestionValue> QuestionValue { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveySubmission> SurveySubmissions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SurveySubmission>(entity =>
        {
            entity.HasOne(b => b.Survey)
                  .WithMany()
                  .HasForeignKey(bc => bc.SurveyId)
                  .OnDelete(DeleteBehavior.NoAction); // ON DELETE NO ACTION eklendi
        });
        modelBuilder.Entity<QuestionValue>(entity =>
        {
            entity.HasOne(b => b.Question)
                  .WithMany()
                  .HasForeignKey(bc => bc.QuestionId)

                  .OnDelete(DeleteBehavior.NoAction); // ON DELETE NO ACTION eklendi
        });
    }
}
