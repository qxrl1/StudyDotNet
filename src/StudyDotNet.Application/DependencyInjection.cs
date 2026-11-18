using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudyDotNet.Application.Abstractions.Services;
using StudyDotNet.Application.Questions.Evaluation;

namespace StudyDotNet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IQuestionAnswerEvaluator, QuestionAnswerEvaluator>();

        return services;
    }
}
