using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace StudyDotNet.Api.Authentication;

public sealed class AdminApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
ILoggerFactory logger, UrlEncoder encoder, IConfiguration configuration)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var configuredApiKey = configuration["Admin:ApiKey"];
        if (string.IsNullOrEmpty(configuredApiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail(
                "Admin API key authentication is not configured."));
        }

        if (!Request.Headers.TryGetValue(AdminApiKeyDefaults.HeaderName, out var suppliedValues)
            || suppliedValues.Count != 1)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var suppliedKey = suppliedValues[0];
        if (string.IsNullOrEmpty(suppliedKey) || suppliedKey.Length > 256)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var expectedHash = SHA256.HashData(Encoding.UTF8.GetBytes(configuredApiKey));
        var suppliedHash = SHA256.HashData(Encoding.UTF8.GetBytes(suppliedKey));

        if (!CryptographicOperations.FixedTimeEquals(expectedHash, suppliedHash))
        {
            return Task.FromResult(AuthenticateResult.Fail("The admin API key is invalid."));
        }

        var identity = new ClaimsIdentity([new Claim(ClaimTypes.Role, "Admin")], AdminApiKeyDefaults.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AdminApiKeyDefaults.Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
