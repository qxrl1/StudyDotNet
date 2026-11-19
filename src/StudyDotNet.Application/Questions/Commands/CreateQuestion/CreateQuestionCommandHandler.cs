using MediatR;
using StudyDotNet.Application.Abstractions.Persistence;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Application.Questions.Commands.CreateQuestion;

public sealed class CreateQuestionCommandHandler(
    ITopicRepository topicRepository,
    IQuestionRepository questionRepository)
    : IRequestHandler<CreateQuestionCommand, QuestionDetailsResponse>
{
    public async Task<QuestionDetailsResponse> Handle(
        CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        if (!await topicRepository.ExistsByIdAsync(request.TopicId, cancellationToken))
        {
            throw new ResourceNotFoundException("Topic", request.TopicId);
        }

        var question = Question.Create(
            request.TopicId,
            request.Title,
            request.Statement,
            request.Explanation,
            request.Difficulty,
            request.Type,
            request.Options);
        question.Publish();

        await questionRepository.AddAsync(question, cancellationToken);

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
