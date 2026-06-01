// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping unfriendly error pages.
/// </summary>
internal static class HelloUnfriendlyErrorPages
{
    /// <summary>
    /// Maps the unfriendly error pages to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the unfriendly error pages.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello/error-400", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-401", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-402", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-403", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-404", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-500", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/error-502", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            return Results.Empty;
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }
}
