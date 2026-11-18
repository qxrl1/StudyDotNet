using StudyDotNet.Application.Common;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Abstractions.Persistence;

public interface IQuestionRepository
{
    Task<Question?> GetPublishedByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<PagedResult<QuestionSummaryResponse>> GetPublishedPageAsync(
        string? topicSlug,
        Difficulty? difficulty,
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}
