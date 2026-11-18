using StudyDotNet.Domain.Entities.QuestionOptions;
using StudyDotNet.Domain.Exceptions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Domain.Entities.Questions;

public sealed partial class Question
{
    public static Question Create(Guid topicId, string title, string statement, string explanation,
       Difficulty difficulty, QuestionType type, IEnumerable<NewQuestionOption> options)
    {
        if (topicId == Guid.Empty)
            throw new DomainRuleViolationException("A question must belong to a topic.");

        ValidateText(title, TITLE_MAX_LENGTH, "title");
        ValidateText(statement, STATEMENT_MAX_LENGTH, "statement");
        ValidateText(explanation, EXPLANATION_MAX_LENGTH, "explanation");

        if (!Enum.IsDefined(difficulty))
            throw new DomainRuleViolationException("Question difficulty is not supported.");

        if (!Enum.IsDefined(type))
            throw new DomainRuleViolationException("Question type is not supported.");

        ArgumentNullException.ThrowIfNull(options);

        var optionDefinitions = options.ToArray();
        ValidateOptions(type, optionDefinitions);

        var question = new Question(
            topicId,
            title.Trim(),
            statement.Trim(),
            explanation.Trim(),
            difficulty,
            type);

        question._options.AddRange(optionDefinitions.Select(option =>
            new QuestionOption(question.Id, option.Text, option.IsCorrect, option.Order)));

        return question;
    }

    public void Publish()
    {
        IsPublished = true;
    }

    private static void ValidateText(string? value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > maxLength)
            throw new DomainRuleViolationException(
                $"Question {fieldName} must contain between 1 and {maxLength} characters.");
    }

    private static void ValidateOptions(QuestionType type, IReadOnlyCollection<NewQuestionOption> options)
    {
        if (options.Count < 2)
            throw new DomainRuleViolationException("A question must have at least two options.");

        if (options.Any(option => option is null))
            throw new DomainRuleViolationException("Question options cannot contain null values.");

        if (options.Select(option => option.Order).Distinct().Count() != options.Count)
            throw new DomainRuleViolationException("Question option order values must be unique.");

        var correctCount = options.Count(option => option.IsCorrect);
        if (correctCount == 0)
            throw new DomainRuleViolationException("A question must have at least one correct option.");

        if (type is QuestionType.SingleChoice or QuestionType.TrueFalse && correctCount != 1)
            throw new DomainRuleViolationException(
                $"{type} questions must have exactly one correct option.");

        if (type is QuestionType.TrueFalse && options.Count != 2)
            throw new DomainRuleViolationException("TrueFalse questions must have exactly two options.");
    }
}
