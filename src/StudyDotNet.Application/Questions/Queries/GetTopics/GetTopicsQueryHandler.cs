using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetTopics;

public sealed class GetTopicsQueryHandler(ITopicRepository topicRepository)
    : IRequestHandler<GetTopicsQuery, IReadOnlyList<TopicResponse>>
{
    public Task<IReadOnlyList<TopicResponse>> Handle(
        GetTopicsQuery request,
        CancellationToken cancellationToken)
    {
        return topicRepository.GetAllAsync(cancellationToken);
    }
}
