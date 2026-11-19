using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Application.Abstractions.Persistence;

public interface ITopicRepository
{
    Task<IReadOnlyList<TopicResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> AddAsync(Topic topic, CancellationToken cancellationToken);

    Task<bool> ExistsByIdAsync(Guid topicId, CancellationToken cancellationToken);
}
