using System.Reflection;
using System.Text;
using AStar.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AStar.Restful.RootDocument;

/// <summary>
/// The <see cref="RootDocument"/> class used to retrieve the default root document.
/// </summary>
public class RootDocument
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public List<LinkResponse> GetLinks(Assembly assembly, CancellationToken cancellationToken = default)
    {
        var links = new List<LinkResponse>();
        if(cancellationToken.IsCancellationRequested)
        {
            return links;
        }

        var endpoints = GetEndpoints(assembly);

        foreach(var endpoint in endpoints)
        {
            var customAttributes = endpoint.GetCustomAttributes(typeof(RouteAttribute)).FirstOrDefault() as RouteAttribute;
            var rel = endpoint.ReflectedType?.Name;
            var methods = endpoint.GetMethods().Where(f=>f.DeclaringType?.FullName?.Contains("Endpoint") == true);
            var routeBuilder = new StringBuilder();
            _ = routeBuilder.Append(customAttributes?.Template ?? "");

            if(methods.Any())
            {
                foreach(var method in methods)
                {
                    var template = string.Empty;
                    var httpMethod = string.Empty;
                    var httpMethodAttributes = method?.GetCustomAttributes(typeof(HttpMethodAttribute));
                    if(httpMethodAttributes is not null)
                    {
                        foreach(var httpMethodAttribute in httpMethodAttributes)
                        {
                            var res=  GetHttpMethodWithTemplate(httpMethodAttribute);
                            template = res.template;
                            httpMethod = res.httpMethod;
                        }
                    }

                    var routeTemplate = template.IsNotNullOrWhiteSpace() ? $"/{template}" : string.Empty;
                    _ = routeBuilder.Append(routeTemplate);
                    var route = routeBuilder.ToString();
                    if(route.IsNotNullOrWhiteSpace())
                    {
                        links.Add(new LinkResponse() { Rel = rel ?? "self", Href = route, Method = httpMethod });
                    }
                }
            }
        }

        return links;
    }

    private static (string httpMethod, string? template) GetHttpMethodWithTemplate(Attribute httpMethodAttribute)
    {
        string httpMethod;
        string? template;
        var getAttribute = httpMethodAttribute as HttpGetAttribute;
        var postAttribute = httpMethodAttribute as HttpPostAttribute;
        var deleteAttribute = httpMethodAttribute as HttpDeleteAttribute;
        var putAttribute = httpMethodAttribute as HttpPutAttribute;
        var patchAttribute = httpMethodAttribute as HttpPatchAttribute;
        if(getAttribute is not null)
        {
            template = getAttribute.Template;
            httpMethod = "GET";
        }
        else if(postAttribute is not null)
        {
            template = postAttribute.Template;
            httpMethod = "POST";
        }
        else if(deleteAttribute is not null)
        {
            template = deleteAttribute.Template;
            httpMethod = "DELETE";
        }
        else if(putAttribute is not null)
        {
            template = putAttribute.Template;
            httpMethod = "PUT";
        }
        else if(patchAttribute is not null)
        {
            template = patchAttribute.Template;
            httpMethod = "PATCH";
        }
        else
        {
            throw new NotImplementedException();
        }

        return (httpMethod, template);
    }

    private static IEnumerable<Type> GetEndpoints(Assembly assembly)
        => assembly.GetTypes().Where(t => t.Namespace?.Contains("Endpoint") == true
            && !t.Name.EndsWith("Response")
            && !t.Name.EndsWith("Put") && !t.Name.EndsWith("Patch")
            && !t.Name.EndsWith("Delete")
            && !t.Name.EndsWith("Post")
            && !t.Name.EndsWith("Create")
        && !t.Name.StartsWith('<'));
}
