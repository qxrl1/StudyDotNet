using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Questions.Contracts;

public sealed record QuestionDetailsResponse(
    Guid Id,
    string Title,
    string Statement,
    Difficulty Difficulty,
    QuestionType Type,
    IReadOnlyList<QuestionOptionResponse> Options);
