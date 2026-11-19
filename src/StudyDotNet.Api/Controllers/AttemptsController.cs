using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Application.Questions.Queries.GetAttemptById;

namespace StudyDotNet.Api.Controllers;

[ApiController]
[Route("api/attempts")]
public sealed class AttemptsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}", Name = "GetAttemptById")]
    [ProducesResponseType<AttemptResultResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AttemptResultResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var attempt = await sender.Send(new GetAttemptByIdQuery(id), cancellationToken);
        return Ok(attempt);
    }
}
