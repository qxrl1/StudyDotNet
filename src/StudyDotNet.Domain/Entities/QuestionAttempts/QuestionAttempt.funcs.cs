using StudyDotNet.Domain.Exceptions;

namespace StudyDotNet.Domain.Entities.QuestionAttempts;

public sealed partial class QuestionAttempt
{
    public static QuestionAttempt Submit(Guid questionId, Guid? userId, 
        IEnumerable<Guid> selectedOptionIds, bool isCorrect, DateTimeOffset submittedAt)
    {
        if (questionId == Guid.Empty)
            throw new DomainRuleViolationException("An attempt must reference a question.");

        ArgumentNullException.ThrowIfNull(selectedOptionIds);
        var optionIds = selectedOptionIds.ToArray();

        if (optionIds.Length == 0 || optionIds.Any(id => id == Guid.Empty))
            throw new DomainRuleViolationException("An attempt must contain valid selected options.");

        if (optionIds.Distinct().Count() != optionIds.Length)
            throw new DomainRuleViolationException("An option cannot be selected more than once.");

        var attempt = new QuestionAttempt(questionId, userId, isCorrect, submittedAt);
        attempt._selectedOptions.AddRange(optionIds.Select(id =>
            new QuestionAttemptOption(attempt.Id, id)));

        return attempt;
    }
}
