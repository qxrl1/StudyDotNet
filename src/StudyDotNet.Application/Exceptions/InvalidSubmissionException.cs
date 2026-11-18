namespace StudyDotNet.Application.Exceptions;

public sealed class InvalidSubmissionException(string message) : Exception(message)
{
}
