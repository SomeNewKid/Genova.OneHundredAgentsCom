// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Execution;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping miscellaneous hello endpoints.
/// </summary>
[SuppressMessage(
    "CodeQuality",
    "IDE0079:Remove unnecessary suppression",
    Justification = "Unused route parameter required by ASP.NET routing")]
internal static class MarkdownPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";

    /// <summary>
    /// Maps the miscellaneous hello endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the miscellaneous hello endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        #pragma warning disable ASP0018 // Unused route parameter
        endpoints.MapGet("/{*slugs}", (HttpContext httpContext, IExecutionContextAccessor executionContextAccessor) =>
        {
            IExecutionContext executionContext = executionContextAccessor.Current;

            string path = StringHelper.GetNonNullValue(httpContext.Request.Path.Value);
            string query = StringHelper.GetNonNullValue(httpContext.Request.QueryString.Value);

            string extension = FileHelper.GetFileExtension(path);
            if (!string.IsNullOrEmpty(extension))
            {
                return Results.NotFound();
            }

            string? html = PageRenderer.RenderMarkdown(executionContext.RequestContext, path, query);
            if (string.IsNullOrEmpty(html))
            {
                return Results.NotFound();
            }

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
        #pragma warning restore ASP0018 // Unused route parameter
    }
}
