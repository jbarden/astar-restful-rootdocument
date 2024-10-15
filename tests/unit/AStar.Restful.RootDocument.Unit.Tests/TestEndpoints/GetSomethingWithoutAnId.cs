﻿using System.Net.Mime;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Restful.RootDocument.Unit.Tests.TestEndpoints;

[ApiController]
[Route("/something")]
[ApiVersion(1.0)]
public class GetSomethingWithoutAnId()
{
    /// <summary>
    /// Gets something.
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token.</param>
    /// <returns>The root document.</returns>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointDescription("This endpoint gets something")]
    [HttpGet]
    public ActionResult<int> GetRootDocument(CancellationToken cancellationToken)
                => cancellationToken.IsCancellationRequested
                    ? (ActionResult<int>)new BadRequestResult()
                    : (ActionResult<int>)new OkObjectResult(1);
}
