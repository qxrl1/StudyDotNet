using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using StudyDotNet.Application;
using StudyDotNet.Infrastructure;
using StudyDotNet.Api;
using StudyDotNet.Api.Authentication;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AdminApiKeyDefaults.Scheme,
        new OpenApiSecurityScheme
        {
            Description = "Admin API key required for topic and question creation.",
            Name = AdminApiKeyDefaults.HeaderName,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

    options.OperationFilter<AdminApiKeySwaggerOperationFilter>();
});

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(AdminApiKeyDefaults.Scheme)
                .AddScheme<AuthenticationSchemeOptions, AdminApiKeyAuthenticationHandler>(AdminApiKeyDefaults.Scheme, _ => { });
builder.Services.AddAuthorization(o => o.AddPolicy(AdminApiKeyDefaults.Policy,
        policy => policy
            .AddAuthenticationSchemes(AdminApiKeyDefaults.Scheme)
            .RequireAuthenticatedUser()
            .RequireRole("Admin")));

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
