using Microsoft.EntityFrameworkCore;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.QuestionAttempts;

namespace StudyDotNet.Infrastructure.Persistence.Repositories;

internal sealed class QuestionAttemptRepository(StudyDotNetDbContext dbContext)
    : IQuestionAttemptRepository
{
    public async Task AddAsync(QuestionAttempt attempt, CancellationToken cancellationToken)
    {
        dbContext.QuestionAttempts.Add(attempt);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<AttemptResultResponse?> GetByIdAsync(Guid attemptId, CancellationToken cancellationToken)
    {
        var attempt = await dbContext.QuestionAttempts
            .AsNoTracking()
            .Include(item => item.SelectedOptions)
            .SingleOrDefaultAsync(item => item.Id == attemptId, cancellationToken);

        if (attempt is null)
            return null;

        var question = await dbContext.Questions
            .AsNoTracking()
            .Include(item => item.Options)
            .SingleOrDefaultAsync(item => item.Id == attempt.QuestionId, cancellationToken)
            ?? throw new InvalidOperationException(
                $"Question '{attempt.QuestionId}' for attempt '{attempt.Id}' is missing.");

        return AttemptResultResponse.From(attempt, question);
    }
}
