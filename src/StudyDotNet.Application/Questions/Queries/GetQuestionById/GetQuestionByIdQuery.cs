using MediatR;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetQuestionById;

public sealed record GetQuestionByIdQuery(Guid Id) : IRequest<QuestionDetailsResponse>;
