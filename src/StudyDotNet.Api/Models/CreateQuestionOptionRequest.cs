using System.ComponentModel.DataAnnotations;
using StudyDotNet.Domain.Entities.QuestionOptions;

namespace StudyDotNet.Api.Models;

public sealed class CreateQuestionOptionRequest
{
    [Required]
    [StringLength(QuestionOption.TEXT_MAX_LENGTH, MinimumLength = QuestionOption.TEXT_MIN_LENGTH)]
    public string Text { get; init; } = string.Empty;

    public bool IsCorrect { get; init; }

    [Range(1, int.MaxValue)]
    public int Order { get; init; }
}
