namespace StudyDotNet.Api.Authentication;

public static class AdminApiKeyDefaults
{
    public const string Scheme = "AdminApiKey";
    public const string Policy = "AdminOnly";
    public const string HeaderName = "X-Admin-Api-Key";
}
