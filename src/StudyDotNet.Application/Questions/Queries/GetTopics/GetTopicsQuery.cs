using MediatR;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetTopics;

public sealed record GetTopicsQuery : IRequest<IReadOnlyList<TopicResponse>>;
