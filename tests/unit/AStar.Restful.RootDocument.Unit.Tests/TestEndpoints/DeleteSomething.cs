using System.Net.Mime;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Restful.RootDocument.Unit.Tests.TestEndpoints;

[ApiController]
[Route("/delete")]
[ApiVersion(1.0)]
public class DeleteSomething()
{
    /// <summary>
    /// Deletes something...
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token.</param>
    /// <returns>Whether the item was deleted or not.</returns>  [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointDescription("Deletes something description...")]
    [HttpDelete("{id}")]
    public ActionResult<int> DeleteTask(int id, CancellationToken cancellationToken)
            => cancellationToken.IsCancellationRequested
                    ? (ActionResult<int>)new BadRequestResult()
                    : (ActionResult<int>)new OkObjectResult(id);
}
