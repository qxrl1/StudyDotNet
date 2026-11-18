using MediatR;
using StudyDotNet.Application.Common;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.ValueObjects;

namespace StudyDotNet.Application.Questions.Queries.GetQuestions;

public sealed record GetQuestionsQuery(
    string? TopicSlug,
    Difficulty? Difficulty,
    int Page,
    int PageSize) : IRequest<PagedResult<QuestionSummaryResponse>>;
