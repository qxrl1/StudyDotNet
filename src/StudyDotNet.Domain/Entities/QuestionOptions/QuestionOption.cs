using StudyDotNet.Domain.Exceptions;

namespace StudyDotNet.Domain.Entities.QuestionOptions;

public sealed class QuestionOption : BaseEntity
{
    public Guid QuestionId { get; private set; }

    public string Text { get; private set; } = string.Empty;

    public bool IsCorrect { get; private set; }

    public int Order { get; private set; }

    public static readonly int TEXT_MAX_LENGTH = 1000;
    public static readonly int TEXT_MIN_LENGTH = 1;

    private QuestionOption()
    {
    }

    internal QuestionOption(Guid questionId, string text, bool isCorrect, int order)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length > TEXT_MAX_LENGTH)
            throw new DomainRuleViolationException(
                $"Question option text must contain between {TEXT_MIN_LENGTH} and {TEXT_MAX_LENGTH} characters.");

        if (order < 1)
            throw new DomainRuleViolationException("Question option order must be a positive number.");

        QuestionId = questionId;
        Text = text.Trim();
        IsCorrect = isCorrect;
        Order = order;
    }

}
