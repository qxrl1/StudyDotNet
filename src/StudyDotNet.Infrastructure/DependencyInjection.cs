using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyDotNet.Infrastructure.Persistence;
using StudyDotNet.Infrastructure.Persistence.Repositories;
using StudyDotNet.Application.Abstractions.Persistence;

namespace StudyDotNet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("StudyDotNetSql")
            ?? throw new InvalidOperationException("ConnectionStrings:StudyDotNetSql configuration value is required.");

        services.AddDbContext<StudyDotNetDbContext>(options => options.UseNpgsql(connectionString));
        
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuestionAttemptRepository, QuestionAttemptRepository>();

        return services;
    }
}
