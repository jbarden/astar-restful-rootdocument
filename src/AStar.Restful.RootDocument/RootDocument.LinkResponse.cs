namespace AStar.Restful.RootDocument;

/// <summary>
/// The <see cref="LinkResponse"/> class containing the HATEOAS links.
/// </summary>
public class LinkResponse
{
    /// <summary>
    /// The Rel link.
    /// </summary>
    public string Rel { get; internal set; } = string.Empty;

    /// <summary>
    /// The HRef <see langword="for"/>the link.
    /// </summary>
    public string Href { get; internal set; } = string.Empty;

    /// <summary>
    /// The applicable HTTP Method (GET, POST, etc.).
    /// </summary>
    public string Method { get; internal set; } = "GET";
}
