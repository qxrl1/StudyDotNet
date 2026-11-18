using MediatR;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetAttemptById;

public sealed record GetAttemptByIdQuery(Guid Id) : IRequest<AttemptResultResponse>;
