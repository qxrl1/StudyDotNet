using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;

namespace StudyDotNet.Application.Questions.Queries.GetAttemptById;

public sealed class GetAttemptByIdQueryHandler(IQuestionAttemptRepository attemptRepository)
    : IRequestHandler<GetAttemptByIdQuery, AttemptResultResponse>
{
    public async Task<AttemptResultResponse> Handle(
        GetAttemptByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await attemptRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException("Question attempt", request.Id);
    }
}
