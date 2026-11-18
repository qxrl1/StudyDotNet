using Microsoft.EntityFrameworkCore;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Infrastructure.Persistence.Repositories;

internal sealed class TopicRepository(StudyDotNetDbContext dbContext) : ITopicRepository
{
    public async Task<IReadOnlyList<TopicResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Topics
            .AsNoTracking()
            .OrderBy(topic => topic.Name)
            .Select(topic => new TopicResponse(topic.Id, topic.Name, topic.Slug))
            .ToListAsync(cancellationToken);
    }
}
