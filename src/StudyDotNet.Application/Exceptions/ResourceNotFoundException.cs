namespace StudyDotNet.Application.Exceptions;

public sealed class ResourceNotFoundException(string resourceName, Guid id)
    : Exception($"{resourceName} '{id}' was not found.");
