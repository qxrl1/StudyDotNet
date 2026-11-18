using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Questions.Contracts;

public sealed record QuestionSummaryResponse(
    Guid Id,
    string Title,
    Difficulty Difficulty,
    QuestionType Type,
    string TopicSlug);
