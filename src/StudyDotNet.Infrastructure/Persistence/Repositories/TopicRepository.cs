using Microsoft.EntityFrameworkCore;
using Npgsql;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Infrastructure.Persistence.Repositories;

internal sealed class TopicRepository(StudyDotNetDbContext dbContext) : ITopicRepository
{
    public async Task<bool> AddAsync(Topic topic, CancellationToken cancellationToken)
    {
        dbContext.Topics.Add(topic);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException exception)
            when (exception.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.UniqueViolation,
                ConstraintName: "IX_Topics_Slug"
            })
        {
            return false;
        }
    }

    public Task<bool> ExistsByIdAsync(Guid topicId, CancellationToken cancellationToken)
    {
        return dbContext.Topics.AnyAsync(topic => topic.Id == topicId, cancellationToken);
    }

    public async Task<IReadOnlyList<TopicResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Topics
            .AsNoTracking()
            .OrderBy(topic => topic.Name)
            .Select(topic => new TopicResponse(topic.Id, topic.Name, topic.Slug))
            .ToListAsync(cancellationToken);
    }
}
