// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Html;
using Genova.Common.Razor;
using Genova.Common.Websites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping miscellaneous hello endpoints.
/// </summary>
[SuppressMessage(
    "CodeQuality",
    "IDE0079:Remove unnecessary suppression",
    Justification = "Unused route parameter required by ASP.NET routing")]
internal static class HelloPages
{
    private const string HtmlContentType = "text/html; charset=utf-8";
    private const string NotFoundHeadingLocalizationKey = "ENGINE_ERROR_NotFound_Heading";

    /// <summary>
    /// Maps the miscellaneous hello endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the miscellaneous hello endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/hello", (HttpContext httpContext) =>
        {
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Hello</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Hello</h1>
                    <p>Version 0.2</p>
                    <!-- HelloPages -->
                </body>
                </html>
                """).ToString();

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/image", (HttpContext httpContext) =>
        {
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Hello Image</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Hello Image</h1>
                    <img src="/-/images/logo-opengraph.png" alt="Grubenwald looks upon his automaton" />
                </body>
                </html>
                """).ToString();

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/request", (HttpContext httpContext) =>
        {
            string method = httpContext.Request.Method;
            PathString path = httpContext.Request.Path;

            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Request Details</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Request Details</h1>
                    <pre>Request: {method} {path}</pre>
                    <!-- HelloPages -->
                </body>
                </html>
                """).ToString();

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/exception", (HttpContext httpContext) =>
        {
            throw new InvalidOperationException("This is a test exception for integration testing purposes.");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet("/hello/cookie", (HttpContext httpContext) =>
        {
            // Get the data protection provider
            IDataProtector protector = httpContext.RequestServices
                .GetRequiredService<IDataProtectionProvider>()
                .CreateProtector("HelloCookieProtector");

            // Encrypt the cookie value
            string encryptedValue = protector.Protect("Hello, World!");

            // Add the encrypted cookie to the response
            httpContext.Response.Cookies.Append("Hello", encryptedValue);

            // Return the HTML response
            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Hello, Cookie</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>Hello, Cookie</h1>
                    <!-- HelloPages -->
                </body>
                </html>
                """).ToString();

            return Results.Content(html, HtmlContentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        #pragma warning disable ASP0018 // Unused route parameter
        endpoints.MapGet("/hello/{*slugs}", (HttpContext httpContext) =>
        {
            IServiceProvider services = httpContext.RequestServices;
            IStringLocalizer localizer = services.GetRequiredService<IStringLocalizer>();

            string html = new HtmlBuilder().Append($"""
                <!DOCTYPE html>
                <html>
                <head>
                    <title>{localizer[NotFoundHeadingLocalizationKey]}</title>
                    <meta charset="UTF-8">
                </head>
                <body>
                    <h1>{localizer[NotFoundHeadingLocalizationKey]}</h1>
                    <p>{localizer["ENGINE_ERROR_NotFound_Message"]}</p>
                    <!-- HelloPages -->
                </body>
                </html>
                """).ToString();

            return Results.Content(html, HtmlContentType)
                          .WithStatusCode(StatusCodes.Status404NotFound);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
        #pragma warning restore ASP0018 // Unused route parameter
    }
}
