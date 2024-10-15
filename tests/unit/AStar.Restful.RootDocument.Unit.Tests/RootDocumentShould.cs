using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace AStar.Restful.RootDocument.Unit.Tests;

public class RootDocumentShould
{
    [Fact]
    public void Return()
    {
        var sut = new RootDocument(new NullLogger<RootDocument>())   ;

        var response = sut.GetLinks(Assembly.GetExecutingAssembly(), CancellationToken.None) ;

        _ = response.Should().BeEquivalentTo(new[]
                        { new LinkResponse() { Rel = "self", Href = "/delete/{id}", Method = "DELETE" },
                        new LinkResponse() { Rel = "self", Href = "/", Method = "GET" },
                        new LinkResponse() { Rel = "self", Href = "/{id}", Method = "GET" },
                        new LinkResponse() { Rel = "self", Href = "/something/{id}", Method = "GET" },
                        new LinkResponse() { Rel = "self", Href = "/something", Method = "GET" },
                        new LinkResponse() { Rel = "self", Href = "/patch/{id}", Method = "PATCH" },
                        new LinkResponse() { Rel = "self", Href = "/post/{id}", Method = "POST" },
                        new LinkResponse() { Rel = "self", Href = "/put/{id}", Method = "PUT" }
                        });
    }
}
