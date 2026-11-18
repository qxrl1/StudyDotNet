using System.Text.RegularExpressions;
using StudyDotNet.Domain.Exceptions;

namespace StudyDotNet.Domain.Entities;

public sealed class Topic : BaseEntity
{
    private static readonly Regex SlugPattern = new("^[a-z0-9]+(?:-[a-z0-9]+)*$", RegexOptions.Compiled);

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public static readonly int NAME_MAX_LENGTH = 100;
    public static readonly int NAME_MIN_LENGTH = 1;

    public static readonly int SLUG_MAX_LENGTH = 120;

    private Topic()
    {
    }

    private Topic(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }

    public static Topic Create(string name, string slug)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > NAME_MAX_LENGTH)
            throw new DomainRuleViolationException($"Topic name must contain between {NAME_MIN_LENGTH} and {NAME_MAX_LENGTH} characters.");

        if (string.IsNullOrWhiteSpace(slug) || slug.Length > SLUG_MAX_LENGTH || !SlugPattern.IsMatch(slug))
            throw new DomainRuleViolationException(
                "Topic slug must contain lowercase letters, numbers, and single hyphens only.");

        return new Topic(name.Trim(), slug);
    }
}
