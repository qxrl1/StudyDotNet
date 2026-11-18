using StudyDotNet.Application.Abstractions.Services;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Questions.Evaluation;

public sealed class QuestionAnswerEvaluator : IQuestionAnswerEvaluator
{
    public bool Evaluate(Question question, IReadOnlyCollection<Guid> selectedOptionIds)
    {
        ArgumentNullException.ThrowIfNull(question);
        ArgumentNullException.ThrowIfNull(selectedOptionIds);

        if (!question.IsPublished)
            throw new InvalidSubmissionException("The question is not available for attempts.");

        if (selectedOptionIds.Count == 0)
            throw new InvalidSubmissionException("At least one option must be selected.");

        if (selectedOptionIds.Distinct().Count() != selectedOptionIds.Count)
            throw new InvalidSubmissionException("An option cannot be selected more than once.");

        var selectedIds = selectedOptionIds.ToHashSet();
        var questionOptions = question.Options.ToDictionary(option => option.Id);

        if (selectedIds.Any(id => !questionOptions.ContainsKey(id)))
            throw new InvalidSubmissionException(
                "Every selected option must belong to the question being answered.");

        if (question.Type is QuestionType.SingleChoice or QuestionType.TrueFalse && selectedIds.Count != 1)
            throw new InvalidSubmissionException(
                $"{question.Type} questions require exactly one selected option.");

        var correctIds = questionOptions.Values
            .Where(option => option.IsCorrect)
            .Select(option => option.Id)
            .ToHashSet();

        return selectedIds.SetEquals(correctIds);
    }
}
