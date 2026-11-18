using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Abstractions.Services;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.QuestionAttempts;

namespace StudyDotNet.Application.Questions.Commands.SubmitAnswer;

public sealed class SubmitAnswerCommandHandler(
    IQuestionRepository questionRepository,
    IQuestionAttemptRepository attemptRepository,
    IQuestionAnswerEvaluator answerEvaluator,
    TimeProvider timeProvider)
    : IRequestHandler<SubmitAnswerCommand, AttemptResultResponse>
{
    public async Task<AttemptResultResponse> Handle(
        SubmitAnswerCommand request,
        CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetPublishedByIdAsync(
            request.QuestionId,
            cancellationToken)
            ?? throw new ResourceNotFoundException("Question", request.QuestionId);

        var isCorrect = answerEvaluator.Evaluate(question, request.SelectedOptionIds);
        var attempt = QuestionAttempt.Submit(
            question.Id,
            userId: null,
            request.SelectedOptionIds,
            isCorrect,
            timeProvider.GetUtcNow());

        await attemptRepository.AddAsync(attempt, cancellationToken);

        return AttemptResultResponse.From(attempt, question);
    }
}
