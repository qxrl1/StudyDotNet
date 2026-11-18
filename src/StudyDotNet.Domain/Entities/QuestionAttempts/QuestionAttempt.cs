using StudyDotNet.Domain.Exceptions;

namespace StudyDotNet.Domain.Entities.QuestionAttempts;

public sealed partial class QuestionAttempt : BaseEntity
{
    private readonly List<QuestionAttemptOption> _selectedOptions = [];

    public Guid QuestionId { get; private set; }

    public Guid? UserId { get; private set; }

    public bool IsCorrect { get; private set; }

    public DateTimeOffset SubmittedAt { get; private set; }

    public IReadOnlyCollection<QuestionAttemptOption> SelectedOptions => _selectedOptions.AsReadOnly();

    private QuestionAttempt()
    {
    }

    private QuestionAttempt(Guid questionId, Guid? userId, bool isCorrect, DateTimeOffset submittedAt)
    {
        QuestionId = questionId;
        UserId = userId;
        IsCorrect = isCorrect;
        SubmittedAt = submittedAt;
    }
}
