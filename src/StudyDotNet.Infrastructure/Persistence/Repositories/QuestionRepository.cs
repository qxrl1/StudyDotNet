using Microsoft.EntityFrameworkCore;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Common;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.Questions;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Infrastructure.Persistence.Repositories;

internal sealed class QuestionRepository(StudyDotNetDbContext dbContext) : IQuestionRepository
{
    public async Task<Question?> GetPublishedByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        return await dbContext.Questions
            .AsNoTracking()
            .Include(question => question.Options)
            .SingleOrDefaultAsync(
                question => question.Id == questionId && question.IsPublished,
                cancellationToken);
    }

    public async Task<PagedResult<QuestionSummaryResponse>> GetPublishedPageAsync(string? topicSlug, 
        Difficulty? difficulty, int page, int pageSize,CancellationToken cancellationToken)
    {
        var questions =
            from question in dbContext.Questions.AsNoTracking()
            join topic in dbContext.Topics.AsNoTracking() on question.TopicId equals topic.Id
            where question.IsPublished
            select new { Question = question, TopicSlug = topic.Slug };

        if (topicSlug is not null)
            questions = questions.Where(item => item.TopicSlug == topicSlug);

        if (difficulty is not null)
            questions = questions.Where(item => item.Question.Difficulty == difficulty);

        var totalCount = await questions.CountAsync(cancellationToken);
        var offset = (long)(page - 1) * pageSize;
        if (offset > int.MaxValue)
            return new PagedResult<QuestionSummaryResponse>([], page, pageSize, totalCount);

        var items = await questions
            .OrderBy(item => item.Question.Id)
            .Skip((int)offset)
            .Take(pageSize)
            .Select(item => new QuestionSummaryResponse(
                item.Question.Id,
                item.Question.Title,
                item.Question.Difficulty,
                item.Question.Type,
                item.TopicSlug))
            .ToListAsync(cancellationToken);

        return new PagedResult<QuestionSummaryResponse>(items, page, pageSize, totalCount);
    }
}
