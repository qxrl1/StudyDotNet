using StudyDotNet.Domain.Entities.Questions;

namespace StudyDotNet.Application.Abstractions.Services;

public interface IQuestionAnswerEvaluator
{
    bool Evaluate(Question question, IReadOnlyCollection<Guid> selectedOptionIds);
}
