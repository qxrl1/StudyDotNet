namespace StudyDotNet.Domain.Entities.QuestionAttempts;

public sealed class QuestionAttemptOption
{
    public Guid AttemptId { get; private set; }

    public Guid QuestionOptionId { get; private set; }

    private QuestionAttemptOption()
    {
    }

    internal QuestionAttemptOption(Guid attemptId, Guid questionOptionId)
    {
        AttemptId = attemptId;
        QuestionOptionId = questionOptionId;
    }
}
