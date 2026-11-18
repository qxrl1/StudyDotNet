using StudyDotNet.Domain.Entities;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.Exceptions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.UnitTests.Domain;

public sealed class QuestionTests
{
    [Fact]
    public void Create_SingleChoiceWithMultipleCorrectOptions_Throws()
    {
        var options = new[]
        {
            new NewQuestionOption("Option A", true, 1),
            new NewQuestionOption("Option B", true, 2)
        };

        Assert.Throws<DomainRuleViolationException>(() =>
            Question.Create(
                Guid.NewGuid(),
                "Title",
                "Statement",
                "Explanation",
                Difficulty.Beginner,
                QuestionType.SingleChoice,
                options));
    }

    [Fact]
    public void Create_TrueFalseWithMoreThanTwoOptions_Throws()
    {
        var options = new[]
        {
            new NewQuestionOption("True", true, 1),
            new NewQuestionOption("False", false, 2),
            new NewQuestionOption("Maybe", false, 3)
        };

        Assert.Throws<DomainRuleViolationException>(() =>
            Question.Create(
                Guid.NewGuid(),
                "Title",
                "Statement",
                "Explanation",
                Difficulty.Beginner,
                QuestionType.TrueFalse,
                options));
    }

    [Fact]
    public void Create_ValidMultipleChoice_PreservesOptionsAndStartsUnpublished()
    {
        var question = Question.Create(
            Guid.NewGuid(),
            "Title",
            "Statement",
            "Explanation",
            Difficulty.Intermediate,
            QuestionType.MultipleChoice,
            [
                new NewQuestionOption("Option A", true, 1),
                new NewQuestionOption("Option B", false, 2),
                new NewQuestionOption("Option C", true, 3)
            ]);

        Assert.False(question.IsPublished);
        Assert.Equal(3, question.Options.Count);
        Assert.Equal(2, question.Options.Count(option => option.IsCorrect));
    }
}
