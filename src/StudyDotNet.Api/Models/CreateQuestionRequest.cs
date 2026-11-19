using System.ComponentModel.DataAnnotations;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Api.Models;

public sealed class CreateQuestionRequest
{
    [Required]
    public Guid TopicId { get; init; }

    [Required]
    [StringLength(Question.TITLE_MAX_LENGTH, MinimumLength = 1)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [StringLength(Question.STATEMENT_MAX_LENGTH, MinimumLength = 1)]
    public string Statement { get; init; } = string.Empty;

    [Required]
    [StringLength(Question.EXPLANATION_MAX_LENGTH, MinimumLength = 1)]
    public string Explanation { get; init; } = string.Empty;

    [Required]
    public Difficulty? Difficulty { get; init; }

    [Required]
    public QuestionType? Type { get; init; }

    [Required]
    [MinLength(2)]
    public IReadOnlyCollection<CreateQuestionOptionRequest> Options { get; init; } = [];
}
