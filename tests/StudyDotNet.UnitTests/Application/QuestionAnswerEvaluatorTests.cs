using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Evaluation;
using StudyDotNet.Domain.Entities;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.UnitTests.Application;

public sealed class QuestionAnswerEvaluatorTests
{
    private readonly QuestionAnswerEvaluator _evaluator = new();

    [Fact]
    public void Evaluate_SingleChoiceWithCorrectOption_ReturnsTrue()
    {
        var question = CreateQuestion(
            QuestionType.SingleChoice,
            new NewQuestionOption("Correct", true, 1),
            new NewQuestionOption("Incorrect", false, 2));
        question.Publish();

        var correctOptionId = question.Options.Single(option => option.IsCorrect).Id;

        Assert.True(_evaluator.Evaluate(question, [correctOptionId]));
    }

    [Fact]
    public void Evaluate_MultipleChoiceWithOnlyPartOfCorrectSet_ReturnsFalse()
    {
        var question = CreateQuestion(
            QuestionType.MultipleChoice,
            new NewQuestionOption("Correct A", true, 1),
            new NewQuestionOption("Correct B", true, 2),
            new NewQuestionOption("Incorrect", false, 3));
        question.Publish();

        var selectedOptionId = question.Options.First(option => option.IsCorrect).Id;

        Assert.False(_evaluator.Evaluate(question, [selectedOptionId]));
    }

    [Fact]
    public void Evaluate_MultipleChoiceWithExactCorrectSet_ReturnsTrue()
    {
        var question = CreateQuestion(
            QuestionType.MultipleChoice,
            new NewQuestionOption("Correct A", true, 1),
            new NewQuestionOption("Correct B", true, 2),
            new NewQuestionOption("Incorrect", false, 3));
        question.Publish();

        var selectedOptionIds = question.Options
            .Where(option => option.IsCorrect)
            .Select(option => option.Id)
            .ToArray();

        Assert.True(_evaluator.Evaluate(question, selectedOptionIds));
    }

    [Fact]
    public void Evaluate_OptionFromAnotherQuestion_Throws()
    {
        var question = CreateQuestion(
            QuestionType.SingleChoice,
            new NewQuestionOption("Correct", true, 1),
            new NewQuestionOption("Incorrect", false, 2));
        question.Publish();

        Assert.Throws<InvalidSubmissionException>(() =>
            _evaluator.Evaluate(question, [Guid.NewGuid()]));
    }

    [Fact]
    public void Evaluate_UnpublishedQuestion_Throws()
    {
        var question = CreateQuestion(
            QuestionType.SingleChoice,
            new NewQuestionOption("Correct", true, 1),
            new NewQuestionOption("Incorrect", false, 2));

        Assert.Throws<InvalidSubmissionException>(() =>
            _evaluator.Evaluate(question, [question.Options.First().Id]));
    }

    [Fact]
    public void Evaluate_DuplicateSelection_Throws()
    {
        var question = CreateQuestion(
            QuestionType.SingleChoice,
            new NewQuestionOption("Correct", true, 1),
            new NewQuestionOption("Incorrect", false, 2));
        question.Publish();

        var optionId = question.Options.First().Id;

        Assert.Throws<InvalidSubmissionException>(() =>
            _evaluator.Evaluate(question, [optionId, optionId]));
    }

    private static Question CreateQuestion(
        QuestionType type,
        params NewQuestionOption[] options)
    {
        return Question.Create(
            Guid.NewGuid(),
            "Title",
            "Statement",
            "Explanation",
            Difficulty.Beginner,
            type,
            options);
    }
}
