using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetQuestionById;

public sealed class GetQuestionByIdQueryHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionByIdQuery, QuestionDetailsResponse>
{
    public async Task<QuestionDetailsResponse> Handle(
        GetQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetPublishedByIdAsync(request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException("Question", request.Id);

        return new QuestionDetailsResponse(
            question.Id,
            question.Title,
            question.Statement,
            question.Difficulty,
            question.Type,
            question.Options
                .OrderBy(option => option.Order)
                .Select(option => new QuestionOptionResponse(option.Id, option.Text, option.Order))
                .ToArray());
    }
}
