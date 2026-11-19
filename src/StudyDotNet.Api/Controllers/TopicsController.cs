using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyDotNet.Api.Authentication;
using StudyDotNet.Api.Models;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Application.Questions.Commands.CreateTopic;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Application.Questions.Queries.GetTopics;

namespace StudyDotNet.Api.Controllers;

[ApiController]
[Route("api/topics")]
public sealed class TopicsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<TopicResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TopicResponse>>> Get(
        CancellationToken cancellationToken)
    {
        var topics = await sender.Send(new GetTopicsQuery(), cancellationToken);
        return Ok(topics);
    }

    [HttpPost]
    [Authorize(Policy = AdminApiKeyDefaults.Policy)]
    [ProducesResponseType<TopicResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TopicResponse>> Create(
        CreateTopicRequest request,
        CancellationToken cancellationToken)
    {
        var topic = await sender.Send(
            new CreateTopicCommand(request.Name, request.Slug),
            cancellationToken);

        return Created("/api/topics", topic);
    }
}
