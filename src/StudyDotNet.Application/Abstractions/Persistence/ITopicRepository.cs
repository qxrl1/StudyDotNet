using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Abstractions.Persistence;

public interface ITopicRepository
{
    Task<IReadOnlyList<TopicResponse>> GetAllAsync(CancellationToken cancellationToken);
}
