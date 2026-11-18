using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyDotNet.Domain.Entities.QuestionAttempts;
using StudyDotNet.Domain.Entities.QuestionOptions;

namespace StudyDotNet.Infrastructure.Persistence.Configurations;

internal sealed class QuestionAttemptOptionConfiguration : IEntityTypeConfiguration<QuestionAttemptOption>
{
    public void Configure(EntityTypeBuilder<QuestionAttemptOption> builder)
    {
        builder.ToTable("QuestionAttemptOptions");
        builder.HasKey(option => new { option.AttemptId, option.QuestionOptionId });

        builder.HasOne<QuestionOption>()
            .WithMany()
            .HasForeignKey(option => option.QuestionOptionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
