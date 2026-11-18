using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyDotNet.Domain.Entities.QuestionAttempts;
using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Infrastructure.Persistence.Configurations;

internal sealed class QuestionAttemptConfiguration : IEntityTypeConfiguration<QuestionAttempt>
{
    public void Configure(EntityTypeBuilder<QuestionAttempt> builder)
    {
        builder.ToTable("QuestionAttempts");
        builder.HasKey(attempt => attempt.Id);
        builder.Property(attempt => attempt.UserId);
        builder.Property(attempt => attempt.IsCorrect).IsRequired();
        builder.Property(attempt => attempt.SubmittedAt).IsRequired();

        builder.HasOne<Question>()
            .WithMany()
            .HasForeignKey(attempt => attempt.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(attempt => attempt.SelectedOptions)
            .WithOne()
            .HasForeignKey(option => option.AttemptId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(attempt => attempt.SelectedOptions)
            .HasField("_selectedOptions")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
