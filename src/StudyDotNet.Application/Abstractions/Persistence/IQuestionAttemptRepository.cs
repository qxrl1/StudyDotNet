using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.QuestionAttempts;

namespace StudyDotNet.Application.Abstractions.Persistence;

public interface IQuestionAttemptRepository
{
    Task AddAsync(QuestionAttempt attempt, CancellationToken cancellationToken);

    Task<AttemptResultResponse?> GetByIdAsync(Guid attemptId, CancellationToken cancellationToken);
}
