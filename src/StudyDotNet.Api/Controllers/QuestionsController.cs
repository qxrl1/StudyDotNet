using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyDotNet.Api.Authentication;
using StudyDotNet.Api.Models;
using StudyDotNet.Application.Common;
using StudyDotNet.Application.Questions.Commands.CreateQuestion;
using StudyDotNet.Application.Questions.Commands.SubmitAnswer;
using StudyDotNet.Application.Questions.Contracts;
using StudyDotNet.Application.Questions.Queries.GetQuestionById;
using StudyDotNet.Application.Questions.Queries.GetQuestions;
using StudyDotNet.Domain.ValueObjects;
using StudyDotNet.Domain.Entities;

namespace StudyDotNet.Api.Controllers;

[ApiController]
[Route("api/questions")]
public sealed class QuestionsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResult<QuestionSummaryResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<QuestionSummaryResponse>>> Get(
        [FromQuery] string? topic,
        [FromQuery] Difficulty? difficulty,
        [FromQuery, Range(1, int.MaxValue)] int page = 1,
        [FromQuery, Range(1, 100)] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetQuestionsQuery(topic, difficulty, page, pageSize), cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<QuestionDetailsResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuestionDetailsResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var question = await sender.Send(new GetQuestionByIdQuery(id), cancellationToken);
        return Ok(question);
    }

    [HttpPost]
    [Authorize(Policy = AdminApiKeyDefaults.Policy)]
    [ProducesResponseType<QuestionDetailsResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuestionDetailsResponse>> Create(CreateQuestionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateQuestionCommand(
                request.TopicId,
                request.Title,
                request.Statement,
                request.Explanation,
                request.Difficulty!.Value,
                request.Type!.Value,
                request.Options.Select(option =>
                    new NewQuestionOption(option.Text, option.IsCorrect, option.Order)).ToArray());

        var question = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = question.Id }, question);
    }

    [HttpPost("{id:guid}/attempts")]
    [ProducesResponseType<AttemptResultResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AttemptResultResponse>> SubmitAnswer(Guid id, SubmitAnswerRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new SubmitAnswerCommand(id, request.SelectedOptionIds), cancellationToken);

        return CreatedAtRoute("GetAttemptById", new { id = result.AttemptId }, result);
    }
}
