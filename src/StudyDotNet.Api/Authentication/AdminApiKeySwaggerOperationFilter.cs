using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StudyDotNet.Api.Authentication;

public sealed class AdminApiKeySwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.MethodInfo.IsDefined(typeof(AuthorizeAttribute), inherit: true))
        {
            return;
        }

        operation.Security ??= [];
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(
                AdminApiKeyDefaults.Scheme,
                context.Document)] = []
        });
    }
}
