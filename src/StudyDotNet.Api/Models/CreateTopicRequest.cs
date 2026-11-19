using System.ComponentModel.DataAnnotations;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Api.Models;

public sealed class CreateTopicRequest
{
    [Required]
    [StringLength(Topic.NAME_MAX_LENGTH, MinimumLength = Topic.NAME_MIN_LENGTH)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [StringLength(Topic.SLUG_MAX_LENGTH, MinimumLength = 1)]
    public string Slug { get; init; } = string.Empty;
}
