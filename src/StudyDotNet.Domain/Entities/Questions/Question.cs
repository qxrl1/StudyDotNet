using StudyDotNet.Domain.Entities.QuestionOptions;
using StudyDotNet.Domain.Exceptions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Domain.Entities.Questions;

public sealed partial class Question : BaseEntity
{
    private readonly List<QuestionOption> _options = [];

    public Guid TopicId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Statement { get; private set; } = string.Empty;

    public string Explanation { get; private set; } = string.Empty;

    public Difficulty Difficulty { get; private set; }

    public QuestionType Type { get; private set; }

    public bool IsPublished { get; private set; }

    public IReadOnlyCollection<QuestionOption> Options => _options.AsReadOnly();

    private Question()
    {
    }

    private Question(Guid topicId, string title, string statement, string explanation, Difficulty difficulty, QuestionType type)
    {
        TopicId = topicId;
        Title = title;
        Statement = statement;
        Explanation = explanation;
        Difficulty = difficulty;
        Type = type;
    }
}
