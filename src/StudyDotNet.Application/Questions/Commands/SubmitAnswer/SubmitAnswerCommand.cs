using MediatR;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Commands.SubmitAnswer;

public sealed record SubmitAnswerCommand(
    Guid QuestionId,
    IReadOnlyCollection<Guid> SelectedOptionIds) : IRequest<AttemptResultResponse>;
