﻿using System.Net.Mime;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Restful.RootDocument.Unit.Tests.TestEndpoints;

[ApiController]
[Route("/put")]
[ApiVersion(1.0)]
internal class PutSomething
{
    /// <summary>
    /// Updates something...
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token.</param>
    /// <returns>Whether the item was deleted or not.</returns>  [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointDescription("Updates something description...")]
    [HttpPut("{id}")]
    public ActionResult<bool> UpdateTask(CancellationToken cancellationToken)
            => cancellationToken.IsCancellationRequested
                    ? (ActionResult<bool>)new BadRequestResult()
                    : (ActionResult<bool>)new OkObjectResult(true);
}
