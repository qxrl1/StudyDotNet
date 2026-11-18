namespace StudyDotNet.Domain.Exceptions;

public sealed class DomainRuleViolationException(string message) : Exception(message)
{
}
