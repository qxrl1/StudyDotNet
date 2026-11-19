using System.ComponentModel.DataAnnotations;

namespace StudyDotNet.Api.Models;

public sealed class SubmitAnswerRequest
{
    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<Guid> SelectedOptionIds { get; init; } = [];
}
