using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Common;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetQuestions;

public sealed class GetQuestionsQueryHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionsQuery, PagedResult<QuestionSummaryResponse>>
{
    public Task<PagedResult<QuestionSummaryResponse>> Handle(
        GetQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Page < 1)
        {
            throw new InvalidRequestException("Page must be greater than zero.");
        }

        if (request.PageSize is < 1 or > 100)
        {
            throw new InvalidRequestException("Page size must be between 1 and 100.");
        }

        if (request.TopicSlug is not null && string.IsNullOrWhiteSpace(request.TopicSlug))
        {
            throw new InvalidRequestException("Topic slug cannot be blank.");
        }

        return questionRepository.GetPublishedPageAsync(
            request.TopicSlug,
            request.Difficulty,
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}
