// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping scanner endpoints.
/// </summary>
/// <remarks>
/// For more information, see https://www.scanners.org/protocol.html.
/// </remarks>
internal static class HelloScannerPage
{
    /// <summary>
    /// Maps the scanner endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the scanner endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello/scanner", (HttpContext httpContext, IExecutionContextAccessor executionContextAccessor) =>
        {
            IExecutionContext executionContext = executionContextAccessor.Current;

            string query = StringHelper.GetNonNullValue(httpContext.Request.QueryString.Value);

            IEnumerable<string?> routes = GetRoutes(httpContext);

            // Build the HTML scanner content
            string listItems = string.Join("\n", routes.Select(route => $"""
                <li><a href="{route}">{route}</a></li>
            """));

            string fragment = new HtmlBuilder().Append($"""
                <h1>Scanner</h1>
                <ul>
                    {listItems:raw}
                </ul>
                """).ToString();

            string? html = PageRenderer.RenderHtml(executionContext.RequestContext, fragment, query);

            return Results.Text(html, "text/html");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Checks if the given route is to a file with an extension.
    /// </summary>
    /// <param name="route">The route.</param>
    /// <returns><c>true</c> if the route is to a file with an extension, otherwise <c>false</c>.</returns>
    internal static bool HasFileExtension(string route)
    {
        string[] slugs = route.Split('?')[0].Split('/');
        string finalSlug = slugs[slugs.Length - 1];
        return finalSlug.Contains('.');
    }

    /// <summary>
    /// Converts an embedded file name to a scanner route if it represents a markdown page.
    /// </summary>
    /// <param name="embeddedFile">The embedded file resource name.</param>
    /// <returns>The route string if the file should be included in the scanner; otherwise, <c>null</c>.</returns>
    internal static string? GetMarkdownRoute(string embeddedFile)
    {
        if (string.IsNullOrWhiteSpace(embeddedFile))
        {
            return null;
        }

        // Must end with ".md"
        if (!embeddedFile.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        // Must start with the expected prefix
        const string prefix = "Genova.OneHundredAgentsCom.wwwroot.";
        if (!embeddedFile.StartsWith(prefix, StringComparison.Ordinal))
        {
            return null;
        }

        // Remove prefix and ".md" extension
        string path = embeddedFile.Substring(prefix.Length, embeddedFile.Length - prefix.Length - 3);

        // If the path is empty or whitespace, it's not a valid route
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        // Split by '.' to get path segments
        string[] segments = path.Split('.');

        // Handle index.md at root or in folders
        if (segments.Length == 1 && segments[0] == "index")
        {
            return "/";
        }

        if (segments.Length > 1 && segments[^1] == "index")
        {
            // Remove the last "index" segment
            string route = "/" + string.Join('/', segments, 0, segments.Length - 1);
            return route;
        }

        // Otherwise, join as a normal path
        return "/" + string.Join('/', segments);
    }

    /// <summary>
    /// Retrieves all valid routes for the scanner.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>A collection of valid routes.</returns>
    private static HashSet<string?> GetRoutes(HttpContext httpContext)
    {
        // Get the EndpointDataSource service
        EndpointDataSource endpointDataSource =
            httpContext.RequestServices.GetRequiredService<EndpointDataSource>();

        // Retrieve all registered endpoints and exclude those with pattern matching or file extensions
        IEnumerable<string?> routes = endpointDataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Where(endpoint => IsGetMethod(endpoint))
            .Where(endpoint => endpoint.Metadata.GetMetadata<WebsiteRouteInfo>()?.WebsiteId == Website.Identifier)
            .Select(endpoint => endpoint.RoutePattern.RawText)
            .Where(route => route != null
                            && !route.Contains('{')
                            && !route.Contains('}')
                            && !HasFileExtension(route))
            .Distinct();

        // Modify the routes collection
        return ModifyRoutes(routes);
    }

    /// <summary>
    /// Modifies the routes collection by removing and adding specific routes.
    /// </summary>
    /// <param name="routes">The original collection of routes.</param>
    /// <returns>The modified collection of routes.</returns>
    private static HashSet<string?> ModifyRoutes(IEnumerable<string?> routes)
    {
        // Convert to a HashSet for efficient modification
        HashSet<string?> routeSet = [.. routes];

        return routeSet;
    }

    private static bool IsGetMethod(RouteEndpoint endpoint)
    {
        HttpMethodMetadata? httpMethodMetadata = endpoint.Metadata.GetMetadata<HttpMethodMetadata>();
        return httpMethodMetadata?.HttpMethods.Contains("GET") ?? false;
    }
}
