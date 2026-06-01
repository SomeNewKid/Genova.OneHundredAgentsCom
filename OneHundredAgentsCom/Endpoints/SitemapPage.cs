// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Execution;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping sitemap endpoints.
/// </summary>
/// <remarks>
/// For more information, see https://www.sitemaps.org/protocol.html.
/// </remarks>
internal static class SitemapPage
{
    private const string HeroImage = "sitemap";

    /// <summary>
    /// Maps the sitemap endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the sitemap endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        MapSitemapXmlEndpoint(endpoints);
        MapSitemapHtmlEndpoint(endpoints);
    }

    private static void MapSitemapXmlEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/sitemap.xml", (HttpContext httpContext) =>
        {
            string domain = httpContext.Request.Host.ToString();
            AgentCatalogue catalogue = new();

            List<string> locs = [];
            locs.Add(SitemapLoc(domain, "/"));
            locs.Add(SitemapLoc(domain, "/sitemap"));
            locs.Add(SitemapLoc(domain, "/notices"));
            AppendAgentSitemapLocations(locs, domain, catalogue);

            string set = string.Join("\n", locs);

            string xml = $"""
                <urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
                {set}
                </urlset>
                """;

            return Results.Text(xml, "application/xml");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    private static void MapSitemapHtmlEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/sitemap", (HttpContext httpContext, IExecutionContextAccessor executionContextAccessor) =>
        {
            IExecutionContext executionContext = executionContextAccessor.Current;

            string query = StringHelper.GetNonNullValue(httpContext.Request.QueryString.Value);
            AgentCatalogue catalogue = new();

            StringBuilder builder = new();
            builder.AppendLine("<h1>Sitemap</h1>");
            builder.AppendLine($"<p>The home page has these same links with pictures and charm. This page is for crawlers and other list enthusiasts.</p>");
            builder.AppendLine($"<p class=\"slim\"><a href=\"/\">Home</a></p>");
            builder.AppendLine($"<p><a href=\"/notices\">Notices</a></p>");
            AppendAgentGroupsHtml(builder, catalogue);

            string fragment = builder.ToString();

            string? html = PageRenderer.RenderHtml(executionContext.RequestContext, fragment, query, null, HeroImage);

            return Results.Text(html, "text/html");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    private static void AppendAgentSitemapLocations(List<string> locs, string domain, AgentCatalogue catalogue)
    {
        foreach (AgentGroup group in catalogue.Groups)
        {
            foreach (AgentEntry agent in group.Agents)
            {
                locs.Add(SitemapLoc(domain, $"/{agent.Slug}"));
            }
        }
    }

    private static void AppendAgentGroupsHtml(StringBuilder builder, AgentCatalogue catalogue)
    {
        foreach (AgentGroup group in catalogue.Groups)
        {
            if (group.Agents.Count == 0)
            {
                continue;
            }

            builder.AppendLine($"<h2>{group.Title}</h2>");
            builder.AppendLine("<ul>");

            foreach (AgentEntry agent in group.Agents)
            {
                builder.AppendLine($"<li><a href=\"/{agent.Slug}\">{agent.Name}</a></li>");
            }

            builder.AppendLine("</ul>");
        }
    }

    private static string SitemapLoc(string domain, string path)
    {
        return $"""
                <url>
                    <loc>https://{domain}{path}</loc>
                </url>
                """;
    }
}
