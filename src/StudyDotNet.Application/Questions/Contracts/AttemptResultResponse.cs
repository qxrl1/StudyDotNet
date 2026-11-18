using StudyDotNet.Domain.Entities.QuestionAttempts;
using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Application.Questions.Contracts;

public sealed record AttemptResultResponse(
    Guid AttemptId,
    bool IsCorrect,
    string Feedback,
    IReadOnlyList<CorrectOptionResponse> CorrectOptions,
    string Explanation,
    DateTimeOffset SubmittedAt)
{
    public static AttemptResultResponse From(QuestionAttempt attempt, Question question)
    {
        var correctOptions = question.Options
            .Where(option => option.IsCorrect)
            .OrderBy(option => option.Order)
            .Select(option => new CorrectOptionResponse(option.Id, option.Text))
            .ToArray();

        return new AttemptResultResponse(
            attempt.Id,
            attempt.IsCorrect,
            attempt.IsCorrect ? "Correct!" : "Incorrect.",
            correctOptions,
            question.Explanation,
            attempt.SubmittedAt);
    }
}
