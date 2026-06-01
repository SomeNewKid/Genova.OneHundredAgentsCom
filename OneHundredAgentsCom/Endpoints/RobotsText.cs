// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Execution;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Maps the GET route for /robots.txt and serves the embedded wwwroot/robots.txt resource.
/// Returns 404 when the embedded resource is missing.
/// </summary>
internal static class RobotsText
{
    private const string RobotsContentType = "text/plain; charset=utf-8";

    /// <summary>
    /// Maps the robots text file endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the robots text file endpoint.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/robots.txt", (HttpContext httpContext, IExecutionContextAccessor executionContextAccessor) =>
        {
            // The robots.txt file is expected to live beside the index.html for the site root:
            // embedded resource path: "wwwroot/robots.txt"
            string normalizedPath = WwwFiles.NormalizeFilePath("wwwroot/robots.txt");

            Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), normalizedPath);
            if (stream == null)
            {
                return Results.NotFound();
            }

            using StreamReader reader = new(stream);
            string content = reader.ReadToEnd();

            return Results.Content(content, RobotsContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
