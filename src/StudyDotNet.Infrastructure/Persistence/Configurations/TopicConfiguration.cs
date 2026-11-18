using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Infrastructure.Persistence.Configurations;

internal sealed class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("Topics");
        builder.HasKey(topic => topic.Id);
        
        builder.Property(topic => topic.Name)
            .HasMaxLength(Topic.NAME_MAX_LENGTH)
            .IsRequired();
        builder.Property(topic => topic.Slug)
            .HasMaxLength(Topic.SLUG_MAX_LENGTH)
            .IsRequired();
        
        builder.HasIndex(topic => topic.Slug).IsUnique();
    }
}
