using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyDotNet.Domain.Entities;
using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Infrastructure.Persistence.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");
        builder.HasKey(question => question.Id);
        
        builder.Property(question => question.Title)
            .HasMaxLength(Question.TITLE_MAX_LENGTH)
            .IsRequired();
        builder.Property(question => question.Statement)
            .HasMaxLength(Question.STATEMENT_MAX_LENGTH)
            .IsRequired();
        builder.Property(question => question.Explanation).
            HasMaxLength(Question.STATEMENT_MAX_LENGTH)
            .IsRequired();
       
        builder.Property(question => question.Difficulty)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(question => question.Type)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(question => question.IsPublished)
            .IsRequired();

        builder.HasOne<Topic>()
            .WithMany()
            .HasForeignKey(question => question.TopicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(question => question.Options)
            .WithOne()
            .HasForeignKey(option => option.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(question => question.Options)
            .HasField("_options")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(question => new
        {
            question.TopicId,
            question.Difficulty,
            question.IsPublished
        });
    }
}
