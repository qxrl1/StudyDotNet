namespace StudyDotNet.Application.Exceptions;

public sealed class InvalidRequestException(string message) : Exception(message)
{
}
