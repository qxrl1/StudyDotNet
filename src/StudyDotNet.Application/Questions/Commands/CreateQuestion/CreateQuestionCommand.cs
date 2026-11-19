using MediatR;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Questions.Commands.CreateQuestion;

public sealed record CreateQuestionCommand(
    Guid TopicId,
    string Title,
    string Statement,
    string Explanation,
    Difficulty Difficulty,
    QuestionType Type,
    IReadOnlyCollection<NewQuestionOption> Options) : IRequest<QuestionDetailsResponse>;
