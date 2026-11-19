using MediatR;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Commands.CreateTopic;

public sealed record CreateTopicCommand(string Name, string Slug) : IRequest<TopicResponse>;
