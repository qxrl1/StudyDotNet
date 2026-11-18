using Microsoft.EntityFrameworkCore;
using StudyDotNet.Domain.Entities;
using StudyDotNet.Domain.Entities.QuestionAttempts;
using StudyDotNet.Domain.Entities.QuestionOptions;
using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Infrastructure.Persistence;

public sealed class StudyDotNetDbContext(DbContextOptions<StudyDotNetDbContext> options)
    : DbContext(options)
{
    public DbSet<Topic>           Topics            { get; set; }

    public DbSet<Question>        Questions         { get; set; }

    public DbSet<QuestionOption>  QuestionOptions   { get; set; }

    public DbSet<QuestionAttempt> QuestionAttempts  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudyDotNetDbContext).Assembly);
    }
}
