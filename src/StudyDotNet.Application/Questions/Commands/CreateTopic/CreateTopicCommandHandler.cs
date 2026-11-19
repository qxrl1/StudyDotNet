using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Application.Questions.Commands.CreateTopic;

public sealed class CreateTopicCommandHandler(ITopicRepository topicRepository)
    : IRequestHandler<CreateTopicCommand, TopicResponse>
{
    public async Task<TopicResponse> Handle(
        CreateTopicCommand request,
        CancellationToken cancellationToken)
    {
        var topic = Topic.Create(request.Name, request.Slug);
        if (!await topicRepository.AddAsync(topic, cancellationToken))
        {
            throw new ConflictException($"A topic with slug '{topic.Slug}' already exists.");
        }

        return new TopicResponse(topic.Id, topic.Name, topic.Slug);
    }
}
