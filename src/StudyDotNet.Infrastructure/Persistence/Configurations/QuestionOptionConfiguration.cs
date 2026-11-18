using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyDotNet.Domain.Entities.QuestionOptions;

namespace StudyDotNet.Infrastructure.Persistence.Configurations;

internal sealed class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable("QuestionOptions");
        builder.HasKey(option => option.Id);

        builder.Property(option => option.Text)
            .HasMaxLength(QuestionOption.TEXT_MAX_LENGTH)
            .IsRequired();
        
        builder.Property(option => option.IsCorrect).IsRequired();
        builder.Property(option => option.Order).HasColumnName("DisplayOrder").IsRequired();
        
        builder.HasIndex(option => new { option.QuestionId, option.Order }).IsUnique();
    }
}
