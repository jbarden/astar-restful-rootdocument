using System.Net.Mime;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AStar.Restful.RootDocument.Unit.Tests.TestEndpoints;

[ApiController]
[Route("/patch")]
[ApiVersion(1.0)]
internal class PatchSomething
{
    /// <summary>
    /// Patches something.
    /// </summary>
    /// <param name="cancellationToken">An optional cancellation token.</param>
    /// <returns>The root document.</returns>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointDescription("Patches something description...")]
    [HttpPatch("{id}")]
    public ActionResult JsonPatchWithModelState(
    [FromBody] JsonPatchDocument<Customer> patchDoc)
        => patchDoc != null
                        ? new ObjectResult(new Customer())
                        : new BadRequestResult();

    internal partial class Customer
    {
    }
}
